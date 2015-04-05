using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.SqlHandler;
using TJW.Model;
using TJW.Utils;
using System.Data;
using System.IO;

namespace TJW.UI.TJW_Admin
{
    public partial class MngClothPicture : BasePage
    {
        SH_Cloth sh_cloth;
        Picture picModel;
        SH_Common sh_common;
        CommonTools commonTools;
        public string Msg;
        public string picShowMsg;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FunDeletePic();
                GetPictureTypeList();
                GetPictureList();
                ShowPictureMessage();
                GetEditPicture();
            }
        }

        #region Get color
        private void GetPictureTypeList()
        {
            sh_cloth = new SH_Cloth();
            List<PictureType> list = sh_cloth.GetPictureTypeList();
            dpPicType.DataSource = list;
            dpPicType.DataTextField = "TypeName";
            dpPicType.DataValueField = "PictureTypeId";
            dpPicType.DataBind();
        }
        #endregion

        #region Add cloth picture
        private void Add()
        {
            if (Request.QueryString["clothGUID"] != null)
            {
                picModel = new Picture();
                commonTools = new CommonTools();

                picModel.ClothGUID = Request.QueryString["clothGUID"].ToString();
                picModel.PictureTypeId = Convert.ToInt32(dpPicType.SelectedItem.Value);
                picModel.CreateDate = DateTime.Now;
                picModel.CreateUserId = LoginUser.UserId;
                picModel.PicHref = txtCustomAddress.Value;
                picModel.PicWord = txtCustomWord.Value;

                string dbPath = "";
                int uploadResult = commonTools.RtnFileName(fileHidden, ref dbPath, Request.QueryString["clothGUID"].ToString());
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
                    picModel.PicturePath = dbPath;
                    sh_cloth = new SH_Cloth();
                    sh_cloth.AddClothPicture(picModel);
                    Response.Redirect("MngClothPicture.aspx?clothGUID=" + Request.QueryString["clothGUID"].ToString());
                }
            }
        }
        #endregion

        #region Edit cloth picture
        private void Edit()
        {
            picModel = new Picture();
            commonTools = new CommonTools();
            sh_cloth = new SH_Cloth();

            //update picture
            picModel.PictureId = Convert.ToInt32(Request.QueryString["editId"]);
            picModel.PictureTypeId = Convert.ToInt32(dpPicType.SelectedItem.Value);
            picModel.PicHref = txtCustomAddress.Value;
            picModel.PicWord = txtCustomWord.Value;

            if (fileHidden.PostedFile.FileName == "")
            {
                sh_cloth.EditClothPictureNo(picModel);
            }
            else
            {
                //delete old picture
                string oldPath = Server.MapPath("../" + sh_cloth.GetClothPicturePath(Request.QueryString["editId"].ToString()));
                if (File.Exists(oldPath))
                {
                    try
                    {
                        File.Delete(oldPath);
                    }
                    catch
                    { }
                }

                string dbPath = "";
                int uploadResult = commonTools.RtnFileName(fileHidden, ref dbPath, Request.QueryString["clothGUID"].ToString());
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
                    picModel.PicturePath = dbPath;
                    sh_cloth.EditClothPicture(picModel);                    
                }
            }
            Response.Redirect("MngClothPicture.aspx?clothGUID=" + Request.QueryString["clothGUID"].ToString());
        }
        #endregion

        #region Get picture
        private void GetPictureList()
        {
            if (Request.QueryString["clothGUID"] != null)
            {
                sh_cloth = new SH_Cloth();
                rpPicture.DataSource = sh_cloth.GetPicture(Request.QueryString["clothGUID"].ToString());
                rpPicture.DataBind();
            }
        }
        #endregion

        #region Show picture message
        private void ShowPictureMessage()
        {
            string dpTest = dpPicType.SelectedItem.Value;
            switch (dpTest)
            {
                case "8":
                    picShowMsg = "宽度：870px（不设高度）";
                    break;
                case "7":
                    picShowMsg = "宽度：450px;高度：500px";
                    break;
                case "3":
                    picShowMsg = "宽度：215px;高度：230px";
                    break;
                case "1":
                    picShowMsg = "宽度：270px;高度：305px";
                    break;
                case "2":
                    picShowMsg = "宽度：114px;高度：145px";
                    break;
                case "4":
                    picShowMsg = "宽度：440px;高度：400px";
                    break;
                case "5":
                    picShowMsg = "宽度：218px;高度：200px";
                    break;
                case "9":
                    picShowMsg = "宽度：620px;高度：310px";
                    break;
                case "10":
                    picShowMsg = "宽度：151px;高度：165px";
                    break;
                case "11":
                    picShowMsg = "宽度：285px;高度：308px";
                    break;
                case "12":
                    picShowMsg = "宽度：270px;高度：200px";
                    break;
                case "13":
                    picShowMsg = "宽度：50px;高度：45px";
                    break;
                case "14":
                    picShowMsg = "宽度：270px;高度：200px";
                    break;
                case "15":
                    picShowMsg = "宽度：50px;高度：45px";
                    break;
                case "16":
                    picShowMsg = "宽度：225px;高度：337px";
                    break;
                case "17":
                    picShowMsg = "宽度：165px;高度：165px";
                    break;

            }
        }
        #endregion

        #region Delete picture
        private void FunDeletePic()
        {
            if (Request.QueryString["delId"] != null)
            {
                sh_cloth = new SH_Cloth();
                DataSet ds = sh_cloth.GetDeletePathId(Request.QueryString["delId"]);
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
                sh_cloth.DeleteClothPicture(Request.QueryString["delId"].ToString());
                Response.Redirect("MngClothPicture.aspx?clothGUID=" + Request.QueryString["clothGUID"]);
            }
        }
        #endregion

        #region Get edit picture
        private void GetEditPicture()
        {
            if (Request.QueryString["editId"] != null)
            {
                sh_cloth = new SH_Cloth();
                DataSet ds = sh_cloth.GetClothPictureEdit(Request.QueryString["editId"]);
                //for multiple operation
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sh_common = new SH_Common();
                    dpPicType.ClearSelection();
                    sh_common.SetDpListValue(dpPicType, ds.Tables[0].Rows[0][0].ToString());
                    fileImg.Attributes.Add("style", "display:block");
                    fileImg.Src = "../" + ds.Tables[0].Rows[0][1].ToString();
                    ShowPictureMessage();

                    addWBtnId.Attributes.Add("style", "display:block");

                    txtCustomAddress.Value = ds.Tables[0].Rows[0][2].ToString();
                    txtCustomWord.Value = ds.Tables[0].Rows[0][3].ToString();
                    if (ds.Tables[0].Rows[0][2].ToString() != "")
                    {
                        cusDp.Attributes.Add("style", "display:block");
                        hdCustomFlag.Value = "1";
                    }
                    if (ds.Tables[0].Rows[0][3].ToString() != "")
                    {
                        cusWord.Attributes.Add("style", "display:block");
                        hdCustomWordFlag.Value = "1";
                    }
                }
                else
                {
                    Response.Redirect("MngClothPicture.aspx?clothGUID=" + Request.QueryString["clothGUID"]);
                }
            }

        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["clothGUID"] != null)
            {
                if (Request.QueryString["editId"] != null)
                {
                    Edit();
                }
                else
                {
                    Add();
                }
            }
        }

        protected void dpPicType_TextChanged(object sender, EventArgs e)
        {
            ShowPictureMessage();
            addWBtnId.Attributes.Add("style", "display:block");
        }
    }
}