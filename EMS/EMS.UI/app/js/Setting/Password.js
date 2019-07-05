

jQuery(document).ready(function($) {

   $("#Password").click(function(){
        $("#Password").attr('data-target','#myModal2');
   })

   $("#okpassword").blur(function(){
        var password = $("#password").val();
        var okpassword = $("#okpassword").val();
        if(password != okpassword){
            $("#edtPassword").attr('disabled',true);
            alert('新密码和确认密码不一致！！')
        }else{
            $("#edtPassword").attr('disabled',false);
        }
   })
   //修改密码
   $("#edtPassword").click(function(e){
        var url = "/api/UpdateUserPassword";
        var oldPassword = $("#oldPassword").val();
        var password = $("#password").val();
        var mdold = $.md5(oldPassword);
        var newps = $.md5(password);
        var okpassword = $("#okpassword").val();
        // if(password != okpassword){
        //     $("#edtPassword").attr('disabled',true);
        //     alert('新密码和确认密码不一致！！')
        //     return;
        // }
        $.ajax({
            type: "post",
            url: url,
            data: {
                oldPassword:mdold,
                password:newps
            },
            success: function (res) {
                if(res.resultState.state == '0'){
                    alert('密码修改成功!');
                    $("#myModal2").modal('hide')
                }
            }
        });
   });
});