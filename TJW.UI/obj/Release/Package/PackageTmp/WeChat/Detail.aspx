<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="TJW.UI.WeChat.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Styles/WeChat.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="detail_title">
            <%=Titles %>
        </div>
        <div class="detail_time">
            <%=Time %>
        </div>

        <div class="detail_contents">
            <%=Contents %>
        </div>
    </form>
</body>
</html>
