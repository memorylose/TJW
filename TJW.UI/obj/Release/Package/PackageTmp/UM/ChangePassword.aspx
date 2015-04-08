<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TJW.UI.UM.ChangePassword" %>

<%@ Register Src="../UC/SiteTop.ascx" TagName="SiteTop" TagPrefix="uc1" %>
<%@ Register Src="../UC/SiteBottom.ascx" TagName="SiteBottom" TagPrefix="uc2" %>
<%@ Register Src="../UC/UMLeft.ascx" TagName="UMLeft" TagPrefix="uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="/Styles/Common.css" />
    <link rel="stylesheet" href="/Styles/UM.css" />
    <link rel="stylesheet" href="/Styles/R_Login.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/Regist.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteTop ID="SiteTop1" runat="server" />
        <div class="add_center">
            <uc3:UMLeft ID="UMLeft1" runat="server" />
            <div>
                <%=Msg %>
                <div class="add_c_right">
                    <div class="regist_main_div">
                        <div class="regist_main_div_word">原始密码：</div>
                        <input type="password" class="regist_main_div_tb" id="oldPwd" runat="server" /><div class="regist_msg">请输入原始密码</div>

                    </div>
                    <div class="regist_main_div">
                        <div class="regist_main_div_word">新密码：</div>
                        <input type="password" class="regist_main_div_tb" id="passWord" runat="server" /><div id="pwdregist_msg" class="regist_msg">密码长度为6-16位的英文数字组合</div>
                        <div id="pwdregist_error" class="regist_error"></div>
                        <div id="pwdregist_right" class="regist_right"></div>
                    </div>
                    <div class="regist_main_div">
                        <div class="regist_main_div_word">重复密码：</div>
                        <input type="password" class="regist_main_div_tb" id="rpassWord" runat="server" /><div id="rpregist_msg" class="regist_msg">请再输入一次密码</div>
                        <div id="rpregist_error" class="regist_error"></div>
                        <div id="rpregist_right" class="regist_right"></div>

                    </div>

                    <div class="regist_main_div">
                        <div class="regist_main_div_word">验证码：</div>

                        <input type="text" class="regist_main_div_tb" style="width: 80px; height: 40px;" id="txtTz" runat="server" />
                        <img id="imgVerify" src="/VerifyCode.aspx?" class="regist_verifyCode" alt="看不清？点击更换" onclick="this.src=this.src+'?'" />

                        <div id="yzregist_msg" class="regist_msg" style="margin-top: 7px;">请输入验证码</div>
                        <div id="yzregist_error" class="regist_error" style="margin-top: 7px;"></div>
                        <div id="yzregist_right" class="regist_right" style="margin-top: 7px;"></div>

                    </div>

                    <div class="regist_main_div">
                        <div class="regist_main_div_word"></div>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="regist_main_div_btn" Text="修改密码" OnClientClick="return CheckChangePwd()" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>

        <input type="hidden" id="hidPassword" value="" />
        <input type="hidden" id="hidRpPassword" value="" />
        <input type="hidden" id="hidValidate" value="" />
    </form>
</body>
</html>
