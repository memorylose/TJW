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
    public partial class MngPageShow : System.Web.UI.Page
    {
        SH_Cloth sh_cloth;
        Show showModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetShowList();
                GetShowEdit();
                DeleteShow();
            }
        }



        #region Add show
        private void AddShow()
        {
            showModel = new Show();
            showModel.ShowName = txtShowName.Value;

            sh_cloth = new SH_Cloth();
            sh_cloth.AddShowNumber(showModel);
            Response.Redirect("MngPageShow.aspx");
        }
        #endregion

        #region Edit show
        public void EditShow()
        {
            if (Request.QueryString["editId"] != null)
            {
                sh_cloth = new SH_Cloth();
                sh_cloth.EditShowNum(Convert.ToInt32(Request.QueryString["editId"]), txtShowName.Value);
                Response.Redirect("MngPageShow.aspx");
            }
        }
        #endregion

        #region Delete show
        private void DeleteShow()
        {
            if (Request.QueryString["delId"] != null)
            {
                sh_cloth = new SH_Cloth();
                sh_cloth.DeteleShowNumber(Convert.ToInt32(Request.QueryString["delId"]));
                Response.Redirect("MngPageShow.aspx");
            }
        }
        #endregion

        #region Get show edit
        private void GetShowEdit()
        {
            if (Request.QueryString["editId"] != null)
            {
                sh_cloth = new SH_Cloth();
                List<Show> showList = sh_cloth.GetShowEdit(Convert.ToInt32(Request.QueryString["editId"]));
                foreach (Show model in showList)
                {
                    txtShowName.Value = model.ShowName;
                }
                addFId.Attributes.Add("style", "display:block");
            }
        }
        #endregion

        #region Get size
        private void GetShowList()
        {
            sh_cloth = new SH_Cloth();

            rpShow.DataSource = sh_cloth.GetShowNumber();
            rpShow.DataBind();
        }
        #endregion

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["editId"] != null)
            {
                EditShow();
            }
            else
            {
                AddShow();
            }
        }
    }
}