<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngClothType.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngClothType" %>

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
                <input type="button" class="commonBtn" value="添加大类" onclick="DBtn('.addFBtn')" />
                <input type="button" class="commonBtn" value="添加小类"  onclick="DBtn('.addCBtn')"/>

            </div>

            <div class="addFBtn" id="addFId" runat="server">
                <div class="addBtnWord">大类名称：</div>
                <input type="text" id="txtFname" runat="server" class="input_text" style="float: left;" />
                <asp:Button ID="btnF" runat="server" CssClass="commonBtn" Text="提交" Style="float: left; margin-left: 10px; margin-top: 2px;" OnClick="btnF_Click" />
            </div>

            <div class="addCBtn" id="addCId" runat="server">
                <div class="addBtnWord">小类名称：</div>
                <asp:DropDownList ID="dpFType" runat="server" CssClass="typeDp"></asp:DropDownList>
                <input type="text" id="txtCname" runat="server" class="input_text" style="float: left;" />
                <asp:Button ID="btnC" runat="server" CssClass="commonBtn" Text="提交" Style="float: left; margin-left: 10px; margin-top: 2px;" OnClick="btnC_Click" />

            </div>

            <table class="adminTable">
                <tr>
                    <th>名称</th>
                    <th>操作</th>
                </tr>
                <asp:Repeater ID="rpFClothType" runat="server">
                    <ItemTemplate>
                     
                        <tr>
                            <td><%#Eval("ClothFatherName") %></td>
                            <td><a href="MngClothType.aspx?editFId=<%#Eval("ClothTypeId") %>" class="clothtype_a">修改</a><a class="clothtype_aLine">|</a><a href="MngClothType.aspx?delFId=<%#Eval("ClothTypeId") %>" onclick="return delcfm('删除大类之后，小类也会随之删除，确认吗？')" class="clothtype_a">删除</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="divSep"></div>
            <table class="adminTable">
                <tr>
                    <th>名称</th>
                    <th>所属</th>
                    <th>操作</th>
                </tr>
                <asp:Repeater ID="rpCClothType" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("ClothFatherName") %></td>
                            <td><%#Eval("TopName") %></td>
                            <td><a href="MngClothType.aspx?editCId=<%#Eval("ClothTypeId") %>" class="clothtype_a">修改</a><a class="clothtype_aLine">|</a><a href="MngClothType.aspx?delCId=<%#Eval("ClothTypeId") %>" onclick="return delcfm('确认要删除吗？')" class="clothtype_a">删除</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>
</body>
</html>
