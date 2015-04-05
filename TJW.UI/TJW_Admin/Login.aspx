<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TJW.UI.TJW_Admin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>天街网后台登录</title>
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/AdminLogin.js"></script>
    <link rel="stylesheet" href="../Styles/AdminLogin.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="top">
            <img src="../AdminImages/adminlogo.png" />
        </div>
        <div class="main">
            <div class="main_msg" id="msgId" runat="server"><%=Msg %></div>
            <input type="text" class="main_text" id="txtUsername" runat="server" />
            <input type="password" class="main_text" id="txtPassword" runat="server" />
            <div class="main_yzm" id="yzDiv" runat="server">
                <input type="text" class="main_text_verify" id="txtVerify" runat="server" />
                <img id="imgVerify" src="../VerifyCode.aspx?" alt="看不清？点击更换" onclick="this.src=this.src+'?'" />
                <div class="clear"></div>
            </div>
            <asp:Button ID="Button1" runat="server" CssClass="main_btn" Text="登录" OnClick="Button1_Click" />

        </div>
    </form>
</body>
</html>
