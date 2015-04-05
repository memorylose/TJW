using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.UI.UM
{
    public partial class MyAddress : System.Web.UI.Page
    {
        SH_Common _shCommon;
        SH_UserManagement _shUserMng;
        SH_Login _shLogin;
        protected string AddressShow;
        protected string ErrorMsg;

        protected void Page_Load(object sender, EventArgs e)
        {
            _shCommon = new SH_Common();
            if (!Page.IsPostBack)
            {
                if (!_shCommon.CheckSession(ConstValue.LoginSessionName))
                {
                    Response.Redirect("../Login.aspx?um=1");
                }
                else
                {
                    //delete address
                    DelAddress();
                    //set default
                    AddressDefault();
                    //show address
                    AddressShow = ShowAddress();
                }
            }
        }

        protected void txtBtn_Click(object sender, EventArgs e)
        {
            _shCommon = new SH_Common();

            if (CheckAddress())
            {
                if (_shCommon.CheckSession(ConstValue.LoginSessionName))
                {
                    if (Request.QueryString["editId"] != null)
                    {

                    }
                    else
                    {
                        Add();
                    }
                }
                else
                {
                    Response.Redirect("../Login.aspx?um=1");
                }
            }
        }

        #region Add
        private void Add()
        {
            _shUserMng = new SH_UserManagement();
            Address model = new Address();
            _shLogin = new SH_Login();
            model.UserId = Convert.ToInt32(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString()));
            //max 3 address
            if (_shUserMng.GetUserAddressNumber(model.UserId) == 3)
            {
                ErrorMsg = "<div class=\"add_address_error\">您最多只能添加3个地址</div>";
            }
            else
            {
                model.UserAddress = _shUserMng.GetName_PCD("p", Request.Form["nameProvince"].ToString()) + _shUserMng.GetName_PCD("c", Request.Form["nameCity"].ToString()) + _shUserMng.GetName_PCD("d", Request.Form["nameDistinct"].ToString()) + txtJD.Value;
                model.UserName = txtName.Value;
                model.UserTel = txtTel.Value;
                model.UserCode = txtCode.Value;
                if (_shUserMng.GetUserAddressNumber(model.UserId) == 0)
                {
                    model.IsDefault = true;
                }
                else
                {
                    model.IsDefault = false;
                }
                model.CreateTime = DateTime.Now;

                _shUserMng.AddAddress(model);
                Response.Redirect("MyAddress.aspx");
            }
        }
        #endregion

        #region Address check
        private bool CheckAddress()
        {
            bool result = true;
            CommonTools _cTools = new CommonTools();
            if (string.Equals(Request.Form["nameProvince"].ToString(), "请选择"))
            {
                msgProvinceId.Attributes.Add("style", "color:#FA817E");
                result = false;
            }
            else if (txtJD.Value.Length < 5 || txtJD.Value.Length > 100)
            {
                msgProvinceId.Attributes.Add("style", "color:#999999");

                msgDistinctId.Attributes.Add("style", "color:#FA817E");
                txtJD.Attributes.Add("style", "border:1px solid #FA817E");
            }
            else if (!_cTools.CheckCode(txtCode.Value))
            {
                msgDistinctId.Attributes.Add("style", "color:#999999");
                txtJD.Attributes.Add("style", "border:1px solid #DDDDDD");

                msgCodeId.Attributes.Add("style", "color:#FA817E");
                txtCode.Attributes.Add("style", "border:1px solid #FA817E");
            }
            else if (!_cTools.CheckPhoneNumber(txtTel.Value))
            {
                msgCodeId.Attributes.Add("style", "color:#999999");
                txtCode.Attributes.Add("style", "border:1px solid #DDDDDD");

                msgTelId.Attributes.Add("style", "color:#FA817E");
                txtTel.Attributes.Add("style", "border:1px solid #FA817E");
            }
            else
            {
                msgTelId.Attributes.Add("style", "color:#999999");
                txtTel.Attributes.Add("style", "border:1px solid #DDDDDD");
            }
            return result;
        }
        #endregion

        #region Show address
        private string ShowAddress()
        {
            _shUserMng = new SH_UserManagement();
            _shLogin = new SH_Login();
            DataSet ds = _shUserMng.GetAddress(_shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString()));
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strBuilder.Append("<div class=\"address_show\">");
                strBuilder.Append("<div class=\"address_show_name\">" + ds.Tables[0].Rows[i][2].ToString() + "</div>");
                strBuilder.Append("<div class=\"address_show_address\">" + CommonTools.CutString(ds.Tables[0].Rows[i][1].ToString(), 25, true) + "</div>");
                strBuilder.Append("<div class=\"address_show_tel\">" + ds.Tables[0].Rows[i][4].ToString() + "</div>");
                strBuilder.Append("<div class=\"address_show_op\">" + SetDefault(Convert.ToBoolean(ds.Tables[0].Rows[i][5]), ds.Tables[0].Rows[i][0].ToString()) + "<a href=\"\">编辑</a><a onclick=\"CheckCartDelete('" + ds.Tables[0].Rows[i][0] + "')\">删除</a></div>");
                strBuilder.Append("</div>");
            }
            return strBuilder.ToString();
        }
        #endregion

        #region Set default
        private string SetDefault(bool isDefault, string defaultId)
        {
            if (isDefault)
            {
                return "<span>当前地址</span>";
            }
            else
            {
                return "<a href=\"MyAddress.aspx?defaultId=" + defaultId + "\">设为默认</a>";
            }
        }
        #endregion

        #region Delete address
        private void DelAddress()
        {
            if (Request.QueryString["delId"] != null)
            {
                _shUserMng = new SH_UserManagement();
                _shUserMng.DeleteAddress(Convert.ToInt32(Request.QueryString["delId"]));
                Response.Redirect("MyAddress.aspx");
            }
        }
        #endregion

        #region Address default
        private void AddressDefault()
        {
            if (Request.QueryString["defaultId"] != null)
            {
                _shLogin = new SH_Login();
                _shUserMng = new SH_UserManagement();
                _shUserMng.SetDefault(Convert.ToInt32(Request.QueryString["defaultId"]), _shLogin.GetUserId(Session[ConstValue.LoginSessionName].ToString()));
            }
        }
        #endregion
    }
}