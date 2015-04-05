/* ======================================================================== 
* Author：Cass 
* Time：9/26/2014 10:33:29 AM 
* Description:  
* ======================================================================== 
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TJW.Model;
using TJW.Utils;

namespace TJW.SqlHandler
{
    public class SH_WeChat
    {
        #region Wechat message

        #region Add users to database from wechat server

        public void AddUsersToDB(WeChatUsers model)
        {
            string strSql = "INSERT INTO TJW_WeChat_Users(FakeId,NickName,RemarkName,GroupName) VALUES (@FakeId,@NickName,@RemarkName,@GroupName)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@FakeId",SqlDbType.NVarChar,50),
                                     new SqlParameter("@NickName",SqlDbType.NVarChar,50),
                                     new SqlParameter("@RemarkName",SqlDbType.NVarChar,50),
                                     new SqlParameter("@GroupName",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = model.FakeId;
            sqlParm[1].Value = model.NickName;
            sqlParm[2].Value = model.RemarkName;
            sqlParm[3].Value = model.GroupName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        public void TrancateUsers()
        {
            string strSql = "TRUNCATE TABLE TJW_WeChat_Users";
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, null);
        }

        #endregion

        #region Add message
        public void AddMessage(WeChatMessage model)
        {
            string strSql = "INSERT INTO TJW_WeChat_Message(Title,Describe,Contents,PicturePath,TypeId,CreateUserId,CreateDate) VALUES (@Title,@Describe,@Contents,@PicturePath,@TypeId,@CreateUserId,@CreateDate)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@Title",SqlDbType.NVarChar,100),
                                     new SqlParameter("@Describe",SqlDbType.NVarChar,200),
                                     new SqlParameter("@Contents",SqlDbType.NVarChar),
                                     new SqlParameter("@PicturePath",SqlDbType.NVarChar,100),
                                     new SqlParameter("@TypeId",SqlDbType.Int),
                                     new SqlParameter("@CreateUserId",SqlDbType.Int),
                                     new SqlParameter("@CreateDate",SqlDbType.DateTime)
                                   };
            sqlParm[0].Value = model.Title;
            sqlParm[1].Value = model.Describe;
            sqlParm[2].Value = model.Contents;
            sqlParm[3].Value = model.PicturePath;
            sqlParm[4].Value = model.TypeId;
            sqlParm[5].Value = model.CreateUserId;
            sqlParm[6].Value = model.CreateDate;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get wechat type
        public List<WeChatType> GetWeChatType()
        {
            string strSql = " SELECT TypeId,TypeName FROM TJW_WeChat_MessageType ORDER BY TypeId ASC ";
            List<WeChatType> list = new List<WeChatType>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new WeChatType()
                {
                    Id = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    TypeName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Get message

        public DataSet GetWeChatMessage(string whereType, string typeId)
        {
            string whereQuery = string.Empty;

            //where typeId
            if (whereType == "type" && typeId != "")
            {
                whereQuery = " AND  TypeId = " + typeId;
            }

            string strSql = "SELECT TOP " + ConstValue.weChatShowNumber + " Title,PicturePath,Id FROM TJW_WeChat_Message WHERE 1 = 1 " + whereQuery + " ORDER BY Id DESC";
            return SqlHelper.ExcuteDataSet(strSql);
        }

        #endregion

        #region Get detail message
        public DataSet GetDetailMessage(string id)
        {
            string strSql = "SELECT Title,Describe,Contents,TypeId,CreateDate FROM TJW_WeChat_Message WHERE Id = @Id";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@Id",SqlDbType.Int),
                                   };
            sqlParm[0].Value = id;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #region WeChat message pager
        public string WeChatPagerBasicSql()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" WITH RowWith AS(");
            strBuilder.Append(" SELECT Id,Title,(SELECT TypeName FROM TJW_WeChat_MessageType B WHERE A.TypeId = B.TypeId) AS TypeName,");
            strBuilder.Append(" ROW_NUMBER() OVER (ORDER BY Id DESC) AS RowNumber");
            strBuilder.Append(" FROM TJW_WeChat_Message A)");
            strBuilder.Append(" SELECT * FROM RowWith WHERE 1 = 1");
            return strBuilder.ToString();
        }
        #endregion

        #region Get edit message
        public DataSet GetEditMessage(string Id)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT Id,Title,Describe,Contents,PicturePath,(SELECT TypeName FROM TJW_WeChat_MessageType B WHERE A.TypeId = B.TypeId) AS TypeName");
            strBuilder.Append(" FROM TJW_WeChat_Message A WHERE Id = @Id");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@Id",SqlDbType.Int),
                                   };
            sqlParm[0].Value = Id;
            return SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
        }
        #endregion

        #region Delete message
        public void DeleteMessage(string Id)
        {
            string strSql = "DELETE FROM TJW_WeChat_Message WHERE Id = @Id";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@Id",SqlDbType.Int),
                                   };
            sqlParm[0].Value = Id;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Update messsage

        public void UpdateMessageWithImage(WeChatMessage model)
        {
            string strSql = "UPDATE TJW_WeChat_Message SET Title = @Title,Describe = @Describe,Contents = @Contents,PicturePath = @PicturePath,TypeId = @TypeId WHERE Id = @Id ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@Title",SqlDbType.NVarChar,100),
                                     new SqlParameter("@Describe",SqlDbType.NVarChar,200),
                                     new SqlParameter("@Contents",SqlDbType.NVarChar),
                                     new SqlParameter("@PicturePath",SqlDbType.NVarChar,100),
                                     new SqlParameter("@TypeId",SqlDbType.Int),
                                     new SqlParameter("@Id",SqlDbType.Int),
                                   };
            sqlParm[0].Value = model.Title;
            sqlParm[1].Value = model.Describe;
            sqlParm[2].Value = model.Contents;
            sqlParm[3].Value = model.PicturePath;
            sqlParm[4].Value = model.TypeId;
            sqlParm[5].Value = model.Id;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        public void UpdateMessage(WeChatMessage model)
        {
            string strSql = "UPDATE TJW_WeChat_Message SET Title = @Title,Describe = @Describe,Contents = @Contents,TypeId = @TypeId WHERE Id = @Id ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@Title",SqlDbType.NVarChar,100),
                                     new SqlParameter("@Describe",SqlDbType.NVarChar,200),
                                     new SqlParameter("@Contents",SqlDbType.NVarChar),
                                     new SqlParameter("@TypeId",SqlDbType.Int),
                                     new SqlParameter("@Id",SqlDbType.Int),
                                   };
            sqlParm[0].Value = model.Title;
            sqlParm[1].Value = model.Describe;
            sqlParm[2].Value = model.Contents;
            sqlParm[3].Value = model.TypeId;
            sqlParm[4].Value = model.Id;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        #endregion

        #endregion

        #region WeChat message type

        #region Add message type
        public void AddMessageType(string typeName)
        {
            string strSql = "INSERT INTO TJW_WeChat_MessageType(TypeName) VALUES (@TypeName)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TypeName",SqlDbType.NVarChar,20)
                                   };
            sqlParm[0].Value = typeName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Update message type
        public void UpdateMessageType(string typeId, string typeName)
        {
            string strSql = "UPDATE TJW_WeChat_MessageType SET TypeName = @TypeName WHERE TypeId = @TypeId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TypeId",SqlDbType.Int),
                                     new SqlParameter("@TypeName",SqlDbType.NVarChar,20)
                                   };
            sqlParm[0].Value = typeId;
            sqlParm[1].Value = typeName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get message type
        public DataSet GetMessageType()
        {
            string strSql = "SELECT TypeId,TypeName FROM TJW_WeChat_MessageType ORDER BY TypeId DESC";
            return SqlHelper.ExcuteDataSet(strSql);
        }
        #endregion

        #region Delete message type
        public void DeleteMessageType(string typeId)
        {
            string strSql = "DELETE FROM TJW_WeChat_MessageType WHERE TypeId = @TypeId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = typeId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get edit message type
        public DataSet GetEditMessageType(string editId)
        {
            string strSql = "SELECT TypeId,TypeName FROM TJW_WeChat_MessageType WHERE TypeId = @TypeId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = editId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #endregion
    }
}
