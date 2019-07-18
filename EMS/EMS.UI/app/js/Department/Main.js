var DepartmentMain = (function(){

	function _main(){

		this.show=function(){
			var url = "/api/DeptOverview";

			var buildId=$.cookie('buildId');
			if(buildId==undefined || buildId==null || buildId == "null")
				getDataFromServer(url,"");
			else
				getDataFromServer(url,"buildId="+buildId+"&a=&b=");
		};

		this.initDom = function(){
			$("#energylist").change(function(event) {
				var url = "/api/DeptOverview";
				var filter = energys.filter(function(item){
					return item.energyItemCode == $("#energylist").val();
				});

				if(filter.length>0){
					unit = filter[0].energyItemUnit;
					switch (unit) {
						case '千瓦时':
							unit = 'kW·h'
							break;
						case '吨':
							unit = 'T'
							break;
						case '立方米':
							unit = 'm³'
							break;
						case '兆焦':
							unit = 'MJ'
							break;
						default:
							break;
					}
				}
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
					showExamine(data);
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
				var url = "/api/DeptOverview";
				getDataFromServer(url,"buildId="+$("#buildinglist").val())
			});
		};

		function showEnergys(data){
			if(!data.hasOwnProperty('energys'))
				return;

			energys = data.energys;

			unit = energys[0].energyItemUnit;
			switch (unit) {
				case '千瓦时':
					unit = 'kW·h'
					break;
				case '吨':
					unit = 'T'
					break;
				case '立方米':
					unit = 'm³'
					break;
				case '兆焦':
					unit = 'MJ'
					break;
				default:
					break;
			}

			EMS.DOM.initSelect(data.energys,$("#energylist"),"energyItemName","energyItemCode");
		}

		//显示当日昨日对比
		function showCompare(data){

			$("#compareBar").html("");

			if(!data.hasOwnProperty('momDay'))
				return;

			var today=[];
			var yesterday=[];
			var names=[];
			var dataZoom;

			$.each(data.momDay, function(key, val) {
				
				if($.inArray(val.name, names)<0){
					var count =names.push(val.name);
					var arr = data.momDay.filter(function(innerobj){
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

		function showExamine(data){

			$("#planChart").html("");

			if(!data.hasOwnProperty('rankByYear') && !data.hasOwnProperty('planValue'))
				return;
			var ids=[];
			var names=[];
			var obj1 = generateDeptList(ids,names,data.rankByYear);
			var obj2 = generateDeptList(obj1.id,obj1.name,data.planValue);

			ids = obj2.id;
			names = obj2.name;

			var actualValues= [];
			var planValues=[];

			$.each(ids, function(key, val) {
				
				var tempAct =data.rankByYear.filter(function(item){
					return item.id == val;
				});

				var tempPlan = data.planValue.filter(function(item){
					return item.id == val;
				});

				if(tempPlan.length>0)
					planValues[key] = tempPlan[0].value;
				else
					planValues[key] = undefined;

				if(tempAct.length>0){
					if(tempPlan.length == 0 ){
						actualValues[key] = tempAct[0].value;
						return true;
					}

					if(tempPlan[0].value == undefined){
						actualValues[key] = tempAct[0].value;
						return true;
					}
				
					if(parseFloat(tempAct[0].value)>parseFloat(tempPlan[0].value))
						actualValues[key] = {value:tempAct[0].value,itemStyle:{normal:{color:"#FF0000"}}};
					else
						actualValues[key] = tempAct[0].value;
				}
				else
					actualValues[key] = undefined;

			});

			showChart(planValues,actualValues,names);
		}

		function showChart(planData,actualData,names){
			var series = [
				{
                    name: '计划值',
                    type: 'bar',
                    data: planData,

                },{
                    name: '实际值',
                    type: 'bar',
                    data: actualData,
                }
			];

			var legend={
				data: ['计划值', '实际值'],
                top:'top'
			};

			var xAxis=names;

			var grid ={
                left: 80,
                right: 10,
                top:5,
                bottom:25
            };

			EMS.Chart.showBar(echarts,$("#planChart"),legend,xAxis,series,grid);
		}


		function generateDeptList(ids,names,list){

			$.each(list, function(key, val) {
				if($.inArray(val.id, ids)<0){
					ids.push(val.id);
					names.push(val.name);
				}
			});

			return {id:ids,name:names};
		}

		//显示饼图
		function showPie(data){

			$("#monthPie").html("");

			if(!data.hasOwnProperty("last31DayPieChart"))
				return;

			if(data.last31DayPieChart.length <=0)
				return;

			var names=[];
			var values=[];
			$.each(data.last31DayPieChart, function(key, val) {
				values.push({name:val.name,value:val.value});
				names.push(val.name);
			});

			EMS.Chart.showPie(echarts,$('#monthPie'),names,values,"区域用能");
		}

		function showStackBar(data){
			$("#monthStackBar").html("");

			if(!data.hasOwnProperty('last31Day'))
				return;

			if(data.last31Day.length<=0)
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
			if(data.last31Day.length >0 ){
				$.each(data.last31Day, function(key, val) {
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

			EMS.Chart.showStackBar(echarts,$("#monthStackBar"),names,times,dataArray,unit);
		}

	};

	return _main;

})();

jQuery(document).ready(function($) {

	$("#deenergy").attr("class","start active");
	$("#de_survey").attr("class","active");
	
	var departmentMain = new DepartmentMain();
	departmentMain.initDom();
	departmentMain.show();
});