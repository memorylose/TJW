<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngClothPicture.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngClothPicture" %>

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
                <input type="button" class="commonBtn" value="添加图片" onclick="DBtn('.addWBtn')" />
            </div>

            <div class="addWBtn" style="display: none;" id="addWBtnId" runat="server">
                <div class="aau_div">
                    <div class="aau_div_left">图片类型：</div>
                    <div class="aau_div_right">
                        <asp:DropDownList ID="dpPicType" runat="server" CssClass="typeDp" AutoPostBack="True" OnTextChanged="dpPicType_TextChanged"></asp:DropDownList><div class="picShowMsg"><%=picShowMsg %></div>
                    </div>
                    <div class="clear"></div>
                </div>

                <div class="aau_div">
                    <div class="aau_div_left" style="width: 80px;">自定义地址：</div>
                    <div class="aau_div_right" style="width: 400px;">
                        <input type="checkbox" id="ckCustomAd" runat="server" style="float: left; margin-top: 11px;" onclick="CustomAddressCheck()" /><div style="float: left; margin-top: 5px; margin-right: 5px; padding-top: 5px;">自定义地址</div>
                        <div id="cusDp" runat="server" style="display: none">
                            <input type="text" id="txtCustomAddress" class="input_text" runat="server" />
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>

                <div class="aau_div">
                    <div class="aau_div_left" style="width: 80px;">自定义文字：</div>
                    <div class="aau_div_right" style="width: 400px;">
                        <input type="checkbox" id="ckCustomWord" runat="server" style="float: left; margin-top: 11px;" onclick="CustomWordCheck()" /><div style="float: left; margin-top: 5px; margin-right: 5px; padding-top: 5px;">自定义文字</div>
                        <div id="cusWord" runat="server" style="display: none">
                            <input type="text" id="txtCustomWord" class="input_text" runat="server" />
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>

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
                    <th>自定义地址</th>
                    <th>自定义文字</th>
                    <th>操作</th>
                </tr>
                <asp:Repeater ID="rpPicture" runat="server">
                    <ItemTemplate>

                        <tr>
                            <td>
                                <img src='../<%#Eval("PicturePath") %>' style="width: 50px; height: 50px;" /></td>
                            <td><%#Eval("pTypeName") %></td>
                            <td><%#Eval("PicHref") %></td>
                            <td><%#Eval("PicWord") %></td>
                            <td><a href="MngClothPicture.aspx?clothGUID=<%#Eval("ClothGUID") %>&editId=<%#Eval("PictureId") %>" class="clothtype_a">修改</a><a class="clothtype_aLine">|</a><a href="MngClothPicture.aspx?clothGUID=<%#Eval("ClothGUID") %>&delId=<%#Eval("PictureId") %>" onclick="return delcfm('确认要删除吗？')" class="clothtype_a">删除</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>

        <input type="hidden" id="hdCustomFlag" runat="server" value="0" />
        <input type="hidden" id="hdCustomWordFlag" runat="server" value="0" />

    </form>
</body>
</html>
