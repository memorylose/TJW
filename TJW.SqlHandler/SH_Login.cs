/* ======================================================================== 
* Author：Cass 
* Time：8/18/2014 6:52:56 PM 
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
    public class SH_Login
    {
        #region Check regist user name
        public bool CheckRegistUsername(string userName)
        {
            bool result = true;
            string strSql = "SELECT COUNT(*) FROM TJW_Users WHERE UserName = @UserName";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserName",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = userName;
            int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, sqlParm));
            if (i > 0)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Check regist user name
        public bool CheckRegistMail(string mail)
        {
            bool result = true;
            string strSql = "SELECT COUNT(*) FROM TJW_Users WHERE UserMail = @UserMail";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserMail",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = mail;
            int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, sqlParm));
            if (i > 0)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Insert regist user
        public void InsertRegistUser(Users model)
        {
            string strSql = "INSERT INTO TJW_Users(UserName,UserPwd,UserMail,IsValid,CreateDate,CreateUserRegion,LastLoginDate,LoginTimes) VALUES (@UserName,@UserPwd,@UserMail,@IsValid,@CreateDate,@CreateUserRegion,@LastLoginDate,@LoginTimes)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserName",SqlDbType.NVarChar,30),
                                     new SqlParameter("@UserPwd",SqlDbType.NVarChar,100),
                                     new SqlParameter("@UserMail",SqlDbType.NVarChar,30),
                                     new SqlParameter("@IsValid",SqlDbType.Bit),
                                     new SqlParameter("@CreateDate",SqlDbType.DateTime),
                                     new SqlParameter("@CreateUserRegion",SqlDbType.NVarChar,10),
                                     new SqlParameter("@LastLoginDate",SqlDbType.DateTime),
                                     new SqlParameter("@LoginTimes",SqlDbType.Int),
                                   };
            sqlParm[0].Value = model.UserName;
            sqlParm[1].Value = model.UserPwd;
            sqlParm[2].Value = model.UserMail;
            sqlParm[3].Value = model.IsValid;
            sqlParm[4].Value = model.CreateDate;
            sqlParm[5].Value = model.CreateUserRegion;
            sqlParm[6].Value = model.LastLoginDate;
            sqlParm[7].Value = model.LoginTimes;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Check same ip limit
        /// <summary>
        /// Same ip,three times one day,status is false.
        /// </summary>
        /// <returns></returns>
        public bool CheckIpLimit(string currentIp)
        {
            bool result = true;
            //string strSql = "SELECT count(*) FROM TJW_LoginLog WHERE IpAddress = @IpAddress AND  SUBSTRING((SELECT CONVERT(VARCHAR(30),LoginDate,20) AS DATETIME),0,11) = (SELECT CONVERT(VARCHAR(100), GETDATE(), 23)) AND LoginStatus = 'FALSE'";
            //SqlParameter[] sqlParm = { 
            //                         new SqlParameter("@IpAddress",SqlDbType.NVarChar,20)
            //                       };
            //sqlParm[0].Value = currentIp;
            //int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, sqlParm));
            //if (i > ConstValue.ValidateCodeTimes)
            //{
            //    result = false;
            //}
            //return result;
            return result;
        }
        #endregion

        #region Check password
        public bool CheckPassword(string userName, string userPwd)
        {
            //get old password
            string strSql = " SELECT UserPwd FROM TJW_Users WHERE UserName = @UserName ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserName",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = userName;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string oldPwd = ds.Tables[0].Rows[0][0].ToString();
                //check password
                if (MyEncrypt.ValidatePassword(userPwd, oldPwd))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //user name is not exist
                return false;
            }
        }
        #endregion

        #region Add login log
        public void AddLoginLog(string Username, string ipAddress, bool loginStatus)
        {
            string strSql = "INSERT INTO TJW_LoginLog(UserName,IpAddress,LoginDate,LoginStatus) VALUES (@UserName,@IpAddress,@LoginDate,@LoginStatus) ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserName",SqlDbType.NVarChar,30),
                                     new SqlParameter("@IpAddress",SqlDbType.NVarChar,20),
                                     new SqlParameter("@LoginDate",SqlDbType.DateTime),
                                     new SqlParameter("@LoginStatus",SqlDbType.Bit)
                                   };
            sqlParm[0].Value = Username;
            sqlParm[1].Value = ipAddress;
            sqlParm[2].Value = DateTime.Now;
            sqlParm[3].Value = loginStatus;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region GetUserId
        public string GetUserId(string userName)
        {
            string strSql = "SELECT UserId FROM TJW_Users WHERE UserName =@UserName";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserName",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = userName;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }
        #endregion

        #region Add login time and times
        public void AddLoginTime(string lastRegion, string userId)
        {
            string strSql = "UPDATE TJW_Users SET LastLoginDate = @LastLoginDate,LastLoginRegion=@LastLoginRegion,LoginTimes= LoginTimes + 1 WHERE UserId = @UserId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@LastLoginDate",SqlDbType.DateTime),
                                     new SqlParameter("@LastLoginRegion",SqlDbType.NVarChar,10),
                                     new SqlParameter("@UserId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = DateTime.Now;
            sqlParm[1].Value = lastRegion;
            sqlParm[2].Value = userId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get member

        /// <summary>
        /// get all user info
        /// </summary>
        /// <returns></returns>
        public DataSet GetMember()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT UserName,UserMail,CreateDate,LastLoginDate,LoginTimes,(SELECT GradeNum FROM TJW_Grade B WHERE B.UserId = A.UserId) AS GradeNum,");
            strBuilder.Append(" (SELECT ValidGradeNum FROM TJW_Grade B WHERE B.UserId = A.UserId) AS VGradeNum,UserId");
            strBuilder.Append(" FROM TJW_Users A ORDER BY UserId DESC");
            return SqlHelper.ExcuteDataSet(strBuilder.ToString());
        }
        #endregion

        #region Get edit grade
        public DataSet GetEditGrade(string userId)
        {
            string strSql = "SELECT GradeNum,ValidGradeNum FROM TJW_Grade WHERE UserId = @UserId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = userId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #region Update grade
        public void UpdateGrade(string gradeNum,string validGrade,string userId)
        {
            string strSql = "UPDATE TJW_Grade SET GradeNum=@GradeNum,ValidGradeNum=@ValidGradeNum WHERE UserId = @UserId ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@GradeNum",SqlDbType.Int),
                                     new SqlParameter("@ValidGradeNum",SqlDbType.Int),
                                     new SqlParameter("@UserId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = gradeNum;
            sqlParm[1].Value = validGrade;
            sqlParm[2].Value = userId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion
    }
}
