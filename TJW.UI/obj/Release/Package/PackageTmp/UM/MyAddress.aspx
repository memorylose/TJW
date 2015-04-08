<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAddress.aspx.cs" Inherits="TJW.UI.UM.MyAddress" %>

<%@ Register Src="../UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>
<%@ Register Src="../UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<%@ Register Src="../UC/UMLeft.ascx" TagName="UMLeft" TagPrefix="uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../Styles/Common.css" />
    <link rel="stylesheet" href="../Styles/UM.css" />
    <link rel="stylesheet" href="../Styles/jNotify.jquery.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jNotify.jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/Address.js"></script>

    <script type="text/javascript" src="../Scripts/lib.js"></script>
    <script type="text/javascript" src="../Scripts/layer.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteTop ID="SiteTop1" runat="server" />
        <div class="add_center">
            <uc3:UMLeft ID="UMLeft1" runat="server" />
            <div class="add_c_right">
                <div class="add_c_right_top">地址管理</div>
                <%=ErrorMsg %>
                <div class="add_c_right_add" onclick="ShowAddress()">添加新地址</div>
                <div class="add_address_div">
                    <div class="add_address_rows">
                        <div class="add_address_word_title">省:</div>
                        <select class="address_select" id="slProvince" name="nameProvince" onchange="ChangeCity(this.value);"></select>
                        <div class="add_address_word">市:</div>
                        <select class="address_select" id="slCity" name="nameCity" onchange="ChangeDistinct(this.value);"></select>
                        <div class="add_address_word">区:</div>
                        <select class="address_select" id="slDistinct" name="nameDistinct"></select>
                    </div>
                    <div class="add_address_rows_4" id="msgProvinceId" runat="server">请选择省市区</div>
                    <div class="add_address_rows_2">
                        <div class="add_address_word_title">街道地址:</div>
                        <textarea id="txtJD" runat="server" class="address_txtArea"></textarea>
                        <div class="clear"></div>
                    </div>
                    <div class="add_address_rows_4" id="msgDistinctId" runat="server">请填写街道地址，最少5个字，最多不能超过100个字</div>
                    <div class="add_address_rows_3">
                        <div class="add_address_word_title">邮编:</div>
                        <input type="text" runat="server" id="txtCode" class="address_txtInput" />
                        <div class="address_inputTs" id="msgCodeId" runat="server">请填写邮编</div>
                    </div>
                    <div class="add_address_rows_3">
                        <div class="add_address_word_title">联系人姓名:</div>
                        <input type="text" runat="server" id="txtName" class="address_txtInput" maxlength="5" />
                        <div class="address_inputTs">请填写收货人,最多不超过5个字</div>
                    </div>
                    <div class="add_address_rows_3">
                        <div class="add_address_word_title">联系人电话:</div>
                        <input type="text" runat="server" id="txtTel" class="address_txtInput" />
                        <div class="address_inputTs" id="msgTelId" runat="server">请填写收货人电话</div>
                    </div>
                    <div class="add_address_rows_3">
                        <asp:Button ID="txtBtn" runat="server" CssClass="address_btn" Text="提交" OnClientClick="return SubmitBtn()" OnClick="txtBtn_Click" />
                        <input type="button" id="btnCancel" class="address_btnCancel" value="取消" onclick="CancelBtn()" />
                    </div>
                </div>

                <%=AddressShow %>
            </div>
            <div class="clear"></div>
        </div>
        <uc2:SiteBottom ID="SiteBottom1" runat="server" />
    </form>
</body>
</html>
