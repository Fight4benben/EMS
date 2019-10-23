

var CollectMulti = (function(){

    function _collectMulti(){
        this.show = function(){
			var url = "/api/MultiRateCollect/";

			getDataFromServer(url,"","GET");
		};

		this.initDom = function(){
			initTime();
			initLoad();
        };
        
        function initTime(){
			initDate("Start");
			initDate("End");
        };
        
        function initDate(type){
			switch(type){
				case 'Start':
					EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#StartDate"),$("#StartBox"));
					$("#StartHour").val(0);
					$("#StartMinute").val(0);
				break;
				case 'End':
					EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#EndDate"),$("#EndBox"));
					var date = new Date();
					var hour = date.getHours();

					var minute = date.getMinutes();
					if(minute <10){
						$("#EndHour").val(hour-1);
						$("#EndMinute").val(55);
					}else{
						$("#EndHour").val(hour);
						$("#EndMinute").val(minute - minute%5 -5);
					}
				break;
			}
        };
        
		function clearData(){
			$("#tableData").html("");
        };

        function getDataFromServer(url,params,httpType){
			EMS.Loading.show();
			if(httpType =="POST")
				$.post(url, params, function(data, textStatus, xhr) {
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
			else
				jQuery.getJSON(url,params, function(data) {

				  try{
						showBuilds(data);
					    showTreeview(data);
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
			if($.cookie('buildId') != undefined && $.cookie('buildId')!=null)
				$("#buildinglist").val($.cookie("buildId"));

			$("#buildinglist").change(function(event) {

				clearData();

				var buildId = $("#buildinglist").val();
				$.cookie("buildId",buildId,{path:'/'});
				getDataFromServer("/api/MultiRateCollect","buildId="+buildId);
			});
        };
        
        function initLoad(){
			$("#BtnExec").click(function(){
				var buildId = $("#buildinglist").val();
				var circuits = getCheckedTreeIdArray().join(',');
				var startTime = $('#StartBox').val()+" "+$("#StartHour").find("option:selected").text()+":"+
					$("#StartMinute").find("option:selected").text()+":00";
				var endTime = $('#EndBox').val()+" "+$("#EndHour").find("option:selected").text()+":"+
					$("#EndMinute").find("option:selected").text()+":00";

				var url ="/api/MultiRateCollect/Collect";

				getDataFromServer(url,{
					buildId:buildId,
					energyCode:'01000',
					circuits:circuits,
					startTime:startTime,
					endTime:endTime
				},"POST");
			});
            //导出
            $("#dayExport").click(function(event){
                $('#mainTable').tableExport({type:'excel',escape:'false',fileName: '复费率集抄报表'});
            })
			$("#treeSearch").click(function(){
				var inputValue = $("#search-input").val().trim();

				if(inputValue==="")
					return;

				$("#treeview").treeview('uncheckAll',{silent:true})

				var nodes = EMS.Tool.searchTree($("#treeview"),inputValue);

				if(nodes.length===0){
					alert("查不到当前回路名称，请重新输入名称！");
					return;
				}

				$.each(nodes, function(index, val) {
					$('#treeview').treeview('checkNode', [ val.nodeId, { silent: true } ]);
				});


				var buildId = $("#buildinglist").val();
				var circuits = getCheckedTreeIdArray().join(',');
				var startTime = $('#StartBox').val()+" "+$("#StartHour").find("option:selected").text()+":"+
					$("#StartMinute").find("option:selected").text()+":00";
				var endTime = $('#EndBox').val()+" "+$("#EndHour").find("option:selected").text()+":"+
					$("#EndMinute").find("option:selected").text()+":00";

				var url ="/api/MultiRateCollect/Collect";

				getDataFromServer(url,{
					buildId:buildId,
					energyCode:'01000',
					circuits:circuits,
					startTime:startTime,
					endTime:endTime
				},"POST");
			});
        };
        
        //根据数据显示树状结构，如果不包含树状结构数据则不更新数据。
		function showTreeview(data){
			if(!data.hasOwnProperty('treeView'))
				return;

			$("#treeview").html("");//更新树状结构之前先将该区域清空
			$("#treeview").parent('div').css('overflow','auto');
			$("#treeview").width(350);
			$(".count-info-te").height($("#main-content").height());
			$("#treeview").height($(".count-info-te").height() - 258);

			EMS.DOM.initTreeview(data.treeView,$("#treeview"),{
				showIcon: true,
				showCheckbox: true,
				showBorder:false,
				levels:2,
				onNodeChecked: function(event, node) {

					if($("#related").prop('checked'))
						checkChildren(node,$("#treeview"));
					else
						$("#treeview").treeview('checkNode',[node.nodeId,{silent:true}]);
				},
				onNodeUnchecked: function (event, node) {

					if($("#related").prop('checked'))
						unCheckChildren(node,$("#treeview"));
					else
						$("#treeview").treeview('uncheckNode',[node.nodeId,{silent:true}]);
				}
			});

			$("#treeview").treeview('checkAll',{silent:true});
		};

		//获取选中的回路
		function getCheckedTreeIdArray(){
			var idArray = [];
			var treeArray = $("#treeview").treeview('getChecked');

			$.each(treeArray, function(key, val) {
				idArray.push(val.id);
			});

			return idArray;
		}

		//树状结构级联选择，采用递归方法。需要传入两个参数，一个当前的node一个是tree的div
		function checkChildren(node,$Tree){
			var str =JSON.stringify(node);
			var pattern = new RegExp('nodes');
			if(pattern.test(str)){
				$.each(node.nodes,function(key,val){
					$Tree.treeview('checkNode',[val.nodeId]);
					checkChildren(val,$Tree);
				});
			}
		};

		//级联方式取消树状结构的选中状态
		function unCheckChildren(node,$Tree){
			var str =JSON.stringify(node);
			var pattern = new RegExp('nodes');
			if(pattern.test(str)){
				$.each(node.nodes,function(key,val){
					$Tree.treeview('uncheckNode',[val.nodeId]);
					unCheckChildren(val,$Tree);
				});
			};
        };
        
        function showTable(data){
			var startTotal = 0;
			var endTotal = 0;
			var columns = [
                {field:'name',title:'回路名称'},
                {field:'paramName',title:'类型'},
				{field:'startValue',title:'起始数值'},
				{field:'endValue',title:'截止数值'},
				{field:'diffValue',title:'差值'}
			];

			var rows=[];

			if(!data.hasOwnProperty('data'))
				return;

			if(data.data.length == 0)
				return;

			$.each(data.data, function(key, val) {
				if(parseInt(val.startValue) != -9999 && parseInt(val.endValue) != -9999){
					startTotal +=val.startValue;
					endTotal +=val.endValue;
 				}
				rows.push({name:val.name,paramName:val.paramName,startValue:val.startValue.toFixed(2),
					endValue:val.endValue.toFixed(2),diffValue:val.diffValue.toFixed(2)});
			});
			rows.push({name:'合计',startValue:startTotal.toFixed(2),endValue:endTotal.toFixed(2),diffValue:(endTotal-startTotal).toFixed(2)});

			$("#tableData").html("<table id='mainTable'></table>");
			$("#tableData>table").attr('data-height',$("#tableData").height());

			$("#tableData>table").bootstrapTable({
				striped:true,
				classes:'table table-border',
				columns:columns,
                data:rows,
                //合并单元格
                onAll:function(rows){
                    mergeCells(data, "name",0, $("#mainTable"));
                }
            });
            $("tbody>tr>td").css('vertical-align','middle');
            function mergeCells(data,fieldName,colspan,target){
                var sortMap = {};
                for(var i = 0 ; i < data.data.length ; i++){
                    for(var prop in data.data[i]){
                        if(prop == fieldName){
                            var key = data.data[i][prop]
                            if(sortMap.hasOwnProperty(key)){
                                sortMap[key] = sortMap[key] * 1 + 1;
                            } else {
                                sortMap[key] = 1;
                            }
                            break;
                        } 
                    }
                }
                var index = 0;
                for(var prop in sortMap){
                    var count = sortMap[prop] * 1;
                    $(target).bootstrapTable('mergeCells',{index:index, field:fieldName, colspan:colspan, rowspan: count});   
                    index += count;
                }
            }
            
		}
    }
    return _collectMulti;
})();



jQuery(document).ready(function($) {


	$("#flenergy").attr("class","start active");
	$("#cir_multi_collect").attr("class","active");
	
	var collectMulti = new CollectMulti();
	collectMulti.initDom();
	collectMulti.show();

});