/* ======================================================================== 
* Author：Cass 
* Time：10/13/2014 2:51:21 PM 
* Description:  
* ======================================================================== 
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TJW.Model;

namespace TJW.SqlHandler
{
    public class SH_News
    {
        #region Add news
        public void AddNews(News model)
        {
            string strSql = "INSERT INTO TJW_News(Title,SubTitle,Contents,IsStr,AddTime,CreateUserId,IndexTitle,PicPath) VALUES (@Title,@SubTitle,@Contents,@IsStr,@AddTime,@CreateUserId,@IndexTitle,@PicPath)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@Title",SqlDbType.NVarChar,50),
                                     new SqlParameter("@SubTitle",SqlDbType.NVarChar,2000),
                                     new SqlParameter("@Contents",SqlDbType.NVarChar),
                                     new SqlParameter("@IsStr",SqlDbType.VarChar,20),
                                     new SqlParameter("@AddTime",SqlDbType.DateTime),
                                     new SqlParameter("@CreateUserId",SqlDbType.Int),
                                     new SqlParameter("@IndexTitle",SqlDbType.NVarChar,50),
                                     new SqlParameter("@PicPath",SqlDbType.NVarChar,100)
                                   };
            sqlParm[0].Value = model.Title;
            sqlParm[1].Value = model.SubTitle;
            sqlParm[2].Value = model.Contents;
            sqlParm[3].Value = model.IsStr;
            sqlParm[4].Value = model.AddTime;
            sqlParm[5].Value = model.CreateUserId;
            sqlParm[6].Value = model.IndexTitle;
            sqlParm[7].Value = model.PicPath;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Update news
        public void UpdateNews(News model)
        {
            string strSql = "UPDATE TJW_News SET Title=@Title,SubTitle=@SubTitle,Contents=@Contents,IsStr=@IsStr,IndexTitle=@IndexTitle WHERE NewsId = @NewsId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@Title",SqlDbType.NVarChar,50),
                                     new SqlParameter("@SubTitle",SqlDbType.NVarChar,2000),
                                     new SqlParameter("@Contents",SqlDbType.NVarChar),
                                     new SqlParameter("@NewsId",SqlDbType.Int),
                                     new SqlParameter("@IsStr",SqlDbType.VarChar,20),
                                     new SqlParameter("@IndexTitle",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = model.Title;
            sqlParm[1].Value = model.SubTitle;
            sqlParm[2].Value = model.Contents;
            sqlParm[3].Value = model.NewsId;
            sqlParm[4].Value = model.IsStr;
            sqlParm[5].Value = model.IndexTitle;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        public void UpdateNewsWithPic(News model)
        {
            string strSql = "UPDATE TJW_News SET Title=@Title,SubTitle=@SubTitle,Contents=@Contents,IsStr=@IsStr,IndexTitle=@IndexTitle,PicPath=@PicPath WHERE NewsId = @NewsId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@Title",SqlDbType.NVarChar,50),
                                     new SqlParameter("@SubTitle",SqlDbType.NVarChar,2000),
                                     new SqlParameter("@Contents",SqlDbType.NVarChar),
                                     new SqlParameter("@NewsId",SqlDbType.Int),
                                     new SqlParameter("@IsStr",SqlDbType.VarChar,20),
                                     new SqlParameter("@IndexTitle",SqlDbType.NVarChar,50),
                                     new SqlParameter("@PicPath",SqlDbType.NVarChar,100)
                                   };
            sqlParm[0].Value = model.Title;
            sqlParm[1].Value = model.SubTitle;
            sqlParm[2].Value = model.Contents;
            sqlParm[3].Value = model.NewsId;
            sqlParm[4].Value = model.IsStr;
            sqlParm[5].Value = model.IndexTitle;
            sqlParm[6].Value = model.PicPath;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Delete news
        public void DeleteNews(string newId)
        {
            string strSql = "DELETE FROM TJW_News WHERE NewsId=@NewsId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@NewsId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = newId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get news
        public DataSet GetNews()
        {
            string strSql = "SELECT NewsId,Title,IsStr,AddTime FROM TJW_News ORDER BY NewsId DESC";
            return SqlHelper.ExcuteDataSet(strSql);
        }
        #endregion

        #region Get edit news
        public DataSet GetEditNews(string newsId)
        {
            string strSql = "SELECT Title,Contents,IsStr,SubTitle,IndexTitle,PicPath FROM TJW_News WHERE NewsId = @NewsId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@NewsId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = newsId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #region Get news pager
        public string GetNewsPager(bool isCount)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" WITH RowWith AS");
            strBuilder.Append(" (");
            strBuilder.Append(" SELECT NewsId,Title,SubTitle,IsStr,AddTime,ROW_NUMBER() OVER (ORDER BY NewsId DESC) AS RowNumber,PicPath FROM TJW_News");
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

        #region Get tj part
        public DataSet GetTjPart(string topNum, string where)
        {
            string strSql = " SELECT TOP " + topNum + " NewsId,Title,IndexTitle,PicPath,SubTitle FROM TJW_News WHERE 1 = 1 " + where + " ORDER BY NewsId DESC";
            return SqlHelper.ExcuteDataSet(strSql);
        }
        #endregion

        #region Get news detail
        public DataSet GetNewsDetail(string newsId)
        {
            string strSql = "SELECT Title,SubTitle,Contents,AddTime FROM TJW_News WHERE NewsId = @NewsId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@NewsId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = newsId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #region Get picture
        public string GetPicturePath(string newsId)
        {
            string strSql = "SELECT PicPath FROM TJW_News WHERE NewsId = @NewsId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@NewsId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = newsId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds.Tables[0].Rows[0][0].ToString();
        }
        #endregion

        #region Check news count
        public bool CheckNewsCount(string newsId)
        {
            bool result = true;
            string strSql = "SELECT COUNT(0) FROM TJW_News WHERE NewsId = @NewsId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@NewsId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = newsId;
            int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, sqlParm));
            if (i < 1)
            {
                result = false;
            }
            return result;
        }
        #endregion
    }
}
