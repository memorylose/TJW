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
    public partial class AddCloth : BasePage
    {
        public string Msg;
        SH_Cloth sh_cloth;
        Cloth clothModel;
        SH_Common sh_common;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindClothType();
                BindCustomDp();
                BindClothChildType();
                BindClothColor();
                BindClothSize();
                BindShow();
                GetEdit();
                SetIsValid();

            }
        }

        #region Bind cloth type
        private void BindClothType()
        {
            sh_cloth = new SH_Cloth();
            List<ClothType> list = sh_cloth.GetFClothType();
            dpF.DataSource = list;
            dpF.DataTextField = "ClothFatherName";
            dpF.DataValueField = "ClothTypeId";
            dpF.DataBind();
        }
        private void BindClothChildType()
        {
            sh_cloth = new SH_Cloth();
            int clothTypeId = Convert.ToInt32(dpF.SelectedItem.Value);
            List<ClothType> list = sh_cloth.GetChildType(clothTypeId);
            dpC.DataSource = list;
            dpC.DataTextField = "ClothFatherName";
            dpC.DataValueField = "ClothTypeId";
            dpC.DataBind();
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

        #region Bind Show
        public void BindShow()
        {
            sh_cloth = new SH_Cloth();
            List<Show> list = sh_cloth.GetShowNumber();
            rdShow.DataSource = list;
            rdShow.DataTextField = "ShowName";
            rdShow.DataValueField = "ShowId";
            rdShow.DataBind();
        }
        #endregion

        #region Check empty
        private bool CheckEmpty()
        {
            bool result = true;
            if (string.IsNullOrEmpty(txtClothName.Value))
            {
                Msg = MessageTools.ShowMessage(0, "服饰名称不能为空");
                txtClothName.Focus();
                result = false;
            }
            else if (string.IsNullOrEmpty(txtStore.Value))
            {
                Msg = MessageTools.ShowMessage(0, "库存不能为空");
                txtStore.Focus();
                result = false;
            }
            else if (string.IsNullOrEmpty(dpC.Text))
            {
                Msg = MessageTools.ShowMessage(0, "服饰类别不能为空");
                result = false;
            }
            else if (string.IsNullOrEmpty(txtPrice.Value))
            {
                Msg = MessageTools.ShowMessage(0, "价格不能为空");
                txtPrice.Focus();
                result = false;
            }
            else if (string.IsNullOrEmpty(dpColor.Text))
            {
                Msg = MessageTools.ShowMessage(0, "颜色不能为空");
                result = false;
            }
            else if (string.IsNullOrEmpty(dpSize.Text))
            {
                Msg = MessageTools.ShowMessage(0, "尺寸不能为空");
                result = false;
            }
            else if (string.IsNullOrEmpty(rdShow.Text))
            {
                Msg = MessageTools.ShowMessage(0, "显示位置不能为空");
                result = false;
            }
            return result;
        }
        #endregion

        #region Set checkbox

        private string SetCheckbox(bool ck)
        {
            if (ck)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        #endregion

        #region Get custom dp
        private void BindCustomDp()
        {
            sh_cloth = new SH_Cloth();
            List<CustomBH> list = sh_cloth.GetCustomFatherName();
            dpCustom.DataSource = list;
            dpCustom.DataTextField = "CustomName";
            dpCustom.DataValueField = "CustomId";
            dpCustom.DataBind();
        }
        #endregion

        #region Add cloth
        private void AddUpdate()
        {
            sh_cloth = new SH_Cloth();
            clothModel = new Cloth();

            clothModel.ClothGuid = CommonTools.GenerateGUID("", false);
            clothModel.StuffUGUID = CommonTools.GenerateGUID("C", true);
            clothModel.ClothName = txtClothName.Value;
            clothModel.ClothTypeId = Convert.ToInt32(dpC.SelectedItem.Value);
            clothModel.StoreCount = Convert.ToInt32(txtStore.Value);
            clothModel.OriginalPrice = float.Parse(txtOriPrice.Value);
            clothModel.Price = float.Parse(txtPrice.Value);
            clothModel.ColorId = Convert.ToInt32(dpColor.SelectedItem.Value);
            clothModel.SizeId = Convert.ToInt32(dpSize.SelectedItem.Value);
            clothModel.IsVaild = ckValid.Checked;
            clothModel.ShowNum = Convert.ToInt32(rdShow.SelectedItem.Value);
            clothModel.ZheKou = dpZK.SelectedItem.Value;

            clothModel.CreateDate = DateTime.Now;
            clothModel.CreateUserId = LoginUser.UserId;
            clothModel.ModifiedDate = DateTime.Now;
            clothModel.ModifiedUserId = LoginUser.UserId;
            clothModel.IsTj = SetCheckbox(ckRmTop.Checked) + SetCheckbox(ckRm.Checked) + SetCheckbox(ckRxTop.Checked) + SetCheckbox(ckRx.Checked);
            if (Request.Form["ckCustomBH"] != null)
            {
                clothModel.CustomBHId = Convert.ToInt32(dpCustom.SelectedItem.Value);
            }
            else
            {
                clothModel.CustomBHId = 0;
            }

            if (Request.QueryString["editId"] != null)
            {
                clothModel.ClothId = Convert.ToInt32(Request.QueryString["editId"]);
                sh_cloth.UpdateCloth(clothModel);
                sh_cloth.UpdateZk(dpZK.SelectedItem.Value, Convert.ToInt32(Request.QueryString["editId"]));
                Response.Redirect("MngCloth.aspx");
            }
            else
            {
                sh_cloth.AddCloth(clothModel);
                Response.Redirect("MngCloth.aspx");
            }

        }
        #endregion

        #region Get edit

        private void GetEdit()
        {
            if (Request.QueryString["editId"] != null)
            {
                sh_cloth = new SH_Cloth();
                sh_common = new SH_Common();
                DataSet ds = sh_cloth.GetClothEdit(Request.QueryString["editId"]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dpC.ClearSelection();
                    dpColor.ClearSelection();
                    dpSize.ClearSelection();
                    dpF.ClearSelection();
                    dpZK.ClearSelection();

                    txtClothName.Value = ds.Tables[0].Rows[0][0].ToString();
                    sh_common.SetDpListValue(dpC, ds.Tables[0].Rows[0][1].ToString());
                    txtStore.Value = ds.Tables[0].Rows[0][2].ToString();
                    txtOriPrice.Value = ds.Tables[0].Rows[0][3].ToString();
                    txtPrice.Value = ds.Tables[0].Rows[0][4].ToString();
                    sh_common.SetDpListValue(dpColor, ds.Tables[0].Rows[0][5].ToString());
                    sh_common.SetDpListValue(dpSize, ds.Tables[0].Rows[0][6].ToString());
                    ckValid.Checked = SetBool(ds.Tables[0].Rows[0][7]);
                    ckRmTop.Checked = SetBool(ds.Tables[0].Rows[0][8]);
                    ckRm.Checked = SetBool(ds.Tables[0].Rows[0][9]);
                    ckRxTop.Checked = SetBool(ds.Tables[0].Rows[0][10]);
                    ckRx.Checked = SetBool(ds.Tables[0].Rows[0][11]);
                    sh_common.SetRdListValue(rdShow, ds.Tables[0].Rows[0][12].ToString());
                    sh_common.SetDpListValue(dpF, ds.Tables[0].Rows[0][13].ToString());
                    sh_common.SetDpListValue(dpZK, SetZheKouDp(ds.Tables[0].Rows[0]["ZheKou"].ToString()));

                    if (ds.Tables[0].Rows[0][14].ToString() != "" && ds.Tables[0].Rows[0][14].ToString() != null && ds.Tables[0].Rows[0][14].ToString() != "0")
                    {
                        hdCustomFlag.Value = "1";
                        cusDp.Attributes.Add("style", "display:block");
                        sh_common.SetDpListValue(dpCustom, ds.Tables[0].Rows[0][14].ToString());
                    }

                }
                else
                {
                    Response.Redirect("MngCloth.aspx");
                }
            }

        }

        #endregion

        #region Set IsValid

        private void SetIsValid()
        {
            if (Request.QueryString["editId"] == null)
            {
                ckValid.Checked = true;
            }
        }

        #endregion

        #region Set bool
        private bool SetBool(object code)
        {
            if (code.ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Set zhekou dp

        private string SetZheKouDp(string zk)
        {
            string result = "";
            if (zk == "10")
                result = "原价";
            else
                result = zk + "折";
            return result;
        }

        #endregion


        protected void dpF_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindClothChildType();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CheckEmpty())
            {
                AddUpdate();
            }
        }

    }
}