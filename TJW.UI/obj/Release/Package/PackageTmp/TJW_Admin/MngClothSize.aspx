<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngClothSize.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngClothSize" %>

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
                <input type="button" class="commonBtn" value="添加尺寸" onclick="DBtn('.addFBtn')" />
            </div>
            <div class="addFBtn" id="addFId" runat="server">
                <div class="addBtnWord">尺寸名称：</div>
                <input type="text" id="txtSizeName" runat="server" class="input_text" style="float: left;" />
                <asp:Button ID="btnSize" runat="server" CssClass="commonBtn" Text="提交" Style="float: left; margin-left: 10px; margin-top: 2px;" OnClick="btnSize_Click" />
            </div>
            <table class="adminTable">
                <tr>
                    <th>名称</th>
                    <th>操作</th>
                </tr>
                <asp:Repeater ID="rpSize" runat="server">
                    <ItemTemplate>

                        <tr>
                            <td><%#Eval("SizeName") %></td>
                            <td><a href="MngClothSize.aspx?editId=<%#Eval("SizeId") %>" class="clothtype_a">修改</a><a class="clothtype_aLine">|</a><a href="MngClothSize.aspx?delId=<%#Eval("SizeId") %>" onclick="return delcfm('确认要删除吗？')" class="clothtype_a">删除</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>
</body>
</html>
