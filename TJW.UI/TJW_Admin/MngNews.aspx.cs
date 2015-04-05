using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;

namespace TJW.UI.TJW_Admin
{
    public partial class MngNews : System.Web.UI.Page
    {
        SH_News _shNews;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FunGetNews();
                FunDeleteNews();
            }
        }

        #region Get news

        private void FunGetNews()
        {
            _shNews = new SH_News();
            rpNews.DataSource = _shNews.GetNews();
            rpNews.DataBind();
        }
        #endregion

        #region Set Location
        protected string SetLocation(string IsStr)
        {
            string result = string.Empty;
            if (IsStr.Substring(2, 1) == "1")
            {
                result += "首页推荐/";
            }
            if (IsStr.Substring(0, 1) == "1")
            {
                result += "新闻推荐/";
            }
            if (IsStr.Substring(1, 1) == "1")
            {
                result += "热点新闻/";
            }

            int length = result.Length;
            if (length != 0)
            {
                if (result.Substring(length - 1, 1) == "/")
                {
                    result = result.Substring(0, length - 1);
                }
                return result;
            }
            else
            {
                return "";
            }

        }
        #endregion

        #region Delete news
        private void FunDeleteNews()
        {
            if (Request.QueryString["delId"] != null)
            {
                _shNews = new SH_News();

                string imgPath = Server.MapPath("~/" + _shNews.GetPicturePath(Request.QueryString["delId"]));
                if (File.Exists(imgPath))
                {
                    File.Delete(imgPath);
                }
                _shNews.DeleteNews(Request.QueryString["delId"]);
                Response.Redirect("MngNews.aspx");
            }
        }
        #endregion
    }
}