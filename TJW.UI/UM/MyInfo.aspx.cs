using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.UM
{
    public partial class MyInfo : System.Web.UI.Page
    {
        protected string UserName;
        protected string Mail;
        protected string CreateDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SH_Common _shCommon = new SH_Common();
                if (_shCommon.CheckSession(ConstValue.LoginSessionName))
                {
                    string userName = Session[ConstValue.LoginSessionName].ToString();
                    SH_UserManagement _um = new SH_UserManagement();
                    DataSet ds = _um.GetUserInfo(userName);
                    UserName = ds.Tables[0].Rows[0][0].ToString();
                    Mail = ds.Tables[0].Rows[0][1].ToString();
                    CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0][2]).ToString("yyyy-MM-dd");
                }
                else
                {
                    Response.Redirect("/Login.aspx?mi=1");
                }
            }
        }
    }
}