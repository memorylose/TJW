/* ======================================================================== 
* Author：Cass 
* Time：8/6/2014 5:03:30 PM 
* Description:  Cloth info
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class Cloth
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ClothId { get; set; }

        /// <summary>
        /// Cloth GUID
        /// </summary>
        public string ClothGuid { get; set; }

        /// <summary>
        /// Stuff GUID
        /// </summary>
        public string StuffUGUID { get; set; }

        /// <summary>
        /// Cloth name
        /// </summary>
        public string ClothName { get; set; }

        /// <summary>
        /// Cloth type id
        /// </summary>
        public int ClothTypeId { get; set; }

        /// <summary>
        /// Store count
        /// </summary>
        public int StoreCount { get; set; }

        /// <summary>
        /// Cloth price
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Original price
        /// </summary>
        public float OriginalPrice { get; set; }

        /// <summary>
        /// Cloth color id
        /// </summary>
        public int ColorId { get; set; }

        /// <summary>
        /// Cloth size id
        /// </summary>
        public int SizeId { get; set; }

        /// <summary>
        /// Cloth valid
        /// </summary>
        public bool IsVaild { get; set; }

        /// <summary>
        /// Cloth show where 
        /// </summary>
        public int ShowNum { get; set; }

        /// <summary>
        /// Custom bh id
        /// </summary>
        public int CustomBHId { get; set; }

        /// <summary>
        /// Create cloth date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Create user id
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// Modify date
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// zhekou
        /// </summary>
        public string ZheKou { get; set; }

        /// <summary>
        /// Modify user id
        /// </summary>
        public int ModifiedUserId { get; set; }

        /// <summary>
        /// 右侧的推荐(热门推荐大，热门推荐，热销商品大，热销商品)
        /// </summary>
        public string IsTj { get; set; }

        //For select resut
        public string ClothTypeName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string ShowName { get; set; }

    }
}
