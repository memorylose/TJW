<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyInfo.aspx.cs" Inherits="TJW.UI.UM.MyInfo" %>

<%@ Register Src="../UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>
<%@ Register Src="../UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<%@ Register Src="../UC/UMLeft.ascx" TagName="UMLeft" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>个人信息</title>
    <link rel="stylesheet" href="../Styles/Common.css" />
    <link rel="stylesheet" href="../Styles/UM.css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteTop ID="SiteTop1" runat="server" />

        <div class="add_center">
            <uc3:UMLeft ID="UMLeft1" runat="server" />
            <div class="add_c_right">
                <div class="cp_div">
                    <div class="cp_div_title">用户名：</div>
                    <div class="cp_div_word"><%=UserName %></div>
                </div>
                <div class="cp_div">
                    <div class="cp_div_title">邮箱：</div>
                    <div class="cp_div_word"><%=Mail %></div>
                </div>
                <div class="cp_div">
                    <div class="cp_div_title">创建日期：</div>
                    <div class="cp_div_word"><%=CreateDate %></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
