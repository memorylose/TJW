using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Model;
using TJW.SqlHandler;

namespace TJW.UI.TJW_Admin
{
    public partial class MngCustomBH : System.Web.UI.Page
    {
        SH_Cloth _shCloth;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetFatherType();
                BindGv();
                GetEdit();
                DeleteCustom();
            }
        }

        protected void btnType_Click(object sender, EventArgs e)
        {
            AddCustom(txtType.Value, "0");
            Response.Redirect("MngCustomBH.aspx");
        }

        protected void btnBH_Click(object sender, EventArgs e)
        {
            AddCustom(txtBH.Value, dpFType.SelectedItem.Value);
            Response.Redirect("MngCustomBH.aspx");
        }

        #region Add custom
        private void AddCustom(string customName, string customFatherId)
        {
            _shCloth = new SH_Cloth();
            if (Request.QueryString["editId"] != null)
            {
                _shCloth.UpdateCustomBH(customName, customFatherId, Request.QueryString["editId"].ToString());
            }
            else
            {
                _shCloth.AddCustomType(customName, customFatherId);
            }
        }
        #endregion

        #region Get father type
        private void GetFatherType()
        {
            _shCloth = new SH_Cloth();
            List<CustomBH> list = _shCloth.GetCustomFatherName();
            dpFType.DataSource = list;
            dpFType.DataTextField = "CustomName";
            dpFType.DataValueField = "CustomId";
            dpFType.DataBind();
        }
        #endregion

        #region Bind gv
        private void BindGv()
        {
            _shCloth = new SH_Cloth();
            DataSet ds = _shCloth.GetCustomBH();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        #endregion

        #region Get edit
        private void GetEdit()
        {
            if (Request.QueryString["editId"] != null)
            {
                _shCloth = new SH_Cloth();
                SH_Common sh_common = new SH_Common();
                DataSet ds = _shCloth.GetCustomEdit(Request.QueryString["editId"]);
                txtBH.Value = ds.Tables[0].Rows[0][0].ToString();
                sh_common.SetDpListValue(dpFType, ds.Tables[0].Rows[0][1].ToString());
            }

        }
        #endregion

        #region Delete
        private void DeleteCustom()
        {
            if (Request.QueryString["delId"] != null)
            {
                _shCloth = new SH_Cloth();
                _shCloth.DeleteCustomBH(Request.QueryString["delId"].ToString());
                Response.Redirect("MngCustomBH.aspx");
            }
        }
        #endregion
    }
}