'use strict';
var CircuitMain = (function(){
	function _circuitMain(){

		this.show = function(){
			var url = "/api/circuitoverview";
			getDataFromServer(url,"");
		}

		function getDataFromServer(url,params){
			$.getJSON(url, params, function(data) {
				console.log(data);
				if(data.hasOwnProperty('message'))
					location = "/Account/Login";

				showBuildList(data);
				showEnergyButtons(data);
				showCircuits(data);
				showCompareInfo(data);
				showLoadingCurve(data);
			});
		}

		//显示建筑列表
		function showBuildList(data){

			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");
		};

		//显示分类列表
		function showEnergyButtons(data){

			if(!data.hasOwnProperty('energys'))
				return;

			$("#te_surveyBtns").html("");

			$.each(data.energys, function(index, val) {

				switch(val.energyItemCode){
					case "01000":
						$("#te_surveyBtns").append('<acronym title="电"><button class="btn btn-elc" type="button"></button></acronym>');
					break;
					case "02000":
						$("#te_surveyBtns").append('<acronym title="水"><button class="btn btn-water" type="button"></button></acronym>');
					break;
					case "13000":
						$("#te_surveyBtns").append('<acronym title="光伏"><button class="btn btn-solar" type="button"></button></acronym>');
					break;
					default:
						$("#te_surveyBtns").append('<acronym title="'+val.energyItemName+'"><button class="btn btn-empty" type="button">'+
							val.energyItemName.substring(0,1)+'</button></acronym>');
				}
			});

			$("#te_surveyBtns button").eq(0).css('background-color','#F08500');
		};

		//填充支路信息
		function showCircuits(data){

			if(!data.hasOwnProperty('circuits'))
				return;

			EMS.DOM.initSelect(data.circuits,$("#circuits"),"circuitName","circuitId");
		}

		//显示同环比信息
		function showCompareInfo(data){
			clearElementContent($("#today>p"));
			clearElementContent($("#yesterday>p"));
			clearElementContent($("#dayTrend>p").eq(0));
			clearElementContent($("#dayTrend>p").eq(1));

			clearElementContent($("#curMonth>p"));
			clearElementContent($("#lastMonth>p"));
			clearElementContent($("#monthTrend>p").eq(0));
			clearElementContent($("#monthTrend>p").eq(1));

			if(data.hasOwnProperty("momDayData")){
				var todayValue;
				var yesterdayValue;
				$.each(data.momDayData, function(key, val) {

					if(!val.hasOwnProperty('time'))
						return true;//相当于continue

					if(new Date(val.time).toLocaleDateString() == new Date().toLocaleDateString()){
						$("#today>p").text(val.value);
						todayValue = val.value;
					}else{
						$("#yesterday>p").text(val.value);
						yesterdayValue = val.value;
					}
				});

				if(todayValue !== undefined && yesterdayValue !== undefined){
					var diff = todayValue- yesterdayValue ;
					$("#dayTrend>p").eq(0).text(diff);
					$("#dayTrend>p").eq(1).text(((diff/yesterdayValue)*100).toFixed(1)+"%");
					diff>0 ? $("#dayTrend>span").html('<img src="/app/img/survey-up.png">'):$("#dayTrend>span").html('<img src="/app/img/survey-down.png">');

				}else{
					$("#dayTrend>p").eq(0).text("-");
					$("#dayTrend>p").eq(1).text("-");
				}
			}

			if(data.hasOwnProperty('momMonthData')){
				var curMonthValue;
				var lastMonthValue;
				$.each(data.momMonthData, function(key, val) {

					if(!val.hasOwnProperty('time'))
						return true;//相当于continue

					if(new Date(val.time).getMonth() == new Date().getMonth()){
						$("#curMonth>p").text(val.value);
						curMonthValue = val.value;
					}else{
						$("#lastMonth>p").text(val.value);
						lastMonthValue = val.value;
					}
				});

				if(curMonthValue !== undefined && lastMonthValue !== undefined){
					var diff = curMonthValue- lastMonthValue ;
					$("#monthTrend>p").eq(0).text(diff.toFixed(1));
					$("#monthTrend>p").eq(1).text(((diff/yesterdayValue)*100).toFixed(1)+"%");

					diff>0 ? $("#monthTrend>span").html('<img src="/app/img/survey-up.png">'):$("#monthTrend>span").html('<img src="/app/img/survey-down.png">');
				}else{
					$("#monthTrend>p").eq(0).text("-");
					$("#monthTrend>p").eq(1).text("-");
				}
			}
		}

		//显示负荷曲线
		function showLoadingCurve(data){

			$("#survey_powerline").html("");

			if(!data.hasOwnProperty('loadData'))
				return;
			var loadData = data.loadData;

			loadData.sort(EMS.Tool.sortByObjTime);

			var times = [];
			var values = [];

			$.each(loadData, function(key, val) {
				var currentTime = new Date(val.time);
				times.push(currentTime.getHours()+":"+currentTime.getMinutes());
				values.push(val.value);
			});

			var series = {
				type:'line',
				data:values
			};

			EMS.Chart.showLine(echarts,$("#survey_powerline"),undefined,times,series);

		}

		//显示趋势曲线
		function showTrendData(data){

		}

		//清空DIV中的内容
		function clearElementContent($element){
			$element.html("-");
		}

	};

	return _circuitMain;
})();

jQuery(document).ready(function($) {
	
	var main = new CircuitMain();

	main.show();
});