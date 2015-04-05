/* ======================================================================== 
* Author：Cass 
* Time：8/7/2014 3:14:12 PM 
* Description:  
* ======================================================================== 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TJW.Utils
{
    public class MessageTools
    {
        /// <summary>
        /// Show simple message
        /// </summary>
        /// <param name="outMsg"></param>
        /// <param name="msg"></param>
        public static void ShowMessage(System.Web.UI.HtmlControls.HtmlGenericControl divId, ref string outMsg, string msg)
        {
            divId.Attributes.Add("style", "display:block");
            outMsg = msg;
        }

        /// <summary>
        /// Show admin successful message
        /// </summary>
        /// <param name="msgType">1:success 0:failed</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string ShowMessage(int msgType, string msg)
        {
            string divCls = "";
            string imgPath = "";

            switch (msgType)
            {
                case 1:
                    divCls = "suc_msg";
                    imgPath = "../AdminImages/icn_alert_success.png";
                    break;
                case 0:
                    divCls = "failed_msg";
                    imgPath = "../AdminImages/icn_alert_error.png";
                    break;
            }
            return "<div class=\"" + divCls + "\"><img src=\"" + imgPath + "\" class=\"suc_msg_img\" />" + msg + "</div>";
        }

        /// <summary>
        /// Show regist message
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string ShowRegistMessage(string msg)
        {
            return "<div class=\"regist_back_msg\">" + msg + "</div>";
        }

        /// <summary>
        /// Show login message
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string ShowLoginMessage(string msg)
        {
            return "<div class=\"login_error_msg\">" + msg + "</div>";
        }
    }
}
