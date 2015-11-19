using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJW.Utils;

namespace TJW.UI
{
    public partial class Test4 : System.Web.UI.Page
    {
        [STAThread]
        protected void Page_Load(object sender, EventArgs e)
        {
            var argbColor = Color.FromName("#B1C8E1");

            var colorName = FindColorName(argbColor);
        }

        public string GetGUIDList()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 38; i++)
            {
                sb.Append(CommonTools.GenerateGUID("C", true));
                sb.Append("<br>");
                Thread.Sleep(1000);
            }
            return sb.ToString();
        }



        private static string FindColorName(Color argbColor)
        {
            var propertyInfos = typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var propertyInfo in propertyInfos)
            {
                if (!propertyInfo.CanRead)
                    continue;

                var value = propertyInfo.GetValue(null, null);
                if (!(value is Color))
                    continue;

                if (((Color)value).ToArgb() == argbColor.ToArgb())
                    return propertyInfo.Name;
            }

            return null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string test = Request["test1"].ToString();
        }
    }
}