<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAdminRole.aspx.cs" Inherits="TJW.UI.TJW_Admin.AddAdminRole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>后台用户权限添加</title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <%=Msg %>
        <div class="aau_div">
            <div class="aau_div_left">权限名称：</div>
            <div class="aau_div_right">
                <input type="text" id="txtRoleName" class="input_text" runat="server" />
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
