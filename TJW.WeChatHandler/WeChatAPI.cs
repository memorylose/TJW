/* ======================================================================== 
* Author：Cass 
* Time：9/28/2014 8:17:31 AM 
* Description:  
* ======================================================================== 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TJW.Utils;

namespace TJW.WeChatHandler
{
    public class WeChatAPI
    {
        #region 微信接口类
        public static bool CheckSignature(HttpContext context, string token)
        {
            //token是否为空
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            else
            {
                //将token、timestamp、nonce三个参数进行字典序排序
                string[] arr = {
					token,
					context.Request.QueryString ["timestamp"],
					context.Request.QueryString ["nonce"]
				};
                //字典排序
                Array.Sort(arr);
                //sha1加密
                var s = MyEncrypt.GetSHA1(string.Join("", arr)).ToLower();
                var a = context.Request.QueryString["signature"];

                if (s == a)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
