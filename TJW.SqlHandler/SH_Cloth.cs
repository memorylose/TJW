/* ======================================================================== 
* Author：Cass 
* Time：8/13/2014 1:54:49 PM 
* Description:  All cloth info
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
    public class SH_Cloth
    {
        #region Cloth Type

        #region Get cloth type

        public List<ClothType> GetFClothType()
        {
            string strSql = " SELECT ClothTypeId,ClothFatherName FROM TJW_ClothType WHERE ClothFahterId = 0 ORDER BY ClothTypeId ASC ";
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            List<ClothType> list = new List<ClothType>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new ClothType()
                {
                    ClothTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    ClothFatherName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }

        public List<ClothType> GetCClothType()
        {
            string strSql = " SELECT ClothTypeId,ClothFatherName,(SELECT ClothFatherName FROM TJW_ClothType AS B WHERE B.ClothTypeId = A.ClothFahterId) AS FATHERNAME FROM TJW_ClothType AS A WHERE ClothFahterId != 0  ORDER BY ClothFahterId ASC ";
            List<ClothType> list = new List<ClothType>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new ClothType()
                {
                    ClothTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    ClothFatherName = ds.Tables[0].Rows[i][1].ToString(),
                    TopName = ds.Tables[0].Rows[i][2].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Get father name
        public List<ClothType> GetFatherName()
        {
            string strSql = " SELECT ClothTypeId,ClothFatherName FROM TJW_ClothType WHERE ClothFahterId = 0 ORDER BY ClothTypeId ASC ";
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            List<ClothType> list = new List<ClothType>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new ClothType()
                {
                    ClothTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    ClothFatherName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Get father count
        public bool CheckFatherCount()
        {
            bool result = true;
            string strSql = "SELECT COUNT(*) FROM TJW_ClothType WHERE ClothFahterId = 0";
            int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, null));
            if (i == 1)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Get type edit
        public List<ClothType> GetTypeEdit(int editId)
        {
            string strSql = " SELECT ClothFatherName,(SELECT ClothFatherName FROM TJW_ClothType AS B WHERE B.ClothTypeId = A.ClothFahterId) AS FATHERNAME FROM TJW_ClothType AS A WHERE ClothTypeId = @ClothTypeId ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothTypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = editId;
            List<ClothType> list = new List<ClothType>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new ClothType()
                {
                    ClothFatherName = ds.Tables[0].Rows[i][0].ToString(),
                    TopName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Add cloth type
        public void AddClothType(ClothType model)
        {
            string strSql = "INSERT INTO TJW_ClothType(ClothFatherName,ClothFahterId,CreateDate,CreateUserId) VALUES (@ClothFatherName,@ClothFahterId,@CreateDate,@CreateUserId)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothFatherName",SqlDbType.NVarChar,10),
                                     new SqlParameter("@ClothFahterId",SqlDbType.Int),
                                     new SqlParameter("@CreateDate",SqlDbType.DateTime),
                                     new SqlParameter("@CreateUserId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = model.ClothFatherName;
            sqlParm[1].Value = model.ClothFahterId;
            sqlParm[2].Value = DateTime.Now;
            sqlParm[3].Value = model.CreateUserId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Edit cloth type
        public void EditFClothType(ClothType model)
        {
            string strSql = "UPDATE TJW_ClothType SET ClothFatherName = @ClothFatherName WHERE ClothTypeId = @ClothTypeId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothFatherName",SqlDbType.NVarChar,10),
                                     new SqlParameter("@ClothTypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = model.ClothFatherName;
            sqlParm[1].Value = model.ClothTypeId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        public void EditCClothType(ClothType model)
        {
            string strSql = "UPDATE TJW_ClothType SET ClothFatherName = @ClothFatherName , ClothFahterId = @ClothFahterId WHERE ClothTypeId = @ClothTypeId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothFatherName",SqlDbType.NVarChar,10),
                                     new SqlParameter("@ClothFahterId",SqlDbType.Int),
                                     new SqlParameter("@ClothTypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = model.ClothFatherName;
            sqlParm[1].Value = model.ClothFahterId;
            sqlParm[2].Value = model.ClothTypeId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Delete cloth type
        public void DeleteClothType(int delId)
        {
            string strSql = " DELETE FROM TJW_ClothType WHERE ClothTypeId = @ClothTypeId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothTypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = delId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        public void DeleteChildClothType(int delId)
        {
            string strSql = " DELETE FROM TJW_ClothType WHERE ClothFahterId = @ClothFahterId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothFahterId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = delId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #endregion

        #region Cloth Color

        #region Add color
        public void AddClothColor(Color model)
        {
            string strSql = " INSERT INTO TJW_Color(ColorName) VALUES (@ColorName) ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ColorName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = model.ColorName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Edit color
        public void EditColor(int editId, string colorName)
        {
            string strSql = " UPDATE TJW_Color SET ColorName = @ColorName WHERE ColorId = @ColorId ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ColorId",SqlDbType.Int),
                                     new SqlParameter("@ColorName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = editId;
            sqlParm[1].Value = colorName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Delete color
        public void DeteleColor(int delId)
        {
            string strSql = " DELETE FROM TJW_Color WHERE ColorId = @ColorId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ColorId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = delId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get color list
        public List<Color> GetColor()
        {
            string strSql = " SELECT ColorId,ColorName FROM TJW_Color ORDER BY ColorId ASC ";
            List<Color> list = new List<Color>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Color()
                {
                    ColorId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    ColorName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Get type edit
        public List<Color> GetColorEdit(int editId)
        {
            string strSql = " SELECT ColorId,ColorName FROM TJW_Color WHERE ColorId = @ColorId ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ColorId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = editId;
            List<Color> list = new List<Color>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Color()
                {
                    ColorId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    ColorName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #endregion

        #region Cloth Size

        #region Add size
        public void AddClothSize(Size model)
        {
            string strSql = " INSERT INTO TJW_Size(SizeName) VALUES (@SizeName) ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@SizeName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = model.SizeName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Edit size
        public void EditSize(int editId, string sizeName)
        {
            string strSql = " UPDATE TJW_Size SET SizeName = @SizeName WHERE SizeId = @SizeId ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@SizeId",SqlDbType.Int),
                                     new SqlParameter("@SizeName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = editId;
            sqlParm[1].Value = sizeName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Delete size
        public void DeteleSize(int delId)
        {
            string strSql = " DELETE FROM TJW_Size WHERE SizeId = @SizeId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@SizeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = delId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get size list
        public List<Size> GetSize()
        {
            string strSql = " SELECT SizeId,SizeName FROM TJW_Size ORDER BY SizeId ASC ";
            List<Size> list = new List<Size>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Size()
                {
                    SizeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    SizeName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Get size edit
        public List<Size> GetSizeEdit(int editId)
        {
            string strSql = " SELECT SizeId,SizeName FROM TJW_Size WHERE SizeId = @SizeId ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@SizeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = editId;
            List<Size> list = new List<Size>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Size()
                {
                    SizeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    SizeName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #endregion

        #region Picture type

        #region Add picture type
        public void AddPictureType(PictureType model)
        {
            string strSql = " INSERT INTO TJW_PictureType(TypeName) VALUES (@TypeName) ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@TypeName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = model.TypeName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Edit picture type
        public void EditPictureType(int editId, string typeName)
        {
            string strSql = " UPDATE TJW_PictureType SET TypeName = @TypeName WHERE PictureTypeId = @PictureTypeId ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@PictureTypeId",SqlDbType.Int),
                                     new SqlParameter("@TypeName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = editId;
            sqlParm[1].Value = typeName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Delete picture type
        public void DetelePictureType(int delId)
        {
            string strSql = " DELETE FROM TJW_PictureType WHERE PictureTypeId = @PictureTypeId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@PictureTypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = delId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get picture type list
        public List<PictureType> GetPictureTypeList()
        {
            string strSql = " SELECT PictureTypeId,TypeName FROM TJW_PictureType ORDER BY PictureTypeId ASC ";
            List<PictureType> list = new List<PictureType>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new PictureType()
                {
                    PictureTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    TypeName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Get picture type edit
        public List<PictureType> GetPictureTypeEdit(int editId)
        {
            string strSql = " SELECT PictureTypeId,TypeName FROM TJW_PictureType WHERE PictureTypeId = @PictureTypeId ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@PictureTypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = editId;
            List<PictureType> list = new List<PictureType>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new PictureType()
                {
                    PictureTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    TypeName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #endregion

        #region Show number

        #region Add show number
        public void AddShowNumber(Show model)
        {
            string strSql = " INSERT INTO TJW_Show(ShowName) VALUES (@ShowName) ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ShowName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = model.ShowName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Edit show number
        public void EditShowNum(int editId, string ShowName)
        {
            string strSql = " UPDATE TJW_Show SET ShowName = @ShowName WHERE ShowId = @ShowId ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ShowId",SqlDbType.Int),
                                     new SqlParameter("@ShowName",SqlDbType.NVarChar,10)
                                   };
            sqlParm[0].Value = editId;
            sqlParm[1].Value = ShowName;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Delete show number
        public void DeteleShowNumber(int delId)
        {
            string strSql = " DELETE FROM TJW_Show WHERE ShowId = @ShowId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ShowId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = delId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get show list
        public List<Show> GetShowNumber()
        {
            string strSql = " SELECT ShowId,ShowName FROM TJW_Show ORDER BY ShowId ASC ";
            List<Show> list = new List<Show>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Show()
                {
                    ShowId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    ShowName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Get size edit
        public List<Show> GetShowEdit(int editId)
        {
            string strSql = " SELECT ShowId,ShowName FROM TJW_Show WHERE ShowId = @ShowId ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ShowId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = editId;
            List<Show> list = new List<Show>();

            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Show()
                {
                    ShowId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    ShowName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #endregion

        #region Cloth

        #region Add cloth
        public void AddCloth(Cloth model)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" INSERT INTO TJW_Cloth(ClothGuid,StuffUGUID,ClothName,ClothTypeId,StoreCount,OriginalPrice,Price,ColorId,SizeId,IsVaild,ShowNum,CreateDate,CreateUserId,IsTj,CustomBHId,ZheKou) ");
            strBuilder.Append(" VALUES (@ClothGuid,@StuffUGUID,@ClothName,@ClothTypeId,@StoreCount,@OriginalPrice,@Price,@ColorId,@SizeId,@IsVaild,@ShowNum,@CreateDate,@CreateUserId,@IsTj,@CustomBHId,@ZheKou) ");

            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50),
                                     new SqlParameter("@ClothName",SqlDbType.NVarChar,50),
                                     new SqlParameter("@ClothTypeId",SqlDbType.Int),
                                     new SqlParameter("@StoreCount",SqlDbType.Int),
                                     new SqlParameter("@OriginalPrice",SqlDbType.Float),
                                     new SqlParameter("@Price",SqlDbType.Float),
                                     new SqlParameter("@ColorId",SqlDbType.Int),
                                     new SqlParameter("@SizeId",SqlDbType.Int),
                                     new SqlParameter("@IsVaild",SqlDbType.Bit),
                                     new SqlParameter("@ShowNum",SqlDbType.Int),
                                     new SqlParameter("@CreateDate",SqlDbType.DateTime),
                                     new SqlParameter("@CreateUserId",SqlDbType.Int),
                                     new SqlParameter("@StuffUGUID",SqlDbType.NVarChar,30),
                                     new SqlParameter("@IsTj",SqlDbType.VarChar,10),
                                     new SqlParameter("@CustomBHId",SqlDbType.Int),
                                     new SqlParameter("@ZheKou",SqlDbType.NVarChar,10)
                                   };

            sqlParm[0].Value = model.ClothGuid;
            sqlParm[1].Value = model.ClothName;
            sqlParm[2].Value = model.ClothTypeId;
            sqlParm[3].Value = model.StoreCount;
            sqlParm[4].Value = model.OriginalPrice;
            sqlParm[5].Value = model.Price;
            sqlParm[6].Value = model.ColorId;
            sqlParm[7].Value = model.SizeId;
            sqlParm[8].Value = model.IsVaild;
            sqlParm[9].Value = model.ShowNum;
            sqlParm[10].Value = model.CreateDate;
            sqlParm[11].Value = model.CreateUserId;
            sqlParm[12].Value = model.StuffUGUID;
            sqlParm[13].Value = model.IsTj;
            sqlParm[14].Value = model.CustomBHId;
            sqlParm[15].Value = model.ZheKou;

            SqlHelper.ExcuteNonQuery(CommandType.Text, strBuilder.ToString(), sqlParm);
        }
        #endregion

        #region Update cloth
        public void UpdateCloth(Cloth model)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" UPDATE TJW_Cloth SET ClothName=@ClothName,ClothTypeId=@ClothTypeId,");
            strBuilder.Append(" StoreCount=@StoreCount,OriginalPrice=@OriginalPrice,Price=@Price,");
            strBuilder.Append(" ColorId=@ColorId,SizeId=@SizeId,IsVaild=@IsVaild,ShowNum=@ShowNum,ModifiedDate=@ModifiedDate,ModifiedUserId=@ModifiedUserId,IsTj=@IsTj,CustomBHId=@CustomBHId");
            strBuilder.Append(" WHERE ClothId=@ClothId");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothName",SqlDbType.NVarChar,50),
                                     new SqlParameter("@ClothTypeId",SqlDbType.Int),
                                     new SqlParameter("@StoreCount",SqlDbType.Int),
                                     new SqlParameter("@OriginalPrice",SqlDbType.Float),
                                     new SqlParameter("@Price",SqlDbType.Float),
                                     new SqlParameter("@ColorId",SqlDbType.Int),
                                     new SqlParameter("@SizeId",SqlDbType.Int),
                                     new SqlParameter("@IsVaild",SqlDbType.Bit),
                                     new SqlParameter("@ShowNum",SqlDbType.Int),
                                     new SqlParameter("@ModifiedDate",SqlDbType.DateTime),
                                     new SqlParameter("@ModifiedUserId",SqlDbType.Int),
                                     new SqlParameter("@IsTj",SqlDbType.VarChar,10),
                                     new SqlParameter("@ClothId",SqlDbType.Int),
                                     new SqlParameter("@CustomBHId",SqlDbType.Int)
                                   };

            sqlParm[0].Value = model.ClothName;
            sqlParm[1].Value = model.ClothTypeId;
            sqlParm[2].Value = model.StoreCount;
            sqlParm[3].Value = model.OriginalPrice;
            sqlParm[4].Value = model.Price;
            sqlParm[5].Value = model.ColorId;
            sqlParm[6].Value = model.SizeId;
            sqlParm[7].Value = model.IsVaild;
            sqlParm[8].Value = model.ShowNum;
            sqlParm[9].Value = model.ModifiedDate;
            sqlParm[10].Value = model.ModifiedUserId;
            sqlParm[11].Value = model.IsTj;
            sqlParm[12].Value = model.ClothId;
            sqlParm[13].Value = model.CustomBHId;

            SqlHelper.ExcuteNonQuery(CommandType.Text, strBuilder.ToString(), sqlParm);
        }
        #endregion

        #region Update zk
        public void UpdateZk(string zk,int clothId)
        {
            string strUpdate = "UPDATE TJW_Cloth SET ZheKou = @ZheKou WHERE ClothGuid = (SELECT  ClothGuid FROM TJW_Cloth WHERE ClothId = @ClothId)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ZheKou",SqlDbType.NVarChar,10),
                                     new SqlParameter("@ClothId",SqlDbType.Int)
                                   };

            sqlParm[0].Value = zk;
            sqlParm[1].Value = clothId;
            SqlHelper.ExcuteNonQuery(CommandType.Text,strUpdate,sqlParm);
        }

        #endregion

        #region Get child type
        public List<ClothType> GetChildType(int clothTypeId)
        {
            string strSql = " SELECT ClothTypeId,ClothFatherName FROM TJW_ClothType WHERE ClothFahterId =@ClothTypeId  ORDER BY ClothTypeId ASC ";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothTypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = clothTypeId;
            List<ClothType> list = new List<ClothType>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new ClothType()
                {
                    ClothTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    ClothFatherName = ds.Tables[0].Rows[i][1].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Get cloth list
        public List<Cloth> GetCloth()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ClothId,ClothGuid,ClothName,(SELECT ClothFatherName FROM TJW_ClothType WHERE ClothTypeId = A.ClothTypeId) AS ClothTName, ");
            strBuilder.Append(" StoreCount,OriginalPrice,Price,(SELECT ColorName FROM TJW_Color WHERE ColorId = A.ColorId) AS ColorName, ");
            strBuilder.Append(" (SELECT SizeName FROM TJW_Size WHERE SizeId = A.SizeId) AS SizeName,IsVaild, ");
            strBuilder.Append(" (SELECT ShowName FROM TJW_Show WHERE ShowId = A.ShowNum) AS ShowName FROM TJW_Cloth A ORDER BY ClothId,ClothGuid DESC ");
            List<Cloth> list = new List<Cloth>();
            DataSet ds = SqlHelper.ExcuteDataSet(strBuilder.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Cloth()
                {
                    ClothId = Convert.ToInt32(ds.Tables[0].Rows[i][0]),
                    ClothGuid = ds.Tables[0].Rows[i][1].ToString(),
                    ClothName = ds.Tables[0].Rows[i][2].ToString(),
                    ClothTypeName = ds.Tables[0].Rows[i][3].ToString(),
                    StoreCount = Convert.ToInt32(ds.Tables[0].Rows[i][4]),
                    OriginalPrice = float.Parse(ds.Tables[0].Rows[i][5].ToString()),
                    Price = float.Parse(ds.Tables[0].Rows[i][6].ToString()),
                    ColorName = ds.Tables[0].Rows[i][7].ToString(),
                    SizeName = ds.Tables[0].Rows[i][8].ToString(),
                    IsVaild = Convert.ToBoolean(ds.Tables[0].Rows[i][9]),
                    ShowName = ds.Tables[0].Rows[i][10].ToString()
                });
            }
            return list;
        }
        #endregion

        #region Get cloth name
        public string GetClothName(int clothId)
        {
            string strSql = "SELECT ClothName FROM TJW_Cloth WHERE ClothId = @ClothId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothId",SqlDbType.Int),
                                   };
            sqlParm[0].Value = clothId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds.Tables[0].Rows[0][0].ToString();
        }
        #endregion

        #region Get cloth info from same cloth
        public DataSet GetSameCloth(int clothId)
        {
            string strSql = " SELECT ClothId,ClothGuid,StuffUGUID,ClothName,ClothTypeId,OriginalPrice,Price,IsVaild,ShowNum,IsTj,CustomBHId,ZheKou FROM TJW_Cloth WHERE ClothId = @ClothId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothId",SqlDbType.Int)
                                   };

            sqlParm[0].Value = clothId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #region Get cloth detail

        public DataSet GetClothDetailNamePrice(string clothGuid)
        {
            string strSql = "SELECT ClothName,Price,StoreCount,ZheKou FROM TJW_Cloth WHERE ClothGuid = @ClothGuid";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50),
                                   };
            sqlParm[0].Value = clothGuid;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }

        public List<Cloth> GetClothDetailColor(string clothGuid)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("SELECT DISTINCT((SELECT ColorName FROM TJW_Color WHERE TJW_Color.ColorId = A.ColorId)) AS ColorName FROM TJW_Cloth A  ");
            strBuilder.Append(" WHERE ClothGuid = @ClothGuid ");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50),
                                   };
            sqlParm[0].Value = clothGuid;
            List<Cloth> list = new List<Cloth>();
            DataSet ds = SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Cloth()
               {
                   ColorName = ds.Tables[0].Rows[i][0].ToString()
               });
            }
            return list;
        }
        public List<Cloth> GetClothDetailSize(string clothGuid)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT DISTINCT((SELECT SizeName FROM TJW_Size WHERE SizeId = A.SizeId)) AS SizeName FROM TJW_Cloth A  ");
            strBuilder.Append(" WHERE ClothGuid = @ClothGuid ORDER BY SizeName DESC");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = clothGuid;
            List<Cloth> list = new List<Cloth>();
            DataSet ds = SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Cloth()
                {
                    SizeName = ds.Tables[0].Rows[i][0].ToString()
                });
            }
            return list;
        }

        public List<Picture> GetDetailPicture(string clothGuid, int pictureTypeId)
        {
            string strSql = "SELECT PicturePath FROM TJW_Picture WHERE ClothGUID = @ClothGUID AND PictureTypeId = @PictureTypeId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50),
                                     new SqlParameter("@PictureTypeId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = clothGuid;
            sqlParm[1].Value = pictureTypeId;
            List<Picture> list = new List<Picture>();
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new Picture()
                {
                    PicturePath = ds.Tables[0].Rows[i][0].ToString()
                });
            }
            return list;
        }

        #region Get detail same picture

        public DataSet GetDetailSamePicture(string clothGuid)
        {
            StringBuilder strBuidler = new StringBuilder();
            strBuidler.Append(" SELECT TOP 10 ClothGUID,PicturePath,(SELECT TOP 1 ClothName FROM TJW_Cloth C WHERE C.ClothGuid = A.ClothGUID) AS ClothName FROM TJW_Picture A WHERE ClothGUID ");
            strBuidler.Append(" IN (SELECT ClothGuid FROM TJW_Cloth WHERE ClothTypeId IN ");
            strBuidler.Append(" (SELECT ClothTypeId FROM TJW_ClothType WHERE ClothFahterId = (SELECT ClothFahterId FROM TJW_ClothType WHERE ClothTypeId = ");
            strBuidler.Append(" (SELECT TOP 1 ClothTypeId FROM TJW_Cloth WHERE ClothGuid = @ClothGuid)))) AND PictureTypeId = 7 ORDER BY PictureId DESC");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = clothGuid;
            return SqlHelper.ExcuteDataSet(strBuidler.ToString(), CommandType.Text, sqlParm);
        }

        #endregion

        #region Get new picture

        public DataSet GetNewPicture()
        {
            StringBuilder strBuidler = new StringBuilder();
            strBuidler.Append(" SELECT TOP 10 ClothGUID,PicturePath,(SELECT TOP 1 ClothName FROM TJW_Cloth C WHERE C.ClothGuid = A.ClothGUID) AS ClothName FROM TJW_Picture A WHERE ");
            strBuidler.Append(" PictureTypeId = 7 AND PicHref = '' AND PicWord = '' ORDER BY PictureId DESC");
            return SqlHelper.ExcuteDataSet(strBuidler.ToString(), CommandType.Text, null);
        }

        #endregion


        #endregion

        #region Cloth pager
        public string ClothPagerBasicSql(string searchCon)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" WITH RowWith AS");
            strBuilder.Append(" (");
            strBuilder.Append(" SELECT ClothId,ClothGuid,ClothName,(SELECT ClothFatherName FROM TJW_ClothType WHERE ClothTypeId = A.ClothTypeId) AS ClothTName,");
            strBuilder.Append(" StoreCount,OriginalPrice,Price,(SELECT ColorName FROM TJW_Color WHERE ColorId = A.ColorId) AS ColorName,");
            strBuilder.Append(" (SELECT SizeName FROM TJW_Size WHERE SizeId = A.SizeId) AS SizeName,IsVaild,");
            strBuilder.Append(" (SELECT ShowName FROM TJW_Show WHERE ShowId = A.ShowNum) AS ShowName,");
            strBuilder.Append(" ROW_NUMBER() OVER (ORDER BY ClothId DESC) AS RowNumber,StuffUGUID FROM TJW_Cloth A");
            strBuilder.Append(" )");
            strBuilder.Append(" SELECT * FROM RowWith WHERE 1 = 1 ");
            strBuilder.Append(searchCon);
            return strBuilder.ToString();
        }

        public DataSet GetChildCloth(string clothGuid)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ClothId,ClothGuid,ClothName,(SELECT ClothFatherName FROM TJW_ClothType WHERE ClothTypeId = A.ClothTypeId) AS ClothTName,");
            strBuilder.Append(" StoreCount,OriginalPrice,Price,(SELECT ColorName FROM TJW_Color WHERE ColorId = A.ColorId) AS ColorName,");
            strBuilder.Append(" (SELECT SizeName FROM TJW_Size WHERE SizeId = A.SizeId) AS SizeName,IsVaild,");
            strBuilder.Append(" (SELECT ShowName FROM TJW_Show WHERE ShowId = A.ShowNum) AS ShowName,");
            strBuilder.Append(" StuffUGUID,ZheKou FROM TJW_Cloth A WHERE ClothGuid = @ClothGuid");
            strBuilder.Append(" ORDER BY ClothId DESC");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = clothGuid;
            return SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
        }

        public DataSet GetClothNoPage(string conditions)
        {
            string strSql = "SELECT ClothName,ClothGuid FROM TJW_Cloth GROUP BY ClothName,ClothGuid HAVING 1 = 1  " + conditions + " ORDER BY MIN(ClothId) DESC";
            return SqlHelper.ExcuteDataSet(strSql);
        }
        #endregion

        #region Delete cloth
        public void DeleteCloth(string clothId)
        {
            string strSql = "DELETE FROM TJW_Cloth WHERE ClothId = @ClothId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = clothId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        public DataSet GetDeleteClothId(string clothGuid)
        {
            string strSql = "SELECT ClothId FROM TJW_Cloth WHERE ClothGuid = @ClothGuid";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = clothGuid;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        public DataSet GetDeleteClothPicture(string clothGuid)
        {
            string strSql = "SELECT PicturePath FROM TJW_Picture WHERE ClothGUID = @ClothGUID";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = clothGuid;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        public DataSet GetChildDeletePath(string clothId)
        {
            string strSql = "SELECT PicturePath FROM TJW_Picture WHERE ClothGUID = (SELECT ClothGuid FROM TJW_Cloth WHERE ClothId = @ClothId)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = clothId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #region Get delete path base on picture id
        public DataSet GetDeletePathId(string pictureId)
        {
            string strSql = "SELECT PicturePath FROM TJW_Picture WHERE PictureId = @PictureId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@PictureId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = pictureId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds;
        }
        #endregion

        #region Delete cloth picture
        public void DeleteClothPicture(string pictureId)
        {
            string strSql = "DELETE FROM TJW_Picture WHERE PictureId = @PictureId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@PictureId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = pictureId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        public DataSet DeleteClothPicture(int clothId)
        {
            string strSql = "SELECT PicturePath FROM TJW_Picture WHERE ClothGUID = (SELECT ClothGuid FROM TJW_Cloth WHERE ClothId = @ClothId)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = clothId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds;
        }

        #endregion

        #region If the last cloth
        public bool IfLastCloth(int clothId)
        {
            bool result = true;
            string strSql = "SELECT COUNT(0) FROM TJW_Cloth WHERE ClothGuid = (SELECT ClothGuid FROM TJW_Cloth WHERE ClothId = @ClothId)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = clothId;
            if (Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, sqlParm)) > 1)
            {
                result = false;
            }
            return result;
        }

        #endregion

        #region Get cloth edit

        public DataSet GetClothEdit(string clothId)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ClothName,(SELECT ClothFatherName FROM TJW_ClothType B WHERE B.ClothTypeId = A.ClothTypeId) AS CTypeName,");
            strBuilder.Append(" StoreCount,OriginalPrice,Price,(SELECT ColorName FROM TJW_Color C WHERE C.ColorId =A.ColorId) AS ColorName,");
            strBuilder.Append(" (SELECT SizeName FROM TJW_Size D WHERE D.SizeId =A.SizeId) AS SizeName,IsVaild,");
            strBuilder.Append(" SUBSTRING(IsTj,1,1) AS HotTop,SUBSTRING(IsTj,2,1) AS Hot,SUBSTRING(IsTj,3,1) AS ProTop,SUBSTRING(IsTj,4,1) AS Pro,");
            strBuilder.Append(" (SELECT ShowName FROM TJW_Show E WHERE E.ShowId =A.ShowNum) AS Show,");
            strBuilder.Append(" (SELECT ClothFatherName FROM TJW_ClothType B WHERE B.ClothTypeId = (SELECT ClothFahterId FROM TJW_ClothType WHERE ClothTypeId = A.ClothTypeId)) AS FTypeName,");
            strBuilder.Append(" (SELECT CustomName FROM TJW_CustomBH WHERE CustomId = CustomBHId) AS CustomName,ZheKou");
            strBuilder.Append(" FROM TJW_Cloth A WHERE ClothId = @ClothId");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = clothId;
            return SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
        }

        #endregion

        #region Get cloth count
        public bool CheckClothCount(string clothGuid)
        {
            bool result = true;
            string strSql = "SELECT COUNT(0) FROM TJW_Cloth WHERE ClothGuid = @ClothGuid";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGuid",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = clothGuid;
            int i = Convert.ToInt32(SqlHelper.ExcuteScalar(CommandType.Text, strSql, sqlParm));
            if (i < 1)
            {
                result = false;
            }
            return result;
        }
        #endregion
        #endregion

        #region Cloth picture

        #region Add cloth picture
        public void AddClothPicture(Picture model)
        {
            string strSql = "INSERT INTO TJW_Picture(ClothGUID,PictureTypeId,PicturePath,CreateDate,CreateUserId,PicHref,PicWord) VALUES (@ClothGUID,@PictureTypeId,@PicturePath,@CreateDate,@CreateUserId,@PicHref,@PicWord)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGUID",SqlDbType.NVarChar,50),
                                     new SqlParameter("@PictureTypeId",SqlDbType.Int),
                                     new SqlParameter("@PicturePath",SqlDbType.NVarChar,100),
                                     new SqlParameter("@CreateDate",SqlDbType.DateTime),
                                     new SqlParameter("@CreateUserId",SqlDbType.Int),
                                     new SqlParameter("@PicHref",SqlDbType.NVarChar,100),
                                     new SqlParameter("@PicWord",SqlDbType.NVarChar,50)
                                   };

            sqlParm[0].Value = model.ClothGUID;
            sqlParm[1].Value = model.PictureTypeId;
            sqlParm[2].Value = model.PicturePath;
            sqlParm[3].Value = model.CreateDate;
            sqlParm[4].Value = model.CreateUserId;
            sqlParm[5].Value = model.PicHref;
            sqlParm[6].Value = model.PicWord;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Edit cloth picture
        public void EditClothPicture(Picture model)
        {
            string strSql = "UPDATE TJW_Picture SET PicturePath = @PicturePath , PictureTypeId = @PictureTypeId,PicHref=@PicHref,PicWord=@PicWord WHERE PictureId = @PictureId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@PictureTypeId",SqlDbType.Int),
                                     new SqlParameter("@PicturePath",SqlDbType.NVarChar,100),
                                     new SqlParameter("@PictureId",SqlDbType.Int),
                                     new SqlParameter("@PicHref",SqlDbType.NVarChar,100),
                                     new SqlParameter("@PicWord",SqlDbType.NVarChar,50)
                                   };

            sqlParm[0].Value = model.PictureTypeId;
            sqlParm[1].Value = model.PicturePath;
            sqlParm[2].Value = model.PictureId;
            sqlParm[3].Value = model.PicHref;
            sqlParm[4].Value = model.PicWord;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        public void EditClothPictureNo(Picture model)
        {
            string strSql = "UPDATE TJW_Picture SET PictureTypeId = @PictureTypeId,PicHref=@PicHref,PicWord=@PicWord WHERE PictureId = @PictureId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@PictureTypeId",SqlDbType.Int),
                                     new SqlParameter("@PictureId",SqlDbType.Int),
                                     new SqlParameter("@PicHref",SqlDbType.NVarChar,100),
                                     new SqlParameter("@PicWord",SqlDbType.NVarChar,50)
                                   };

            sqlParm[0].Value = model.PictureTypeId;
            sqlParm[1].Value = model.PictureId;
            sqlParm[2].Value = model.PicHref;
            sqlParm[3].Value = model.PicWord;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }
        #endregion

        #region Get cloth picture
        public DataSet GetPicture(string clothGUID)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT PictureId,ClothGUID,(SELECT TypeName FROM TJW_PictureType WHERE PictureTypeId = A.PictureTypeId) AS pTypeName ");
            strBuilder.Append(" ,PicturePath,PicHref,PicWord FROM TJW_Picture A WHERE ClothGUID = @ClothGUID ORDER BY PictureId ASC ");
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@ClothGUID",SqlDbType.NVarChar,50)
                                   };
            sqlParm[0].Value = clothGUID;
            return SqlHelper.ExcuteDataSet(strBuilder.ToString(), CommandType.Text, sqlParm);
        }
        #endregion

        #region Get cloth picture edit
        public DataSet GetClothPictureEdit(string pictureId)
        {
            string strSql = "SELECT (SELECT TypeName FROM TJW_PictureType B WHERE B.PictureTypeId = A.PictureTypeId) AS TypeName ,A.PicturePath,A.PicHref,A.PicWord FROM TJW_Picture A WHERE A.PictureId = @PictureId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@PictureId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = pictureId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            return ds;
        }
        #endregion

        #region Get cloth picture path
        public string GetClothPicturePath(string pictureId)
        {
            string strSql = "SELECT PicturePath FROM TJW_Picture WHERE PictureId = @PictureId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@PictureId",SqlDbType.Int)
                                   };
            sqlParm[0].Value = pictureId;
            DataSet ds = SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

        #endregion

        #region Custom BH

        #region Add/Update custom type&BH
        public void AddCustomType(string customName, string customFatherId)
        {
            string strSql = "INSERT INTO TJW_CustomBH(CustomName,CustomFatherId) VALUES (@CustomName,@CustomFatherId)";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@CustomName",SqlDbType.NVarChar,50),
                                     new SqlParameter("@CustomFatherId",SqlDbType.Int)
                                   };

            sqlParm[0].Value = customName;
            sqlParm[1].Value = customFatherId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        public void UpdateCustomBH(string customName, string customFatherId, string customId)
        {
            string strSql = "UPDATE TJW_CustomBH SET CustomName = @CustomName , CustomFatherId = @CustomFatherId WHERE CustomId = @CustomId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@CustomName",SqlDbType.NVarChar,50),
                                     new SqlParameter("@CustomFatherId",SqlDbType.Int),
                                     new SqlParameter("@CustomId",SqlDbType.Int)
                                   };

            sqlParm[0].Value = customName;
            sqlParm[1].Value = customFatherId;
            sqlParm[2].Value = customId;
            SqlHelper.ExcuteNonQuery(CommandType.Text, strSql, sqlParm);
        }

        #endregion

        #region Get father name
        public List<CustomBH> GetCustomFatherName()
        {
            string strSql = " SELECT CustomId,CustomName FROM TJW_CustomBH WHERE CustomFatherId = 0 ORDER BY CustomId ASC ";
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

        #region Get custom bh
        public DataSet GetCustomBH()
        {
            string strSql = "SELECT CustomId,CustomName,(SELECT CustomName FROM TJW_CustomBH WHERE CustomId = A.CustomFatherId) AS TypeName FROM TJW_CustomBH A WHERE CustomFatherId != 0 ORDER BY CustomId DESC";
            return SqlHelper.ExcuteDataSet(strSql);
        }
        #endregion

        #region Get custom edit bh
        public DataSet GetCustomEdit(string customId)
        {
            string strSql = "SELECT CustomName,(SELECT CustomName FROM TJW_CustomBH WHERE CustomId = A.CustomFatherId) AS TypeName FROM TJW_CustomBH A WHERE CustomId = @CustomId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@CustomId",SqlDbType.Int)
                                   };

            sqlParm[0].Value = customId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #region Delete custom
        public DataSet DeleteCustomBH(string customId)
        {
            string strSql = "DELETE FROM TJW_CustomBH WHERE CustomId = @CustomId";
            SqlParameter[] sqlParm = { 
                                     new SqlParameter("@CustomId",SqlDbType.Int)
                                   };

            sqlParm[0].Value = customId;
            return SqlHelper.ExcuteDataSet(strSql, CommandType.Text, sqlParm);
        }
        #endregion

        #endregion

        #region Net Income

        public DataSet GetNetIncome()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT IncomeId,OrderNumber,Price,Grade,IncomeSucTime,(SELECT OrderStatusId FROM TJW_Order B WHERE B.OrderNumber = A.OrderNumber) AS StatusName");
            strBuilder.Append(" FROM TJW_NetIncome A WHERE IsSuc = 'True' ORDER BY IncomeId DESC");
            return SqlHelper.ExcuteDataSet(strBuilder.ToString());
        }

        #endregion
    }
}
