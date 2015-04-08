<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CYDetail.aspx.cs" Inherits="TJW.UI.CY.CYDetail" %>

<%@ Register Src="../UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>
<%@ Register Src="../UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/Styles/Common.css" />
    <link rel="stylesheet" href="/Styles/Detail.css" />
    <link rel="stylesheet" href="/Styles/jNotify.jquery.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.min.js"></script>
    <script type="text/javascript" src="/Scripts/Cart.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteTop ID="SiteTop1" runat="server" />
        <div class="detail">
            <div class="detail_left">
                <div class="detail_left_top">
                    <div class="detail_left_top_left">
                        <%=TopImage %>
                    </div>
                    <div class="detail_left_top_right">
                        <div class="detail_left_top_right_title"><%=TeaName %></div>
                        <div class="detail_left_top_right_money">
                            <div class="detail_left_top_right_money_left">现价:</div>
                            <div class="detail_left_top_right_money_right">￥<%=TeaPrice %></div>
                            <div class="clear"></div>
                        </div>

                        <div class="detail_left_top_right_ps">
                            <div class="detail_left_top_right_ps_left">配送: </div>
                            <div class="detail_left_top_right_ps_right">大连 至 全国</div>
                            <div class="clear"></div>
                        </div>

                        <div class="detail_left_top_right_ps">
                            <div class="detail_left_top_right_ps_left">快递: </div>
                            <div class="detail_left_top_right_ps_right">免运费</div>
                            <div class="clear"></div>
                        </div>
                        <div class="detail_left_top_right_line"></div>

                        <div class="detail_left_top_right_ps">
                            <div class="detail_left_top_right_ps_left_1">年份: </div>
                            <div class="detail_left_top_right_ps_right" style="padding-top: 3px;" id="colorList">
                                <%=TeaYear %>
                            </div>
                            <div class="clear"></div>
                        </div>

                        <div class="detail_left_top_right_ps">
                            <div class="detail_left_top_right_ps_left_1">类别: </div>
                            <div class="detail_left_top_right_ps_right" style="padding-top: 3px;" id="sizeList">
                                <%=TeaType %>
                            </div>
                            <div class="clear"></div>
                        </div>

                        <div class="detail_left_top_right_ps">
                            <div class="detail_left_top_right_ps_left_1">数量: </div>
                            <div class="detail_num_div">
                                <div class="detail_num_j" onclick="CartDown()">-</div>
                                <div class="detail_num">
                                    <input type="text" id="carNum" readonly="true" value="1" runat="server" class="detail_num_input" />
                                </div>
                                <div class="detail_num_j" onclick="CartUp()">+</div>
                                <div class="detail_kc"></div>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="detail_left_top_right_line"></div>

                        <div class="detail_left_top_right_ps">
                            <asp:Button ID="Button1" runat="server" CssClass="detail_gm" Text="立即购买" OnClick="Button1_Click" />
                            <div class="detail_gwc" id="teaCartId">加入购物车</div>
                            <div class="clear"></div>
                        </div>

                    </div>

                    <div class="clear"></div>
                </div>
                <div class="detail_content">
                    <div class="teaDetailDes"><%=TeaDes %></div>
                    <%=pictures %>
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
