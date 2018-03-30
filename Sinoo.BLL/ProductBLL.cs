using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sinoo.DAL;
using System.Data;
using System.Data.SqlClient;
using Sinoo.Model;

namespace Sinoo.BLL
{
    /// <summary>
    /// 业务管理逻辑
    /// </summary>
    public class ProductBLL
    {
        #region 实例化数据访问类

        SqlServerProvider Provider = new SqlServerProvider();

        #endregion

        #region 产品信息

        /// <summary>
        /// 获取全部产品信息数据
        /// </summary>
        /// <returns></returns>
        public DataTable SelectProductBase()
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(" SELECT "
                    + " PA01001, "
                    + " PA01002, "
                    + " PA01003, "
                    + " PA01004, "
                    + " PA01997, "
                    + " PA01998 "
                    + " FROM PA01 "
                    + " WHERE PA01997=0 "
                    + " ORDER BY PA01998 DESC ", 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据ID获取产品信息数据
        /// </summary>
        /// <param name="_PA01001">id</param>
        /// <returns></returns>
        public DataTable SelectProductBaseById(string _PA01001)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + "  [PA01001] "
                      + " ,[PA01002] "
                      + " ,[PA01003] "
                      + " ,[PA01004] "
                      + " ,[PA01005] "
                      + " ,[PA01997] "
                      + " ,[PA01998] "
                      + " ,[PA01999] "
                      + " ,dbo.FX_GetPrice(PA01001,0) AS Netprice "
                      + " ,dbo.FX_GetPrice(PA01001,1) AS One "
                      + " ,dbo.FX_GetPrice(PA01001,2) AS Two "
                      + " ,dbo.FX_GetPrice(PA01001,3) AS Three "
                      + " ,dbo.FX_GetPrice(PA01001,4) AS Four "
                      + " FROM PA01 "
                      + " WHERE PA01997 = 0 AND PA01001={0} ", _PA01001);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据名称获取产品信息数据
        /// </summary>
        /// <param name="_PA01001">id</param>
        /// <returns></returns>
        public DataTable SelectProductBaseByName(string _PA01003)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + "  [PA01001] "
                      + " ,[PA01002] "
                      + " ,[PA01003] "
                      + " ,[PA01004] "
                      + " ,[PA01005] "
                      + " ,[PA01997] "
                      + " ,[PA01998] "
                      + " ,[PA01999] "
                      + " ,dbo.FX_GetPrice(PA01001,0) AS Netprice "
                      + " ,dbo.FX_GetPrice(PA01001,1) AS One "
                      + " ,dbo.FX_GetPrice(PA01001,2) AS Two "
                      + " ,dbo.FX_GetPrice(PA01001,3) AS Three "
                      + " ,dbo.FX_GetPrice(PA01001,4) AS Four "
                      + " FROM PA01 "
                      + " WHERE PA01997 = 0 AND PA01003 = '{0}' ", _PA01003);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据名称获取产品信息数据(退单)
        /// </summary>
        /// <param name="_PA01001">id</param>
        /// <returns></returns>
        public DataTable SelectProductBaseByName(string _OB01005, string _OA01002)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format(@"
                    SELECT PA01001,PA01005 FROM OA01
                        INNER JOIN OB01 ON OA01999 = OB01002
                        INNER JOIN PA01 ON PA01003 = OB01005
                        WHERE OA01997 = 0 AND OA01002 = '{0}' AND OB01005 = '{1}' 
                         ", _OA01002, _OB01005);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据IDName获取产品信息数据
        /// </summary>
        /// <param name="_PA01001">id</param>
        /// <returns></returns>
        public DataTable SelectProductBaseByIDName(string _PA01001, string _PA01003)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format(@"SELECT [PA01001] , [PA01002] , [PA01003] ,[PA01004] ,
                                [PA01005] , [PA01997] , [PA01998] ,[PA01999] ,
                                    a.Netprice,a.One,a.Two,a.Three,a.Four
                                FROM [dbo].[PA01] LEFT JOIN (
				                     select pb01001,max(case when pb01003 = 0 then PB01004  end) Netprice,
                                     max(case  when pb01003=1 then PB01004  end) One,
                                     max(case  when pb01003=2 then PB01004  end) Two,
                                     max(case  when pb01003=3 then PB01004  end) Three,
                                     max(case  when pb01003=4 then PB01004  end) Four from PB01
                                     group by pb01001 
                    )  a on a.PB01001 = PA01001
                      WHERE PA01997 = 0  AND PA01001 <> {0} AND PA01003 = '{1}' ", _PA01001, _PA01003);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageSize">每页显示数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="strWhereAdd">Where语句</param>
        /// <param name="obj">数据总数</param>
        /// <returns></returns>
        public DataTable SelectProductBasePage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                //                string strColumn = @"[PA01001] , [PA01002] , [PA01003] ,[PA01004] ,
                //                                   [PA01005] , [PA01997] , [PA01998] ,[PA01999] ,
                //                                   dbo.FX_GetPrice(PA01001,0) AS Netprice,
                //                                   dbo.FX_GetPrice(PA01001,1) AS One ,
                //                                   dbo.FX_GetPrice(PA01001,2) AS Two ,
                //                                   dbo.FX_GetPrice(PA01001,3) AS Three ,
                //                                   dbo.FX_GetPrice(PA01001,4) AS Four ,
                //                                   ROW_NUMBER() OVER(ORDER BY PA01998 DESC ) AS RowNumber";
                //                string strTableName = @" [dbo].[PA01] ";
                //                string strWhere = @" WHERE  PA01997 = 0 " + strWhereAdd;



                string strWhere = @" WHERE  PA01997 = 0 " + strWhereAdd;


                string strColumn = @" * ,
		                            ROW_NUMBER() OVER(ORDER BY PA01998 DESC ) AS RowNumber";
                string strTableName = string.Format(@"
                (
                  SELECT [PA01001] , [PA01002] , [PA01003] ,[PA01004] ,
                     [PA01005] , [PA01997] , [PA01998] ,[PA01999] ,
                      a.Netprice,a.One,a.Two,a.Three,a.Four
                    FROM [dbo].[PA01] LEFT JOIN (
				             select pb01001,max(case when pb01003 = 0 then PB01004  end) Netprice,
                             max(case  when pb01003=1 then PB01004  end) One,
                             max(case  when pb01003=2 then PB01004  end) Two,
                             max(case  when pb01003=3 then PB01004  end) Three,
                             max(case  when pb01003=4 then PB01004  end) Four from PB01
                             group by pb01001 
                    )  a on a.PB01001 = PA01001
                   {0}
                ) A ", strWhere);


                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value="",DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value="",DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="strWhereAdd">Where语句</param>
        /// <returns></returns>
        public DataSet ExportProductBasePage(string strWhereAdd)
        {

            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format(@"SELECT [PA01001] , [PA01002] , [PA01003] ,[PA01004] ,
                     [PA01005] , [PA01997] , [PA01998] ,[PA01999] ,
                      a.Netprice,a.One,a.Two,a.Three,a.Four
                    FROM [dbo].[PA01] LEFT JOIN (
				             select pb01001,max(case when pb01003 = 0 then PB01004  end) Netprice,
                             max(case  when pb01003=1 then PB01004  end) One,
                             max(case  when pb01003=2 then PB01004  end) Two,
                             max(case  when pb01003=3 then PB01004  end) Three,
                             max(case  when pb01003=4 then PB01004  end) Four from PB01
                             group by pb01001 
                    )  a on a.PB01001 = PA01001  {0}", strWhereAdd);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        /// <summary>
        /// 插入产品信息数据
        /// </summary>
        /// <param name="_PA01">实体</param>
        /// <returns>结果</returns>
        public int InsertProductBase(ProductBase _ProductBase)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_PA01", 1, new SqlParameter[]{
                  new SqlParameter(){ParameterName=@"PA01001",Value=1, DbType=DbType.Int32},
                  new SqlParameter(){ParameterName=@"PA01003",Value=_ProductBase.PA01003, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"PA01005",Value=_ProductBase.PA01005, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"PA01997",Value="", DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"One", Value=_ProductBase.Priceone,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Two",Value=_ProductBase.Pricetwo, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Three", Value=_ProductBase.Pricethree,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Four", Value=_ProductBase.Pricefour,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Netprice", Value=_ProductBase.Netprice,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Order", Value="PA01_Insert",DbType=DbType.String}
              });
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新产品信息数据
        /// </summary>
        /// <param name="_PA01">实体</param>
        /// <returns>结果</returns>
        public int UpdateProductBase(ProductBase _ProductBase)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_PA01", 1, new SqlParameter[]{
                  new SqlParameter(){ParameterName=@"PA01001",Value=_ProductBase.PA01001, DbType=DbType.Int32},
                  new SqlParameter(){ParameterName=@"PA01003",Value=_ProductBase.PA01003, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"PA01005",Value=_ProductBase.PA01005, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"PA01997",Value="", DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"One", Value=_ProductBase.Priceone,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Two",Value=_ProductBase.Pricetwo, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Three", Value=_ProductBase.Pricethree,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Four", Value=_ProductBase.Pricefour,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Netprice", Value=_ProductBase.Netprice,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Order", Value="PA01_Update",DbType=DbType.String}
              });
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除产品信息数据
        /// </summary>
        /// <param name="_PA01">实体</param>
        /// <returns>结果</returns>
        public int RemoveProductBase(ProductBase _ProductBase)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_PA01", 1, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PA01001",Value=_ProductBase.PA01001, DbType=DbType.Int32},
                  new SqlParameter(){ParameterName=@"PA01003",Value="", DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"PA01005",Value="", DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"PA01997",Value=_ProductBase.PA01997, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"One", Value="",DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Two",Value="", DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Three", Value="",DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Four", Value="",DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Netprice", Value="",DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value="PA01_Remove",DbType=DbType.String}
              });
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}
