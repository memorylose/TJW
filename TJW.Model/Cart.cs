/* ======================================================================== 
* Author：Cass 
* Time：8/6/2014 5:01:28 PM 
* Description:  Cart info
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class Cart
    {
        /// <summary>
        /// ID
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// Stuff Id(Z1,C2)
        /// </summary>
        public string StuffUGUID { get; set; }

        /// <summary>
        /// Cloth count
        /// </summary>
        public int ClothCount { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Create cart date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Is custom BH
        /// </summary>
        public int IsCustomBH { get; set; }
    }
}
