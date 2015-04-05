/* ======================================================================== 
* Author：Cass 
* Time：10/13/2014 2:25:42 PM 
* Description:  
* ======================================================================== 
*/

using System;

namespace TJW.Model
{
    public class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string IndexTitle { get; set; }
        public string Contents { get; set; }
        public string PicPath { get; set; }
        public string IsStr { get; set; }
        public DateTime AddTime { get; set; }
        public int CreateUserId { get; set; }
    }
}
