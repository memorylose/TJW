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
    public class SH_Index
    {
        #region Detail

        #region Get cart size
        public List<Size> GetCartSize(string clothGuid, string colorName)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT (SELECT SizeName FROM TJW_Size B WHERE B.SizeId = A.SizeId) AS SizeName FROM TJW_Cloth A   ");
            strBuilder.Append(" WHERE ColorId = (SELECT ColorId FROM TJW_Color WHERE ColorName =@ColorName) AND A.ClothGuid = @ClothGuid AND A.IsVaild = 'True' ");
            strBuilder.Append(" ORDER BY SizeName DESC ");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50),
                                     new SqlParameter("@ColorName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = clothGuid;
            sqlParm[1].Value = colorName;
            List<Size> list = new List<Size>();
            DataSet ds = SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Size()
                {
                    SizeName = ds.Tables[0].Rows[i][0].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Add cart
        public void AddCart(Cart model)
        {
            string strSql = " INSERT INTO TJW_Cart(StuffUGUID,ClothCount,UserId,CreateDate,IsCustomBH) VALUES (@StuffUGUID,@ClothCount,@UserId,@CreateDate,@IsCustomBH) ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@StuffUGUID",SqlDbType.NVarChar,30),
                                     new SqlParameter("@ClothCount",SqlDbType.Int),
                                     new SqlParameter("@UserId",SqlDbType.Int),
                                     new SqlParameter("@CreateDate",SqlDbType.DateTime),
                                     new SqlParameter("@IsCustomBH",SqlDbType.Int),
                                   };
            sqlParm[0].Value = model.StuffUGUID;
            sqlParm[1].Value = model.ClothCount;
            sqlParm[2].Value = model.UserId;
            sqlParm[3].Value = model.CreateDate;
            sqlParm[4].Value = model.IsCustomBH;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Check same cart
        public bool CheckSameCart(Cart model)
        {
            bool result = true;
            string strSql = "SELECT COUNT(*) FROM TJW_Cart WHERE StuffUGUID = @StuffUGUID AND UserId =@UserId ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@StuffUGUID",SqlDbType.NVarChar,30),
                                     new SqlParameter("@UserId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = model.StuffUGUID;
            sqlParm[1].Value = model.UserId;
            int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, sqlParm));
            if (i > 0)
            {
                result = false;
            }
            return result;
        }
        public bool CheckSameCartWithBH(Cart model)
        {
            bool result = true;
            string strSql = "SELECT COUNT(*) FROM TJW_Cart WHERE StuffUGUID = @StuffUGUID AND UserId =@UserId AND IsCustomBH = @IsCustomBH ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@StuffUGUID",SqlDbType.NVarChar,30),
                                     new SqlParameter("@UserId",SqlDbType.Int),
                                     new SqlParameter("@IsCustomBH",SqlDbType.Int)
                                   };
            sqlParm[0].Value = model.StuffUGUID;
            sqlParm[1].Value = model.UserId;
            sqlParm[2].Value = model.IsCustomBH;
            int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, sqlParm));
            if (i > 0)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Check if > 10 item in cart
        public bool CheckItemNumInCart(int UserId)
        {
            bool result = true;
            string strSql = "SELECT COUNT(*) FROM TJW_Cart WHERE UserId =" + UserId;
            int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, null));
            if (i > ConstValue.ItemNumberInCart)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Get cloth unique GUID
        public string GetClothUGUID(string clothGuid, string colorName, string sizeName)
        {
            string strSql = "SELECT StuffUGUID FROM TJW_Cloth WHERE ClothGuid = @ClothGuid AND ColorId = (SELECT ColorId FROM TJW_Color WHERE ColorName = @ColorName) AND SizeId = (SELECT SizeId FROM TJW_Size WHERE SizeName = @SizeName)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50),
                                     new SqlParameter("@ColorName",SqlDbType.NVarChar,10),
                                     new SqlParameter("@SizeName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = clothGuid;
            sqlParm[1].Value = colorName;
            sqlParm[2].Value = sizeName;

            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            if (ds.Tables[0].Rows.Count == 1)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "-1";
            }
        }
        #endregion

        #region Get cloth unique GUID_BH
        public string GetClothUGUID_BH(string clothGuid, int bhNum)
        {
            string strSql = "SELECT StuffUGUID FROM TJW_Cloth WHERE ClothGuid = @ClothGuid AND CustomBHId = (SELECT CustomFatherId FROM TJW_CustomBH WHERE CustomId = @CustomId)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50),
                                     new SqlParameter("@CustomId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = clothGuid;
            sqlParm[1].Value = bhNum;

            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            if (ds.Tables[0].Rows.Count == 1)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "-1";
            }
        }
        #endregion


        #region Check BH

        public DataSet IsSetBh(string clothGuid)
        {
            string strSql = "SELECT CustomBHId FROM TJW_Cloth WHERE ClothGuid = @ClothGuid";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = clothGuid;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds;
        }

        #endregion

        #region Get cloth unique GUID with custom BH
        public string GetClothUGUIDWithBH(string clothGuid)
        {
            string strSql = "SELECT TOP 1 StuffUGUID FROM TJW_Cloth WHERE ClothGuid = @ClothGuid";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = clothGuid;

            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            if (ds.Tables[0].Rows.Count == 1)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "-1";
            }
        }
        #endregion

        #region Set custom BH and set
        public List<CustomBH> SetCustomBH(string fatherId)
        {
            string strSql = " SELECT CustomId,CustomName FROM TJW_CustomBH WHERE CustomFatherId = " + fatherId + " ORDER BY CustomId ASC ";
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            List<CustomBH> list = new List<CustomBH>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new CustomBH()
                {
                    CustomId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    CustomName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Is custom BH
        public bool IsCustomBH(string clothGuid, ref string fatherId)
        {
            bool result = false;
            string strSql = "SELECT CustomBHId FROM TJW_Cloth WHERE ClothGuid = @ClothGuid";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = clothGuid;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() != "" && ds.Tables[0].Rows[0][0].ToString() != "0")
                {
                    fatherId = ds.Tables[0].Rows[0][0].ToString();
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region Check women street
        public bool CheckWomenStreet(string clothGuid)
        {
            bool result = false;
            string strSql = "SELECT PictureTypeId FROM TJW_Picture WHERE ClothGUID = @ClothGUID";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGUID",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = clothGuid;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            if (ds.Tables[0].Rows[0][0].ToString() == "17")
            {
                result = true;
            }
            return result;
        }
        #endregion

        #endregion

        #region My Cart

        #region Get cart info

        public DataSet GetCart(string userId)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ");
            strBuilder.Append(" CASE SUBSTRING(StuffUGUID,1,1)");
            strBuilder.Append(" WHEN 'C' THEN (SELECT ClothName FROM TJW_Cloth WHERE A.StuffUGUID = TJW_Cloth.StuffUGUID)");
            strBuilder.Append(" WHEN 'T' THEN (SELECT TeaName FROM TJW_Tea WHERE A.StuffUGUID = TJW_Tea.StuffUGUID)");
            strBuilder.Append(" END AS Name,");
            strBuilder.Append(" CASE SUBSTRING(StuffUGUID,1,1)");
            strBuilder.Append(" WHEN 'C' THEN N'颜色：' + (SELECT (SELECT ColorName FROM TJW_Color WHERE TJW_Color.ColorId = TJW_Cloth.ColorId ) FROM TJW_Cloth WHERE A.StuffUGUID = TJW_Cloth.StuffUGUID) +','+CAST(IsCustomBH AS VARCHAR(10))");
            strBuilder.Append(" WHEN 'T' THEN N'类别：' + (SELECT (SELECT TypeName FROM TJW_TeaType WHERE TJW_TeaType.TypeId = TJW_Tea.TeaTypeId) FROM TJW_Tea WHERE A.StuffUGUID = TJW_Tea.StuffUGUID)");
            strBuilder.Append(" END AS ColorType,");
            strBuilder.Append(" CASE SUBSTRING(StuffUGUID,1,1)");
            strBuilder.Append(" WHEN 'C' THEN N'尺寸：' + (SELECT (SELECT SizeName FROM TJW_Size WHERE TJW_Size.SizeId = TJW_Cloth.SizeId ) FROM TJW_Cloth WHERE A.StuffUGUID = TJW_Cloth.StuffUGUID) +','+CAST(IsCustomBH AS VARCHAR(10))");
            strBuilder.Append(" WHEN 'T' THEN N'年限：' + CAST((SELECT TeaYear FROM TJW_Tea WHERE A.StuffUGUID = TJW_Tea.StuffUGUID) AS NVARCHAR(10))");
            strBuilder.Append(" END AS YearSize,");
            strBuilder.Append(" CASE SUBSTRING(StuffUGUID,1,1)");
            strBuilder.Append(" WHEN 'C' THEN (SELECT Price FROM TJW_Cloth WHERE A.StuffUGUID = TJW_Cloth.StuffUGUID)");
            strBuilder.Append(" WHEN 'T' THEN (SELECT TeaPrice FROM TJW_Tea WHERE A.StuffUGUID = TJW_Tea.StuffUGUID)");
            strBuilder.Append(" END AS Price,");
            strBuilder.Append(" CASE SUBSTRING(StuffUGUID,1,1)");
            strBuilder.Append(" WHEN 'C' THEN (SELECT Price FROM TJW_Cloth WHERE A.StuffUGUID = TJW_Cloth.StuffUGUID)*ClothCount");
            strBuilder.Append(" WHEN 'T' THEN (SELECT TeaPrice FROM TJW_Tea WHERE A.StuffUGUID = TJW_Tea.StuffUGUID)*ClothCount");
            strBuilder.Append(" END AS TotalPrice,");
            strBuilder.Append(" CASE SUBSTRING(StuffUGUID,1,1)");
            strBuilder.Append("	WHEN 'C' THEN (SELECT TOP 1 PicturePath FROM TJW_Picture WHERE TJW_Picture.ClothGUID = (SELECT ClothGuid FROM TJW_Cloth WHERE A.StuffUGUID = TJW_Cloth.StuffUGUID) AND PictureTypeId = 7 ORDER BY PictureId DESC ) ");
            strBuilder.Append(" WHEN 'T' THEN (SELECT TOP 1 PicturePath FROM TJW_TeaPicture WHERE A.StuffUGUID = TJW_TeaPicture.TeaStuffGUID AND PictureTypeId = 2 ORDER BY PictureId DESC)");
            strBuilder.Append(" END AS PicturePath,");
            strBuilder.Append(" ClothCount,CartId,StuffUGUID");
            strBuilder.Append(" FROM TJW_Cart A WHERE UserId = @UserId ORDER BY CartId DESC");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = userId;
            return SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
        }


        #endregion

        #region Delete cart
        public void DeleteCart(string cartId)
        {
            string strSql = "DELETE FROM TJW_Cart WHERE CartId = @CartId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@CartId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = cartId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get cart custom BH name
        public string GetCartBH(string cusId)
        {
            string strSql = "SELECT CustomName FROM TJW_CustomBH WHERE CustomId = @CustomId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@CustomId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = cusId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds.Tables[0].Rows[0][0].ToString();
        }
        #endregion

        #region Get cloth guid

        public string GetClothGuid(string stuffGuid)
        {
            string strSql = "SELECT ClothGuid FROM TJW_Cloth WHERE StuffUGUID = @StuffUGUID";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@StuffUGUID",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = stuffGuid;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        #endregion

        #endregion

        #region Order

        #region Get address id

        public int GetAddressId(int userId)
        {
            int result = 0;
            string strSql = "SELECT AddressId FROM TJW_Address WHERE UserId = " + userId + " ORDER BY AddressId DESC";
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string strSqlIsDefault = "SELECT AddressId FROM TJW_Address WHERE UserId = " + userId + " AND IsDefault = 'True' ";
                DataSet dsIsDefault = SqlHelper.ExcuteDataSet(strSqlIsDefault);
                if (dsIsDefault.Tables[0].Rows.Count > 0)
                {
                    result = Convert.ToInt32(dsIsDefault.Tables[0].Rows[0][0]);
                }
                else
                {
                    result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
            }
            return result;
        }

        #endregion

        #region Add order in Cart page
        public void AddOrderInCart(Order moder)
        {
            string strSql = "INSERT INTO TJW_Order(OrderNumber,StuffInfo,OrderStatusId,CreateCartDate,CreateUserId,PayTypeId,AddressId) VALUES (@OrderNumber,@StuffInfo,@OrderStatusId,@CreateCartDate,@CreateUserId,@PayTypeId,@AddressId)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderNumber",SqlDbType.VarChar,20),
                                     new SqlParameter("@StuffInfo",SqlDbType.NVarChar,250),
                                     new SqlParameter("@OrderStatusId",SqlDbType.VarChar,20),
                                     new SqlParameter("@CreateCartDate",SqlDbType.DateTime),
                                     new SqlParameter("@CreateUserId",SqlDbType.Int),
                                     new SqlParameter("@PayTypeId",SqlDbType.Int),
                                     new SqlParameter("@AddressId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = moder.OrderNumber;
            sqlParm[1].Value = moder.StuffInfo;
            sqlParm[2].Value = moder.OrderStatusId;
            sqlParm[3].Value = moder.CreateCartDate;
            sqlParm[4].Value = moder.CreateUserId;
            sqlParm[5].Value = moder.PayTypeId;
            sqlParm[6].Value = moder.AddressId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get cart in order

        /// <summary>
        /// 获取购物车的信息，在购物车页面没有checkbox的时候，可以直接从数据库获取，如果以后加入checkbox选择的话，就需要从页面获取。
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet GetCartInOrder(string userId)
        {
            string strSql = "SELECT StuffUGUID,ClothCount,CartId,IsCustomBH FROM TJW_Cart WHERE UserId = @UserId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = userId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }

        #endregion

        #region Get order

        public DataSet GetOrder(string orderNum)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT OrderNumber,StuffInfo,(SELECT PayTypeName FROM TJW_PayType B WHERE B.PayTypeId = A.PayTypeId) AS PayTypeName,");
            strBuilder.Append(" A.OrderStatusId, AddressId FROM TJW_Order A WHERE OrderNumber = @OrderNumber");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderNumber",SqlDbType.VarChar,20)
                                   };
            sqlParm[0].Value = orderNum;
            return SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
        }

        public DataSet GetOrderAddress(string userId)
        {
            string strSql = "SELECT UserAddress,UserName,UserCode,UserTel FROM TJW_Address WHERE IsDefault = 'True' AND UserId = @UserId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@UserId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = userId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #region Insert into NetIncome

        public void AddNetIncome(NetIncome model)
        {
            string strSql = "INSERT INTO TJW_NetIncome(OrderNumber,Price,Grade,IncomeTime,IsSuc,TradeNumber) VALUES (@OrderNumber,@Price,@Grade,@IncomeTime,@IsSuc,@TradeNumber)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderNumber",SqlDbType.VarChar,20),
                                     new SqlParameter("@Price",SqlDbType.Float),
                                     new SqlParameter("@Grade",SqlDbType.Int),
                                     new SqlParameter("@IncomeTime",SqlDbType.DateTime),
                                     new SqlParameter("@IsSuc",SqlDbType.Bit),
                                     new SqlParameter("@TradeNumber",SqlDbType.VarChar,100)
                                   };
            sqlParm[0].Value = model.OrderNumber;
            sqlParm[1].Value = model.Price;
            sqlParm[2].Value = model.Grade;
            sqlParm[3].Value = model.IncomeTime;
            sqlParm[4].Value = model.IsSuc;
            sqlParm[5].Value = model.TradeNumber;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        #endregion

        #region Change order status when pay over

        public void ChangeOrder_Pay(int orderType, string orderNum)
        {
            string strSql = "UPDATE TJW_Order SET OrderStatusId = @OrderStatusId WHERE OrderNumber = @OrderNumber";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderNumber",SqlDbType.VarChar,20),
                                     new SqlParameter("@OrderStatusId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = orderNum;
            sqlParm[1].Value = orderType;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        #endregion

        #region Return grade when pay

        public void ReturnGrade_Pay(string orderNum, ref string r_grade)
        {
            DataSet ds = GetPriceAndUserId(orderNum);
            string price = ds.Tables[0].Rows[0][0].ToString();
            string grade = price.Substring(0, price.LastIndexOf('.'));
            r_grade = grade;
            string userId = ds.Tables[0].Rows[0][1].ToString();

            string strSql = "UPDATE TJW_Grade SET GradeNum = GradeNum + @GradeNum, ValidGradeNum = ValidGradeNum + @ValidGradeNum WHERE UserId = @UserId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@GradeNum",SqlDbType.Int),
                                     new SqlParameter("@ValidGradeNum",SqlDbType.Int),
                                     new SqlParameter("@UserId",SqlDbType.Int),
                                   };
            sqlParm[0].Value = grade;
            sqlParm[1].Value = grade;
            sqlParm[2].Value = userId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        #endregion

        #region Change income status

        public void ChangeNetIncomeStatus(string orderNum, string tradeNumber)
        {
            string strSql = "UPDATE TJW_NetIncome SET IsSuc = @IsSuc, IncomeSucTime = @IncomeSucTime,TradeNumber=@TradeNumber WHERE OrderNumber = @OrderNumber";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@IsSuc",SqlDbType.Bit),
                                     new SqlParameter("@IncomeSucTime",SqlDbType.DateTime),
                                     new SqlParameter("@OrderNumber",SqlDbType.VarChar,20),
                                     new SqlParameter("@TradeNumber",SqlDbType.VarChar,100)
                                   };
            sqlParm[0].Value = true;
            sqlParm[1].Value = DateTime.Now;
            sqlParm[2].Value = orderNum;
            sqlParm[3].Value = tradeNumber;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        #endregion

        #region Get NetIcome price and userid

        private DataSet GetPriceAndUserId(string orderNum)
        {
            string strSql = "SELECT Price,(SELECT CreateUserId FROM TJW_Order B WHERE B.OrderNumber = @OrderNumber) AS UserId FROM TJW_NetIncome WHERE OrderNumber = @OrderNumber";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderNumber",SqlDbType.VarChar,20)
                                   };
            sqlParm[0].Value = orderNum;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }

        #endregion

        #region Get store count detail page

        public string GetStoreCountDetailPage(string colorName, string sizeName, string guid)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT StoreCount FROM TJW_Cloth WHERE ColorId = (SELECT ColorId FROM TJW_Color WHERE ColorName = @ColorName)");
            strBuilder.Append(" AND SizeId = (SELECT SizeId FROM TJW_Size WHERE SizeName = @SizeName) AND ClothGuid = @ClothGuid");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ColorName",SqlDbType.NVarChar,10),
                                     new SqlParameter("@SizeName",SqlDbType.NVarChar,10),
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = colorName;
            sqlParm[1].Value = sizeName;
            sqlParm[2].Value = guid;
            DataSet ds = SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
            if (ds.Tables[0].Rows.Count == 1)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }

        #endregion

        #region
        public DataSet GetOrderDetails(string orderNumber)
        {
            string strSql = "SELECT StuffInfo FROM TJW_Order WHERE OrderNumber = @OrderNumber";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@OrderNumber",SqlDbType.VarChar,20)
                                   };
            sqlParm[0].Value = orderNumber;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #region Reduce store count

        public void ReduceStoreCount(int count, string stuffGuid)
        {
            string strSql = "UPDATE TJW_Cloth SET StoreCount = StoreCount - @StoreCount WHERE StuffUGUID = @StuffUGUID";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@StoreCount",SqlDbType.Int),
                                     new SqlParameter("@StuffUGUID",SqlDbType.NVarChar,30)
                                   };
            sqlParm[0].Value = count;
            sqlParm[1].Value = stuffGuid;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        #endregion

        #endregion

        #region List
        public string ListPagerBasicSql(string showNum, bool isCount, string where)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" WITH PICROW AS");
            strBuilder.Append(" (");
            strBuilder.Append(" SELECT ClothGUID FROM TJW_Picture A  " + showNum + "");
            strBuilder.Append(" )");
            strBuilder.Append(" SELECT * FROM ");
            strBuilder.Append(" (SELECT ROW_NUMBER() OVER(ORDER BY PictureId DESC) AS 'RowNumber',ClothGUID,(SELECT TOP 1 ClothName FROM TJW_Cloth WHERE ClothGuid = B.ClothGUID)AS ClothName, ");
            strBuilder.Append(" (SELECT TOP 1 Price FROM TJW_Cloth WHERE ClothGuid = B.ClothGUID) AS Price,PicturePath");
            strBuilder.Append(" FROM TJW_Picture B WHERE B.ClothGUID IN (SELECT ClothGUID FROM PICROW GROUP BY ClothGUID) AND PictureTypeId = 16) as TEMP WHERE 1 = 1");
            strBuilder.Append(" " + where);
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
