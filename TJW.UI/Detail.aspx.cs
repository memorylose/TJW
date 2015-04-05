using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI
{
    public partial class Detail : System.Web.UI.Page
    {
        SH_Cloth sh_cloth;
        SH_Common _shCommon;
        SH_Index _shIndex;
        SH_Login _shLogin;
        CommonTools _commonTools;

        protected string clothName;
        protected string clothPrice;
        protected string clothZkPrice;
        protected string ZkNum;
        protected StringBuilder sbColorName;
        protected StringBuilder sbSizeName;
        protected string topPicture;
        protected StringBuilder pictures;
        private string urlParam;
        protected string StoreCount;
        protected string pageTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values.ContainsKey("ClothGuid"))
                {
                    //check url parameters
                    urlParam = Page.RouteData.Values["ClothGuid"].ToString();
                    _commonTools = new CommonTools();
                    sh_cloth = new SH_Cloth();

                    if (_commonTools.CheckClothGuidNum(urlParam) && sh_cloth.CheckClothCount(urlParam))
                    {
                        BindCustomDp();
                        GetNameAndPrice();
                        GetColorSizeStoreCount();
                        GetPictures();
                        // CheckWomenStreet();
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

        #region Get name and price
        private void GetNameAndPrice()
        {
            sh_cloth = new SH_Cloth();
            DataSet ds = sh_cloth.GetClothDetailNamePrice(urlParam);
            clothName = ds.Tables[0].Rows[0][0].ToString();
            clothPrice = ds.Tables[0].Rows[0][1].ToString();
            clothZkPrice = clothPrice;
            if (ds.Tables[0].Rows[0][3].ToString() != "10")
            {
                zkDiv.Attributes.Add("style", "display:block");
                string zkPercent = "0.1";
                decimal zkCount = decimal.Parse(ds.Tables[0].Rows[0][3].ToString());
                decimal old = decimal.Parse(clothPrice);
                decimal zkTotal = zkCount * old;
                zkTotal = zkTotal * decimal.Parse(zkPercent);
                clothPrice = zkTotal.ToString();
                ZkNum = ds.Tables[0].Rows[0][3].ToString() + "折";
            }
            else
                zkDiv.Attributes.Add("style", "display:none");


            CommonTools _commonTools = new CommonTools();
            clothPrice = _commonTools.SaveTwoPosition(clothPrice);
            clothZkPrice = _commonTools.SaveTwoPosition(clothZkPrice);

            int s_count = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                s_count += Convert.ToInt32(ds.Tables[0].Rows[i][2]);
            }
            StoreCount = s_count.ToString();
            if (StoreCount == "0")
            {
                StoreCount = "<span style=\"color:red\">已售罄</span>";
            }
            pageTitle = clothName;
        }
        #endregion

        #region Get color and size
        protected void GetColorSizeStoreCount()
        {
            sbColorName = new StringBuilder();
            sbSizeName = new StringBuilder();

            int j = 0;
            sh_cloth = new SH_Cloth();
            List<Cloth> clothColorList = sh_cloth.GetClothDetailColor(urlParam);
            List<Cloth> clothSizeList = sh_cloth.GetClothDetailSize(urlParam);

            foreach (Cloth model in clothColorList)
            {
                j++;
                sbColorName.Append("<div class=\"detail_color\" id=\"colorId" + j + "\" onclick=\"ColorClick(this.id)\">" + model.ColorName + "</div>");
            }

            foreach (Cloth model in clothSizeList)
            {
                j++;
                sbSizeName.Append("<div class=\"detail_size\" id=\"sizeId" + j + "\" onclick=\"SizeClick(this.id)\">" + model.SizeName + "</div>");
            }

        }
        #endregion

        #region Get pictures
        protected void GetPictures()
        {
            sh_cloth = new SH_Cloth();
            //get top picture
            List<Picture> topPicList = sh_cloth.GetDetailPicture(urlParam, 7);
            foreach (Picture model in topPicList)
            {
                topPicture = "<img src=\"/" + model.PicturePath + "\" />";
            }

            //get detail picture
            pictures = new StringBuilder();
            List<Picture> picList = sh_cloth.GetDetailPicture(urlParam, 8);
            foreach (Picture model in picList)
            {
                pictures.Append("<img src=\"/" + model.PicturePath + "\" />");
            }
        }
        #endregion

        #region Buy

        private void Buy()
        {
            _shCommon = new SH_Common();

            if (_shCommon.CheckSession(ConstValue.LoginSessionName))
            {
                if (Page.RouteData.Values.ContainsKey("ClothGuid"))
                {
                    urlParam = Page.RouteData.Values["ClothGuid"].ToString();
                }
                _shIndex = new SH_Index();
                _shLogin = new SH_Login();
                TJW.Model.Order _orderModel = new Model.Order();

                DataSet ds = _shIndex.IsSetBh(urlParam);
                string bh = string.Empty;
                //check exist bh or not
                if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == 0)
                {
                    _orderModel.StuffInfo = _shIndex.GetClothUGUID(urlParam, hidColor.Value, hidSize.Value) + "," + carNum.Value;
                }
                else
                {
                    bh = "," + dpCustom.SelectedItem.Value;
                    _orderModel.StuffInfo = _shIndex.GetClothUGUID_BH(urlParam, Convert.ToInt32(dpCustom.SelectedItem.Value)) + "," + carNum.Value + bh;
                }
                _orderModel.OrderNumber = _shCommon.GenerateOrderNumber(Convert.ToInt32(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString())));

                //get stuffinfo failed
                if (_orderModel.StuffInfo == "-1")
                {
                    Response.Redirect("/Index.aspx");
                }

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
                Response.Redirect("/Login.aspx?cart=" + Page.RouteData.Values["ClothGuid"].ToString());
            }

        }

        #endregion

        #region Get custom dp
        private void BindCustomDp()
        {
            _shIndex = new SH_Index();
            string fatherId = "";
            if (_shIndex.IsCustomBH(urlParam, ref fatherId))
            {
                List<CustomBH> list = _shIndex.SetCustomBH(fatherId);
                dpCustom.DataSource = list;
                dpCustom.DataTextField = "CustomName";
                dpCustom.DataValueField = "CustomId";
                dpCustom.DataBind();

                customDiv.Attributes.Add("style", "display:block");
                colorDiv.Attributes.Add("style", "display:none");
                sizeDiv.Attributes.Add("style", "display:none");

                //for cart check
                hidCusFlag.Value = "1";
            }
        }
        #endregion

        #region Check women street

        private void CheckWomenStreet()
        {
            _shIndex = new SH_Index();
            if (_shIndex.CheckWomenStreet(urlParam))
            {
                colorDiv.Attributes.Add("style", "display:none");
                sizeDiv.Attributes.Add("style", "display:none");
            }
        }

        #endregion

        #region Get same picture
        protected string GetSamePicture()
        {
            TJW.HtmlOuts.Index _htmlIndex = new HtmlOuts.Index();
            return _htmlIndex.GetSamePicture(urlParam);
        }
        #endregion

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            Buy();
        }
    }
}