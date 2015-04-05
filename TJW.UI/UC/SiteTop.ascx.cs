using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.UC
{
    public partial class SiteTop : System.Web.UI.UserControl
    {
        SH_Common _shCommon;
        public string Msg;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _shCommon = new SH_Common();
                if (_shCommon.CheckSession(ConstValue.LoginSessionName))
                {
                    Msg = "<div class=\"top_slogin\">欢迎：<a href=\"/UM/MyOrder.aspx\" id=\"userAId\">" + Session[ConstValue.LoginSessionName].ToString() + "</a></div>";
                }
                else
                {
                    Msg = "<div class=\"top_regist\"><a href=\"/Register.aspx\"  target=\"_blank\">注册</a></div><div class=\"top_login\"><a href=\"/Login.aspx\"  target=\"_blank\">登录</a></div>";
                }

                if (_shCommon.CheckSession(ConstValue.LoginSessionName))
                {
                    quitDiv.Attributes.Add("style", "display:block");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/List/" + txtSearch.Value);
        }
    }
}