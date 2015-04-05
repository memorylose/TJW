using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI
{
    public partial class Index : System.Web.UI.Page
    {
        TJW.HtmlOuts.Index htmlOut;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected string StrBelowTurningImage()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.BelowTurningImage();
        }

        protected string StrTurningImage()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.TurningImage();
        }

        protected string StrAuthorSuggest()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.AuthorSuggest();
        }

        protected string StrCurrentSeasonTop()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.CurrentSeasonTop();
        }
        protected string StrCurrentSeason()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.CurrentSeason();
        }
        protected string StrSingleProduct()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.SingleProduct();
        }
        protected string StrSaleProductTop()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.SaleProductTop();
        }
        protected string StrSaleProduct(int saleType)
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.SaleProduct(saleType);
        }
        protected string StrHotSuggestTop()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.HotSuggestTop();
        }
        protected string StrHotSuggest()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.HotSuggest();
        }
        protected string StrHotProductTop()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.HotProductTop();
        }
        protected string StrHotProduct()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.HotProduct();
        }
        protected string StrNews()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.News();
        }
        protected string StrWomenStreet()
        {
            htmlOut = new HtmlOuts.Index();
            return htmlOut.WomenStreet();
        }
    }
}