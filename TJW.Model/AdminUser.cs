/* ======================================================================== 
* Author：Cass 
* Time：8/6/2014 4:58:36 PM 
* Description: Admin user info 
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class AdminUser
    {
        /// <summary>
        /// ID
        /// </summary>
        public int AdminUserId { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string AdminUserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string AdminPwd { get; set; }

        /// <summary>
        /// Admin role id
        /// </summary>
        public int AdminRoleId { get; set; }

        /// <summary>
        /// Create user date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Is valid
        /// </summary>
        public bool IsValid { get; set; }
    }
}
