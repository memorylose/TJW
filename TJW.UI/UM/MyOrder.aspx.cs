using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.HtmlOuts;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.UM
{
    public partial class MyOrder : System.Web.UI.Page
    {
        SH_Common _shCommon;
        UserMng _userMng;
        SH_Login _shLogin;
        protected string orders;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _shCommon = new SH_Common();
                _shLogin = new SH_Login();
                if (_shCommon.CheckSession(ConstValue.LoginSessionName))
                {
                    if (Request.QueryString["t"] != null)
                    {
                        string where = " AND OrderStatusId = " + Request.QueryString["t"].ToString();
                        //这里需要对t进行判断
                        //orders = GetOrder(where);
                        orders = GetOrder("");

                    }
                    else
                    {
                        orders = GetOrder("");
                    }
                    Del();
                }
                else
                {
                    Response.Redirect("../Login.aspx?um=2");
                }
            }
        }

        #region Get order
        protected string GetOrder(string statusId)
        {
            _userMng = new UserMng();
            return _userMng.HtmlMyOrder(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString()), statusId);
        }
        #endregion

        #region Del

        private void Del()
        {
            if (Request.QueryString["del"] != null)
            {
                string href = "/UM/MyOrder.aspx";
                _userMng = new UserMng();
                _userMng.DeleteMyOrder(Convert.ToInt32(Request.QueryString["del"]));
                Response.Redirect(href);
            }
        }

        #endregion
    }
}