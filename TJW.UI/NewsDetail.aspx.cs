using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI
{
    public partial class NewsDetail : System.Web.UI.Page
    {
        protected string Title;
        protected string SubTitles;
        protected string Time;
        protected string Contents;
        private string urlParam;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values.ContainsKey("NewsId"))
                {
                    urlParam = Page.RouteData.Values["NewsId"].ToString();
                    CommonTools _commonTools = new CommonTools();
                    SH_News _shNews = new SH_News();

                    if (_commonTools.CheckNumber(urlParam))
                    {
                        DataSet ds = _shNews.GetNewsDetail(urlParam);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Title = ds.Tables[0].Rows[0][0].ToString();
                            SubTitles = ds.Tables[0].Rows[0][1].ToString();
                            Time = Convert.ToDateTime(ds.Tables[0].Rows[0][3]).ToString("yyyy年MM月dd日 hh:mm");
                            Contents = ds.Tables[0].Rows[0][2].ToString();
                        }
                        else
                        {
                            Response.Redirect("/Index.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("/Index.aspx");
                }
            }
        }

        #region News TJ Html

        protected string NewsTj()
        {
            TJW.HtmlOuts.Index _indexHtml = new HtmlOuts.Index();
            return _indexHtml.NewsTj();
        }

        protected string NewsHot()
        {
            TJW.HtmlOuts.Index _indexHtml = new HtmlOuts.Index();
            return _indexHtml.NewsHot();
        }
        #endregion
    }
}