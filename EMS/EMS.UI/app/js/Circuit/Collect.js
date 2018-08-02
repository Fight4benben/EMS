var Collect = (function(){
	function _collect(){

		this.show = function(){
			var url = "/api/CircuitCollect/";

			getDataFromServer(url,"","GET");
		};

		this.initDom = function(){
			initTime();
			initLoad();
		};

		function clearData(){
			$("#tableData").html("");
		}

		function initLoad(){
			$("#BtnExec").click(function(){
				var buildId = $("#buildinglist").val();
				var energyCode = $('.btn-solar-selected').attr('value');
				var circuits = getCheckedTreeIdArray().join(',');
				var startTime = $('#StartBox').val()+" "+$("#StartHour").find("option:selected").text()+":"+
					$("#StartMinute").find("option:selected").text()+":00";
				var endTime = $('#EndBox').val()+" "+$("#EndHour").find("option:selected").text()+":"+
					$("#EndMinute").find("option:selected").text()+":00";

				var url ="/api/CircuitCollect/Collect";

				getDataFromServer(url,{
					buildId:buildId,
					energyCode:energyCode,
					circuits:circuits,
					startTime:startTime,
					endTime:endTime
				},"POST");
			});
		}

		function initEnergyBtns(){
			$("#te_CollectBtns").html("");

			$("#selectall").change(function(event) {
				var state = $(this).prop('checked');

				if(state)
					$("#treeview").treeview('checkAll');
				else
					$("#treeview").treeview('uncheckAll');
			});
		}

		function initTime(){
			initDate("Start");
			initDate("End");
		}

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
		}

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
					  	showEnergys(data);
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

			$("#buildinglist").change(function(event) {

				clearData();

				var buildId = $(this).val();
				
				 //console.log($("#daycalendarBox").val());
				getDataFromServer("/api/CircuitCollect","buildId="+buildId);
			});
		};

		//生成不同能源类型的按钮
		function showEnergys(data){
			
			if(!data.hasOwnProperty('energys'))
				return;

			initEnergyBtns();

			$.each(data.energys, function(key, val) {

				switch(val.energyItemCode){
					case "01000":
						$("#te_CollectBtns").append('<acronym title="电"><button id="elec" class="btn btn-elc" value="01000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "02000":
						$("#te_CollectBtns").append('<acronym title="水"><button id="water" class="btn btn-water" value="02000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "13000":
						$("#te_CollectBtns").append('<acronym title="光伏"><button id="solar" class="btn btn-solar" value="13000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					default:
						$("#te_CollectBtns").append('<acronym title="'+val.energyItemName+'"><button class="btn btn-empty" value="'+val.energyItemCode+'" type="button">'+
							val.energyItemName.substring(0,1)+'</button></acronym>');
				}
			});

			$("#te_CollectBtns button").eq(0).addClass('btn-solar-selected')

			$("#te_CollectBtns button").click(function(event) {//为能源按钮绑定click事件，进行数据加载
				var $current = $(this);

				var isNotRepeat = setEnergyBtnStyle($current);

				if(isNotRepeat){
					clearData();
					//发送请求
					getDataFromServer("/api/CircuitCollect","buildId="+$("#buildinglist").val()+"&energyCode="+
						$current.attr('value'));
				}
			});
		};

		//选中分类能耗按钮后添加选中样式或判断如果再次点击本分类，则不重新加载数据
		function setEnergyBtnStyle($current){
			var id = $("#te_CollectBtns .btn-solar-selected")[0].id;

			if($current[0].id==id){
				return false;
			}

			$("#te_CollectBtns button").removeClass('btn-solar-selected');

			$current.addClass('btn-solar-selected');

			return true;
		}

		//根据数据显示树状结构，如果不包含树状结构数据则不更新数据。
		function showTreeview(data){
			if(!data.hasOwnProperty('treeView'))
				return;

			$("#treeview").html("");//更新树状结构之前先将该区域清空
			$("#treeview").parent('div').css('overflow','auto');
			$("#treeview").width(350);
			$(".count-info-te").height($("#main-content").height());
			//console.log($("#main-content").height());
			$("#treeview").height($(".count-info-te").height() - 258);
			//$("#treeview").parent('div').height(800);

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
			var columns = [
				{field:'name',title:'回路名称'},
				{field:'startValue',title:'起始数值'},
				{field:'endValue',title:'截止数值'},
				{field:'diffValue',title:'差值'},
			];

			var rows=[];

			if(!data.hasOwnProperty('data'))
				return;

			if(data.data.length == 0)
				return;

			$.each(data.data, function(key, val) {
				rows.push({name:val.name,startValue:val.startValue.toFixed(2),
					endValue:val.endValue.toFixed(2),diffValue:val.diffValue.toFixed(2)});
			});

			$("#tableData").html("<table></table>");
			$("#tableData>table").attr('data-height',$("#tableData").height());

			$("#tableData>table").bootstrapTable({
				striped:true,
				classes:'table table-border',
				columns:columns,
				data:rows
			});
		}

	};

	return _collect;
})();

jQuery(document).ready(function($) {


	$("#flenergy").attr("class","start active");
	$("#te_collect").attr("class","active");
	
	var collect = new Collect();
	collect.initDom();
	collect.show();

});