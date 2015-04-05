/* ======================================================================== 
* Author：Cass 
* Time：8/7/2014 3:20:36 PM 
* Description:  Const value 
* ======================================================================== 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TJW.Utils
{
    public class ConstValue
    {
        #region Const value
        public const string AdminLoginDefaultUsername = "请输入用户名";
        public const string AdminLoginDefaultPassword = "请输入密码";
        public const string PublicIpWebsite = "http://www.ip138.com/ips1388.asp";
        public const int ValidateCodeTimes = 3;  //show validate code when try 3 times
        public const int StoreCount = 4; //when store count is 3, make it as red and bold
        public const string IndexDirectToDetailPage = "Detail.aspx?clothGUID="; //index data hyperlink to detail page
        public const string IndexDirectToDetailPageForId = "Detail.aspx?clothId="; //index data hyperlink to detail page
        public const string LoginSessionName = "_loginUser";
        public const int ItemNumberInCart = 9;
        public const string ReturnIndex = "/Index.aspx";
        #endregion

        #region Set pager
        public object pageParams = System.Web.HttpContext.Current.Request.QueryString["pager"]; //page parameter
        public string pnAbleClass = "npAbleNumber";
        public string pnDisAbleClass = "npDisableNumber";
        public string pagerNumberClass = "pageNumber";
        public string currentpagerNumberClass = "choosePageNumber";
        #endregion

        #region Password
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 1000;

        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;

        #endregion

        #region WeChat

        public const string weChatRtnCorrectMsg = "ok";
        public const string weChatUserName = "ybzgw8";
        public const string weChatPassword = "ry121860";
        public const string weChatToken = "20141120CASS";
        public const int weChatShowNumber = 3;
        public const string Appid = "wxba5fc0189fd21928";
        public const string AppSecret = "ce554806ee5c5af98c44d2918017f754";

        //event
        public const string WCMenuName1 = "时尚资讯";
        public const string WCMenuKey1 = "news";

        public const string WCMenuName2 = "最新活动";
        public const string WCMenuKey2 = "action";

        public const string WCMenuName3_1 = "联系我们";
        public const string WCMenuKey3_1 = "contact";

        public const string WCMenuName3_2 = "私人定制";
        public const string WCMenuKey3_2 = "custom";

        #endregion

        #region Users
        public const int SuperUserId = 1;
        #endregion
    }
}
