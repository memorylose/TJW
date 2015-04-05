using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TJW.Model;

namespace TJW.SqlHandler
{
    public class SH_Tea
    {
        #region Tea type

        #region Add tea type
        public void AddTeaType(TeaType model)
        {
            string strSql = "INSERT INTO TJW_TeaType(TypeName) VALUES (@TypeName)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TypeName",SqlDbType.NVarChar,15)
                                   };
            sqlParm[0].Value = model.TypeName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Delete type
        public void DeleteTeaType(int teaId)
        {
            string strSql = "DELETE FROM TJW_TeaType WHERE TypeId = @TypeId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = teaId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get tea type
        public DataSet GetTeaType()
        {
            string strSql = "SELECT TypeId,TypeName FROM TJW_TeaType ORDER BY TypeId DESC";
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            return ds;
        }
        #endregion

        #endregion

        #region Tea

        #region Add tea
        public void AddTea(Tea model)
        {
            string strSql = "INSERT INTO TJW_Tea(TeaName,TeaPrice,StuffUGUID,TeaTypeId,TeaYear,TeaDescription,IsValid,CreateDate,CreateUserId,TeaOriPrice,TeaCount) VALUES(@TeaName,@TeaPrice,@StuffUGUID,@TeaTypeId,@TeaYear,@TeaDescription,@IsValid,@CreateDate,@CreateUserId,@TeaOriPrice,@TeaCount)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TeaName",SqlDbType.NVarChar,50),
                                     new SqlParameter("@TeaPrice",SqlDbType.Float),
                                     new SqlParameter("@StuffUGUID",SqlDbType.NVarChar,30),
                                     new SqlParameter("@TeaTypeId",SqlDbType.Int),
                                     new SqlParameter("@TeaYear",SqlDbType.Int),
                                     new SqlParameter("@TeaDescription",SqlDbType.NVarChar,2000),
                                     new SqlParameter("@IsValid",SqlDbType.Bit),
                                     new SqlParameter("@CreateDate",SqlDbType.DateTime),
                                     new SqlParameter("@CreateUserId",SqlDbType.Int),
                                     new SqlParameter("@TeaOriPrice",SqlDbType.Float),
                                     new SqlParameter("@TeaCount",SqlDbType.Int)
                                   };
            sqlParm[0].Value = model.TeaName;
            sqlParm[1].Value = model.TeaPrice;
            sqlParm[2].Value = model.StuffUGUID;
            sqlParm[3].Value = model.TeaTypeId;
            sqlParm[4].Value = model.TeaYear;
            sqlParm[5].Value = model.TeaDescription;
            sqlParm[6].Value = model.IsValid;
            sqlParm[7].Value = model.CreateDate;
            sqlParm[8].Value = model.CreateUserId;
            sqlParm[9].Value = model.TeaOriPrice;
            sqlParm[10].Value = model.TeaCount;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Update tea

        public void UpdateTea(Tea model)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" UPDATE TJW_Tea SET TeaName = @TeaName , TeaCount = @TeaCount , TeaOriPrice = @TeaOriPrice , ");
            strBuilder.Append(" TeaPrice = @TeaPrice , TeaTypeId = @TeaTypeId , TeaYear = @TeaYear , ");
            strBuilder.Append(" TeaDescription = @TeaDescription , ModifyDate = @ModifyDate , ModifyUserId = @ModifyUserId");
            strBuilder.Append(" WHERE TeaId = @TeaId");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TeaName",SqlDbType.NVarChar,50),
                                     new SqlParameter("@TeaCount",SqlDbType.Int),
                                     new SqlParameter("@TeaOriPrice",SqlDbType.Float),
                                     new SqlParameter("@TeaPrice",SqlDbType.Float),
                                     new SqlParameter("@TeaTypeId",SqlDbType.Int),
                                     new SqlParameter("@TeaYear",SqlDbType.Int),
                                     new SqlParameter("@TeaDescription",SqlDbType.NVarChar,2000),
                                     new SqlParameter("@ModifyDate",SqlDbType.DateTime),
                                     new SqlParameter("@ModifyUserId",SqlDbType.Int),
                                     new SqlParameter("@TeaId",SqlDbType.Int)                                                              
                                   };
            sqlParm[0].Value = model.TeaName;
            sqlParm[1].Value = model.TeaCount;
            sqlParm[2].Value = model.TeaOriPrice;
            sqlParm[3].Value = model.TeaPrice;
            sqlParm[4].Value = model.TeaTypeId;
            sqlParm[5].Value = model.TeaYear;
            sqlParm[6].Value = model.TeaDescription;
            sqlParm[7].Value = model.ModifyDate;
            sqlParm[8].Value = model.ModifyUserId;
            sqlParm[9].Value = model.TeaId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strBuilder.ToString(), sqlParm);
        }

        #endregion

        #region Get tea
        public DataSet GetTea()
        {
            string strSql = "SELECT TeaId,TeaName,TeaOriPrice,TeaPrice,(SELECT TypeName FROM TJW_TeaType B WHERE A.TeaTypeId = B.TypeId) AS TypeName,TeaTypeId,TeaYear,StuffUGUID,TeaCount FROM TJW_Tea A ORDER BY TeaId DESC";
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            return ds;
        }
        #endregion

        #region Get tea detail
        public DataSet GetTeaDetailWord(string teaGUID)
        {
            string strSql = "SELECT TeaName,TeaPrice,StuffUGUID,(SELECT TypeName FROM TJW_TeaType B WHERE B.TypeId = A.TeaTypeId ) AS TypeName,TeaYear,TeaDescription FROM TJW_Tea A WHERE StuffUGUID = @StuffUGUID";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@StuffUGUID",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = teaGUID;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds;
        }
        public DataSet GetTeaDetailPicture(string teaGUID, string picType)
        {
            string strSql = "SELECT PicturePath FROM TJW_TeaPicture WHERE TeaStuffGUID = @TeaStuffGUID AND PictureTypeId = " + picType + "ORDER BY PictureId DESC";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TeaStuffGUID",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = teaGUID;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds;
        }


        #endregion

        #region Delete tea
        public void DeleteTea(string stuffUGUID)
        {
            string strSql = "DELETE FROM TJW_Tea WHERE StuffUGUID = @StuffUGUID";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@StuffUGUID",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = stuffUGUID;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get delete path
        public DataSet GetDeletePath(string stuffUGUID)
        {
            string strSql = "SELECT PicturePath,PictureId FROM TJW_TeaPicture WHERE TeaStuffGUID = @TeaStuffGUID";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TeaStuffGUID",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = stuffUGUID;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds;
        }
        #endregion

        #region Delete tea picture
        public void DeleteTeaPicture(string pictureId)
        {
            string strSql = "DELETE FROM TJW_TeaPicture WHERE PictureId = @PictureId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@PictureId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = pictureId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get delete path base on picture id
        public DataSet GetDeletePathId(string pictureId)
        {
            string strSql = "SELECT PicturePath FROM TJW_TeaPicture WHERE PictureId = @PictureId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@PictureId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = pictureId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds;
        }
        #endregion

        #region Get edit tea
        public DataSet GetEditTea(int teaId)
        {
            string strSql = "SELECT TeaName,TeaCount,TeaOriPrice,TeaPrice,TeaTypeId,TeaYear,TeaDescription,(SELECT TypeName FROM TJW_TeaType B WHERE A.TeaTypeId = B.TypeId) AS TypeName FROM TJW_Tea A WHERE TeaId = @TeaId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TeaId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = teaId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #endregion

        #region Tea picture

        #region Add tea picture type
        public void AddTeaPictureType(TeaPictureType model)
        {
            string strSql = "INSERT INTO TJW_TeaPictureType(TypeName) VALUES (@TypeName)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TypeName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = model.TypeName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Delete picture type
        public void DeleteTeaPictureType(int teaId)
        {
            string strSql = "DELETE FROM TJW_TeaPictureType WHERE TeaPictureTypeId = @TeaPictureTypeId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TeaPictureTypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = teaId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get tea picture type
        public DataSet GetTeaPictureType()
        {
            string strSql = "SELECT TeaPictureTypeId,TypeName FROM TJW_TeaPictureType ORDER BY TeaPictureTypeId DESC";
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            return ds;
        }
        #endregion

        #region Add tea picture
        public void AddTeaPicture(TeaPicture model)
        {
            string strSql = "INSERT INTO TJW_TeaPicture(TeaStuffGUID,PicturePath,PictureTypeId) VALUES (@TeaStuffGUID,@PicturePath,@PictureTypeId)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TeaStuffGUID",SqlDbType.NVarChar,30),
                                     new SqlParameter("@PicturePath",SqlDbType.NVarChar,100),
                                     new SqlParameter("@PictureTypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = model.TeaStuffGUID;
            sqlParm[1].Value = model.PicturePath;
            sqlParm[2].Value = model.PictureTypeId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get tea picture
        public DataSet GetTeaPicture(string teaGUID)
        {
            string strSql = "SELECT PictureId,PicturePath,(SELECT TypeName FROM  TJW_TeaPictureType B WHERE B.TeaPictureTypeId = A.PictureTypeId) AS TypeName FROM TJW_TeaPicture A WHERE TeaStuffGUID = @TeaStuffGUID ORDER BY PictureId DESC";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TeaStuffGUID",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = teaGUID;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds;
        }
        #endregion

        #region Check same picture
        public bool CheckSameTeaPicture(string stuffId, string typeId)
        {
            bool result = true;

            //except detail page (PictureTypeId = 3)
            if (typeId != "3")
            {
                string strSql = "SELECT COUNT(*) FROM TJW_TeaPicture WHERE TeaStuffGUID = @TeaStuffGUID AND PictureTypeId = " + typeId;
                SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TeaStuffGUID",SqlDbType.NVarChar,30)
                                   };
                sqlParm[0].Value = stuffId;
                int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, sqlParm));
                if (i > 0)
                {
                    result = false;
                }
            }
            return result;
        }
        #endregion

        #endregion
    }
}
