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
    public partial class MyGrade : System.Web.UI.Page
    {
        SH_Common _shCommon;
        protected string totalGrade;
        protected string grade;
        protected string vipName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _shCommon = new SH_Common();
                if (_shCommon.CheckSession(ConstValue.LoginSessionName))
                {
                    SetGrade();
                }
                else
                {
                    Response.Redirect("/Login.aspx?mygrade=1");
                }
            }
        }

        #region Set grade
        private void SetGrade()
        {
            string userName = Session[ConstValue.LoginSessionName].ToString();
            SH_UserManagement _userMng = new SH_UserManagement();
            SH_Login _sh_login = new SH_Login();
            _shCommon = new SH_Common();

            string bank = "1";
            string userId = _sh_login.GetUserId(userName);
            DataSet ds = _userMng.GetCurrentGrade(userId);
            totalGrade = ds.Tables[0].Rows[0][0].ToString();
            grade = ds.Tables[0].Rows[0][1].ToString();

            vipName = _shCommon.GetVipType(Convert.ToInt32(totalGrade), ref bank);
        }
        #endregion
    }
}