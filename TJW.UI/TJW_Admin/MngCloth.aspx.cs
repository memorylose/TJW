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
using PagerHelper;
using System.IO;
using System.Web.UI.HtmlControls;



namespace TJW.UI.TJW_Admin
{
    public partial class MngCloth : BasePage
    {
        SH_Cloth sh_cloth;
        SH_AdminUser _shAdmin;
        private int PageSize = 20;
        public string Msg;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DeleteCloth();
                GetClothList("");
            }
        }


        #region Get cloth
        private void GetClothList(string searchCon)
        {
            sh_cloth = new SH_Cloth();
            GridView1.DataSource = sh_cloth.GetClothNoPage(searchCon);
            GridView1.DataBind();
        }
        #endregion

        #region Delete
        private void DeleteCloth()
        {
            if (Request.QueryString["delId"] != null)
            {
                sh_cloth = new SH_Cloth();
                DataSet picDs = sh_cloth.GetDeleteClothPicture(Request.QueryString["delId"]);
                for (int i = 0; i < picDs.Tables[0].Rows.Count; i++)
                {
                    string picPath = Server.MapPath("../" + picDs.Tables[0].Rows[i][0].ToString());
                    if (File.Exists(picPath))
                    {
                        File.Delete(picPath);
                    }
                }
                DataSet clothDs = sh_cloth.GetDeleteClothId(Request.QueryString["delId"]);
                for (int i = 0; i < clothDs.Tables[0].Rows.Count; i++)
                {
                    sh_cloth.DeleteCloth(clothDs.Tables[0].Rows[i][0].ToString());
                }
                Response.Redirect("MngCloth.aspx");
            }
        }

        #endregion


        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCloth.aspx");
        }

        protected void rpCloth_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //pageDiv.Visible = false;

            if (string.IsNullOrEmpty(txtSearch.Value))
            {
                Msg = MessageTools.ShowMessage(0, "搜索框不能为空");
            }
            else
            {
                string conditions = string.Empty;
                if (rdBtnType.SelectedItem.Text == "名称")
                {
                    conditions = " AND ClothName LIKE N'%" + txtSearch.Value + "%' ";
                }
                else if (rdBtnType.SelectedItem.Text == "条码")
                {
                    conditions = " AND ClothGuid = (SELECT ClothGuid FROM TJW_Cloth WHERE StuffUGUID LIKE N'%" + txtSearch.Value + "%')";
                }
                GetClothList(conditions);
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("MngCloth.aspx");
        }

        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {
            GetClothList("");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
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