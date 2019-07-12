
var AlarmRecord = (function(){

	function _alarmRecord(){
        this.show = function(){
			initDateTime();
            initButton();
			var url = "/api/MeterAlarmLog";

			getDataFromServer(url,"");
        };
        
        function initDateTime(){
            EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#StartdayCalendar"),$("#StartdaycalendarBox"));
            EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#EnddayCalendar"),$("#EnddaycalendarBox"));
        }
        function initButton(){
			$("#Load").click(function(event) {
				if($("#alarmTypeList").val() == 0){
                    var params = "buildID="+$("#buildinglist").val()+"&beginDate="+
                    $("#StartdaycalendarBox").val()+"&endDate="+$("#EnddaycalendarBox").val()+
                    "&pageIndex="+1+"&pageSize="+100;
                }else{
                    var params = "buildID="+$("#buildinglist").val()+"&beginDate="+
                $("#StartdaycalendarBox").val()+"&endDate="+$("#EnddaycalendarBox").val()+
                "&alarmType="+$("#alarmTypeList").val()+"&pageIndex="+1+"&pageSize="+100;
                }
				getDataFromServer('/api/MeterAlarmLog',params);
			});
		}
        function getDataFromServer(url,params){
			EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				try{
					showBuilds(data);
                    showalarmType(data)
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
        // kW·h
        function showBuilds(data){
			if(!data.hasOwnProperty('builds'))
				return;
                EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");

                $("#buildinglist").change(function(event) {
    
                    if($("#alarmTypeList").val() == 0){
                        var params = "buildID="+$("#buildinglist").val()+"&beginDate="+
                        $("#StartdaycalendarBox").val()+"&endDate="+$("#EnddaycalendarBox").val()+
                        "&pageIndex="+1+"&pageSize="+100;
                    }else{
                        var params = "buildID="+$("#buildinglist").val()+"&beginDate="+
                    $("#StartdaycalendarBox").val()+"&endDate="+$("#EnddaycalendarBox").val()+
                    "&alarmType="+$("#alarmTypeList").val()+"&pageIndex="+1+"&pageSize="+100;
                    }
                    getDataFromServer('/api/MeterAlarmLog',params);
                });
            }
            function showalarmType(data){
                if(!data.hasOwnProperty('alarmType'))
                    return;
                var array = data.alarmType;
                //array.push({id:0,typeName:'无'})
                array.unshift({id:0,typeName:'无'})
                EMS.DOM.initSelect(array,$("#alarmTypeList"),"typeName","id");
    
                $("#alarmTypeList").change(function(event) {
                    if($("#alarmTypeList").val() == 0){
                        var params = "buildID="+$("#buildinglist").val()+"&beginDate="+
                        $("#StartdaycalendarBox").val()+"&endDate="+$("#EnddaycalendarBox").val()+
                        "&pageIndex="+1+"&pageSize="+100;
                    }else{
                        var params = "buildID="+$("#buildinglist").val()+"&beginDate="+
                    $("#StartdaycalendarBox").val()+"&endDate="+$("#EnddaycalendarBox").val()+
                    "&alarmType="+$("#alarmTypeList").val()+"&pageIndex="+1+"&pageSize="+100;
                    }
                    getDataFromServer('/api/MeterAlarmLog',params);
                });
        }

        function showTable(data){

            var columns=[
				{field:'buildName',title:'建筑名称'},
                {field:'meterName',title:'仪表名称'},
                {field:'collectionName',title:'网关名称'},
                {field:'meterParamName',title:'参数名称'},
                {field:'typeName',title:'报警类型'},
                {field:'normalRange',title:'正常值范围'},
                {field:'alarmTime',title:'报警时间'},
                {field:'alarmValue',title:'报警值'},
            ];
            var tableRows = [];
            if(data.alarmLogs!=null && data.alarmLogs.length>0){
                $.each(data.alarmLogs, function (index, val) { 
                     var row = {};
                     row.id = val.id;
                     row.buildName = val.buildName;
                     row.collectionName = val.collectionName;
                     row.meterParamName = val.meterParamName;
                     row.typeName = val.typeName;
                     if(row.typeName == '网关离线'){
                        row.meterName = '-';
                     }else{
                        row.meterName = val.meterName;
                     }
                     row.normalRange = val.normalRange;
                     row.alarmTime = val.alarmTime;
                     row.alarmValue = val.alarmValue
                     
                     tableRows.push(row)
                });
                
            }
            var height = $("#alarmTable").height();
			$("#alarmTable").html('<table></table>');
			$("#alarmTable>table").attr('data-height',height);

			EMS.DOM.showTable($("#alarmTable>table"),columns,tableRows,{striped:true,classes:'table table-border'});

           showPage(data.pageInfos);
        }
        function  showPage(data){
            $("#paginationOld").pagination({
                currentPage:data.currentPage,
                totalPage:data.totalPage,
                isShow:false,
                count:10,
                callback: function(current) {
                    var url = "/api/MeterAlarmLog";
                    var buildID = $("#buildinglist").val();
                    var alarmType =$("#alarmTypeList").val();
                    var beginDate = $("#StartdaycalendarBox").val();
                    var endDate = $("#EnddaycalendarBox").val();
                    var pageIndex = current;
                    if(alarmType == 0){
                        var params ="buildID="+buildID+"&beginDate="+beginDate+"&endDate="+endDate+"&pageIndex="+pageIndex+"&pageSize="+100;
                    }else{
                        var params ="buildID="+buildID+"&alarmType="+alarmType+"&beginDate="+beginDate+"&endDate="+endDate+"&pageIndex="+pageIndex+"&pageSize="+100;
                    }
                    $.ajax({
                        type :"GET",
                        url :url,
                        data :params,
                        async : false,
                        dataType :"json",
                        success :recieve
                    });
                    //返回数据处理
                    function recieve(data){
                        if(data!=null || data.length>0)
                        showTable(data);
                    }
                }
            })
        }
    }

	return _alarmRecord;

})();


jQuery(document).ready(function($) {

    $("#alarm").attr("class","start active");
    $("#alarmrecord").attr("class", "active");

    var alarmRecord = new AlarmRecord();

	alarmRecord.show();
});