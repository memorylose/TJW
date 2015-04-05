<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistWaiting.aspx.cs" Inherits="TJW.UI.RegistWaiting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Styles/Common.css" />
    <link rel="stylesheet" href="Styles/R_Login.css" />
    <script type="text/javascript" src="Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript">
        function delayURL(url) {
            var delay = $('#time').text();
            if (delay > 0) {
                delay--;
                $('#time').text(delay);
            } else {
                window.top.location.href = url;
            }
            setTimeout("delayURL('" + url + "')", 1000);
        }
        $(function () {
            delayURL('Index.aspx');
        });
    </script>
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
            <div class="rw_grade"><%=strGrade %></div>
            <div class="rw_word">注册成功，<span id="time">6</span> 秒之后进入天街网</div>
            <div class="rw_go"><a href="Index.aspx">直接去</a></div>
        </div>
    </form>
</body>
</html>
