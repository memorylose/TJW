using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;
using TJW.WeChatHandler;

namespace TJW.UI.WeChat
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WeChatHanlde();
        }

        #region WeChat handle

        private void WeChatHanlde()
        {
            //set wechat menu
            //此绑定经过测试，只需要绑定一次即可。
            //MyMenu();

            //cutomer operation
            CustomerOperation();
        }

        #endregion

        #region Customer operation

        private void CustomerOperation()
        {
            try
            {
                var result = "";
                var document = new StreamReader(Request.InputStream).ReadToEnd();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(document);

                var serverAccount = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();
                var clientOpenId = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
                var MsgType = doc.GetElementsByTagName("MsgType")[0].InnerText.Trim();

                //text type
                if (string.Equals(MsgType.Trim(), "text"))
                {
                    string MsgContent = doc.GetElementsByTagName("Content")[0].InnerText.Trim();
                }

                //event type
                if (string.Equals(MsgType.Trim(), "event"))
                {
                    string strType = string.Empty;
                    var eventKey = doc.GetElementsByTagName("EventKey")[0].InnerText.Trim();
                    var eventName = doc.GetElementsByTagName("Event")[0].InnerText.Trim();

                    //首次关注
                    if (string.Equals(eventName, "subscribe"))
                    {
                        result = ResponseTextMsg("天街网欢迎您的加入，更多体验请关注tianjiew.com", clientOpenId, serverAccount);
                    }
                    //取消关注
                    if (string.Equals(eventKey, "unsubscribe"))
                    {

                    }

                    //menu click
                    if (string.Equals(eventName.Trim(), "CLICK"))
                    {
                        switch (eventKey)
                        {
                            //时尚资讯
                            case ConstValue.WCMenuKey1:
                                strType = "1";
                                break;
                            //最新活动
                            case ConstValue.WCMenuKey2:
                                strType = "2";
                                break;
                            //最新活动
                            case ConstValue.WCMenuKey3_1:
                                strType = "3";
                                break;
                            //最新活动
                            case ConstValue.WCMenuKey3_2:
                                strType = "4";
                                break;

                        }
                        result = ResponsePicTextMsg(clientOpenId, serverAccount, strType);
                    }
                }

                HttpContext.Current.Response.Write(result);
            }
            catch
            {
                if (Request.HttpMethod.ToUpper() == "GET")
                {
                    if (WeChatAPI.CheckSignature(Context, "20141120CASS"))
                    {
                        if (Request.QueryString["echostr"] != null)
                        {
                            //如果验证成功，返回echostr
                            Response.Write(Request.QueryString["echostr"]);
                            Response.End();
                        }
                    }
                }
            }
        }

        #endregion

        #region WeChat return pictext message

        public string ResponsePicTextMsg(string clientUser, string serverUser, string typeId)
        {
            SH_WeChat _shWeChat = new SH_WeChat();
            DataSet ds = _shWeChat.GetWeChatMessage("type", typeId);
            string basePath = "http://www.tianjiew.com/";

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks);
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            //ArticleCount 必须与下面item数量相同
            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", ds.Tables[0].Rows.Count);
            sb.AppendFormat("<Articles>");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.AppendFormat("<item>");
                sb.AppendFormat("<Title><![CDATA[{0}]]></Title>", "" + ds.Tables[0].Rows[i][0] + "");
                sb.AppendFormat("<Description><![CDATA[{0}]]></Description>", "");
                sb.AppendFormat("<PicUrl><![CDATA[{0}]]></PicUrl>", basePath + ds.Tables[0].Rows[i][1]);
                sb.AppendFormat("<Url><![CDATA[{0}]]></Url>", basePath + "WeChat/Detail.aspx?wcId=" + ds.Tables[0].Rows[i][2]);
                sb.AppendFormat("</item>");
            }

            sb.AppendFormat("</Articles>");
            sb.AppendFormat("</xml>");
            return sb.ToString();
        }

        #endregion

        #region Return text

        public static string ResponseTextMsg(string content, string clientUser, string serverUser)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks);
            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", content);
            sb.AppendFormat("</xml>");
            return sb.ToString();
        }
        #endregion

        #region Set wechat menu

        public void MyMenu()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" {");
            strBuilder.Append("\"button\": [");
            strBuilder.Append("{");
            strBuilder.Append("\"type\": \"view\",");
            strBuilder.Append("\"name\": \"" + ConstValue.WCMenuName1 + "\",");
            strBuilder.Append("\"url\": \"http://www.tianjiew.com\",");
            strBuilder.Append("\"key\": \"" + ConstValue.WCMenuKey1 + "\"");
            strBuilder.Append("},");
            strBuilder.Append("{");
            strBuilder.Append("\"type\": \"click\",");
            strBuilder.Append("\"name\": \"" + ConstValue.WCMenuName2 + "\",");
            strBuilder.Append("\"key\": \"" + ConstValue.WCMenuKey2 + "\"");
            strBuilder.Append("},");
            strBuilder.Append("{");
            strBuilder.Append("\"type\": \"click\",");
            strBuilder.Append("\"name\": \"" + ConstValue.WCMenuName3_1 + "\",");
            strBuilder.Append("\"key\": \"" + ConstValue.WCMenuKey3_1 + "\"");
            strBuilder.Append("}");

            //strBuilder.Append("{");
            //strBuilder.Append(" \"name\": \"关于天街\",");
            //strBuilder.Append("\"sub_button\": [");
            //strBuilder.Append("{");
            //strBuilder.Append("\"type\": \"click\",");
            //strBuilder.Append("\"name\": \"" + ConstValue.WCMenuName3_1 + "\",");
            //strBuilder.Append("\"key\": \"" + ConstValue.WCMenuKey3_1 + "\"");
            //strBuilder.Append("}");
            ////strBuilder.Append("{");
            ////strBuilder.Append("\"type\": \"click\",");
            ////strBuilder.Append("\"name\": \"" + ConstValue.WCMenuName3_2 + "\",");
            ////strBuilder.Append("\"key\": \"" + ConstValue.WCMenuKey3_2 + "\"");
            ////strBuilder.Append("}");
            //strBuilder.Append(" ]");
            //strBuilder.Append(" }");
            strBuilder.Append(" ]");
            strBuilder.Append("}");

            LogTools log = new LogTools();
            log.CheckFile(Server.MapPath("/Test.txt"));

            GroupUsers _groupUsers = new GroupUsers();
            string i = _groupUsers.SetMenuHttpPost("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + _groupUsers.IsExistAccess_Token(false) + "", strBuilder.ToString());

            log.WriteLog("i:" + i, Server.MapPath("/Test.txt"));

            //if result is not ok, do it again 
            MenuRtnMsg rtnMsg = SeriaFunc.JsonDeserialize<MenuRtnMsg>(i);
            if (!string.Equals(rtnMsg.errmsg, ConstValue.weChatRtnCorrectMsg))
            {
                i = _groupUsers.SetMenuHttpPost("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + _groupUsers.IsExistAccess_Token(true) + "", strBuilder.ToString());
            }
            log.WriteLog("i:" + i, Server.MapPath("/Test.txt"));

            Response.Write(i);
        }

        #endregion

        private void WeChatTest()
        {
            LogTools log = new LogTools();
            log.CheckFile(Server.MapPath("/Test.txt"));

            //公众平台上开发者设置的token, appID, EncodingAESKey
            string sToken = "20141120CASS";
            string sAppID = "wxba5fc0189fd21928";
            string sEncodingAESKey = "eIzorQUdzppJ32gUm9BB6eboqMYeDWaxgyUyA5VxRIO";
            log.WriteLog("start:", Server.MapPath("/Test.txt"));

            WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(sToken, sEncodingAESKey, sAppID);

            /* 1. 对用户回复的数据进行解密。
            * 用户回复消息或者点击事件响应时，企业会收到回调消息，假设企业收到的推送消息：
            * 	POST /cgi-bin/wxpush? msg_signature=477715d11cdb4164915debcba66cb864d751f3e6&timestamp=1409659813&nonce=1372623149 HTTP/1.1
               Host: qy.weixin.qq.com
               Content-Length: 613
            *
            * 	<xml>
                   <ToUserName><![CDATA[wx5823bf96d3bd56c7]]></ToUserName>
                   <Encrypt><![CDATA[RypEvHKD8QQKFhvQ6QleEB4J58tiPdvo+rtK1I9qca6aM/wvqnLSV5zEPeusUiX5L5X/0lWfrf0QADHHhGd3QczcdCUpj911L3vg3W/sYYvuJTs3TUUkSUXxaccAS0qhxchrRYt66wiSpGLYL42aM6A8dTT+6k4aSknmPj48kzJs8qLjvd4Xgpue06DOdnLxAUHzM6+kDZ+HMZfJYuR+LtwGc2hgf5gsijff0ekUNXZiqATP7PF5mZxZ3Izoun1s4zG4LUMnvw2r+KqCKIw+3IQH03v+BCA9nMELNqbSf6tiWSrXJB3LAVGUcallcrw8V2t9EL4EhzJWrQUax5wLVMNS0+rUPA3k22Ncx4XXZS9o0MBH27Bo6BpNelZpS+/uh9KsNlY6bHCmJU9p8g7m3fVKn28H3KDYA5Pl/T8Z1ptDAVe0lXdQ2YoyyH2uyPIGHBZZIs2pDBS8R07+qN+E7Q==]]></Encrypt>
               </xml>
            */
            string sReqMsgSig = Request.QueryString["signature"];
            string sReqTimeStamp = Request.QueryString["timestamp"];
            string sReqNonce = Request.QueryString["nonce"];

            string sReqData = "<xml><ToUserName><![CDATA[wx5823bf96d3bd56c7]]></ToUserName><Encrypt><![CDATA[RypEvHKD8QQKFhvQ6QleEB4J58tiPdvo+rtK1I9qca6aM/wvqnLSV5zEPeusUiX5L5X/0lWfrf0QADHHhGd3QczcdCUpj911L3vg3W/sYYvuJTs3TUUkSUXxaccAS0qhxchrRYt66wiSpGLYL42aM6A8dTT+6k4aSknmPj48kzJs8qLjvd4Xgpue06DOdnLxAUHzM6+kDZ+HMZfJYuR+LtwGc2hgf5gsijff0ekUNXZiqATP7PF5mZxZ3Izoun1s4zG4LUMnvw2r+KqCKIw+3IQH03v+BCA9nMELNqbSf6tiWSrXJB3LAVGUcallcrw8V2t9EL4EhzJWrQUax5wLVMNS0+rUPA3k22Ncx4XXZS9o0MBH27Bo6BpNelZpS+/uh9KsNlY6bHCmJU9p8g7m3fVKn28H3KDYA5Pl/T8Z1ptDAVe0lXdQ2YoyyH2uyPIGHBZZIs2pDBS8R07+qN+E7Q==]]></Encrypt></xml>";
            string sMsg = "";  //解析之后的明文
            int ret = 0;
            ret = wxcpt.DecryptMsg(sReqMsgSig, sReqTimeStamp, sReqNonce, sReqData, ref sMsg);
            log.WriteLog("ret:" + ret, Server.MapPath("/Test.txt"));

            if (ret != 0)
            {
                System.Console.WriteLine("ERR: Decrypt fail, ret: " + ret);
                return;
            }


            System.Console.WriteLine(sMsg);

            log.WriteLog("sMsg:" + sMsg, Server.MapPath("/Test.txt"));

            /*
             * 2. 企业回复用户消息也需要加密和拼接xml字符串。
             * 假设企业需要回复用户的消息为：
             * 		<xml>
             * 		<ToUserName><![CDATA[mycreate]]></ToUserName>
             * 		<FromUserName><![CDATA[wx5823bf96d3bd56c7]]></FromUserName>
             * 		<CreateTime>1348831860</CreateTime>
                    <MsgType><![CDATA[text]]></MsgType>
             *      <Content><![CDATA[this is a test]]></Content>
             *      <MsgId>1234567890123456</MsgId>
             *      </xml>
             * 生成xml格式的加密消息过程为：
             */
            string sRespData = "<xml><ToUserName><![CDATA[mycreate]]></ToUserName><FromUserName><![CDATA[wx582测试一下中文的情况，消息长度是按字节来算的396d3bd56c7]]></FromUserName><CreateTime>1348831860</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[this is a test]]></Content><MsgId>1234567890123456</MsgId></xml>";
            string sEncryptMsg = ""; //xml格式的密文
            ret = wxcpt.EncryptMsg(sRespData, sReqTimeStamp, sReqNonce, ref sEncryptMsg);
            System.Console.WriteLine("sEncryptMsg");
            System.Console.WriteLine(sEncryptMsg);

            log.WriteLog("sEncryptMsg:" + sEncryptMsg, Server.MapPath("/Test.txt"));


            /*测试：
             * 将sEncryptMsg解密看看是否是原文
             * */
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sEncryptMsg);
            XmlNode root = doc.FirstChild;
            string sig = root["MsgSignature"].InnerText;
            string enc = root["Encrypt"].InnerText;
            string timestamp = root["TimeStamp"].InnerText;
            string nonce = root["Nonce"].InnerText;
            string stmp = "";
            ret = wxcpt.DecryptMsg(sig, timestamp, nonce, sEncryptMsg, ref stmp);
            System.Console.WriteLine("stemp");
            System.Console.WriteLine(stmp + ret);
            log.WriteLog("stmp+ret:" + stmp + ret, Server.MapPath("/Test.txt"));


            return;
        }
    }
}