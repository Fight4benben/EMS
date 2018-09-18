﻿var Average = (function(){
	function _average(){
		this.show = function(){
			initDom();

			var url="/api/DepartmentAverage";

			getDataFromServer(url,"");
		}

		function initDom(){
			//季度选择框默认不显示
			showOrHideSeason(false);
			//初始化日期
			initDate("MM");

			initDateType();

			initSearchButton();
		}

		function showOrHideSeason(isShow){
			isShow?$(".season-alarm").show():$(".season-alarm").hide();
		}

		function initDate(type){
			switch(type){
				case "MM"://月
				EMS.DOM.initDateTimePicker('YEARMONTH',new Date(),$("#dayCalendar"),$("#daycalendarBox"),{format:'yyyy-mm',
									        language: 'zh-CN',
									        autoclose: 1,
									        startView: 3,
									        minView: 3,
									        forceParse: false,
									        pickerPosition: "bottom-left"});
				break;
				case "QQ"://季度
				case "YY":
				EMS.DOM.initDateTimePicker('YEAR',new Date(),$("#dayCalendar"),$("#daycalendarBox"),{format:'yyyy',
									        language: 'zh-CN',
									        autoclose: 1,
									        startView: 4,
									        minView: 4,
									        forceParse: false,
									        pickerPosition: "bottom-left"});
				break;

			}
		}

		function initDateType(){
			$("#dateType").change(function(event) {
				var typeValue = $(this).val();
				initDate(typeValue);

				typeValue === "QQ" ? showOrHideSeason(true) : showOrHideSeason(false);

			});
		}

		function initSearchButton(){
			$("#searchButton").click(function(event) {
				
				var buildId = $("#buildinglist").val();
				var date; 

				var type =$("#dateType").val();
				if(type=="QQ"){
					var season = $("#season").val();
					date = $("#daycalendarBox").val()+"-"+EMS.Tool.appendZero(parseInt(season)*3);
				}else if(type=="YY"){
					date = $("#daycalendarBox").val()+"-01-01";
				}else 
					date = $("#daycalendarBox").val()+"-01";

				var energyCode = $("#energys").val();

				var url = "/api/DepartmentAverage";
				var params = "buildId="+buildId+"&date="+date+"&type="+type+"&energyCode="+energyCode;

				getDataFromServer(url,params);

			});			
		}

		function getDataFromServer(url,params){
			EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				//console.log(data);
				try{
					showBuilds(data);
					showEnergys(data);
					//setTarget(data);
					showTable(data);
				}catch(e){

				}finally{
					EMS.Loading.hide();
				}
				
			}).fail(function(e){
				EMS.Tool.statusProcess(e.status);
				EMS.Loading.hide();
			});
		};

		function showBuilds(data){
			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");
		}

		function showEnergys(data){
			if(!data.hasOwnProperty('energys'))
				return;

			EMS.DOM.initSelect(data.energys,$("#energys"),"energyItemName","energyItemCode");
		}

		function showTable(data){
			var columns=[
				{field:'name',title:'部门名称',width:'250px'},
				{field:'totalValue',title:'总能耗'},
				{field:'totalPeople',title:'部门人数'},
				{field:'peopleAvg',title:'部门人均能耗'},
				{field:'totalArea',title:'部门面积'},
				{field:'areaAvg',title:'部门单位面积能耗'}
			];
			var rows = [];

			$.each(data.averageData, function(key, val) {
				var row={};
				row.id = val.id;
				row.name = val.name;
				row.totalValue = val.totalValue;
				row.totalPeople = val.totalPeople;
				row.peopleAvg = val.peopleAvg.toFixed(2);
				row.totalArea = val.totalArea.toFixed(2);
				row.areaAvg = val.areaAvg.toFixed(2);

				rows.push(row);	
			});

			var height = $("#alarmTable").height();
			$("#alarmTable").html('<table></table>');
			$("#alarmTable>table").attr('data-height',height);

			EMS.DOM.showTable($("#alarmTable>table"),columns,rows,{striped:true,classes:'table table-border'});
		}

	};

	return _average;
})();

jQuery(document).ready(function($) {
	
	$("#deenergy").attr("class","start active");
	$("#de_average").attr("class","active");

	var average = new Average();

	average.show();

});