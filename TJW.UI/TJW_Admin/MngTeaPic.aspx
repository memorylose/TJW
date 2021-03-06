﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngTeaPic.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngTeaPic" %>

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

        <%=Msg %>
        <div style="height: 20px;"></div>

        <div class="addBtnDiv">
            <input type="button" class="commonBtn" value="添加图片" onclick="DBtn('.addWBtn')" />
        </div>

        <div class="addWBtn" style="display: none;">
            <div class="aau_div">
                <div class="aau_div_left" style="width: 92px;">图片：</div>
                <div class="MiddleRight">
                    <img id="fileImg" src="" runat="server" />
                    <input type="button" onclick="fileHidden.click()" value="选择文件" class="btnUp" />
                    <input type="file" id="fileHidden" runat="server" class="fileUp" onchange="UploadChange(this.value,this.files[0])" />
                    <input type="text" id="txtImgSrc" runat="server" readonly="true" class="inputText" style="display: none;" />
                </div>
                <div class="clear"></div>
            </div>
            <div class="aau_div">
                <div class="aau_div_left">图片类型：</div>
                <div class="aau_div_right">
                    <asp:DropDownList ID="dpPicType" runat="server" CssClass="typeDp"></asp:DropDownList>
                </div>
                <div class="clear"></div>
            </div>
            <div class="aau_div">
                <div class="aau_div_left"></div>
                <div class="aau_div_right">
                    <asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="commonBtn" OnClick="btnSubmit_Click" />
                </div>
                <div class="clear"></div>
            </div>
        </div>

        <table class="adminTable">
            <tr>
                <th>图片</th>
                <th>图片位置</th>
                <th>操作</th>
            </tr>
            <asp:Repeater ID="rpTeaPicture" runat="server">
                <ItemTemplate>

                    <tr>
                        <td>
                            <img src='../<%#Eval("PicturePath") %>' style="width: 50px; height: 50px;" /></td>
                        <td><%#Eval("TypeName") %></td>
                        <td><a href="MngClothPicture.aspx?editId=<%#Eval("PictureId") %>" class="clothtype_a">修改</a><a class="clothtype_aLine">|</a><a href="MngTeaPic.aspx?delId=<%#Eval("PictureId") %>" onclick="return delcfm('确认要删除吗？')" class="clothtype_a">删除</a></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>


    </form>
</body>
</html>
