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
    public partial class MngClothColor : System.Web.UI.Page
    {
        SH_Cloth sh_cloth;
        Color colorModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetColorList();
                GetColorEdit();
                DeleteColor();
            }
        }

        protected void btnColor_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["editId"] != null)
            {
                EditColor();
            }
            else
            {
                AddColor();
            }
        }

        #region Add color
        private void AddColor()
        {
            colorModel = new Color();
            colorModel.ColorName = txtColorName.Value;

            sh_cloth = new SH_Cloth();
            sh_cloth.AddClothColor(colorModel);
            Response.Redirect("MngClothColor.aspx");
        }
        #endregion

        #region Edit color
        public void EditColor()
        {
            if (Request.QueryString["editId"] != null)
            {
                sh_cloth = new SH_Cloth();
                sh_cloth.EditColor(Convert.ToInt32(Request.QueryString["editId"]), txtColorName.Value);
                Response.Redirect("MngClothColor.aspx");
            }
        }
        #endregion

        #region Delete color
        private void DeleteColor()
        {
            if (Request.QueryString["delId"] != null)
            {
                sh_cloth = new SH_Cloth();
                sh_cloth.DeteleColor(Convert.ToInt32(Request.QueryString["delId"]));
                Response.Redirect("MngClothColor.aspx");
            }
        }
        #endregion

        #region Get color edit
        private void GetColorEdit()
        {
            if (Request.QueryString["editId"] != null)
            {
                sh_cloth = new SH_Cloth();
                List<Color> colorList = sh_cloth.GetColorEdit(Convert.ToInt32(Request.QueryString["editId"]));
                foreach (Color model in colorList)
                {
                    txtColorName.Value = model.ColorName;
                }
                addFId.Attributes.Add("style", "display:block");
            }
        }
        #endregion

        #region Get color
        private void GetColorList()
        {
            sh_cloth = new SH_Cloth();
            colorModel = new Color();

            rpColor.DataSource = sh_cloth.GetColor();
            rpColor.DataBind();

        }
        #endregion
    }
}