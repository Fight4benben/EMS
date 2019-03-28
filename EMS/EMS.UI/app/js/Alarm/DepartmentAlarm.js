var DepartmentAlarm = (function(){

	function _departmentAlarm(){
		this.show = function(){
			initDom();

			var url="/api/AlarmDepartment";

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
				EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(new Date()-24*60*60*1000),$("#dayCalendar"),$("#daycalendarBox"));
				break;
				case "MM"://月
				EMS.DOM.initDateTimePicker('YEARMONTH',new Date(new Date().getFullYear(),new Date().getMonth()-1),$("#dayCalendar"),$("#daycalendarBox"),{format:'yyyy-mm',
									        language: 'zh-CN',
									        autoclose: 1,
									        startView: 3,
									        minView: 3,
									        forceParse: false,
									        pickerPosition: "bottom-left"});
				break;
				case "SS"://季度
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

				typeValue === "SS" ? showOrHideSeason(true) : showOrHideSeason(false);

			});
		}

		function initSearchButton(){
			$("#searchButton").click(function(event) {
				
				var buildId = $("#buildinglist").val();
				var date; 

				var type =$("#dateType").val();
				if(type=="SS"){
					var season = $("#season").val();
					date = $("#daycalendarBox").val()+"-"+EMS.Tool.appendZero(parseInt(season));
				}else
					date = $("#daycalendarBox").val();
				var energyCode = $("#energys").val();

				var url = "/api/AlarmDepartment";
				var params = "buildId="+buildId+"&date="+date+"&type="+type+"&energyCode="+energyCode;

				getDataFromServer(url,params);

			});
		}


		function getDataFromServer(url,params){
			EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				//console.log(data);
				try{
					showBuilds(data);
					showEnergys(data);
					showTable(data);
				}catch(exception){

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

			var buildId = $("#buildinglist").val();
			var firstLevel = 0.2;
			var secondLevel = 0.5;

			if(data.buildAlarmLevels.length>0){
				data.buildAlarmLevels.filter(function(item) {
					firstLevel = item.level1;
					secondLevel = item.level2;
					return item.buildID === buildId;
				});
			}

			var datebox = new Date($("#daycalendarBox").val().replace(new RegExp('-','g'),'/'));

			var current;
			var last;

			var typeValue = $("#dateType").val();
			var columns;
			switch(typeValue){
				case "DD"://日
					current = datebox.getFullYear()+"年"+(datebox.getMonth()+1) +'月'+datebox.getDate()+'日';
					datebox.setDate(datebox.getDate()-1);
					last = datebox.getFullYear()+"年"+(datebox.getMonth()+1) +'月'+(datebox.getDate())+'日';
					columns=[
						{field:'name',title:'名称'},
						{field:'value',title:'('+current+')用能'},
						{field:'lastValue',title:'('+last+')用能'},
						{field:'diffValue',title:'差值'},
						{field:'rate',title:'比例'}
					];
				break;
				case "MM"://月
					current = datebox.getFullYear()+"年"+(datebox.getMonth()+1) +'月';
					datebox.setMonth(datebox.getMonth()-1);
					last = datebox.getFullYear()+"年"+(datebox.getMonth()+1) +'月';
					columns=[
						{field:'name',title:'名称'},
						{field:'value',title:'('+current+')用能'},
						{field:'lastValue',title:'('+last+')同期'},
						{field:'diffValue',title:'差值'},
						{field:'rate',title:'比例'}
					];
				break;
				case "SS"://季度
					current = datebox.getFullYear()+'年'+$("#season").val()/3 + '季';

					if(parseInt($("#season").val())/3===1){
						last = (datebox.getFullYear() -1 )+'年4季';
					}else
						last = datebox.getFullYear() + '年' + (parseInt($("#season").val())/3-1)+'季';

					columns=[
						{field:'name',title:'名称'},
						{field:'value',title:'('+current+')用能'},
						{field:'lastValue',title:'('+last+')同期'},
						{field:'diffValue',title:'差值'},
						{field:'rate',title:'比例'}
					];
				break;

			}
			

			var rows = [];

			$.each(data.compareDatas, function(key, val) {
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

					if(parseInt(row.level)>=(firstLevel*100) && parseInt(row.level)<=(secondLevel*100))
						style={classes:'warning'}//style={css:{'background-color':'#FFFF00'}};
					else if(parseInt(row.level)>(secondLevel*100))
						style={classes:'danger'}//style={css:{'background-color':'#FF0000','color':'white'}};
					else if(parseInt(row.level)>=-(secondLevel*100) && parseInt(row.level)<=-(firstLevel*100))
						style={classes:'info'}//style={css:{'background-color':'#1E90FF','color':'white'}};
					else if(parseInt(row.level)<-(secondLevel*100))
						style={classes:'success'}//style={css:{'background-color':'#66CD00','color':'white'}};
					   
		             return style;
				}});

			$("table td").css('font-size','15px');

		}

	};

	return _departmentAlarm;

})();

jQuery(document).ready(function($) {
	$("#alarm").attr("class","start active");
	$("#deptAlarm").attr("class","active");
	
	var departmentAlarm = new DepartmentAlarm();

	departmentAlarm.show();
});