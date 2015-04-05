/* ======================================================================== 
* Author：Cass 
* Time：8/6/2014 5:35:08 PM 
* Description:  Stuff info
* ======================================================================== 
*/


namespace TJW.Model
{
    public class StuffInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int StuffId { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Stuff name
        /// </summary>
        public string StuffName { get; set; }

        /// <summary>
        /// Stuff address
        /// </summary>
        public string StuffAddress { get; set; }

        /// <summary>
        /// Stuff post code
        /// </summary>
        public int StuffPostCode { get; set; }

        /// <summary>
        /// Stuff tel
        /// </summary>
        public string StuffTel { get; set; }

        /// <summary>
        /// Stuff valid
        /// </summary>
        public bool IsValid { get; set; }
    }
}
