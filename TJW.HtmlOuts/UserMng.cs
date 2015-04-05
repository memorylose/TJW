using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.HtmlOuts
{
    public class UserMng
    {
        #region Order html

        private string SetPath()
        {
            return System.Web.HttpContext.Current.Server.MapPath("/Test.txt");
        }

        /// <summary>
        /// For my order
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public string HtmlMyOrder(string userId, string statusId)
        {
            DataSet ds = GetOrder(userId, statusId);
            StringBuilder strBuilder = new StringBuilder();
            SH_AdminUser _adminUser = new SH_AdminUser();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                decimal total = 0;
                string[] split = ds.Tables[0].Rows[i][1].ToString().Split('/');
                string[] statusSplit = ds.Tables[0].Rows[i][2].ToString().Split(',');

                strBuilder.Append("<div class=\"order_div\">");

                int k = -1;
                foreach (string s in split)
                {
                    k++;

                    string[] detail = s.Split(',');
                    DataSet detailDs = null;
                    string ColorType = string.Empty;
                    string YearSize = string.Empty;

                    switch (detail[0].ToString().Substring(0, 1))
                    {
                        case "C":
                            detailDs = GetClothInfo(detail[0].ToString());
                            ColorType = "颜色：";
                            YearSize = "尺寸：";
                            break;
                        case "T":
                            detailDs = GetTeaInfo(detail[0].ToString());
                            ColorType = "类别：";
                            YearSize = "年限：";
                            break;
                    }

                    string dataColorType = "";
                    string dataYearSize = "";

                    if (detailDs.Tables[0].Rows.Count > 0)
                    {
                        //get custom bh(the third number)
                        dataColorType = detailDs.Tables[0].Rows[0][3].ToString();
                        dataYearSize = detailDs.Tables[0].Rows[0][4].ToString();
                    }

                    if (detail.Length > 2)
                    {
                        ColorType = "编号：";
                        YearSize = "";
                        SH_Index _shIndex = new SH_Index();
                        dataColorType = _shIndex.GetCartBH(detail[2].ToString());
                        dataYearSize = "";
                    }

                    strBuilder.Append("<div class=\"order_div_top\">");
                    strBuilder.Append("<div class=\"order_div_word\">订单编号：" + ds.Tables[0].Rows[i][0].ToString() + "</div>");
                    strBuilder.Append("<div class=\"order_div_word_time\">成交时间：" + ds.Tables[0].Rows[i][3].ToString() + "</div>");
                    strBuilder.Append("<div class=\"order_div_word_1\">数量</div>");
                    strBuilder.Append("<div class=\"order_div_word_1\">价钱</div>");
                    strBuilder.Append("<div class=\"order_div_word_2\">状态</div>");
                    strBuilder.Append("</div>");

                    strBuilder.Append("<div class=\"order_detail\">");
                    strBuilder.Append("<div class=\"order_detail_1\">");
                    strBuilder.Append("<a href=\"/Detail/" + detailDs.Tables[0].Rows[0][6].ToString() + "\"><img src=\"/" + detailDs.Tables[0].Rows[0][2].ToString() + "\" /></a>");
                    strBuilder.Append("</div>");
                    strBuilder.Append("<div class=\"order_detail_2\">");
                    strBuilder.Append("<div class=\"order_detail_2_top\"><a href=\"/Detail/" + detailDs.Tables[0].Rows[0][6].ToString() + "\">" + detailDs.Tables[0].Rows[0][0].ToString() + "</a></div>");
                    strBuilder.Append("<div class=\"order_detail_2_bottom\">" + ColorType + " " + dataColorType + " " + YearSize + " " + dataYearSize + " </div>");
                    strBuilder.Append("</div>");
                    strBuilder.Append("<div class=\"order_detail_3\">" + detail[1].ToString() + "</div>");
                    strBuilder.Append("<div class=\"order_detail_4\">￥" + detailDs.Tables[0].Rows[0][1].ToString() + "</div>");
                    strBuilder.Append("<div class=\"order_detail_5\">" + _adminUser.GetStatusName(Convert.ToInt32(statusSplit[k])) + "</div>");
                    strBuilder.Append("<div class=\"clear\"></div>");
                    strBuilder.Append("</div>");
                    total += decimal.Parse(detailDs.Tables[0].Rows[0][1].ToString());
                }
                strBuilder.Append("<div class=\"order_bottom\">");
                strBuilder.Append("<div class=\"order_bottom_del\"><a href=\"/Order.aspx?o=" + ds.Tables[0].Rows[i][0] + "\">查看订单</a><a href=\"/UM/MyOrder.aspx?del=" + ds.Tables[0].Rows[i]["OrderId"] + "\">删除订单</a></div>");
                strBuilder.Append("<div class=\"order_bottom_total\">总计：<span>￥" + total + "</span></div>");
                strBuilder.Append("</div>");
                strBuilder.Append("</div>");
            }

            return strBuilder.ToString();
        }

        /// <summary>
        /// For order
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public string HtmlOrder(string stuffInfo, string status, ref decimal total)
        {
            CommonTools _commonTools = new CommonTools();
            StringBuilder strBuilder = new StringBuilder();
            SH_AdminUser _adminUser = new SH_AdminUser();

            total = 0;
            string[] split = stuffInfo.Split('/');
            string[] statusSplit = status.Split(',');

            int k = -1;
            foreach (string s in split)
            {
                k++;
                string[] detail = s.Split(',');
                DataSet detailDs = null;
                string ColorType = string.Empty;
                string YearSize = string.Empty;
                string url = "";
                string statusName = _adminUser.GetStatusName(Convert.ToInt32(statusSplit[k]));


                switch (detail[0].ToString().Substring(0, 1))
                {
                    case "C":
                        detailDs = GetClothInfo(detail[0].ToString());
                        ColorType = "颜色：";
                        YearSize = "尺寸：";
                        url = "/Detail/" + detailDs.Tables[0].Rows[0][6];
                        break;
                    case "T":
                        detailDs = GetTeaInfo(detail[0].ToString());
                        ColorType = "类别：";
                        YearSize = "年限：";
                        url = "/Tea/D/" + detailDs.Tables[0].Rows[0][6];
                        break;
                }

                //get custom bh(the third number)
                string dataColorType = detailDs.Tables[0].Rows[0][3].ToString();
                string dataYearSize = detailDs.Tables[0].Rows[0][4].ToString();
                if (detail.Length > 2)
                {
                    ColorType = "编号：";
                    YearSize = "";
                    SH_Index _shIndex = new SH_Index();
                    dataColorType = _shIndex.GetCartBH(detail[2].ToString());
                    dataYearSize = "";
                }

                strBuilder.Append("<table class=\"order_table\" cellspacing=\"0\">");
                strBuilder.Append("<thead>");
                strBuilder.Append("<tr>");
                strBuilder.Append("<td width=\"290px\">商品</td>");
                strBuilder.Append("<td width=\"100px\">单价(元)</td>");
                strBuilder.Append("<td width=\"100px\">数量</td>");
                strBuilder.Append("<td width=\"100px\">库存状态</td>");
                strBuilder.Append("<td width=\"100px\">订单额(元)</td>");
                strBuilder.Append("<td width=\"100px\">订单状态</td>");
                strBuilder.Append("</tr>");
                strBuilder.Append("</thead>");
                strBuilder.Append("<tbody>");
                strBuilder.Append("<tr>");
                strBuilder.Append("<td>");
                strBuilder.Append("<div class=\"order_table_sp_left\">");
                strBuilder.Append("<a href=\"" + url + "\">");
                strBuilder.Append("<img src=\"" + detailDs.Tables[0].Rows[0][2].ToString() + "\" /></a>");
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"order_table_sp_right\">");
                strBuilder.Append("<div class=\"order_table_sp_right_title\"><a href=\"" + url + "\">" + detailDs.Tables[0].Rows[0][0].ToString() + "</a></div>");
                strBuilder.Append("<div class=\"order_table_sp_right_size\">" + ColorType + " " + dataColorType + " </div>");
                strBuilder.Append("<div class=\"order_table_sp_right_size\">" + YearSize + " " + dataYearSize + "</div>");
                strBuilder.Append("</div>");
                strBuilder.Append("</td>");
                strBuilder.Append("<td>");
                strBuilder.Append("<div class=\"order_table_sp_right_div\">" + detailDs.Tables[0].Rows[0][1].ToString() + "</div>");
                strBuilder.Append("</td>");
                strBuilder.Append("<td>");
                strBuilder.Append("<div class=\"order_table_sp_right_div\">" + detail[1].ToString() + "</div>");
                strBuilder.Append("</td>");
                strBuilder.Append("<td>");
                strBuilder.Append("<div class=\"order_table_sp_right_div\">" + _commonTools.GetStoreCountMsg(Convert.ToInt32(detail[1]), Convert.ToInt32(detailDs.Tables[0].Rows[0][5])) + "</div>");
                strBuilder.Append("</td>");
                strBuilder.Append("<td>");
                strBuilder.Append("<div class=\"order_table_sp_right_div\">" + decimal.Parse(detailDs.Tables[0].Rows[0][1].ToString()) * Convert.ToInt32(detail[1]) + "</div>");
                strBuilder.Append("</td>");
                strBuilder.Append("<td>");
                strBuilder.Append("<div class=\"order_table_sp_right_div\">" + statusName + "</div>");
                strBuilder.Append("</td>");
                strBuilder.Append("</tr>");
                strBuilder.Append("</tbody>");
                strBuilder.Append("</table>");
                total += decimal.Parse(detailDs.Tables[0].Rows[0][1].ToString()) * Convert.ToInt32(detail[1]);
            }

            return strBuilder.ToString();
        }

        #endregion

        #region HtmlSql

        #region Get order

        public DataSet GetOrder(string userId, string statusId)
        {
            string strSql = "SELECT OrderNumber,StuffInfo,OrderStatusId,CreateCartDate,OrderId FROM TJW_Order A WHERE CreateUserId = @CreateUserId " + statusId + " ORDER BY OrderId DESC";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@CreateUserId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = userId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }

        #endregion

        #region Get cloth info

        public DataSet GetClothInfo(string stuffGuid)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT  ClothName,Price,");
            strBuilder.Append(" (SELECT TOP 1 PicturePath FROM TJW_Picture B WHERE B.ClothGUID = A.ClothGuid AND PictureTypeId = 7 ORDER BY PictureId DESC) AS PicPath,");
            strBuilder.Append(" (SELECT ColorName FROM TJW_Color C WHERE C.ColorId = A.ColorId) AS ColorName,");
            strBuilder.Append(" (SELECT SizeName FROM TJW_Size D WHERE D.SizeId = A.SizeId) AS SizeName,StoreCount,ClothGuid,CustomBHId");
            strBuilder.Append(" FROM TJW_Cloth A WHERE StuffUGUID = '" + stuffGuid + "'");
            return SqlHelper.ExcuteDataSet(strBuilder.ToString());
        }

        #endregion

        #region Get tea info

        public DataSet GetTeaInfo(string stuffGuid)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT TeaName,TeaPrice,");
            strBuilder.Append(" (SELECT PicturePath FROM TJW_TeaPicture B WHERE B.TeaStuffGUID = '" + stuffGuid + "' AND PictureTypeId = 2) AS PicPath,");
            strBuilder.Append(" (SELECT TypeName FROM TJW_TeaType C WHERE C.TypeId = A.TeaTypeId) AS TypeName,");
            strBuilder.Append(" TeaYear,TeaCount,StuffUGUID");
            strBuilder.Append(" FROM TJW_Tea A WHERE StuffUGUID = '" + stuffGuid + "'");
            return SqlHelper.ExcuteDataSet(strBuilder.ToString());
        }

        #endregion

        #region Myorder del

        public void DeleteMyOrder(int orderId)
        {
            string strSql = "DELETE FROM TJW_Order WHERE OrderId = @OrderId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = orderId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        #endregion

        #endregion
    }
}
