using PagerHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.TJW_Admin
{
    public partial class AddWeChat : BasePage
    {
        public string Msg;
        SH_WeChat _shWeChat;
        SH_Common _shCommon;
        private string rtnPath;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindType();
                FunGetEditMessage();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CheckEmpty())
            {
                WeChatMessage model = new WeChatMessage();
                CommonTools _commonTools = new CommonTools();
                _shWeChat = new SH_WeChat();

                model.Title = txtTitle.Value;
                model.Describe = txtDescribe.Value;
                model.Contents = txtContent.Value;
                model.TypeId = Convert.ToInt32(dpType.SelectedItem.Value);

                if (Request.QueryString["editId"] != null)
                {
                    model.Id = Convert.ToInt32(Request.QueryString["editId"]);
                    if (fileHidden.Value != "")
                    {
                        if (UploadImg() == 1)
                        {
                            //delete old picture
                            DataSet ds = _shWeChat.GetEditMessage(Request.QueryString["editId"]);
                            string imgPath = Server.MapPath("~/" + ds.Tables[0].Rows[0][4].ToString());
                            if (File.Exists(imgPath))
                            {
                                File.Delete(imgPath);
                            }

                            model.PicturePath = rtnPath;
                            _shWeChat.UpdateMessageWithImage(model);
                            Response.Redirect("MngWeChatMessage.aspx");
                        }
                     
                    }
                    else
                    {
                        _shWeChat.UpdateMessage(model);
                        Response.Redirect("MngWeChatMessage.aspx");
                    }
                }
                else
                { 
                    
                    if (UploadImg() == 1)
                    {
                        model.CreateUserId = LoginUser.UserId;
                        model.CreateDate = DateTime.Now;
                        model.PicturePath = rtnPath;
                        _shWeChat.AddMessage(model);
                        Response.Redirect("MngWeChatMessage.aspx");
                    }
                }
            }
        }

        #region Upload image
        private int UploadImg()
        {
            CommonTools _commonTools = new CommonTools();
            int uploadResult = _commonTools.RtnFileName(fileHidden, ref rtnPath);
            if (uploadResult == -1)
            {
                Msg = MessageTools.ShowMessage(0, "上传图片格式错误");
            }
            else if (uploadResult == -2)
            {
                Msg = MessageTools.ShowMessage(0, "创建目录失败");
            }
            return uploadResult;
        }

        #endregion

        #region Bind type
        private void BindType()
        {
            _shWeChat = new SH_WeChat();
            List<WeChatType> list = _shWeChat.GetWeChatType();
            dpType.DataSource = list;
            dpType.DataTextField = "TypeName";
            dpType.DataValueField = "Id";
            dpType.DataBind();
        }
        #endregion

        #region Check empty
        private bool CheckEmpty()
        {
            bool result = false;
            if (string.IsNullOrEmpty(txtTitle.Value))
            {
                Msg = MessageTools.ShowMessage(0, "标题不能为空");
            }
            else if (string.IsNullOrEmpty(txtContent.Value))
            {
                Msg = MessageTools.ShowMessage(0, "内容不能为空");
            }
            else
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region Get edit mesage
        private void FunGetEditMessage()
        {
            if (Request.QueryString["editId"] != null)
            {
                DataSet ds = _shWeChat.GetEditMessage(Request.QueryString["editId"]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _shCommon = new SH_Common();
                    txtTitle.Value = ds.Tables[0].Rows[0][1].ToString();
                    txtDescribe.Value = ds.Tables[0].Rows[0][2].ToString();
                    txtContent.Value = ds.Tables[0].Rows[0][3].ToString();
                    fileImg.Src = "../" + ds.Tables[0].Rows[0][4].ToString();
                    _shCommon.SetDpListValue(dpType, ds.Tables[0].Rows[0][5].ToString());
                }
            }
        }
        #endregion

        protected void btnInsertImg_Click(object sender, EventArgs e)
        {
            CommonTools _commonTools = new CommonTools();
            string dbPath = "";
            int uploadResult = _commonTools.RtnFileName(fileContents, ref dbPath);
            if (uploadResult == -1)
            {
                Msg = MessageTools.ShowMessage(0, "上传图片格式错误");
            }
            else if (uploadResult == -2)
            {
                Msg = MessageTools.ShowMessage(0, "创建目录失败");
            }
            else
            {
                string baseUrl = "http://" + Request.Url.Authority + "/";
                txtContent.Value += "<img src=\"" + baseUrl + dbPath + "\" style=\"width:760px;height:530px; margin:0 auto; display:block;\">";
            }
        }
    }
}