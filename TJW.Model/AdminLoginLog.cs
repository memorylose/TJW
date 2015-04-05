/* ======================================================================== 
* Author：Cass 
* Time：8/7/2014 1:44:51 PM 
* Description:  Admin login log
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class AdminLoginLog
    {
        /// <summary>
        /// ID
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// Admin user id
        /// </summary>
        public string AdminUserName { get; set; }

        /// <summary>
        /// Ip address
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Login date
        /// </summary>
        public DateTime LoginDate { get; set; }

        /// <summary>
        /// Login status 
        /// </summary>
        public bool LoginStatus { get; set; }
    }
}
