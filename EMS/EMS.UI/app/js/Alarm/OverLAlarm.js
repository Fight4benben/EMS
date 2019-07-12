
var OverLAlarm = (function(){
    function _overLAlarm(){
        var baseUrl = '/api/MeterAlarmSetApi';
        var buidlsList;
        var array = [];
        var flatArray;
        var treeid;
        this.show = function(){
            getDataFromServer(baseUrl,"");
        };
        this.getSelectedInfo = function(){
            return selectedInfo;
        }
        function getDataFromServer(url,params){
            EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				try{
                    showBuilds(data);
                    showCode(data);
                    showTreeview(data);
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
        //生成建筑列表，追加到select中
		function showBuilds(data){
			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");
            buidlsList = data.builds;
			$("#buildinglist").change(function(event) {
                var buildId = $("#buildinglist").val();

				getDataFromServer("/api/MeterAlarmSetApi","BuildID="+buildId);
			});
		};
        function showCode(data){
            if(!data.hasOwnProperty('energys'))
				return;

            EMS.DOM.initSelect(data.energys,$("#codelist"),"energyItemName","energyItemCode");

            $("#codelist").change(function(event) {
                var buildId = $("#buildinglist").val();
                var code = $("#codelist").val();
				getDataFromServer("/api/MeterAlarmSetApi","BuildID="+buildId+"&code="+code);
			});
        };
        //递归遍历树
        function setName(obj){
            for(var i in obj){
                obj[i].name = obj[i].text;
                if(obj[i].nodes){
                    setName(obj[i].nodes)
                }
            }
            array.push(obj)
            //拍平数组
            flatArray = [].concat(...array)
        }
        
        //根据数据显示树状结构，如果不包含树状结构数据则不更新数据。
		function showTreeview(data){
			if(!data.hasOwnProperty('treeView'))
                return;
                
            setName(data.treeView)

			$("#treeview").html("");//更新树状结构之前先将该区域清空
			$("#treeview").parent('div').css('overflow','auto');
			$("#treeview").width(350);
			$(".count-info-te").height($("#main-content").height());
			//console.log($("#main-content").height());
			$("#treeview").height($(".count-info-te").height() - 300);

			$(".treeview .list-group").css('height','');
			
            treeid = data.treeView[0].id;
			EMS.DOM.initTreeview(data.treeView,$("#treeview"),{
				showIcon: true,
				showCheckbox: false,
				showBorder:false,
				levels:2,
				onNodeSelected:function(event,node){
					treeid = node.id;
					var buildId = $("#buildinglist").val();

                    var code = $("#codelist").val();
                    getDataFromServer("/api/MeterAlarmSetApi","BuildID="+buildId+"&code="+code+"&circuitID="+treeid);
				}
			});

			$("#treeview").treeview('selectNode',[0,{silent:true}]);
		};
       

        function showTable(data){
            if(!data.hasOwnProperty('data'))
                return;
            var columns=[
                {field:'binded',title:'是否启用',checkbox:'true'},
                {field:'buildID',title:'建筑编号'},
                {field:'meterID',title:'仪表编号'},
                {field:'paramName',title:'参数名称'},
                {field:'paramCode',title:'参数代码'},
                {field:'delay',title:'延时时间'},
                {field:'lowest',title:'低低限'},
                {field:'low',title:'低限'},
                {field:'high',title:'高限'},
                {field:'highest',title:'高高限'},
                {field:'setting',title:'设置'},
            ];
            var rows = [];

			$.each(data.data, function(key, val) {
                var row = {};

                val.state ==1 ? row.binded = true : row.binded = false;

				row.buildID = val.buildID;
                row.meterID = val.meterID;
                row.paramID = val.paramID;
                row.paramName = val.paramName;
                row.paramCode = val.paramCode;
                row.level = val.level;
                row.delay = val.delay;
                row.lowest = val.lowest;
                row.low = val.low;
                row.high = val.low;
                row.highest = val.highest;
                row.setting = '<button class="btn btn-warning setting" data-toggle="modal">设置</button>';
				rows.push(row);
            });
            var height = $(".content").height();
				$(".content").html('<table id="mainTable"></table>');
				$(".content>table").attr('data-height',height-7);

			EMS.DOM.showTable($(".content>table"),columns,rows,{striped:true,classes:'table table-border'});
            
            var $trs = $("table>tbody>tr");
            $("tbody>tr>td").css({
                'padding-top':'4px',
                'padding-bottom':'4px',
                'padding-left':'10px',
                'padding-right':'10px',
            });
            $trs.css('background','bluesky').removeClass('currentSelect');

            $("#mainTable").on('click-row.bs.table',function(e,row,$element){
                $(".currentSelect").css('background','white').removeClass('currentSelect');
                $element.css('background','#cee4f9').addClass('currentSelect')

                $(".setting").attr('data-target',"#myModal")
                selectedInfo = row
            });
        };

        $("#myModal").on('shown.bs.modal',function (e) { 
            var selectRow = selectedInfo;
            console.log(selectRow)
            $("#BuildID").val(selectRow.buildID);
            $.each(buidlsList,function(key,val){
                if(val.buildID === selectRow.buildID){
                    $("#BuildName").val(val.buildName);
                }
            });
            $.each(flatArray,function(key,val){
                if(val.id === treeid){
                    $("#MeterName").val(val.text);
                }
            });
            $("#MeterID").val(selectRow.meterID);
            $("#ParamID").val(selectRow.paramID);
            $("#ParamName").val(selectRow.paramName);
            $("#ParamCode").val(selectRow.paramCode);
            $("#Level").val(selectRow.level);
            $("#Delay").val(selectRow.delay);
            $("#Lowest").val(selectRow.lowest);
            $("#Low").val(selectRow.low);
            $("#High").val(selectRow.high);
            $("#Highest").val(selectRow.highest);
        });
        $('#myModal').on('hidden.bs.modal', function (){
            $('input').val('');
        });
        $("#bindBtn").click(function(){
            var BuildID = $("#BuildID").val();
            var MeterID = $("#MeterID").val();
            var ParamID = $("#ParamID").val();
            var ParamCode = $("#ParamCode").val();
            var Level = $("#Level").val();
            var Delay = $("#Delay").val();
            var Lowest = $("#Lowest").val();
            var Low = $("#Low").val();
            var High = $("#High").val();
            var Level = $("#Level").val();
            var Highest = $("#Highest").val();
            var State = $("#State").val();
            $.ajax({
                type: "post",
                url: baseUrl,
                data: {
                    BuildID:BuildID,
                    MeterID:MeterID,
                    ParamID:ParamID,
                    ParamCode:ParamCode,
                    Level:Level,
                    Delay:Delay,
                    Lowest:Lowest,
                    Low:Low,
                    High:High,
                    Highest:Highest,
                    State:State
                },
                success: function (response) {
                    if(response.state == '0'){
                        alert("设置成功！！");
                        getDataFromServer("/api/MeterAlarmSetApi","BuildID="+BuildID);
                        $("#myModal").modal('hide')
                    }
                }
            });
        });
    }
    return _overLAlarm;
})();





jQuery(document).ready(function($) {

	$("#settings").attr("class", "start active");
    $("#menu_overlimit_setting").attr("class","active");
    
    var overLAlarm = new OverLAlarm();
    
    overLAlarm.show();
});