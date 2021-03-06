﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngWeChatMessage.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngWeChatMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
                <th>类别</th>
                <th>操作</th>
            </tr>
            <asp:Repeater ID="rpWeChat" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("Title") %></td>
                        <td><%#Eval("TypeName") %></td>
                        <td><a href="AddWeChat.aspx?editId=<%#Eval("Id") %>" class="clothtype_a">修改</a><a class="clothtype_aLine">|</a><a href="MngWeChatMessage.aspx?delId=<%#Eval("Id") %>" onclick="return delcfm('确认要删除吗？')" class="clothtype_a">删除</a></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
