'use strict';
var RingRatio = (function(){

	function _ringRatio(){

		this.show = function(){

			var url="/api/CircuitRingRatio";
			var buildId=$.cookie('buildId');
			if(buildId==undefined || buildId==null || buildId == "null")
				getDataFromServer(url);
			else
				getDataFromServer(url,"buildId="+buildId);

		};

		this.initDom = function(){
			initDateTime();
			initEnergyBtns();
		}

		function initDateTime(){
			EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#calendar"),$("#calendarbox"),{
				format:'yyyy-mm-dd',
		        language: 'zh-CN',
		        autoclose: 1,
		        startView: 2,
		        minView: 2,
		        forceParse: 0,
		        pickerPosition: "bottom-left"
			});

			$("#calendarbox").change(function(event) {
				var date = $(this).val();
				var buildId = $("#buildinglist").val();
				var energyCode = $(".btn-solar-selected").attr('value');
				
				 //console.log($("#daycalendarBox").val());
				getDataFromServer("/api/CircuitRingRatio","buildId="+buildId+"&energyCode="+energyCode+"&date="+date);
			});
		}

		function initEnergyBtns(){
			$("#te_CompareBtns").html("");

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

				$('#treeview').treeview('selectNode', [ nodes[0].nodeId ]);

			});
		}

		function getDataFromServer(url,params){
			EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				try{
					showBuilds(data);
					showEnergys(data);
					showTreeview(data);
					showData(data);
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
				var buildId = $(this).val();
				
				$.cookie("buildId",buildId,{path:'/'});
				getDataFromServer("/api/CircuitRingRatio","buildId="+buildId+"&date="+$("#calendarbox").val());
			});
		};

		//生成不同能源类型的按钮
		function showEnergys(data){
			
			if(!data.hasOwnProperty('energys'))
				return;

			initEnergyBtns();

			$.each(data.energys, function(key, val) {

				switch(val.energyItemCode){
					case "01000":
						$("#te_CompareBtns").append('<acronym title="电"><button id="elec" class="btn btn-elc" value="01000" style="width: 20px; height: 20px;" unit="'+val.energyItemUnit+'" type="button"></button></acronym>')
					break;
					case "02000":
						$("#te_CompareBtns").append('<acronym title="水"><button id="water" class="btn btn-water" value="02000" style="width: 20px; height: 20px;" unit="'+val.energyItemUnit+'" type="button"></button></acronym>')
					break;
					case "13000":
						$("#te_CompareBtns").append('<acronym title="光伏"><button id="solar" class="btn btn-solar" value="13000" style="width: 20px; height: 20px;"  unit="'+val.energyItemUnit+'" type="button"></button></acronym>')
					break;
					default:
						$("#te_CompareBtns").append('<acronym title="'+val.energyItemName+'"><button id="compare'+val.energyItemCode+'" class="btn btn-empty" value="'+val.energyItemCode+'" unit="'+val.energyItemUnit+'" type="button">'+
							val.energyItemName.substring(0,1)+'</button></acronym>');
				}
			});

			$("#te_CompareBtns button").eq(0).addClass('btn-solar-selected')

			$("#te_CompareBtns button").click(function(event) {//为能源按钮绑定click事件，进行数据加载
				var $current = $(this);

				var isNotRepeat = setEnergyBtnStyle($current);

				if(isNotRepeat){
					//发送请求
					getDataFromServer("/api/CircuitRingRatio","buildId="+$("#buildinglist").val()+"&energyCode="+
						$current.attr('value')+"&date="+$("#calendarbox").val());
				}
			});
		};

		//选中分类能耗按钮后添加选中样式或判断如果再次点击本分类，则不重新加载数据
		function setEnergyBtnStyle($current){
			var id = $("#te_CompareBtns .btn-solar-selected")[0].id;

			if($current[0].id==id){
				return false;
			}

			$("#te_CompareBtns button").removeClass('btn-solar-selected');

			$current.addClass('btn-solar-selected');

			return true;
		}

		//根据数据显示树状结构，如果不包含树状结构数据则不更新数据。
		function showTreeview(data){
			if(!data.hasOwnProperty('treeView'))
				return;

			$("#treeview").html("");//更新树状结构之前先将该区域清空
			$("#treeview").parent('div').css('overflow','auto');
			$("#treeview").width(350);
			$(".count-info-te").height($("#main-content").height());
			//console.log($("#main-content").height());
			$("#treeview").height($(".count-info-te").height() - 300);

			$(".treeview .list-group").css('height','');
			

			EMS.DOM.initTreeview(data.treeView,$("#treeview"),{
				showIcon: true,
				showCheckbox: false,
				showBorder:false,
				levels:2,
				onNodeSelected:function(event,node){
					var id = node.id;
					var buildId = $("#buildinglist").val();
					var date = $("#calendar").val();
					var energyCode = $(".btn-solar-selected").attr('value');

					getDataFromServer("/api/CircuitRingRatio","buildId="+buildId+"&energyCode="+
						energyCode+"&circuitId="+id+"&date="+$("#calendarbox").val());
				}
			});

			$("#treeview").treeview('selectNode',[0,{silent:true}]);
		};

		function showData(data){

			var currentData = [];
			var previousData=[];

			$.each(data.compareData, function(key, val) {
				
				var date = new Date(val.time);

				if(date.getFullYear()+"-"+EMS.Tool.appendZero(date.getMonth()+1)+"-"+EMS.Tool.appendZero(date.getDate()) 
					== $("#calendarbox").val()){
					currentData[date.getHours()] = val.value;
				}else{
					previousData[date.getHours()] = val.value;
				}
			});

			showChart(currentData,previousData);

			showTable(currentData,previousData);
			
		}

		function showChart(currentData,previousData){
			var series = [
				{
                    name: '本期',
                    type: 'bar',
                    data: currentData,

                },{
                    name: '同期',
                    type: 'bar',
                    data: previousData,
                }
			];

			var legend={
				data: ['本期', '同期'],
                top:'top'
			};

			var times=['0时','1时', '2时', '3时', '4时', '5时', '6时', '7时', '8时', '9时', '10时', '11时', '12时',
					'13时', '14时', '15时', '16时', '17时', '18时', '19时', '20时', '21时', '22时', '23时'];

			var grid ={
                left: 80,
                right: 10,
                top:5,
                bottom:25
            };

			EMS.Chart.showBar(echarts,$("#compareChart"),legend,times,series,grid);
		}

		function showTable(currentData,previousData){
			$("#comparetable").html("");
			var name="";
			switch($(".btn-solar-selected").attr('value')){
				case "01000":
					name="用电量";
				break;
				case "02000":
					name="用水量"
				break;
				case "13000":
					name="发电量";
				break;
				default:
					name=$(".btn-solar-selected").parent('acronym').attr('title')+"用量";
			}

			var splitArray =  $("#calendarbox").val().split('-');
			var lastDay = parseInt(splitArray[2])-1;

			var columns=[
				{field:'time',title:'时间'},
				{field:'today',title:"["+$("#calendarbox").val()+"]"+name+"("+$(".btn-solar-selected").attr('unit')+")"},
				{field:'yesterday',title:"["+splitArray[0]+"-"+splitArray[1]+"-"+lastDay+"]"+name+"("+$(".btn-solar-selected").attr('unit')+")"},
				{field:'compare',title:'同比(%)'}
			];
			var rows=[];
			//$("#comparetable th").eq(1).text("["+$("#calendarbox").val()+"]"+name+"("+$(".btn-solar-selected").attr('unit')+")");
			//$("#comparetable th").eq(2).text("["+splitArray[0]+"-"+splitArray[1]+"-"+lastDay+"]"+name+"("+$(".btn-solar-selected").attr('unit')+")");

			for (var i = 0; i < 24; i++) {
				var row={};
				row.time = (i).toString()+'时';
				row.today = (currentData[i]==undefined?'-':currentData[i]);
				row.yesterday = (previousData[i]==undefined?'-':previousData[i]);
				row.compare = ((currentData[i]==undefined)||(previousData[i]==undefined||(previousData[i]==0))?'-':((currentData[i]-previousData[i])*100/previousData[i]).toFixed(2)+"%");
				
				rows.push(row);
			}

			rows.push({
				time:'环比',
				today:EMS.Tool.getSum(currentData).toFixed(2),
				yesterday:EMS.Tool.getSum(previousData).toFixed(2),
				compare:((EMS.Tool.getSum(currentData)-EMS.Tool.getSum(previousData))*100/EMS.Tool.getSum(previousData)).toFixed(2)+'%'
			});

			var height = $("#comparetable").height();
			$("#comparetable").html('<table></table>');
			$("#comparetable>table").attr('data-height',height);

			EMS.DOM.showTable($("#comparetable>table"),columns,rows,{striped:true,classes:'table table-border'});
		}

	};

	return _ringRatio;

})();

jQuery(document).ready(function($) {

	$("#flenergy").attr("class","start active");
	$("#ringratio").attr("class","active");

	var ringRatio = new RingRatio();

	ringRatio.initDom();
	ringRatio.show();
	
});