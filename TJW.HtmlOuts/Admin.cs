/* ======================================================================== 
* Author：Cass 
* Time：9/28/2014 4:52:02 PM 
* Description:  
* ======================================================================== 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TJW.Utils;

namespace TJW.HtmlOuts
{
    public class Admin
    {
        #region Admin main page

        #region Common item

        public string MainCommonItem(string type, int roleId)
        {
            StringBuilder strBuilder = new StringBuilder();
            switch (type)
            {
                case "cloth":
                    strBuilder.Append("<a href=\"MngCloth.aspx\" target=\"f1\" class=\"main_detail\">服饰管理</a>");
                    strBuilder.Append("<a href=\"AddCloth.aspx\" target=\"f1\" class=\"main_detail\">添加服饰</a>");
                    strBuilder.Append("<a href=\"MngCustomBH.aspx\" target=\"f1\" class=\"main_detail\">自定义编号</a>");
                    strBuilder.Append(SuperClothItem(roleId));
                    break;
            }
            return strBuilder.ToString();
        }

        #endregion

        #region Cloth item

        public string SuperClothItem(int roleId)
        {
            if (roleId == ConstValue.SuperUserId)
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("<a href=\"MngClothType.aspx\" target=\"f1\" class=\"main_detail\">服饰类别管理</a>");
                strBuilder.Append("<a href=\"MngClothColor.aspx\" target=\"f1\" class=\"main_detail\">服饰颜色管理</a>");
                strBuilder.Append("<a href=\"MngClothSize.aspx\" target=\"f1\" class=\"main_detail\">服饰尺寸管理</a>");
                strBuilder.Append("<a href=\"MngPageShow.aspx\" target=\"f1\" class=\"main_detail\">显示位置管理</a>");
                strBuilder.Append("<a href=\"MngPictureType.aspx\" target=\"f1\" class=\"main_detail\">图片类别管理</a>");
                return strBuilder.ToString();
            }
            else
            {
                return "";
            }
        }

        #endregion

        #endregion
    }
}
