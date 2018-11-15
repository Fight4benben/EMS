var Param = (function(){

	function _param(){

		var baseUrl="/api/HistoryParam";
		var paramList=[];
		var lastSelectedOrder;

		function initDom(){
			initTime();
			initTableAndChart();
			//1.查询按钮事件
			$("#BtnExec").click(function(event) {
				var paramsIds=[];
				var circuitIDs = $("#treeview").treeview('getSelected');
				if(circuitIDs.length==0)
					return;

				var time = $("#StartBox").val();
				$("input[name='param']:checked").each(function() {
					paramsIds.push($(this).val());
				});

				var params = "circuitID="+circuitIDs[0].id+"&meterParamIDs="+paramsIds.join(',')
								+"&startTime="+time+"&step=5";

				getData(baseUrl,params);

			});

			//树状结构搜索
			$("#treeSearch").click(function(){
				var inputValue = $("#search-input").val().trim();

				if(inputValue==="")
					return;

				$("#treeview").treeview('unselectAll',{silent:true})

				var nodes = EMS.Tool.searchTree($("#treeview"),inputValue);

				if(nodes.length===0){
					alert("查不到当前回路名称，请重新输入名称！");
					return;
				}

				$('#treeview').treeview('selectNode', [ nodes[0].nodeId ]);

			});
		}

		function initTime(){
			EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#StartDate"),$("#StartBox"));
		}

		function initTableAndChart(){
			$("#paramChart").html("");
			$("#paramtable").html("");
		}

		this.show = function(){
			initDom();
			getData(baseUrl,"");
		}

		function getData(url,params){
			EMS.Loading.show();
			$.getJSON(url, params, function(data) {

				try{
					showBuildList(data);
					showEnergys(data);
					showTreeview(data);
					showParams(data);

					showChartAndTable(data);
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
				var params = "buildId="+buildId;

				getData(baseUrl,params);
			})
		}

		function showEnergys(data){
			if(!data.hasOwnProperty('energys'))
				return;

			EMS.DOM.initSelect(data.energys,$("#energys"),"energyItemName","energyItemCode");

			$("#energys").change(function(event) {
				initTableAndChart();

				var buildId = $("#buildinglist").val();
				var energyCode = $(this).val();
				
				var params = "buildId="+buildId+"&energyCode="+energyCode;

				getData(baseUrl,params);
			})
		}

		function showTreeview(data){
			if(!data.hasOwnProperty('treeView'))
				return;

			EMS.DOM.initTreeview(data.treeView,$("#treeview"),{
				showIcon: true,
				showCheckbox: false,
				showBorder:false,
				levels:2,
				onNodeSelected:function(event,node){
					initTableAndChart();
					var id = node.id;
					var buildId = $("#buildinglist").val();
					var energyCode = $("#energys").attr('value');

					getData(baseUrl,"buildId="+buildId+"&energyCode="+
						energyCode+"&circuitID="+id);
				}
			});
		}

		function showParams(data){
			
			if(!data.hasOwnProperty('meterParam'))
				return;

			paramList=[];

			paramList = data.meterParam;
			var orders=[];

			$.each(paramList, function(key, val) {
				
				//paramOrder : 顺序
				//paramType： 参数类型
				isArrayContainsObj(orders,"paramOrder",val.paramOrder)==false
					? orders.push({"paramOrder":val.paramOrder,"paramType":val.paramType}): 1==1;

			});

			//console.log(orders);
			showParamType(orders);

			//获取第一个参数类型对应的所有参数
			if(orders.length>0){
				var orderfilter = orders.filter(function(currentValue) {
					return currentValue.paramOrder == lastSelectedOrder;
				});
				var filters;
				if(lastSelectedOrder ==undefined || orderfilter.length ==0){
					filters = paramList.filter(function(currentValue,index){
						return currentValue.paramOrder == orders[0].paramOrder;
					});

					lastSelectedOrder = orders[0].paramOrder;
				}else{
					$("#paramlist").val(lastSelectedOrder);
					filters = paramList.filter(function(currentValue,index){
						return currentValue.paramOrder == lastSelectedOrder;
					});
				}
				

				showCurrentParamList(filters);
			}
			
		}

		function showParamType(array){
			EMS.DOM.initSelect(array,$("#paramlist"),"paramType","paramOrder");

			$("#paramlist").change(function(event) {
				initTableAndChart();
				var paramOrder = $(this).val();
				var filters = paramList.filter(function(currentValue,index){
					return currentValue.paramOrder == paramOrder;
				});

				lastSelectedOrder = paramOrder;

				showCurrentParamList(filters);
			});
		}

		function showCurrentParamList(paramlist){
			$("#paramdetail").html("<ul></ul>");

			$.each(paramlist, function(key, val) {
				$("#paramdetail>ul").append('<li><input type="checkbox" checked="true" name="param" value="'+
					val.paramID+'">'+val.paramCode+"|"+val.paramName+"|"+val.paramUnit+'</li>')
			});

		}


		function isArrayContainsObj(array,prop,key){
			var filterArray = array.filter(function(currentValue,index){
				return currentValue[prop] == key;
			});

			if(filterArray.length>0)
				return true;
			else
				return false;
		}

		function showChartAndTable(data){
			$("#paramChart").html("");
			$("#paramtable").html("<table></table>");
			$("#paramtable>table").attr('data-height',$("#paramtable").height());

			if(!data.hasOwnProperty('data'))
				return;

			if(data.data.length ==0)
				return;

			var times=[];
			var serieses=[];
			var legend=[];

			var columns=[
				{field:'name',title:'回路名称'},
				{field:'time',title:'时间'}
				];
			var rows=[];

			$.each(data.data, function(key, val) {
				var series={name:val.paramName,type:'line',data:[]};
				columns.push({field:val.paramCode,title:val.paramName+"("+val.paramCode+")"});
				legend.push(val.paramName);
				$.each(val.values, function(index, paramValue) {
					if(key==0)
						times.push(EMS.Tool.toTimeString(new Date(paramValue.time)));

					if(paramValue.value==-9999){
						series.data.push('-');
					}else{
						series.data.push(paramValue.value);
					}

					var filterIndex;
					var filters = rows.filter(function(currentValue,currentIndex) {

						if(currentValue.time == paramValue.time)  {
							filterIndex = currentIndex;
							return true;
						}else{
							return false;
						}
						//filterIndex = currentIndex;
						//return currentValue.time == paramValue.time;
					});

					if(filters.length > 0){
						if(paramValue.value==-9999){
							rows[filterIndex][val.paramCode] ='-';
						}else{
							rows[filterIndex][val.paramCode] = paramValue.value;
						}
					}else{
						if(paramValue.value==-9999){
							var row={name:val.name,time:paramValue.time};
							row[val.paramCode]='-';
							rows.push(row)
						}else{
							var row={name:val.name,time:paramValue.time};
							row[val.paramCode]=paramValue.value;
							rows.push(row)
						}
					}
				});



				serieses.push(series);
			});

			EMS.Chart.showLine(echarts,$("#paramChart"),legend,times,serieses);

			$("#paramtable>table").bootstrapTable({
				striped:true,
				classes:'table table-border',
				columns:columns,
				data:rows
			});
		}

	}

	return _param;

})();

jQuery(document).ready(function($) {
	$("#flenergy").attr("class","start active");
	$("#cir_params").attr("class","active");

	var param = new Param();
	param.show();
});