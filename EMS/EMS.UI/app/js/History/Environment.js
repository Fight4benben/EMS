var Environment = (function(){
	function _environment(){

		this.show = function(){
			init();
			var url = "/api/THParam";
			getData(url,"");

		}

		function init(){
			EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));

			$("#daycalendarBox").change(function(event) {
				var date = $(this).val();
				var buildId = $("#buildinglist").val();
				var circuitId = $("#environmentlist").val();
				var url="/api/THParam";
				var params = "buildId="+buildId+"&circuitID="+circuitId+"&startDay="+date;
				getData(url,params);
			});
		}

		function getData(url,params){
			EMS.Loading.show();
			$.getJSON(url, params, function(data) {

				try{
					showBuildList(data);
					showEnvironmentMeterList(data);
					showLines(data);
				}catch(exception){
					console.log(exception);
				}finally{
					EMS.Loading.hide();
				}

			}).fail(function(e){
				EMS.Tool.statusProcess(e.status);
				EMS.Loading.hide();
			});
		}

		function showBuildList(data){

			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");

			$("#buildinglist").change(function(event) {
				var buildId = $(this).val();
				var url="/api/THParam";
				var params = "buildId="+buildId;

				getData(url,params);
			})
		}

		function showEnvironmentMeterList(data){
			if(!data.hasOwnProperty('treeList'))
				return;

			EMS.DOM.initSelect(data.treeList,$("#environmentlist"),"name","id");

			$("#environmentlist").change(function(event) {
				var id = $(this).val();
				var url="/api/THParam";
				var params = "buildId="+$("#buildinglist").val()+"&circuitID="+id;
				getData(url,params);
			});

			getData("/api/THParam","buildId="+$("#buildinglist").val()+"&circuitID="+$("#environmentlist").val());
		}

		function showLines(data){
			//var tempArray
			if(!data.hasOwnProperty('data'))
				return;

			var tempTimes = [];
			var humiTimes=[];
			var tempValues=[];
			var humiValues=[];


			$.each(data.data, function(key, val) {
				
				if(val.paramName=="温度"){
					$.each(val.values, function(index, data) {
					
						tempTimes.push(EMS.Tool.toTimeString(new Date(data.time)));
						if(data.value==-9999){
							tempValues.push('-');
						}else{
							tempValues.push(data.value);
						}
					});
				}else if(val.paramName=="湿度"){
					$.each(val.values, function(index, data) {
					
						humiTimes.push(EMS.Tool.toTimeString(new Date(data.time)));
						if(data.value==-9999){
							humiValues.push('-');
						}else{
							humiValues.push(data.value);
						}
					});
				}

			});

			EMS.Chart.showLine(echarts,$("#temperature"),undefined,tempTimes,{type:'line',data:tempValues});
			EMS.Chart.showLine(echarts,$("#humidity"),undefined,humiTimes,{type:'line',data:humiValues});
		}

	}

	return _environment;
})();

jQuery(document).ready(function($) {
	$("#flenergy").attr("class","start active");
	$("#cir_env").attr("class","active");
	var environment = new Environment();

	environment.show();

});