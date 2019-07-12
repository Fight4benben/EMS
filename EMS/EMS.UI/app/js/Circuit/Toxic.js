
var Toxic = (function(){
    function _toxic(){
        this.show = function(url ,params ){
            getDataFromServer(url,params);
        }
        this.initDom = function(){
            EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
        }
        function getDataFromServer(url,params){
            EMS.Loading.show();
            $.getJSON(url,params, function(data) {
                try{
                    showBuilds(data);
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
        function showBuilds(data){
            if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");

			$("#buildinglist").change(function(event) {
                var buildId = $("#buildinglist").val();
				getDataFromServer("/api/ToxicGasesApi","buildId="+buildId);
			});
        };

        function setEnergyBtnStyle($current){
			var id = $("#treeview")[0].id;

			if($current[0].id==id){
				return false;
			}

			$("#treeview button").removeClass('btn-solar-selected');

			$current.addClass('btn-solar-selected');

			return true;
		}

        function showTreeview(data){
            $("#treeview").html('')
            if(!data.hasOwnProperty('devices'))
                return;
            $.each(data.devices, function(key, val) {
                $("#treeview").append('<button title="'+val.name+'" id="'+ val.id +'" value="'+ val.id +'" class="btn btn-info">'+val.name+'</button>')
            })
            $("#treeview button").eq(0).addClass('btn-solar-selected')
            $("#treeview button").click(function(event) {
				var $current = $(this);
				var isNotRepeat = setEnergyBtnStyle($current);

				if(isNotRepeat){
					//发送请求
					getDataFromServer("/api/ToxicGasesApi","buildId="+$("#buildinglist").val()+"&meterID="+$current.attr('value'));
				}
			});
        };
        function showTable(data){
            if(!data.hasOwnProperty('currentData'))
                return;
            var columns = [
                {field:'paramCode',title:'名称'},
                {field:'paramName',title:'简称'},
                {field:'value',title:'报警值'},
                {field:'paramUnit',title:'原始单位'}
            ];
            var tableRows = [];
            if(data.currentData!=null && data.currentData.length>0){
                $.each(data.currentData, function (index, val) { 
                     var row = {};
                     row.paramCode = val.paramCode;
                     row.paramName = val.paramName;
                     row.value = val.value;
                     row.paramUnit = val.paramUnit;
                     tableRows.push(row)
                });
                
            }
            var height = $("#content").height();
				$("#content").html('<table id="mainTable"></table>');
				$("#content>table").attr('data-height',height-7);

			EMS.DOM.showTable($("#content>table"),columns,tableRows,{striped:true,classes:'table table-border'});
        }
    };
    return _toxic;
})()



jQuery(document).ready(function($) {
    $("#flenergy").attr("class","start active");
    $("#cir_toxic").attr("class","active");
    
    var toxic = new Toxic();

	toxic.initDom();
	toxic.show("/api/ToxicGasesApi","");
});