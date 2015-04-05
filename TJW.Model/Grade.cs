/* ======================================================================== 
* Author：Cass 
* Time：10/24/2014 3:24:19 PM 
* Description:  
* ======================================================================== 
*/


namespace TJW.Model
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int UserId { get; set; }
        public int GradeNum { get; set; }

        public int ValidGradeNum { get; set; }
    }
}
