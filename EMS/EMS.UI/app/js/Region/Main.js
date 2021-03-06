var RegionMain = (function(){

	function _main(){

		this.show=function(){
			var url = "/api/RegionMain";

			var buildId=$.cookie('buildId');
			if(buildId==undefined || buildId==null || buildId == "null")
				getDataFromServer(url,"");
			else
				getDataFromServer(url,"buildId="+buildId+"&a=&b=");

		};

		this.initDom = function(){
			$("#energylist").change(function(event) {
				var url = "/api/RegionMain";
				var filter = energys.filter(function(item){
					return item.energyItemCode == $("#energylist").val();
				});

				if(filter.length>0)
					unit = filter[0].energyItemUnit;
				else 
					unit = "";
				getDataFromServer(url,"buildId="+$("#buildinglist").val()+
					"&energyCode="+$("#energylist").val());
			});
		}

		var rankData=[];
		var energys;
		var unit;

		function getDataFromServer(url,params){
			EMS.Loading.show($("#main-content").parent('div'));
			$.getJSON(url, params, function(data) {
				//console.log(data);
				if(data.hasOwnProperty('message'))
					location = "/Account/Login";
				try{
					//console.log(data);
					showBuildList(data);
					showEnergys(data);
					showCompare(data);
					showRank(data);
					showPie(data);
					showStackBar(data);
				}catch(e){

				}finally{
					EMS.Loading.hide($("#main-content").parent('div'));
				}

				
			}).fail(function(e){
				EMS.Tool.statusProcess(e.status);
			});
		}

		//显示建筑列表
		function showBuildList(data){

			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");

			if($.cookie('buildId') != undefined && $.cookie('buildId')!=null)
				$("#buildinglist").val($.cookie("buildId"));

			$("#buildinglist").change(function(event) {
				var buildId = $("#buildinglist").val();
				$.cookie("buildId",buildId,{path:'/'});
				var url = "/api/RegionMain";
				getDataFromServer(url,"buildId="+$("#buildinglist").val())
			});
		};

		function showEnergys(data){
			if(!data.hasOwnProperty('energys'))
				return;

			energys = data.energys;

			unit = energys[0].energyItemUnit;

			EMS.DOM.initSelect(data.energys,$("#energylist"),"energyItemName","energyItemCode");
		}

		//显示当日昨日对比
		function showCompare(data){

			$("#compareBar").html("");

			if(!data.hasOwnProperty('compareValues'))
				return;

			var today=[];
			var yesterday=[];
			var names=[];
			var dataZoom;

			$.each(data.compareValues, function(key, val) {
				
				if($.inArray(val.name, names)<0){
					var count =names.push(val.name);
					var arr = data.compareValues.filter(function(innerobj){
						return innerobj.name == val.name;
					});

					$.each(arr, function(index, value) {
						var date = new Date(value.time);
						if(date.getDate() == new Date().getDate()){
							today[count-1] = value.value;
						}else{
							yesterday[count-1] = value.value;
						}
					});
				}

				
			});

			var series=[
				{
					name: '今日',
	            	type: 'bar',
	            	data:today
				},{
					name: '昨日',
	            	type: 'bar',
	            	data:yesterday
				}
			];

			if(names.length >6){
				dataZoom = [
		            {
		            	type:'slider',
		                show: true,
		                start:0,
		                end:100*(6/names.length),
		                yAxisIndex: 0,
		                filterMode: 'filter',
		                width: 15,
		                height: '80%',
		                showDataShadow: false,
		                left: '0%'
		            }
		        ];
			}


			EMS.Chart.showLandscapeBar(echarts,$("#compareBar"),['今日','昨日'],names,series,undefined,dataZoom);
		};

		//显示排名信息
		function showRank(data){
			//清除按钮，清除图表
			$("#regionBtns").html("");
			$("#rankBar").html("");

			if(!data.hasOwnProperty('rankValues'))
				return;

			var ids = [];

			$.each(data.rankValues, function(key, val) {
				if($.inArray(val.classifyID, ids)<0){
					ids.push(val.classifyID);
					$("#regionBtns").append('<div class="col-sm-12 col-xs-3" value="'+
						val.classifyID+'"><span>'+val.classifyName+'</span></div>');

					var regionFilter = data.rankValues.filter(function(item) {
						return item.classifyID == val.classifyID;
					});

					var obj = {name:[],data:[]};
					$.each(regionFilter, function(index, value) {
						obj.name[index] = value.name;
						obj.data[index] = value.value;
					});

					rankData[val.classifyID] = obj;
				}
			});

			if(ids.length ==0)
				return;

			$("#regionBtns>div").eq(0).addClass('le-time-select');

			drawRank($(".le-time-select").attr('value'));

			$("#regionBtns>div").click(function(event) {
				var $this = $(this);
				var code = $this.attr('value');

				if($this.hasClass('le-time-select'))
					return;

				$(".le-time-select").removeClass('le-time-select');

				$this.addClass('le-time-select');

				drawRank(code);
			});
		}

		function drawRank(code){

			var name = ["", "", "", "", "", "", "", "", "", ""];
			var value =[0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

			$.each(rankData[code].name, function(key, val) {
				name[key] = val;
			});

			$.each(rankData[code].data, function(key, val) {
				value[key] = val;
			});

			var series = {
            	type: 'bar',
            	data:value
			}
			EMS.Chart.showBar(echarts,$("#rankBar"),undefined,name,series);
		}

		//显示饼图
		function showPie(data){

			$("#monthPie").html("");

			if(!data.hasOwnProperty("pieValues"))
				return;

			if(data.pieValues.length <=0)
				return;

			var names=[];
			var values=[];
			$.each(data.pieValues, function(key, val) {
				values.push({name:val.name,value:val.value});
				names.push(val.name);
			});

			if(names.length>5)
				EMS.Chart.showPie(echarts,$('#monthPie'),names,values,"区域用能",false);
			else
				EMS.Chart.showPie(echarts,$('#monthPie'),names,values,"区域用能",true);
		}

		function showStackBar(data){
			$("#monthStackBar").html("");

			if(!data.hasOwnProperty('stackValues'))
				return;

			if(data.stackValues.length<=0)
				return;

			var curDate = new Date();
			curDate.setDate(curDate.getDate()-31);
			var startDate = new Date(curDate.getFullYear(),curDate.getMonth(),curDate.getDate());
			var times=[];
			var dataArray = [];
			var names=[];

			//先生成时间数组
			for (var i=0; i<=30; i++) {
				i==0?1+1:startDate.setDate(startDate.getDate()+1);
				
				var tempDate = new Date(startDate.getFullYear(),startDate.getMonth(),startDate.getDate(),0,0,0);

				times.push((tempDate.getMonth()+1)+"-"+(tempDate.getDate()));
			}

			//遍历数据，根据时间填充数据
			if(data.stackValues.length >0 ){
				$.each(data.stackValues, function(key, val) {
					var outArray = dataArray.filter(function(itemData,index) {
						return itemData.name == val.name; 
					});

					//判断当前时间在time数组中的位置
					var locationTime = new Date(val.time);
					var locationX = (locationTime.getMonth()+1)+'-'+locationTime.getDate();
					var locationIndex = $.inArray(locationX,times);

					if(outArray.length <= 0){
						if(locationIndex!=-1){
							var tempObj = {name:val.name,type:'bar',stack:'itemStack',data:[]};
							tempObj.data[locationIndex] = val.value.toFixed(1);
							dataArray.push(tempObj);

						}
					}else{
						$.each(dataArray, function(index, value) {
							if(value.name == val.name){
								value.data[locationIndex]=val.value.toFixed(1);
							}
						});
					}
				});
			}

			$.each(dataArray, function(index, val) {
				names.push(val.name);
			});
			var legendFlag = false;

			names.length>5 ? legendFlag = false : legendFlag = true;
			EMS.Chart.showStackBar(echarts,$("#monthStackBar"),names,times,dataArray,unit,legendFlag);
		}

	};

	return _main;

})();

jQuery(document).ready(function($) {

	$("#leenergy").attr("class","start active");
	$("#le_survey").attr("class","active");
	
	var regionMain = new RegionMain();
	regionMain.initDom();
	regionMain.show();
});