<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TJW.UI.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Styles/Common.css" />
    <link rel="stylesheet" href="Styles/R_Login.css" />
    <script type="text/javascript" src="Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="Scripts/Regist.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="regist_top">
            <div class="regist_top_logo">
                <img src="Images/logo3.png" />
            </div>
            <div class="regist_top_word">已经有账号了？<a href="Login.aspx">去登录</a></div>
        </div>
        <div class="regist_main">

            <%=Msg %>

            <div class="regist_main_top">新用户注册</div>
            <div class="regist_main_div">
                <div class="regist_main_div_word">用户名：</div>
                <input type="text" class="regist_main_div_tb" id="userName" runat="server" /><div id="unregist_msg" class="regist_msg">用户名为6-16位英文，数字以及下划线</div>
                <div id="unregist_error" class="regist_error"></div>
                <div id="unregist_right" class="regist_right"></div>
            </div>
            <div class="regist_main_div">
                <div class="regist_main_div_word">密码：</div>
                <input type="password" class="regist_main_div_tb" id="passWord" runat="server" /><div id="pwdregist_msg" class="regist_msg">密码长度为6-16位的英文数字组合</div>
                <div id="pwdregist_error" class="regist_error"></div>
                <div id="pwdregist_right" class="regist_right"></div>
            </div>
            <div class="regist_main_div">
                <div class="regist_main_div_word">重复密码：</div>
                <input type="password" class="regist_main_div_tb" id="rpassWord" runat="server" /><div id="rpregist_msg" class="regist_msg">请再输入一次密码</div>
                <div id="rpregist_error" class="regist_error"></div>
                <div id="rpregist_right" class="regist_right"></div>

            </div>
            <div class="regist_main_div">
                <div class="regist_main_div_word">邮箱：</div>
                <input type="text" class="regist_main_div_tb" id="eMail" runat="server" /><div id="emregist_msg" class="regist_msg">请输入邮箱，用于找回密码</div>
                <div id="emregist_error" class="regist_error"></div>
                <div id="emregist_right" class="regist_right"></div>

            </div>

            <div class="regist_main_div">
                <div class="regist_main_div_word">验证码：</div>

                <input type="text" class="regist_main_div_tb" style="width: 80px; height: 40px;" id="txtTz" runat="server" />
                <img id="imgVerify" src="VerifyCode.aspx?" class="regist_verifyCode" alt="看不清？点击更换" onclick="this.src=this.src+'?'" />

                <div id="yzregist_msg" class="regist_msg" style="margin-top: 7px;">请输入验证码</div>
                <div id="yzregist_error" class="regist_error" style="margin-top: 7px;"></div>
                <div id="yzregist_right" class="regist_right" style="margin-top: 7px;"></div>

            </div>

            <div class="regist_main_div">
                <div class="regist_main_div_word"></div>
                <asp:Button ID="btnSubmit" runat="server" CssClass="regist_main_div_btn" Text="立即注册" OnClientClick="return CheckRegistButton()" OnClick="btnSubmit_Click" />
            </div>
        </div>

        <input type="hidden" id="hidUsername" value="" />
        <input type="hidden" id="hidPassword" value="" />
        <input type="hidden" id="hidRpPassword" value="" />
        <input type="hidden" id="hidEmail" value="" />
        <input type="hidden" id="hidValidate" value="" />
    </form>
</body>
</html>
