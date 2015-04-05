using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;

namespace TJW.UI.WeChat
{
    public partial class Detail : System.Web.UI.Page
    {
        SH_WeChat _shWeChat;
        public string Contents;
        public string Titles;
        public string Time;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FunGetDetailMessage();
            }
        }

        #region Get detail message
        private void FunGetDetailMessage()
        {
            if (Request.QueryString["wcId"] != null)
            {
                _shWeChat = new SH_WeChat();
                DataSet ds = _shWeChat.GetDetailMessage(Request.QueryString["wcId"]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Titles = ds.Tables[0].Rows[0][0].ToString();
                    Contents = ds.Tables[0].Rows[0][2].ToString();
                    Time = Convert.ToDateTime(ds.Tables[0].Rows[0][4]).ToString("yyyy-MM-dd");
                   
                }
            }
        }
        #endregion
    }
}