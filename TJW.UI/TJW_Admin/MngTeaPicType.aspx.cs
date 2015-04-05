using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;

namespace TJW.UI.TJW_Admin
{
    public partial class MngTeaPicType : System.Web.UI.Page
    {
        TJW.Model.TeaPictureType _model;
        SH_Tea _shTea;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DeleteTeaTypePic();
                FunGetPictureType();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["editId"] != null)
            {

            }
            else
            {
                Add();
            }

        }
        #region Add picture type
        private void Add()
        {
            _model = new Model.TeaPictureType();
            _model.TypeName = txtTeaTypePicName.Value;

            _shTea = new SH_Tea();
            _shTea.AddTeaPictureType(_model);
            Response.Redirect("MngTeaPicType.aspx");
        }
        #endregion

        #region Get picture type
        private void FunGetPictureType()
        {
            _shTea = new SH_Tea();
            rpTeaPictureType.DataSource = _shTea.GetTeaPictureType();
            rpTeaPictureType.DataBind();
        }
        #endregion

        #region Delete tea picture type
        private void DeleteTeaTypePic()
        {
            if (Request.QueryString["delId"] != null)
            {
                _shTea = new SH_Tea();
                _shTea.DeleteTeaPictureType(Convert.ToInt32(Request.QueryString["delId"]));
                Response.Redirect("MngTeaPicType.aspx");
            }
        }
        #endregion
    }
}