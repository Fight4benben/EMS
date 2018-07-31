var Statistic = (function(){

	function _statistic(){

		this.show = function(){
			var url = "/api/ItemStatistic";

			getDataFromServer(url,"");
		}

		function getDataFromServer(url,params){
			EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				try{
					showBuilds(data);
					showEstimateMonth(data);
					showActualMonth(data);
					showEstimateYear(data);
					showActualYear(data);
				}catch(e){
					
				}finally{
					EMS.Loading.hide();
				}
				
			}).fail(function(e){
				EMS.Tool.statusProcess(e.status);
			});

		}

		function showBuilds(data){
			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");

			$("#buildinglist").change(function(){
				var buildId = $(this).val();
				var tempDate = new Date();
				var date = tempDate.getFullYear()+"-"+(tempDate.getMonth()+1)+"-"+tempDate.getDate();

				var url = "/api/ItemStatistic";

				getDataFromServer(url,"buildId="+buildId+"&date="+date);
			});
		}

		function showEstimateMonth(data){
			for (var i = 2; i <= 4; i++) {
				$("#statisticTable>tbody>tr").eq(1).children('td').eq(i).html("-");
			}

			if(!data.hasOwnProperty('monthPlanData'))
				return;

			$.each(data.monthPlanData, function(key, val) {
				if(val.id=="01000"){
					$("#statisticTable>tbody>tr").eq(1).children('td').eq(2).html(val.value);
				}else if(val.id=="02000"){
					$("#statisticTable>tbody>tr").eq(1).children('td').eq(3).html(val.value);
				}else if(val.id=="03000"){
					$("#statisticTable>tbody>tr").eq(1).children('td').eq(4).html(val.value);
				}
			});
		}

		function showActualMonth(data){
			for (var i = 1; i <= 3; i++) {
				$("#statisticTable>tbody>tr").eq(2).children('td').eq(i).html("-");
			}

			if(!data.hasOwnProperty('monthRealData'))
				return;

			$.each(data.monthRealData, function(key, val) {
				if(val.id=="01000"){
					$("#statisticTable>tbody>tr").eq(2).children('td').eq(1).html(val.value);
				}else if(val.id=="02000"){
					$("#statisticTable>tbody>tr").eq(2).children('td').eq(2).html(val.value);
				}else if(val.id=="03000"){
					$("#statisticTable>tbody>tr").eq(2).children('td').eq(3).html(val.value);
				}
			});
		}

		function showEstimateYear(data){
			for (var i = 2; i <= 4; i++) {
				$("#statisticTable>tbody>tr").eq(3).children('td').eq(i).html("-");
			}

			if(!data.hasOwnProperty('yearPlanData'))
				return;

			$.each(data.yearPlanData, function(key, val) {
				if(val.id=="01000"){
					$("#statisticTable>tbody>tr").eq(3).children('td').eq(2).html(val.value);
				}else if(val.id=="02000"){
					$("#statisticTable>tbody>tr").eq(3).children('td').eq(3).html(val.value);
				}else if(val.id=="03000"){
					$("#statisticTable>tbody>tr").eq(3).children('td').eq(4).html(val.value);
				}
			});
		}

		function showActualYear(data){
			for (var i = 1; i <= 3; i++) {
				$("#statisticTable>tbody>tr").eq(4).children('td').eq(i).html("-");
			}

			if(!data.hasOwnProperty('yearRealData'))
				return;

			$.each(data.yearRealData, function(key, val) {
				if(val.id=="01000"){
					$("#statisticTable>tbody>tr").eq(4).children('td').eq(1).html(val.value);
				}else if(val.id=="02000"){
					$("#statisticTable>tbody>tr").eq(4).children('td').eq(2).html(val.value);
				}else if(val.id=="03000"){
					$("#statisticTable>tbody>tr").eq(4).children('td').eq(3).html(val.value);
				}
			});
		}

	};

	return _statistic;
})();

jQuery(document).ready(function($) {

	$("#seenergy").attr("class","start active");
	$("#se_statistic").attr("class","active");
	
	var statistic = new Statistic();

	statistic.show();
});