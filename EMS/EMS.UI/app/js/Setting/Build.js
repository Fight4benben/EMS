
var Build=(function(){

	function _build(){
        var baseUrl ='/api/BuildSetApi';
        this.getSelectedInfo = function(){
            return selectedInfo;
        }
        this.show = function(){
            getDataFromServer(baseUrl,"");
        }
        function getDataFromServer(url,params) {
            EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				try{
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
        function showTable(data){
            var columns=[
				{field:'buildName',title:'建筑名称'},
				{field:'buildAddr',title:'地址'},
                {field:'buildLong',title:'经度'},
                {field:'buildLat',title:'纬度'},
                {field:'totalArea',title:'总面积'},
                {field:'transCount',title:'变压器数量'},
                {field:'installCapacity',title:'安装容量'},
                {field:'operateCapacity',title:'运行容量'},
                {field:'designMeters',title:'仪表数量'},
                {field:'setting',title:'修改'},
            ];
            var tableRows = [];
            if(data.buildInfo!=null && data.buildInfo.length>0){
                $.each(data.buildInfo, function (index, val) { 
                     var row = {};
                     row.buildID = val.buildID;
                     row.buildName = val.buildName;
                     row.buildAddr = val.buildAddr;
                     row.buildLong = val.buildLong;
                     row.buildLat = val.buildLat;
                     row.totalArea = val.totalArea;
                     row.transCount = val.transCount;
                     row.installCapacity = val.installCapacity;
                     row.operateCapacity = val.operateCapacity;
                     row.designMeters = val.designMeters;
                     row.setting = '<button class="btn btn-warning setting" value="'+ val.buildID +'" data-toggle="modal">修改</button>';
                    
                     tableRows.push(row)
                });
            }
            var height = $("#table-user-menus").height();
				$("#table-user-menus").html('<table id="mainTable"></table>');
				$("#table-user-menus>table").attr('data-height',height);

            EMS.DOM.showTable($("#table-user-menus>table"),columns,tableRows,{striped:true,classes:'table table-border'});

            $("#mainTable").on('click-row.bs.table',function(e,row,$element){
                $(".currentSelect").css('background','white').removeClass('currentSelect');
                $element.css('background','#cee4f9').addClass('currentSelect')

                $(".setting").attr('data-target',"#myModal")
                selectedInfo = row
            });
        }
        $("#myModal").on('shown.bs.modal',function (e) { 
            var row = selectedInfo;
            $("#buildid").val(row.buildID);
            $("#buildName").val(row.buildName);
            $("#buildAddr").val(row.buildAddr);
            $("#buildLong").val(row.buildLong);
            $("#buildLat").val(row.buildLat);
            $("#totalArea").val(row.totalArea);
            $("#transCount").val(row.transCount);
            $("#installCapacity").val(row.installCapacity);
            $("#operateCapacity").val(row.operateCapacity);
            $("#designMeters").val(row.designMeters);
        });
        $("#bindBtn").click(function(){
            var buildID = $("#buildid").val();
            var buildName = $("#buildName").val();
            var buildAddr = $("#buildAddr").val();
            var buildLong = $("#buildLong").val();
            var buildLat = $("#buildLat").val();
            var totalArea = $("#totalArea").val();
            var transCount = $("#transCount").val();
            var installCapacity = $("#installCapacity").val();
            var operateCapacity = $("#operateCapacity").val();
            var designMeters = $("#designMeters").val();
            var numberOfPeople = '0';
            $.ajax({
                type: "POST",
                url: baseUrl,
                data: {
                    buildID:buildID,buildName:buildName,buildAddr:buildAddr,
                    buildLong:buildLong,buildLat:buildLat,totalArea:totalArea,transCount:transCount,
                    installCapacity:installCapacity,operateCapacity:operateCapacity,designMeters:designMeters,numberOfPeople:numberOfPeople
                },
                success: function (res) {
                    console.log(res)
                    if(res.resultState.details == 'OK'){
                        alert("修改成功！！");
                        getDataFromServer("/api/BuildSetApi","");
                        $("#myModal").modal('hide')
                    }
                }
            });
        })
    }
	return _build;

})();


jQuery(document).ready(function($) {
	
	$("#settings").attr("class", "start active");
    $("#menu_build_setting").attr("class", "active");

    var build = new Build();
    build.show();
});