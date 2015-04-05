using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;
using TJW.Model;
using TJW.Utils;

namespace TJW.UI.TJW_Admin
{
    public partial class MngClothType : BasePage
    {
        SH_Cloth sh_cloth;
        ClothType clothModel;
        SH_Common sh_common;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetTypeList();
                GetFatherType();
                DeleteType();
                GetEditInfo();
            }
        }

        protected void btnF_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["editFId"] != null)
            {
                EditType();
            }
            else
            {
                AddType(0);            
            }
        }

        protected void btnC_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["editCId"] != null)
            {
                EditType();
            }
            else
            {
                AddType(1);                
            }
        }

        #region Bind cloth type
        private void GetTypeList()
        {
            sh_cloth = new SH_Cloth();

            //bind two kind of cloth type
            rpFClothType.DataSource = sh_cloth.GetFClothType();
            rpFClothType.DataBind();

            rpCClothType.DataSource = sh_cloth.GetCClothType();
            rpCClothType.DataBind();
        }
        #endregion

        #region Get father type
        private void GetFatherType()
        {
            sh_cloth = new SH_Cloth();
            List<ClothType> list = sh_cloth.GetFatherName();
            dpFType.DataSource = list;
            dpFType.DataTextField = "ClothFatherName";
            dpFType.DataValueField = "ClothTypeId";
            dpFType.DataBind();
        }
        #endregion

        #region Add cloth type
        private void AddType(int type)
        {
            sh_cloth = new SH_Cloth();
            clothModel = new ClothType();

            clothModel.CreateUserId = LoginUser.UserId;
            if (type == 0)
            {
                clothModel.ClothFahterId = 0;
                clothModel.ClothFatherName = txtFname.Value;
            }
            else
            {
                clothModel.ClothFahterId = Convert.ToInt32(dpFType.SelectedItem.Value);
                clothModel.ClothFatherName = txtCname.Value;
            }

            sh_cloth.AddClothType(clothModel);
            Response.Redirect("MngClothType.aspx");
        }
        #endregion

        #region Edit cloth type
        private void EditType()
        {
            sh_cloth = new SH_Cloth();
            clothModel = new ClothType();
            if (Request.QueryString["editFId"] != null)
            {
                clothModel.ClothTypeId = Convert.ToInt32(Request.QueryString["editFId"]);
                clothModel.ClothFatherName = txtFname.Value;
                sh_cloth.EditFClothType(clothModel);
                Response.Redirect("MngClothType.aspx");
            }
            else if (Request.QueryString["editCId"] != null)
            {
                clothModel.ClothTypeId = Convert.ToInt32(Request.QueryString["editCId"]);
                clothModel.ClothFatherName = txtCname.Value;
                clothModel.ClothFahterId = Convert.ToInt32(dpFType.SelectedItem.Value);
                sh_cloth.EditCClothType(clothModel);
                Response.Redirect("MngClothType.aspx");
            }
        }
        #endregion

        #region Delete type
        private void DeleteType()
        {
            sh_cloth = new SH_Cloth();
            //delete father type
            if (Request.QueryString["delFId"] != null)
            {
                int delId = Convert.ToInt32(Request.QueryString["delFId"]);
                if (sh_cloth.CheckFatherCount())
                {
                    //if father count =1 , can not delete anymore
                    sh_cloth.DeleteClothType(delId);
                    //delete child type
                    sh_cloth.DeleteChildClothType(delId);
                }
                Response.Redirect("MngClothType.aspx");
            }
            //delete child type
            if (Request.QueryString["delCId"] != null)
            {
                int delId = Convert.ToInt32(Request.QueryString["delCId"]);
                sh_cloth.DeleteClothType(delId);
                Response.Redirect("MngClothType.aspx");
            }
        }
        #endregion

        #region Get edit info
        private void GetEditInfo()
        {
            if (Request.QueryString["editFId"] != null || Request.QueryString["editCId"] != null)
            {
                sh_cloth = new SH_Cloth();
                sh_common = new SH_Common();

                int id = 0;
                if (Request.QueryString["editFId"] != null)
                {
                    id = Convert.ToInt32(Request.QueryString["editFId"]);
                }
                else if (Request.QueryString["editCId"] != null)
                {
                    id = Convert.ToInt32(Request.QueryString["editCId"]);
                }

                List<ClothType> typeList = sh_cloth.GetTypeEdit(id);
                foreach (ClothType model in typeList)
                {
                    if (Request.QueryString["editFId"] != null)
                    {
                        txtFname.Value = model.ClothFatherName;
                        addFId.Attributes.Add("style", "display:block");
                    }
                    else if (Request.QueryString["editCId"] != null)
                    {
                        sh_common.SetDpListValue(dpFType, model.TopName);
                        txtCname.Value = model.ClothFatherName;
                        addCId.Attributes.Add("style", "display:block");
                    }
                }
            }
        }
        #endregion
    }
}