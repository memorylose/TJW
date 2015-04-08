<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyGrade.aspx.cs" Inherits="TJW.UI.UM.MyGrade" %>

<%@ Register Src="../UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>
<%@ Register Src="../UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<%@ Register Src="../UC/UMLeft.ascx" TagName="UMLeft" TagPrefix="uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../Styles/Common.css" />
    <link rel="stylesheet" href="../Styles/UM.css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteTop ID="SiteTop1" runat="server" />

        <div class="add_center">
            <uc3:UMLeft ID="UMLeft1" runat="server" />
            <div class="add_c_right">
                <div class="grade">您总积分为：<%=totalGrade %>分,可用积分为：<%=grade %>分</div>
                <div class="grade">您的级别为：<%=vipName %></div>
                <div class="grade"><a href="/Service/GradeHelp.aspx">查看规则</a></div>
            </div>
        </div>
    </form>
</body>
</html>
