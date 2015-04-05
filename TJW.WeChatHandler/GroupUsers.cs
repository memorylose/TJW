/* ======================================================================== 
* Author：Cass 
* Time：9/26/2014 11:03:57 AM 
* Description:  
* ======================================================================== 
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using TJW.Model;
using TJW.Utils;

namespace TJW.WeChatHandler
{
    public class GroupUsers
    {
        /// <summary>
        /// Get user list
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="token"></param>
        /// <param name="groupId"></param>
        /// <param name="group_name"></param>
        /// <param name="count"></param>
        /// <param name="groupdata"></param>
        public void GetGroupList(CookieContainer cc, string token, string groupId, string group_name, string count, ref List<SingleGroup> groupdata)
        {
            SingleGroup obj_single = new SingleGroup();
            obj_single.group_name = group_name;
            string TotalUser;
            if (count != "0")
            {
                TotalUser = count;
            }
            else
            {
                return;
            }
            string Url = "https://mp.weixin.qq.com/cgi-bin/contactmanage?t=user/index&pagesize=" + TotalUser + "&pageidx=0&type=0&groupid=" + groupId.Trim() + "&token=" + token + "&lang=zh_CN";
            HttpWebRequest webRequest2 = (HttpWebRequest)WebRequest.Create(Url);
            webRequest2.CookieContainer = cc;
            webRequest2.ContentType = "text/html; charset=UTF-8";
            webRequest2.Method = "GET";
            webRequest2.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
            webRequest2.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse response2 = (HttpWebResponse)webRequest2.GetResponse();
            StreamReader sr2 = new StreamReader(response2.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            string text2 = sr2.ReadToEnd();
            MatchCollection mcJsonData;
            Regex rexJsonData = new Regex(@"(?<=friendsList : \({""contacts"":).*(?=}\).contacts)");
            mcJsonData = rexJsonData.Matches(text2);
            if (mcJsonData.Count != 0)
            {
                JArray JsonArray = (JArray)JsonConvert.DeserializeObject(mcJsonData[0].Value);
                obj_single.groupdata = JsonArray;
                groupdata.Add(obj_single);
            }
        }

        /// <summary>
        /// get group info
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public List<SingleGroup> getAllGroupInfo(CookieContainer cc, string token)
        {
            try
            {
                string Url = "https://mp.weixin.qq.com/cgi-bin/contactmanage?t=user/index&pagesize=10&pageidx=0&type=0&groupid=0&token=" + token + "&lang=zh_CN";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
                webRequest.CookieContainer = cc;
                webRequest.ContentType = "text/html; charset=UTF-8";
                webRequest.Method = "GET";
                webRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                string text = sr.ReadToEnd();
                MatchCollection mcGroup;
                Regex GroupRex = new Regex(@"(?<=""groups"":).*(?=\}\).groups)");
                mcGroup = GroupRex.Matches(text);
                List<SingleGroup> allgroupinfo = new List<SingleGroup>();
                if (mcGroup.Count != 0)
                {
                    JArray groupjarray = (JArray)JsonConvert.DeserializeObject(mcGroup[0].Value);
                    for (int i = 0; i < groupjarray.Count; i++)
                    {
                        GetGroupList(cc, token, groupjarray[i]["id"].ToString(), groupjarray[i]["name"].ToString(), groupjarray[i]["cnt"].ToString(), ref allgroupinfo);
                    }
                }
                return allgroupinfo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        /// <summary>
        /// POST:check username and password
        /// </summary>
        /// <param name="paramsData"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string CheckLoginHttpPost(string paramsData, string url, CookieContainer cc)
        {

            //CookieContainer cc = new CookieContainer();//接收缓存
            byte[] byteArray = Encoding.UTF8.GetBytes(paramsData); // 转化
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);  //新建一个WebRequest对象用来请求或者响应url
            webRequest.CookieContainer = cc;                                      //保存cookie                                      //保存cookie                  
            webRequest.Method = "POST";                                          //请求方式是POST
            webRequest.ContentType = "application/x-www-form-urlencoded";       //请求的内容格式为application/x-www-form-urlencoded
            webRequest.Referer = "https://mp.weixin.qq.com/";//request的referer地址，网络上的版本因为这句没写所以会出现invalid referrer
            webRequest.ContentLength = byteArray.Length;
            Stream newStream = webRequest.GetRequestStream();           //返回用于将数据写入 Internet 资源的 Stream。
            newStream.Write(byteArray, 0, byteArray.Length);    //写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// POST:Set menu 
        /// </summary>
        /// <param name="posturl"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public string SetMenuHttpPost(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        #region Get access token

        public Access_token GetAccessToken()
        {
            string appid = ConstValue.Appid;
            string secret = ConstValue.AppSecret;
            string strUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
            Access_token _accessToken = new Access_token();

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strUrl);

            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                _accessToken = SeriaFunc.ParseFromJson<Access_token>(content);
            }
            return _accessToken;
        }

        #endregion

        #region Check access token expired

        public string IsExistAccess_Token(bool again)
        {
            string Token = string.Empty;
            DateTime YouXRQ;
            string filepath = System.Web.HttpContext.Current.Server.MapPath("~/XML/WeChat.xml");
            StreamReader str = new StreamReader(filepath, System.Text.Encoding.UTF8);
            XmlDocument xml = new XmlDocument();
            xml.Load(str);
            str.Close();
            str.Dispose();
            Token = xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText;
            YouXRQ = Convert.ToDateTime(xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText);

            if (again)
            {
                YouXRQ = DateTime.Now.AddYears(-100);
            }

            if (DateTime.Now > YouXRQ)
            {
                DateTime _youxrq = DateTime.Now;
                Access_token mode = GetAccessToken();
                xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText = mode.access_token;
                _youxrq = _youxrq.AddSeconds(int.Parse(mode.expires_in));
                xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText = _youxrq.ToString();
                xml.Save(filepath);
                Token = mode.access_token;
            }
            return Token;
        }

        #endregion
    }
}
