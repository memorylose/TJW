using PagerHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.HtmlOuts;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.CY
{
    public partial class Index : System.Web.UI.Page
    {
        TeaOuts _teaOuts;
        private int PageSize = 20;
        private int tableCount;
        private string urlPager;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values.ContainsKey("Pager"))
                {
                    urlPager = Page.RouteData.Values["Pager"].ToString();
                }

                BindTea();
            }
        }

        #region Bind HTML
        private void BindTea()
        {
            _teaOuts = new TeaOuts();
            rpTea.DataSource = SetPagerDs();
            rpTea.DataBind();

            if (SetPagerDs().Tables[0].Rows.Count > PageSize)
            {
                pageId.Attributes.Add("style", "display:block");
            }
        }
        #endregion

        #region Set pager
        private DataSet SetPagerDs()
        {
            TeaOuts _teaOuts = new TeaOuts();
            PagerUtil pUtil = new PagerUtil();
            ConstValue cValue = new ConstValue();
            tableCount = Convert.ToInt32(_teaOuts.ShowTeaWithPage(true));
            return pUtil.ShowData(_teaOuts.ShowTeaWithPage(false), PageSize, urlPager, SqlConnections.RtnConecs());
        }
        #endregion

        #region Set pager html
        protected string PagerHtml()
        {
            ConstValue cValue = new ConstValue();
            bool isFirstParams = true;
            string basePageUrl = "/Tea";

            PagerUtil pUtil = new PagerUtil();
            return pUtil.HtmlPager(cValue.pnAbleClass, cValue.pnDisAbleClass, cValue.pagerNumberClass, cValue.currentpagerNumberClass, tableCount, PageSize, isFirstParams, basePageUrl, urlPager, SqlConnections.RtnConecs());
        }
        #endregion
    }
}