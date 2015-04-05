/* ======================================================================== 
* Author：Cass 
* Time：9/26/2014 10:51:37 AM 
* Description:  
* ======================================================================== 
*/


using Newtonsoft.Json.Linq;
namespace TJW.Model
{
    #region 数据库分组数据
    public class WeChatUsers
    {
        public int Id { get; set; }
        public string FakeId { get; set; }
        public string NickName { get; set; }
        public string RemarkName { get; set; }
        public string GroupName { get; set; }
    }
    #endregion

    #region 用来分组
    public class SingleGroup
    {
        public string group_name;
        public JArray groupdata;
    }
    #endregion

    #region  当发送用户名密码到微信服务器的时候，获取返回的数据
    public class WeiXinRetInfo
    {
        public base_resp base_resp { get; set; }
        //redirect_url 会携带token
        public string redirect_url { get; set; }

        //for final bind
        public string ret { get; set; }
        public string msg { get; set; }
    }

    public class base_resp
    {
        public string ret { get; set; }
        public string err_msg { get; set; }
    }

    //For menu return message
    public class MenuRtnMsg
    {
        public string errmsg { get; set; }
        public string errcode { get; set; }
    }
    #endregion
}
