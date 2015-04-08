<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TJW.UI.CY.Index" %>

<%@ Register Src="../UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>

<%@ Register Src="../UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="/Styles/CY.css" />
    <link rel="stylesheet" href="/Styles/Common_1100.css" />
    <link rel="stylesheet" href="/Styles/Pager.css" />

</head>
<body>
    <form id="form1" runat="server">


        <uc1:SiteTop ID="SiteTop1" runat="server" />



        <div class="cy_center">
            <div class="cy_center_left">
                <div class="cy_center_left_gg">
                    <div class="cy_center_left_gg_top">
                        公告
                    </div>
                    <div class="cy_center_left_gg_word">
                        本公司主营福建福鼎白茶、红茶，白茶主要有：白毫银针、白牡丹、老白茶。以茶会友，欢迎新朋旧友来本公司品尝购买。
                    </div>
                    <div class="cy_center_left_gg_top">
                        品茶地址
                    </div>
                    <div class="cy_center_left_gg_word">
                        大连市沙河口区富鸿国际大厦A座1202
                    </div>
                    <div class="cy_center_left_gg_top">
                        工作时间
                    </div>
                    <div class="cy_center_left_gg_word">
                        9：00 - 18:00
                    </div>
                    <div class="cy_center_left_gg_top">
                        联系方式
                    </div>
                    <div class="cy_center_left_gg_word">
                        座机：0411-39976363
                    </div>

                </div>




                <div class="cy_center_left_fl">

                    <div class="cy_center_left_fl_top">
                        分类
                    </div>

                    <div class="cy_center_left_fl_ct">
                        <div class="cy_center_left_fl_ct_f">
                            茶叶分类
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            普洱
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            龙井
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            白茶
                        </div>
                    </div>

                    <div class="cy_center_left_fl_ct">
                        <div class="cy_center_left_fl_ct_f">
                            价格分类
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            0-99元
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            100-199元
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            200-299元
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            300-399元
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            400-499元
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            500以上
                        </div>
                    </div>

                    <div class="cy_center_left_fl_ct" style="border: 0;">
                        <div class="cy_center_left_fl_ct_f">
                            年份
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            1年内
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            1-3年
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            3-5年
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            5-10年
                        </div>
                        <div class="cy_center_left_fl_ct_c">
                            10年以上
                        </div>

                    </div>

                </div>
            </div>
            <div class="cy_center_right">
                <asp:Repeater ID="rpTea" runat="server">
                    <ItemTemplate>
                        <div class="cy_center_r_div">
                            <a href="/Tea/D/<%#Eval("StuffUGUID") %>" target="_blank">
                                <img src="../<%#Eval("PicturePath") %>" /></a>
                            <div class="cy_center_r_money">
                                ￥<%#Eval("TeaPrice") %>
                            </div>
                            <div class="cy_center_r_word">
                                <a href="/Tea/D/<%#Eval("StuffUGUID") %>" target="_blank"><%#Eval("TeaName") %></a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="clear"></div>

                <div class="cy_center_r_page_div" id="pageId" runat="server">
                    <%=PagerHtml() %>
                </div>
            </div>

            <div class="clear"></div>
        </div>

        <uc2:SiteBottom ID="SiteBottom1" runat="server" />

    </form>
</body>
</html>
