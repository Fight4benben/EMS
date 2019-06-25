var Topology = (function(){
	function _topology(){
        
        this.showSvg = function(){
			var url = "/api/svg";
            getDataFromServer(url,"");
        };
        function getDataFromServer(url,params){
			EMS.Loading.show($("#main-content").parent('div'));
			$.getJSON(url, params, function(data) {
				//console.log(data);
				if(data.hasOwnProperty('message'))
					location = "/Account/Login";
				try{
                    showBuildList(data);
                    showSvgList(data);
                    showSvgView(data.svgView);
                    showDataOnSvg(data.data);
				}catch(e){

				}finally{
					EMS.Loading.hide($("#main-content").parent('div'));
				}

				
			}).fail(function(e){
				EMS.Tool.statusProcess(e.status);
            });
        };

        //默认全部闭合
        function initStatus() {
            try{
                $('g[name="off"]').hide();
            } 
            catch(e){
                console.log(e);
            }
        }
        //显示建筑列表
		function showBuildList(data){

			if(!data.hasOwnProperty('builds'))
				return;
			EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");

			$("#buildinglist").change(function(event) {
                $("#svglist").html("");
                $("#svg-content").html("");
                var url = "/api/svg";
                getDataFromServer(url,"buildId="+$("#buildinglist").val())
			});
        };
        //更改建筑信息，刷新svgList
        function showSvgList(data){
            if(!data.hasOwnProperty('svgs'))
                return;
            EMS.DOM.initSelect(data.svgs,$("#svglist"),"svgName","svgID");
        };
        //显示一次图
        function showSvgView(data){
            if(data !== undefined){
                $("#svg-content").html("").css('background-color','black');
                $("#svg-content").html(data);
                initStatus();
            }
        };
        //加载信息到svg
        function showDataOnSvg(data){
            if(data == null)
                return;
            for(i=0;i < data.meterValueList.length; i++){
                var meterID = $("#" + data.meterValueList[i].meterID);
                for(j=0;j<data.meterValueList[i].paramList.length;j++){
                    var paramCode = data.meterValueList[i].paramList[j].code;
                    var paramCodeValue = data.meterValueList[i].paramList[j].value;
                    if(paramCode === "Ia" || paramCode === "Ib" || paramCode === "Ic"){
                        paramCodeValue = paramCodeValue +  "A";
                    }else if(paramCode === "Ua" || paramCode === "Ub" || paramCode === "Uc"){
                        paramCodeValue = paramCodeValue +  "V";
                    }
                    try
                    {
                        meterID.children("text[name='" + paramCode + "']").text(paramCodeValue)
                    }catch(e){
                        console.log(e);
                    }
                }
            }
        };
        //一次图名change事件
        $("#svglist").change(function (){

            getDataFromServer()
        });
        //定时器
        setInterval(refreshSvg,180000);
        function refreshSvg(){
            var buildname = $("#buildinglist").val();
            var picName = $("#svglist").val();
            var url = "/api/svg";
            var param = "buildId="+buildname+"&svgId="+picName+"&dataType="+1;
            $.getJSON(url,param,function(data){
                showDataOnSvg(data);
            })
        }
	};
	return _topology;
})();


jQuery(document).ready(function ($) {

    $("#svgview").attr("class", "start active");
    $("#svgview").attr("class", "active");

    var topology = new Topology();

    topology.showSvg();
});
