using PagerHelper;
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
    public partial class List : System.Web.UI.Page
    {
        private int PageSize = 25;
        SH_Index _shIndex;
        private string urlParam;
        private string urlPager;
        private int tableCount;
        protected string pageTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values.ContainsKey("ListId"))
                {
                    urlParam = Page.RouteData.Values["ListId"].ToString();
                    if (Page.RouteData.Values.ContainsKey("Pager"))
                    {
                        urlPager = Page.RouteData.Values["Pager"].ToString();
                    }

                    if (string.Equals(urlParam, "2") || string.Equals(urlParam, "3") || string.Equals(urlParam, "5") || string.Equals(urlParam, "17") || string.Equals(urlParam, "18"))
                    {
                        SetTitle(urlParam);
                        GetList(" WHERE PictureTypeId IN (" + urlParam + ") ", "");
                    }
                    else
                    {
                        CommonTools _tools = new CommonTools();
                        urlParam = _tools.CheckString(urlParam);

                        SetTitle("天街网-搜索结果");
                        GetList(" WHERE (SELECT TOP 1 ClothName FROM TJW_Cloth B WHERE B.ClothGuid = A.ClothGUID) LIKE N'%" + urlParam + "%' ", "");
                    }
                }
                else
                {
                    Response.Redirect("/Index.aspx");
                }
            }
        }

        #region Get List
        private void GetList(string showNum, string where)
        {
            _shIndex = new SH_Index();
            DataSet ds = SetPagerDs(showNum, where);
            rpList.DataSource = SetPagerDs(showNum, where);
            rpList.DataBind();
        }
        #endregion

        #region Set pager
        private DataSet SetPagerDs(string showNum, string where)
        {
            _shIndex = new SH_Index();
            PagerUtil pUtil = new PagerUtil();
            ConstValue cValue = new ConstValue();
            tableCount = Convert.ToInt32(_shIndex.ListPagerBasicSql(showNum, true, where));
            return pUtil.ShowData(_shIndex.ListPagerBasicSql(showNum, false, where), PageSize, urlPager, SqlConnections.RtnConecs());
        }
        #endregion

        #region Set pager html
        protected string PagerHtml()
        {
            ConstValue cValue = new ConstValue();
            bool isFirstParams = true;
            string basePageUrl = "/List/" + urlParam;

            PagerUtil pUtil = new PagerUtil();
            string test = pUtil.HtmlPager(cValue.pnAbleClass, cValue.pnDisAbleClass, cValue.pagerNumberClass, cValue.currentpagerNumberClass, tableCount, PageSize, isFirstParams, basePageUrl, urlPager, SqlConnections.RtnConecs());
            return pUtil.HtmlPager(cValue.pnAbleClass, cValue.pnDisAbleClass, cValue.pagerNumberClass, cValue.currentpagerNumberClass, tableCount, PageSize, isFirstParams, basePageUrl, urlPager, SqlConnections.RtnConecs());
        }
        #endregion

        #region Set title
        private void SetTitle(string urlParam)
        {

            switch (urlParam)
            {
                case "2":
                    pageTitle = "天街网-时尚街";
                    break;
                case "3":
                    pageTitle = "天街网-单品街";
                    break;
                case "5":
                    pageTitle = "天街网-配饰街";
                    break;
                case "17":
                    pageTitle = "天街网-女人街";
                    break;
            }
        }
        #endregion
    }
}