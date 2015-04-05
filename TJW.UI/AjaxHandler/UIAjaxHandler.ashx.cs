using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.AjaxHandler
{
    /// <summary>
    /// FOR CHECK: [1:TRUE  0:FALSE  -1:FALSE]
    /// 
    /// </summary>
    public class UIAjaxHandler : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Charset = "utf-8";


            string Method = context.Request.Params["Method"];
            switch (Method)
            {
                case "CheckRUN":
                    CheckRegistUsername(context);
                    break;
                case "CheckREmail":
                    CheckRegistEmail(context);
                    break;
                case "CheckRPwd":
                    CheckPassword(context);
                    break;
                case "GetCartSize":
                    ShowCartSize(context);
                    break;
                case "AddCart":
                    AddCart(context);
                    break;
                case "GetClothUGUID":
                    GetClothUGUID(context);
                    break;
                case "AddTeaCart":
                    AddTeaCart(context);
                    break;
                case "BindProvince":
                    BindProvince(context);
                    break;
                case "BindCity":
                    BindCity(context);
                    break;
                case "BindDistinct":
                    BindDistinct(context);
                    break;
                case "GetAddressCount":
                    GetAddressCount(context);
                    break;
                case "GetStoreCount_Detail":
                    GetStoreCount_Detail(context);
                    break;
            }
        }

        #region Check regist user name
        private void CheckRegistUsername(HttpContext context)
        {
            SH_Login _shLogin = new SH_Login();
            CommonTools _commonTools = new CommonTools();
            string txtUname = context.Request.QueryString["registUserName"].ToString();

            if (_commonTools.RegChineseNumberAlpha(txtUname))
            {
                if (_shLogin.CheckRegistUsername(txtUname))
                {
                    context.Response.Write("1");
                }
                else
                {
                    context.Response.Write("0");
                }
            }
            else
            {
                context.Response.Write("-1");
            }
        }
        #endregion

        #region Check regist email
        private void CheckRegistEmail(HttpContext context)
        {
            SH_Login _shLogin = new SH_Login();
            CommonTools _commonTools = new CommonTools();
            string txtEmail = context.Request.QueryString["registEmail"].ToString();

            if (_commonTools.RegEmail(txtEmail))
            {
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("0");
            }
        }
        #endregion

        #region Check password
        private void CheckPassword(HttpContext context)
        {
            SH_Login _shLogin = new SH_Login();
            _shLogin = new SH_Login();
            CommonTools _commonTools = new CommonTools();
            string txtPassword = context.Request.QueryString["registPassword"].ToString();

            if (_commonTools.RegPassword(txtPassword))
            {
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("0");
            }
        }
        #endregion

        #region Show cart size()
        private void ShowCartSize(HttpContext context)
        {
            SH_Index _shIndex = new SH_Index();
            StringBuilder stringBuilder = new StringBuilder();
            _shIndex = new SH_Index();
            List<Size> sizeList = _shIndex.GetCartSize(context.Request.QueryString["clothGuid"].ToString(), context.Request.QueryString["colorName"].ToString());
            int j = 0;
            foreach (Size model in sizeList)
            {
                j++;
                stringBuilder.Append("<div class=\"detail_size\"  id=\"sizeId" + j + "\" onclick=\"SizeClick(this.id)\">" + model.SizeName + "</div>");
            }
            context.Response.Write(stringBuilder.ToString());
        }
        #endregion

        #region Add cart
        private void AddCart(HttpContext context)
        {
            SH_Index _shIndex = new SH_Index();
            SH_Login _shLogin = new SH_Login();
            SH_Common _shCommon = new SH_Common();
            Cart _cartModel = new Cart();

            if (_shCommon.CheckSession(ConstValue.LoginSessionName))
            {
                if (context.Request.QueryString["clothId"] != null && context.Request.QueryString["clothCount"] != null)
                {
                    _cartModel.StuffUGUID = context.Request.QueryString["clothId"];
                    _cartModel.ClothCount = Convert.ToInt32(context.Request.QueryString["clothCount"]);
                    _cartModel.UserId = Convert.ToInt32(_shLogin.GetUserId(context.Session[ConstValue.LoginSessionName].ToString()));
                    _cartModel.CreateDate = DateTime.Now;

                    //check same item in cart(basic item and custom BH item)
                    //BH item
                    if (context.Request.QueryString["dpCusId"].ToString() != "null")
                    {
                        _cartModel.IsCustomBH = Convert.ToInt32(context.Request.QueryString["dpCusId"]);
                        if (_shIndex.CheckSameCartWithBH(_cartModel))
                        {
                            if (_shIndex.CheckItemNumInCart(_cartModel.UserId))
                            {
                                _shIndex.AddCart(_cartModel);
                                context.Response.Write("1");
                            }
                            else
                            {
                                //>10 item in cart
                                context.Response.Write("-5");
                            }
                        }
                        else
                        {
                            //exist same item in cart
                            context.Response.Write("-1");
                        }
                    }
                    //basic item
                    else
                    {
                        if (_shIndex.CheckSameCart(_cartModel))
                        {
                            if (_shIndex.CheckItemNumInCart(_cartModel.UserId))
                            {
                                _cartModel.IsCustomBH = 0;
                                _shIndex.AddCart(_cartModel);
                                context.Response.Write("1");
                            }
                            else
                            {
                                //>10 item in cart
                                context.Response.Write("-5");
                            }
                        }
                        else
                        {
                            //exist same item in cart
                            context.Response.Write("-1");
                        }
                    }
                }
                else
                {
                    context.Response.Write("-3");
                }
            }
            else
            {
                //no login
                context.Response.Write("-2");
            }
        }
        #endregion

        #region Get cloth unique GUID in cart page
        private void GetClothUGUID(HttpContext context)
        {
            SH_Index _shIndex = new SH_Index();
            string result = "";
            if (context.Request.QueryString["dpCusId"].ToString() != "null")
            {
                result = _shIndex.GetClothUGUIDWithBH(context.Request.QueryString["clothGuid"].ToString());
            }
            else
            {
                result = _shIndex.GetClothUGUID(context.Request.QueryString["clothGuid"].ToString(), context.Request.QueryString["colorName"].ToString(), context.Request.QueryString["sizeName"].ToString());
            }
            if (result != "-1")
            {
                //success
                context.Response.Write(result);
            }
            else
            {
                //failed
                context.Response.Write("0");
            }
        }
        #endregion

        #region Add tea cart
        private void AddTeaCart(HttpContext context)
        {
            SH_Index _shIndex = new SH_Index();
            SH_Login _shLogin = new SH_Login();
            SH_Common _shCommon = new SH_Common();
            Cart _cartModel = new Cart();

            if (_shCommon.CheckSession(ConstValue.LoginSessionName))
            {
                if (context.Request.QueryString["teaGUID"] != null && context.Request.QueryString["teaCount"] != null)
                {
                    _cartModel.StuffUGUID = context.Request.QueryString["teaGUID"];
                    _cartModel.ClothCount = Convert.ToInt32(context.Request.QueryString["teaCount"]);
                    _cartModel.UserId = Convert.ToInt32(_shLogin.GetUserId(context.Session[ConstValue.LoginSessionName].ToString()));
                    _cartModel.CreateDate = DateTime.Now;

                    if (_shIndex.CheckSameCart(_cartModel))
                    {

                        if (_shIndex.CheckItemNumInCart(_cartModel.UserId))
                        {
                            _shIndex.AddCart(_cartModel);
                            context.Response.Write("1");
                        }
                        else
                        {
                            //>10 item in cart
                            context.Response.Write("-5");
                        }

                    }
                    else
                    {
                        //exist in cart
                        context.Response.Write("-1");
                    }
                }
                else
                {
                    context.Response.Write("-3");
                }
            }
            else
            {
                //no login
                context.Response.Write("-2");
            }
        }
        #endregion

        #region Bind province
        private void BindProvince(HttpContext context)
        {
            SH_UserManagement _shUM = new SH_UserManagement();
            CommonTools _commonTools = new CommonTools();
            DataSet ds = _shUM.GetProvince();
            context.Response.Write(_commonTools.MStoJson(ds.Tables[0]));
        }
        #endregion

        #region Bind city
        private void BindCity(HttpContext context)
        {
            if (context.Request.QueryString["proId"] != null)
            {
                SH_UserManagement _shUM = new SH_UserManagement();
                CommonTools _commonTools = new CommonTools();
                DataSet ds = _shUM.GetCity(context.Request.QueryString["proId"].ToString());
                context.Response.Write(_commonTools.MStoJson(ds.Tables[0]));
            }
            else
            {
                context.Response.Write("-1");
            }
        }
        #endregion

        #region Bind distinct
        private void BindDistinct(HttpContext context)
        {
            if (context.Request.QueryString["cityId"] != null)
            {
                SH_UserManagement _shUM = new SH_UserManagement();
                CommonTools _commonTools = new CommonTools();
                DataSet ds = _shUM.GetDistinct(context.Request.QueryString["cityId"].ToString());
                context.Response.Write(_commonTools.MStoJson(ds.Tables[0]));
            }
            else
            {
                context.Response.Write("-1");
            }
        }
        #endregion

        #region Get address count
        private void GetAddressCount(HttpContext context)
        {
            SH_UserManagement _shUserMng = new SH_UserManagement();
            SH_Login _shLogin = new SH_Login();
            if (context.Request.QueryString["userName"] != null)
            {
                int userId = Convert.ToInt32(_shLogin.GetUserId(context.Request.QueryString["userName"].ToString()));
                context.Response.Write(_shUserMng.GetUserAddressNumber(userId));
            }
        }
        #endregion

        #region Get store count at detail page
        private void GetStoreCount_Detail(HttpContext context)
        {
            SH_Index _shIndex = new SH_Index();
            if (context.Request.QueryString["colorName"] != null && context.Request.QueryString["sizeName"] != null && context.Request.QueryString["guid"] != null)
            {
                var colorName = context.Request.QueryString["colorName"];
                string sizeName = context.Request.QueryString["sizeName"].ToString();
                string guid = context.Request.QueryString["guid"].ToString();
                context.Response.Write(_shIndex.GetStoreCountDetailPage(colorName, sizeName, guid));
            }
            else
            {
                context.Response.Write("-1");
            }
        }
        #endregion

        #region IsReusable
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}