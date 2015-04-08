<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDetail.aspx.cs" Inherits="TJW.UI.NewsDetail" %>

<%@ Register Src="UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>

<%@ Register Src="UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Title %></title>
    <link rel="stylesheet" href="/Styles/Common.css" />
    <link rel="stylesheet" href="/Styles/List.css" />
    <link href="/Styles/kefu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/service.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteTop ID="SiteTop1" runat="server" />

        <div class="newsList">
            <div class="newsList_left">
                <div class="newsDetail_t"><%=Title %></div>
                <div class="newsDetail_time">
                    <div class="newsDetail_time_logo"><a href="/Index.aspx">天街网</a></div>
                    <div class="newsDetail_time_logo"><a><%=Time %></a></div>
                </div>

                <div class="newsContentZY"><span>[摘要]</span><%=SubTitles %></div>

                <div class="newsContent">
                    <%=Contents %>
                </div>
            </div>
            <div class="newsList_right">
                <div class="newsList_right_tj">
                    <div class="newsList_right_tj_t">新闻推荐</div>
                    <%=NewsTj() %>
                </div>
                <div class="newsList_right_tj">
                    <div class="newsList_right_tj_t">热点新闻</div>
                    <%=NewsHot() %>
                </div>
            </div>
            <div class="clear"></div>
        </div>
        <uc2:SiteBottom ID="SiteBottom1" runat="server" />
    </form>
</body>
</html>
