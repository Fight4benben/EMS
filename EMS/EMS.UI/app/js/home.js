var Home = (function(){
	function _home(){

		//不同分类下的小时数据与对比数据
		var energyArray=[];

		this.initDom = function(){
			initEnergyClassifyTable();
			initDateTimePicker();
			initBuildInfo();
			initChart();
		};
		
		this.showHome=function(url,params){
			getDataFromServer(url,params);
		};

		function initDateTimePicker(){
			EMS.DOM.initDateTimePicker("CURRENTDATE",new Date(),$("#calendar"),$("#box"));

			$("#box").change(function(event) {
				var params = 'buildId='+$("#buildinglist").val()+'&date='+$("#box").val();

				getDataFromServer("api/homepage/home",params);
			});
		};

		function initBuildInfo(){
			$("#buildinginfo h4").text("");
			$("#buildinginfo span").text("");
			$("#TransCnt h4").text('-');
			$("#InstallCap h4").text("-");
			$("#OperateCap h4").text("-");
			$("#metersTotal h4").text("-");
		}

		function initEnergyClassifyTable(){
			$("#energytable tbody").html("");
		}

		function initChart(){
			$("#main_pie").html("");
		};

		function initButtons(){
			$("#btngroup").html("");
		}

		//从服务器端获取数据的方法
		function getDataFromServer(url,params){

			jQuery.getJSON(url,params, function(data) {
			  console.log(data);
			  showBuildList(data);
			  showBuildInfo(data);
			  showEnergyClassifyTable(data);
			  showEnergyItemPie(data);
			  showCompareEnergyButton(data);
			});
		};

		//显示建筑列表
		function showBuildList(data){

			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");
		};

		//显示当前建筑物详细信息
		function showBuildInfo(data){
			initBuildInfo();
			if(!data.hasOwnProperty('currentBuild'))
				return;

			var currentBuild = data.currentBuild;

			$("#buildinginfo h4").text(currentBuild.buildName);
			$("#buildinginfo span").eq(0).text(currentBuild.buildAddr);
			$("#buildinginfo span").eq(1).text("建筑面积"+currentBuild.totalArea+"平方米");

			$("#TransCnt h4").text(currentBuild.transCount);
			$("#InstallCap h4").text(currentBuild.installCapacity);
			$("#OperateCap h4").text(currentBuild.operateCapacity);
			$("#metersTotal h4").text(currentBuild.designMeters);
		};

		//显示分类能耗表格
		function showEnergyClassifyTable(data){

			initEnergyClassifyTable();

			if(!data.hasOwnProperty('energyClassify'))
				return;

			if(data.energyClassify.length==0)
				return;

			$.each(data.energyClassify, function(key, val) {

				$("#energytable tbody").append('<tr><td>'+val.energyItemName+'</td><td>'+
					(isNaN(val.monthValue)?'-':val.monthValue.toFixed(2))+'</td><td>'+
					(isNaN(val.yearValue)?'-':val.yearValue.toFixed(2))+'</td><td>'+val.unit+'</td></tr>');
			});
		}

		//显示饼图
		function showEnergyItemPie(data){

			initChart();

			if(!data.hasOwnProperty('energyItems'))
				return;

			if(data.energyItems.length==0)
				return;

			var values=[];
			var names=[];

			$.each(data.energyItems, function(key, val) {
				values.push({name:val.energyItemName,value:val.value});
				names.push(val.energyItemName);
			});

			EMS.Chart.showPie(echarts,$('#main_pie'),names,values,"分项用能");
		}

		function showCompareEnergyButton(data){

			initButtons();

			if(!data.hasOwnProperty('compareValues'))
				return;

			if(data.compareValues.length==0)
				return;

			$.each(data.compareValues, function(key, val) {
				switch(val.energyItemCode){
					case "01000":
						$("#btngroup").append('<div class="col-sm-1 col-xs-2"><acronym title="电"><button class="btn btn-elc" type="button"></button></acronym></div>');
					break;
					case "02000":
						$("#btngroup").append('<div class="col-sm-1 col-xs-2"><acronym title="水"><button class="btn btn-water" type="button"></button></acronym></div>');
					break;
					case "03000":
						$("#btngroup").append('<div class="col-sm-1 col-xs-2"><acronym title="燃气"><button class="btn btn-gas" type="button"></button></acronym></div>');
					break;
					case "13000":
						$("#btngroup").append('<div class="col-sm-1 col-xs-2"><acronym title="光伏"><button class="btn btn-solar" type="button"></button></acronym></div>');
					break;
				}

				var hourToday = data.hourValues.todayValues.filter(function(hourValue){
					return hourValue.energyItemCode == val.energyItemCode;
				});
				var hourYesterday = data.hourValues.yesterdayValues.filter(function(hourValue){
					return hourValue.energyItemCode == val.energyItemCode;
				});
				energyArray.push({code:val.energyItemCode,compareValues:{today:val.todayValue,yesterday:val.yesterdayValue},
					hourValues:{today:hourToday,yesterday:hourYesterday}});
			});

			console.log(energyArray);

		};



	};
	return _home;
})();
jQuery(document).ready(function($) {
	
	var home = new Home();
	home.initDom();
	home.showHome("api/homepage/home","");

	

});