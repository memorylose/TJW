/* ======================================================================== 
* Author：Cass 
* Time：8/6/2014 5:15:25 PM 
* Description:  Order info
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class Order
    {
        /// <summary>
        /// ID
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// order number
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// stuff info
        /// </summary>
        public string StuffInfo { get; set; }

        /// <summary>
        /// pay type 
        /// </summary>
        public int PayTypeId { get; set; }

        /// <summary>
        /// order status
        /// </summary>
        public string OrderStatusId { get; set; }

        /// <summary>
        /// address id
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// create cart date
        /// </summary>
        public DateTime CreateCartDate { get; set; }

        /// <summary>
        /// create order date
        /// </summary>
        public DateTime CreateOrderDate { get; set; }

        /// <summary>
        /// create user id
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// user pay date
        /// </summary>
        public DateTime UserPayDate { get; set; }
    }
}
