/* ======================================================================== 
* Author：Cass 
* Time：8/6/2014 5:22:43 PM 
* Description:  Password info
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class Password
    {
        /// <summary>
        /// ID
        /// </summary>
        public int PasswordId { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Modify times
        /// </summary>
        public int ModifiedTimes { get; set; }

        /// <summary>
        /// Modify date
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
