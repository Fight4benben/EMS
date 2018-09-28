var UserBuilding = (function(){
	function _userBuilding(){
		var baseUrl = "/api/UserBuildingApi";

		var operateResult = {};

		function initDom(){
			$("#userlist").change(function(){
				var userName = $(this).find("option:selected").text();

				getDataFromServer(baseUrl,"userName="+userName);
			});
		}

		this.show = function(){
			initDom();
			getDataFromServer(baseUrl,"");
		}

		function getDataFromServer(url,params){
			EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				try{
					showUsers(data);
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

		function showUsers(data){
			if(!data.hasOwnProperty('users'))
				return;

			EMS.DOM.initSelect(data.users,$("#userlist"),"userName","userId");
		}

		function showTable(data){
			$("#selectedUserBuilding").html("");
			if(!data.hasOwnProperty('userBuildings'))
				return;

			var columns=[
				{field:'binded',title:'是否绑定',checkbox:'true'},
				{field:'buildID',title:'建筑编号'},
				{field:'buildName',title:'建筑名称'}
				
			];

			var rows = [];

			$.each(data.userBuildings, function(key, val) {
				var row = {};
				row.buildID = val.buildID;
				row.buildName = val.buildName;
				val.binded ==0 ? row.binded = false : row.binded=true;

				rows.push(row);
			});

			var height = $("#selectedUserBuilding").height();
			$("#selectedUserBuilding").html('<table id="mainTable"></table>');
			$("#selectedUserBuilding>table").attr('data-height',height);

			EMS.DOM.showTable($("#selectedUserBuilding>table"),columns,rows,{striped:true,checkboxHeader:false,classes:'table table-border'});

			$("table td").css('font-size','15px');

			$("#mainTable").on('check.bs.table',function(event,row,$element){
				$element.attr('disabled','disabled');
				setTimeout(function(){
					var userName =$("#userlist").find('option:selected').text();
					$.post(baseUrl+"/AddBuild", {userName: userName,buildId:row.buildID}, function(data) {
					//console.log(data);
						if(data != 1){
							console.log("绑定建筑失败,建筑编号为："+row.buildId);
							$("#mainTable").bootstrapTable('uncheck',$element.attr('data-index'));
						}
					});
					$element.removeAttr('disabled');
				},500);
				

			});

			$("#mainTable").on('uncheck.bs.table',function(event,row,$element){
				$element.attr('disabled','disabled');
				setTimeout(function(){
					var userName =$("#userlist").find('option:selected').text();
					$.ajax({
	                            url: baseUrl,
	                            type: 'delete',
	                            contentType: 'application/json',
	                            data: JSON.stringify({
	                                userName: $("#userlist").find("option:selected").text(),
	                                buildId: row.buildID
	                            })
	                        }).done(function (data) {
	                            if (data != 1) {
	                                $("#mainTable").bootstrapTable('check',$element.attr('data-index'));
	                            } 
	                            
	                        })
	                        .fail(function () {
	                            console.log("error");
	                        })
	                        .always(function () {
	                            console.log("complete");
	                        });
	                $element.removeAttr('disabled');
				},500);
				
			});
		}
	};

	return _userBuilding;
})();

jQuery(document).ready(function($) {
	$("#settings").attr("class", "start active");
    $("#menu_user_building").attr("class", "active");

    var userBuilding = new UserBuilding();
    userBuilding.show();
});