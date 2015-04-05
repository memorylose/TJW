using PagerHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI
{
    public partial class NewsList : System.Web.UI.Page
    {
        SH_News _shNews;
        private int PageSize = 10;
        private string flagPagerShow = "1";

        private string urlParam;
        private string urlPager;
        private int tableCount;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values.ContainsKey("Pager"))
                {
                    urlPager = Page.RouteData.Values["Pager"].ToString();
                }

                rpNews.DataSource = SetPagerDs();
                rpNews.DataBind();

                ////set pager show
                //if (SetPagerDs().Tables[0].Rows.Count > PageSize)
                //{
                //    flagPagerShow = "1";
                //}
            }
        }

        #region Set pager
        private DataSet SetPagerDs()
        {
            _shNews = new SH_News();
            PagerUtil pUtil = new PagerUtil();
            ConstValue cValue = new ConstValue();

            tableCount = Convert.ToInt32(_shNews.GetNewsPager(true));
            return pUtil.ShowData(_shNews.GetNewsPager(false), PageSize, urlPager, SqlConnections.RtnConecs());
        }
        #endregion

        #region Set pager html
        protected string PagerHtml()
        {
            if (string.Equals(flagPagerShow, "1"))
            {
                ConstValue cValue = new ConstValue();
                bool isFirstParams = true;
                string basePageUrl = "/NewsList";

                PagerUtil pUtil = new PagerUtil();
                StringBuilder strBuilder = new StringBuilder();

                strBuilder.Append("<div class=\"newsList_page\">");
                strBuilder.Append(pUtil.HtmlPager(cValue.pnAbleClass, cValue.pnDisAbleClass, cValue.pagerNumberClass, cValue.currentpagerNumberClass, tableCount, PageSize, isFirstParams, basePageUrl, urlPager, SqlConnections.RtnConecs()));
                strBuilder.Append("</div>");
                return strBuilder.ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

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