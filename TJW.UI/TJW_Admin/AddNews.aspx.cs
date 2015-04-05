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
    public partial class AddNews : BasePage
    {
        CommonTools commonTools;
        SH_News _shNews;
        public string Msg;
        private string rtnPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetEditNews();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AddUpdate();
        }

        #region Add update
        private void AddUpdate()
        {
            News model = new News();
            commonTools = new CommonTools();
            _shNews = new SH_News();

            model.Title = txtTitle.Value;
            model.Contents = myArea1.Value;
            model.AddTime = DateTime.Now;
            model.CreateUserId = LoginUser.UserId;
            model.SubTitle = txtzy.Text;
            model.IsStr = SetIsStr();
            model.IndexTitle = txtIndexTitle.Value;
      
            if (Request.QueryString["editId"] != null)
            {
                model.NewsId = Convert.ToInt32(Request.QueryString["editId"]);
                if (fileHidden.Value != "")
                {
                    if (UploadImg() == 1)
                    {
                        //delete old picture
                        string imgPath = Server.MapPath("~/" + _shNews.GetPicturePath(Request.QueryString["editId"]));
                        if (File.Exists(imgPath))
                        {
                            File.Delete(imgPath);
                        }

                        model.PicPath = rtnPath;
                        _shNews.UpdateNewsWithPic(model);
                        Response.Redirect("MngNews.aspx");
                    }
                }
                else
                {
                    _shNews.UpdateNews(model);
                    Response.Redirect("MngNews.aspx");
                }
            }
            else
            {
                if (UploadImg() == 1)
                {
                    model.PicPath = rtnPath;
                    _shNews.AddNews(model);
                    Response.Redirect("MngNews.aspx");
                }
            }
        }
        #endregion

        #region Upload image
        private int UploadImg()
        {
            CommonTools _commonTools = new CommonTools();
            int uploadResult = _commonTools.RtnNewsFileName(fileHidden, ref rtnPath);
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

        #region Get edit news

        private void GetEditNews()
        {
            if (Request.QueryString["editId"] != null)
            {
                SH_News _shNews = new SH_News();
                DataSet ds = _shNews.GetEditNews(Request.QueryString["editId"]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTitle.Value = ds.Tables[0].Rows[0][0].ToString();
                    myArea1.Value = ds.Tables[0].Rows[0][1].ToString();
                    txtzy.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtIndexTitle.Value = ds.Tables[0].Rows[0][4].ToString();
                    GetStr(ds.Tables[0].Rows[0][2].ToString());
                    fileImg.Src = "../" + ds.Tables[0].Rows[0][5].ToString();
                }
            }
        }

        #endregion

        #region Set IsStr
        /// <summary>
        /// 新闻推荐
        /// 热点新闻
        /// </summary>
        /// <returns></returns>
        private string SetIsStr()
        {
            string tj = ckTj.Checked ? "1" : "0";
            string hotTj = ckHot.Checked ? "1" : "0";
            string syNews = ckSyNews.Checked ? "1" : "0";
            return tj + hotTj + syNews;
        }

        private void GetStr(string str)
        {
            if (str.Length > 2)
            {
                ckTj.Checked = (str.Substring(0, 1) == "1") ? true : false;
                ckHot.Checked = (str.Substring(1, 1) == "1") ? true : false;
                ckSyNews.Checked = (str.Substring(2, 1) == "1") ? true : false;
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
                myArea1.Value += "<img src=\"" + baseUrl + dbPath + "\" style=\"width:760px;height:530px; margin:0 auto; display:block;\">";
            }
        }
    }
}