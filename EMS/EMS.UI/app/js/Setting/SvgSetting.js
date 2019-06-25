var test = {
    download:function(svgId){
        console.log(svgId);
        var url = '/api/SvgFile';
        var downloadUrl = url+'?'+'id='+svgId;

        window.location.href=downloadUrl
    }
}
var SvgSetting=(function(){
 

	function _svgSetting(){
        var baseUrl ='/api/SvgSetting';

        var url = '/api/SvgFile';
        var bindUrl = '/api/SvgBinding';
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
                     row.download = '<button class="btn btn-warning download" value="'+ val.svgId +'" onClick="test.download(\''+val.svgId+'\')">下载</button>';
                     row.setting = '<button class="btn btn-warning setting" data-toggle="modal">绑定</button>';
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

                $(".upload").attr('data-target','#myModal4');
                $(".setting").attr('data-target',"#myModal5")
                selectedInfo = row
            });
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
                    if(response.flag == true){
                        alert('新增成功！！')
                        getDataFromServer("/api/SvgSetting","buildId="+buildid);
                        $("#myModal").modal('hide')
                    }
                },
            });
        });
        //修改
        $("#myModal2").on('shown.bs.modal',function (e) { 
            var selectRow = selectedInfo;
            $("#svgid").val(selectRow.svgId)
            $("#svgname").val(selectRow.svgName)
        });
        $("#edtBtn").click(function(){
            var svgid = $("#svgid").val();
            var svgname = $("svgname").val();
            var buildId = $("#buildinglist").val();
            var data = "svgid="+svgid+"&svgname="+svgname
            $.ajax({
                type: "PUT",
                url: baseUrl,
                data: data,
                success: function (response) {
                    if(response.flag == true){
                        alert('修改成功！！')
                        getDataFromServer("/api/SvgSetting","buildId="+buildId);
                        $("#myModal2").modal('hide')
                    }
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
        });
        //上传
        $("#uploadBtn").click(function(event){
            var row = selectedInfo;
            var svgId = selectedInfo.svgId;
            var file = $("#inputFile")[0].files[0];
            if(file == undefined){
                alert("请选择要上传的文件！")
                return;
            }
            var fileName = $("#inputFile")[0].files[0].name;
            var nameArr = fileName.split('.');
            if(nameArr[1]!="svg"){
                alert('文件格式不正确，请选择.svg结尾的文件');
                return;
            }
            var formdata = new FormData();
            formdata.append('svgId',svgId);
            formdata.append('file',file);
            $.ajax({
                type: "post",
                url: url,
                data: formdata,
                contentType:false,
                processData:false,
            })
            .done(function(res){
                if(res.flag == true){
                    alert('文件上传成功！')
                }
            })
            .fail(function(res){
                alert("文件上传失败！")
            })
        });
        //下载
        $('.download').click(function(event){
            //var svgId = $('.download').val()
            var row = selectedInfo;
            var svgId = row.svgId;
            var downloadUrl = url+'?'+'id='+svgId;

            window.location.href=downloadUrl
        });
        //绑定
        $("#myModal5").on('shown.bs.modal',function (e) { 
            var selectRow = selectedInfo;
            var svgId = selectRow.svgId
            var buildId = $("#buildinglist").val();
            $.ajax({
                type: "get",
                url: baseUrl,
                data: {buildId:buildId,svgId:svgId},
                success: function (res) {
                    showBDtable(res)
                }
            });
        });
        function showBDtable(data){

            var columns = [{field:'binded',title:'是否绑定',checkbox:'true'},{field:'name',title:'仪表名称'}];
            var columnparam = [{field:'binded',title:'是否绑定',checkbox:'true'},{field:'name',title:'参数名称'}];
            var tableRows = [];
            var rows = [];
            var selectMeter;
            var selectParams;
            if(data.selectedMeters.length != 0){
                selectMeter = data.selectedMeters;
            }
            if(data.selectedParams.length != 0){
                selectParams = data.selectedParams;
            }

            if(data.meterList!=null && data.meterList.length>0){
                $.each(data.meterList, function (index, val) {
                    var row = {};
                    
                    row.name = val.name;
                    row.id = val.id;
                    for(var i=0;i<selectMeter.length;i++){
                        if(val.id == selectMeter[i]){
                            row.binded = true
                            tableRows.push(row)
                            return
                        }else{
                            row.binded = false
                        }
                    } 
                    tableRows.push(row)
                });
                var height = $("#meter").height();
				$("#meter").html('<table id="meterTable"></table>');
				$("#meter>table").attr('data-height',height);

			    EMS.DOM.showTable($("#meter>table"),columns,tableRows,{striped:true,classes:'table table-border'});
            }
            if(data.paramList!=null && data.paramList.length>0){
                $.each(data.paramList, function (index, val) { 
                    var row = {};
                    row.name = val.name;
                    row.id = val.id;
                    for(var i=0;i<selectParams.length;i++){
                        if(val.id == selectParams[i]){
                            row.binded = true
                            rows.push(row)
                            return
                        }else{
                            row.binded = false
                        }
                    } 
                    rows.push(row)
               });
               var height = $("#params").height();
               $("#params").html('<table id="paramsTable"></table>');
               $("#params>table").attr('data-height',height);

               EMS.DOM.showTable($("#params>table"),columnparam,rows,{striped:true,classes:'table table-border'});
            }
        };
        //修改或者绑定数据到对应的一次图：
        $("#bindBtn").click(function(event){
            var buildId = $("#buildinglist").val();
            var selectRow = selectedInfo;
            var svgId = selectRow.svgId
            var getMeterRows = $("#meterTable").bootstrapTable('getSelections');
            var getParamsRows = $("#paramsTable").bootstrapTable('getSelections');
            var pmeters;
            var pparams;
            var meterid = [];
            var paramid = [];
            getMeterRows.forEach(e => {
                meterid.push(e.id)
            });
            getParamsRows.forEach(e => {
                paramid.push(e.id)
            });
            pmeters = meterid.join('|');
            pparams = paramid.join('|');
            $.ajax({
                type: "POST",
                url: bindUrl,
                data: {svgid:svgId,pmeters:pmeters,pparams:pparams},
                success: function (response) {
                    if(response.flag == true){
                        alert("一次图绑定信息成功！！");
                        getDataFromServer("/api/SvgSetting","buildId="+buildId);
                        $("#myModal5").modal('hide')
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