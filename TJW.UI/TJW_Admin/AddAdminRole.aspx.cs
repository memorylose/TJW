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
    public partial class AddAdminRole : System.Web.UI.Page
    {
        public string Msg;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AdminRole model = new AdminRole();
            SH_AdminUser shAdmin = new SH_AdminUser();

            model.AdminRoleName = txtRoleName.Value;
            shAdmin.AddAdminRole(model);
            Msg = MessageTools.ShowMessage(1, "权限添加成功");
        }        
    }
}