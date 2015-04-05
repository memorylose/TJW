/* ======================================================================== 
* Author：Cass 
* Time：8/28/2014 3:39:19 PM 
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

namespace TJW.SqlHandler
{
    public class SH_UserManagement
    {
        #region Address

        #region Add address
        public void AddAddress(Address model)
        {
            string strSql = "INSERT INTO TJW_Address(UserId,UserAddress,UserName,UserCode,UserTel,IsDefault,CreateTime) VALUES (@UserId,@UserAddress,@UserName,@UserCode,@UserTel,@IsDefault,@CreateTime)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserId",SqlDbType.Int),
                                     new SqlParameter("@UserAddress",SqlDbType.NVarChar,200),
                                     new SqlParameter("@UserName",SqlDbType.NVarChar,10),
                                     new SqlParameter("@UserCode",SqlDbType.NVarChar,10),
                                     new SqlParameter("@UserTel",SqlDbType.NVarChar,20),
                                     new SqlParameter("@IsDefault",SqlDbType.Bit),
                                     new SqlParameter("@CreateTime",SqlDbType.DateTime)

                                   };
            sqlParm[0].Value = model.UserId;
            sqlParm[1].Value = model.UserAddress;
            sqlParm[2].Value = model.UserName;
            sqlParm[3].Value = model.UserCode;
            sqlParm[4].Value = model.UserTel;
            sqlParm[5].Value = model.IsDefault;
            sqlParm[6].Value = model.CreateTime;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get user address number
        public int GetUserAddressNumber(int userId)
        {
            string strSql = "SELECT COUNT(0) FROM TJW_Address WHERE UserId = " + userId;
            int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, null));
            return i;
        }
        #endregion

        #region Get province/city/distinct name
        public string GetName_PCD(string type, string PCDId)
        {
            string strSql = "";
            string result = "";

            if (string.Equals(type, "p"))
            {
                strSql = "SELECT ProvinceName FROM TJW_Province WHERE ProvinceId=" + PCDId;
            }
            else if (string.Equals(type, "c"))
            {
                strSql = "SELECT CityName FROM TJW_City WHERE CityId=" + PCDId;
            }
            else if (string.Equals(type, "d"))
            {
                strSql = "SELECT DistrictName FROM TJW_District WHERE DistrictId=" + PCDId;
            }
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                result = "-1";
            }
            return result;
        }

        #endregion

        #region Get province
        public DataSet GetProvince()
        {
            string strSql = "SELECT ProvinceId , ProvinceName FROM TJW_Province ORDER BY ProvinceId ASC";
            return SqlHelper.ExcuteDataSet(strSql);
        }
        #endregion

        #region Get city
        public DataSet GetCity(string provinceId)
        {
            string strSql = "SELECT CityId,CityName FROM TJW_City WHERE ProvinceId = @ProvinceId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ProvinceId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = provinceId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #region Get distinct
        public DataSet GetDistinct(string cityId)
        {
            string strSql = "SELECT DistrictId,DistrictName FROM TJW_District WHERE CityId = @CityId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@CityId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = cityId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #region Get address
        public DataSet GetAddress(string userId)
        {
            string strSql = "SELECT AddressId,UserAddress,UserName,UserCode,UserTel,IsDefault FROM TJW_Address WHERE UserId =" + userId + " ORDER BY IsDefault DESC ";
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            return ds;
        }
        #endregion

        #region Delete address
        public void DeleteAddress(int addressId)
        {
            string strSql = "DELETE FROM TJW_Address WHERE AddressId = @AddressId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@AddressId",SqlDbType.Int)

                                   };
            sqlParm[0].Value = addressId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Set default
        public void SetDefault(int defaultId, string userId)
        {
            DataSet ds = GetAddress(userId);
            string strSql = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //set current address as default
                if (Convert.ToInt32(ds.Tables[0].Rows[i][0]) == defaultId)
                {
                    strSql = "UPDATE TJW_Address SET IsDefault = 'True' WHERE AddressId = @AddressId ";

                }
                //set others as undefault
                else
                {
                    strSql = "UPDATE TJW_Address SET IsDefault = 'False' WHERE AddressId = @AddressId ";
                }
                SqlParameter[] sqlParm = { 
                                     new SqlParameter("@AddressId",SqlDbType.Int)

                                   };
                sqlParm[0].Value = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
            }
        }
        #endregion

        #endregion

        #region Grade
        public void AddGrade(string userId, int gradeNum)
        {
            string strSql = "INSERT INTO TJW_Grade(UserId,GradeNum,ValidGradeNum) VALUES (@UserId,@GradeNum,@ValidGradeNum)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserId",SqlDbType.Int),
                                     new SqlParameter("@GradeNum",SqlDbType.Int),
                                        new SqlParameter("@ValidGradeNum",SqlDbType.Int)

                                   };
            sqlParm[0].Value = userId;
            sqlParm[1].Value = gradeNum;
            sqlParm[2].Value = gradeNum;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        public DataSet GetCurrentGrade(string userId)
        {
            string strSql = "SELECT GradeNum,ValidGradeNum FROM TJW_Grade WHERE UserId = @UserId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = userId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds;
        }
        #endregion

        #region Change password

        public void ChangePassword(string newPwd, string userName)
        {
            string strSql = "UPDATE TJW_Users SET UserPwd = @UserPwd, ModifyDate = @ModifyDate, ModifiedTimes = ModifiedTimes + 1 WHERE UserName = @UserName";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserPwd",SqlDbType.NVarChar,100),
                                     new SqlParameter("@ModifyDate",SqlDbType.DateTime),
                                     new SqlParameter("@UserName",SqlDbType.NVarChar,30)

                                   };
            sqlParm[0].Value = newPwd;
            sqlParm[1].Value = DateTime.Now;
            sqlParm[2].Value = userName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        #endregion

        #region Get user info

        public DataSet GetUserInfo(string userName)
        {
            string strSql = "SELECT UserName,UserMail,CreateDate FROM TJW_Users WHERE UserName = @UserName";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserName",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = userName;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }

        #endregion
    }
}
