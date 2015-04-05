<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="TJW.UI.Order" %>

<%@ Register Src="UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>

<%@ Register Src="UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单：<%=orderNumber %></title>
    <link rel="stylesheet" href="Styles/Common.css" />
    <link rel="stylesheet" href="Styles/Order.css" />
    <link href="/Styles/kefu.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/service.js"></script>
    <script type="text/javascript" src="/Scripts/lib.js"></script>
    <script type="text/javascript" src="/Scripts/layer.min.js"></script>

    <script type="text/javascript">
        function CheckAddress() {
            var result = false;
            var traget = document.getElementById('addressDid');
            if (traget.style.display != "none") {
                result = true;
            }
            else {
                alert('请填写地址信息');
            }
            return result;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteTop ID="SiteTop1" runat="server" />
        <div class="order">
            <div class="order_one_top">
                订单详情
            </div>
            <div class="order_one">
                <div class="order_one_title">订单编号:</div>
                <div class="order_one_content"><%=orderNumber %></div>
                <div class="clear"></div>
            </div>
            <div class="order_one_top">
                收货信息 <a href="/UM/MyAddress.aspx">修改</a>
            </div>
            <div id="addressDid" runat="server">
                <div class="order_one">
                    <div class="order_one_title">收获人:</div>
                    <div class="order_one_content"><%=addressPeople %></div>
                    <div class="clear"></div>
                </div>

                <div class="order_one">
                    <div class="order_one_title">收获地址:</div>
                    <div class="order_one_content"><%=address %></div>
                    <div class="clear"></div>
                </div>
                <div class="order_one">
                    <div class="order_one_title">收获邮编:</div>
                    <div class="order_one_content"><%=postCode %></div>
                    <div class="clear"></div>
                </div>

                <div class="order_one">
                    <div class="order_one_title">联系电话:</div>
                    <div class="order_one_content"><%=tel %></div>
                    <div class="clear"></div>
                </div>
            </div>
            <div class="order_one_top">
                付款方式
            </div>
            <div class="order_one">
                <div class="order_one_title">付款方式:</div>
                <div class="order_one_content"><%=payType %></div>
                <div class="clear"></div>
            </div>
            <div class="order_two_top">商品清单</div>
            <%=strStuff %>
            <div class="cart_result">
                <div class="order_vip" id="zkId" runat="server">注：您是天街网<%=vipName %>，订单已为您打 <span style="font-weight: bold; color: red;"><%=bankS %></span> 折。<a href="">会员规则</a></div>


                <asp:Button ID="Button1" runat="server" CssClass="cart_result_submit" Text="确认并付款" OnClick="Button1_Click" OnClientClick="return CheckAddress()" />
                <div class="cart_result_money">￥<%=strTotal %></div>
              <%--  <div class="cart_result_zj">订单金额</div>--%>
                <div class="cart_result_zj_yf" id="yfDiv" runat="server">运费：10元</div>
            </div>

        </div>
        <uc2:SiteBottom ID="SiteBottom1" runat="server" />
    </form>
</body>
</html>
