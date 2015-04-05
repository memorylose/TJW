using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI
{
    public partial class Login : System.Web.UI.Page
    {
        public string Msg;
        CommonTools cTools;
        SH_Login _shLogin;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                cTools = new CommonTools();
                _shLogin = new SH_Login();
                if (!_shLogin.CheckIpLimit(cTools.GetPublicIP()))
                {
                    ShowValidateCode();
                }
                //clear validate code cookie
                Response.Cookies.Add(new HttpCookie("CheckCode", ""));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            cTools = new CommonTools();
            _shLogin = new SH_Login();

            //check user name and password empty
            if (string.IsNullOrEmpty(txtUsername.Value) || string.IsNullOrEmpty(txtPassword.Value))
            {
                Msg = MessageTools.ShowLoginMessage("用户名密码不能为空");
            }
            //check three time illegal login
            else if (!_shLogin.CheckIpLimit(cTools.GetPublicIP()))
            {
                if (Request.Cookies["CheckCode"] == null)
                {
                    Msg = MessageTools.ShowLoginMessage("您的浏览器设置已被禁用 Cookies，您必须设置浏览器允许使用 Cookies 选项后才能登录");
                }
                else
                {
                    ShowValidateCode();
                    //Check the first submit(the first submit should only display validate code, not submit)
                    if (string.Compare(txtYz.Value, string.Empty) != 0)
                    {
                        //Check validate code
                        if (String.Compare(Request.Cookies["CheckCode"].Value.ToLower(), txtYz.Value.Trim().ToLower(), true) != 0)
                        {
                            Msg = MessageTools.ShowLoginMessage("验证码错误");
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

        #region Show validate code
        private void ShowValidateCode()
        {
            yzDiv.Attributes.Add("style", "display:block");
        }
        #endregion

        #region Check password
        private void CheckPassword(string userName, string userPwd)
        {
            _shLogin = new SH_Login();
            cTools = new CommonTools();
            if (!_shLogin.CheckPassword(userName, userPwd))
            {
                //add log
                _shLogin.AddLoginLog(userName, cTools.GetPublicIP(), false);
                Msg = MessageTools.ShowLoginMessage("用户名密码错误");
            }
            else
            {
                //add log
                _shLogin.AddLoginLog(userName, "", true);
                //add login session
                Session[ConstValue.LoginSessionName] = userName;

                //add last login time and login times
                _shLogin.AddLoginTime("", _shLogin.GetUserId(userName));
                TransferPage();
            }
        }
        #endregion

        #region Transfer page
        private void TransferPage()
        {
            //check cloth add cart session
            if (Request.QueryString["cart"] != null)
            {
                Response.Redirect("/Detail/" + Request.QueryString["cart"]);
            }
            //check click mycart session
            else if (Request.QueryString["mycart"] != null)
            {
                Response.Redirect("MyCart.aspx");
            }
            //check tea cart
            else if (Request.QueryString["teaCart"] != null)
            {
                Response.Redirect("/Tea/D/" + Request.QueryString["teaCart"]);
            }
            //check grade
            else if (Request.QueryString["cp"] != null)
            {
                Response.Redirect("/UM/ChangePassword.aspx");
            }
            //change password
            else if (Request.QueryString["mygrade"] != null)
            {
                Response.Redirect("/UM/MyGrade.aspx");
            }
            //check user management
            else if (Request.QueryString["um"] != null)
            {
                string redirectUrl = "";
                string umParams = Request.QueryString["um"].ToString();
                switch (umParams)
                {
                    //my address
                    case "1":
                        redirectUrl = "UM/MyAddress.aspx";
                        break;
                    //my order all
                    case "2":
                        redirectUrl = "UM/MyOrder.aspx";
                        break;
                }
                Response.Redirect(redirectUrl);
            }
            else if (Request.QueryString["o"] != null)
            {
                Response.Redirect("Order.aspx?o=" + Request.QueryString["o"]);
            }
            //default
            else
            {
                Response.Redirect("Index.aspx");
            }
        }
        #endregion
    }
}