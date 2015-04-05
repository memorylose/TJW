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
    public partial class MngClothSize : System.Web.UI.Page
    {
        SH_Cloth sh_cloth;
        Size sizeModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetSizeList();
                GetSizeEdit();
                DeleteSize();
            }
        }

        protected void btnSize_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["editId"] != null)
            {
                EditSize();
            }
            else
            {
                AddSize();
            }
        }

        #region Add size
        private void AddSize()
        {
            sizeModel = new Size();
            sizeModel.SizeName = txtSizeName.Value;

            sh_cloth = new SH_Cloth();
            sh_cloth.AddClothSize(sizeModel);
            Response.Redirect("MngClothSize.aspx");
        }
        #endregion

        #region Edit size
        public void EditSize()
        {
            if (Request.QueryString["editId"] != null)
            {
                sh_cloth = new SH_Cloth();
                sh_cloth.EditSize(Convert.ToInt32(Request.QueryString["editId"]), txtSizeName.Value);
                Response.Redirect("MngClothSize.aspx");
            }
        }
        #endregion

        #region Delete size
        private void DeleteSize()
        {
            if (Request.QueryString["delId"] != null)
            {
                sh_cloth = new SH_Cloth();
                sh_cloth.DeteleSize(Convert.ToInt32(Request.QueryString["delId"]));
                Response.Redirect("MngClothSize.aspx");
            }
        }
        #endregion

        #region Get cloth size
        private void GetSizeEdit()
        {
            if (Request.QueryString["editId"] != null)
            {
                sh_cloth = new SH_Cloth();
                List<Size> sizeList = sh_cloth.GetSizeEdit(Convert.ToInt32(Request.QueryString["editId"]));
                foreach (Size model in sizeList)
                {
                    txtSizeName.Value = model.SizeName;
                }
                addFId.Attributes.Add("style", "display:block");
            }
        }
        #endregion

        #region Get size
        private void GetSizeList()
        {
            sh_cloth = new SH_Cloth();
            sizeModel = new Size();

            rpSize.DataSource = sh_cloth.GetSize();
            rpSize.DataBind();

        }
        #endregion
    }
}