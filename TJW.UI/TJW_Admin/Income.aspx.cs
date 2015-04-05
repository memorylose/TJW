using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;

namespace TJW.UI.TJW_Admin
{
    public partial class Income : System.Web.UI.Page
    {
        protected string SendStr = "发货";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetIncome();
                BindDetail();
                S_Stuff();
            }
        }

        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {
            GetIncome();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
        }

        private void GetIncome()
        {
            SH_Cloth _shCloth = new SH_Cloth();
            GridView1.DataSource = _shCloth.GetNetIncome();
            GridView1.DataBind();
        }

        protected string SetStatus(string status)
        {
            int j = 0;
            string[] split = status.Split(',');
            foreach (string s in split)
            {
                if (s == "1")
                {
                    j++;
                }
            }
            if (j == 0)
            {
                return "已发货";
            }
            else
            {
                return "您有<span style=\"color:red\">" + j + "</span>件物品未发货";
            }
            
        }

        private void BindDetail()
        {
            if (Request.QueryString["detail"] != null)
            {
                detailId.Attributes.Add("style", "display:block");
                SH_AdminUser _admin = new SH_AdminUser();
                DataSet ds = _admin.GetIncomeAddress(Request.QueryString["detail"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblOrder.InnerText = Request.QueryString["detail"].ToString();
                    lblName.InnerText = ds.Tables[0].Rows[0]["UserName"].ToString();
                    lblAddress.InnerText = ds.Tables[0].Rows[0]["UserAddress"].ToString();
                    lblCode.InnerText = ds.Tables[0].Rows[0]["UserCode"].ToString();
                    lblTel.InnerText = ds.Tables[0].Rows[0]["UserTel"].ToString();
                }

                //bind detail
                GridView2.DataSource = _admin.GetIncomeDetail(Request.QueryString["detail"].ToString());
                GridView2.DataBind();
            }
        }

        private void S_Stuff()
        {
            if (Request.QueryString["stuff"] != null && Request.QueryString["detail"] != null)
            {
                SH_AdminUser _admin = new SH_AdminUser();
                DataSet ds = _admin.GetStuffAndStatus(Request.QueryString["detail"].ToString());
                string stuffInfo = ds.Tables[0].Rows[0][0].ToString();
                string statusInfo = ds.Tables[0].Rows[0][1].ToString();

                int k = -1;
                string status = "";
                string dot = ",";

                string[] stuffSplit = stuffInfo.Split('/');
                string[] statusSplit = statusInfo.Split(',');

                foreach (string s in stuffSplit)
                {
                    k++;
                    string[] sSplit = s.Split(',');

                    //check the last row
                    if (k == stuffSplit.Length - 1)
                    {
                        dot = "";
                    }

                    //check the current row which need to send
                    if (sSplit[0] == Request.QueryString["stuff"].ToString())
                    {
                        status += "5" + dot;
                    }
                    else
                    {
                        status += statusSplit[k] + dot;
                    }     
                }

                _admin.ChangeOrderStatus(Request.QueryString["detail"].ToString(), status);
                Response.Redirect("Income.aspx"); 
            }
        }

        protected string SendStuff(string stuffGuid)
        {
            string url = "Income.aspx?detail=" + Request.QueryString["detail"] + "&stuff=" + stuffGuid;
            return url;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["detail"] != null)
            {
                SH_AdminUser _admin = new SH_AdminUser();
                _admin.ChangeOrderStatus(Request.QueryString["detail"].ToString(), "");
                Response.Redirect("Income.aspx");
            }
        }
    }
}