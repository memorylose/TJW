<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TJW.UI.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Styles/Common.css" />
    <link rel="stylesheet" href="Styles/R_Login.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="regist_top" style="width: 1000px;">
            <div class="regist_top_logo">
                <img src="Images/logo3.png" />
            </div>

        </div>
        <div class="login_main">

            <div class="login_main_left">
                <div class="login_main_left_top">
                    登录
                </div>
                <%=Msg %>
                <div class="login_input_div">
                    <img src="Images/login1.png" />
                    <input type="text" class="login_input_text" id="txtUsername" runat="server" />
                </div>
                <div class="login_input_div">
                    <img src="Images/login2.png" />
                    <input type="password" class="login_input_text" id="txtPassword" runat="server" />
                </div>
                <div class="login_yz_input_div" id="yzDiv" runat="server">
                    <input type="text" class="login_input_yz" id="txtYz" runat="server" />
                    <img id="imgVerify" src="VerifyCode.aspx?" class="login_verifyCode" alt="看不清？点击更换" onclick="this.src=this.src+'?'" />
                </div>
                <div class="login_dl">
                    <asp:Button ID="Button1" runat="server" Text="登录" CssClass="login_btn" OnClick="Button1_Click" />
                    <div class="login_btn_right"><a href="">忘记密码？</a><a href="Register.aspx">新用户注册</a></div>
                </div>
            </div>
            <div id="clear"></div>
        </div>
    </form>
</body>
</html>
