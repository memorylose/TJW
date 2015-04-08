<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyCart.aspx.cs" Inherits="TJW.UI.MyCart" %>

<%@ Register Src="UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>
<%@ Register Src="UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的购物车</title>
    <link rel="stylesheet" href="Styles/Common.css" />
    <link rel="stylesheet" href="Styles/Cart.css" />
    <link href="/Styles/kefu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="Scripts/lib.js"></script>
    <script type="text/javascript" src="Scripts/layer.min.js"></script>
    <script type="text/javascript" src="Scripts/Cart.js"></script>
    <script type="text/javascript" src="/Scripts/service.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteTop ID="SiteTop1" runat="server" />
        <div class="cart">
            <div class="cart_top">
                <div class="cart_top_img"></div>
            </div>
            <div class="cart_title">
                <%--<div class="cart_title_1">--%>
                <%--<input type="checkbox" id="ckAll"  class="cart_title_1_ck" onclick="AllSelected()" />
                    <div class="cart_title_1_word">全选</div>--%>
                <%--     </div>--%>
                <div class="cart_title_2">商品</div>
                <div class="cart_title_3">商品信息</div>
                <div class="cart_title_4">单价</div>
                <div class="cart_title_4">数量</div>

                <div class="cart_title_4">小计</div>
                <div class="cart_title_4">操作</div>
            </div>


            <asp:Repeater ID="rpCart" runat="server">
                <ItemTemplate>

                    <div class="cart_content">
                        <div class="cart_content_1">
                            <%--    <asp:CheckBox ID="ckSelect" runat="server" CssClass="cart_content_1_ck"/>
                            <asp:Label ID="lbChk" runat="server" Text='<%#Eval("CartId") %>' CssClass="cart_label"></asp:Label>--%>
                        </div>
                        <div class="cart_content_2">
                            <a href="<%#SetCartHref(Eval("StuffUGUID").ToString()) %>">
                                <img src="<%#Eval("PicturePath") %>" /></a><div class="cart_content_2_right">
                                    <div class="cart_content_2_right_top"><a href="<%#SetCartHref(Eval("StuffUGUID").ToString()) %>"><%#Eval("Name") %></a></div>
                                </div>
                        </div>
                        <div class="cart_content_3_info">
                            <div class="cart_content_3_a1"><%#SetCustomBH(Eval("ColorType").ToString(),"1") %></div>
                            <div class="cart_content_3_a1"><%#SetCustomBH(Eval("YearSize").ToString(),"0") %></div>
                        </div>
                        <div class="cart_content_3">
                            <div class="cart_content_3_a2"><%#Eval("Price") %></div>
                        </div>
                        <div class="cart_content_3">
                            <div class="cart_content_3_num">
                                <div class="cart_content_3_num_div">
                                    <div class="cart_content_3_num_div_add" id="red_<%#Eval("CartId") %>" onclick="AddCartNum(this.id,0)">-</div>
                                    <input type="text" readonly="true" id="input_<%#Eval("CartId") %>" value='<%#Eval("ClothCount") %>' /><div class="cart_content_3_num_div_add" id="add_<%#Eval("CartId") %>" onclick="AddCartNum(this.id,1)">+</div>
                                </div>
                            </div>
                        </div>
                        <div class="cart_content_4">
                            <div class="cart_content_4_a1"><%#Eval("TotalPrice") %></div>
                        </div>
                        <div class="cart_content_4"><a class="cart_content_4_a2" onclick="return CheckCartDelete('<%#Eval("Name") %>','<%#Eval("CartId") %>')">删除</a></div>
                    </div>
                </ItemTemplate>

            </asp:Repeater>





            <div class="cart_result">

                <%--            <div class="cart_result_ck_del">删除</div>--%>

                <asp:Button ID="Button1" runat="server" Text="提交订单" CssClass="cart_result_submit" OnClick="Button1_Click" OnClientClick="SetHiddenCart()" />
                <div class="cart_result_money">￥<%=AllPrice %></div>
                <div class="cart_result_zj">共有 <span><%=AllCount %></span> 件商品，总计</div>
            </div>
        </div>

        <input type="hidden" value="" runat="server" id="hdCount" />

        <uc2:SiteBottom ID="SiteBottom1" runat="server" />
    </form>
</body>
</html>
