<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngNews.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngNews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新闻管理</title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <link rel="stylesheet" href="../Styles/Pager.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Admin.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="adminTable">
            <tr>
                <th>标题</th>
                <th>位置</th>
                <th>添加时间</th>
                <th>操作</th>
            </tr>
            <asp:Repeater ID="rpNews" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("Title") %></td>
                        <td><%#SetLocation(Eval("IsStr").ToString()) %></td>
                        <td><%#Eval("AddTime") %></td>
                        <td><a href="AddNews.aspx?editId=<%#Eval("NewsId") %>" class="clothtype_a">修改</a><a class="clothtype_aLine">|</a><a href="MngNews.aspx?delId=<%#Eval("NewsId") %>" onclick="return delcfm('确认要删除吗？')" class="clothtype_a">删除</a></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
