'use strict';
var CircuitReport = (function(){

	function _circuitReport(){

		function initEnergyBtns(){
			$("#te_countBtns").html("");
		}

		function initDateTime(){
			EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
		}

		this.initDom = function(){
			initDateTime();
		};

		this.showReport = function(url,params){
			getDataFromServer(url,params);
		};

		function getDataFromServer(url,params){
			jQuery.getJSON(url,params, function(data) {
			  console.log(data);
			  showBuilds(data);
			  showEnergys(data);
			  showTreeview(data);
			});
			
		};

		function showBuilds(data){
			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");
		};

		function showEnergys(data){
			
			if(!data.hasOwnProperty('energys'))
				return;

			initEnergyBtns();

			$.each(data.energys, function(key, val) {

				switch(val.energyItemCode){
					case "01000":
						$("#te_countBtns").append('<acronym title="电"><button class="btn btn-elc" tyle="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "02000":
						$("#te_countBtns").append('<acronym title="水"><button class="btn btn-water" tyle="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "13000":
						$("#te_countBtns").append('<acronym title="光伏"><button class="btn btn-solar" tyle="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
				}
			});
		};

		function showTreeview(data){
			if(!data.hasOwnProperty('treeView'))
				return;

			$("#treeview").html("");
			$("#treeview").parent('div').css('overflow','auto');
			$("#treeview").width(350);
			$("#treeview").parent('div').height($('.build-info').height() - 243);
			//$("#treeview").parent('div').height(800);

			EMS.DOM.initTreeview(data.treeView,$("#treeview"),{
				showIcon: true,
				showCheckbox: true,
				showBorder:false,
				levels:2})
		};

		function showTable(data){
			
		}

	};

	return _circuitReport;

})();

jQuery(document).ready(function($) {
	
	var circuitReport = new CircuitReport();

	circuitReport.initDom();
	circuitReport.showReport("/api/CircuitReport/report","");

});