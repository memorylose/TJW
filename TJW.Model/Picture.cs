/* ======================================================================== 
* Author：Cass 
* Time：8/6/2014 5:26:54 PM 
* Description:  Picture info
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class Picture
    {
        /// <summary>
        /// ID
        /// </summary>
        public int PictureId { get; set; }
        /// <summary>
        /// Cloth Id
        /// </summary>
        public string ClothGUID { get; set; }

        /// <summary>
        /// Picture type id
        /// </summary>
        public int PictureTypeId { get; set; }

        /// <summary>
        /// Custom href
        /// </summary>
        public string PicHref { get; set; }

        /// <summary>
        /// Custom word
        /// </summary>
        public string PicWord { get; set; }

        /// <summary>
        /// Create date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Create user id
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// Picture path
        /// </summary>
        public string PicturePath { get; set; }

        //for get list
        public string pTypeName { get; set; }
        public string ClothName { get; set; }
        public string Price { get; set; }
        public int rowNum { get; set; }
        public int ClothId { get; set; }
    }
}
