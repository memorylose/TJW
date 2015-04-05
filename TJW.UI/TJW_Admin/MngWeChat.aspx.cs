using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;
using TJW.WeChatHandler;


namespace TJW.UI.TJW_Admin
{
    public partial class MngWeChat : System.Web.UI.Page
    {
        public string rtnMsg;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdateList_Click(object sender, EventArgs e)
        {
            UpdateList();
        }

        public void UpdateList()
        {
            try
            {
                WeChatUsers model = new WeChatUsers();
                SH_WeChat _shWeChat = new SH_WeChat();

                //clear wechat table
                _shWeChat.TrancateUsers();

                CookieContainer cc = new CookieContainer();
                GroupUsers _groupUsers = new GroupUsers();

                string loginParamData = "username=" + ConstValue.weChatUserName + "&pwd=" + MyEncrypt.GetMd5Hash(ConstValue.weChatPassword).ToUpper() + "&imgcode=&f=json";
                string loginUrl = "https://mp.weixin.qq.com/cgi-bin/login?lang=zh_CN";
                string rtnLogin = _groupUsers.CheckLoginHttpPost(loginParamData, loginUrl, cc);

                WeiXinRetInfo loginModel = SeriaFunc.JsonDeserialize<WeiXinRetInfo>(rtnLogin);
                base_resp baseLogin = loginModel.base_resp;

                //check username and password
                if (string.Equals(baseLogin.err_msg, ConstValue.weChatRtnCorrectMsg))
                {
                    int tokenPlaceNum = loginModel.redirect_url.LastIndexOf('=');
                    string rtnToken = loginModel.redirect_url.Substring(tokenPlaceNum + 1);

                    //get group users
                    string Url = "https://mp.weixin.qq.com/cgi-bin/contactmanage?t=user/index&pagesize=10&pageidx=0&type=0&groupid=0&token=" + rtnToken + "&lang=zh_CN";
                    List<SingleGroup> list = _groupUsers.getAllGroupInfo(cc, rtnToken);

                    
                    //add group users into database
                    foreach (SingleGroup sin in list)
                    {
                        for (int i = 0; i < sin.groupdata.Count; i++)
                        {
                            model.FakeId = sin.groupdata[i]["id"].ToString();
                            model.NickName = sin.groupdata[i]["nick_name"].ToString();
                            model.RemarkName = sin.groupdata[i]["remark_name"].ToString();
                            model.GroupName = sin.group_name;

                            _shWeChat.AddUsersToDB(model);
                        }
                    }
                }
                else
                {
                    //incorrect username and password
                    rtnMsg = baseLogin.err_msg;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("ex:" + ex.ToString());
            }
        }
    }
}