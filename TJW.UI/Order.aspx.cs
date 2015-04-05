using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Bank;
using TJW.HtmlOuts;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI
{
    public partial class Order : System.Web.UI.Page
    {
        SH_Index _shIndex;
        SH_Common _shCommon;
        SH_Login _shLogin;
        UserMng _userMng;
        private bool IsAddress = false;

        #region Front display

        protected string orderNumber;
        protected string orderStatus;
        protected string payType;

        protected string addressPeople;
        protected string address;
        protected string postCode;
        protected string tel;

        protected decimal myTotal = 0;
        protected string strTotal;
        protected string strStuff;

        protected int grade;
        protected string vipName;
        protected string bankS;

        #endregion

        #region Bank

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetOrderPage();
            }
        }

        #region Set order page

        private void SetOrderPage()
        {
            if (Request.QueryString["o"] != null)
            {
                _shCommon = new SH_Common();
                if (_shCommon.CheckSession(ConstValue.LoginSessionName))
                {
                    //set grade
                    string bank = "1";
                    SetGrade(ref bank);
                    if (bank != "1")
                    {
                        zkId.Attributes.Add("style", "display:block");
                        bankS = bank.Substring(2);
                    }

                    _shIndex = new SH_Index();
                    _shLogin = new SH_Login();

                    DataSet ds = _shIndex.GetOrder(Request.QueryString["o"].ToString());
                    orderNumber = ds.Tables[0].Rows[0][0].ToString();

                    ViewState["orderNumber"] = orderNumber;

                    //orderStatus = ds.Tables[0].Rows[0][3].ToString();
                    payType = ds.Tables[0].Rows[0][2].ToString();
                    DataSet addressDs = _shIndex.GetOrderAddress(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString()));
                    if (addressDs.Tables[0].Rows.Count > 0)
                    {
                        addressPeople = addressDs.Tables[0].Rows[0][1].ToString();
                        address = addressDs.Tables[0].Rows[0][0].ToString();
                        postCode = addressDs.Tables[0].Rows[0][2].ToString();
                        tel = addressDs.Tables[0].Rows[0][3].ToString();

                        Session["IsAddress"] = "1";
                    }
                    else
                    {
                        addressDid.Attributes.Add("style", "display:none");
                    }

                    //string[] stuff = ds.Tables[0].Rows[0][1].ToString().Split('/');

                    //set html
                    _userMng = new UserMng();
                    strStuff = _userMng.HtmlOrder(ds.Tables[0].Rows[0][1].ToString(), ds.Tables[0].Rows[0][3].ToString(), ref myTotal);

                    CommonTools _commonTools = new CommonTools();
                    myTotal = myTotal * decimal.Parse(bank);

                    string yf = "10.00";
                    strTotal = _commonTools.SaveTwoPosition(myTotal.ToString());

                    decimal dciTotal = decimal.Parse(strTotal);

                    if (dciTotal < 100)
                    {
                        yfDiv.Attributes.Add("style", "display:block");
                        dciTotal = Decimal.Add(dciTotal, decimal.Parse(yf));
                    }
                    else
                        yfDiv.Attributes.Add("style", "display:none");

                    strTotal = dciTotal.ToString();
                    ViewState["myTotal"] = strTotal;

                }
                else
                {
                    Response.Redirect("Login.aspx?o=" + Request.QueryString["o"]);
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        #endregion

        #region Set grade
        private void SetGrade(ref string bank)
        {
            string userName = Session[ConstValue.LoginSessionName].ToString();
            SH_UserManagement _userMng = new SH_UserManagement();
            _shLogin = new SH_Login();
            _shCommon = new SH_Common();

            string userId = _shLogin.GetUserId(userName);
            DataSet ds = _userMng.GetCurrentGrade(userId);
            vipName = _shCommon.GetVipType(Convert.ToInt32(ds.Tables[0].Rows[0][0]), ref bank);
        }
        #endregion

        #region Send

        private void Send()
        {

            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //支付类型,必填，不能修改
            string payment_type = "1";

            //服务器异步通知页面路径,需http://格式的完整路径，不能加?id=123这类自定义参数  
            string notify_url = "http://www.tianjiew.com/AlipayPage/notify_url.aspx";

            //页面跳转同步通知页面路径,需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/  
            string return_url = "http://www.tianjiew.com/AlipayPage/return_url.aspx";

            //卖家支付宝帐户  
            string seller_email = "tjw@tianjiew.com";

            //商户网站订单系统中唯一订单号
            string out_trade_no = ViewState["orderNumber"].ToString();

            //订单名称
            string subject = "天街网的订单";

            //付款金额
            string total_fee = ViewState["myTotal"].ToString();

            //订单描述        
            string body = "天街网的订单";

            //商品展示地址,需以http://开头的完整路径
            string show_url = "http://www.tianjiew.com";

            //防钓鱼时间戳,若要使用请调用类文件submit中的query_timestamp函数
            string anti_phishing_key = "";

            //客户端的IP地址,非局域网的外网IP地址，如：221.0.0.1
            string exter_invoke_ip = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////


            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("seller_email", seller_email);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");

            Response.Write(sHtmlText);

        }

        #endregion

        #region Add NetIncome

        private void AddIncome()
        {
            _shIndex = new SH_Index();
            NetIncome _model = new NetIncome();

            _model.OrderNumber = ViewState["orderNumber"].ToString();
            _model.Price = Convert.ToDouble(ViewState["myTotal"].ToString());
            _model.IsSuc = false;
            _model.Grade = 0;
            _model.IncomeTime = DateTime.Now;
            _model.TradeNumber = "";

            _shIndex.AddNetIncome(_model);
        }

        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["IsAddress"] != null)
            {
                if (Session["IsAddress"].ToString() == "1")
                {
                    AddIncome();

                    Send();
                }
                else
                {
                    Response.Redirect("/Order.aspx?o=" + Request.QueryString["o"]);
                }
            }
            else
            {
                Response.Redirect("/Order.aspx?o=" + Request.QueryString["o"]);
            }
        }
    }
}