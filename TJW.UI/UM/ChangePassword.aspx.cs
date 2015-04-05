using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.UM
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected string Msg;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SH_Common _shCommon = new SH_Common();
                if (_shCommon.CheckSession(ConstValue.LoginSessionName))
                {
                    Response.Cookies.Add(new HttpCookie("CheckCode", ""));
                }
                else
                {
                    Response.Redirect("/Login.aspx?cp=1");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CheckEmpty() && CheckLogic())
            {
                SH_Common _shCommon = new SH_Common();
                if (_shCommon.CheckSession(ConstValue.LoginSessionName))
                {
                    SH_Login _shLogin = new SH_Login();
                    string userName = Session[ConstValue.LoginSessionName].ToString();

                    if (!_shLogin.CheckPassword(userName, oldPwd.Value))
                    {
                        Msg = MessageTools.ShowRegistMessage("原始密码错误");
                    }
                    else
                    {
                        SH_UserManagement _um = new SH_UserManagement();
                        _um.ChangePassword(MyEncrypt.CreateHash(passWord.Value), userName);
                        Msg = MessageTools.ShowRegistMessage("密码修改成功");
                    }

                }
                else
                {
                    Response.Redirect("/Login.aspx?cp=1");
                }
            }
        }

        #region Check empty

        private bool CheckEmpty()
        {
            bool result = false;
            if (oldPwd.Value == string.Empty)
            {
                Msg = MessageTools.ShowRegistMessage("原始密码不能为空");
            }
            else if (passWord.Value == string.Empty)
            {
                Msg = MessageTools.ShowRegistMessage("新密码不能为空");
            }
            else if (rpassWord.Value == string.Empty)
            {
                Msg = MessageTools.ShowRegistMessage("重复密码不能为空");
            }
            else if (string.Compare(txtTz.Value.ToLower(), Request.Cookies["CheckCode"].Value.ToLower()) != 0)
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("验证码错误");
            }
            else
            {
                result = true;
            }
            return result;
        }

        #endregion

        #region Check logic

        private bool CheckLogic()
        {
            bool result = false;
            CommonTools _commonTools = new CommonTools();
            if (!_commonTools.RegPassword(passWord.Value))
            {
                Msg = MessageTools.ShowRegistMessage("密码不合法");
            }
            else if (string.Compare(passWord.Value, rpassWord.Value) != 0)
            {
                Msg = MessageTools.ShowRegistMessage("密码不一致");
            }
            else
            {
                result = true;
            }
            return result;
        }

        #endregion
    }
}