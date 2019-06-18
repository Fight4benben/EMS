var SvgSetting=(function(){

	function _svgSetting(){
        var baseUrl ='/api/SvgSetting';
		this.getSelectedInfo = function(){
            return selectedInfo;
        }

        this.show = function(){
            // var buildId=$.cookie('buildId');
			// if(buildId==undefined || buildId==null || buildId == "null")
			// 	getDataFromServer(baseUrl,"");
			// else
            //     getDataFromServer(baseUrl,"buildId="+buildId);
            getDataFromServer(baseUrl,"");
        }

        function getDataFromServer(url,params) {
            EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				try{
					showBuilds(data);
					showTable(data);
				}catch(e){

				}finally{
					EMS.Loading.hide();
				}
			}).fail(function(e){
				EMS.Tool.statusProcess(e.status);
				EMS.Loading.hide();
			});
        }

        //生成建筑列表，追加到select中
		function showBuilds(data){
			if(!data.hasOwnProperty('builds'))
				return;

			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");
			// if($.cookie('buildId') != undefined && $.cookie('buildId')!=null)
			// 	$("#buildinglist").val($.cookie("buildId"));

			$("#buildinglist").change(function(event) {
				var buildId = $("#buildinglist").val();
				//$.cookie("buildId",buildId,{path:'/'});
				getDataFromServer("/api/SvgSetting","buildId="+buildId);
			});
		};

        function showTable(data){
            var columns=[
				{field:'svgId',title:'一次图编号'},
				{field:'svgName',title:'一次图名'},
                {field:'path',title:'path'},
                {field:'upload',title:'文件上传'},
                {field:'download',title:'文件下载'},
                {field:'setting',title:'绑定设置'},
            ];
            var tableRows = [];
            if(data.svgs!=null && data.svgs.length>0){
                $.each(data.svgs, function (index, val) { 
                     var row = {};
                     row.svgId = val.svgId;
                     row.svgName = val.svgName;
                     row.path = val.path;
                     row.upload = '<button class="btn btn-warning upload" value="'+ val.svgId +'" data-toggle="modal">上传</button>';
                     row.download = '<button class="btn btn-warning download">下载</button>';
                     row.setting = '<button class="btn btn-warning setting">绑定</button>';
                     tableRows.push(row)
                });
                
            }
            var height = $("#selectedSvg").height();
				$("#selectedSvg").html('<table id="mainTable"></table>');
				$("#selectedSvg>table").attr('data-height',height);

			EMS.DOM.showTable($("#selectedSvg>table"),columns,tableRows,{striped:true,classes:'table table-border'});
            
            var $trs = $("table>tbody>tr");
            $("tbody>tr>td").css({
                'padding-top':'2px',
                'padding-bottom':'2px',
                'padding-left':'8px',
                'padding-right':'8px',
            });
            $trs.css('background','bluesky').removeClass('currentSelect');

            $("#mainTable").on('click-row.bs.table',function(e,row,$element){
                $(".currentSelect").css('background','white').removeClass('currentSelect');
                $element.css('background','#cee4f9').addClass('currentSelect')
                selectedInfo = row
                console.log(selectedInfo)
            })
        }

        $("#addModal").click(function(event) {
            $("#addModal").attr('data-target','#myModal');
        });
        $("#edtModal").click(function(event) {
            $("#edtModal").attr('data-target','#myModal2');
        });
        $("#delModal").click(function(event) {
            $("#delModal").attr('data-target','#myModal3');
        });
        $('.upload').click(function(event){
            $(".upload").attr('data-target','#myModal4');
        });
        
        //新增
        $("#addBtn").click(function(e){
            var buildid = $("#buildinglist").val();
            var svgName = $("#svgName").val();
            $.ajax({
                type: "post",
                url: baseUrl,
                data: {
                    buildid:buildid,
                    svgname:svgName
                },
                success: function (response) {
                    console.log(response)
                },
                error:function(){

                }
            });
        });
        //修改
        $("#myModal2").on('shown.bs.modal',function (e) { 
            var selectRow = selectedInfo;
            var svgid = $("#svgid").val(selectRow.svgId)
            var svgname = $("#svgname").val(selectRow.svgName)
        });
        $("#edtBtn").click(function(){
            var svgid = $("#svgid").val();
            var svgname = $("svgname").val();
            //var data = "svgid="+svgid+"&"
            $.ajax({
                type: "PUT",
                url: baseUrl,
                data: ,
                success: function (response) {
                    
                }
            });
        })
        //删除
        $("#delBtn").click(function(event){
            var selectRow = selectedInfo;
            var svgid = selectRow.svgId;
            var buildId = $("#buildinglist").val();
            $.ajax({
                type: "DELETE",
                url: baseUrl,
                data: {
                    svgid:svgid
                },
                success: function (response) {
                    if(response.flag == true){
                        alert('删除一次图成功！！')
                        getDataFromServer("/api/SvgSetting","buildId="+buildId);
                        $("#myModal3").modal('hide')  
                    }
                }
            });
        })
	}
	return _svgSetting;

})();




jQuery(document).ready(function($) {
	
	$("#settings").attr("class", "start active");
    $("#menu_svg_setting").attr("class", "active");

    var svg = new SvgSetting();
    svg.show();
});