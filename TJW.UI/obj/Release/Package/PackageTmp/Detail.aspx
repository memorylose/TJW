<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="TJW.UI.Detail" %>

<%@ Register Src="UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>

<%@ Register Src="UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=pageTitle %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/Styles/Common.css" />
    <link rel="stylesheet" href="/Styles/Detail.css" />
    <link rel="stylesheet" href="/Styles/jNotify.jquery.css" />
    <link href="/Styles/kefu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.min.js"></script>
    <script type="text/javascript" src="/Scripts/Cart.js"></script>
    <script type="text/javascript" src="/Scripts/service.js"></script>
    <script type="text/javascript">
        function test() {
            alert($('#dpCustom').val());
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">

        <uc1:SiteTop ID="SiteTop1" runat="server" />
        <div class="detail">
            <div class="detail_left">
                <div class="detail_left_top">
                    <div class="detail_left_top_left">
                        <%=topPicture %>
                    </div>
                    <div class="detail_left_top_right">
                        <div class="detail_left_top_right_title"><%=clothName %></div>

                        <div class="detail_left_top_right_money_zk" id="zkDiv" runat="server">
                            <div class="detail_left_top_right_money_left">原价:</div>
                            <div class="detail_left_top_right_money_right_yj">￥<%=clothZkPrice %></div>
                             <div class="detail_left_top_right_money_right_zk"><%=ZkNum %></div>
                            <div class="clear"></div>
                        </div>

                        <div class="detail_left_top_right_money">
                            <div class="detail_left_top_right_money_left">现价:</div>
                            <div class="detail_left_top_right_money_right">￥<%=clothPrice %></div>
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


                        <div class="detail_left_top_right_ps_bh" id="customDiv" runat="server">
                            <div class="detail_left_top_right_ps_left_1">商品编号: </div>
                            <div class="detail_left_top_right_ps_right_custom">
                                <asp:DropDownList ID="dpCustom" runat="server" CssClass="addCloth_dp"></asp:DropDownList>
                            </div>
                            <div class="clear"></div>
                        </div>


                        <div class="detail_left_top_right_ps" id="colorDiv" runat="server">
                            <div class="detail_left_top_right_ps_left_1">颜色: </div>
                            <div class="detail_left_top_right_ps_right" id="colorList">
                                <%=sbColorName %>
                            </div>
                            <div class="clear"></div>
                        </div>

                        <div class="detail_left_top_right_ps" id="sizeDiv" runat="server">
                            <div class="detail_left_top_right_ps_left_1">尺码: </div>
                            <div class="detail_left_top_right_ps_right" id="sizeList">
                                <%=sbSizeName %>
                            </div>
                            <div class="clear"></div>
                        </div>


                        <div class="detail_left_top_right_ps" id="Div1" runat="server">
                            <div class="detail_left_top_right_ps_left_1">库存: </div>
                            <div class="detail_left_top_right_ps_right_count" id="countId">
                                <%=StoreCount %>
                            </div>
                            <div class="clear"></div>
                        </div>

                        <div class="detail_left_top_right_ps">
                            <div class="detail_left_top_right_ps_left_1">数量: </div>
                            <div class="detail_num_div">
                                <div class="detail_num_j" onclick="CartDown()">-</div>
                                <div class="detail_num">
                                    <input type="text" id="carNum" readonly="true" value="1" class="detail_num_input" runat="server" />
                                </div>
                                <div class="detail_num_j" onclick="CartUp()">+</div>
                                <div class="detail_kc"></div>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="detail_left_top_right_line"></div>

                        <div class="detail_left_top_right_ps">
                            <asp:Button ID="btnBuy" runat="server" Text="立即购买" CssClass="detail_gm" OnClick="btnBuy_Click" OnClientClick="return Buy()" />
                            <div class="detail_gwc" id="cartId">加入购物车</div>
                            <div class="clear"></div>
                        </div>

                    </div>

                    <div class="clear"></div>
                </div>
                <div class="detail_content">

                    <%=pictures %>
                </div>
            </div>
            <div class="detail_right">
                <div class="detail_right_top">同类商品推荐</div>
                <%=GetSamePicture() %>
            </div>
            <div class="clear"></div>
        </div>
        <uc2:SiteBottom ID="SiteBottom1" runat="server" />

        <input type="hidden" id="hidColor" runat="server" value="" />
        <input type="hidden" id="hidSize" runat="server" value="" />
        <input type="hidden" id="hidCusFlag" runat="server" value="0" />
    </form>
</body>
</html>
