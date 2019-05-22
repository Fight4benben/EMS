var Home = (function(){
	function _home(){

		//不同分类下的小时数据与对比数据
		var energyArray=[];

		var lineMode;

		this.initDom = function(){
			initEnergyClassifyTable();
			initDateTimePicker();
			initBuildsSelect();
			initBuildInfo();
			initChartLine();
			initChart();
		};
		
		this.showHome=function(url,params){
			var buildId=$.cookie('buildId');
			if(buildId==undefined || buildId==null || buildId == "null")
				getDataFromServer(url,params);
			else
				getDataFromServer(url,"buildId="+buildId);
		};

		function initDateTimePicker(){
			EMS.DOM.initDateTimePicker("CURRENTDATE",new Date(),$("#calendar"),$("#box"));

			$("#box").change(function(event) {
				var params = 'buildId='+$("#buildinglist").val()+'&date='+$("#box").val();

				getDataFromServer("/api/homepage/home",params);
			});
		};

		function initBuildsSelect(){
			$("#buildinglist").change(function(){
				var buildId = $(this).val();
				var date = $("#box").val();
				$.cookie("buildId",buildId,{path:'/'});
				initChartLine();
				getDataFromServer("/api/homepage/home","buildId="+buildId+"&date="+date);
			});
		}

		function initBuildInfo(){
			$("#buildinginfo h4").text("");
			$("#buildinginfo span").text("");
			$("#TransCnt h4").text('-');
			$("#InstallCap h4").text("-");
			$("#OperateCap h4").text("-");
			$("#metersTotal h4").text("-");
		}

		function initEnergyClassifyTable(){
			$("#energytable tbody").html("");
		}

		function initChart(){
			$("#main_pie").html("");
		};

		function initButtons(){
			$("#btngroup").html("");
		}

		function initChartLine(){
			$("#main_line").html("");
		}

		//从服务器端获取数据的方法
		function getDataFromServer(url,params){
			EMS.Loading.show();
			jQuery.getJSON(url,params, function(data) {
			  //console.log(data);
			  try{
				  showBuildList(data);
				  showBuildInfo(data);
				  showEnergyClassifyTable(data);
				  showEnergyItemPie(data);
				  showCompareEnergyButtonAndShowLineAndCompare(data);
				  showMD(data);
				  showMdValues(data);
			  }catch(e){

			  }finally{
			  	EMS.Loading.hide();
			  }
			}).fail(function(e){
				EMS.Tool.statusProcess(e.status);
			});
		};

		//显示建筑列表
		function showBuildList(data){

			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");

		};

		function getCurrentBuildId(){
			return $("#buildinglist").val();
		}

		//显示当前建筑物详细信息
		function showBuildInfo(data){
			initBuildInfo();
			if(!data.hasOwnProperty('currentBuild'))
				return;

			var currentBuild = data.currentBuild;

			$("#buildinginfo h4").text(currentBuild.buildName);
			$("#buildinginfo span").eq(0).text(currentBuild.buildAddr);
			$("#buildinginfo span").eq(1).text("建筑面积"+currentBuild.totalArea+"平方米");

			$("#TransCnt h4").text(currentBuild.transCount);
			$("#InstallCap h4").text(currentBuild.installCapacity);
			$("#OperateCap h4").text(currentBuild.operateCapacity);
			$("#metersTotal h4").text(currentBuild.designMeters);

			$("#buildinglist").val(currentBuild.buildId);

		};

		//显示分类能耗表格
		function showEnergyClassifyTable(data){

			initEnergyClassifyTable();

			if(!data.hasOwnProperty('energyClassify'))
				return;

			if(data.energyClassify.length==0)
				return;

			$.each(data.energyClassify, function(key, val) {

				$("#energytable tbody").append('<tr><td>'+val.energyItemName+'</td><td>'+
					(isNaN(val.monthValue)?'-':val.monthValue.toFixed(2))+'</td><td>'+
					(isNaN(val.yearValue)?'-':val.yearValue.toFixed(2))+'</td><td>'+val.unit+'</td></tr>');
			});
		}

		//显示饼图
		function showEnergyItemPie(data){

			initChart();

			if(!data.hasOwnProperty('energyItems'))
				return;

			if(data.energyItems.length==0)
				return;

			var values=[];
			var names=[];

			$.each(data.energyItems, function(key, val) {
				values.push({name:val.energyItemName,value:val.value});
				names.push(val.energyItemName);
			});

			EMS.Chart.showPie(echarts,$('#main_pie'),names,values,"分项用能");
		}

		function showCompareEnergyButtonAndShowLineAndCompare(data){

			initButtons();

			if(!data.hasOwnProperty('lineMode'))
				lineMode = "Normal";
			else
				lineMode = data.lineMode;

			if(!data.hasOwnProperty('compareValues'))
				return;

			if(data.compareValues.length==0)
				return;

			energyArray=[];

			$.each(data.compareValues, function(key, val) {
				switch(val.energyItemCode){
					case "01000":
						$("#btngroup").append('<div class="col-sm-1 col-xs-2"><acronym title="电"><button class="btn btn-elc" value="01000" type="button"></button></acronym></div>');
					    break;
					case "02000":
						$("#btngroup").append('<div class="col-sm-1 col-xs-2"><acronym title="水"><button class="btn btn-water" value="02000" type="button"></button></acronym></div>');
					    break;
					case "03000":
						$("#btngroup").append('<div class="col-sm-1 col-xs-2"><acronym title="燃气"><button class="btn btn-gas" value="03000" type="button"></button></acronym></div>');
					    break;
					case "13000":
						$("#btngroup").append('<div class="col-sm-1 col-xs-2"><acronym title="光伏"><button class="btn btn-solar" value="13000" type="button"></button></acronym></div>');
                        break;
                    case "05000":
                        $("#btngroup").append('<div class="col-sm-1 col-xs-2"><acronym title="集中供冷"><button class="btn btn-empty" value="05000" type="button">冷量</button></acronym></div>');
                        break;
				}

				var hourToday = data.hourValues.todayValues.filter(function(hourValue){
					return hourValue.energyItemCode == val.energyItemCode;
				});
				var hourYesterday = data.hourValues.yesterdayValues.filter(function(hourValue){
					return hourValue.energyItemCode == val.energyItemCode;
				});
				energyArray.push({code:val.energyItemCode,compareValues:{today:val.todayValue,yesterday:val.yesterdayValue},
					hourValues:{today:hourToday,yesterday:hourYesterday}});
			});

			//console.log(energyArray);

			$.each($("#btngroup button"), function(index, val) {
				$(val).attr('value')==energyArray[0].code?$(val).css('background-color','#F08500'):1+1;
			});

			$("#btngroup button").click(function(event) {
				var $this = $(this);
				$("#btngroup button").css('background-color','#969696');
				$(this).css('background-color','#F08500');
				//console.log($(this).attr("value"));

				$.each(energyArray, function(key, val) {
					if(val.code == $this.attr("value")){
						if(lineMode=="Special")
							showPreviewLine(energyArray[key]);
						else
							showLine(energyArray[key]);
						showCompareValue(val);
					}
				});
			});


			//var 
			if(lineMode == "Special")
				showPreviewLine(energyArray[0]);
			else
				showLine(energyArray[0]);

			showCompareValue(energyArray[0]);
		};

		function showLine(data){
			initChartLine();
			var hours=['0时', '1时', '2时', '3时', '4时', '5时', '6时', '7时', '8时', '9时', '10时', '11时', '12时', 
					'13时', '14时', '15时', '16时', '17时', '18时', '19时', '20时', '21时', '22时', '23时'];

			var todayValue=[];
			var yesterValue=[];


			$.each(hours, function(key, val) {
				
				var currentValue = data.hourValues.today.filter(function(hourValue){
					var timeString = hourValue.valueTime.replace('-','/');
					var date = new Date(timeString);
					return key ==  date.getHours();
				});

				var yesValue = data.hourValues.yesterday.filter(function(hourValue){
					var timeString = hourValue.valueTime.replace('-','/');
					var date = new Date(timeString);
					return key ==  date.getHours();
				});

				if(currentValue.length!=0)
					todayValue[key] = currentValue[0].value.toFixed(2);

				if(yesValue.length !=0)
					yesterValue[key] = yesValue[0].value.toFixed(2);
			});

			var series = [
				{
					name: '昨日',
                    type: 'line',
                    data: yesterValue
				},{
					name: '今日',
                    type: 'line',
                    data: todayValue
				}
			];

			EMS.Chart.showLine(echarts,$("#main_line"),['昨日', '今日'],hours,series);
		};

		function isSelectedDateToday(){
			var date = $("#box").val();
			var year = parseInt(date.split('-')[0]);
			var month = parseInt(date.split('-')[1]);
			var day = parseInt(date.split('-')[2]);

			var curDate = new Date();
			if(curDate.getFullYear() ==year && 
				(curDate.getMonth()+1) == month && 
				day == curDate.getDate())
				return true;
			else
				return false;

		}

		function showPreviewLine(data){
			//initChartLine();
			var hours=['0时', '1时', '2时', '3时', '4时', '5时', '6时', '7时', '8时', '9时', '10时', '11时', '12时', 
					'13时', '14时', '15时', '16时', '17时', '18时', '19时', '20时', '21时', '22时', '23时'];

			var todayValue=[];
			var yesterValue=[];
			var todayHour = new Date().getHours();

			$.each(hours, function(key, val) {
				
				var currentValue = data.hourValues.today.filter(function(hourValue){
					var timeString = hourValue.valueTime.replace('-','/');
					var date = new Date(timeString);
					return key ==  date.getHours();
				});

				var yesValue = data.hourValues.yesterday.filter(function(hourValue){
					var timeString = hourValue.valueTime.replace('-','/');
					var date = new Date(timeString);
					return key ==  date.getHours();
				});

				if(currentValue.length!=0){
					if(key === todayHour-1)
						yesterValue[key] = currentValue[0].value.toFixed(2);

					if(key <= todayHour){
						todayValue[key] = currentValue[0].value.toFixed(2);
						
					}
					else
						todayValue[key] = undefined;
				}

				if(yesValue.length !=0){
					if(key >= todayHour)
						yesterValue[key] = yesValue[0].value.toFixed(2);
				}
			});

			var up=[];
			var low=[];
			$.each(yesterValue, function(index, val) {
				if(val != undefined){
					if(index == new Date().getHours()-1){
						up[index] = (val*0).toFixed(1);
						low[index] = (val*1).toFixed(1);
					}else{
						up[index] = (val*0.2).toFixed(1);
						low[index] = (val*0.9).toFixed(1);
					}
					
				}
			});

			var options={
		        tooltip: {
	                //trigger: 'axis'
	                trigger: 'axis',
		            formatter: '{a2}:{c2}<br/>{a3}:{c3}'
	            },
	            legend: {
	                data: ['昨日', '今日'],
	                bottom:'bottom'
	            },
	            grid: {
	                left: 50,
	                right: 10,
	                top:35,
	                bottom:'20%'
	            },
	            xAxis: {
	                type: 'category',
	                boundaryGap: false,
	                data: hours
	            },
	            yAxis: {
	                type: 'value',
	                nameLocation:'end',
	                axisLabel: {
	                	formatter: '{value}'
	            	}
	            },
		        series: [
			        	{
			        		name:'L',
			        		type:'line',
			        		data:low,
			        		lineStyle:{
			        			normal:{
			        				opacity:0
			        			}
			        		},
			        		stack: 'TEST',
			        		symbol: 'none'
			        	},
			        	{
							name: 'U',
				            type: 'line',
				            data: up,
				            lineStyle: {
				                normal: {
				                    opacity: 0
				                }
				            },
				            areaStyle: {
				                normal: {
				                    color: '#ccc'
				                }
				            },
				            stack: 'TEST',
				            symbol: 'none'
			        	},
			            {
			                name: '昨日',
			                type: 'line',
			                lineStyle:{
			                	normal:{
			                		type:'dashed'
			                	}
			                	
			                },
			                data: yesterValue,

			            },
			            {
			                name: '今日',
			                type: 'line',
			                data: todayValue,
			            }
			        ],
		        	color:['#1E90FF','#FF8C00','#FF0000','#9ACD32', '#91c7ae','#749f83',  '#ca8622', '#bda29a','#6e7074', '#546570', '#c4ccd3']
		    };

		    var myLine = echarts.init($("#main_line").get(0),'macarons');

		    myLine.setOption(options);
		}

		function showCompareValue(data){

			$("#updown").html("");

			var tValue;
			var yValue;
			if(data.compareValues.today==undefined){
				tValue=0;
				$("#hb_today").text('-');
			}else{
				tValue=data.compareValues.today;
				$("#hb_today").text(data.compareValues.today);
			}

			if(data.compareValues.yesterday==undefined){
				yValue=0;
				$("#hb_yesterday").text('-');
			}else{
				yValue=data.compareValues.yesterday;
				$("#hb_yesterday").text(data.compareValues.yesterday);
			}

			$("#hb_rate p:first").html((tValue-yValue).toFixed(1));

			if(yValue==0){
				//$("#hb_rate p:first").html("");
				$("#hb_rate p:last").html("");
			}else{
				$("#hb_rate p:last").html(((tValue-yValue)*100/yValue).toFixed(1)+'%');
			}

			if(tValue-yValue>0){
				$("#updown").append('<img src="/app/img/up.png" />');
			}else{
				$("#updown").append('<img src="/app/img/down.png" />');
			}

			switch(data.code){
				case "01000":
					$("#hb_lbltoday").text("当日用电(单位kW·h)");
					$("#hb_yesterday").prev('div').text('昨日同期(单位kW·h)');
				break;
				case "02000":
					$("#hb_lbltoday").text("当日用水(单位 t)");
					$("#hb_yesterday").prev('div').text('昨日同期(单位 t)');
				break;
				case "03000":
					$("#hb_lbltoday").text("当日用气(单位 m³)");
					$("#hb_yesterday").prev('div').text('昨日同期(单位 m³)');
				break;
				case "13000":
					$("#hb_lbltoday").text("当日发电(单位kW·h)");
					$("#hb_yesterday").prev('div').text('昨日同期(单位kW·h)');
				break;
			}
		}

		function showMD(data){
			if(!data.hasOwnProperty('showMD')){
				$("#index_md").hide();
				$("#index_md img").hide();
				return;
			}

			$("#index_md ul").html("");
		}

		function showMdValues(data){
			$("#index_md ul").html("");

			if(!data.hasOwnProperty('mdValues'))
				return;

			if(data.mdValues.length == 0){
				$("#index_md").hide();
				$("#index_md img").hide();
			}else{
				$("#index_md").show();
				$("#index_md img").show();
			}


			$.each(data.mdValues, function(key, val) {
				$("#index_md ul").append('<li>'+val.name+":"+val.value + "kW</li>");
			});
		}

	};
	return _home;
})();
jQuery(document).ready(function($) {

	$("body").addClass('page-header-fixed page-sidebar-fixed page-footer-fixed');
	$('.header').addClass('navbar-fixed-top');

	$("#fistpage").attr("class","start active");
	$("#fistpage").attr("class","active");
	
	var home = new Home();
	home.initDom();
	home.showHome("/api/homepage/home","");

});