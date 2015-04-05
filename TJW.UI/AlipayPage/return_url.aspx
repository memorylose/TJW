<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="return_url.aspx.cs" Inherits="TJW.UI.AlipayPage.return_url" %>

<%@ Register Src="/UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>

<%@ Register Src="/UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>天街网-付款成功</title>
    <link rel="stylesheet" href="/Styles/Common.css" />
    <link rel="stylesheet" href="/Styles/Detail.css" />
</head>
<body>
    <form id="form1" runat="server">

        <uc1:SiteTop ID="SiteTop1" runat="server" />
        <div class="detail">
            <div class="detail_left">
                <div class="bank_div">
                    <div class="bank_div_left"><img src="/Images/result_y.png" /></div>

                    <div class="bank_div_right">
                        <div class="bank_div_right_1"><%=Msg %></div>
                        <div class="bank_div_right_2">天街网提醒您：您已获得 <%=s_grade %> 个积分。 <a href="">我的积分</a></div>
                        <div class="bank_div_right_2">订单编号：<%=s_number %></div>
                        <div class="bank_div_right_2"> <a href="/Index.aspx">继续购买</a></div>
                    </div>


                </div>
            </div>
            <div class="detail_right">
                <div class="detail_right_top">最新商品推荐</div>
                <%=GetSamePicture() %>
            </div>
            <div class="clear"></div>
        </div>
        <uc2:SiteBottom ID="SiteBottom1" runat="server" />
    </form>
</body>
</html>
