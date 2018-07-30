var Statistic = (function(){

	function _statistic(){

		this.show = function(){
			var url = "/api/ItemStatistic";

			getDataFromServer(url,"");
		}

		function getDataFromServer(url,params){

			$.getJSON(url,params, function(data) {
				console.log(data);
				showBuilds(data);
			});

		}

		function showBuilds(data){
			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");
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