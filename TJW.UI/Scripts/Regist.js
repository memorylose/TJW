$(function () {
    CheckRegistUsername();
    CheckRegistPwd();
    CheckRegistRpPwd();
    CheckRegistEmail();
    CheckVerifyCode();
});
//regist username
function CheckRegistUsername() {
    $('#userName').focus(function () {
        $(this).css('border', '1px solid #63AEE8');
        $('#unregist_error').css('display', 'none');
        $('#unregist_msg').css('display', 'block');
        $('#unregist_right').css('display', 'none');
    });

    $('#userName').blur(function () {
        //check empty
        if ($(this).val() == '') {
            $('#unregist_msg').css('display', 'none');
            $('#unregist_error').css('display', 'block');
            $('#unregist_error').text('用户名不能为空');
            $(this).css('border', '1px solid red');
        }
        else {
            //check exist user name
            _CheckRegistUN();
        }
    });
}
function _CheckRegistUN() {
    $.ajax({
        url: '../AjaxHandler/UIAjaxHandler.ashx?registUserName=' + $('#userName').val() + '',
        type: "post",
        data: { Method: 'CheckRUN' },
        success: function (msg) {
            if (msg == '1') {
                $('#unregist_right').css('display', 'block');
                $('#unregist_error').css('display', 'none');
                $('#unregist_msg').css('display', 'none');
                $('#hidUsername').prop('value', '1');
            }
            else if (msg == '-1') {
                $('#unregist_error').css('display', 'block');
                $('#unregist_error').text('用户名不合法');
                $('#unregist_msg').css('display', 'none');
                $('#unregist_right').css('display', 'none');
                $('#userName').css('border', '1px solid red');
            }
            else if (msg == '0') {
                $('#unregist_error').css('display', 'block');
                $('#unregist_error').text('用户名已存在');
                $('#unregist_msg').css('display', 'none');
                $('#unregist_right').css('display', 'none');
                $('#userName').css('border', '1px solid red');
            }
        }
    });
}

//Check regist password
function CheckRegistPwd() {
    $('#passWord').focus(function () {
        $(this).css('border', '1px solid #63AEE8');
        $('#pwdregist_error').css('display', 'none');
        $('#pwdregist_msg').css('display', 'block');
        $('#pwdregist_right').css('display', 'none');
    });

    $('#passWord').blur(function () {
        _CheckPwd();
    });
}
function _CheckPwd() {
    $.ajax({
        url: '../AjaxHandler/UIAjaxHandler.ashx?registPassword=' + $('#passWord').val() + '',
        type: "post",
        data: { Method: 'CheckRPwd' },
        success: function (msg) {
            if (msg == '1') {
                $('#pwdregist_msg').css('display', 'none');
                $('#pwdregist_error').css('display', 'none');
                $(this).css('border', '1px solid #DDDDDD');
                $('#pwdregist_right').css('display', 'block');
                $('#hidPassword').prop('value', '1');
            }
            else if (msg == '0') {
                $('#pwdregist_msg').css('display', 'none');
                $('#pwdregist_error').css('display', 'block');
                $('#pwdregist_error').text('密码不合法');
                $('#passWord').css('border', '1px solid red');
            }
        }
    });
}

//Check regist repeat password
function CheckRegistRpPwd() {
    $('#rpassWord').focus(function () {
        $(this).css('border', '1px solid #63AEE8');
        $('#rpregist_error').css('display', 'none');
        $('#rpregist_msg').css('display', 'block');
        $('#rpregist_right').css('display', 'none');
    });
    $('#rpassWord').blur(function () {
        _CheckRpPwd();
    });
}

function _CheckRpPwd() {
    if ($('#rpassWord').val() == '') {
        $('#rpregist_msg').css('display', 'none');
        $('#rpregist_error').css('display', 'block');
        $('#rpregist_error').text('重复密码不能为空');
        $('#rpassWord').css('border', '1px solid red');
    }
    else if ($('#passWord').val() != $('#rpassWord').val()) {
        $('#rpregist_msg').css('display', 'none');
        $('#rpregist_error').css('display', 'block');
        $('#rpregist_error').text('两次密码不一致');
        $('#rpassWord').css('border', '1px solid red');
    }
    else {
        $('#rpregist_msg').css('display', 'none');
        $('#rpregist_error').css('display', 'none');
        $(this).css('border', '1px solid #DDDDDD');
        $('#rpregist_right').css('display', 'block');
        $('#hidRpPassword').prop('value', '1');
    }
}
//Check regist email
function CheckRegistEmail() {
    $('#eMail').focus(function () {
        $(this).css('border', '1px solid #63AEE8');
        $('#emregist_error').css('display', 'none');
        $('#emregist_msg').css('display', 'block');
        $('#emregist_right').css('display', 'none');
    });

    $('#eMail').blur(function () {
        _CheckRegistEmail();
    });
}
function _CheckRegistEmail() {
    $.ajax({
        url: '../AjaxHandler/UIAjaxHandler.ashx?registEmail=' + $('#eMail').val() + '',
        type: "post",
        data: { Method: 'CheckREmail' },
        success: function (msg) {
            if (msg == '1') {
                $('#emregist_right').css('display', 'block');
                $('#emregist_error').css('display', 'none');
                $('#emregist_msg').css('display', 'none');
                $('#hidEmail').prop('value', '1');
            }
            else if (msg == '0') {
                $('#emregist_error').css('display', 'block');
                $('#emregist_error').text('邮箱不合法');
                $('#emregist_msg').css('display', 'none');
                $('#emregist_right').css('display', 'none');
                $('#eMail').css('border', '1px solid red');
            }
        }
    });
}
//regist submit button check
function CheckRegistButton() {
    if ($('#hidUsername').val() == '1' && $('#hidPassword').val() == '1' && $('#hidRpPassword').val() == '1' && $('#hidEmail').val() == '1' && $('#hidValidate').val() == '1') {
        return true;
    }
    else {
        $('#imgVerify').click();
        return false;
    }
}
//verify code
function CheckVerifyCode() {
    $('#txtTz').focus(function () {
        $('#yzregist_error').css('display', 'none');
        $('#yzregist_msg').css('display', 'block');
        $('#yzregist_right').css('display', 'none');
        $('#txtTz').css('border', '1px solid #63AEE8');
    });

    $('#txtTz').blur(function () {
        if ($('#txtTz').val() == '') {
            $('#yzregist_msg').css('display', 'none');
            $('#yzregist_error').css('display', 'block');
            $('#yzregist_error').text('验证码不能为空');
            $('#txtTz').css('border', '1px solid red');
        }
        else if ($('#txtTz').val().toLocaleLowerCase() != getCookie('CheckCode').toLocaleLowerCase()) {
            $('#yzregist_msg').css('display', 'none');
            $('#yzregist_error').css('display', 'block');
            $('#yzregist_error').text('验证码错误');
            $('#txtTz').css('border', '1px solid red');
        }
        else {
            $('#yzregist_right').css('display', 'block');
            $('#yzregist_error').css('display', 'none');
            $('#yzregist_msg').css('display', 'none');
            $('#hidValidate').prop('value', '1');
        }
    });
}
//get cookie
function getCookie(objName) {
    var arrStr = document.cookie.split("; ");
    for (var i = 0; i < arrStr.length; i++) {
        var temp = arrStr[i].split("=");
        if (temp[0] == objName) return unescape(temp[1]);
    }
}

//change password sumit validation
function CheckChangePwd() {
    if ($('#hidPassword').val() == '1' && $('#hidRpPassword').val() == '1' && $('#hidValidate').val() == '1') {
        return true;
    }
    else {
        $('#imgVerify').click();
        return false;
    }
}