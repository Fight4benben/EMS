var OverAll = (function(){

	function _overAll(){

		this.show = function(){
			initBuilds();
			initDom();
			$(".searchBtn").click(function(event) {
				var id = $(this)[0].id;
				var keyWord = $("#search-keyword").val();

				if(keyWord===""){
					alert("请输入查询内容！");
					return;
				}

				var date; 

				var type =$("#dateType").val();
				if(type=="QQ"){
					var season = $("#season").val();
					date = $("#daycalendarBox").val()+"-"+EMS.Tool.appendZero(parseInt(season));
				}else
					date = $("#daycalendarBox").val();

				var url="/api/OverAllSearch";
				var params ="timeType="+type+"&type="+id+"&keyWord="+keyWord+"&buildID="+$("#buildinglist").val()+"&energyCode=01000"+"&endDay="+date;

				getDataFromServer(url,params,id);
			});
		}

		function initDom(){
			//季度选择框默认不显示
			showOrHideSeason(false);
			//初始化日期
			initDate("DD");

			initDateType();
		}

		function showOrHideSeason(isShow){
			if(isShow){
				$(".season-alarm").show();
				$("#currentMonth").prev('div').html("本季")
				$("#lastMonth").prev('div').html("上季")
			}else{
				$(".season-alarm").hide();
				$("#currentMonth").prev('div').html("本月")
				$("#lastMonth").prev('div').html("上月")
			}
				

		}

		function initDate(type){
			switch(type){
				case "DD"://日
				EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
				break;
				case "MM"://月
				EMS.DOM.initDateTimePicker('YEARMONTH',new Date(),$("#dayCalendar"),$("#daycalendarBox"),{format:'yyyy-mm',
									        language: 'zh-CN',
									        autoclose: 1,
									        startView: 3,
									        minView: 3,
									        forceParse: false,
									        pickerPosition: "bottom-left"});
				break;
				case "QQ"://季度
				EMS.DOM.initDateTimePicker('YEAR',new Date(),$("#dayCalendar"),$("#daycalendarBox"),{format:'yyyy',
									        language: 'zh-CN',
									        autoclose: 1,
									        startView: 4,
									        minView: 4,
									        forceParse: false,
									        pickerPosition: "bottom-left"});
				break;

			}
		}

		function initDateType(){
			$("#dateType").change(function(event) {
				var typeValue = $(this).val();
				initDate(typeValue);

				typeValue === "QQ" ? showOrHideSeason(true) : showOrHideSeason(false);

			});
		}

		function initBuilds(){
			var url="/api/OverAllSearch";

			$.getJSON(url,"", function(data) {
				showBuildList(data);
			});
		}

		function showBuildList(data){

			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");
		};



		function getDataFromServer(url,params,type){
			EMS.Loading.show();
			$.getJSON(url,params, function(data) {

				try{
					showTrendData(data);
					showTable(data,type);
					showRingRatio(data);
					showEnergyAvgs(data);
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

		function showTrendData(data){

			var times=[];
			var values=[];

			var position;

			var typeValue = $("#dateType").val();

			switch(typeValue){
				case "DD"://日
					position = 1;
				break;
				case "MM"://月
					position = 0
				break;
				case "QQ"://季度
					position = 0;
				break;

			}

			$.each(data.timeData, function(index, val) {
				times.push(val.time.split(' ')[position]);
				values.push(val.value);
			});

			var series={
				type:'bar',
				data:values
			};

			var grid ={
                left: 30,
                right: 10,
                top:5,
                bottom:25
            };

			EMS.Chart.showBar(echarts,$("#history-line"),undefined,times,series,grid,"default");
		}

		function showTable(data,type){
			//console.log(type);
			var currentDate = new Date();

			var columns=[];
			var rows=[];
			switch(type){
				case 'Circuit':
					columns.push({field:'name',title:'回路名称'});
				break;
				case 'Dept':
					columns.push({field:'name',title:'部门名称'});
				break;
				case 'Region':
					columns.push({field:'name',title:'区域名称'});
				break;
			}

			var typeValue = $("#dateType").val();

			switch(typeValue){
				case "DD"://日
					for (var i = 0; i <= 23; i++) {
						columns.push({field:i,title:i+"时"});
					}
				break;
				case "MM"://月
					var monthDate = new Date(parseInt($("#daycalendarBox").val().split('-')[0]),
						parseInt($("#daycalendarBox").val().split('-')[1]),0).getDate();
					for (var i = 1; i <= monthDate; i++) {
						columns.push({field:i,title:i+"日"});
					}
				break;
				case "QQ"://季度
					var endMonth = $("#season").val();
					for (var i = endMonth-2; i <= endMonth; i++) {
						columns.push({field:i,title:i+"月"});
					}
				break;
			}

			var row={};
			$.each(data.timeData, function(key, val) {
				var time = new Date(val.time);
				row.name = val.name;
				row[getColumnByType(typeValue,time)] = val.value;
			});

			rows.push(row);

			$("#dayReport").html('<table></table>');
			$("#dayReport>table").attr('data-height',$("#dayReport").height());

			EMS.DOM.showTable($("#dayReport>table"),columns,rows,{striped:true,classes:'table table-border'});
		}

		function getColumnByType(type,time){
			var timeIndex;

			switch(type){
				case "DD"://日
					timeIndex = time.getHours();
				break;
				case "MM"://月
					timeIndex = time.getDate();
				break;
				case "QQ"://季度
					timeIndex = time.getMonth()+1;
				break;
			}

			return timeIndex;
		}

		function showRingRatio(data){
			if(data.momData.length===0)
				return;

			var obj = data.momData[0];

			if(obj.hasOwnProperty('value'))
				$("#currentMonth").html(obj.value + " kW·h");
			else
				$("#currentMonth").html("-");

			if(obj.hasOwnProperty('lastValue'))
				$("#lastMonth").html(obj.lastValue + " kW·h");
			else
				$("#lastMonth").html("-");

			if(obj.hasOwnProperty('diffValue')){
				if(obj.diffValue>0)
					$("#ringRatio").html(obj.diffValue + " kW·h<img src='../app/img/Overall-up.png'/>");
				else
					$("#ringRatio").html(obj.diffValue + " kW·h<img src='../app/img/Overall-down.png'/>");
			}
			else
				$("#ringRatio").html("-");
		}

		function showEnergyAvgs(data){
			if(data.hasOwnProperty('monthAverageData')){
				if(data.monthAverageData.length > 0){
					data.monthAverageData[0].hasOwnProperty('areaAvg')
						? $("#square-month>h3").html(data.monthAverageData[0].areaAvg+"kW·h/㎡") 
						: $("#square-month>h3").html("-");

					data.monthAverageData[0].hasOwnProperty('peopleAvg') 
						? $("#people-month>h3").html(data.monthAverageData[0].peopleAvg+"kW·h/人") 
						: $("#people-month>h3").html("-");	
				}
			}

			if(data.hasOwnProperty('yearAverageData')){
				if(data.yearAverageData.length > 0){
					data.yearAverageData[0].hasOwnProperty('areaAvg')
						? $("#square-year>h3").html(data.yearAverageData[0].areaAvg+"kW·h/㎡") 
						: $("#square-year>h3").html("-");

					data.yearAverageData[0].hasOwnProperty('peopleAvg') 
						? $("#people-year>h3").html(data.yearAverageData[0].peopleAvg+"kW·h/人") 
						: $("#people-year>h3").html("-");	
				}
			}
		}
	};

	return _overAll;

})();

jQuery(document).ready(function($) {
	
	$("#overAll").attr("class","start active");
	$("#overAll").attr("class","active");
	
	var overAll = new OverAll();
	overAll.show();
});