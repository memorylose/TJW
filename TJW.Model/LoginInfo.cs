/* ======================================================================== 
* Author：Cass 
* Time：8/12/2014 8:59:14 AM 
* Description:  Login info 
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    [Serializable]
    public class LoginInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime LoginTime { get; set; }
        public string Roles { get; set; }
    }
}
