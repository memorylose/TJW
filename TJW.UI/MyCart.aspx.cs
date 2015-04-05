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
using TJW.Model;

namespace TJW.UI
{
    public partial class MyCart : System.Web.UI.Page
    {
        SH_Common _shCommon;
        SH_Login _shLogin;
        SH_Index _shIndex;
        public string AllPrice;
        public string AllCount;
        public StringBuilder cartBuilder;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _shCommon = new SH_Common();
                if (_shCommon.CheckSession(ConstValue.LoginSessionName))
                {
                    DeleteMyCart();
                    GetCart();
                }
                else
                {
                    Response.Redirect("Login.aspx?mycart=1");
                }

            }
        }

        #region Get cart
        private void GetCart()
        {
            _shLogin = new SH_Login();
            _shIndex = new SH_Index();
            string userName = Session[ConstValue.LoginSessionName].ToString();
            DataSet ds = _shIndex.GetCart(_shLogin.GetUserId(userName));
            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal totalPrice = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    totalPrice += decimal.Parse(ds.Tables[0].Rows[i]["TotalPrice"].ToString());
                }
                AllCount = ds.Tables[0].Rows.Count.ToString();
                AllPrice = totalPrice.ToString();
            }
            //if cart empty,show something
            rpCart.DataSource = ds;
            rpCart.DataBind();
        }
        #endregion

        #region Delete cart
        private void DeleteMyCart()
        {
            if (Request.QueryString["cartId"] != null)
            {
                _shIndex = new SH_Index();
                _shIndex.DeleteCart(Request.QueryString["cartId"].ToString());
                Response.Redirect("MyCart.aspx");
            }
        }
        #endregion

        #region Set custom BH
        protected string SetCustomBH(string bh, string type)
        {
            string[] split = bh.Split(',');
            string result = split[0].ToString();
            if (split.Length > 1)
            {
                if (split[1].ToString() != "0")
                {
                    if (type == "0")
                    {
                        result = "";
                    }
                    else if (type == "1")
                    {
                        _shIndex = new SH_Index();
                        result = "编号：" + _shIndex.GetCartBH(split[1].ToString());
                    }
                }
            }
            return result;
        }
        #endregion

        #region Set Cart href

        protected string SetCartHref(string stuffGuid)
        {
            string href = "";
            switch (stuffGuid.Substring(0, 1))
            {
                case "C":
                    _shIndex = new SH_Index();
                    href = "/Detail/" + _shIndex.GetClothGuid(stuffGuid);
                    break;
                case "T":
                    href = "/Tea/D/" + stuffGuid;
                    break;
            }
            return href;
        }

        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {


            _shCommon = new SH_Common();
            if (_shCommon.CheckSession(ConstValue.LoginSessionName))
            {
                //get stuffinfo
                _shIndex = new SH_Index();
                _shLogin = new SH_Login();
                StringBuilder strStuffInfo = new StringBuilder();
                TJW.Model.Order _orderModel = new Model.Order();
                string status = "";

                DataSet ds = _shIndex.GetCartInOrder(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString()));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //set bh
                    string cusBH = "";
                    if (ds.Tables[0].Rows[i][3].ToString() != "0")
                    {
                        cusBH = "," + ds.Tables[0].Rows[i][3].ToString();
                    }
                    string lastStr = "/";
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        lastStr = "";
                    }

                    string[] countSplit = hdCount.Value.Split(',');
                    CommonTools _commonTool = new CommonTools();
                    if (!_commonTool.CheckNumber(countSplit[i]))
                    {
                        countSplit[i] = "1";
                    }

                    strStuffInfo.Append(ds.Tables[0].Rows[i][0].ToString() + "," + countSplit[i] + cusBH + lastStr);

                    //set order status
                    string statusSplit = ",";
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        statusSplit = "";
                    }
                    status += "1" + statusSplit;
                }


                _orderModel.OrderNumber = _shCommon.GenerateOrderNumber(Convert.ToInt32(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString())));
                _orderModel.StuffInfo = strStuffInfo.ToString();
                //未付款
                _orderModel.OrderStatusId = status;
                _orderModel.CreateCartDate = DateTime.Now;
                _orderModel.CreateUserId = Convert.ToInt32(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString()));
                //在线付款
                _orderModel.PayTypeId = 1;
                _orderModel.AddressId = _shIndex.GetAddressId(Convert.ToInt32(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString())));

                _shIndex.AddOrderInCart(_orderModel);

                //delete cart
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    _shIndex.DeleteCart(ds.Tables[0].Rows[i][2].ToString());
                }

                Response.Redirect("Order.aspx?o=" + _orderModel.OrderNumber);
            }
            else
            {
                Response.Redirect("Login.aspx?mycart=1");
            }
        }
    }
}