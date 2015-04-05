using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.TJW_Admin
{
    public partial class Login : System.Web.UI.Page
    {
        public string Msg;
        SH_AdminLogin sh_adminLogin;
        SH_AdminUser sh_adminUser;
        CommonTools cTools;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckRtnUrl();
                cTools = new CommonTools();
                sh_adminLogin = new SH_AdminLogin();
                //if (!sh_adminLogin.CheckIpLimit(cTools.GetPublicIP()))
                //{
                //    ShowValidateCode();
                //}
                //clear validate code cookie
                Response.Cookies.Add(new HttpCookie("CheckCode", ""));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            sh_adminLogin = new SH_AdminLogin();
            cTools = new CommonTools();

            //Check username and password empty
            if (string.IsNullOrEmpty(txtUsername.Value) || string.Equals(txtUsername.Value, ConstValue.AdminLoginDefaultUsername) || string.IsNullOrEmpty(txtPassword.Value) || string.Equals(txtPassword.Value, ConstValue.AdminLoginDefaultPassword))
            {
                MessageTools.ShowMessage(msgId, ref Msg, "用户名密码不能为空");
            }

            //Check 3 times user name and ip address
            else if (!sh_adminLogin.CheckSameUserLimit(txtUsername.Value))
            {
                //Check browser cookie
                if (Request.Cookies["CheckCode"] == null)
                {
                    MessageTools.ShowMessage(msgId, ref Msg, "您的浏览器设置已被禁用 Cookies，您必须设置浏览器允许使用 Cookies 选项后才能使用本系统。");
                    ClearInput();
                }
                else
                {
                    ShowValidateCode();

                    //Check the first submit(the first submit should only display validate code, not submit)
                    if (string.Compare(txtVerify.Value, string.Empty) != 0)
                    {
                        //Check validate code
                        if (String.Compare(Request.Cookies["CheckCode"].Value, txtVerify.Value.Trim(), true) != 0)
                        {
                            MessageTools.ShowMessage(msgId, ref Msg, "验证码错误");
                            ClearValidateCode();
                        }
                        else
                        {
                            CheckPassword(txtUsername.Value.Trim(), txtPassword.Value.Trim());
                        }
                    }
                }
            }
            else
            {
                CheckPassword(txtUsername.Value.Trim(), txtPassword.Value.Trim());
            }
        }

        #region Set login cookie
        private void SetLoginCookie(string userName)
        {
            sh_adminUser = new SH_AdminUser();
            //set cookie
            LoginInfo dl = new LoginInfo();
            dl.UserId = sh_adminUser.GetAdminUserId(userName);
            dl.UserName = userName;
            dl.Roles = "UserAdmin";
            dl.LoginTime = DateTime.Now;

            string strLogininfo = SeriaFunc.SerializeFun(dl);
            FormsAuthenticationTicket ft = new FormsAuthenticationTicket(1, "AdminTicket", DateTime.Now, DateTime.Now.AddMinutes(20), false, strLogininfo);
            string strTicket = FormsAuthentication.Encrypt(ft);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strTicket);
            cookie.Expires = ft.Expiration;
            Response.Cookies.Add(cookie);
            Response.Redirect("Main.aspx");
        }
        #endregion

        #region Check password
        private void CheckPassword(string userName, string userPwd)
        {
            sh_adminLogin = new SH_AdminLogin();
            cTools = new CommonTools();
            if (!sh_adminLogin.CheckAdminPassword(userName, userPwd))
            {
                //add log
                sh_adminLogin.AddLoginLog(userName, "", false);
                MessageTools.ShowMessage(msgId, ref Msg, "用户名密码错误");
                ClearInput();
                ClearValidateCode();
            }
            else
            {
                //add log
                sh_adminLogin.AddLoginLog(userName, "", true);
                //add login cookie
                SetLoginCookie(userName);
            }
        }
        #endregion

        #region Clear all textbox
        /// <summary>
        /// Clear all textbox
        /// </summary>
        private void ClearInput()
        {
            txtPassword.Value = ConstValue.AdminLoginDefaultPassword;
        }
        #endregion

        #region Show validate code
        private void ShowValidateCode()
        {
            yzDiv.Attributes.Add("style", "display:block");
        }
        #endregion

        #region Clear validate code
        private void ClearValidateCode()
        {
            txtVerify.Value = "";
        }
        #endregion

        #region Return login page
        private void CheckRtnUrl()
        {
            if (Request.QueryString["ReturnUrl"] != null)
            {
                Response.Redirect("Login.aspx");
                form1.Target = "_blank";
            }
        }
        #endregion
    }
}