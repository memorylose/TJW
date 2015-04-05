$(function () {
    LoginUserNameFocusBlur();
    LoginPwdFocusBlur();
});

//Login username and password focus and blur
function LoginUserNameFocusBlur() {
    $('#txtUsername').focus(function () {
        $('#txtUsername').css('border', '2px solid #63AEE8');
    });
    $('#txtUsername').blur(function () {
        $('#txtUsername').css('border', '1px solid #DDDDDD');
    });
}
function LoginPwdFocusBlur() {
    $('#txtPassword').focus(function () {
        $('#txtPassword').css('border', '2px solid #63AEE8');
    });
    $('#txtPassword').blur(function () {
        $('#txtPassword').css('border', '1px solid #DDDDDD');
    });
}

