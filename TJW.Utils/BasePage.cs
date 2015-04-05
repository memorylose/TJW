/* ======================================================================== 
* Author：Cass 
* Time：8/12/2014 3:39:25 PM 
* Description:  Admin base page
* ======================================================================== 
*/

using System.Web.Security;
using System.Web.UI;
using TJW.Model;

namespace TJW.Utils
{
    public class BasePage : Page
    {
        public LoginInfo LoginUser
        {
            get
            {
                string strUser = ((FormsIdentity)this.Context.User.Identity).Ticket.UserData;
                return SeriaFunc.DnSerializeFun(strUser);
            }
        }
    }
}
