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
    public partial class AddTea : BasePage
    {
        Tea _teaModel;
        SH_Tea _shTea;
        SH_Common sh_common;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindTeaType();
                FunGetEditTea();
            }
        }

        #region Bind tea type
        private void BindTeaType()
        {
            _shTea = new SH_Tea();
            dpTeaType.DataSource = _shTea.GetTeaType();
            dpTeaType.DataTextField = "TypeName";
            dpTeaType.DataValueField = "TypeId";
            dpTeaType.DataBind();
        }

        #endregion

        #region Add tea
        private void FnAddUpdateTea()
        {
            _teaModel = new Tea();

            _teaModel.TeaName = txtTeaName.Value;
            _teaModel.TeaCount = Convert.ToInt32(txtCount.Value);
            _teaModel.TeaOriPrice = float.Parse(txtOriPrice.Value);
            _teaModel.TeaPrice = float.Parse(txtPrice.Value);
            _teaModel.TeaTypeId = Convert.ToInt32(dpTeaType.SelectedItem.Value);
            _teaModel.TeaYear = Convert.ToInt32(dpTeaYear.SelectedItem.Value);
            _teaModel.TeaDescription = txtDes.Value;
            _shTea = new SH_Tea();

            if (Request.QueryString["editId"] != null)
            {
                _teaModel.TeaId = Convert.ToInt32(Request.QueryString["editId"]);
                _teaModel.ModifyUserId = LoginUser.UserId;
                _teaModel.ModifyDate = DateTime.Now;
                _shTea.UpdateTea(_teaModel);
                Response.Redirect("MngTea.aspx");
            }
            else
            {
                _teaModel.StuffUGUID = CommonTools.GenerateGUID("T", true);
                _teaModel.IsValid = true;
                _teaModel.CreateDate = DateTime.Now;
                _teaModel.CreateUserId = LoginUser.UserId;
                
                _shTea.AddTea(_teaModel);
                Response.Redirect("MngTea.aspx");
            }
        }
        #endregion

        #region Get edit tea
        private void FunGetEditTea()
        {
            if (Request.QueryString["editId"] != null)
            {
                _shTea = new SH_Tea();
                DataSet ds = _shTea.GetEditTea(Convert.ToInt32(Request.QueryString["editId"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sh_common = new SH_Common();
                    sh_common.SetDpListValue(dpTeaType, ds.Tables[0].Rows[0][7].ToString());
                    sh_common.SetDpListValue(dpTeaYear, SetEditTeaYear(ds.Tables[0].Rows[0][5].ToString()));

                    txtTeaName.Value = ds.Tables[0].Rows[0][0].ToString();
                    txtCount.Value = ds.Tables[0].Rows[0][1].ToString();
                    txtOriPrice.Value = ds.Tables[0].Rows[0][2].ToString();
                    txtPrice.Value = ds.Tables[0].Rows[0][3].ToString();
                    txtDes.Value = ds.Tables[0].Rows[0][6].ToString();
                }
                else
                {
                    Response.Redirect("MngTea.aspx");
                }
            }
        }
        #endregion

        #region Set edit tea year
        private string SetEditTeaYear(string year)
        {
            if (string.Equals(year, "11"))
            {
                return "10年以上";
            }
            else
            {
                return year + "年";
            }
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            FnAddUpdateTea();
        }
    }
}