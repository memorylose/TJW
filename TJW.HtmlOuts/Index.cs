using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Routing;
using TJW.Model;
using TJW.SqlHandler;
using TJW.Utils;

namespace TJW.HtmlOuts
{
    public class Index
    {
        #region Below turning image
        public string BelowTurningImage()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImage(4, 10);
            foreach (Picture model in list)
            {
                strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a>");
            }
            return strBuilder.ToString();
        }
        #endregion

        #region turning image
        public string TurningImage()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImageWithWord(10, 9);
            string href = "";
            string word = "";
            foreach (Picture model in list)
            {
                //check custom picture href
                if (model.PicHref != "")
                {
                    href = model.PicHref;
                }
                else
                {
                    href = GVPath(model.ClothGUID);
                }
                //check custom word
                if (model.PicWord != "")
                {
                    word = model.PicWord;
                }
                else
                {
                    word = model.ClothName;
                }
                strBuilder.Append(" <li><a href=\"" + href + "\"  target=\"_blank\" ><img src=\"" + model.PicturePath + "\" alt=\"\" /></a><a href=\"" + href + "\"  target=\"_blank\" ><p class=\"caption\">" + word + "</p></a></li>");
            }
            return strBuilder.ToString();
        }

        #endregion

        #region Author suggest
        public string AuthorSuggest()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImage(1, 11);
            foreach (Picture model in list)
            {
                strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a>");
            }
            return strBuilder.ToString();
        }

        #endregion

        #region Current season new product top
        public string CurrentSeasonTop()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImage(1, 1);
            foreach (Picture model in list)
            {
                strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a>");
            }
            return strBuilder.ToString();
        }

        #endregion

        #region Current season new product
        public string CurrentSeason()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImage(10, 2);
            foreach (Picture model in list)
            {
                strBuilder.Append("<div class=\"second_main_left_detail_pic\"><a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a></div>");
            }
            return strBuilder.ToString();
        }

        #endregion

        #region Singel product
        public string SingleProduct()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImage(8, 3);
            int j = 0;
            foreach (Picture model in list)
            {
                j = j + 1;
                if (j == 1 || j == 3 || j == 5 || j == 7)
                {
                    strBuilder.Append("<div class=\"second_main_left_second_div\">");
                }
                strBuilder.Append("<div class=\"tj_div_" + j + "\"><a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a></div>");
                if (j == 2 || j == 4 || j == 6 || j == 8)
                {
                    strBuilder.Append("</div>");
                }
            }
            return strBuilder.ToString();
        }

        #endregion

        #region Sale product top
        public string SaleProductTop()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImageWithWord(1, 4);
            foreach (Picture model in list)
            {
                strBuilder.Append("<div class=\"third_left_top_pic\"><a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a></div>");
                strBuilder.Append("<div class=\"third_left_top_word\">");
                strBuilder.Append("<div class=\"third_left_top_word_left\">");
                strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" >" + model.ClothName + "</a>");
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"third_left_top_word_right\">");
                strBuilder.Append("" + model.Price + "");
                strBuilder.Append("</div>");
                strBuilder.Append("</div>");
            }
            return strBuilder.ToString();
        }

        #endregion

        #region Sale product
        public string SaleProduct(int saleType)
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImageWithWord(8, 5);
            int i = 0;
            foreach (Picture model in list)
            {
                i++;
                if (saleType == 0)
                {
                    if (i < 3)
                    {
                        strBuilder.Append("<div class=\"third_left_bottom_div\">");
                        strBuilder.Append("<div class=\"third_left_bottom_div_pic\">");
                        strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a>");
                        strBuilder.Append("</div>");
                        strBuilder.Append("<div class=\"third_left_bottom_div_word\">");
                        strBuilder.Append("<div class=\"third_left_bottom_div_word_left\">");
                        strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" >" + CommonTools.CutString(model.ClothName, 10, false) + "</a>");
                        strBuilder.Append("</div>");
                        strBuilder.Append("<div class=\"third_left_bottom_div_word_money\">");
                        strBuilder.Append("" + model.Price + "");
                        strBuilder.Append("</div>");
                        strBuilder.Append("<div class=\"clear\"></div>");
                        strBuilder.Append("</div>");
                        strBuilder.Append("</div>");
                    }
                }
                if (saleType == 1)
                {
                    if (i > 2)
                    {
                        strBuilder.Append(" <div class=\"third_left_bottom_div\">");
                        strBuilder.Append(" <div class=\"third_left_bottom_div_pic\">");
                        strBuilder.Append(" <a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a>");
                        strBuilder.Append(" </div>");
                        strBuilder.Append(" <div class=\"third_left_bottom_div_word\">");
                        strBuilder.Append(" <div class=\"third_left_bottom_div_word_left\">");
                        strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" > " + CommonTools.CutString(model.ClothName, 10, false) + "</a>");
                        strBuilder.Append(" </div>");
                        strBuilder.Append(" <div class=\"third_left_bottom_div_word_money\">");
                        strBuilder.Append(" " + model.Price + "");
                        strBuilder.Append(" </div>");
                        strBuilder.Append(" <div class=\"clear\"></div>");
                        strBuilder.Append(" </div>");
                        strBuilder.Append(" </div>");
                    }
                }
            }
            return strBuilder.ToString();
        }

        #endregion

        #region Hot suggest top
        public string HotSuggestTop()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImageWithWordCondition(1, 12, " AND SUBSTRING(IsTj,1,1) = '1' ");
            foreach (Picture model in list)
            {
                strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a>");
            }
            return strBuilder.ToString();
        }
        #endregion

        #region Hot suggest
        public string HotSuggest()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImageWithWordCondition(8, 13, " AND SUBSTRING(IsTj,2,1) = '1' ");
            int j = 0;

            foreach (Picture model in list)
            {
                j = j + 1;
                string exceptOneSty = "_2";
                string exceptOne = j.ToString();

                if (j == 1)
                {
                    exceptOneSty = "";
                    exceptOne = "";
                }

                strBuilder.Append("<div class=\"second_main_right_main\">");
                strBuilder.Append("<div class=\"second_main_right_main_first" + exceptOneSty + "\">");
                strBuilder.Append(exceptOne);
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"second_main_right_main_second\">");
                strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a>");
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"second_main_right_main_third\">");
                strBuilder.Append("<div class=\"second_main_right_main_third_word\">");
                strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" >" + model.ClothName + "</a>");
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"second_main_right_main_third_money\">");
                strBuilder.Append("￥" + model.Price + "");
                strBuilder.Append("</div>");
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"clear\"></div>");
                strBuilder.Append("</div>");

            }
            return strBuilder.ToString();
        }
        #endregion

        #region Hot product top
        public string HotProductTop()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImageWithWordCondition(1, 14, " AND SUBSTRING(IsTj,3,1) = '1' ");
            foreach (Picture model in list)
            {
                strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a>");
            }
            return strBuilder.ToString();
        }
        #endregion

        #region Hot product
        public string HotProduct()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImageWithWordCondition(9, 15, " AND SUBSTRING(IsTj,4,1) = '1' ");
            int j = 0;

            foreach (Picture model in list)
            {
                j = j + 1;
                string exceptOneSty = "_2";
                string exceptOne = j.ToString();

                if (j == 1)
                {
                    exceptOneSty = "";
                    exceptOne = "";
                }

                strBuilder.Append("<div class=\"second_main_right_main\">");
                strBuilder.Append("<div class=\"second_main_right_main_first" + exceptOneSty + "\">");
                strBuilder.Append(exceptOne);
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"second_main_right_main_second\">");
                strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" ><img src=\"" + model.PicturePath + "\" /></a>");
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"second_main_right_main_third\">");
                strBuilder.Append("<div class=\"second_main_right_main_third_word\">");
                strBuilder.Append("<a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\" >" + CommonTools.CutString(model.ClothName,10,false) + "</a>");
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"second_main_right_main_third_money\">");
                strBuilder.Append("￥" + model.Price + "");
                strBuilder.Append("</div>");
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"clear\"></div>");
                strBuilder.Append("</div>");

            }
            return strBuilder.ToString();
        }
        #endregion

        #region Detail same picture
        public string GetSamePicture(string urlParam)
        {
            SH_Cloth sh_cloth = new SH_Cloth();
            DataSet ds = sh_cloth.GetDetailSamePicture(urlParam);
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strBuilder.Append("<div class=\"detail_right_div\">");
                strBuilder.Append("<a href=\"" + GVPath(ds.Tables[0].Rows[i][0].ToString()) + "\" target=\"_blank\" ><img src=\"/" + ds.Tables[0].Rows[i][1].ToString() + "\" /></a>");
                strBuilder.Append("<div class=\"detail_right_div_word\"></div>");
                strBuilder.Append("<div class=\"detail_right_div_word_1\"><a href=\"" + GVPath(ds.Tables[0].Rows[i][0].ToString()) + "\" target=\"_blank\" >" + ds.Tables[0].Rows[i][2].ToString() + "</a></div>");
                strBuilder.Append("</div>");
            }
            return strBuilder.ToString();
        }
        #endregion

        #region Bank success picture
        public string GetSuccessPicture()
        {
            SH_Cloth sh_cloth = new SH_Cloth();
            DataSet ds = sh_cloth.GetNewPicture();
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strBuilder.Append("<div class=\"detail_right_div\">");
                strBuilder.Append("<a href=\"" + GVPath(ds.Tables[0].Rows[i][0].ToString()) + "\" target=\"_blank\" ><img src=\"/" + ds.Tables[0].Rows[i][1].ToString() + "\" /></a>");
                strBuilder.Append("<div class=\"detail_right_div_word\"></div>");
                strBuilder.Append("<div class=\"detail_right_div_word_1\"><a href=\"" + GVPath(ds.Tables[0].Rows[i][0].ToString()) + "\" target=\"_blank\" >" + ds.Tables[0].Rows[i][2].ToString() + "</a></div>");
                strBuilder.Append("</div>");
            }
            return strBuilder.ToString();
        }
        #endregion

        #region News TJ Html

        public string NewsTj()
        {
            SH_News _shNews = new SH_News();
            DataSet ds = _shNews.GetTjPart("7", " AND SUBSTRING(IsStr,1,1) = '1'");
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strBuilder.Append(" <div class=\"newsList_right_tj_c\">");
                strBuilder.Append(" <div class=\"newsList_right_tj_c_img\">" + (i + 1) + "</div>");
                strBuilder.Append(" <div class=\"newsList_right_tj_c_word\"><a href=\"/NewsDetail/" + ds.Tables[0].Rows[i][0] + "\" target=\"_blank\" >" + ds.Tables[0].Rows[i][1].ToString() + "</a></div>");
                strBuilder.Append(" </div>");
            }
            return strBuilder.ToString();
        }

        public string NewsHot()
        {
            SH_News _shNews = new SH_News();
            DataSet ds = _shNews.GetTjPart("7", " AND SUBSTRING(IsStr,2,1) = '1'");
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strBuilder.Append(" <div class=\"newsList_right_tj_c\">");
                strBuilder.Append(" <div class=\"newsList_right_tj_c_img2\"></div>");
                strBuilder.Append(" <div class=\"newsList_right_tj_c_word\"><a href=\"/NewsDetail/" + ds.Tables[0].Rows[i][0] + "\" target=\"_blank\" >" + ds.Tables[0].Rows[i][1].ToString() + "</a></div>");
                strBuilder.Append(" </div>");
            }
            return strBuilder.ToString();
        }
        #endregion

        #region News
        public string News()
        {
            SH_News _shNews = new SH_News();
            DataSet ds = _shNews.GetTjPart("5", " AND SUBSTRING(IsStr,3,1) = '1'");
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strBuilder.Append("<div class=\"first_l_main\">");
                strBuilder.Append("<div class=\"first_l_main_pic\">");
                strBuilder.Append("<a href=\"" + NewsPath(ds.Tables[0].Rows[i][0].ToString()) + "\" target=\"_blank\" >");
                strBuilder.Append("<img src=\"" + ds.Tables[0].Rows[i][3].ToString() + "\" /></a>");
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"first_l_main_word\"><a href=\"" + NewsPath(ds.Tables[0].Rows[i][0].ToString()) + "\" target=\"_blank\" >" + ds.Tables[0].Rows[i][2].ToString() + "</a></div>");
                strBuilder.Append("<div class=\"first_l_main_zy\"><a href=\"" + NewsPath(ds.Tables[0].Rows[i][0].ToString()) + "\" target=\"_blank\" >" + CommonTools.CutString(ds.Tables[0].Rows[i][4].ToString(), 35, true) + "</a></div>");
                strBuilder.Append("<div class=\"clear\"></div>");
                strBuilder.Append("</div>");
            }
            return strBuilder.ToString();
        }

        #endregion

        #region Women street
        public string WomenStreet()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Picture> list = ShowIndexImage(10, 17);
            foreach (Picture model in list)
            {
                strBuilder.Append("<div class=\"w_single\"><a href=\"" + GVPath(model.ClothGUID) + "\" target=\"_blank\"><img src=\"" + model.PicturePath + "\" /></a></div>");
            }
            return strBuilder.ToString();
        }
        #endregion

        #region HtmlSql

        /// <summary>
        /// Only image
        /// </summary>
        /// <param name="topCount"></param>
        /// <param name="picTypeId"></param>
        /// <returns></returns>
        public List<Picture> ShowIndexImage(int topCount, int picTypeId)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT TOP " + topCount + " ");
            strBuilder.Append(" ClothGUID,PicturePath FROM TJW_Picture A WHERE PictureTypeId = " + picTypeId + " ORDER BY PictureId DESC");
            List<Picture> list = new List<Picture>();
            DataSet ds = SqlHelper.ExcuteDataSet(strBuilder.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Picture()
                {
                    PicturePath = ds.Tables[0].Rows[i][1].ToString(),
                    ClothGUID = ds.Tables[0].Rows[i][0].ToString()
                });
            }
            return list;
        }

        /// <summary>
        /// Image and word
        /// </summary>
        /// <param name="topCount"></param>
        /// <param name="showNum"></param>
        /// <param name="picTypeId"></param>
        /// <returns></returns>
        public List<Picture> ShowIndexImageWithWord(int topCount, int picTypeId)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT TOP " + topCount + " ");
            strBuilder.Append(" (SELECT top 1 B.ClothName FROM TJW_Cloth B WHERE B.ClothGuid = A.ClothGUID) AS ClothName,");
            strBuilder.Append(" (SELECT top 1 B.Price FROM TJW_Cloth B WHERE B.ClothGuid = A.ClothGUID) AS Price,");
            strBuilder.Append(" ClothGUID,PicturePath,PicHref,PicWord FROM TJW_Picture A WHERE PictureTypeId = " + picTypeId + " ORDER BY PictureId DESC");
            List<Picture> list = new List<Picture>();
            DataSet ds = SqlHelper.ExcuteDataSet(strBuilder.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Picture()
               {
                   ClothName = ds.Tables[0].Rows[i][0].ToString(),
                   Price = ds.Tables[0].Rows[i][1].ToString(),
                   ClothGUID = ds.Tables[0].Rows[i][2].ToString(),
                   PicturePath = ds.Tables[0].Rows[i][3].ToString(),
                   PicHref = ds.Tables[0].Rows[i][4].ToString(),
                   PicWord = ds.Tables[0].Rows[i][5].ToString()
               });
            }
            return list;
        }

        /// <summary>
        /// Image and word and tuijian condition
        /// </summary>
        /// <param name="topCount"></param>
        /// <param name="showNum"></param>
        /// <param name="picTypeId"></param>
        /// <param name="tjWhere"></param>
        /// <returns></returns>
        public List<Picture> ShowIndexImageWithWordCondition(int topCount, int picTypeId, string tjWhere)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" WITH PICROW AS");
            strBuilder.Append(" (");
            strBuilder.Append(" SELECT TOP " + topCount + " ");
            strBuilder.Append(" (SELECT top 1 B.IsVaild FROM TJW_Cloth B WHERE B.ClothGuid = A.ClothGUID) AS IsVaild,");
            strBuilder.Append(" (SELECT top 1 B.IsTj FROM TJW_Cloth B WHERE B.ClothGuid = A.ClothGUID) AS IsTj,");
            strBuilder.Append(" (SELECT top 1 B.ClothName FROM TJW_Cloth B WHERE B.ClothGuid = A.ClothGUID) AS ClothName,");
            strBuilder.Append(" (SELECT top 1 B.Price FROM TJW_Cloth B WHERE B.ClothGuid = A.ClothGUID) AS Price,");
            strBuilder.Append(" ClothGUID,PicturePath FROM TJW_Picture A WHERE PictureTypeId = " + picTypeId + " ORDER BY PictureId DESC");
            strBuilder.Append(" )");
            strBuilder.Append(" SELECT * FROM PICROW WHERE IsVaild = 'True' ");

            List<Picture> list = new List<Picture>();
            DataSet ds = SqlHelper.ExcuteDataSet(strBuilder.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Picture()
                {
                    ClothName = ds.Tables[0].Rows[i][2].ToString(),
                    Price = ds.Tables[0].Rows[i][3].ToString(),
                    PicturePath = ds.Tables[0].Rows[i][5].ToString(),
                    ClothGUID = ds.Tables[0].Rows[i][4].ToString()
                });
            }
            return list;
        }

        #endregion

        #region Routing url
        public string GVPath(string id)
        {
            RouteValueDictionary parameters = new RouteValueDictionary() { { "ClothGuid", id }, };
            VirtualPathData vpd = RouteTable.Routes.GetVirtualPath(null, "ForDetail", parameters);
            return vpd.VirtualPath;
        }
        public string NewsPath(string id)
        {
            RouteValueDictionary parameters = new RouteValueDictionary() { { "NewsId", id }, };
            VirtualPathData vpd = RouteTable.Routes.GetVirtualPath(null, "ForNewsDetail", parameters);
            return vpd.VirtualPath;
        }
        #endregion
    }
}
