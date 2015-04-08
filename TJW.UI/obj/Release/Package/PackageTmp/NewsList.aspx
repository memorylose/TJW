<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="TJW.UI.NewsList" %>

<%@ Register Src="UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>

<%@ Register Src="UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>天街网-时尚资讯</title>
    <link rel="stylesheet" href="Styles/Common.css" />
    <link rel="stylesheet" href="Styles/List.css" />
    <link href="/Styles/kefu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/service.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteTop ID="SiteTop1" runat="server" />

        <div class="newsList">
            <div class="newsList_left">
                <asp:Repeater ID="rpNews" runat="server">
                    <ItemTemplate>
                        <div class="newsList_left_content">
                            <div class="newsList_left_content_img">
                                <a href="/NewsDetail/<%#Eval("NewsId") %>" target="_blank">
                                    <img src="/<%#Eval("PicPath") %>" /></a>
                            </div>
                            <div class="newsList_left_content_right">
                                <div class="newsList_left_content_right_t"><a href="/NewsDetail/<%#Eval("NewsId") %>" target="_blank"><%#Eval("Title") %></a></div>
                                <div class="newsList_left_content_right_s"><%#Convert.ToDateTime(Eval("AddTime")).ToString("yyyy-MM-dd HH:dd") %></div>
                                <div class="newsList_left_content_right_zy"><%#Eval("SubTitle") %><a href="NewsDetail/<%#Eval("NewsId") %>" target="_blank">[详细]</a></div>
                            </div>
                        </div>
                    </ItemTemplate>

                </asp:Repeater>

                <%=PagerHtml() %>
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
