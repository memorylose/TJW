/* ======================================================================== 
* Author：Cass 
* Time：8/6/2014 5:49:07 PM 
* Description:  User info
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class Users
    {
        /// <summary>
        /// ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// User mail
        /// </summary>
        public string UserMail { get; set; }

        /// <summary>
        /// User valid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Create date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Create user region
        /// </summary>
        public string CreateUserRegion { get; set; }

        /// <summary>
        /// Last login date
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Last login region
        /// </summary>
        public string LastLoginRegion { get; set; }

        /// <summary>
        /// Modify datetime
        /// </summary>
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// Modify region
        /// </summary>
        public string ModifyRegion { get; set; }

        /// <summary>
        /// Modify times
        /// </summary>
        public int ModifiedTimes { get; set; }

        /// <summary>
        /// Login time
        /// </summary>
        public int LoginTimes { get; set; }
    }
}
