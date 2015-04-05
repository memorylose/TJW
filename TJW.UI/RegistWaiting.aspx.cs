using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TJW.UI
{
    public partial class RegistWaiting : System.Web.UI.Page
    {
        protected string strGrade;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (DateTime.Now < Convert.ToDateTime("2014-12-31"))
                {
                    strGrade = "您已获得3000积分";
                }
            }
        }
    }
}