
var CircuitPay = (function(){

	function _circuitPay(){
        //公开暴露的方法:初始化页面
		this.initDom = function(){
			initDateTime();
			initChange();
			initSearchButton();
        };
        //公开暴露的方法：页面加载后生成默认数据
		this.showPayRate = function(url,params){
			//var buildId=$.cookie('buildId');
			//if(buildId==undefined || buildId==null || buildId == "null")
				getDataFromServer(url,params);
			//else
				//getDataFromServer(url,"buildId="+buildId);
        };
        //从服务器获取json数据
		function getDataFromServer(url,params,httpType){
			EMS.Loading.show();
			if(httpType =="POST")
				$.post(url, params, function(data, textStatus, xhr) {
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
			else
				jQuery.getJSON(url,params, function(data) {

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
        //生成建筑列表，追加到select中
		function showBuilds(data){
			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");
			// if($.cookie('buildId') != undefined && $.cookie('buildId')!=null)
			// 	$("#buildinglist").val($.cookie("buildId"));

			$("#buildinglist").change(function(event) {
				var buildId = $("#buildinglist").val();
				//$.cookie("buildId",buildId,{path:'/'});
				getDataFromServer("/api/MultiRate","buildId="+buildId+"&type="+getTypeByReportSelected()+"&date="+$("#daycalendarBox").val());
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

        function initDateTime(){
			EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
        }
        //初始化页面时，先加载
		function initChange(){
			$("#dayReportClick").click(function(){
				var $current = $(this);
				var isContinue = setSelectStyle($current);
				if(isContinue){

					EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
					//发送请求	

					getDataFromServer("/api/MultiRate",{
						buildId:$("#buildinglist").val(),
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
					

					getDataFromServer("/api/MultiRate",{
						buildId:$("#buildinglist").val(),
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
					getDataFromServer("/api/MultiRate",{
						buildId:$("#buildinglist").val(),
						circuits:getCheckedTreeIdArray().join(','),type:"YY",date:$("#daycalendarBox").val()
					},'POST');
				}
			});
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
        //获取选中的报表类型：返回DD/MM/YY
		function getTypeByReportSelected(){
            var type;
            console.log($(".current"))
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
        
        function initSearchButton(){
			//查询数据
			$("#daySearch").click(function(event) {
				
				getDataFromServer("/api/MultiRate",{
					buildId:$("#buildinglist").val(),
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


				getDataFromServer("/api/MultiRate",{
					buildId:$("#buildinglist").val(),
					circuits:getCheckedTreeIdArray().join(','),type:getTypeByReportSelected(),date:$("#daycalendarBox").val()
				},'POST');
			});
        };
        //根据数据显示表格内容
		function showTable(data){
            var times=[];
			var names=[];
			var dataList=[];
            if(data.reportType=="DD"){
				report="时";
				$("#dayReportClick").addClass('current');
				color = '#F08500';
			}
			else if(data.reportType=="MM"){
                report="日";
                $("#monthReportClick").addClass('current');
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
					dataList.push({name:val.name,paramName:val.paramName,data:[{time:time,value:val.value.toFixed(2)}]});
				}
            });
            var columns = [{field:'name',title:'回路名称'},{field:'paramName',title:'分类名称'}];
            var rows =[];

			$.each(times, function(index, val) {
				columns.push({field:val,title:EMS.Tool.appendZero(val)+report});
            });
            var totalRow={};
            $.each(dataList, function(index, val) {
				var row={};
				row.name = val.name;
                row.paramName = val.paramName
                $.each(val.data, function(key,value){
					row[value.time] = value.value;
				});
				rows.push(row);
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

	return _circuitPay;

})();




jQuery(document).ready(function($) {

	$("#flenergy").attr("class","start active");
    $("#cir_multi").attr("class","active");
    
    var circuitPay = new CircuitPay();

	circuitPay.initDom();
	circuitPay.showPayRate("/api/MultiRate","");
	
});