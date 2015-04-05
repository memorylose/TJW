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

namespace TJW.UI.CY
{
    public partial class CYDetail : System.Web.UI.Page
    {
        #region String

        protected string TeaName;
        protected string TeaPrice;
        protected string TeaYear;
        protected string TeaType;
        protected string TeaDes;
        protected string TopImage;
        protected StringBuilder pictures;
        private string urlParam;
        SH_Tea _shTea;
        SH_Common _shCommon;
        SH_Index _shIndex;
        SH_Login _shLogin;

        #endregion

        #region Bind Detail
        private void GetTeaDetail(string param)
        {
            _shTea = new SH_Tea();
            DataSet ds = _shTea.GetTeaDetailWord(param);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TeaName = ds.Tables[0].Rows[i]["TeaName"].ToString();
                TeaPrice = ds.Tables[0].Rows[i]["TeaPrice"].ToString();
                TeaYear = SetCommonTea(Convert.ToInt32(ds.Tables[0].Rows[i]["TeaYear"]));
                TeaType = ds.Tables[0].Rows[i]["TypeName"].ToString();
                TeaDes = ds.Tables[0].Rows[i]["TeaDescription"].ToString();
            }

            //get top image
            DataSet dsTopImage = _shTea.GetTeaDetailPicture(param, "2");
            if (dsTopImage.Tables[0].Rows.Count > 0)
            {
                TopImage = "<img src=\"/" + dsTopImage.Tables[0].Rows[0]["PicturePath"].ToString() + "\">";
            }

            //get pictures
            pictures = new StringBuilder();
            DataSet dsImages = _shTea.GetTeaDetailPicture(param, "3");
            if (dsImages.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < dsImages.Tables[0].Rows.Count; j++)
                {
                    pictures.Append("<img src=\"/" + dsImages.Tables[0].Rows[j]["PicturePath"].ToString() + "\">");
                }
            }
        }

        #endregion

        #region Set year
        private string SetCommonTea(int year)
        {
            return SH_Common.SetYear(year);
        }
        #endregion

        #region Get same picture
        protected string GetSamePicture()
        {
            TJW.HtmlOuts.Index _htmlIndex = new HtmlOuts.Index();
            return _htmlIndex.GetSuccessPicture();
        }
        #endregion

        #region Buy

        private void Buy()
        {
            _shCommon = new SH_Common();

            if (_shCommon.CheckSession(ConstValue.LoginSessionName))
            {
                if (Page.RouteData.Values.ContainsKey("TeaId"))
                {
                    urlParam = Page.RouteData.Values["TeaId"].ToString();
                }
                _shIndex = new SH_Index();
                _shLogin = new SH_Login();

                TJW.Model.Order _orderModel = new Model.Order();
                _orderModel.StuffInfo = urlParam + "," + carNum.Value;
                _orderModel.OrderNumber = _shCommon.GenerateOrderNumber(Convert.ToInt32(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString())));

                //未付款
                _orderModel.OrderStatusId = "1";
                _orderModel.CreateCartDate = DateTime.Now;
                _orderModel.CreateUserId = Convert.ToInt32(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString()));
                //在线付款
                _orderModel.PayTypeId = 1;
                _orderModel.AddressId = _shIndex.GetAddressId(Convert.ToInt32(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString())));

                _shIndex.AddOrderInCart(_orderModel);
                Response.Redirect("/Order.aspx?o=" + _orderModel.OrderNumber);
            }
            else
            {
                if (Page.RouteData.Values.ContainsKey("TeaId"))
                {
                    Response.Redirect("/Login.aspx?teaCart=" + Page.RouteData.Values["TeaId"].ToString());
                }
                else
                {
                    Response.Redirect("/Index.aspx");
                }
            }

        }

        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values.ContainsKey("TeaId"))
                {
                    urlParam = Page.RouteData.Values["TeaId"].ToString();
                    GetTeaDetail(urlParam);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Buy();
        }
    }
}