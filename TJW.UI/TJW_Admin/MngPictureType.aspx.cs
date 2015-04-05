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
    public partial class MngPictureType : System.Web.UI.Page
    {
        SH_Cloth sh_cloth;
        PictureType typeModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetTypeList();
                GetPictureTypeEdit();
                DeletePictureType();
            }
        }

        #region Add picture type
        private void AddPictureType()
        {
            typeModel = new PictureType();
            typeModel.TypeName = txtTypeName.Value;

            sh_cloth = new SH_Cloth();
            sh_cloth.AddPictureType(typeModel);
            Response.Redirect("MngPictureType.aspx");
        }
        #endregion

        #region Edit picture type
        public void EditPictureType()
        {
            if (Request.QueryString["editId"] != null)
            {
                sh_cloth = new SH_Cloth();
                sh_cloth.EditPictureType(Convert.ToInt32(Request.QueryString["editId"]), txtTypeName.Value);
                Response.Redirect("MngPictureType.aspx");
            }
        }
        #endregion

        #region Delete picture type
        private void DeletePictureType()
        {
            if (Request.QueryString["delId"] != null)
            {
                sh_cloth = new SH_Cloth();
                sh_cloth.DetelePictureType(Convert.ToInt32(Request.QueryString["delId"]));
                Response.Redirect("MngPictureType.aspx");
            }
        }
        #endregion

        #region Get picture type edit
        private void GetPictureTypeEdit()
        {
            if (Request.QueryString["editId"] != null)
            {
                sh_cloth = new SH_Cloth();
                List<PictureType> typeList = sh_cloth.GetPictureTypeEdit(Convert.ToInt32(Request.QueryString["editId"]));
                foreach (PictureType model in typeList)
                {
                    txtTypeName.Value = model.TypeName;
                }
                addFId.Attributes.Add("style", "display:block");
            }
        }
        #endregion

        #region Get color
        private void GetTypeList()
        {
            sh_cloth = new SH_Cloth();
            typeModel = new PictureType();

            rpType.DataSource = sh_cloth.GetPictureTypeList();
            rpType.DataBind();

        }
        #endregion

        protected void btnType_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["editId"] != null)
            {
                EditPictureType();
            }
            else
            {
                AddPictureType();
            }
        }

    }
}