var Status = (function(){

	function _status(){

		function initDom(){
			$("#buildinglist").change(function(event) {
				var buildId = $(this).val();

				var params = "buildId="+buildId;
				getDataFromServer(baseurl,params);
			});

			$("#energys").change(function(event) {
				var energyCode = $(this).val();
				var buildId = $("#buildinglist").val();

				var params = "buildId="+buildId+"&energyCode="+energyCode;
				getDataFromServer(baseurl,params);
			});

			$("#Disconnect").on("change",function(){
				var buildId = $("#buildinglist").val();
				var energyCode = $("#energys").val();
				var type = 0;
				if($(this)[0].checked){
					type=1;
				}else{
					type=0;
				}

				var params = "buildId="+buildId+"&energyCode="+energyCode+"&type="+type;
				getDataFromServer(baseurl,params);
			})
		}
		var baseurl="/api/MeterConnectState";

		this.show = function(){
			initDom();
			getDataFromServer(baseurl,"");
		}

		function getDataFromServer(url,params){
			EMS.Loading.show();

			$.getJSON(url,params, function(data) {
				//console.log(data);
				try{
					showBuilds(data);
					showEnergys(data);
					showTable(data);
				}catch(e){

				}finally{
					EMS.Loading.hide();
				}
			}).fail(function(e){
				EMS.Tool.statusProcess(e.status);
				EMS.Loading.hide();
			});
		}

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
				{field:'name',title:'仪表名称'},
				{field:'collectionName',title:'网关名称'},
				{field:'states',title:'通讯状态'},
				{field:'disConnectTime',title:'中断时刻'},
				{field:'diffDate',title:'累计中断时间'}
			];


			var height = $("#alarmTable").height();
			$("#alarmTable").html('<table></table>');
			$("#alarmTable>table").attr('data-height',height);

			EMS.DOM.showTable($("#alarmTable>table"),columns,data.connectStates,{striped:true,classes:'table table-border',
				rowStyle:function(row,index){
					if(row.states=="通讯中断")
						return {classes:'danger'};
					else
						return {};
			}});
		}

	}

	return _status;

})();

jQuery(document).ready(function($) {
	
	$("#menu_state").attr("class","start active");
	$("#menu_communicate").attr("class","active");

	var status = new Status();

	status.show();

});