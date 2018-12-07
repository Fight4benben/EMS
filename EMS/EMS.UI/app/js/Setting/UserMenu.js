var UserMenu=(function(){

	function _userMenu(){

		var baseUrl = "/api/UserMenuApi";

		var  fullMenus = [];
		var currentUserMenus = [];

		function initDom(){
			$("#userlist").change(function(){
				var userId = $(this).val();

				getDataFromServer(baseUrl,"userID="+userId);
			});

			$("#searchButton").click(function(event) {
				console.log(currentUserMenus);
				var selectedRows = $("#mainTable").bootstrapTable('getSelections');
				var menus = [];
				$.each(selectedRows, function(index, val) {
					menus.push(val.menuID);
				});
				
				if(currentUserMenus.length === menus.length){
					var  flag = true;//flag==true 表明全部相同，不需要提交，否则提交
					$.each(menus, function(index, val) {
						if($.inArray(val, currentUserMenus) == -1){
							flag=false;
							return false;
						}else{
							return true;
						}
					});

					if(flag){
						alert("菜单未更改，请更改后再提交！")
						return;
					}
				}

				alert("提交更改！");
			});
		}

		this.show = function(){
			initDom();
			getDataFromServer(baseUrl,"");
		}

		function getDataFromServer(url,params){
			EMS.Loading.show();
			$.getJSON(url,params, function(data) {
				console.log(data);
				try{
					showUsers(data);
					appendTotalMenu(data);
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

		function appendTotalMenu(data){

			if(!data.hasOwnProperty('adminMenu'))
				return;

			fullMenus = [];

			$.each(data.adminMenu, function(key, val) {
				fullMenus.push({"menuID":val.menuID,"menuName":val.menuName,"isUsing":false});
			});
		}

		function showTable(data){
			var rows=[];
			currentUserMenus=[];
			if(data.hasOwnProperty('userMenu')){

				if(data.userMenu.length > 0){
					if(data.userMenu[0].toLowerCase()=="all"){
						$.each(fullMenus, function(key, val) {
							rows.push({menuID:val.menuID,menuName:val.menuName,isUsing:true});
							currentUserMenus.push(val.menuID);
						});
					}else{
						$.each(fullMenus, function(key, val) {
							var flag;
							$.inArray(val.menuID, data.userMenu)>-1 ? flag=true:flag=false;
							rows.push({menuID:val.menuID,menuName:val.menuName,isUsing:flag});
							if(flag){
								currentUserMenus.push(val.menuID);
							}
						});
					}
				}

			}else if(data.hasOwnProperty('adminMenu')){
				$.each(data.adminMenu, function(key, val) {
					rows.push({menuID:val.menuID,menuName:val.menuName,isUsing:true});
					currentUserMenus.push(val.menuID);
				});
			}

			var columns=[
				{field:'isUsing',title:'是否选中',checkbox:'true'},
				{field:'menuID',title:'菜单编号'},
				{field:'menuName',title:'菜单名称'}];

			var height = $("#table-user-menus").height();
				$("#table-user-menus").html('<table id="mainTable"></table>');
				$("#table-user-menus>table").attr('data-height',height);

			EMS.DOM.showTable($("#table-user-menus>table"),columns,rows,{striped:true,checkboxHeader:false,classes:'table table-border'});
		}

	}

	return _userMenu;

})();

jQuery(document).ready(function($) {
	
	$("#settings").attr("class", "start active");
    $("#menu_user_menu").attr("class", "active");

    var menu = new UserMenu();
    menu.show();

});