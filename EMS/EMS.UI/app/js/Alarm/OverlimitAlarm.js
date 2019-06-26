
var OverAlarm = (function(){

    function _overAlarm(){
        var url = "/api/MeterAlarming";
        var params = "&pageIndex="+1+"&pageSize="+10;
        this.show = function(){
            
        }
        $('.go-top').click(function(){

            $(".go-top").attr('data-target','#myModalAlarm');
        });

        $("#myModalAlarm").on('shown.bs.modal',function (e) { 
            var url = "/api/MeterAlarming";
            var params = "&pageIndex="+1+"&pageSize="+10;
            getDataFromServer(url,params);
        });

        function getDataFromServer(url,params){

			$.getJSON(url, params, function(data) {
				
				try{
                    showData(data);
				}catch(e){

				}
			}).fail(function(e){
                console,log(e)
            });
        };

        $('.nav-tabs li').click(function(){
            　　$(this).addClass('active').siblings().removeClass('active');
            　　var _id = $(this).attr('data-id');
            　　$('.tabs-contents').find('#'+_id).addClass('active').siblings().removeClass('active');
            
            　　switch(_id){
            　　　　case "tabContent1":
                　　　　　　getDataFromServer(url,params);
            　　　　　　break;
            　　　　case "tabContent2":
                　　　　　　getDataFromServer(url,"");
            　　　　　　break;
            　　　　default:
                　　　　　　getDataFromServer(url,"");
            　　　　　　break;
            　　}
        });
        function showData(data){
            var columns=[
				{field:'buildName',title:'建筑名称'},
				{field:'meterName',title:'仪表名称'},
                {field:'meterParamName',title:'参数名称'},
                {field:'typeName',title:'报警类型'},
                {field:'normalRange',title:'正常值范围'},
                {field:'alarmTime',title:'报警时间'},
                {field:'alarmValue',title:'报警值'},
                {field:'isConfirm',title:'确认'}
            ];
            var tableRows = [];
            if(data.data!=null && data.data.length>0){
                $.each(data.data, function (index, val) { 
                     var row = {};
                     row.id = val.id;
                     row.buildName = val.buildName;
                     row.meterName = val.meterName;
                     row.meterParamName = val.meterParamName;
                     row.typeName = val.typeName;
                     row.normalRange = val.normalRange;
                     row.alarmTime = val.alarmTime;
                     row.alarmValue = val.alarmValue
                     if(val.isConfirm == 0){
                         row.isConfirm = '<button class="btn btn-warning " value="'+ val.id +'">确认</button>';
                     }
                     tableRows.push(row)
                });
                
            }
            var height = $("#tabContent1").height();
				$("#tabContent1").html('<table id="mainTable"></table>');
				$("#tabContent1>table").attr('data-height',height);

            EMS.DOM.showTable($("#tabContent1>table"),columns,tableRows,{striped:true,classes:'table table-border'});

            showPagtion(data.pageInfos);
        };
        function  showPagtion(data){
            BootstrapPagination($("#pagination"), {
                layoutScheme: "prevgrouppage,prevpage,pagenumber,nextpage,nextgrouppage",
                total: data.totalNumber,
                pageSize: data.pageSize,
                pageIndex: data.currentPage,
                pageGroupSize: 5,
                // pageInputTimeout: 800,
                // pageSizeList: [5, 10, 20, 50, 100],
                //当分页更改后引发此事件。
                pageChanged: function (pageIndex, pageSize) {
                    var url = "/api/MeterAlarming";
                    var pageIndex = pageIndex;
                    var params =  "&pageIndex="+pageIndex+"&pageSize="+10;
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
                            showData(data);
                    }
                }
            });
        }
        //定时任务
        setInterval(refreshAlarm,5000);
        function refreshAlarm(){
            
        }
	};
	return _overAlarm;
})();




jQuery(document).ready(function($) {

    var overAlarm = new OverAlarm();
    overAlarm.show();
});