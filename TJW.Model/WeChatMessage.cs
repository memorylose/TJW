/* ======================================================================== 
* Author：Cass 
* Time：9/26/2014 3:43:42 PM 
* Description:  
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class WeChatMessage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Describe { get; set; }
        public string Contents { get; set; }
        public string PicturePath { get; set; }
        public int TypeId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
