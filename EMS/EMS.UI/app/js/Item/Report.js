var ItemReport = (function(){

	function _itemReport(){

		this.show=function(){
			var url ="/api/ItemReport";
			getDataFromServer(url,"");
		}
		//公开暴露的方法:初始化页面
		this.initDom = function(){
			initDateTime();
			initChange();
			//initSearchButton();
		};

		function initEnergyBtns(){
			$("#se_countBtns").html("");
		}
		//获取选中的回路
		function getCheckedTreeIdArray(){
			var idArray = [];
			var treeArray = $("#treeview").treeview('getChecked');

			$.each(treeArray, function(key, val) {
				idArray.push(val.id);
			});

			return idArray;
		}

		//初始化页面时，先加载
		function initChange(){
			$("#dayReportClick").click(function(){
				var $current = $(this);
				var isContinue = setSelectStyle($current);
				if(isContinue){

					EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
					//发送请求	

				}

			});

			$("#monthReportClick").click(function() {
				var $current = $(this);
				var isContinue = setSelectStyle($current);
				if(isContinue){

					EMS.DOM.initDateTimePicker('YEARMONTH',new Date(),$("#dayCalendar"),$("#daycalendarBox"),{format:'yyyy-mm',
									        language: 'zh-CN',
									        autoclose: 1,
									        startView: 3,
									        minView: 3,
									        forceParse: false,
									        pickerPosition: "bottom-left"});

					//发送请求
			
				}
			});

			$("#yearReportClick").click(function() {
				var $current = $(this);
				var isContinue = setSelectStyle($current);
				if(isContinue){

					EMS.DOM.initDateTimePicker('YEAR',new Date(),$("#dayCalendar"),$("#daycalendarBox"),{format:'yyyy',
									        language: 'zh-CN',
									        autoclose: 1,
									        startView: 4,
									        minView: 4,
									        forceParse: false,
									        pickerPosition: "bottom-left"});

					//发送请求
					
				}
			});
		}

		//设置日月年标签的选中样式：重复点击当前标签，不做任何处理，不重新加载数据
		function setSelectStyle($current){
			var id =$current.parent('ul').children('.current')[0].id;
			if($current[0].id == id){
				return false;
			}

			$current.parent('ul').children('li').removeClass('current');

			$current.addClass('current');

			return true;
		}

		//选中分类能耗按钮后添加选中样式或判断如果再次点击本分类，则不重新加载数据
		function setEnergyBtnStyle($current){
			var id = $("#se_countBtns .btn-solar-selected")[0].id;

			if($current[0].id==id){
				return false;
			}

			$("#se_countBtns button").removeClass('btn-solar-selected');

			$current.addClass('btn-solar-selected');

			return true;
		}

		//获取选中的报表类型：返回DD/MM/YY
		function getTypeByReportSelected(){
			var type;
			switch($(".current")[0].id){
				case "dayReportClick":
					type="DD";
				break;
				case "monthReportClick":
					type="MM";
				break;
				case "yearReportClick":
					type="YY";
				break;
			};
			return type;
		}

		function initDateTime(){
			EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
		}

		function getDataFromServer(url,params){
			$.getJSON(url,params, function(data) {
				EMS.Loading.show($("#main-content").parent('div'));
				if(data.hasOwnProperty('message'))
					location = "/Account/Login";
				try{
					console.log(data);
					showBuilds(data);
					showEnergys(data);
					showTreeview(data);
					showTable(data);
				}catch(e){

				}finally{
					EMS.Loading.hide($("#main-content").parent('div'));
				}
			});
		}

		//生成建筑列表，追加到select中
		function showBuilds(data){
			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");

			$("#buildinglist").change(function(event) {
				var buildId = $(this).val();
				
				 //console.log($("#daycalendarBox").val());
				getDataFromServer("/api/ItemReport","buildId="+buildId+"&type="+getTypeByReportSelected()+"&date="+$("#daycalendarBox").val());
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
						$("#se_countBtns").append('<acronym title="电"><button id="elec" class="btn btn-elc" value="01000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "02000":
						$("#se_countBtns").append('<acronym title="水"><button id="water" class="btn btn-water" value="02000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "13000":
						$("#se_countBtns").append('<acronym title="光伏"><button id="solar" class="btn btn-solar" value="13000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					default:
						$("#se_countBtns").append('<acronym title="'+val.energyItemName+'"><button class="btn btn-empty" code="'+val.energyItemCode+'" type="button">'+
							val.energyItemName.substring(0,1)+'</button></acronym>');
				}
			});

			$("#se_countBtns button").eq(0).addClass('btn-solar-selected')

			$("#se_countBtns button").click(function(event) {//为能源按钮绑定click事件，进行数据加载
				var $current = $(this);

				var isNotRepeat = setEnergyBtnStyle($current);

				if(isNotRepeat){
					//发送请求
					
				}
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
		}

		//根据数据显示表格内容
		function showTable(data){
			var times=[];
			var names=[];
			var dataList=[];
			var report;
			var color;

			if(data.reportType=="DD"){
				report="时";
				$("#dayReportClick").addClass('current');
				color = '#F08500';
			}
			else if(data.reportType=="MM"){
				report="日";
				color = '#74B000';
			}
			else {
				report ="月";
				color = '#0076D0';
			}

			$.each(data.data, function(index, val) {
				
				var date = new Date(val.time);
				var time;
				switch(data.reportType){
					case "DD":
						time = date.getHours();
					break;
					case "MM":
						time = date.getDate();
					break;
					case "YY":
						time = date.getMonth()+1;
					break;
				}

				if( $.inArray(time, times)==-1){
					times.push(time);
				}

				if( $.inArray(val.name, names) != -1){
					dataList[$.inArray(val.name, names)].data.push({time:time,value:val.value.toFixed(2)});
				}else{
					names.push(val.name);
					dataList.push({name:val.name,data:[{time:time,value:val.value.toFixed(2)}]});
				}

			});
			times.sort(function(a,b){
				return a-b;
			});
			//console.log(dataList);

			var columns = [{field:'name',title:'分项名称'}];
			var rows =[];

			$.each(times, function(index, val) {
				columns.push({field:val,title:EMS.Tool.appendZero(val)+report});
			});

			$.each(dataList, function(index, val) {
				var row={};
				row.name = val.name;
				$.each(val.data, function(key,value){
					row[value.time] = value.value;

				});

				rows.push(row);
			});

			$("#dayReport").html('<table></table>');
			var totalHeight = $("#main-content").height() - 127;
			$("#dayReport").height(totalHeight);
			$("#dayReport>table").attr('data-height',totalHeight);

			EMS.DOM.showTable($("#dayReport>table"),columns,rows,{striped:true,classes:'table table-border'});
			$("#dayReport table th").css({
				'background-color':color,
				'color':'white'});

			$("thead>tr>th").eq(0).css('minWidth','250px');
		}

	};

	return _itemReport;

})();

jQuery(document).ready(function($) {

	$("#seenergy").attr("class","start active");
	$("#se_count").attr("class","active");


	var report = new ItemReport();
	report.initDom();
	report.show();
});