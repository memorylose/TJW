using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.HtmlOuts;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.TJW_Admin
{
    public partial class Main : BasePage
    {
        public string MainUserName;
        public string MainTime;

        CommonTools cTools;
        SH_AdminUser _shAdminUser;
        Admin _adminOut;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }

        }

        #region Get current time
        protected string GetCurrentTime()
        {
            cTools = new CommonTools();
            return cTools.GetTimeInterval();
        }
        #endregion

        #region Get current user
        protected string GetCurrentUser()
        {
            return LoginUser.UserName;
        }
        #endregion

        #region SetRole

        private int GetRole()
        {
            _shAdminUser = new SH_AdminUser();
            return _shAdminUser.GetRoleId(LoginUser.UserId);
        }

        #endregion

        #region Html cloth

        protected string HtmlCloth()
        {
            _adminOut = new Admin();
            return _adminOut.MainCommonItem("cloth", GetRole());
        }

        #endregion
    }
}