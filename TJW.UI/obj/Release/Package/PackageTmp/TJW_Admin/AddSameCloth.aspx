<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSameCloth.aspx.cs" Inherits="TJW.UI.TJW_Admin.AddSameCloth" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="aau_div">
            <div class="aau_div_left">名称：</div>
            <div class="aau_div_right">
                <input type="text" id="txtClothName" class="input_text" runat="server" readonly="true" style="border:0;" />
            </div>
            <div class="clear"></div>
        </div>

        <div class="aau_div">
            <div class="aau_div_left">颜色：</div>
            <div class="aau_div_right">
                <asp:DropDownList ID="dpColor" runat="server" CssClass="addCloth_dp"></asp:DropDownList>
            </div>
            <div class="clear"></div>
        </div>

        <div class="aau_div">
            <div class="aau_div_left">尺寸：</div>
            <div class="aau_div_right">
                <asp:DropDownList ID="dpSize" runat="server" CssClass="addCloth_dp"></asp:DropDownList>
            </div>
            <div class="clear"></div>
        </div>

        <div class="aau_div">
            <div class="aau_div_left">库存：</div>
            <div class="aau_div_right">
                <input type="text" id="txtStore" class="input_text" runat="server" />
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
    </form>
</body>
</html>
