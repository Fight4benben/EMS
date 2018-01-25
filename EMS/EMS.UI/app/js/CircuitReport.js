'use strict';
var CircuitReport = (function(){

	function _circuitReport(){

		function initEnergyBtns(){
			$("#te_countBtns").html("");
		}

		function initDateTime(){
			EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
		}

		function initChange(){
			$("#dayReportClick").click(function(){
				var $current = $(this);
				var isContinue = setSelectStyle($current);
				if(isContinue)
					EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));

			});

			$("#monthReportClick").click(function() {
				var $current = $(this);
				var isContinue = setSelectStyle($current);
				if(isContinue)
					EMS.DOM.initDateTimePicker('YEARMONTH',new Date(),$("#dayCalendar"),$("#daycalendarBox"),{format:'yyyy-mm',
									        language: 'zh-CN',
									        autoclose: 1,
									        startView: 3,
									        minView: 3,
									        forceParse: false,
									        pickerPosition: "bottom-left"});
			});

			$("#yearReportClick").click(function() {
				var $current = $(this);
				var isContinue = setSelectStyle($current);
				if(isContinue)
					EMS.DOM.initDateTimePicker('YEAR',new Date(),$("#dayCalendar"),$("#daycalendarBox"),{format:'yyyy',
									        language: 'zh-CN',
									        autoclose: 1,
									        startView: 4,
									        minView: 4,
									        forceParse: false,
									        pickerPosition: "bottom-left"});
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

		function setEnergyBtnStyle($current){
			var id = $("#te_countBtns .btn-solar-selected")[0].id;

			if($current[0].id==id){
				return false;
			}

			$("#te_countBtns button").removeClass('btn-solar-selected');

			$current.addClass('btn-solar-selected');

			return true;
		}

		function getCheckedTreeIdArray(){
			var idArray = [];
			var treeArray = $("#treeview").treeview('getChecked');

			$.each(treeArray, function(key, val) {
				idArray.push(val.id);
			});

			return idArray;
		}

		this.initDom = function(){
			initDateTime();
			initChange();
		};

		this.showReport = function(url,params){
			getDataFromServer(url,params);
		};



		function getDataFromServer(url,params){
			jQuery.getJSON(url,params, function(data) {
			  console.log(data);
			  showBuilds(data);
			  showEnergys(data);
			  showTreeview(data);
			  showTable(data);
			});
		};

		function showBuilds(data){
			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");

			$("#buildinglist").change(function(event) {
				var buildId = $(this).val();
				
				 //console.log($("#daycalendarBox").val());
				getDataFromServer("/api/CircuitReport/report","buildId="+buildId+"&type="+getTypeByReportSelected+"&date="+$("#daycalendarBox").val());
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
				}
			});

			$("#te_countBtns button").eq(0).addClass('btn-solar-selected')

			$("#te_countBtns button").click(function(event) {
				var $current = $(this);

				var isNotRepeat = setEnergyBtnStyle($current);

				if(isNotRepeat){
					//发送请求
					getDataFromServer("/api/CircuitReport/report","buildId="+$("#buildinglist").val()+"&energyCode="+
						$current.attr('value')+"&type="+getTypeByReportSelected()+"&date="+$("#daycalendarBox").val());
				}
			});
		};

		function showTreeview(data){
			if(!data.hasOwnProperty('treeView'))
				return;

			$("#treeview").html("");
			$("#treeview").parent('div').css('overflow','auto');
			$("#treeview").width(350);
			$(".count-info-te").height($("#main-content").height());
			console.log($("#main-content").height());
			$("#treeview").height($(".count-info-te").height() - 258);
			//$("#treeview").parent('div').height(800);

			EMS.DOM.initTreeview(data.treeView,$("#treeview"),{
				showIcon: true,
				showCheckbox: true,
				showBorder:false,
				levels:2});

			$("#treeview").treeview('checkAll',{silent:true});
		};

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

			var columns = [{field:'name',title:'回路名称'}];
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

	return _circuitReport;

})();

jQuery(document).ready(function($) {
	
	var circuitReport = new CircuitReport();

	circuitReport.initDom();
	circuitReport.showReport("/api/CircuitReport/report","");

});