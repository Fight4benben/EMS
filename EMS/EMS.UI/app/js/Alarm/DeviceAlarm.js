var DeviceAlarm = (function(){

	function _deviceAlarm(){

		this.show = function(){
			initDom();

			var url="/api/AlarmMomDay";

			getDataFromServer(url,"");
		}

		function initDom(){
			//季度选择框默认不显示
			showOrHideSeason(false);
			//初始化日期
			initDate("DD");

			initDateType();

			initSearchButton();
		}

		function showOrHideSeason(isShow){
			isShow?$(".season-alarm").show():$(".season-alarm").hide();
		}

		function initDate(type){
			switch(type){
				case "DD"://日
				EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
				break;
				case "MM"://月
				EMS.DOM.initDateTimePicker('YEARMONTH',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
				break;
				case "SS"://季度
				EMS.DOM.initDateTimePicker('YEAR',new Date(),$("#dayCalendar"),$("#daycalendarBox"));
				break;

			}
		}

		function initDateType(){
			$("#dateType").change(function(event) {
				var typeValue = $(this).val();
				initDate(typeValue);

				typeValue === "SS" ? showOrHideSeason(true) : showOrHideSeason(false);

			});
		}

		function initSearchButton(){
			$("#searchButton").click(function(event) {
				
				var buildId = $("#buildinglist").val();
				var date = $("#daycalendarBox").val();

				var url = "/api/AlarmMomDay";
				var params = "buildId="+buildId+"&date="+date;

				getDataFromServer(url,params);

			});
		}


		function getDataFromServer(url,params){
			EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				console.log(data);
				try{
					showBuilds(data);
					showEnergys(data);
					showTable(data);
				}catch{

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
		}

		function showEnergys(data){
			if(!data.hasOwnProperty('energys'))
				return;

			EMS.DOM.initSelect(data.energys,$("#energys"),"energyItemName","energyItemCode");
		}

		function showTable(data){

			var typeValue = $("#dateType").val();
			var columns;
			switch(typeValue){
				case "DD"://日
					columns=[
						{field:'name',title:'名称'},
						{field:'value',title:'当前用能'},
						{field:'lastValue',title:'前日同期'},
						{field:'diffValue',title:'差值'},
						{field:'rate',title:'比例'}
					];
				break;
				case "MM"://月
					columns=[
						{field:'name',title:'名称'},
						{field:'value',title:'当前用能'},
						{field:'lastValue',title:'上月同期'},
						{field:'diffValue',title:'差值'},
						{field:'rate',title:'比例'}
					];
				break;
				case "SS"://季度
					columns=[
						{field:'name',title:'名称'},
						{field:'value',title:'当前用能'},
						{field:'lastValue',title:'去年同期'},
						{field:'diffValue',title:'差值'},
						{field:'rate',title:'比例'}
					];
				break;

			}
			

			var rows = [];

			$.each(data.compareData, function(key, val) {
				var row={};
				row.name = val.name;
				row.value = val.value;
				row.lastValue = val.lastValue;
				row.diffValue = val.diffValue;
				row.rate = val.rate.toFixed(2)+"%";
				row.level = val.rate;

				rows.push(row);
			});

			var height = $("#alarmTable").height();
			$("#alarmTable").html('<table></table>');
			$("#alarmTable>table").attr('data-height',height);

			EMS.DOM.showTable($("#alarmTable>table"),columns,rows,{striped:true,classes:'table table-border',
				rowStyle:function(row,index){
					var style={};

					if(parseInt(row.level)>=20 && parseInt(row.level)<=50)
						style={css:{'background-color':'#FFFF00'}};
					else if(parseInt(row.level)>50)
						style={css:{'background-color':'#FF0000','color':'white'}};
					else if(parseInt(row.level)>=-50 && parseInt(row.level)<=-20)
						style={css:{'background-color':'#1E90FF','color':'white'}};
					else if(parseInt(row.level)<-50)
						style={css:{'background-color':'#66CD00','color':'white'}};
					   
		             return style;
				}});


		}

		function setRowColor(rowIndex){

		}

	};

	return _deviceAlarm;

})();

jQuery(document).ready(function($) {

	$("#alarm").attr("class","start active");
	$("#equipAlarm").attr("class","active");
	
	var deviceAlarm = new DeviceAlarm();

	deviceAlarm.show();

});