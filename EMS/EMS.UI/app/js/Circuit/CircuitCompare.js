'use strict';
var CircuitCompare = (function(){

	function _compare(){

		this.show = function(){
			var url="/api/CircuitCompare";

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
			EMS.DOM.initDateTimePicker('YEAR',new Date(),$("#calendar"),$("#calendarbox"),{
				format:'yyyy',
		        language: 'zh-CN',
		        autoclose: 1,
		        startView: 4,
		        minView: 4,
		        forceParse: 0,
		        pickerPosition: "bottom-left"
			});

			$("#calendarbox").change(function(event) {
				var date = $(this).val();
				var buildId = $("#buildinglist").val();
				var energyCode = $(".btn-solar-selected").attr('value');
				
				 //console.log($("#daycalendarBox").val());
				getDataFromServer("/api/CircuitCompare","buildId="+buildId+"&energyCode="+energyCode+"&date="+date);
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
				var buildId = $("#buildinglist").val();
				$.cookie("buildId",buildId,{path:'/'});
				
				getDataFromServer("/api/CircuitCompare","buildId="+buildId+"&date="+$("#calendarbox").val());
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
					case "03000":
						$("#te_CompareBtns").append('<acronym title="燃气"><button id="gas" class="btn btn-gas" value="03000" style="width: 20px; height: 20px;" unit="'+val.energyItemUnit+'" type="button"></button></acronym>')
					break;
					case "40000":
						$("#te_CompareBtns").append('<acronym title="蒸汽"><button id="steam" class="btn btn-steam" value="40000" style="width: 20px; height: 20px;"  unit="'+val.energyItemUnit+'" type="button"></button></acronym>')
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
					getDataFromServer("/api/CircuitCompare","buildId="+$("#buildinglist").val()+"&energyCode="+
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

					getDataFromServer("/api/CircuitCompare","buildId="+buildId+"&energyCode="+
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

				if(date.getFullYear() == parseInt($("#calendarbox").val())){
					currentData[date.getMonth()] = val.value;
				}else{
					previousData[date.getMonth()] = val.value;
				}
			});

			showChart(currentData,previousData);

			//console.log();
			
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

			var times=['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'];

			var grid ={
                left: 80,
                right: 10,
                top:5,
                bottom:25
            };

			EMS.Chart.showBar(echarts,$("#compareChart"),legend,times,series,grid);
		}

		function showTable(currentData,previousData){

			$("#comparetable tbody").html("");
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

			$("#comparetable th").eq(1).text("["+$("#calendarbox").val()+"]"+name+"("+$(".btn-solar-selected").attr('unit')+")");
			$("#comparetable th").eq(2).text("["+(parseInt($("#calendarbox").val())-1).toString()+"]"+name+"("+$(".btn-solar-selected").attr('unit')+")");

			for (var i = 0; i < 12; i++) {
				var pp = previousData[11];
				if(i==0 && pp != undefined){
					$("#comparetable tbody").append('<tr><td>'+(i+1).toString()+'月</td><td>'+
					(currentData[i]==undefined?'-':currentData[i])+'</td><td>'+
					(previousData[i]==undefined?'-':previousData[i])+'</td><td>'+
					((currentData[i]==undefined)||(previousData[i]==undefined||(previousData[i]==0))?'-':
					((currentData[i]-previousData[i])*100/previousData[i]).toFixed(2)+"%")+"</td><td>"+
					
					((currentData[i]==undefined)||((previousData[previousData.length-1]==undefined))?'-':
					((currentData[i]-pp)*100/currentData[i]).toFixed(2)+"%")+"</td></tr>");
					
				}else{
					$("#comparetable tbody").append('<tr><td>'+(i+1).toString()+'月</td><td>'+
					(currentData[i]==undefined?'-':currentData[i])+'</td><td>'+
					(previousData[i]==undefined?'-':previousData[i])+'</td><td>'+
					((currentData[i]==undefined)||(previousData[i]==undefined||(previousData[i]==0))?'-':
					((currentData[i]-previousData[i])*100/previousData[i]).toFixed(2)+"%")+"</td><td>"+

					((currentData[i]==undefined)?'-':((currentData[i]-currentData[i-1])*100/currentData[i]).toFixed(2)+"%")+"</td></tr>");
				}
			}
		}

	};

	return _compare;

})();

jQuery(document).ready(function($) {

	$("#flenergy").attr("class","start active");
	$("#table").attr("class","active");

	var compare = new CircuitCompare();

	compare.initDom();
	compare.show();
	
});