/* ======================================================================== 
* Author：Cass 
* Time：8/8/2014 8:20:28 AM 
* Description:  
* ======================================================================== 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace TJW.SqlHandler
{
    public class SH_Common
    {
        #region Set default dp list
        public void SetDpListValue(DropDownList dp, string dpName)
        {
            ListItem li = dp.Items.FindByText(dpName);
            if (li != null)
            {
                li.Selected = true;
            }
        }
        public void SetRdListValue(RadioButtonList rd, string dpName)
        {
            ListItem li = rd.Items.FindByText(dpName);
            if (li != null)
            {
                li.Selected = true;
            }
        }
        #endregion

        #region Check session
        public bool CheckSession(string sessionName)
        {
            bool result = true;
            if (System.Web.HttpContext.Current.Session[sessionName] == null)
            {
                result = false;
            }
            else if (System.Web.HttpContext.Current.Session[sessionName] == string.Empty)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Set tea year
        public static string SetYear(int year)
        {
            string result = "";
            if (year < 11)
            {
                result = year.ToString() + "年";
            }
            else if (year == 11)
            {
                result = "大于10年";
            }
            return result;
        }
        #endregion

        #region Generate order number
        /// <summary>
        /// // 123 + userid + 2233
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GenerateOrderNumber(int userId)
        {
            Random rnd = new Random();
            int firstThreeNumber = rnd.Next(100, 999);
            string lastTime = DateTime.Now.ToString("MMddhhmmss");
            return firstThreeNumber.ToString() + lastTime;
        }
        #endregion

        #region Get vip type
        public string GetVipType(int gradeNum, ref string bank)
        {
            string result = "";
            if (gradeNum < 3000)
            {
                result = "普通会员";
            }
            else if (gradeNum >= 3000 && gradeNum < 8000)
            {
                result = "铜牌会员";
            }
            else if (gradeNum >= 8000 && gradeNum < 20000)
            {
                result = "银牌会员";
                bank = "0.95";
            }
            else if (gradeNum >= 20000)
            {
                result = "金牌会员";
                bank = "0.9";
            }
            return result;
        }
        #endregion

    }
}
