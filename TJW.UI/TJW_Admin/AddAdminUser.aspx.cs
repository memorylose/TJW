using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.TJW_Admin
{
    public partial class AddAdminUser : System.Web.UI.Page
    {
        public string Msg;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetRoleLIst();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AdminUser model = new AdminUser();

            model.AdminUserName = txtUsername.Value;
            model.AdminPwd = txtPassword.Value;
            model.AdminRoleId = Convert.ToInt32(dpRole.SelectedItem.Value);
            model.CreateDate = DateTime.Now;
            model.IsValid = true;

            SH_AdminUser shAdmin = new SH_AdminUser();
            shAdmin.AddAdminUser(model);

            Msg = MessageTools.ShowMessage(1, "用户添加成功");
        }

        #region Get admin role
        private void GetRoleLIst()
        {
            SH_AdminUser shAdmin = new SH_AdminUser();
            List<AdminRole> list = shAdmin.GetAdminRole();
            dpRole.DataSource = list;
            dpRole.DataTextField = "AdminRoleName";
            dpRole.DataValueField = "AdminRoleId";
            dpRole.DataBind();
        }
        #endregion
    }
}