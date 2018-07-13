var RegionMain = (function(){

	function _main(){

		this.show=function(){
			var url = "/api/RegionMain";

			getDataFromServer(url,"");
		};

		this.initDom = function(){
			$("#energylist").change(function(event) {
				var url = "/api/RegionMain";
				getDataFromServer(url,"buildId="+$("#buildinglist").val()+
					"&energyCode="+$("#energylist").val())
			});
		}

		function getDataFromServer(url,params){
			EMS.Loading.show($("#main-content").parent('div'));
			$.getJSON(url, params, function(data) {
				//console.log(data);
				if(data.hasOwnProperty('message'))
					location = "/Account/Login";
				try{
					console.log(data);
					showBuildList(data);
					showEnergys(data);
					showCompare(data)
					//showRank(data);
					//showPie(data);
					//showStackBar(data);
				}catch(e){

				}finally{
					EMS.Loading.hide($("#main-content").parent('div'));
				}

				
			}).fail(function(e){
				EMS.Tool.statusProcess(e.status);
			});
		}

		//显示建筑列表
		function showBuildList(data){

			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");

			$("#buildinglist").change(function(event) {
				
				var url = "/api/RegionMain";
				getDataFromServer(url,"buildId="+$("#buildinglist").val())
			});
		};

		function showEnergys(data){
			if(!data.hasOwnProperty('energys'))
				return;
			EMS.DOM.initSelect(data.energys,$("#energylist"),"energyItemName","energyItemCode");
		}

		//显示当日昨日对比
		function showCompare(data){

			$("#compareBar").html("");

			if(!data.hasOwnProperty('compareValues'))
				return;

			var today=[];
			var yesterday=[];
			var names=[];

			$.each(data.compareValues, function(key, val) {
				
				if($.inArray(val.name, names)<0){
					var count =names.push(val.name);
					var arr = data.compareValues.filter(function(innerobj){
						return innerobj.name == val.name;
					});

					$.each(arr, function(index, value) {
						var date = new Date(value.time);
						if(date.getDate() == new Date().getDate()){
							today[count-1] = value.value;
						}else{
							yesterday[count-1] = value.value;
						}
					});
				}

				
			});

			var series=[
				{
					name: '今日',
	            	type: 'bar',
	            	data:today
				},{
					name: '昨日',
	            	type: 'bar',
	            	data:yesterday
				}
			];


			EMS.Chart.showLandscapeBar(echarts,$("#compareBar"),['今日','昨日'],names,series);
		};

		//显示排名信息
		function showRank(data){
			//清除按钮，清除图表
			$("#regionBtns").html("");
			$("#rankBar").html("");

			if(!data.hasOwnProperty('rankValues'))
				return;

			var ids = [];

			$.each(data.rankValues, function(key, val) {
				if(!$.inArray(val.classifyID, ids)){
					ids.push(val.classifyID);
					$("#regionBtns").append('<div class="col-sm-12 col-xs-3" value="'+
						val.classifyID+'"><span>'+val.classifyName+'</span></div>');
				}
			});


		}

	};

	return _main;

})();

jQuery(document).ready(function($) {

	$("#leenergy").attr("class","start active");
	$("#le_survey").attr("class","active");
	
	var regionMain = new RegionMain();
	regionMain.initDom();
	regionMain.show();
});