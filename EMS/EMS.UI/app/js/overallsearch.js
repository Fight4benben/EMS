var OverAll = (function(){

	function _overAll(){

		this.show = function(){
			$(".searchBtn").click(function(event) {
				var id = $(this)[0].id;
				var keyWord = $("#search-keyword").val();

				if(keyWord===""){
					alert("请输入查询内容！");
					return;
				}

				var url="/api/OverAllSearch";
				var params ="type="+id+"&keyWord="+keyWord+"&endDay="+EMS.Tool.dateFormat(new Date());

				getDataFromServer(url,params,id);
			});
		}



		function getDataFromServer(url,params,type){
			EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				//console.log(data);
				try{
					showTrendData(data);
					showTable(data,type);
					showRingRatio(data);
				}catch{

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

			$.each(data.last31Day, function(index, val) {
				times.push(val.time.split(' ')[0]);
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

			for (var i = 1; i <= currentDate.getDate(); i++) {
				columns.push({field:i,title:i+'日'});
			}

			var row={};
			$.each(data.monthDate, function(key, val) {
				var time = new Date(val.time);
				row.name = val.name;
				row[time.getDate()] = val.value;
			});

			rows.push(row);

			$("#dayReport").html('<table></table>');
			$("#dayReport>table").attr('data-height',$("#dayReport").height());

			EMS.DOM.showTable($("#dayReport>table"),columns,rows,{striped:true,classes:'table table-border'});
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
	};

	return _overAll;

})();

jQuery(document).ready(function($) {
	
	$("#overAll").attr("class","start active");
	$("#overAll").attr("class","active");
	
	var overAll = new OverAll();
	overAll.show();
});