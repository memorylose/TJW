<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="TJW.UI.List" %>

<%@ Register Src="UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>

<%@ Register Src="UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="Styles/Common.css" />
    <link rel="stylesheet" href="Styles/List.css" />
    <link href="/Styles/kefu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/service.js"></script>
    <title><%=pageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteTop ID="SiteTop1" runat="server" />
        <div class="list">

            <asp:Repeater ID="rpList" runat="server">
                <ItemTemplate>
                    <div class="list_div">
                        <a href="/Detail/<%#Eval("ClothGUID") %>" class="list_div_a" target="_blank">
                            <img src="/<%#Eval("PicturePath") %>" /></a>
                        <div class="list_div_word">
                            <div class="list_div_word_s">
                                <a href="/Detail/<%#Eval("ClothGUID") %>" class="list_div_word_s_a" target="_blank"><%#Eval("ClothName") %></a>
                            </div>
                            <div class="list_div_word_money">
                                <div class="list_div_word_money_left">
                                    <%#Eval("Price") %>
                                </div>
                           <%--     <a href="" class="list_div_word_money_gwc">购买</a>
                                <a href="" class="list_div_word_money_gwc">购物车</a>--%>
                            </div>
                        </div>
                    </div>

                </ItemTemplate>


            </asp:Repeater>

            <div class="clear"></div>
            <div class="cy_center_r_page_div" id="pageId" runat="server">
                <%=PagerHtml() %>
            </div>
        </div>
        <uc2:SiteBottom ID="SiteBottom1" runat="server" />
    </form>
</body>
</html>
