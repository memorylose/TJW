using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;

namespace TJW.UI.TJW_Admin
{
    public partial class MngMember : System.Web.UI.Page
    {
        SH_Login _shLogin;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
                if (Request.QueryString["userId"] != null)
                {
                    GetGradeEdit();
                }
            }
        }

        private void Bind()
        {
            _shLogin = new SH_Login();
            GridView1.DataSource = _shLogin.GetMember();
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {
            Bind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
        }

        private void GetGradeEdit()
        {
            jfModify.Attributes.Add("style","display:block");
            _shLogin = new SH_Login();
            DataSet ds = _shLogin.GetEditGrade(Request.QueryString["userId"]);
            txtTotalJf.Value = ds.Tables[0].Rows[0][0].ToString();
            txtJf.Value = ds.Tables[0].Rows[0][1].ToString();
        }

        protected void btnJf_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["userId"] != null)
            {
                _shLogin = new SH_Login();
                _shLogin.UpdateGrade(txtTotalJf.Value, txtJf.Value, Request.QueryString["userId"]);
                Response.Redirect("MngMember.aspx");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#E2F0FF'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
    }
}