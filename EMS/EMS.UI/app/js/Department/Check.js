var Check = (function(){

	function _check(){

		this.show = function(){
			initDom();

			var url="/api/DepartmentCheck";

			getDataFromServer(url,"");
		}

		function initDom(){
			//季度选择框默认不显示
			showOrHideSeason(false);
			//初始化日期
			initDate("MM");

			initDateType();

			initSearchButton();
		}

		function showOrHideSeason(isShow){
			isShow?$(".season-alarm").show():$(".season-alarm").hide();
		}

		function initDate(type){
			switch(type){
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
				case "YY":
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

		function initSearchButton(){
			$("#searchButton").click(function(event) {
				
				var buildId = $("#buildinglist").val();
				var date; 

				var type =$("#dateType").val();
				if(type=="QQ"){
					var season = $("#season").val();
					date = $("#daycalendarBox").val()+"-"+EMS.Tool.appendZero(parseInt(season)*3);
				}else if(type=="YY"){
					date = $("#daycalendarBox").val()+"-01-01";
				}else 
					date = $("#daycalendarBox").val()+"-01";

				var energyCode = $("#energys").val();

				var url = "/api/DepartmentCheck";
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

			var buildId = $("#buildinglist").val();

			var typeValue = $("#dateType").val();

			var targetValue = 0.02;
			$.each(data.deptCompletionRate, function(key, val) {
				if(val.energyCode === $("#energys").val())
					val.hasOwnProperty('rate')?targetValue = val.rate : targetValue = 0.02;
			});
			var year,month;
			var columns;
			switch(typeValue){
				case "MM"://月
					year = $("#daycalendarBox").val().split('-')[0];
					month = $("#daycalendarBox").val().split('-')[1];
					columns=[
						{field:'name',title:'名称'},
						{field:'areaValue',title:'单位面积能耗('+year+"."+month+')'},
						{field:'areaLastValue',title:'单位面积能耗('+(parseInt(year)-1)+"."+month+')'},
						{field:'totalValue',title:'总能耗('+year+"."+month+')'},
						{field:'totalLastValue',title:'总能耗('+(parseInt(year)-1)+"."+month+')'},
						{field:'rate',title:'比例(%)'}
					];
				break;
				case "QQ"://季度
					year = $("#daycalendarBox").val();
					month = $("#season").val();
					columns=[
						{field:'name',title:'名称'},
						{field:'areaValue',title:'单位面积能耗('+year+"年"+month+'季度)'},
						{field:'areaLastValue',title:'单位面积能耗('+(parseInt(year)-1)+"年"+month+'季度)'},
						{field:'totalValue',title:'总能耗('+year+"年"+month+'季度)'},
						{field:'totalLastValue',title:'总能耗('+(parseInt(year)-1)+"年"+month+'季度)'},
						{field:'rate',title:'比例(%)'}
					];
				break;
				case "YY"://年
					year = $("#daycalendarBox").val();
					columns=[
						{field:'name',title:'名称'},
						{field:'areaValue',title:'单位面积能耗('+year+')'},
						{field:'areaLastValue',title:'单位面积能耗('+(parseInt(year)-1)+')'},
						{field:'totalValue',title:'总能耗('+year+')'},
						{field:'totalLastValue',title:'总能耗('+(parseInt(year)-1)+')'},
						{field:'rate',title:'比例(%)'}
					];
				break;

			}

			var datas=[];

			$.each(data.totalCompareData, function(key, val) {
				var obj={id:val.id,name:val.name};
				if(val.hasOwnProperty('value'))
					obj["totalValue"]=val.value;

				if(val.hasOwnProperty('lastValue'))
					obj["totalLastValue"] = val.lastValue;

				if(val.hasOwnProperty('rate'))
					obj["rate"] = val.rate;

				datas.push(obj);
			});

			$.each(data.areaAvgCompareData, function(key, val) {
				var innerIndex;
				var filters = datas.filter(function(obj,index){
					if(obj.id === val.id)
						innerIndex = index;
					return obj.id === val.id;
				});

				if(filters.length>0){
					if(val.hasOwnProperty('value'))
						datas[innerIndex]["areaValue"]=val.value;

					if(val.hasOwnProperty('lastValue'))
						datas[innerIndex]["areaLastValue"] = val.lastValue;
				}else{
					var obj={id:val.id,name:val.name};
					if(val.hasOwnProperty('value'))
						obj["areaValue"]=val.value;

					if(val.hasOwnProperty('lastValue'))
						obj["areaLastValue"] = val.lastValue;

					if(val.hasOwnProperty('rate'))
						obj["rate"] = val.rate;

					datas.push(obj);
				}

			});

			var height = $("#alarmTable").height();
			$("#alarmTable").html('<table></table>');
			$("#alarmTable>table").attr('data-height',height);

			EMS.DOM.showTable($("#alarmTable>table"),columns,datas,{striped:true,classes:'table table-border',
				rowStyle:function(row,index){
					var style={};

					if(parseInt(row.rate)>= -targetValue*100)
						style={css:{'background-color':'#FFFF00'}};
					else if(parseInt(row.rate)<-(targetValue*100))
						style={css:{'background-color':'#1E90FF','color':'white'}};
					   
		             return style;
				}});


		}
	};

	return _check;

})();

jQuery(document).ready(function($) {
	$("#deenergy").attr("class","start active");
	$("#de_check").attr("class","active");

	var check = new Check();

	check.show();
});