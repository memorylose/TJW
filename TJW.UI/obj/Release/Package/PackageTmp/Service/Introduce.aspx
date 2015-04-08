<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Introduce.aspx.cs" Inherits="TJW.UI.Service.Introduce" %>

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
            <div class="add_c_right">
                <div class="gradeHelp_b">天街网简介</div>
                <div class="gradeHelp_b_1">
                    <span></span>天街网——大连格度科技有限公司制造！天街网是以经营单品女装、配饰、化妆品等女人用品为主的电子商务平台。<br />
                    <span></span>
                    网站与多位资深服装设计师团队合作，独家经营设计师样装。独特、完美是天街网女装的标签，仅此一件是天街网的“小缺点”，设计师的样装款式新颖独特，做工精致，面料考究，是品位高雅女人的消费首选。<br />
                    <span></span>
                    如何穿着打扮才得体大方是很多女性追求的目标。“奢华不是时尚、品位缔造经典”，天街网的时尚搭配师为每位消费者提供顾问式服务，可根据不同的肤色、气质、职业等个人自身条件，提供合理的着装搭配建议，让走进天街网的女性更加靓丽自信，让时尚不再是高消费群体的专属。
                </div>
                <div class="gradeHelp_b">主要栏目</div>
                <div class="gradeHelp_b_1">
                    <span></span>时尚街、单品街、配饰街、女人街、时尚资讯
                </div>
                <div class="gradeHelp_b">客户群体</div>
                <div class="gradeHelp_b_1">
                    <span></span>天街网的主要客户是女性，有着装、搭配顾问服务需求的时尚女性，年龄25岁到60岁之间的时尚消费群体。
                </div>

                <div class="gradeHelp_b">天街网的主营业务</div>
                <div class="gradeHelp_b_1">
                    一、	经营时尚女装、化妆品及女性系列用品的电子商务平台<br />
                    <span></span>
                    网购的诞生改变了人们的消费方式，淘宝网、当当网、京东商城，异军突起的电子商务彻底颠覆了商场时代。近年来，大大小小的电子商务平台如雨后春笋般突起。大而全是电子商务的共同特点，至今为止还没有一个以女性商品为主的电商出现。天街网打破了“大而全”的电商概念，专门经营女性商品的电子商务平台，为女性提供打造时尚形象的顾问式服务，让更放心的商品、更贴心的服务无处不在。
                    <br />
                    <span></span>
                    二、	计算机软件的开发及相关的技术咨询服务<br />
                    <span></span>
                    在开发和完善天街网网站建设的同时，技术团队还承接计算机软件开发及相关的技术咨询服务，为有需求的企业提供专业的技术支持。
                    <br />
                    <span></span>
                    三、	网上贸易代理<br />
                    <span></span>
                    天街网是以经营女装、女性商品为主的电子商务平台。随着网购商品的多元化兴起，天街网为了丰富网站的经营产品，同时，承接网上贸易代理的经营项目。

                </div>
                <div class="gradeHelp_b">网络营销战略</div>

                <div class="gradeHelp_b_1">
                    <span></span>立足大连
                    充分利用大连特有的时尚消费环境，培育天街网的消费群体<br />
                    <span></span>
                    面向全国
电子商务的消费群体没有地域之分，用东北市场逐渐覆盖全国市场<br />
                    <span></span>
                    时尚替代奢华
奢华不等于时尚，时尚是一个人对美的理解，天街网的时尚顾问在未来的几年间会帮助更多的女性达到最佳的时尚品位。
                </div>

                <div class="gradeHelp_b">企业文化</div>
                <div class="gradeHelp_b_1">
                    <span></span>经营理念：真心、诚心、用心、贴心<br />
                    <span></span>
                    人才理念：品德优、能力强、业务精

                </div>
            </div>
            <div class="clear"></div>
        </div>
        <uc2:SiteBottom ID="SiteBottom1" runat="server" />
    </form>
</body>
</html>
