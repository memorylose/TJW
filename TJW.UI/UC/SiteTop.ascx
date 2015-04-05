<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteTop.ascx.cs" Inherits="TJW.UI.UC.SiteTop" %>
<div class="top">
    <div class="top_div">
        <div class="top_rl">
            <%=Msg %>
        </div>
        <div class="top_right_div">
            <div class="top_cart">
                <a href="/MyCart.aspx" target="_blank">我的购物车</a>

            </div>
            <div class="top_paper">
                <a href="/UM/MyOrder.aspx" target="_blank">我的订单</a>
            </div>
            <div class="top_paper">
                <a href="/UM/MyGrade.aspx" target="_blank">我的积分</a>
            </div>
            <div class="top_paper">
                <a href="/Service/Introduce.aspx" target="_blank">服务</a>
            </div>
            <div class="top_paper" style="display: none;" id="quitDiv" runat="server">
                <a href="/Logout.aspx">退出</a>
            </div>
        </div>
    </div>
</div>
<div class="banner">
    <div class="bannerDiv">
        <div class="logo">
            <img src="/Images/logo.jpg" />

        </div>
        <div class="banner_search">
            <input type="text" id="txtSearch" class="txtSea" runat="server" />
            <asp:Button ID="Button1" runat="server" CssClass="btnSea" Text="搜索" OnClick="Button1_Click" />
        </div>
    </div>
</div>
<div class="menu">
    <div class="menuDiv">
        <div class="menuWord" style="border-left: 1px solid #DDDDDD">
            <a href="/Index.aspx" class="menuWord_a1" target="_blank">首页</a> <%--当前页menuWord_a1设置为2--%>
        </div>
        <div class="menuWord">
            <a href="/List/2" class="menuWord_a1" target="_blank">时尚街</a>
        </div>
        <div class="menuWord">
            <a href="/List/3" class="menuWord_a1" target="_blank">单品街
            </a>
        </div>
        <div class="menuWord">
            <a href="/List/5" class="menuWord_a1" target="_blank">配饰街
            </a>
        </div>
        <div class="menuWord">
            <a href="/List/17" class="menuWord_a1" target="_blank">女人街</a>
        </div>
        <div class="menuWord">
            <a href="/NewsList" class="menuWord_a1" target="_blank">时尚资讯</a>
        </div>

    </div>
</div>
<div class="menuBg"></div>
