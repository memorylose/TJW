using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TJW.Model;
using TJW.SqlHandler;

namespace TJW.HtmlOuts
{
    public class TeaOuts
    {
        #region HTML

        #region Tea Index
        public string HtmlTeaIndex()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<Tea> list = ShowTeaIndex(" top 20 ");
            foreach (Tea model in list)
            {
                strBuilder.Append("<div class=\"cy_center_r_div\">");
                strBuilder.Append("<a href=\"CYDetail.aspx?teaGUID=" + model.StuffUGUID + "\"><img src=\"../" + model.PicturePath + "\" /></a>");
                strBuilder.Append("<div class=\"cy_center_r_money\">");
                strBuilder.Append("￥" + model.TeaPrice + "");
                strBuilder.Append("</div>");
                strBuilder.Append("<div class=\"cy_center_r_word\">");
                strBuilder.Append("<a href=\"CYDetail.aspx?teaGUID=" + model.StuffUGUID + "\">" + model.TeaName + "</a>");
                strBuilder.Append("</div>");
                strBuilder.Append("</div>");
            }
            return strBuilder.ToString();
        }
        #endregion

        #endregion



        #region SQL
        public List<Tea> ShowTeaIndex(string top)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT  " + top + "  TeaId,TeaName,TeaPrice,StuffUGUID, (SELECT PicturePath FROM TJW_TeaPicture B WHERE B.TeaStuffGUID = A.StuffUGUID AND PictureTypeId = 1) AS PicturePath ");
            strBuilder.Append(" FROM TJW_Tea A WHERE (SELECT PicturePath FROM TJW_TeaPicture C WHERE C.TeaStuffGUID = A.StuffUGUID AND PictureTypeId = 1) != '' AND A.IsValid = 'True' ORDER BY TeaId DESC ");
            List<Tea> list = new List<Tea>();
            DataSet ds = SqlHelper.ExcuteDataSet(strBuilder.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Tea()
                {
                    TeaId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    TeaName = ds.Tables[0].Rows[i][1].ToString(),
                    TeaPrice = float.Parse(ds.Tables[0].Rows[i][2].ToString()),
                    StuffUGUID = ds.Tables[0].Rows[i][3].ToString(),
                    PicturePath = ds.Tables[0].Rows[i][4].ToString()
                });
            }
            return list;
        }
        public string ShowTeaWithPage(bool isCount)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" WITH RowWith AS");
            strBuilder.Append(" (");
            strBuilder.Append(" SELECT TeaId,TeaName,TeaPrice,StuffUGUID, (SELECT PicturePath FROM TJW_TeaPicture B WHERE B.TeaStuffGUID = A.StuffUGUID AND PictureTypeId = 1) AS PicturePath, ");
            strBuilder.Append(" ROW_NUMBER() OVER (ORDER BY TeaId) AS RowNumber  FROM TJW_Tea A");
            strBuilder.Append(" WHERE (SELECT PicturePath FROM TJW_TeaPicture C WHERE C.TeaStuffGUID = A.StuffUGUID AND PictureTypeId = 1) != '' AND A.IsValid = 'True' ");
            strBuilder.Append(" )");
            strBuilder.Append(" SELECT * FROM RowWith WHERE 1 = 1 ");

            if (!isCount)
            {
                return strBuilder.ToString();
            }
            else
            {
                DataSet ds = SqlHelper.ExcuteDataSet(strBuilder.ToString());
                return ds.Tables[0].Rows.Count.ToString();
            }
        }
        #endregion
    }
}
