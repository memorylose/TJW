/* ======================================================================== 
* Author：Cass 
* Time：8/8/2014 8:19:35 AM 
* Description:  
* ======================================================================== 
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TJW.Utils;
using TJW.Model;

namespace TJW.SqlHandler
{
    public class SH_AdminLogin
    {
        #region Add login log
        /// <summary>
        /// Add admin login log
        /// </summary>
        /// <param name="adminUsername"></param>
        /// <param name="ipAddress"></param>
        /// <param name="loginStatus"></param>
        public void AddLoginLog(string adminUsername, string ipAddress, bool loginStatus)
        {
            string strSql = "INSERT INTO TJW_AdminLoginLog(AdminUserName,IpAddress,LoginDate,LoginStatus) VALUES (@AdminUserName,@IpAddress,@LoginDate,@LoginStatus) ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@AdminUserName",SqlDbType.NVarChar,20),
                                     new SqlParameter("@IpAddress",SqlDbType.NVarChar,20),
                                     new SqlParameter("@LoginDate",SqlDbType.DateTime),
                                     new SqlParameter("@LoginStatus",SqlDbType.Bit)
                                   };
            sqlParm[0].Value = adminUsername;
            sqlParm[1].Value = ipAddress;
            sqlParm[2].Value = DateTime.Now;
            sqlParm[3].Value = loginStatus;

            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Check admin password
        public bool CheckAdminPassword(string userName, string userPwd)
        {
            //get old password
            string strSql = " SELECT AdminPwd FROM TJW_AdminUser WHERE AdminUserName = @AdminUserName ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@AdminUserName",SqlDbType.NVarChar,20)
                                   };
            sqlParm[0].Value = userName;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            string oldPwd = null;
            oldPwd = ds.Tables[0].Rows[0][0].ToString();
            if (!string.IsNullOrEmpty(oldPwd))
            {
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

        #region Check same ip limit
        /// <summary>
        /// Same ip,three times one day,status is false.
        /// </summary>
        /// <returns></returns>
        public bool CheckIpLimit(string currentIp)
        {
            bool result = true;
            string strSql = "SELECT count(*) FROM TJW_AdminLoginLog WHERE IpAddress = @IpAddress AND  SUBSTRING((SELECT CONVERT(VARCHAR(30),LoginDate,20) AS DATETIME),0,11) = (SELECT CONVERT(VARCHAR(100), GETDATE(), 23)) AND LoginStatus = 'FALSE'";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@IpAddress",SqlDbType.NVarChar,20)
                                   };
            sqlParm[0].Value = currentIp;
            int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, sqlParm));
            if (i > ConstValue.ValidateCodeTimes)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Check same user limit
        /// <summary>
        /// If the same user name for today(status is false), return false
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckSameUserLimit(string userName)
        {
            bool result = true;
            string strSql = "SELECT COUNT(*) FROM TJW_AdminLoginLog WHERE AdminUserName = @AdminUserName AND LoginStatus = 'FALSE'";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@AdminUserName",SqlDbType.NVarChar,20)
                                   };
            sqlParm[0].Value = userName;

            int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, sqlParm));
            if (i > ConstValue.ValidateCodeTimes)
            {
                result = false;
            }
            return result;
        }
        #endregion
    }
}
