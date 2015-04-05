/* ======================================================================== 
* Author：Cass 
* Time：8/7/2014 4:25:54 PM 
* Description:  
* ======================================================================== 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace TJW.Utils
{
    public class CommonTools
    {
        #region Get public ip and location
        public string GetPublicIP()
        {
            //Uri uri = new Uri(ConstValue.PublicIpWebsite);
            //WebRequest wr = WebRequest.Create(uri);
            //Stream s = wr.GetResponse().GetResponseStream();
            //StreamReader sr = new StreamReader(s, System.Text.Encoding.Default);
            //string all = sr.ReadToEnd();
            //int i = all.IndexOf("您的IP地址是") + 9;
            //int j = all.IndexOf("]", i);
            //string tempip = all.Substring(i, j - i);
            //string ip = tempip.Replace(" ", "");
            //sr.Close();
            //s.Close();
            //return ip;
            return "";
        }
        public string GetPublicLocation()
        {
            Uri uri = new Uri(ConstValue.PublicIpWebsite);
            WebRequest wr = WebRequest.Create(uri);
            Stream s = wr.GetResponse().GetResponseStream();
            StreamReader sr = new StreamReader(s, System.Text.Encoding.Default);
            string all = sr.ReadToEnd();
            int i = all.IndexOf("来自") + 3;
            int j = all.IndexOf("<", i);
            string tempip = all.Substring(i, j - i);
            string ip = tempip.Replace(" ", "");
            sr.Close();
            s.Close();
            return ip;
        }
        #endregion

        #region Get time interva
        public string GetTimeInterval()
        {
            string result = string.Empty;
            int nowHour = Convert.ToInt32(DateTime.Now.ToString("HH"));

            if (nowHour > 0 && nowHour < 7)  //1-6
            {
                result = "凌晨好";
            }
            if (nowHour > 6 && nowHour < 11)  //7-10
            {
                result = "上午好";
            }
            if (nowHour > 10 && nowHour < 13)  //11-12
            {
                result = "中午好";
            }
            if (nowHour > 12 && nowHour < 19)  //13-18
            {
                result = "下午好";
            }
            if (nowHour > 18 && nowHour <= 24) //19-24
            {
                result = "晚上好";
            }
            return result;
        }
        #endregion

        #region Generate GUID
        public static string GenerateGUID(string stuffType, bool unique)
        {
            Random random = new Random();

            if (unique)
            {
                int uniqueRandom = random.Next(100000, 999999);
                int uniqueLastRandom = random.Next(10, 99);
                string time = DateTime.Now.ToString("mmss");
                return stuffType + uniqueRandom + time + uniqueLastRandom;
            }
            else
            {
                int firstRandom = random.Next(1000, 9999);
                string secondTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                return firstRandom.ToString() + secondTime;
            }
        }
        #endregion

        #region File extension name
        public bool CheckFileExtension(System.IO.Stream fs)
        {
            bool ret = false;
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
                return false;
            }
            //r.Close();
            //fs.Close();
            /*Description
              *4946/104116 txt
              *7173        gif 
              *255216      jpg
              *13780       png
              *6677        bmp
              *239187      txt,aspx,asp,sql
              *208207      xls.doc.ppt
              *6063        xml
              *6033        htm,html
              *4742        js
              *8075        xlsx,zip,pptx,mmap,zip
              *8297        rar   
              *01          accdb,mdb
              *7790        exe,dll           
              *5666        psd 
              *255254      rdp 
              *10056       bt种子 
              *64101       bat 
              *4059        sgf
              */

            String[] fileType = { "255216", "7173", "6677", "13780" };
            String[] fileExtName = { "jpg", "gif", "bmp", "png" };
            String fExt = "";
            for (int i = 0; i < fileType.Length; i++)
            {
                if (fileclass == fileType[i])
                {
                    fExt = fileExtName[i];
                    ret = true;
                    break;
                }
            }
            return ret;
        }
        #endregion

        #region Upload file
        /// <summary>
        /// Upload file and return file path(For TJW)
        /// 1：success
        /// -1：file extension name is illegal
        /// -2：create file path failed
        /// </summary>
        /// <param name="File1"></param>
        /// <param name="userId"></param>
        /// <param name="rtnFileName"></param>
        /// <returns></returns>
        public int RtnFileName(System.Web.UI.HtmlControls.HtmlInputFile File1, ref string rtnFileName, string clothGUID)
        {
            if (CheckFileExtension(File1.PostedFile.InputStream))
            {
                string rtnPath = "";
                string rtnDbPath = "";
                if (CreatePictureFolder(ref rtnPath, ref rtnDbPath, clothGUID))
                {
                    Random randobj = new Random();
                    string FileRandom = DateTime.Now.ToString("yyyyMMddhhmmss") + randobj.Next(9999);
                    int ExtIndex = File1.PostedFile.FileName.LastIndexOf(".");
                    string ExtName = File1.PostedFile.FileName.Substring(ExtIndex);
                    string ServerFilePath = rtnPath + FileRandom + ExtName;
                    rtnFileName = rtnDbPath + FileRandom + ExtName;
                    File1.PostedFile.SaveAs(ServerFilePath);
                    return 1;
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// For WeChat
        /// </summary>
        /// <param name="File1"></param>
        /// <param name="rtnFileName"></param>
        /// <param name="clothGUID"></param>
        /// <returns></returns>
        public int RtnFileName(System.Web.UI.HtmlControls.HtmlInputFile File1, ref string rtnFileName)
        {
            if (CheckFileExtension(File1.PostedFile.InputStream))
            {
                string rtnPath = "";
                string rtnDbPath = "";
                if (CreateWeChatFolder(ref rtnPath, ref rtnDbPath))
                {
                    Random randobj = new Random();
                    string FileRandom = DateTime.Now.ToString("yyyyMMddhhmmss") + randobj.Next(9999);
                    int ExtIndex = File1.PostedFile.FileName.LastIndexOf(".");
                    string ExtName = File1.PostedFile.FileName.Substring(ExtIndex);
                    string ServerFilePath = rtnPath + FileRandom + ExtName;
                    rtnFileName = rtnDbPath + FileRandom + ExtName;
                    File1.PostedFile.SaveAs(ServerFilePath);
                    return 1;
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// For news
        /// </summary>
        /// <param name="File1"></param>
        /// <param name="rtnFileName"></param>
        /// <returns></returns>
        public int RtnNewsFileName(System.Web.UI.HtmlControls.HtmlInputFile File1, ref string rtnFileName)
        {
            if (CheckFileExtension(File1.PostedFile.InputStream))
            {
                string rtnPath = "";
                string rtnDbPath = "";
                if (CreateNewsFolder(ref rtnPath, ref rtnDbPath))
                {
                    Random randobj = new Random();
                    string FileRandom = DateTime.Now.ToString("yyyyMMddhhmmss") + randobj.Next(9999);
                    int ExtIndex = File1.PostedFile.FileName.LastIndexOf(".");
                    string ExtName = File1.PostedFile.FileName.Substring(ExtIndex);
                    string ServerFilePath = rtnPath + FileRandom + ExtName;
                    rtnFileName = rtnDbPath + FileRandom + ExtName;
                    File1.PostedFile.SaveAs(ServerFilePath);
                    return 1;
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                return -1;
            }
        }

        #endregion

        #region Create picture folder

        /// <summary>
        /// For TJW
        /// </summary>
        /// <param name="rtnFolderPath"></param>
        /// <param name="dbPath"></param>
        /// <param name="clothGUID"></param>
        /// <returns></returns>
        public bool CreatePictureFolder(ref string rtnFolderPath, ref string dbPath, string clothGUID)
        {
            bool result = true;
            string currentMonth = DateTime.Now.ToString("yyyyMM");
            string userImage = System.Web.HttpContext.Current.Server.MapPath("~/UserImages/");

            rtnFolderPath = userImage + currentMonth + "/" + clothGUID + "/";
            dbPath = "UserImages/" + currentMonth + "/" + clothGUID + "/";

            if (!Directory.Exists(userImage + currentMonth + "/" + clothGUID))
            {
                try
                {
                    Directory.CreateDirectory(userImage + currentMonth + "/" + clothGUID);
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// For WeChat
        /// </summary>
        /// <param name="rtnFolderPath"></param>
        /// <param name="dbPath"></param>
        /// <returns></returns>
        public bool CreateWeChatFolder(ref string rtnFolderPath, ref string dbPath)
        {
            bool result = true;
            string currentMonth = DateTime.Now.ToString("yyyyMM");
            string userImage = System.Web.HttpContext.Current.Server.MapPath("~/WeChatImages/");

            rtnFolderPath = userImage + currentMonth + "/";
            dbPath = "WeChatImages/" + currentMonth + "/";

            if (!Directory.Exists(userImage + currentMonth))
            {
                try
                {
                    Directory.CreateDirectory(userImage + currentMonth);
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// For news
        /// </summary>
        /// <param name="rtnFolderPath"></param>
        /// <param name="dbPath"></param>
        /// <returns></returns>
        public bool CreateNewsFolder(ref string rtnFolderPath, ref string dbPath)
        {
            bool result = true;
            string currentMonth = DateTime.Now.ToString("yyyyMM");
            string userImage = System.Web.HttpContext.Current.Server.MapPath("~/NewsImages/");

            rtnFolderPath = userImage + currentMonth + "/";
            dbPath = "NewsImages/" + currentMonth + "/";

            if (!Directory.Exists(userImage + currentMonth))
            {
                try
                {
                    Directory.CreateDirectory(userImage + currentMonth);
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }

        #endregion

        #region Check chinese number alpha
        public bool RegChineseNumberAlpha(string txtInput)
        {
            bool result = false;
            Regex r = new Regex(@"^[a-zA-Z0-9_]{6,16}$");
            if (r.IsMatch(txtInput))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region Check email
        public bool RegEmail(string txtInput)
        {
            bool result = false;
            Regex r = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            if (r.IsMatch(txtInput))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region Check password (6-16 alpha and number)
        public bool RegPassword(string txtInput)
        {
            bool result = false;
            Regex r = new Regex(@"^(?=.*?[a-zA-Z])(?=.*?[0-9])[a-zA-Z0-9]{6,16}");
            if (r.IsMatch(txtInput))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region Trasfer to JSON
        public string MStoJson(DataTable dt)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ArrayList dic = new ArrayList();
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> drow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    drow.Add(col.ColumnName, row[col.ColumnName]);
                }
                dic.Add(drow);
            }
            return jss.Serialize(dic);
        }
        #endregion

        #region Check phone number(11 number)
        public bool CheckPhoneNumber(string txtInput)
        {
            bool result = false;
            Regex r = new Regex(@"^\d{11}$");
            if (r.IsMatch(txtInput))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region Check phone number(18 number)
        public bool CheckClothGuidNum(string clothGuid)
        {
            bool result = false;
            Regex r = new Regex(@"^\d{18}$");
            if (r.IsMatch(clothGuid))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region Check  number
        public bool CheckNumber(string newsId)
        {
            bool result = false;
            Regex r = new Regex(@"^[1-9]\d*$");
            if (r.IsMatch(newsId))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region Check code
        public bool CheckCode(string txtInput)
        {
            bool result = false;
            Regex r = new Regex(@"^[1-9][0-9]{5}$");
            if (r.IsMatch(txtInput))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region Filter HTML
        public string CheckString(string Htmlstring)
        {
            //删 除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删 除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);

            //删 除与数据库相关的词 
            Htmlstring = Regex.Replace(Htmlstring, "select", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "insert", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "delete from", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "count''", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "drop table", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "truncate", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "asc", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "mid", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "char", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "exec master", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net localgroup administrators", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "and", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net user", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "or", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "-", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "delete", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "drop", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "script", "", RegexOptions.IgnoreCase);

            //特殊的字符
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            return Htmlstring;
        }
        #endregion

        #region Cut string
        public static string CutString(string oldString, int lengh, bool blExt)
        {
            string extensionStr = "";
            if (blExt)
            {
                extensionStr = "...";
            }
            if (oldString.Length > lengh)
            {
                return oldString.Substring(0, lengh) + extensionStr;
            }
            else
            {
                return oldString;
            }
        }
        #endregion

        #region Check img tag
        public string[] GetHtmlImageUrlList(string sHtmlText)
        {
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }
        #endregion

        #region Store count show
        public string GetStoreCountMsg(int number, int store)
        {
            if (number > store)
            {
                return "无货";

            }
            else
            {
                return "有货";
            }
        }
        #endregion

        #region Save point as two position
        public string SaveTwoPosition(string num)
        {
            int index = num.LastIndexOf('.');
            num = num.Substring(0, index + 3);
            return num;
        }
        #endregion
    }
}
