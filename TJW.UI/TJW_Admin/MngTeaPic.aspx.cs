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
    public partial class MngTeaPic : System.Web.UI.Page
    {
        SH_Tea _shTea;
        TeaPicture _teaPicture;
        CommonTools commonTools;
        public string Msg;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FunDeletePic();
                BindTeaType();
                BindTeaPicture();
            }
        }

        #region Bind tea type
        private void BindTeaType()
        {
            _shTea = new SH_Tea();
            dpPicType.DataSource = _shTea.GetTeaPictureType();
            dpPicType.DataTextField = "TypeName";
            dpPicType.DataValueField = "TeaPictureTypeId";
            dpPicType.DataBind();
        }

        #endregion

        #region Bind tea picture
        private void BindTeaPicture()
        {
            if (Request.QueryString["stfId"] != null)
            {
                _shTea = new SH_Tea();
                rpTeaPicture.DataSource = _shTea.GetTeaPicture(Request.QueryString["stfId"].ToString());
                rpTeaPicture.DataBind();
            }
        }
        #endregion

        #region Delete picture
        private void FunDeletePic()
        {
            if (Request.QueryString["delId"] != null)
            {
                _shTea = new SH_Tea();
                DataSet ds = _shTea.GetDeletePathId(Request.QueryString["delId"]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string picturePath = Server.MapPath("../" + ds.Tables[0].Rows[0][0].ToString());
                    if (File.Exists(picturePath))
                    {
                        try
                        {
                            File.Delete(picturePath);
                        }
                        catch { }
                    }
                }
                _shTea.DeleteTeaPicture(Request.QueryString["delId"].ToString());
                Response.Redirect("MngTeaPic.aspx?stfId=" + Request.QueryString["delId"]);
            }
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["stfId"] != null)
            {
                _shTea = new SH_Tea();

                //check same picture
                if (_shTea.CheckSameTeaPicture(Request.QueryString["stfId"].ToString(), dpPicType.SelectedItem.Value))
                {
                    commonTools = new CommonTools();
                    _teaPicture = new TeaPicture();
                    _teaPicture.TeaStuffGUID = Request.QueryString["stfId"].ToString();
                    _teaPicture.PictureTypeId = Convert.ToInt32(dpPicType.SelectedItem.Value);

                    string dbPath = "";
                    int uploadResult = commonTools.RtnFileName(fileHidden, ref dbPath, Request.QueryString["stfId"].ToString());
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
                        _teaPicture.PicturePath = dbPath;

                        _shTea.AddTeaPicture(_teaPicture);
                        Response.Redirect("MngTeaPic.aspx?stfId=" + Request.QueryString["stfId"]);
                    }
                }
                else
                {
                    Msg = MessageTools.ShowMessage(0, "您已传过相同位置的图片");                    
                }
            }
        }
    }
}