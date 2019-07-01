'use strict';
var CircuitReport = (function(){

	function _circuitReport(){

		function initEnergyBtns(){
			$("#te_countBtns").html("");
		}

		function initDateTime(){
			EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
		}

		function initSearchButton(){
			//查询数据
			$("#daySearch").click(function(event) {
				
				getDataFromServer("/api/CircuitReport/report",{
					buildId:$("#buildinglist").val(),energyCode:$('.btn-solar-selected').attr('value'),
					circuits:getCheckedTreeIdArray().join(','),type:getTypeByReportSelected(),date:$("#daycalendarBox").val()
				},'POST');
			});

			//导出的Excel
			$("#dayExport").click(function(event) {
				var circuitsArray=[];
				$.each(getCheckedTreeIdArray(), function(key, val) {
					circuitsArray.push(val.substr(-4));
				});
				
				window.location = "/Circuit/GetExcel?buildId="+$("#buildinglist").val()+
				"&energyCode="+$('.btn-solar-selected').attr('value')+
				"&circuits="+circuitsArray.join(',')+
				"&type="+getTypeByReportSelected()+"&date="+$("#daycalendarBox").val()
			});

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


				getDataFromServer("/api/CircuitReport/report",{
					buildId:$("#buildinglist").val(),energyCode:$('.btn-solar-selected').attr('value'),
					circuits:getCheckedTreeIdArray().join(','),type:getTypeByReportSelected(),date:$("#daycalendarBox").val()
				},'POST');
			});
		}

		//初始化页面时，先加载
		function initChange(){
			$("#dayReportClick").click(function(){
				var $current = $(this);
				var isContinue = setSelectStyle($current);
				if(isContinue){

					EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
					//发送请求	

					getDataFromServer("/api/CircuitReport/report",{
						buildId:$("#buildinglist").val(),energyCode:$('.btn-solar-selected').attr('value'),
						circuits:getCheckedTreeIdArray().join(','),type:"DD",date:$("#daycalendarBox").val()
					},'POST');
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
					

					getDataFromServer("/api/CircuitReport/report",{
						buildId:$("#buildinglist").val(),energyCode:$('.btn-solar-selected').attr('value'),
						circuits:getCheckedTreeIdArray().join(','),type:"MM",date:$("#daycalendarBox").val()
					},'POST');
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
					getDataFromServer("/api/CircuitReport/report",{
						buildId:$("#buildinglist").val(),energyCode:$('.btn-solar-selected').attr('value'),
						circuits:getCheckedTreeIdArray().join(','),type:"YY",date:$("#daycalendarBox").val()
					},'POST');
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
			var id = $("#te_countBtns .btn-solar-selected")[0].id;

			if($current[0].id==id){
				return false;
			}

			$("#te_countBtns button").removeClass('btn-solar-selected');

			$current.addClass('btn-solar-selected');

			return true;
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
		//公开暴露的方法:初始化页面
		this.initDom = function(){
			initDateTime();
			initChange();
			initSearchButton();
		};
		//公开暴露的方法：页面加载后生成默认数据
		this.showReport = function(url,params){
			var buildId=$.cookie('buildId');
			if(buildId==undefined || buildId==null || buildId == "null")
				getDataFromServer(url,params);
			else
				getDataFromServer(url,"buildId="+buildId);
		};

		//从服务器获取json数据
		function getDataFromServer(url,params,httpType){
			EMS.Loading.show();
			if(httpType =="POST")
				$.post(url, params, function(data, textStatus, xhr) {
					try{
						showBuilds(data);
						showEnergys(data);
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
			else
				jQuery.getJSON(url,params, function(data) {

				  try{
						showBuilds(data);
					  	showEnergys(data);
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
			if($.cookie('buildId') != undefined && $.cookie('buildId')!=null)
				$("#buildinglist").val($.cookie("buildId"));

			$("#buildinglist").change(function(event) {
				var buildId = $("#buildinglist").val();
				$.cookie("buildId",buildId,{path:'/'});
				getDataFromServer("/api/CircuitReport/report","buildId="+buildId+"&type="+getTypeByReportSelected()+"&date="+$("#daycalendarBox").val());
			});
		};

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

		//生成不同能源类型的按钮
		function showEnergys(data){
			
			if(!data.hasOwnProperty('energys'))
				return;

			initEnergyBtns();

			$.each(data.energys, function(key, val) {

				switch(val.energyItemCode){
					case "01000":
						$("#te_countBtns").append('<acronym title="电"><button id="elec" class="btn btn-elc" value="01000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "02000":
						$("#te_countBtns").append('<acronym title="水"><button id="water" class="btn btn-water" value="02000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "13000":
						$("#te_countBtns").append('<acronym title="光伏"><button id="solar" class="btn btn-solar" value="13000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "03000":
						$("#te_countBtns").append('<acronym title="燃气"><button id="gas" class="btn btn-gas" value="03000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "40000":
						$("#te_countBtns").append('<acronym title="蒸汽"><button id="steam" class="btn btn-steam" value="40000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					default:
						$("#te_countBtns").append('<acronym title="'+val.energyItemName+'"><button class="btn btn-empty" value="'+val.energyItemCode+'" type="button">'+
							val.energyItemName.substring(0,1)+'</button></acronym>');
				}
			});

			$("#te_countBtns button").eq(0).addClass('btn-solar-selected')

			$("#te_countBtns button").click(function(event) {//为能源按钮绑定click事件，进行数据加载
				var $current = $(this);

				var isNotRepeat = setEnergyBtnStyle($current);

				if(isNotRepeat){
					//发送请求
					getDataFromServer("/api/CircuitReport/report","buildId="+$("#buildinglist").val()+"&energyCode="+
						$current.attr('value')+"&type="+getTypeByReportSelected()+"&date="+$("#daycalendarBox").val());
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
			var total = {name :'总计',sum:0}

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

				if(total.hasOwnProperty(time)){
					total[time]+= parseFloat(val.value);
				}else{
					total[time] = parseFloat(val.value);
				}

				total.sum += parseFloat(val.value);

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

			var columns = [{field:'name',title:'回路名称'}];
			var rows =[];

			$.each(times, function(index, val) {
				columns.push({field:val,title:EMS.Tool.appendZero(val)+report});
			});

			columns.push({field:'sum',title:'合计'});

			$.each(dataList, function(index, val) {
				var row={};
				row.name = val.name;
				var sum = 0;
				$.each(val.data, function(key,value){
					row[value.time] = value.value;
					sum += parseFloat(value.value);
				});

				row.sum = sum.toFixed(2);

				rows.push(row);
			});
			var totalRow={};
			$.each(total, function(index, val) {
				if(index !== 'name')
					totalRow[index] = val.toFixed(2);
				else
					totalRow[index] = val;
			});

			rows.push(totalRow);

			$("#dayReport").html('<table></table>');
			var windowWidth = $(window).width();
			var totalHeight;
			if(windowWidth>1024)
				totalHeight = $("#main-content").height() - 127;
			else if(windowWidth>500)
				totalHeight = $(".report-modify").height()-50;
			else if(windowWidth<=500)
				totalHeight = $(".report-modify").height()-127;
			
			$("#dayReport").height(totalHeight);
			$("#dayReport>table").attr('data-height',totalHeight);

			EMS.DOM.showTable($("#dayReport>table"),columns,rows,{striped:true,classes:'table table-border'});
			$("#dayReport table th").css({
				'background-color':color,
				'color':'white'});

			$("thead>tr>th").eq(0).css('minWidth','250px');
		}

	};

	return _circuitReport;

})();

jQuery(document).ready(function($) {

	$("body").addClass('page-header-fixed page-sidebar-fixed page-footer-fixed');
	$('.header').addClass('navbar-fixed-top');

	$("#flenergy").attr("class","start active");
	$("#count").attr("class","active");
	
	var circuitReport = new CircuitReport();

	circuitReport.initDom();
	circuitReport.showReport("/api/CircuitReport/report","");

});