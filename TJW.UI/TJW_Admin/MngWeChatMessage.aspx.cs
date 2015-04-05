using PagerHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.TJW_Admin
{
    public partial class MngWeChatMessage : System.Web.UI.Page
    {
        SH_WeChat _shWeChat;
        CommonTools cTools;
        private int PageSize = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DeleteWeChatMessage();
                GetData();
            }
        }

        #region Get data
        private void GetData()
        {
            rpWeChat.DataSource = SetPagerDs();
            rpWeChat.DataBind();
        }

        private DataSet SetPagerDs()
        {
            _shWeChat = new SH_WeChat();
            PagerUtil pUtil = new PagerUtil();
            ConstValue cValue = new ConstValue();
            return pUtil.ShowData(_shWeChat.WeChatPagerBasicSql(), PageSize, cValue.pageParams, SqlConnections.RtnConecs());
        }

        #endregion

        #region Delete wechat message
        private void DeleteWeChatMessage()
        {
            if (Request.QueryString["delId"] != null)
            {
                _shWeChat = new SH_WeChat();
                DataSet ds = _shWeChat.GetEditMessage(Request.QueryString["delId"]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //delete contents image
                    string contents = ds.Tables[0].Rows[0][3].ToString();
                    cTools = new CommonTools();
                    string[] htmlStr = cTools.GetHtmlImageUrlList(contents);
                    foreach (string s in htmlStr)
                    {
                        int countWeChatImages = s.LastIndexOf("WeChatImages");
                        string contentsPath = Server.MapPath("~/" + s.Substring(countWeChatImages));
                        if (File.Exists(contentsPath))
                        {
                            File.Delete(contentsPath);
                        }
                    }
                    //delete images
                    string imgPath = Server.MapPath("~/" + ds.Tables[0].Rows[0][4].ToString());
                    if (File.Exists(imgPath))
                    {
                        File.Delete(imgPath);
                    }

                    _shWeChat.DeleteMessage(Request.QueryString["delId"]);
                }
                Response.Redirect("MngWeChatMessage.aspx");
            }
        }
        #endregion

    }
}