/* ======================================================================== 
* Author：Cass 
* Time：8/8/2014 9:32:36 AM 
* Description:  Manage admin user
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
    public class SH_AdminUser
    {
        #region Add admin user role
        public void AddAdminRole(AdminRole model)
        {
            string strSql = "INSERT INTO TJW_AdminRole(AdminRoleName) VALUES (@AdminRoleName) ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@AdminRoleName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = model.AdminRoleName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Add admin user
        public void AddAdminUser(AdminUser model)
        {
            string strSql = "INSERT INTO TJW_AdminUser(AdminUserName,AdminPwd,AdminRoleId,CreateDate,IsValid) VALUES (@AdminUserName,@AdminPwd,@AdminRoleId,@CreateDate,@IsValid) ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@AdminUserName",SqlDbType.NVarChar,20),
                                     new SqlParameter("@AdminPwd",SqlDbType.NVarChar,200),
                                     new SqlParameter("@AdminRoleId",SqlDbType.Int),
                                     new SqlParameter("@CreateDate",SqlDbType.DateTime),
                                     new SqlParameter("@IsValid",SqlDbType.Bit)
                                   };
            sqlParm[0].Value = model.AdminUserName;
            sqlParm[1].Value = MyEncrypt.CreateHash(model.AdminPwd);
            sqlParm[2].Value = model.AdminRoleId;
            sqlParm[3].Value = model.CreateDate;
            sqlParm[4].Value = model.IsValid;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get admin role
        public List<AdminRole> GetAdminRole()
        {
            string strSql = "SELECT AdminRoleId,AdminRoleName FROM TJW_AdminRole ORDER BY AdminRoleId ASC";
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            List<AdminRole> list = new List<AdminRole>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new AdminRole()
                {
                    AdminRoleId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    AdminRoleName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Get admin user id
        public int GetAdminUserId(string userName)
        {
            string strSql = "SELECT AdminUserId FROM TJW_AdminUser WHERE AdminUserName = @AdminUserName ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@AdminUserName",SqlDbType.NVarChar,20)
                                   };
            sqlParm[0].Value = userName;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
        #endregion

        #region Get role id

        public int GetRoleId(int userId)
        {
            string strSql = "SELECT AdminRoleId FROM TJW_AdminUser WHERE AdminUserId = @AdminUserId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@AdminUserId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = userId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            else
            {
                return -1;
            }
        }

        #endregion

        #region Is superuser
        public bool IsSuperUser(int userId)
        {
            bool result = false;
            int roleId = GetRoleId(userId);
            //if superuser
            if (roleId == ConstValue.SuperUserId)
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region Get income address

        public DataSet GetIncomeAddress(string orderNum)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("SELECT UserAddress,UserName,UserCode,UserTel FROM TJW_Address WHERE UserId = (SELECT CreateUserId FROM TJW_Order WHERE OrderNumber = @OrderNumber)");
            strBuilder.Append("AND IsDefault = 'True'");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderNumber",SqlDbType.VarChar,20)
                                   };
            sqlParm[0].Value = orderNum;
            return SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
        }

        #endregion

        #region Get income detail

        public List<NetIncomeDetail> GetIncomeDetail(string orderNum)
        {
            string strSql = "SELECT StuffInfo,OrderStatusId FROM TJW_Order WHERE OrderNumber = @OrderNumber";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderNumber",SqlDbType.VarChar,20)
                                   };
            sqlParm[0].Value = orderNum;

            List<NetIncomeDetail> list = new List<NetIncomeDetail>();

            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            if (ds.Tables[0].Rows.Count > 0)
            {


                string[] split = ds.Tables[0].Rows[0][0].ToString().Split('/');
                string[] statusSplit = ds.Tables[0].Rows[0][1].ToString().Split(',');

                int j = -1;
                foreach (string s in split)
                {
                    j++;
                    NetIncomeDetail model = new NetIncomeDetail();
                    string[] split_1 = s.Split(',');
                    DataSet ds_1 = GetClothInfo_Income(split_1[0]);
                    model.StuffGuid = split_1[0];
                    model.StuffName = ds_1.Tables[0].Rows[0][0].ToString();
                    model.StoreCount = split_1[1];
                    model.Color = ds_1.Tables[0].Rows[0][2].ToString();
                    model.Size = ds_1.Tables[0].Rows[0][3].ToString();
                    model.Type = ds_1.Tables[0].Rows[0][1].ToString();
                    model.Status = GetStatusName(Convert.ToInt32(statusSplit[j]));
                    if (statusSplit[j] == "1")
                    {
                        model.SendStatus = "发货";
                    }
                    else
                    {
                        model.SendStatus = "";
                    }

                    if (split_1.Length > 2)
                    {
                        model.BH = GetCustomBhName(Convert.ToInt32(split_1[2]));
                    }
                    list.Add(model);
                }
            }
            return list;
        }

        #endregion

        #region Get status name

        public string GetStatusName(int statusId)
        {
            string strSql = "SELECT StatusName FROM TJW_OrderStatus WHERE OrderStatusId = @OrderStatusId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderStatusId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = statusId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        #endregion

        #region Get cloth info at Income

        public DataSet GetClothInfo_Income(string stuffGuid)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ClothName,(SELECT ClothFatherName FROM TJW_ClothType B WHERE B.ClothTypeId = A.ClothTypeId)AS TypeName,");
            strBuilder.Append(" (SELECT ColorName FROM TJW_Color C WHERE C.ColorId = A.ColorId) AS ColorName,");
            strBuilder.Append(" (SELECT SizeName FROM TJW_Size D WHERE D.SizeId = A.SizeId) AS SizeName FROM TJW_Cloth A WHERE StuffUGUID = @StuffUGUID");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@StuffUGUID",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = stuffGuid;

            return SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
        }

        #endregion

        #region Update send stuff

        public void ChangeOrderStatus(string orderNum, string orderSId)
        {
            string strSql = "UPDATE TJW_Order SET OrderStatusId = @OrderStatusId WHERE OrderNumber = @OrderNumber";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderStatusId",SqlDbType.VarChar,20),
                                     new SqlParameter("@OrderNumber",SqlDbType.VarChar,20)
                                   };
            sqlParm[0].Value = orderSId;
            sqlParm[1].Value = orderNum;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        #endregion

        #region Update send stuff

        public DataSet GetStuffAndStatus(string orderNum)
        {
            string strSql = "SELECT StuffInfo,OrderStatusId FROM TJW_Order WHERE OrderNumber = @OrderNumber";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderNumber",SqlDbType.VarChar,20)
                                   };
            sqlParm[0].Value = orderNum;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }

        #endregion

        #region Get custom BH name

        public string GetCustomBhName(int customId)
        {
            string strSql = "SELECT CustomName FROM TJW_CustomBH WHERE CustomId = @CustomId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@CustomId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = customId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        #endregion
    }
}
