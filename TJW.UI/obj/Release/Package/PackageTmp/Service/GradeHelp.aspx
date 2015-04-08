<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradeHelp.aspx.cs" Inherits="TJW.UI.Service.GradeHelp" %>

<%@ Register Src="../UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>
<%@ Register Src="../UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<%@ Register Src="../UC/ServiceLeft.ascx" TagName="ServiceLeft" TagPrefix="uc3" %>
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
            <uc3:ServiceLeft ID="ServiceLeft1" runat="server" />
            <div class="add_c_right" style="height:700px;">
                <div class="gradeHelp_b">天街网积分规则</div>
                <div class="gradeHelp_b_1">1.会员每消费1元钱可兑换积分1分，100分抵1元钱使用。</div>
                <div class="gradeHelp_b_1">2.积分3000分为铜牌会员。</div>
                <div class="gradeHelp_b_1">3.积分8000分升为银牌会员，银牌会员在现有网站打折的价格再打9.5折。</div>
                <div class="gradeHelp_b_1">4.积分20000分升级为金牌会员，金牌会员在现有网站打折的价格再打9折。</div>
                <div class="gradeHelp_b_1"><span style="color: red;">5.2014年12月31日前新注册会员送积分3000分。</span></div>


            </div>
            <div class="clear"></div>
        </div>

        <uc2:SiteBottom ID="SiteBottom1" runat="server" />
    </form>
</body>
</html>
