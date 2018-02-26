$(function () {
    
    $("#tjbtn").click(function () {
        var errorText = $("#error");
        errorText.val("");


        var username = $("#username").val();
        var pwd = $("#password").val();
        var realname = $("#realname").val();
        var email = $("#email").val();
        var phone = $("#phone").val();
        var checkbox = $('input:checkbox[name="checkbox"]:checked').val();

        var val = $('input:radio[name="gender"]:checked').val();
        
        if (username == null || username.length < 6) {
            $.Pro("用户名至少6位字符和数字",{Time: 0.5 });
            return false;
        } else if (pwd == null || pwd.length < 6) {
            $.Pro("密码至少6位", { Time: 0.5, });
            return false;
        } else if (realname == null) {
            $.Pro("请填写真实姓名，做为账户认证凭证。", { Time: 0.5 });
            return false;
        } else if (val == null) {
            $.Pro("请选择性别", { Time: 0.5, });
            return false;
        } else if (!isEmail(email),{ Time: 0.5 }) {
            $.Pro("请输入正确的邮箱格式");
            return false;
        } else if (phone == null || phone.length < 11) {
            $.Pro("请输入正确的手机号", { Time: 0.5 });
            return false;
        } else if (checkbox == null) {
            $.Pro("请阅读并且勾选会员协议", { Time: 0.5 });
            return false;
        }
    });
    function isEmail(str) {
        var myReg = /^[-_A-Za-z0-9]+@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/;
        if (myReg.test(str)) return true;
        return false;
    }
});