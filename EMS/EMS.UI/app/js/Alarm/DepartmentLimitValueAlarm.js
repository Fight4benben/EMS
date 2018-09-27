var OutOfWork = (function(){

	function _outOfWork(){

		this.show = function(){
			initDateTime();
			initButton();

			var url = "/api/AlarmDepartmentOverLimit";

			getDataFromServer(url,"");
		};

		function initDateTime(){
			EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
		}

		function initButton(){
			$("#Load").click(function(event) {
				var url="/api/AlarmDepartmentOverLimit";

				var params = "buildId="+$("#buildinglist").val()+"&date="+
				$("#daycalendarBox").val();

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
					showTable(data);
				}catch(exception){

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
				{field:'name',title:'名称',width:'250px'},
				{field:'timePeriod',title:'非工作时间段'},
				{field:'value',title:'能耗值'},
				{field:'limitValue',title:'限定报警值'},
				{field:'diffValue',title:'差值',cellStyle:function(value,row,index){

					if(row.limitValue!=0){
						var rate = row.diffValue/row.limitValue;
						if(rate>0 && rate<=0.5)
							return {classes:'warning'}
						else if(rate>0.5)
							return {classes:'danger'}
					}
				}}
			];

			var rows = [];

			$.each(data.energyAlarmData, function(key, val) {
				var row={};
				row.name = val.name;
				row.value = val.value;
				row.timePeriod = val.timePeriod;
				row.limitValue = val.limitValue;
				row.diffValue = val.diffValue;

				rows.push(row);
			});

			var height = $("#alarmTable").height();
			$("#alarmTable").html('<table></table>');
			$("#alarmTable>table").attr('data-height',height);

			EMS.DOM.showTable($("#alarmTable>table"),columns,rows,{striped:true,classes:'table table-border'});

			$("table td").css('font-size','15px');
		}


	};

	return _outOfWork;

})();

jQuery(document).ready(function($) {

	$("#alarm").attr("class","start active");
	$("#deptOutofworkAlarm").attr("class","active");
	
	var outOfWork = new OutOfWork();

	outOfWork.show();
});