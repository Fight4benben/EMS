var ItemMain = (function(){

	function _itemMain(){

		this.show = function(){
			var url = "/api/ItemOverview";
			getDataFromServer(url,"");
		};

		var rankData={};

		function getDataFromServer(url,params){
			EMS.Loading.show($("#main-content").parent('div'));
			$.getJSON(url, params, function(data) {
				//console.log(data);
				if(data.hasOwnProperty('message'))
					location = "/Account/Login";
				try{
					//console.log(data);
					showBuildList(data);
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

			$("#buildinglist").change(function(event) {
				
				var url = "/api/ItemOverview";
				getDataFromServer(url,"buildId="+$("#buildinglist").val())
			});
		};

		//显示当日昨日对比
		function showCompare(data){

			$("#compareBar").html("");

			if(!data.hasOwnProperty('energyItemMomDay'))
				return;

			var today=[];
			var yesterday=[];
			var names=[];

			$.each(data.energyItemMomDay, function(key, val) {
				
				if($.inArray(val.name, names)<0){
					var count =names.push(val.name);
					var arr = data.energyItemMomDay.filter(function(innerobj){
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


			EMS.Chart.showLandscapeBar(echarts,$("#compareBar"),['今日','昨日'],names,series);
		};

		function showRank(data){
			//清楚图表数据
			$("#seRankbar").html("");

			if(!data.hasOwnProperty('energyItemRankByMonth'))
				return;

			if(data.energyItemRankByMonth.length <= 0)
				return;

			//隐藏分项按钮
			$("#seBtns>div").hide();
			var light = data.energyItemRankByMonth.filter(filterItem,"01A00");
			var air = data.energyItemRankByMonth.filter(filterItem,"01B00");
			var power = data.energyItemRankByMonth.filter(filterItem,"01C00");
			var special =data.energyItemRankByMonth.filter(filterItem,"01D00");

			//数据处理，并存储到当前对象全局变量中,并显示相关按钮
			if(light.length>0){
				$("#seBtns").find('div[value="01A00"]').show();
				rankData["01A00"]=getItemData(light);
			}

			if(air.length>0){
				$("#seBtns").find('div[value="01B00"]').show();
				rankData["01B00"] = getItemData(air);
			}

			if(power.length>0){
				$("#seBtns").find('div[value="01C00"]').show();
				rankData["01C00"] = getItemData(power);
			}

			if(special.length>0){
				$("#seBtns").find('div[value="01D00"]').show();
				rankData["01D00"] = getItemData(special);
			}

			//显示第一条数据
			$("#seBtns>div:visible").eq(0).addClass('se-time-select');

			drawRankBar($(".se-time-select").attr('value'));

			//按钮click事件
			$("#seBtns>div").click(function(event) {
				var $this = $(this);
				var code = $this.attr('value');

				if($this.hasClass('se-time-select'))
					return;

				$(".se-time-select").removeClass('se-time-select');

				$this.addClass('se-time-select');


				drawRankBar(code);

			});
		}

		function drawRankBar(code){
			
			var name = ["", "", "", "", "", "", "", "", "", ""];
			var value =[0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

			$.each(rankData[code].names, function(key, val) {
				name[key] = val;
			});

			$.each(rankData[code].data, function(key, val) {
				value[key] = val;
			});

			var series = {
            	type: 'bar',
            	data:value
			}
			EMS.Chart.showBar(echarts,$("#seRankbar"),undefined,name,series);
		}

		function filterItem(item){
			
			return item.energyItemCode == this;
		}

		function getItemData(array){
			var obj={names:[],data:[]};
			$.each(array.sort(itemValueSort), function(key, val) {
				obj.names.push(val.name);
				obj.data.push(val.value);
			});

			return obj;
		}

		//对数组进行降序排列
		function itemValueSort(a,b){
			return a.value<b.value;
		}

		function showPie(data){

			$("#pastMonthPie").html("");

			if(!data.hasOwnProperty("energyItemLast31DayPieChart"))
				return;

			if(data.energyItemLast31DayPieChart.length <=0)
				return;

			var names=[];
			var values=[];
			$.each(data.energyItemLast31DayPieChart, function(key, val) {
				values.push({name:val.name,value:val.value});
				names.push(val.name);
			});

			EMS.Chart.showPie(echarts,$('#pastMonthPie'),names,values,"分项用能");
		}

		function showStackBar(data){
			$("#pastMonthStackBar").html("");

			if(!data.hasOwnProperty('energyItemLast31Day'))
				return;

			if(data.energyItemLast31Day.length<=0)
				return;

			var curDate = new Date();
			curDate.setDate(curDate.getDate()-31);
			var startDate = new Date(curDate.getFullYear(),curDate.getMonth(),curDate.getDate());
			var times=[];
			var dataArray = [];
			var names=[];

			for (var i=0; i<=31; i++) {

				startDate.setDate(startDate.getDate()+i);
				var tempDate = new Date(startDate.getFullYear(),startDate.getMonth(),startDate.getDate(),0,0,0);

				times.push((tempDate.getMonth()+1)+"-"+(tempDate.getDate()));

				var array = data.energyItemLast31Day.filter(function(item){
					var inDate = new Date(item.time);
					return (tempDate.getFullYear() == inDate.getFullYear()) && 
						(tempDate.getMonth() == inDate.getMonth())
					&& (tempDate.getDate() == inDate.getDate());
				});

				//console.log(array);
				if(array.length >0 ){
					$.each(array, function(key, val) {

						var outArray = dataArray.filter(function(itemData,index) {
							return itemData.name == val.name; 
						});

						if(outArray.length <= 0){
							dataArray.push({name:val.name,type:'bar',stack:'itemStack',data:[val.value.toFixed(1)]});
						}else{
							$.each(dataArray, function(index, value) {
								if(value.name == val.name)
									value.data.push(val.value.toFixed(1));
							});
						}
					});
				}

				startDate.setDate(startDate.getDate()-i);
			}

			$.each(dataArray, function(index, val) {
				names.push(val.name);
			});

			EMS.Chart.showStackBar(echarts,$("#pastMonthStackBar"),names,times,dataArray,'kW·h');
		}

	};

	return _itemMain;

})();

jQuery(document).ready(function($) {

	$("#seenergy").attr("class","start active");
	$("#se_trunk").attr("class","active");

	var item = new ItemMain();

	item.show();
});