'use strict';
var Compare = (function(){

	function _compare(){

		var selectedId;

		this.show = function(){

			var url="/api/ItemCompare";


			getDataFromServer(url);

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

				var node = $("#treeview").treeview('getSelected');

				if(node.length ==0)
					return;
				
				 //console.log($("#daycalendarBox").val());
				getDataFromServer("/api/ItemCompare","buildId="+buildId+"&formulaId="+node[0].id+"&date="+date);
			});
		}

		function initEnergyBtns(){
			$("#se_CompareBtns").html("");
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

			$("#buildinglist").change(function(event) {
				var buildId = $(this).val();
				
				 //console.log($("#daycalendarBox").val());
				getDataFromServer("/api/ItemCompare","buildId="+buildId+"&date="+$("#calendarbox").val());
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
						$("#se_CompareBtns").append('<acronym title="电"><button id="elec" class="btn btn-elc" value="01000" style="width: 20px; height: 20px;" unit="'+val.energyItemUnit+'" type="button"></button></acronym>')
					break;
				}
			});

			$("#se_CompareBtns button").eq(0).addClass('btn-solar-selected')
		};

		//选中分类能耗按钮后添加选中样式或判断如果再次点击本分类，则不重新加载数据
		function setEnergyBtnStyle($current){
			var id = $("#se_CompareBtns .btn-solar-selected")[0].id;

			if($current[0].id==id){
				return false;
			}

			$("#se_CompareBtns button").removeClass('btn-solar-selected');

			$current.addClass('btn-solar-selected');

			return true;
		}

		//根据数据显示树状结构，如果不包含树状结构数据则不更新数据。
		function showTreeview(data){
			$("#treeview").html("");//更新树状结构之前先将该区域清空

			if(!data.hasOwnProperty('treeView'))
				return;

			if(data.treeView.length==0)
				return;

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
					selectedId = id;
					var buildId = $("#buildinglist").val();
					var date = $("#calendar").val();

					getDataFromServer("/api/ItemCompare","buildId="+buildId+
						"&formulaId="+id+"&date="+$("#calendarbox").val());
				}
			});

			$("#treeview").treeview('selectNode',[0,{silent:true}]);
		};

		function showData(data){
			$("#compareChart").html("");
			$("#comparetable tbody").html("");

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
				$("#comparetable tbody").append('<tr><td>'+(i+1).toString()+'月</td><td>'+
					(currentData[i]==undefined?'-':currentData[i])+'</td><td>'+
					(previousData[i]==undefined?'-':previousData[i])+'</td><td>'+
					((currentData[i]==undefined)||(previousData[i]==undefined||(previousData[i]==0))?'-':((currentData[i]-previousData[i])*100/previousData[i]).toFixed(2)+"%")+"</td></tr>");
			}
		}

	};

	return _compare;

})();

jQuery(document).ready(function($) {

	$("#seenergy").attr("class","start active");
	$("#se_compare").attr("class","active");

	var compare = new Compare();

	compare.initDom();
	compare.show();
	
});