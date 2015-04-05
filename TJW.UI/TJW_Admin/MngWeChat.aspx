<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngWeChat.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngWeChat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="addBtnDiv">
                <asp:Button ID="btnUpdateList" runat="server" Text="更新" CssClass="commonBtn" OnClick="btnUpdateList_Click"/>
            </div>
        </div>
    </form>
</body>
</html>
