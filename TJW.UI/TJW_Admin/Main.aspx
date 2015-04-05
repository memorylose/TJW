<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="TJW.UI.TJW_Admin.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>天街网后台管理</title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Admin.js"></script>
    <style type="text/css">
        html {
            overflow-x: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main_top">
            <img src="../AdminImages/mainLogo.png" class="main_logo" />
            <div class="main_username"><%=GetCurrentUser() %> : <%=GetCurrentTime() %><a href="">退出</a></div>
        </div>
        <div class="main_center">
            <div class="main_left">
                <div class="main_l_outdiv">
                    <a onclick="ClickAdminMenu('#clothHideId')">
                        <div class="main_l_div">
                            <div class="main_l_div_word">服饰管理</div>
                            <div class="main_l_div_add">+</div>
                        </div>
                    </a>
                    <div class="forHidding" id="clothHideId">
                        <%=HtmlCloth() %>
                    </div>
                    <a onclick="ClickAdminMenu('#UserHideId')">
                        <div class="main_l_div">
                            <div class="main_l_div_word">用户管理</div>
                            <div class="main_l_div_add">+</div>
                        </div>
                    </a>

                    <div class="forHidding" id="UserHideId">
                        <a href="" class="main_detail">用户管理</a>
                        <a href="AddAdminUser.aspx" target="f1" class="main_detail">添加用户</a>
                        <a href="" class="main_detail">修改密码</a>
                    </div>



                    <a onclick="ClickAdminMenu('#TeaHideId')">
                        <div class="main_l_div">
                            <div class="main_l_div_word">茶品管理</div>
                            <div class="main_l_div_add">+</div>
                        </div>
                    </a>

                    <div class="forHidding" id="TeaHideId">
                        <a href="AddTea.aspx" class="main_detail" target="f1">茶品添加</a>
                        <a href="MngTea.aspx" target="f1" class="main_detail">茶品管理</a>
                        <a href="MngTeaType.aspx" target="f1" class="main_detail">茶品类别管理</a>
                        <a href="MngTeaPicType.aspx" target="f1" class="main_detail">茶品图片类别管理</a>
                    </div>

                    <a onclick="ClickAdminMenu('#WeChatHideId')">
                        <div class="main_l_div">
                            <div class="main_l_div_word">微信管理</div>
                            <div class="main_l_div_add">+</div>
                        </div>
                    </a>

                    <div class="forHidding" id="WeChatHideId">
                        <a href="MngWeChatMessage.aspx" class="main_detail" target="f1">微信消息管理</a>
                        <a href="AddWeChat.aspx" class="main_detail" target="f1">微信消息添加</a>

                        <a href="MngWeChat.aspx" target="f1" class="main_detail">微信群发消息用户</a>
                        <a href="MngWeChatType.aspx" target="f1" class="main_detail">微信类别管理</a>
                    </div>

                    <a onclick="ClickAdminMenu('#NewsHideId')">
                        <div class="main_l_div">
                            <div class="main_l_div_word">资讯管理</div>
                            <div class="main_l_div_add">+</div>
                        </div>
                    </a>

                    <div class="forHidding" id="NewsHideId">
                        <a href="AddNews.aspx" class="main_detail" target="f1">资讯添加</a>
                        <a href="MngNews.aspx" class="main_detail" target="f1">资讯管理</a>
                    </div>


                    <a onclick="ClickAdminMenu('#MemberHideId')">
                        <div class="main_l_div">
                            <div class="main_l_div_word">会员管理</div>
                            <div class="main_l_div_add">+</div>
                        </div>
                    </a>

                    <div class="forHidding" id="MemberHideId">
                        <a href="MngMember.aspx" class="main_detail" target="f1">会员管理</a>
                    </div>

                    <a onclick="ClickAdminMenu('#SoldId')">
                        <div class="main_l_div">
                            <div class="main_l_div_word">发货管理</div>
                            <div class="main_l_div_add">+</div>
                        </div>
                    </a>

                    <div class="forHidding" id="SoldId">
                        <a href="Income.aspx" class="main_detail" target="f1">发货信息</a>
                    </div>
                </div>
            </div>
            <div class="main_right">
                <iframe name="f1" class="frameSty"></iframe>
            </div>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>
