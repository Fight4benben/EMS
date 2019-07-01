var NoWorkDay = (function(){
    function _noWorkDay(){
        function initEnergyBtns(){
			$("#te_countBtns").html("");
		}
		//公开暴露的方法:初始化页面
		this.initDom = function(){
			initDateTime();
			initSearchButton();
		};
        function initDateTime(){
            EMS.DOM.initDateTimePicker('FIRSTDAY',new Date(),$("#STdayCalendar"),$("#STdaycalendarBox"));
            
			EMS.DOM.initDateTimePicker('CURRENTDATE',new Date(),$("#EDdayCalendar"),$("#EDdaycalendarBox"));
        }
        function initSearchButton(){
			//查询数据
			$("#daySearch").click(function(event) {
				getDataFromServer("/api/NoWorkDay",{
					buildID:$("#buildinglist").val(),energyCode:$('.btn-solar-selected').attr('value'),
					cicruitIDs:getCheckedTreeIdArray().join(','),beginDate:$("#STdaycalendarBox").val(),endDate:$("#EDdaycalendarBox").val()
				},'POST');
			});
            //导出
            $("#dayExport").click(function(event){
               //$('#mainTable').tableExport({type:'excel', fileName: $("#EDdaycalendarBox").val()+'非工作日工作日报表', escape:'false'});
               $('#mainTable').tableExport({type:'excel',escape:'false',fileName: '非工作日工作日报表'});
            })
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

				$.each(nodes, function(index, val) {
					$('#treeview').treeview('checkNode', [ val.nodeId, { silent: true } ]);
				});


				getDataFromServer("/api/NoWorkDay",{
					buildID:$("#buildinglist").val(),energyCode:$('.btn-solar-selected').attr('value'),
					cicruitIDs:getCheckedTreeIdArray().join(','),beginDate:$("#STdaycalendarBox").val(),endDate:$("#EDdaycalendarBox").val()
				},'POST');
			});
        }
        //选中分类能耗按钮后添加选中样式或判断如果再次点击本分类，则不重新加载数据
		function setEnergyBtnStyle($current){
			var id = $("#te_countBtns .btn-solar-selected")[0].id;

			if($current[0].id==id){
				return false;
			}

			$("#te_countBtns button").removeClass('btn-solar-selected');

			$current.addClass('btn-solar-selected');

			return true;
        }
        //获取选中的回路
		function getCheckedTreeIdArray(){
			var idArray = [];
			var treeArray = $("#treeview").treeview('getChecked');

			$.each(treeArray, function(key, val) {
				idArray.push(val.id);
			});

			return idArray;
        }
        //公开暴露的方法：页面加载后生成默认数据
		this.show = function(url,params){
			// var buildId=$.cookie('buildId');
			// if(buildId==undefined || buildId==null || buildId == "null")
				getDataFromServer(url,params);
			//else
				//getDataFromServer(url,"buildId="+buildId);
		};

		//从服务器获取json数据
		function getDataFromServer(url,params,httpType){
			EMS.Loading.show();
			if(httpType =="POST")
				$.post(url, params, function(data, textStatus, xhr) {
					try{
						showBuilds(data);
						showEnergys(data);
						showTreeview(data);
						showTable(data);
					}catch(e){

					}finally{
						EMS.Loading.hide();
					}
					
				}).fail(function(e){
					EMS.Tool.statusProcess(e.status);
					EMS.Loading.hide();
				});
			else
				jQuery.getJSON(url,params, function(data) {

				  try{
						showBuilds(data);
					  	showEnergys(data);
					    showTreeview(data);
					    showTable(data);
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

			$("#buildinglist").change(function(event) {

				// getDataFromServer("/api/NoWorkDay",{
				// 	buildID:$("#buildinglist").val(),energyCode:$('.btn-solar-selected').attr('value'),
				// 	cicruitIDs:getCheckedTreeIdArray().join(','),beginDate:$("#STdaycalendarBox").val(),endDate:$("#EDdaycalendarBox").val()
                // },'POST');
                getDataFromServer("/api/NoWorkDay",{
					buildID:$("#buildinglist").val(),energyCode:$('.btn-solar-selected').attr('value')
				});
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
						$("#te_countBtns").append('<acronym title="电"><button id="elec" class="btn btn-elc" value="01000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "02000":
						$("#te_countBtns").append('<acronym title="水"><button id="water" class="btn btn-water" value="02000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "13000":
						$("#te_countBtns").append('<acronym title="光伏"><button id="solar" class="btn btn-solar" value="13000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
                    break;
                    case "03000":
						$("#te_countBtns").append('<acronym title="燃气"><button id="gas" class="btn btn-gas" value="03000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					case "40000":
						$("#te_countBtns").append('<acronym title="蒸汽"><button id="steam" class="btn btn-steam" value="40000" style="width: 20px; height: 20px;" type="button"></button></acronym>')
					break;
					default:
						$("#te_countBtns").append('<acronym title="'+val.energyItemName+'"><button class="btn btn-empty" value="'+val.energyItemCode+'" type="button">'+
							val.energyItemName.substring(0,1)+'</button></acronym>');
				}
			});

			$("#te_countBtns button").eq(0).addClass('btn-solar-selected')

			$("#te_countBtns button").click(function(event) {//为能源按钮绑定click事件，进行数据加载
				var $current = $(this);

				var isNotRepeat = setEnergyBtnStyle($current);
                
				if(isNotRepeat){
					//发送请求
                    getDataFromServer("/api/NoWorkDay",{
                        buildID:$("#buildinglist").val(),energyCode:$('.btn-solar-selected').attr('value'),beginDate:$("#STdaycalendarBox").val(),endDate:$("#EDdaycalendarBox").val()
                    });
				}
			});
        };
        //根据数据显示树状结构，如果不包含树状结构数据则不更新数据。
		function showTreeview(data){
			if(!data.hasOwnProperty('treeView'))
				return;

			$("#treeview").html("");//更新树状结构之前先将该区域清空
			$("#treeview").parent('div').css('overflow','auto');
			$("#treeview").width(350);
			$(".count-info-te").height($("#main-content").height());
			$("#treeview").height($(".count-info-te").height() - 258);

			EMS.DOM.initTreeview(data.treeView,$("#treeview"),{
				showIcon: true,
				showCheckbox: true,
				showBorder:false,
				levels:2,
				onNodeChecked: function(event, node) {

					if($("#related").prop('checked'))
						checkChildren(node,$("#treeview"));
					else
						$("#treeview").treeview('checkNode',[node.nodeId,{silent:true}]);
				},
				onNodeUnchecked: function (event, node) {

					if($("#related").prop('checked'))
						unCheckChildren(node,$("#treeview"));
					else
						$("#treeview").treeview('uncheckNode',[node.nodeId,{silent:true}]);
				}
			});

			$("#treeview").treeview('checkAll',{silent:true});
		};

		//树状结构级联选择，采用递归方法。需要传入两个参数，一个当前的node一个是tree的div
		function checkChildren(node,$Tree){
			var str =JSON.stringify(node);
			var pattern = new RegExp('nodes');
			if(pattern.test(str)){
				$.each(node.nodes,function(key,val){
					$Tree.treeview('checkNode',[val.nodeId]);
					checkChildren(val,$Tree);
				});
			}
		};

		//级联方式取消树状结构的选中状态
		function unCheckChildren(node,$Tree){
			var str =JSON.stringify(node);
			var pattern = new RegExp('nodes');
			if(pattern.test(str)){
				$.each(node.nodes,function(key,val){
					$Tree.treeview('uncheckNode',[val.nodeId]);
					unCheckChildren(val,$Tree);
				});
			};
		}
        //根据数据显示表格内容
		function showTable(data){
            var columns = [
                {field:'name',title:'回路名称'},
                {field:'work',title:'工作日'},
                {field:'noWork',title:'非工作日'}
            ];
            var rows =[];

            data.data.forEach(element => {
                var row = {};
                row.id = element.id;
                row.name = element.name;
                row.work = element.work;
                row.noWork = element.noWork;
                rows.push(row);
            });
            $("#dayReport").html('<table id="mainTable"></table>');
			var windowWidth = $(window).width();
			var totalHeight;
			if(windowWidth>1024)
				totalHeight = $("#main-content").height()-50;
			else if(windowWidth>500)
				totalHeight = $(".report-modify").height()-50;
			else if(windowWidth<=500)
				totalHeight = $(".report-modify").height()-127;
			
			$("#dayReport").height(totalHeight);
			$("#dayReport>table").attr('data-height',totalHeight);

			EMS.DOM.showTable($("#dayReport>table"),columns,rows,{striped:true,classes:'table table-border'});
			$("#dayReport table th").css({
				'background-color':color,
				'color':'white'});

			$("thead>tr>th").eq(0).css('minWidth','250px');
        }

	};

	return _noWorkDay;
})();











jQuery(document).ready(function($) {

    $("body").addClass('page-header-fixed page-sidebar-fixed page-footer-fixed');
	$('.header').addClass('navbar-fixed-top');

    $("#flenergy").attr("class","start active");
    $("#cir_work").attr("class", "active");

    var noWorkDay = new NoWorkDay();

	noWorkDay.initDom();
	noWorkDay.show("/api/NoWorkDay","");
});