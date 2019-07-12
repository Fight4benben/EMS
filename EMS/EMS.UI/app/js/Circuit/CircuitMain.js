'use strict';
var CircuitMain = (function(){
	function _circuitMain(){

		this.show = function(){
			var url = "/api/circuitoverview";

			var buildId=$.cookie('buildId');
			if(buildId==undefined || buildId==null || buildId == "null")
				getDataFromServer(url,"");
			else
				getDataFromServer(url,"buildId="+buildId+"&a=&b=&c=");
		}

		this.init = function(){
			initCircuitEvent();
		}

		var trendDatas={};

		function getDataFromServer(url,params){
			EMS.Loading.show($("#main-content").parent('div'));
			$.getJSON(url, params, function(data) {
				//console.log(data);
				if(data.hasOwnProperty('message'))
					location = "/Account/Login";
				try{
					showBuildList(data);
					showEnergyButtons(data);
					showCircuits(data);
					showCompareInfo(data);
					showLoadingCurve(data);
					showTrendData(data);
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
				var url = "/api/circuitoverview";
				getDataFromServer(url,"buildId="+$("#buildinglist").val())
			});
		};

		function initCircuitEvent(){
			$("#circuits").change(function(event) {
				
				var url = "/api/circuitoverview";
				getDataFromServer(url,"buildId="+$("#buildinglist").val()+"&energyCode="+"&circuitId="+$(this).val())
			});
		}

		//显示分类列表
		function showEnergyButtons(data){

			if(!data.hasOwnProperty('energys'))
				return;

			$("#te_surveyBtns").html("");

			$.each(data.energys, function(index, val) {

				switch(val.energyItemCode){
					case "01000":
						$("#te_surveyBtns").append('<acronym title="电"><button class="btn btn-elc" code="'+val.energyItemCode+'" type="button"></button></acronym>');
					break;
					case "02000":
						$("#te_surveyBtns").append('<acronym title="水"><button class="btn btn-water" code="'+val.energyItemCode+'" type="button"></button></acronym>');
					break;
					case "13000":
						$("#te_surveyBtns").append('<acronym title="光伏"><button class="btn btn-solar" code="'+val.energyItemCode+'" type="button"></button></acronym>');
					break;
					case "03000":
						$("#te_surveyBtns").append('<acronym title="燃气"><button class="btn btn-gas" code="'+val.energyItemCode+'" type="button"></button></acronym>');
					break;
					case "40000":
						$("#te_surveyBtns").append('<acronym title="蒸汽"><button class="btn btn-steam" code="'+val.energyItemCode+'" type="button"></button></acronym>');
					break;
					default:
						$("#te_surveyBtns").append('<acronym title="'+val.energyItemName+'"><button class="btn btn-empty" code="'+val.energyItemCode+'" type="button">'+
							val.energyItemName.substring(0,1)+'</button></acronym>');
				}
			});

			$("#te_surveyBtns button").eq(0).css('background-color','#F08500');
			$("#te_surveyBtns button").click(function(event) {
				$("#te_surveyBtns button").css('background-color','#969696');
				var $this = $(this);
				$this.css('background-color','#F08500');
				if($this.attr('code') == "13000"){
					$("#today span").html('当日发电')
					$("#curMonth span").html("当月发电")
				}else if($this.attr('code') == "01000"){
					$("#today span").html('当日用电')
					$("#curMonth span").html("当月用电")
				}else{
					$("#today span").html('当日能耗')
					$("#curMonth span").html("当月能耗")
				}
				switch ($this.attr('code')) {
					case '01000':
					case '13000':
						$("#CodeName").html('环比(单位：kW·h)');
						break;
					case '02000':
							$("#CodeName").html('环比(单位：T)');
						break;
					case '03000':
					case '40000':
					case '20000':
							$("#CodeName").html('环比(单位：m³)');
						break;
					case '04000':
					case '05000':
							$("#CodeName").html('环比(单位：MJ)');
						break;
					default:
						break;
				}
				var url = "/api/circuitoverview";
				getDataFromServer(url,"buildId="+$("#buildinglist").val()+"&energyCode="+$this.attr('code'))
			});
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
					$("#dayTrend>p").eq(0).text(diff.toFixed(1));
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
					$("#monthTrend>p").eq(1).text(((diff/lastMonthValue)*100).toFixed(1)+"%");

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
			clearElementContent($("#p_max"));
			clearElementContent($("#p_min"));
			clearElementContent($("#p_avg"));

			if(!data.hasOwnProperty('loadData'))
				return;

			if(data.loadData.length ==0)
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

			var max = _.max(values);
			var min = _.min(values);
			var avg = _.sum(values)/values.length;
			$("#p_max").html(max.toFixed(1));
			$("#p_min").html(min.toFixed(1));
			$("#p_avg").html(avg.toFixed(1));
		}

		//显示趋势曲线
		function showTrendData(data){

			clearElementContent($("#history-line"));

			var id =$(".history-time-select")[0].id;

			if(data.hasOwnProperty('last48HoursData')){
				trendDatas.hourValues = data.last48HoursData;

				bindClick($("#trend_hours"),"hourValues","H");
			}

			if(data.hasOwnProperty('last31DaysData')){
				trendDatas.dayValues = data.last31DaysData;

				bindClick($("#trend_days"),"dayValues","D");
			}

			if(data.hasOwnProperty('last12MonthData')){
				trendDatas.monthValues = data.last12MonthData;
				bindClick($("#trend_months"),"monthValues","M");
			}

			if(data.hasOwnProperty('last3YearData')){
				trendDatas.yearValues = data.last3YearData;
				bindClick($("#trend_years"),"yearValues","Y");
			}


			switch(id){
				case "trend_hours":
					showTrendDataById("H","last48HoursData",data);
				break;
				case "trend_days":
					showTrendDataById("D","last31DaysData",data);
				break;
				case "trend_months":
					showTrendDataById("M","last12MonthData",data);
				break;
				case "trend_years":
					showTrendDataById("Y","last3YearData",data);
				break;
			}


		}

		function showTrendDataById(type,dataName,data){
			if(!data.hasOwnProperty(dataName))
				return;

			if(data[dataName].length ==0)
				return;

			var barData = data[dataName];
			barData.sort(EMS.Tool.sortByObjTime);

			var times = [];
			var values = [];

			$.each(barData, function(index, val) {
				var currentTime = new Date(val.time);
				switch(type){
					case "H":
						times.push(currentTime.toLocaleDateString() + " "+currentTime.getHours());
					break;
					case "D":
						times.push(currentTime.toLocaleDateString());
					break;
					case "M":
						times.push(currentTime.getFullYear() + "/"+ currentTime.getMonth());
					break;
					case "Y":
						times.push(currentTime.getFullYear());
					break;
				}
				values.push(val.value);
			});

			var series={
				type:'bar',
				data:values
			};

			var grid ={
                left: 30,
                right: 10,
                top:5,
                bottom:25
            };

			EMS.Chart.showBar(echarts,$("#history-line"),undefined,times,series,grid,"default");
		}

		//绑定48，31天12月等click事件
		function bindClick($Trend,trendName,type){
			$Trend.click(function(event) {
				var $this = $(this);

				if($this[0].id == $(".history-time-select")[0].id)
					return;

				$(".history-time>div").removeClass('history-time-select');

				$this.addClass('history-time-select');

				showTrendDataById(type,trendName,trendDatas);
			});
		}

		//清空DIV中的内容
		function clearElementContent($element){
			$element.html("-");
		}

	};

	return _circuitMain;
})();

jQuery(document).ready(function($) {

	$("#flenergy").attr("class","start active");
	$("#trunk").attr("class","active");
	
	var main = new CircuitMain();

	main.init();

	main.show();
});