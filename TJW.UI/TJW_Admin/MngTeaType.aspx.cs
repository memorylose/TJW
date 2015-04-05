using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Model;
using TJW.SqlHandler;

namespace TJW.UI.TJW_Admin
{
    public partial class MngTeaType : System.Web.UI.Page
    {
        TeaType _teaModel;
        SH_Tea _shTea;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DeleteTea();
                BindTeaType();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["teaTypeId"] != null)
            {

            }
            else
            {
                AddTeaType();
            }
        }

        #region Add tea type
        private void AddTeaType()
        {
            _teaModel = new TeaType();
            _teaModel.TypeName = txtTeaTypeName.Value;

            _shTea = new SH_Tea();
            _shTea.AddTeaType(_teaModel);
            Response.Redirect("MngTeaType.aspx");
        }
        #endregion

        #region Bind tea type
        private void BindTeaType()
        {
            _shTea = new SH_Tea();
            rpTeaType.DataSource = _shTea.GetTeaType();
            rpTeaType.DataBind();
        }
        #endregion

        #region Delete tea
        private void DeleteTea()
        {
            if (Request.QueryString["delId"] != null)
            {
                _shTea = new SH_Tea();
                _shTea.DeleteTeaType(Convert.ToInt32(Request.QueryString["delId"]));
                Response.Redirect("MngTeaType.aspx");
            }
        }
        #endregion
    }
}