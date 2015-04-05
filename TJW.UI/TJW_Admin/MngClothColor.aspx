<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngClothColor.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngClothColor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Admin.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="addBtnDiv">
                <input type="button" class="commonBtn" value="添加颜色" onclick="DBtn('.addFBtn')" />
            </div>


            <div class="addFBtn" id="addFId" runat="server">
                <div class="addBtnWord">颜色名称：</div>
                <input type="text" id="txtColorName" runat="server" class="input_text" style="float: left;" />
                <asp:Button ID="btnColor" runat="server" CssClass="commonBtn" Text="提交" Style="float: left; margin-left: 10px; margin-top: 2px;" OnClick="btnColor_Click" />
            </div>


            <table class="adminTable">
                <tr>
                    <th>名称</th>
                    <th>操作</th>
                </tr>
                <asp:Repeater ID="rpColor" runat="server">
                    <ItemTemplate>

                        <tr>
                            <td><%#Eval("ColorName") %></td>
                            <td><a href="MngClothColor.aspx?editId=<%#Eval("ColorId") %>" class="clothtype_a">修改</a><a class="clothtype_aLine">|</a><a href="MngClothColor.aspx?delId=<%#Eval("ColorId") %>" onclick="return delcfm('确认要删除吗？')" class="clothtype_a">删除</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

        </div>
    </form>
</body>
</html>
