using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI
{
    public partial class Register : System.Web.UI.Page
    {
        public string Msg;
        CommonTools _commonTools;
        SH_Login _sh_login;
        Users _usersModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ClearInput();
                Response.Cookies.Add(new HttpCookie("CheckCode", ""));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CheckEmpty() && CheckLogic())
            {
                _sh_login = new SH_Login();
                _usersModel = new Users();
                _commonTools = new CommonTools();

                _usersModel.UserName = userName.Value;
                _usersModel.UserPwd = MyEncrypt.CreateHash(passWord.Value);
                _usersModel.UserMail = eMail.Value;
                _usersModel.IsValid = true;
                _usersModel.CreateDate = DateTime.Now;
                _usersModel.CreateUserRegion = "";
                _usersModel.LastLoginDate = DateTime.Now;
                _usersModel.LoginTimes = 0;

                _sh_login.InsertRegistUser(_usersModel);

                Session[ConstValue.LoginSessionName] = _usersModel.UserName;

                int grade = 0;
                //before 12-31, add 3000 grade
                if (DateTime.Now < Convert.ToDateTime("2014-12-31"))
                {
                    grade = 3000;
                }
                SH_UserManagement _userMng = new SH_UserManagement();
                string userId = _sh_login.GetUserId(_usersModel.UserName);
                _userMng.AddGrade(userId, grade);

                Response.Redirect("RegistWaiting.aspx");
            }
        }

        #region Check empty
        private bool CheckEmpty()
        {
            bool result = true;
            if (userName.Value == string.Empty)
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("用户名不能为空");
            }
            else if (passWord.Value == string.Empty)
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("密码不能为空");
            }
            else if (rpassWord.Value == string.Empty)
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("重复密码不能为空");
            }
            else if (eMail.Value == string.Empty)
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("邮件地址不能为空");
            }
            else if (txtTz.Value == string.Empty)
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("验证码不能为空");
            }
            return result;
        }
        #endregion

        #region Check logic
        private bool CheckLogic()
        {
            bool result = true;
            _commonTools = new CommonTools();
            _sh_login = new SH_Login();

            if (Request.Cookies["CheckCode"] == null)
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("您的浏览器设置已被禁用 Cookies，您必须设置浏览器允许使用 Cookies 选项后才能继续注册。");
            }
            else if (!_commonTools.RegChineseNumberAlpha(userName.Value))
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("用户名不合法");
            }
            else if (!_sh_login.CheckRegistUsername(userName.Value))
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("用户名已存在");
            }
            else if (!_sh_login.CheckRegistMail(eMail.Value))
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("邮箱已存在");
            }
            else if (!_commonTools.RegPassword(passWord.Value))
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("密码不合法");
            }
            else if (string.Compare(passWord.Value, rpassWord.Value) != 0)
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("密码不一致");
            }
            else if (!_commonTools.RegEmail(eMail.Value))
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("邮件格式错误");
            }
            else if (string.Compare(txtTz.Value.ToLower(), Request.Cookies["CheckCode"].Value.ToLower()) != 0)
            {
                result = false;
                Msg = MessageTools.ShowRegistMessage("验证码错误");
            }
            return result;
        }
        #endregion

        #region Clear input
        private void ClearInput()
        {
            userName.Value = string.Empty;
            passWord.Value = string.Empty;
            rpassWord.Value = string.Empty;
            eMail.Value = string.Empty;
            txtTz.Value = string.Empty;
        }
        #endregion
    }
}