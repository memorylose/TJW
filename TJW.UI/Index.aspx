<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TJW.UI.Index" %>

<%@ Register Src="UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>

<%@ Register Src="UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>天街网</title>
    <link rel="stylesheet" href="Styles/Common.css" />
    <link rel="stylesheet" href="Styles/Index.css" />
    <link href="Styles/kefu.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="Scripts/responsiveslides.min.js"></script>
    <script type="text/javascript" src="Scripts/Index.js"></script>

    <script type="text/javascript" src="Scripts/service.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteTop ID="SiteTop1" runat="server" />
        <div class="first">
            <div class="first_left">
                <div class="first_l_top">
                    最新资讯<span>News</span>
                </div>
                <%=StrNews() %>
            </div>
            <div class="first_right">
                <div class="first_r_left">
                    <div class="first_r_left_top">
                        <div class="callbacks_container">
                            <ul class="rslides" id="slider4">
                                <%=StrTurningImage() %>
                            </ul>
                        </div>
                    </div>
                    <div class="first_r_left_bottom">
                        <%=StrBelowTurningImage() %>
                    </div>
                </div>
                <div class="first_r_right">

                    <%=StrAuthorSuggest() %>


                    <div class="first_r_right_bottom">
                        <div class="first_r_right_bottom_top">
                            <img src="AdImages/erweima.jpg" />
                        </div>
                        <div class="first_r_right_bottom_bottom">
                            <div class="first_r_right_bottom_bottom_wf">关注微信</div>
                            <div class="first_r_right_bottom_bottom_wf">获取最新的品牌资讯和折扣信息</div>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
            <div class="clear"></div>
        </div>
        <div class="second">
            <div class="second_main_left">
                <div class="second_top">
                    <div class="second_top_word">
                        时尚街
                    </div>
                    <div class="second_top_word_1">
                        简单是一种时尚、标新立异也是一种时尚
                    </div>
                    <div class="second_top_icon">
                        <a href="/List/2">更多</a>
                    </div>
                </div>
                <div class="second_main_left_first">
                    <div class="second_main_left_pic">

                        <%=StrCurrentSeasonTop() %>
                    </div>
                    <div class="second_main_left_detail">
                        <%=StrCurrentSeason() %>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="second_main_left_second">
                    <div class="second_top">
                        <div class="second_top_word">
                            单品街
                        </div>

                        <div class="second_top_word_1">
                            单品街栏目女装都是设计师独版设计，唯一款式、唯一尺码。款式钟意，尺码合适才能和衣服“结缘”
                        </div>
                        <div class="second_top_icon">
                            <a href="/List/3">更多</a>

                        </div>
                    </div>

                    <%=StrSingleProduct() %>
                    <div class="clear"></div>
                </div>
            </div>
            <div class="second_main_right">
                <div class="second_main_right_top">
                    热门推荐
                </div>
                <div class="second_main_right_pic">
                    <%=StrHotSuggestTop() %>
                </div>
                <%--      <div class="second_main_right_top">
                    <div class="second_main_right_top_word">往期热门推荐</div>
                    <div class="second_main_right_top_icon"></div>
                </div>--%>

                <%=StrHotSuggest() %>
            </div>
            <div class="clear"></div>
        </div>
        <div class="third">
            <div class="second_main_left">
                <div class="second_top">
                    <div class="second_top_word">
                        配饰街
                    </div>
                    <div class="second_top_word_1">
                        穿衣搭配的技巧也是一门高深的学问呢
                    </div>
                    <div class="second_top_icon">
                        <a href="/List/5">更多</a>

                    </div>
                </div>
                <div class="third_left">
                    <div class="third_left_top">



                        <%=StrSaleProductTop() %>
                    </div>
                    <div class="third_left_bottom">

                        <%=StrSaleProduct(0) %>
                        <div class="clear"></div>
                    </div>
                </div>
                <div class="third_right">

                    <%=StrSaleProduct(1) %>
                </div>

                <div class="clear"></div>

                <div class="w_div">
                    <div class="second_top">
                        <div class="second_top_word">
                            女人街
                        </div>
                        <div class="second_top_word_1">
                            五彩斑斓的女人世界
                        </div>
                        <div class="second_top_icon">
                            <a href="/List/17">更多</a>
                        </div>
                    </div>
                    <%=StrWomenStreet() %>
                    <div class="clear"></div>
                </div>
            </div>
            <div class="third_main_right">
                <div class="second_main_right_top">
                    热销商品
                </div>
                <div class="second_main_right_pic">
                    <%=StrHotProductTop() %>
                </div>
                <%--       <div class="second_main_right_top">
                    <div class="second_main_right_top_word">往期热销推荐</div>
                    <div class="second_main_right_top_icon"></div>
                </div>--%>


                <%=StrHotProduct() %>
                <div class="third_main_right_ad2">
                    <img src="AdImages/indexa_d2.jpg" />
                </div>
                <div class="third_main_right_ad">
                    <img src="AdImages/ad1.jpg" />
                </div>

            </div>
            <div class="clear"></div>
        </div>
        <uc2:SiteBottom ID="SiteBottom1" runat="server" />
    </form>
</body>
</html>
