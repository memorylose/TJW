<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngChildCloth.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngChildCloth" %>

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

                <th>条码</th>
                <th>名称</th>
                <th>类型</th>
                <th>库存</th>
                <th>颜色</th>
                <th>型号</th>
                <th>进价</th>
                <th>卖价</th>
                <th>折扣</th>
                <th>状态</th>
                <th>位置</th>
                <th>操作</th>
            </tr>
            <asp:Repeater ID="rpCloth" runat="server">
                <ItemTemplate>
                    <tr onmousemove="this.style.backgroundColor='#E2F0FF'" onmouseout="this.style.backgroundColor=''">
                        <td><%#Eval("StuffUGUID") %></td>
                        <td><%#Eval("ClothName") %></td>
                        <td><%#Eval("ClothTName") %></td>
                        <td><%#CheckStoreCount(Convert.ToInt32(Eval("StoreCount"))) %></td>
                        <td><%#Eval("ColorName") %></td>
                        <td><%#Eval("SizeName") %></td>
                        <td><%#Eval("OriginalPrice") %></td>
                        <td><%#Eval("Price") %></td>
                        <td><%#SetZheKou(Eval("ZheKou").ToString()) %></td>
                        <td><%#RtnStatus(Convert.ToBoolean(Eval("IsVaild"))) %></td>
                        <td><%#Eval("ShowName") %></td>
                        <td><a href="AddSameCloth.aspx?clothId=<%#Eval("ClothId") %>" class="clothtype_a">添加服饰</a><a class="clothtype_aLine">|</a><a href="AddCloth.aspx?editId=<%#Eval("ClothId") %>" class="clothtype_a">修改</a><a class="clothtype_aLine">|</a><a href="MngChildCloth.aspx?cCloth=<%#Eval("ClothGuid") %>&delId=<%#Eval("ClothId") %>" onclick="return delcfm('确认删除吗？')" class="clothtype_a">删除</a></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
