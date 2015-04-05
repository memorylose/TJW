<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAdminUser.aspx.cs" Inherits="TJW.UI.TJW_Admin.AddAdminUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>后台用户添加</title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Admin.js"></script>
 
</head>
<body>
    <form id="form1" runat="server">
        <%=Msg %>
        <div class="aau_div">
            <div class="aau_div_left">用户名：</div>
            <div class="aau_div_right">
                <input type="text" id="txtUsername" class="input_text" runat="server" />
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">密码：</div>
            <div class="aau_div_right">
                <input type="password" id="txtPassword" class="input_text" runat="server" />
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">权限：</div>
            <div class="aau_div_right">
                <asp:DropDownList ID="dpRole" runat="server" CssClass="dpCls"></asp:DropDownList>
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
