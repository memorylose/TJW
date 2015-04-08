<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngTea.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngTea" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Admin.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="adminTable">
            <tr>
                <th>茶品名称</th>
                <th>库存</th>
                <th>茶品进价</th>
                <th>茶品卖家</th>
                <th>茶品类别</th>
                <th>茶品年份</th>
                <th>操作</th>
            </tr>
            <asp:Repeater ID="rpTea" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("TeaName") %></td>
                        <td><%#Eval("TeaCount") %></td>
                        <td><%#Eval("TeaOriPrice") %></td>
                        <td><%#Eval("TeaPrice") %></td>
                        <td><%#Eval("TypeName") %></td>
                        <td><%#SetCommonTea(Convert.ToInt32(Eval("TeaYear"))) %></td>
                        <td><a href="MngTeaPic.aspx?stfId=<%#Eval("StuffUGUID") %>" class="clothtype_a">添加图片</a><a href="AddTea.aspx?editId=<%#Eval("TeaId") %>" class="clothtype_a">修改</a><a class="clothtype_aLine">|</a><a href="MngTea.aspx?delId=<%#Eval("StuffUGUID") %>" onclick="return delcfm('确认要删除吗？')" class="clothtype_a">删除</a></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
