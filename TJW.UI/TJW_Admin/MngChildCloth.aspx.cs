using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.TJW_Admin
{
    public partial class MngChildCloth : BasePage
    {
        SH_Cloth sh_cloth;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DeleteCloth();
                FunGetChildCloth();

            }
        }

        #region Get child cloth
        private void FunGetChildCloth()
        {
            if (Request.QueryString["cCloth"] != null)
            {
                sh_cloth = new SH_Cloth();
                rpCloth.DataSource = sh_cloth.GetChildCloth(Request.QueryString["cCloth"]);
                rpCloth.DataBind();
            }
            else
            {
                Response.Redirect("MngCloth.aspx");
            }

        }
        #endregion

        #region Check store count
        protected string CheckStoreCount(int storeCount)
        {
            //if store is 3
            if (storeCount < ConstValue.StoreCount)
            {
                return "<span style=\"color:red;font-weight:bold;\">" + storeCount + "</span>";
            }
            else
            {
                return storeCount.ToString();
            }
        }
        #endregion

        #region RtnStatus
        protected string RtnStatus(bool status)
        {
            if (status)
            {
                return "已上架";
            }
            else
            {
                return "已下架";
            }
        }
        #endregion

        #region Delete

        private void DeleteCloth()
        {
            if (Request.QueryString["delId"] != null)
            {
                sh_cloth = new SH_Cloth();
                DataSet ds = sh_cloth.GetChildDeletePath(Request.QueryString["delId"]);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string picPath = Server.MapPath("../" + ds.Tables[0].Rows[i][0].ToString());
                    if (File.Exists(picPath))
                    {
                        File.Delete(picPath);
                    }
                }
                sh_cloth.DeleteCloth(Request.QueryString["delId"]);
                Response.Redirect("MngChildCloth.aspx?cCloth=" + Request.QueryString["cCloth"]);
            }
        }


        #endregion

        #region Set zhekou

        protected string SetZheKou(string zk)
        {
            string result = "";
            if (zk == "10")
                result = "原价";
            else
                result = zk + "折";
            return result;
        }

        #endregion
    }
}