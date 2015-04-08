<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngTeaPicType.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngTeaPicType" %>

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
        <div class="aau_div">
            <div class="aau_div_left">类别名称：</div>
            <div class="aau_div_right">
                <input type="text" id="txtTeaTypePicName" class="input_text" runat="server" />
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left"></div>
            <div class="aau_div_right">
                <asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="input_btn" OnClick="btnSubmit_Click" />
            </div>
            <div class="clear"></div>
        </div>




        <table class="adminTable">
            <tr>
                <th>类别名称</th>
                <th>操作</th>
            </tr>
            <asp:Repeater ID="rpTeaPictureType" runat="server">
                <ItemTemplate>

                    <tr>
                        <td><%#Eval("TypeName") %></td>
                        <td><a href="MngTeaPicType.aspx?editId=<%#Eval("TeaPictureTypeId") %>" class="clothtype_a">修改</a><a class="clothtype_aLine">|</a><a href="MngTeaPicType.aspx?delId=<%#Eval("TeaPictureTypeId") %>" onclick="return delcfm('确认要删除吗？')" class="clothtype_a">删除</a></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
