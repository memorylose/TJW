using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.TJW_Admin
{
    public partial class AddSameCloth : BasePage
    {
        SH_Cloth sh_cloth;
        Cloth clothModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetClothName();
                BindClothSize();
                BindClothColor();
            }
        }

        #region Get cloth name
        private void GetClothName()
        {
            if (Request.QueryString["clothId"] != null)
            {
                sh_cloth = new SH_Cloth();
                txtClothName.Value = sh_cloth.GetClothName(Convert.ToInt32(Request.QueryString["clothId"]));
            }
        }
        #endregion

        #region Bind cloth color and size
        private void BindClothSize()
        {
            sh_cloth = new SH_Cloth();
            List<Size> list = sh_cloth.GetSize();
            dpSize.DataSource = list;
            dpSize.DataTextField = "SizeName";
            dpSize.DataValueField = "SizeId";
            dpSize.DataBind();
        }
        private void BindClothColor()
        {
            sh_cloth = new SH_Cloth();
            List<Color> list = sh_cloth.GetColor();
            dpColor.DataSource = list;
            dpColor.DataTextField = "ColorName";
            dpColor.DataValueField = "ColorId";
            dpColor.DataBind();
        }
        #endregion

        #region Add
        private void Add()
        {
            if (Request.QueryString["clothId"] != null)
            {
                sh_cloth = new SH_Cloth();
                clothModel = new Cloth();
                DataSet ds = sh_cloth.GetSameCloth(Convert.ToInt32(Request.QueryString["clothId"]));
                clothModel.ClothGuid = ds.Tables[0].Rows[0][1].ToString();
                clothModel.ClothName = ds.Tables[0].Rows[0][3].ToString();
                clothModel.ClothTypeId = Convert.ToInt32(ds.Tables[0].Rows[0][4]);
                clothModel.StoreCount = Convert.ToInt32(txtStore.Value);
                clothModel.OriginalPrice = float.Parse(ds.Tables[0].Rows[0][5].ToString());
                clothModel.Price = float.Parse(ds.Tables[0].Rows[0][6].ToString());
                clothModel.ColorId = Convert.ToInt32(dpColor.SelectedItem.Value);
                clothModel.SizeId = Convert.ToInt32(dpSize.SelectedItem.Value);
                clothModel.IsVaild = Convert.ToBoolean(ds.Tables[0].Rows[0][7]);
                clothModel.IsTj = ds.Tables[0].Rows[0][9].ToString();
                clothModel.ShowNum = Convert.ToInt32(ds.Tables[0].Rows[0][8]);
                clothModel.StuffUGUID = CommonTools.GenerateGUID("C", true);
                clothModel.CreateDate = DateTime.Now;
                clothModel.CreateUserId = LoginUser.UserId;
                clothModel.CustomBHId = Convert.ToInt32(ds.Tables[0].Rows[0][10]);
                clothModel.ZheKou = ds.Tables[0].Rows[0][11].ToString();

                sh_cloth.AddCloth(clothModel);
                Response.Redirect("MngCloth.aspx");
            }
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Add();
        }
    }
}