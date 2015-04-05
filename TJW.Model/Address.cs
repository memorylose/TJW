/* ======================================================================== 
* Author：Cass 
* Time：8/28/2014 3:31:22 PM 
* Description:  User address
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class Address
    {
        /// <summary>
        /// Address id
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string UserAddress { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Post code
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// Tel
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// Is default
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Create time
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
