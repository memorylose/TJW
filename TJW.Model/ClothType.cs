/* ======================================================================== 
* Author：Cass 
* Time：8/6/2014 5:10:15 PM 
* Description:  Cloth type info
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class ClothType
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ClothTypeId { get; set; }

        /// <summary>
        /// Cloth first level name
        /// </summary>
        public string ClothFatherName { get; set; }

        /// <summary>
        /// Cloth first level id
        /// </summary>
        public int ClothFahterId { get; set; }

        /// <summary>
        /// Create date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Create user id
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// Top name
        /// </summary>
        public string TopName { get; set; }
    }
}
