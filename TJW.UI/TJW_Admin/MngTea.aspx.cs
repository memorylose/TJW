using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;

namespace TJW.UI.TJW_Admin
{
    public partial class MngTea : System.Web.UI.Page
    {
        SH_Tea _shTea;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FunDeleteTea();
                FunGetTea();
            }
        }

        #region Get tea
        private void FunGetTea()
        {
            _shTea = new SH_Tea();
            rpTea.DataSource = _shTea.GetTea();
            rpTea.DataBind();
        }
        #endregion

        #region Set tea year
        protected string SetCommonTea(int year)
        {
            return SH_Common.SetYear(year);
        }
        #endregion

        #region Delete tea
        private void FunDeleteTea()
        {
            if (Request.QueryString["delId"] != null)
            {
                _shTea = new SH_Tea();
                DataSet ds = _shTea.GetDeletePath(Request.QueryString["delId"].ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string picPath = Server.MapPath("../" + ds.Tables[0].Rows[i][0].ToString());
                    if (File.Exists(picPath))
                    {
                        try
                        {
                            //delete picture file
                            File.Delete(picPath);
                            //delete picture data
                            _shTea.DeleteTeaPicture(ds.Tables[0].Rows[i][1].ToString());
                        }
                        catch
                        { }
                    }
                }
                //delete tea data
                _shTea.DeleteTea(Request.QueryString["delId"].ToString());
                //delete GUID folder

                Response.Redirect("MngTea.aspx");
            }
        }
        #endregion
    }
}