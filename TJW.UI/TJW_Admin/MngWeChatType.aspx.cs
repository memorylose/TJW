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
    public partial class MngWeChatType : System.Web.UI.Page
    {
        SH_WeChat _shWeChat;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FunDeleteMessageType();
                FunGetEditMessageType();
                FunGetMessageType();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["editId"] != null)
            {
                FunUpdateType();
            }
            else
            {
                FunAddType();
            }
        }

        #region Add type
        private void FunAddType()
        {
            _shWeChat = new SH_WeChat();
            _shWeChat.AddMessageType(txtTypeName.Value);
            Response.Redirect("MngWeChatType.aspx");
        }
        #endregion

        #region Update type
        private void FunUpdateType()
        {
            _shWeChat = new SH_WeChat();
            _shWeChat.UpdateMessageType(Request.QueryString["editId"], txtTypeName.Value);
            Response.Redirect("MngWeChatType.aspx");
        }
        #endregion

        #region Get message type
        private void FunGetMessageType()
        {
            _shWeChat = new SH_WeChat();
            rpType.DataSource = _shWeChat.GetMessageType();
            rpType.DataBind();
        }
        #endregion

        #region Delete message type
        private void FunDeleteMessageType()
        {
            if (Request.QueryString["delId"] != null)
            {
                _shWeChat = new SH_WeChat();
                _shWeChat.DeleteMessageType(Request.QueryString["delId"]);
                Response.Redirect("MngWeChatType.aspx");
            }
        }
        #endregion

        #region Get edit message type
        private void FunGetEditMessageType()
        {
            if (Request.QueryString["editId"] != null)
            {
                _shWeChat = new SH_WeChat();
                DataSet ds = _shWeChat.GetEditMessageType(Request.QueryString["editId"]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTypeName.Value = ds.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                    Response.Redirect("MngWeChatType.aspx"); 
                }
            }
        }
        #endregion
    }
}