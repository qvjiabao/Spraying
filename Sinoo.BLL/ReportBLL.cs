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
    public class ReportBLL
    {
        #region 实例化数据访问类

        SqlServerProvider Provider = new SqlServerProvider();

        #endregion

        #region 日报（Daily Report）

        /// <summary>
        /// 到货统计/NewArrival
        /// </summary>
        /// <returns></returns>
        public DataTable SelectNewArrival(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" OA01009,OA01002,CA01003,UA01013,OA01020,UA01001,
                                      UA01004,UA01005, CAST(OP01015 as decimal(18,2)) OP01015,OC01007,MAX(OC01009) AS OC01009,
                                      ROW_NUMBER() OVER(ORDER BY OA01009 DESC ) AS RowNumber ";
                string strTableName = @" OB01 
                                         INNER JOIN dbo.OC01 ON OB01999=OC01003
                                         INNER JOIN dbo.OA01 ON OA01999 = OB01002
                                         INNER JOIN dbo.OP01 ON OP01003 = OA01999
                                         INNER JOIN dbo.CA01 ON CA01001 = OA01038 
                                         INNER JOIN dbo.UA01 ON UA01001 = OA01013 ";
                string strWhere = string.Format(@" WHERE  OA01003 = 1  AND OA01002 NOT IN(
		                    	SELECT OA01002
		                    	FROM dbo.OA01 
		                       	JOIN dbo.OB01 ON OA01999 = OB01002
		                    	JOIN dbo.OC01 ON OB01999 = OC01003
		                    	WHERE OC01007 =0
		                       ) ");

                string strOrder = string.Format(@"group by OB01002 ,OA01002,OA01009,CA01003,OA01020,UA01001,UA01004,UA01005,OP01015,OC01007,UA01013,OA01013
                                                  HAVING 1=1 {0}", strWhereAdd);
                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value=strOrder,DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 到款统计/Daliy Collection
        /// </summary>
        /// <returns></returns>
        public DataTable SelectDaliyCollection(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" OA01009 , OA01002 , CA01003 , OA01020 ,
                                      UA01004 , UA01005, CAST(OP01007 as decimal(18,2)) OP01007 , 
                                      OP01008 , CAST(OP01009 as decimal(18,2)) OP01009 ,
                                      OP01010 ,CAST(OP01011 as decimal(18,2)) OP01011   , OP01012 , 
                                      CAST(OP01013 as decimal(18,2)) OP01013  ,  OP01014 ,
                                      CAST(OP01015 as decimal(18,2)) OP01015 , 
                                      ROW_NUMBER() OVER(ORDER BY OA01009 DESC ) AS RowNumber ";

                string strTableName = @" dbo.OA01
                                         INNER JOIN dbo.OP01 ON OA01999 = OP01003
                                         INNER JOIN dbo.CA01 ON CA01001 = OA01038
                                         INNER JOIN dbo.UA01 ON UA01001 = OA01013 ";

                string strWhere = string.Format(@" WHERE   OA01003 = 1  {0} ", strWhereAdd);


                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
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
        /// 发货统计/DaliyShipment
        /// </summary>
        /// <returns></returns>
        public DataTable SelectDailyShipment(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" OA01009,OA01002,CA01003,UA01004,UA01005,OB01005,
                                      OB01007,OC01012,OC01013,OC01018,OC01017,OC01011,OC01015,
                                      OA01028,ROW_NUMBER() OVER(ORDER BY OA01009 DESC ) AS RowNumber ";
                string strTableName = @" OB01 
                                         INNER JOIN dbo.OC01 ON OB01999=OC01003
                                         INNER JOIN dbo.OA01 ON OA01999 = OB01002
                                         INNER JOIN dbo.OP01 ON OP01003 = OA01999
                                         INNER JOIN dbo.CA01 ON CA01001 = OA01038 
                                         INNER JOIN dbo.UA01 ON UA01001 = OA01013 ";
                string strWhere = string.Format(@" WHERE  OC01010 = 1 {0} ", strWhereAdd);

                string strOrder = @"group by OA01009,OA01002,CA01003,UA01004,UA01005,OB01005,OB01007,OC01012,OC01013,OC01018,OC01017,OA01028,OC01011,OC01015
                                    HAVING dbo.FX_GetDeliveredState(COUNT(1),SUM(Case when OC01010 = 1 then 1 else 0 end)) ='true'";
                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value=strOrder,DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 到货通知统计/Arrival Notice
        /// </summary>
        /// <returns></returns>
        public DataTable SelectArrivalNotice(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" OA01009,OA01002,CA01003,OA01020,UA01013,UA01001,
                                      UA01004,UA01005,OP01015,OC01007,MAX(OC01009) AS OC01009,
                                      ROW_NUMBER() OVER(ORDER BY OA01009 DESC ) AS RowNumber ";
                string strTableName = @" OB01 
                                         INNER JOIN  dbo.OC01 ON OB01999=OC01003
                                         INNER JOIN  dbo.OA01 ON OA01999 = OB01002
                                         INNER JOIN  dbo.OP01 ON OP01003 = OA01999
                                         INNER JOIN  dbo.CA01 ON CA01001 = OA01038 
                                         INNER JOIN  dbo.UA01 ON UA01001 = OA01013 ";
                string strWhere = string.Format(@"WHERE  OA01003 = 1  AND OC01010 <> 1  AND OA01002 NOT IN(
		                    	SELECT OA01002
		                    	FROM dbo.OA01 
		                       	JOIN dbo.OB01 ON OA01999 = OB01002
		                    	JOIN dbo.OC01 ON OB01999 = OC01003
		                    	WHERE OC01007 =0 and OC01010 = 1
		                       )  ");

                string strOrder = string.Format(@"group by OB01002 ,OA01002,OA01009,CA01003,OA01020,UA01001,UA01004,UA01005,OP01015,OC01007,UA01013 
                                HAVING 1=1 {0}", strWhereAdd); ;
                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value=strOrder,DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 订单明细统计/Order List
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOrderList(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                //string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY OA01009 ASC ) AS RowNumber ";

                string strColumn = @" (CASE WHEN OA01016 = 0 THEN '无' ELSE CASE OA01015 WHEN '' THEN OA01049 ELSE OA01015 END  END ) OA01015
                                     ,(CASE WHEN OA01018 = 0 THEN '无' ELSE CASE OA01017 WHEN '' THEN OA01050 ELSE OA01017 END  END ) OA01017
                                     ,(CASE OA01016 WHEN 0 THEN '无' ELSE CAST(OA01016*100 AS VARCHAR(20)) +'%' END ) OA01016
                                     ,(CASE OA01018 WHEN 0 THEN '无' ELSE CAST(OA01018*100 AS VARCHAR(20)) +'%' END ) OA01018 
                                     ,OA01009,OA01002,CA01002,CA01003,CA01009 
	                                 ,UA01004,UA01005,OB01005,OB01007,CAST(OB01008 as decimal(18,2)) OB01008, (OB01007* OB01008) OA01020
	                                 ,round((OB01007* OB01008 /OA01021/1.17),2) OA01022,OA01021,OA01041,CA01018,OA01025
	                                 ,OA01040,OP01007,OP01008,CAST(OP01009 as decimal(18,2)) OP01009
	                                 ,OP01010,CAST(OP01011 as decimal(18,2)) OP01011,OP01012
                                     ,CAST(OP01013 as decimal(18,2)) OP01013 ,OP01014
	                                 ,CAST(OP01015 as decimal(18,2)) OP01015,OA01010,OC01009,OC01008,OC01011
	                                 ,OC01015,OA01044,CA01024,OB02002,CB02002
	                                 ,CB03002,CB04002,dbo.FX_GetProvinceByCityId(CA01013) AS CA01013
	                                 ,dbo.FX_Get220(OA01043) AS OA01043,dbo.FX_Get240(OA01042) AS OA01042
                                     ,OA01028,ROW_NUMBER() OVER(ORDER BY OA01009 DESC ) AS RowNumber,CB01002,GA03002 ";
                string strTableName = @"  OA01 
                                          LEFT JOIN OB01 on OA01999 = OB01002
                                          INNER JOIN OC01 on OB01999=OC01003
                                          LEFT JOIN dbo.OP01 ON OP01003 = OA01999
                                          INNER JOIN dbo.CA01 ON CA01001 = OA01038 
                                          INNER JOIN dbo.CB01 ON CB01001=CA01016
                                          INNER JOIN dbo.GA03 ON GA03001=CA01013
                                          INNER JOIN dbo.UA01 ON UA01001 = OA01013 
                                          LEFT JOIN dbo.CB03 ON CB03001 = OA01040
                                          LEFT JOIN DBO.CB04 ON CB04001 = OA01041 
                                          LEFT JOIN DBO.OB02 ON OB02001 = OA01025
                                          LEFT JOIN DBO.CB02 ON CB02001 = CA01018  ";
                string strWhere = string.Format(@" WHERE   1 = 1  {0} ", strWhereAdd);

                string strOrder = @"";
                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value=strOrder,DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }


        #endregion

        #region 周报（Weekly Report）

        /// <summary>
        /// 周销售统计/Weekly Sales
        /// </summary>
        /// <returns></returns>
        public DataTable SelectWeeklySalesPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY OA01009 ASC ) AS RowNumber ";
                string strTableName = string.Empty;
                if (blInvoice)
                {
                    strTableName = string.Format(@"
                                    (  select OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                             dbo.FX_GetProvinceIdByCityId(ProvinceId) ProvinceId,dbo.FX_GetProvinceByCityId(ProvinceName) ProvinceName ,
                                             CB04002,OA01021,OA01015,OA01017,OA01044,sum(OA01020) OA01020,sum(OA01022) OA01022
                                       from (
                                        SELECT *  FROM
                                        (   SELECT  OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                    CA01013 ProvinceId ,CA01013 ProvinceName,CB04002,OA01021,OA01015,OA01017,OA01044,
                                                    OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022
                                            FROM OA01 
                                                   INNER JOIN OB01 ON OA01999 = OB01002
                                                   INNER JOIN OC01 ON OB01999 = OC01003
                                                   INNER JOIN CA01 ON CA01001 = OA01038                                       
                                                   INNER JOIN UA01 ON UA01001 = OA01013
                                                   LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0  AND  (OA01015 = '' or OA01015 is null)
                                                AND  (OA01017 = '' or OA01017 is null)  AND  OA01016 = 0 AND OA01018 = 0 {0}
                                        ) A
                                        UNION ALL 
                                        SELECT *  FROM
                                        (   SELECT  OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                   CA01013 ProvinceId ,CA01013 ProvinceName,CB04002,OA01021,OA01015,OA01017,OA01044,
                                                   OB01007*OB01008*OA01016 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
                                            FROM OA01 
                                                INNER JOIN OB01 ON OA01999 = OB01002
                                                INNER JOIN OC01 ON OB01999 = OC01003
                                                INNER JOIN CA01 ON CA01001 = OA01038                                       
                                                INNER JOIN UA01 ON UA01004 = OA01015
                                                LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0 AND  (OA01015 <> '' and OA01015 is not null) {0}
                                        )  B  
                                        UNION ALL 
                                        SELECT *  FROM
                                        (    SELECT  OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                   CA01013 ProvinceId ,CA01013 ProvinceName,CB04002,OA01021,OA01015,OA01017,OA01044,
                                                   OB01007*OB01008*OA01018 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022
                                            FROM OA01 
                                            INNER JOIN OB01 ON OA01999 = OB01002
                                            INNER JOIN OC01 ON OB01999 = OC01003
                                            INNER JOIN CA01 ON CA01001 = OA01038                                       
                                            INNER JOIN UA01 ON UA01004 = OA01017
                                            LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0 AND  (OA01017 <> '' and OA01017 is not null) {0}
                                         ) C  
                                         UNION ALL 
                                         SELECT *  FROM
                                         (    SELECT  OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                   CA01013 ProvinceId ,CA01013 ProvinceName,CB04002,OA01021,OA01015,OA01017,OA01044,
                                                   OB01007*OB01008*(1-OA01016-OA01018) AS OA01020,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022
                                            FROM OA01 
                                                INNER JOIN OB01 ON OA01999 = OB01002
                                                INNER JOIN OC01 ON OB01999 = OC01003
                                                INNER JOIN CA01 ON CA01001 = OA01038                                       
                                                INNER JOIN UA01 ON UA01001 = OA01013
                                                LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0)  {0}
                                          ) D 
                            ) GP  group by OA01001,OA01002,OA01009,OA01013,CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                  ProvinceId , ProvinceName,CB04002,OA01021,OA01015,OA01017,OA01044 ) A", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@"
                                    (  select * from (
                                        SELECT *  FROM
                                        (   SELECT OA01001,OA01002,OA01009,OA01013 , 
                                                   CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                   dbo.FX_GetProvinceIdByCityId(CA01013) ProvinceId ,
                                                   dbo.FX_GetProvinceByCityId(CA01013) ProvinceName ,
                                                   CB04002, 
                                                   OA01020,
                                                   OA01021, OA01015,OA01017,
                                                   OA01022,OA01044 ,
                                                   ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
                                            FROM OA01 
                                                   INNER JOIN OB01 ON OA01999 = OB01002
                                                   INNER JOIN OC01 ON OB01999 = OC01003
                                                   INNER JOIN CA01 ON CA01001 = OA01038                                       
                                                   INNER JOIN UA01 ON UA01001 = OA01013
                                                   LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0  AND  (OA01015 = '' or OA01015 is null)
                                                AND  (OA01017 = '' or OA01017 is null)  AND  OA01016 = 0 AND OA01018 = 0 {0}
                                     )  GP  WHERE NUM=1 
                                     UNION ALL 
                                     SELECT *  FROM
                                     (      SELECT  OA01001,OA01002,OA01009,OA01013 , 
                                                CA01001,CA01003,UA01004,OA01015 UA01005,UA01013 ,
                                                dbo.FX_GetProvinceIdByCityId(CA01013) ProvinceId ,
                                                dbo.FX_GetProvinceByCityId(CA01013) ProvinceName ,
                                                CB04002,(OA01020* isnull(OA01016,0)) OA01020 ,
                                                OA01021,OA01015,OA01017,(OA01022*isnull(OA01016,0)) OA01022,OA01044 ,
                                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
                                            FROM OA01 
                                                INNER JOIN OB01 ON OA01999 = OB01002
                                                INNER JOIN OC01 ON OB01999 = OC01003
                                                INNER JOIN CA01 ON CA01001 = OA01038                                       
                                                INNER JOIN UA01 ON UA01004 = OA01015
                                                LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0 AND  (OA01015 <> '' and OA01015 is not null) {0}
                                    )   GP  WHERE NUM=1 
                                    UNION ALL 
                                    SELECT *  FROM
                                    (       SELECT  OA01001,OA01002,OA01009,OA01013 , 
                                                    CA01001,CA01003,OA01017 UA01004,UA01005,UA01013 ,
                                                    dbo.FX_GetProvinceIdByCityId(CA01013) ProvinceId ,
                                                    dbo.FX_GetProvinceByCityId(CA01013) ProvinceName ,
                                                    CB04002,(OA01020* isnull(OA01018,0)) OA01020 ,
                                                    OA01021,OA01015,OA01017,(OA01022*isnull(OA01018,0)) OA01022,OA01044 ,
                                                    ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
                                            FROM OA01 
                                            INNER JOIN OB01 ON OA01999 = OB01002
                                            INNER JOIN OC01 ON OB01999 = OC01003
                                            INNER JOIN CA01 ON CA01001 = OA01038                                       
                                            INNER JOIN UA01 ON UA01004 = OA01017
                                            LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0 AND  (OA01017 <> '' and OA01017 is not null) {0}
                                    )   GP  WHERE NUM=1 
                                     UNION ALL 
                                    
                                    SELECT *  FROM
                                     (      SELECT  OA01001,OA01002,OA01009,OA01013 , 
                                                CA01001,CA01003,UA01004,UA01005,UA01013 ,
                                                dbo.FX_GetProvinceIdByCityId(CA01013) ProvinceId ,
                                                dbo.FX_GetProvinceByCityId(CA01013) ProvinceName ,
                                                CB04002,(OA01020* (1-isnull(OA01016,0)-isnull(OA01018,0))) OA01020 ,
                                                OA01021,OA01015,OA01017,(OA01022*(1-isnull(OA01016,0)-isnull(OA01018,0))) OA01022,
                                                OA01044 ,
                                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
                                            FROM OA01 
                                                INNER JOIN OB01 ON OA01999 = OB01002
                                                INNER JOIN OC01 ON OB01999 = OC01003
                                                INNER JOIN CA01 ON CA01001 = OA01038                                       
                                                INNER JOIN UA01 ON UA01001 = OA01013
                                                LEFT JOIN CB04 ON CB04001 = CA01020
                                            WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0)  {0}
                                    )   GP  WHERE NUM=1 
                            ) GP ) A", strWhereAdd);
                }


                string strWhere = "";

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
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
        /// 欠款提醒统计/Debts Alert
        /// </summary>
        /// <returns></returns>
        public DataTable SelectDebtsAlertPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY OA01009 ASC ) AS RowNumber ";
                string strTableName = string.Format(@" (
                                SELECT OA01001,OA01002,OA01009,OA01013,CA01001,CA01003, 
	                                   OA01020,OA01021,OA01022,OA01044, OA01051 ,CA01025,
	                                    DATEDIFF(day,b.DebtsDays,getdate()) DebtsDays,
	                                   b.OC01015,b.OC01011,OP01005,
	                                   OP01016,UA01004,UA01013,UA01005,
	                                   ROW_NUMBER() OVER( ORDER BY OC01015 DESC ) NUM
                                FROM OA01 a
                                INNER JOIN CA01 ON CA01001 = OA01038                                       
                                INNER JOIN UA01 ON UA01001 = OA01013 
                                INNER JOIN OP01 ON OA01999 = OP01003 
                                INNER JOIN ( SELECT OA01999,MAX(OC01015) OC01015,MAX(OC01011) OC01011, MIN(OC01009) OC01009
			                                , CASE WHEN MIN(OC01015)>= MIN(OC01011) 
				                                   THEN MIN(OC01011) 
				                                   WHEN MIN(OC01015)< MIN(OC01011) 
				                                   THEN MIN(OC01011) 
				                                   WHEN MIN(OC01015) IS NULL AND MIN(OC01011) IS NOT NULL
				                                   THEN MIN(OC01011) 
				                                   WHEN MIN(OC01015) IS NOT NULL AND MIN(OC01011) IS NULL
				                                   THEN MIN(OC01015) END DebtsDays
                                        FROM OC01 
                                        INNER JOIN OB01 ON OB01999 = OC01003 
                                        INNER JOIN OA01 ON OA01999 = OB01002 
                                        GROUP BY oa01999
                                        )b ON a.oa01999=b.oa01999
                                        WHERE OA01997 = 0 AND  OA01003 = 1 AND OP01016 > 0  {0}
                                       ) A ", strWhereAdd);
                string strWhere = "";
                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value="",DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            DataTable dt = ds.Tables[0];
            if (!blInvoice) return dt;
            foreach (DataRow item in dt.Rows)
            {
                DataTable dtMoney = GetOA01020andOA01022ByOA01002(item["OA01002"].ToString());
                item["OA01020"] = dtMoney.Rows[0]["OA01020"];
                item["OA01022"] = dtMoney.Rows[0]["OA01022"];
            }
            return dt;
        }

        /// <summary>
        ///库存货物提醒/Pending Alert
        /// </summary>
        /// <returns></returns>
        public DataTable SelectPendingAlertPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"  OA01002,OB01005,OB01007,OB01008,OB01009,OC01009,
                                       OA01010, CA01001,CA01003, UA01004,UA01013,OC01015,UA01005,
                                       ROW_NUMBER() OVER(ORDER BY OA01009 ASC ) AS RowNumber,
                                       DATEDIFF(day,OA01010,getdate()) Pendingdays ";
                string strTableName = @" OB01 
                                INNER JOIN OA01 ON(OB01002=OA01999)
                                INNER JOIN CA01 ON CA01001 = OA01038 
                                INNER JOIN UA01 ON UA01001 = OA01013
                                INNER JOIN OC01 ON(OC01003=OB01999) ";
                string strWhere = @" WHERE OB01997 = 0 AND NOT EXISTS(
                        		SELECT TOP 1 1
                        		FROM dbo.OC01  
                        		JOIN dbo.OB01 ON (OC01003=OB01999) 
                        		JOIN OA01 t1 ON(OB01002=OA01999) 
                        		WHERE (OC01010<>0 or oc01007<>1) 
                        		AND oa01999=oa01.OA01999
                        	    )
                                AND (OA01003 = 1 OR OA01003 =3)  " + strWhereAdd;

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
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
        ///周欠款统计/Weekly Debts
        /// </summary>
        /// <returns></returns>
        public DataTable SelectWeeklyDebtsPage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY OA01009 ASC ) AS RowNumber ";
                string strTableName = string.Format(@" (
                               SELECT OA01001,OA01002,OA01009,OA01013,CA01001,CA01003, OA01053,
                                      OA01020,OA01021,OA01022,OA01044,OA01051 ,UA01005,
	                                    DATEDIFF(day,b.DebtsDays,getdate()) DebtsDays,
                                        b.OC01011,b.OC01009,b.OC01015,
	                                  OP01005,OP01016,
                                      UA01004,UA01013,
	                                   ROW_NUMBER() OVER( ORDER BY OC01015 DESC ) NUM
                                FROM OA01 a
                                INNER JOIN CA01 ON CA01001 = OA01038                                       
                                INNER JOIN UA01 ON UA01001 = OA01013 
                                INNER JOIN OP01 ON OA01999 = OP01003 
                                INNER JOIN ( SELECT OA01999,MAX(OC01015) OC01015,MIN(OC01011) OC01011, MIN(OC01009) OC01009
			                                , CASE WHEN MIN(OC01015)>= MIN(OC01011) 
				                                   THEN MIN(OC01011) 
				                                   WHEN MIN(OC01015)< MIN(OC01011) 
				                                   THEN MIN(OC01011) 
				                                   WHEN MIN(OC01015) IS NULL AND MIN(OC01011) IS NOT NULL
				                                  THEN MIN(OC01011) 
				                                   WHEN MIN(OC01015) IS NOT NULL AND MIN(OC01011) IS NULL
				                                   THEN MIN(OC01015) END DebtsDays
                                        FROM OC01 
                                        INNER JOIN OB01 ON OB01999 = OC01003 
                                        INNER JOIN OA01 ON OA01999 = OB01002 
                                        GROUP BY OA01999
                                        )b ON a.oa01999=b.oa01999
                                        WHERE OA01997 = 0 AND  OA01003 = 1 AND OP01016 > 0  {0}
                                       ) A ", strWhereAdd);

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
        ///周库存货物统计/Weekly Pending
        /// </summary>
        /// <returns></returns>
        public DataTable SelectWeeklyPendingPage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"  OA01002,OB01005,OB01007,OB01008,OB01009,OC01009,OC01015,OA01053,
                                       OA01009,OA01010, CA01001,CA01003, UA01004,UA01013,UA01005,
                                       ROW_NUMBER() OVER(ORDER BY OA01009 ASC ) AS RowNumber,
                                       DATEDIFF(day,OA01010,getdate()) Pendingdays ";
                string strTableName = @" OB01 
                                INNER JOIN OA01 ON(OB01002=OA01999)
                                INNER JOIN CA01 ON CA01001 = OA01038 
                                INNER JOIN UA01 ON UA01001 = OA01013
                                INNER JOIN OC01 ON(OC01003=OB01999) ";
                string strWhere = string.Format(@" WHERE OB01997 = 0  AND (OA01003 = 1 OR OA01003 =3) 
                                 AND NOT EXISTS(
		                            SELECT TOP 1 1
		                            FROM dbo.OC01  
		                            JOIN dbo.OB01 ON (OC01003=OB01999) 
		                            JOIN OA01 t1 ON(OB01002=OA01999) 
                                    INNER JOIN CA01 ON CA01001 = OA01038 
                                    INNER JOIN UA01 ON UA01001 = OA01013
		                            WHERE (OC01010<>0 or oc01007<>1) 
		                            AND oa01999=oa01.OA01999 {0} ) 
                                  AND NOT EXISTS(
		                              SELECT  TOP 1 1
		                              FROM dbo.OC01  
		                              JOIN dbo.OB01 ON (OC01003=OB01999) 
		                              JOIN OA01 t1 ON(OB01002=OA01999) 
		                              WHERE t1.OA01999=OA01.OA01999 
		                              AND OC01010 = 0 AND oc01007 = 0) {0} ", strWhereAdd);

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value="",DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        #endregion

        #region 月报（Monthly Report）
        /// <summary>
        /// 按行业代码统计/Sales by SIC
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlySICPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY CB04002 ASC ) AS RowNumber  ";
                string strTableName;
                if (blInvoice)
                {
                    //按开发票统计
                    strTableName = string.Format(@"
                        (SELECT CB04002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                        COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                        SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                        FROM (
		                        SELECT CB04002,OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND  (OA01015 = '' or OA01015 is null)
			                        AND  (OA01017 = '' or OA01017 is null)
			                        AND  OA01016 = 0 AND OA01018 = 0
	                        UNION ALL
		                        SELECT CB04002,OB01007*OB01008*OA01016 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
			                        , OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01015 <> '' and OA01015 is not null)
	                        UNION ALL
		                        SELECT CB04002,OB01007*OB01008*OA01018 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022, OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01017 <> '' and OA01017 is not null) 
	                        UNION ALL
		                        SELECT CB04002,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,OA01002 ,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0} 
			                        AND (OA01016 <> 0 OR OA01018 <> 0) 
	                        
                        ) ABCD 
                        GROUP BY CB04002
                ) A", strWhereAdd);
                }
                else
                {
                    //按订单金额统计
                    strTableName = string.Format(@"
                        (SELECT CB04002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                        COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                        SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                        FROM (
	                        SELECT CB04002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB04002,OA01020,OA01022,OA01002,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND  (OA01015 = '' or OA01015 is null)
			                        AND  (OA01017 = '' or OA01017 is null)
			                        AND  OA01016 = 0 AND OA01018 = 0
	                        ) A 
	                        WHERE A.NUM = 1 
	                        UNION ALL
	                        SELECT CB04002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB04002,OA01020*OA01016 AS OA01020
			                        ,OA01022*OA01016 AS OA01022
			                        , OA01002,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01015 <> '' and OA01015 is not null)
	                        ) A 
	                        WHERE A.NUM = 1 
	                        UNION ALL
	                        SELECT CB04002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB04002,OA01020*OA01018 AS OA01020
			                        ,OA01022*OA01018 AS OA01022
			                        , OA01002,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01017 <> '' and OA01017 is not null) 
	                        ) A 
	                        WHERE A.NUM = 1 
	                        UNION ALL
	                        SELECT CB04002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB04002,OA01020*(1-OA01016-OA01018) AS OA01020
			                        ,OA01022*(1-OA01016-OA01018) AS OA01022
			                        ,OA01002
			                        ,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB04 ON CB04001 = OA01041
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0} 
			                        AND (OA01016 <> 0 OR OA01018 <> 0) 
	                        ) A 
	                        WHERE A.NUM = 1 
                        ) ABCD 
                        GROUP BY CB04002
                ) A", strWhereAdd);
                }



                string strWhere = "";

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value=" ",DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 利润率统计/Sakes By GP
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlySakesByGPPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" * ,ROW_NUMBER() OVER(ORDER BY OA01009 DESC ) AS RowNumber";
                string strTableName = string.Empty;
                if (blInvoice)
                {
                    strTableName = string.Format(@"
                    (
                        SELECT * ,Oa01022-OA01019*1.15 as Profit FROM
                        (
							SELECT OA01002 ,CA01003 ,UA01004 ,UA01005,OA01019,OA01009,SUM(OA01022) OA01022
							FROM (
		                        SELECT OA01002 ,CA01003 ,UA01004 ,UA01005,OA01019,OA01009
                                ,round((OB01009/OA01021/1.17),2) OA01022
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        WHERE OA01997 = 0 AND OA01003 <> 3 {0}
			                        AND  (OA01015 = '' or OA01015 is null)
			                        AND  (OA01017 = '' or OA01017 is null)
			                        AND  OA01016 = 0 AND OA01018 = 0
	                            UNION ALL
		                        SELECT OA01002 ,CA01003 ,UA01004 ,UA01005,OA01019,OA01009
                                ,round((OB01009/OA01021/1.17),2)*OA01016 OA01022
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038  
							    INNER JOIN UA01 ON UA01004 = OA01015
		                        WHERE OA01997 = 0 AND OA01003 <> 3 {0}
			                        AND (OA01015 <> '' and OA01015 is not null)
	                            UNION ALL
								SELECT OA01002 ,CA01003 ,UA01004 ,UA01005,OA01019,OA01009
                                ,round((OB01009/OA01021/1.17),2)*OA01018 OA01022		                        
                                FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        WHERE OA01997 = 0 AND OA01003 <> 3 {0}
			                        AND (OA01017 <> '' and OA01017 is not null) 
	                            UNION ALL
							    SELECT OA01002 ,CA01003 ,UA01004 ,UA01005,OA01019,OA01009
                                ,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) OA01022		                        
                                FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        WHERE OA01997 = 0 AND OA01003 <> 3 {0}
			                        AND (OA01016 <> 0 OR OA01018 <> 0) 
							) ABCD 
							GROUP BY OA01002,CA01003,UA01004,UA01005,OA01009,OA01019
                        )tab
                    ) A", strWhereAdd);

                }
                else
                {
                    strTableName = string.Format(@"
                       (
                        SELECT * ,Oa01022-OA01019*1.15 as Profit FROM
                        (
                            SELECT OA01002 ,CA01003 ,UA01004 ,UA01005
                                ,dbo.FX_GetIndustryReportPrice(OA01002,OA01022) OA01022
                                ,OA01019,OA01009
                                ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC ) AS NUM
                            FROM CA01        
                            JOIN OA01 on OA01038=CA01001
                            JOIN UA01 on UA01001=OA01013 
                            JOIN OB01 on OB01002=OA01999 
                            JOIN OC01 on OC01003=OB01999
                            WHERE OA01997 = 0 AND OA01003 = 1 {0}
                        )  GP  WHERE NUM=1
                    ) A", strWhereAdd);
                }


                //string strWhere = string.Format(" where OA01997 = 0 AND OA01003 = 1 {0}", strWhereAdd);

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
        /// 重点客户统计/Top Customer
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlyTopCustomerPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = " * ,ROW_NUMBER() over(order by OA01020 desc) AS RowNumber";
                string strTableName;
                if (blInvoice)
                {
                    //按开发票统计
                    strTableName = string.Format(@" 
                    (
                        SELECT CA01003,CA01004,CB04002,SUM(OA01020) OA01020,
		                        SUM(OA01022) OA01022,SUM(OA01002) OA01002,UA01005
                        FROM (
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(distinct OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002,OA01003
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01001 = OA01013
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0 
				                        AND  (OA01015 = '' or OA01015 is null)   {0}
				                        AND  (OA01017 = '' or OA01017 is null)
                                        AND  OA01016 = 0 AND OA01018 = 0
		                         ) A 
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(distinct OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,OB01007*OB01008*OA01016 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,OA01002,OA01003
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01004 = OA01015
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0  {0}
				                         AND (OA01015 <> '' and OA01015 is not null) 
		                         ) A 
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(distinct OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,OB01007*OB01008*OA01018 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022 ,OA01002,OA01003
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01004 = OA01017
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0   {0}
				                         AND (OA01017 <> '' and OA01017 is not null)
		                         ) A 
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(distinct OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,OA01002,OA01003
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01001 = OA01013
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0   {0}
				                        AND  (OA01016 <> 0 OR OA01018<>0)
		                         ) A 
		                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
                        ) A  GROUP BY CA01003,CA01004,CB04002,UA01005
                ) A ", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@" 
                    (
                        SELECT CA01003,CA01004,CB04002,SUM(OA01020) OA01020,
		                        SUM(OA01022) OA01022,SUM(OA01002) OA01002,UA01005
                        FROM (
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,OA01020,OA01022,OA01002,OA01003
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01001 = OA01013
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0 
				                        AND  (OA01015 = '' or OA01015 is null)   {0}
				                        AND  (OA01017 = '' or OA01017 is null)
                                        AND  OA01016 = 0 AND OA01018 = 0
		                         ) A where NUM =1
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,(OA01020*OA01016) as OA01020
			                        ,(OA01022*OA01016) as OA01022 ,OA01002,OA01003
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01004 = OA01015
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0  {0}
				                         AND (OA01015 <> '' and OA01015 is not null) 
		                         ) A where NUM =1
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,(OA01020*OA01018) as OA01020
			                        ,(OA01022*OA01018) as OA01022 ,OA01002,OA01003
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01004 = OA01017
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0   {0}
				                         AND (OA01017 <> '' and OA01017 is not null)
		                         ) A where NUM =1
	                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
	                        UNION ALL
	                        SELECT  CA01003,CA01004,CB04002
			                        ,SUM(OA01020) OA01020,SUM(OA01022) OA01022
			                        ,COUNT(OA01002) OA01002
			                        ,UA01005 
	                        FROM (
		                         SELECT CA01001,CA01003,CA01004,UA01001,UA01004,UA01005
			                        ,CB04002,OA01020*(1-OA01016-OA01018) as OA01020
			                        ,OA01022*(1-OA01016-OA01018) as OA01022 ,OA01002,OA01003
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
		                         FROM OA01
		                         INNER JOIN OB01 on OB01002 = OA01999 
		                         INNER JOIN OC01 on OC01003 = OB01999
		                         INNER JOIN UA01 ON UA01001 = OA01013
		                         INNER JOIN CA01 ON CA01001 = OA01038
		                         LEFT JOIN CB04 ON CB04001 = CA01020
		                         Where OA01003 <> 3 and OA01997 = 0   {0}
				                        AND  (OA01016 <> 0 OR OA01018<>0)
		                         ) A where NUM =1
		                        GROUP BY CA01001,CA01003,CA01004,CB04002,UA01001,UA01004,UA01005
                        ) A  GROUP BY CA01003,CA01004,CB04002,UA01005
                ) A ", strWhereAdd);
                }
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
        /// 重点客户统计2/Top Customer2
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectTopCustomerTwoPage(int PageSize, int PageIndex, string[] strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                if (blInvoice)
                {
                    ds = Provider.ReturnDataSetByDataAdapter("PRO_Page_TopCustomerTwoInvoice", 1, ref obj, new SqlParameter[]{
                        new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                        new SqlParameter(){ParameterName=@"YEAR",Value=strWhereAdd[1], DbType=DbType.String},
                        new SqlParameter(){ParameterName=@"Where",Value=strWhereAdd[0], DbType=DbType.String},
                        new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32}});
                }
                else
                {
                    ds = Provider.ReturnDataSetByDataAdapter("PRO_Page_TopCustomerTwo", 1, ref obj, new SqlParameter[]{
                        new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                        new SqlParameter(){ParameterName=@"YEAR",Value=strWhereAdd[1], DbType=DbType.String},
                        new SqlParameter(){ParameterName=@"Where",Value=strWhereAdd[0], DbType=DbType.String},
                        new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32}});
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 重点客户统计2/Top Customer2 总行数
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectTopCustomerTwoCountPage(int PageSize, int PageIndex, string[] strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                if (blInvoice)
                {
                    ds = Provider.ReturnDataSetByDataAdapter("PRO_Page_TopCustomerTwoCountInvoice", 1, ref obj, new SqlParameter[]{
                         new SqlParameter(){ParameterName=@"YEAR",Value=strWhereAdd[1], DbType=DbType.String},
                         new SqlParameter(){ParameterName=@"Where",Value=strWhereAdd[0], DbType=DbType.String},
                    });
                }
                else
                {
                    ds = Provider.ReturnDataSetByDataAdapter("PRO_Page_TopCustomerTwoCount", 1, ref obj, new SqlParameter[]{
                         new SqlParameter(){ParameterName=@"YEAR",Value=strWhereAdd[1], DbType=DbType.String},
                         new SqlParameter(){ParameterName=@"Where",Value=strWhereAdd[0], DbType=DbType.String},
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 重点产品统计/Hot Part List
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlyHotPartListPage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" SUM(OB01007) AS OA01002,OB01005,
		                             ROW_NUMBER() OVER(ORDER BY SUM(OB01007) DESC ) AS RowNumber";
                string strTableName = @" CA01 
                                       LEFT join CB04 on CB04001=CA01020
                                       join OA01 on OA01038=CA01001
                                       join UA01 on UA01001=OA01013 
                                       join OB01 on OB01002=OA01999 
                                       join OC01 on OC01003=OB01999   ";

                string strWhere = string.Format(" where OA01997 = 0 AND OA01003 = 1 {0}", strWhereAdd);

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value="  group by OB01005 ",DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 重点产品统计/Hot Part List2
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlyHotPartListTwoPage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"count(OA01002)as OA01002,OB01005,SUM(OA01022) as oa01022,
		                             CA01004 ,CB04002 ,
		                             ROW_NUMBER() OVER(ORDER BY OB01005 ASC ) AS RowNumber";
                string strTableName = @" CA01 
                                       LEFT join CB04 on CB04001=CA01020
                                       join OA01 on OA01038=CA01001
                                       join UA01 on UA01001=OA01013 
                                       join OB01 on OB01002=OA01999 
                                       join OC01 on OC01003=OB01999   ";

                string strWhere = string.Format(" where OA01997 = 0 AND  OA01003 <> 3  {0}", strWhereAdd);

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value="   group by OB01005,CA01004,CB04002 ",DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 按销售员统计/SalesStatistics
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlySalesStatistics(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY UA01005 ASC ) AS RowNumber";

                string strTableName;

                if (blInvoice)
                {
                    //开发票统计
                    strTableName = string.Format(@"
                        (SELECT UA01005,SUM(OA01022)OA01022,count(OA01002) OA01001
								,SUM(DISTINCT OA01003) OA01003,SUM(DISTINCT OA010031) OA010031
								, count(DISTINCT OA01038) OA01038,SUM(OA01044) OA01044,SUM(OA01054) OA01054
							FROM(
								SELECT * 
									FROM(
										SELECT UA01005
											,SUM(OA01022) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											, OA01038,OA01002
											,SUM(DISTINCT(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END )) OA01044
											,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2) OA01022,OA01003,OA01038,OA01044,OA01054,OA01013,OA01002
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01001 = OA01013 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND  (OA01015 = '' or OA01015 is null) {0}
												AND  (OA01017 = '' or OA01017 is null)  
                                                AND  OA01016 = 0 AND OA01018 = 0 
										)A 
										GROUP BY UA01005,OA01038,OA01002	
									) A
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
											,SUM(OA01022) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											,0 OA01038,OA01002 
											,0 OA01044
											,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,OA01003,OA01038,OA01044,OA01054,OA01013,OA01002,OA01016
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01015 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01015 <> '' and OA01015 is not null)  {0}
										) B 
										GROUP BY UA01005,OA01002	
									) B
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
											,SUM(OA01022) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											,0 OA01038,OA01002 
											,0 OA01044
											,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022,OA01003,OA01038,OA01044,OA01054,OA01013,OA01002,OA01018
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01017
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01017 <> '' and OA01017 is not null) {0}
										) C 
										GROUP BY UA01005,OA01002
									) C
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
												,SUM(OA01022) OA01022
												,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
												,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
												,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
												, OA01038,OA01002
												,SUM(DISTINCT(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END )) OA01044
											    ,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
											FROM (
												SELECT  UA01005,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,OA01003,OA01038,OA01044,OA01054,OA01013,OA01018,OA01016,OA01002
												FROM OA01
												JOIN CA01 ON CA01001 = OA01038
												LEFT JOIN CB04 ON CB04001 = CA01020
												JOIN UA01 ON UA01001 = OA01013
												JOIN OB01 ON OA01999 = OB01002
												JOIN OC01 ON OB01999 = OC01003
												WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0) {0}
											) D
										GROUP BY UA01005,OA01038,OA01002	
									) D
								)TOTAL
							GROUP BY UA01005
                ) A", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@"
                        (SELECT UA01005,SUM(OA01022)OA01022,count(OA01002) OA01001
								,SUM(DISTINCT OA01003) OA01003,SUM(DISTINCT OA010031) OA010031
								, count(DISTINCT OA01038) OA01038,SUM(OA01044) OA01044,sum(OA01054) OA01054
							FROM(
								SELECT * 
									FROM(
										SELECT UA01005
											,SUM(OA01022) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											, OA01038,OA01002
											,SUM(DISTINCT(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END )) OA01044,OA01054
									   FROM (
											SELECT  UA01005,OA01022,OA01003,OA01038,OA01044,OA01013,OA01002
												,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM,isnull(OA01054,0) OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01001 = OA01013 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND  (OA01015 = '' or OA01015 is null) {0}
												AND  (OA01017 = '' or OA01017 is null)  
                                                AND  OA01016 = 0 AND OA01018 = 0 
										)A WHERE NUM = 1 
										GROUP BY UA01005,OA01038,OA01002,OA01054	
									) A
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
											,SUM(OA01022*OA01016) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											,0 OA01038,OA01002 
											,0 OA01044,OA01054
									   FROM (
											SELECT  UA01005,OA01022,OA01003,OA01038,OA01044,OA01013,OA01002,OA01016
												,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM,isnull(OA01054,0) OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01015 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01015 <> '' and OA01015 is not null)  {0}
										) B WHERE NUM = 1 
										GROUP BY UA01005,OA01002,OA01054	
									) B
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
											,SUM(OA01022*OA01018) OA01022,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
											,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
											,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
											,0 OA01038,OA01002 
											,0 OA01044,OA01054
									   FROM (
											SELECT  UA01005,OA01022,OA01003,OA01038,OA01044,OA01013,OA01002,OA01018
												,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM,isnull(OA01054,0) OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01017
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01017 <> '' and OA01017 is not null) {0}
										) C WHERE NUM = 1 
										GROUP BY UA01005,OA01002,OA01054
									) C
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005
												,SUM(OA01022*(1-OA01016-OA01018)) OA01022
												,SUM(CASE WHEN OA01003=1 THEN OA01003 ELSE 0 END) OA01001
												,COUNT(CASE OA01003 WHEN 2 THEN  OA01003 END) OA01003
												,COUNT(CASE OA01003 WHEN 3 THEN  OA01003 END) OA010031
												, OA01038,OA01002
												,SUM(DISTINCT(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END )) OA01044,OA01054
											FROM (
												SELECT  UA01005,OA01022,OA01003,OA01038,OA01044,OA01013,OA01018,OA01016,OA01002
													,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM,isnull(OA01054,0) OA01054
												FROM OA01
												JOIN CA01 ON CA01001 = OA01038
												LEFT JOIN CB04 ON CB04001 = CA01020
												JOIN UA01 ON UA01001 = OA01013
												JOIN OB01 ON OA01999 = OB01002
												JOIN OC01 ON OB01999 = OC01003
												WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0) {0}
											) D WHERE NUM = 1 
										GROUP BY UA01005,OA01038,OA01002,OA01054	
									) D
								)TOTAL
							GROUP BY UA01005
                ) A", strWhereAdd);
                }
                string strWhere = "";

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value=" ",DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 客户类型/By Customer Type 
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlyByCustomerTypePage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = " * ,ROW_NUMBER() over(order by OA01022 DESC) AS RowNumber";
                string strTableName = string.Empty;
                if (blInvoice)
                {
                    strTableName = string.Format(@"
                                        ( SELECT CB01002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                        COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                        COUNT(DISTINCT CASE WHEN OA01044=1 THEN CA01001 ELSE 0 END)-1  as OA01044 
                        FROM (
		                        SELECT CB01002,OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB01 ON CB01001=CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND  (OA01015 = '' or OA01015 is null)
			                        AND  (OA01017 = '' or OA01017 is null)
			                        AND  OA01016 = 0 AND OA01018 = 0
	                        UNION ALL 
		                        SELECT CB01002,OB01007*OB01008*OA01016 AS OA01020
			                        ,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
			                        , OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        LEFT JOIN CB01 ON CB01001=CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01015 <> '' and OA01015 is not null)
	                        UNION ALL 
		                        SELECT CB01002,OB01007*OB01008*OA01018 AS OA01020
			                        ,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022
			                        , OA01002,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        LEFT JOIN CB01 ON CB01001 = CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01017 <> '' and OA01017 is not null) 
	                        UNION ALL 
		                        SELECT CB01002,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020
			                        ,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022
			                        ,OA01002
			                        ,CA01001,OA01044
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB01 ON CB01001 = CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0} 
			                        AND (OA01016 <> 0 OR OA01018 <> 0) 
                        ) ABCD 
                        GROUP BY CB01002 ) a ", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@"
                                        ( SELECT CB01002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                        COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                        COUNT(DISTINCT CASE WHEN OA01044=1 THEN CA01001 ELSE 0 END)-1  as OA01044 
                        FROM (
	                        SELECT CB01002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB01002,OA01020,OA01022,OA01002,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB01 ON CB01001=CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND  (OA01015 = '' or OA01015 is null)
			                        AND  (OA01017 = '' or OA01017 is null)
			                        AND  OA01016 = 0 AND OA01018 = 0
	                        ) A 
	                        WHERE A.NUM = 1 
	                        UNION ALL 
	                        SELECT CB01002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB01002,OA01020*OA01016 AS OA01020
			                        ,OA01022*OA01016 AS OA01022
			                        , OA01002,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        LEFT JOIN CB01 ON CB01001=CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01015 <> '' and OA01015 is not null)
	                        ) A 
	                        WHERE A.NUM = 1 
	                        UNION ALL 
	                        SELECT CB01002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB01002,OA01020*OA01018 AS OA01020
			                        ,OA01022*OA01018 AS OA01022
			                        , OA01002,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        LEFT JOIN CB01 ON CB01001 = CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                        AND (OA01017 <> '' and OA01017 is not null) 
	                        ) A 
	                        WHERE A.NUM = 1 
	                        UNION ALL 
	                        SELECT CB01002,OA01020,OA01022,OA01002,CA01001,OA01044
	                        FROM (
		                        SELECT CB01002,OA01020*(1-OA01016-OA01018) AS OA01020
			                        ,OA01022*(1-OA01016-OA01018) AS OA01022
			                        ,OA01002
			                        ,CA01001,OA01044
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                        FROM OA01 
		                        INNER JOIN OB01 ON OA01999 = OB01002
		                        INNER JOIN OC01 ON OB01999 = OC01003
		                        INNER JOIN CA01 ON CA01001 = OA01038                                       
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        LEFT JOIN CB01 ON CB01001 = CA01016 
		                        WHERE OA01997 = 0 AND OA01003 <> 3   {0} 
			                        AND (OA01016 <> 0 OR OA01018 <> 0) 
	                        ) A 
	                        WHERE A.NUM = 1 
                        ) ABCD 
                        GROUP BY CB01002 ) a ", strWhereAdd);
                }

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
        /// 按开发票金额统计
        /// </summary>
        /// <returns></returns>
        public DataTable SelectInvoicesAmountOfMoneyPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {

            DataSet ds;
            try
            {
                string strColumn = " * ,ROW_NUMBER() over(order by sumrmb desc) AS RowNumber";
                string strTableName = string.Empty;
                if (blInvoice)
                {
                    strTableName = string.Format(@" 
                    (
                        SELECT UA01005,SUM(DISTINCT COUNT) count,SUM(sumrmb)sumrmb,SUM(sumus) sumus
                        FROM
                        (
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
		                           AND  (OA01015 = '' or OA01015 is null)
		                           AND  (OA01017 = '' or OA01017 is null) 
                                   AND  OA01016 = 0 AND OA01018 = 0
	                        ) A  
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008*OA01016 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,OA01016,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
			                        AND (OA01015 <> '' and OA01015 is not null) 
	                        ) B
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008*OA01018 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022,OA01018,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
		                        AND (OA01017 <> '' and OA01017 is not null)
	                        ) C
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,OA01018,OA01016,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
		                         AND  (OA01016 <> 0 OR OA01018<>0)
	                        ) D
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
                        ) A
                        GROUP BY UA01001,UA01005
                ) a ", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@" 
                    (
                        SELECT UA01005,SUM(DISTINCT COUNT) count,SUM(sumrmb)sumrmb,SUM(sumus) sumus
                        FROM
                        (
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01002
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1  {0}
		                           AND  (OA01015 = '' or OA01015 is null)
		                           AND  (OA01017 = '' or OA01017 is null) 
                                   AND  OA01016 = 0 AND OA01018 = 0
	                        ) A  
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
			                        SUM(OA01020*OA01016) as sumrmb,
			                        SUM(OA01022*OA01016) as sumus
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01016,OA01002
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1 {0}
			                        AND (OA01015 <> '' and OA01015 is not null) 
	                        ) B
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
			                        SUM(OA01020*OA01018) sumrmb,
			                        SUM(OA01022*OA01018) sumus
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01018,OA01002
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1 {0}
		                        AND (OA01017 <> '' and OA01017 is not null)
	                        ) C
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
		                        SUM(OA01020*(1-OA01016-OA01018))sumrmb,
		                        SUM(OA01022*(1-OA01016-OA01018))sumus
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01018,OA01016
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1 {0}
		                         AND  (OA01016 <> 0 OR OA01018<>0)
	                        ) D
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
                        ) A
                        GROUP BY UA01001,UA01005
                ) a ", strWhereAdd);
                }

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




            //            DataSet ds;
            //            try
            //            {
            //                string strColumn = @" UA01004 ,count(1) count,SUM(OB01009) sumrmb,
            //	                CAST(ROUND(SUM(OB01009)/1.17, 2) as decimal(38,2)) sumus,UA01013,
            //                    ROW_NUMBER() OVER(ORDER BY UA01004) RowNumber  ";
            //                string strTableName = @" OA01  
            //			        INNER JOIN UA01 ON UA01001 = OA01013
            //		            INNER JOIN OB01 on OB01002=OA01999 
            //                    INNER JOIN OC01 on OC01003=OB01999
            //                    INNER JOIN CA01 on OA01038=CA01001";

            //                string strWhere = @" WHERE OA01997=0 AND OA01003=1 AND OC01014 =1 " + strWhereAdd;
            //                string strOrder = "";
            //                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
            //                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
            //                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
            //                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
            //                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
            //                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
            //                new SqlParameter(){ParameterName=@"Order", Value=strOrder,DbType=DbType.String}
            //            });
            //            }
            //            catch (Exception ex)
            //            {
            //                throw ex;
            //            }
            //            return ds.Tables[0];
        }

        /// <summary>
        /// 按开发票金额统计(统计总金额)
        /// </summary>
        /// <returns></returns>
        public DataTable SelectInvoicesAmountOfMoneyPage(string strWhereAdd, bool blInvoice)
        {
            DataSet ds;
            try
            {
                string strsql = string.Empty;
                object obj = null;
                if (blInvoice)
                {
                    strsql = string.Format(@" SELECT UA01005,SUM(DISTINCT COUNT) count,SUM(sumrmb)sumrmb,SUM(sumus) sumus
                        FROM
                        (
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
		                           AND  (OA01015 = '' or OA01015 is null)
		                           AND  (OA01017 = '' or OA01017 is null) 
                                   AND  OA01016 = 0 AND OA01018 = 0
	                        ) A  
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008*OA01016 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,OA01016,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
			                        AND (OA01015 <> '' and OA01015 is not null) 
	                        ) B
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008*OA01018 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022,OA01018,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
		                        AND (OA01017 <> '' and OA01017 is not null)
	                        ) C
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,OA01018,OA01016,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1  {0}
		                         AND  (OA01016 <> 0 OR OA01018<>0)
	                        ) D
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
                        ) A
                        GROUP BY UA01001,UA01005 ", strWhereAdd);
                }
                else
                {
                    strsql = string.Format(@" SELECT UA01005,SUM(DISTINCT COUNT) count,SUM(sumrmb)sumrmb,SUM(sumus) sumus
                        FROM
                        (
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
				                        sum(OA01020) sumrmb ,sum(OA01022) sumus 
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01002
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1  {0}
		                           AND  (OA01015 = '' or OA01015 is null)
		                           AND  (OA01017 = '' or OA01017 is null) 
                                   AND  OA01016 = 0 AND OA01018 = 0   
	                        ) A  
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
			                        SUM(OA01020*OA01016) as sumrmb,
			                        SUM(OA01022*OA01016) as sumus
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01016,OA01002
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01015
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1 {0}
			                        AND (OA01015 <> '' and OA01015 is not null) 
	                        ) B
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
			                        SUM(OA01020*OA01018) sumrmb,
			                        SUM(OA01022*OA01018) sumus
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01018,OA01002
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01004 = OA01017
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1 {0}
		                        AND (OA01017 <> '' and OA01017 is not null)
	                        ) C
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
	                        UNION ALL
	                        SELECT  UA01001,UA01004,UA01005,COUNT(DISTINCT OA01002) count,
		                        SUM(OA01020*(1-OA01016-OA01018))sumrmb,
		                        SUM(OA01022*(1-OA01016-OA01018))sumus
	                        FROM(
		                        SELECT  UA01001,UA01004,UA01005,UA01013, OA01020,OA01022,OA01018,OA01016
			                        ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) num,OA01002
		                        FROM OA01  
		                        INNER JOIN UA01 ON UA01001 = OA01013
		                        INNER JOIN OB01 ON OB01002=OA01999 
		                        INNER JOIN OC01 ON OC01003=OB01999
		                        INNER JOIN CA01 ON OA01038=CA01001
		                        WHERE OA01997=0 AND OA01003=1 AND OC01014 =1 {0}
		                        AND  (OA01016 <> 0 OR OA01018<>0)
	                        ) D
	                        WHERE num =1 
	                        GROUP BY UA01001,UA01004,UA01013 ,UA01005
                        ) A
                        GROUP BY UA01001,UA01005 ", strWhereAdd);
                }




                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        ///迟交货统计
        /// </summary>
        /// <returns></returns>
        public DataTable SelectLateDeliveryPage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"   OA01002, CA01001,CA01003,OB01005,OC01008,
                                OB01007,OB01009,OB01011,OB01014,OA01009,OA01010,OC01009,
                                ROW_NUMBER() over(order by UA01004)AS RowNumber ,ISNULL(OB01014,0) PB01004 ";
                string strTableName = @" OA01  
		                        INNER JOIN CA01 ON CA01001 = OA01038
                                INNER JOIN UA01 ON UA01001 = OA01013
                                INNER JOIN OB01 ON OB01002 = OA01999 
                                INNER JOIN OC01 ON OC01003 = OB01999 ";
                string strWhere = " WHERE OA01997=0 and OA01010 < isnull(OC01009,GETDATE()) " + strWhereAdd;

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
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
        /// 行业报告/Industry report
        /// </summary>
        /// <returns></returns>
        public DataTable SelectIndustryReportPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = " * ,ROW_NUMBER() over(order by OA01022 desc) AS RowNumber";
                string strTableName = string.Empty;
                if (blInvoice)
                {
                    strTableName = string.Format(@" (
                                            SELECT OA01002,CA01003,UA01001,UA01005,SUM(OA01022) OA01022,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,round((OB01009/OA01021/1.17),2) OA01022
                                                    ,CB02002,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240,GA06003 get220
                                                    ,OA01028,CA01009,CA01010
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01001 = OA01013 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3    {0}
			                                            AND  (OA01015 = '' or OA01015 is null)  
                                                        AND  (OA01017 = '' or OA01017 is null)
                                                        AND  OA01016 = 0 AND OA01018 = 0   
                                            ) A 
                                            GROUP BY OA01002,CA01003,UA01001,UA01005,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            UNION ALL
                                            SELECT OA01002,CA01003,UA01001,UA01005,SUM(OA01022) OA01022,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,CB02002
				                                            ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                                            ,GA06003 get220,OA01028,CA01009,CA01010
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01004 = OA01015 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3    {0}   
			                                            AND (OA01015 <> '' and OA01015 is not null) 
                                            ) A 
                                            GROUP BY OA01002,CA01003,UA01001,UA01005,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            UNION ALL
                                            SELECT  OA01002,CA01003,UA01001,UA01005,SUM(OA01022) OA01022,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022,CB02002
				                                            ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                                            ,GA06003 get220,OA01028,CA01009,CA01010
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01004 = OA01017 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3    {0}
			                                            AND (OA01017 <> '' and OA01017 is not null)
                                            ) A 
                                            GROUP BY OA01002,CA01003,UA01001,UA01005,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            UNION ALL
                                            SELECT OA01002,CA01003,UA01001,UA01005,SUM(OA01022) OA01022,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,CB02002
				                                            ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                                            ,GA06003 get220,OA01028,CA01009,CA01010
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01001 = OA01013 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3   {0}
			                                             AND  (OA01016 <> 0 OR OA01018<>0) 
                                            ) A 
                                            GROUP BY OA01002,CA01003,UA01001,UA01005,CB02002,CB04002,OB02002,OA01045
                                                   ,Province,get240,get220,OA01028,CA01009,CA01010
                                         ) a ", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@" (
                                     SELECT *
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,OA01022,CB02002,CB04002,OB02002,OA01045
				                                            ,P.GA03002 Province ,GA05003 get240,GA06003 get220,OA01028,CA01009
				                                            ,CA01010,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01022) NUM
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01001 = OA01013 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3    {0}
			                                            AND  (OA01015 = '' or OA01015 is null)  
                                                        AND  (OA01017 = '' or OA01017 is null)
                                                        AND  OA01016 = 0 AND OA01018 = 0   
                                            ) A 
                                            WHERE NUM =1 
                                            UNION ALL
                                            SELECT *
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,OA01022*OA01016 OA01022,CB02002
				                                            ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                                            ,GA06003 get220,OA01028,CA01009
				                                            ,CA01010,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01022) NUM
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01004 = OA01015 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3    {0}   
			                                            AND (OA01015 <> '' and OA01015 is not null) 
                                            ) A 
                                            WHERE NUM =1 
                                            UNION ALL
                                            SELECT *
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,OA01022*OA01018 OA01022,CB02002
				                                            ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                                            ,GA06003 get220,OA01028,CA01009
				                                            ,CA01010,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01022) NUM
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01004 = OA01017 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3    {0}
			                                            AND (OA01017 <> '' and OA01017 is not null)
                                            ) A 
                                            WHERE NUM =1 
                                            UNION ALL
                                            SELECT *
                                            FROM (
	                                            SELECT OA01002,CA01003,UA01001,UA01005,OA01022*(1-OA01016-OA01018) OA01022,CB02002
				                                            ,CB04002,OB02002,OA01045,P.GA03002 Province ,GA05003 get240
				                                            ,GA06003 get220,OA01028,CA01009
				                                            ,CA01010,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01022) NUM
	                                            FROM OA01
	                                            INNER JOIN UA01 ON UA01001 = OA01013 
	                                            INNER JOIN CA01 ON CA01001 = OA01038 	
	                                            LEFT JOIN CB02 ON CA01018 = CB02001
	                                            LEFT JOIN CB04 ON CA01020 = CB04001
	                                            LEFT JOIN OB02 ON OB02001 = OA01025
	                                            INNER JOIN OB01 ON OB01002 = OA01999 
	                                            INNER JOIN OC01 ON OC01003 = OB01999
	                                            INNER JOIN GA03 ON GA03001 = CA01013
	                                            INNER JOIN GA03 P ON GA03.GA03003 = P.GA03001
	                                            LEFT JOIN GA05 ON GA05001 = OA01042
	                                            LEFT JOIN GA06 ON GA06001 = OA01043
	                                            WHERE OA01997 = 0 AND OA01003<>3   {0}
			                                             AND  (OA01016 <> 0 OR OA01018<>0) 
                                            ) A 
                                            WHERE NUM =1 ) a ", strWhereAdd);
                }



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
        /// 睡眠客户
        /// </summary>
        /// <returns></returns>
        public DataTable SelectSleepCustomerPage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = " * ,ROW_NUMBER() over(order by OA01009 desc) AS RowNumber";
                string strTableName = string.Format(@" (SELECT * 
                                FROM (
	                                SELECT * 
	                                FROM 
		                                (SELECT CA01003,OA01002,OA01009,UA01001,UA01004,UA01005,UA01013,OA01025,CA01020
	                                            ,ROW_NUMBER() over(partition by CA01001 order by OA01009 DESC) as rownum
	                                            ,CB04002,CA01009,CA01010,GA03002,dbo.FX_GetProvinceByCityId(CA01013) Province
                                                FROM  CA01
                                                INNER JOIN OA01 ON CA01001 = OA01038 
                                                INNER JOIN UA01 ON UA01001 = OA01013
                                                LEFT JOIN CB04 ON CB04001 = CA01020
                                                INNER JOIN GA03 ON GA03001 = CA01013
                                                WHERE OA01997 = 0
                                   ) SleepCustomer 
                                WHERE SleepCustomer.rownum = 1  AND  DATEDIFF(MONTH,OA01009,getdate())>12 {0} ) a) a ", strWhereAdd);
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
        /// 年销售额
        /// </summary>
        /// <returns></returns>
        public DataTable SelectSalesYearPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = " * ,ROW_NUMBER() OVER(ORDER BY YEARS DESC) AS RowNumber";
                string strTableName = string.Empty;
                if (blInvoice)
                {
                    strTableName = string.Format(@" 
                                    (SELECT '' YEARS,COUNT(distinct CustomerNum) CUSTOMERCOUNT
                                            ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) OA01044
                                            ,COUNT(distinct OrderNum) ORDERCOUNT  ,SUM(OA01022) OA01022
                                            ,SUM(OA01020) OA01020
                                        FROM (
			                                        SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                           OA01038 AS CustomerNum,
			                                               OA01044 AS NewCustomerNum,
			                                               OA01002 AS OrderNum,
			                                              round((OB01009/OA01021/1.17),2) OA01022 ,OB01007*OB01008 OA01020
		                                            FROM dbo.OA01 
		                                            JOIN dbo.CA01 ON OA01038 = CA01001
		                                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                                            JOIN dbo.OB01 ON OA01999 = OB01002
		                                            JOIN dbo.OC01 ON OB01999 = OC01003
		                                            WHERE OA01997 = 0 AND OA01003 <> 3  
					                                        AND  (OA01015 = '' or OA01015 is null)  {0}
					                                        AND  (OA01017 = '' or OA01017 is null) 
                                                            AND  OA01016 = 0 AND OA01018 = 0    
	                                          UNION ALL 
		                                            SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                           OA01038 AS  CustomerNum,
				                                           OA01044 AS NewCustomerNum,
				                                           OA01002 AS OrderNum,
				                                           round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,
				                                           (OB01007*OB01008*OA01016) AS OA01020
		                                            FROM dbo.OA01  
		                                            JOIN dbo.CA01 ON OA01038 = CA01001
		                                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                                            JOIN dbo.OB01 ON OA01999 = OB01002
		                                            JOIN dbo.OC01 ON OB01999 = OC01003
		                                            WHERE OA01997 = 0 AND OA01003 <> 3  
				                                        AND   (OA01015 <> '' AND OA01015 IS NOT NULL)  {0}
	                                         UNION ALL 
	                                              SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                         OA01038 AS  CustomerNum,
				                                         OA01044 AS NewCustomerNum,
				                                         OA01002 AS OrderNum,
				                                         round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022,
				                                         OB01007*OB01008*OA01018 AS OA01020
	                                              FROM dbo.OA01 
	                                              JOIN dbo.CA01 ON OA01038 = CA01001
	                                              JOIN dbo.UA01 ON UA01004 = OA01017 	
	                                              JOIN dbo.OB01 ON OA01999 = OB01002
	                                              JOIN dbo.OC01 ON OB01999 = OC01003
	                                              WHERE OA01997 = 0 AND OA01003 <> 3  
		                                                   AND   (OA01017 <> '' AND OA01017 IS NOT NULL)  {0}
	                                         UNION ALL 
		                                          SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                         OA01038 AS  CustomerNum,
				                                         OA01044 AS NewCustomerNum,
				                                         OA01002 AS OrderNum,
				                                         round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,
				                                         OB01007*OB01008*(1-OA01016-OA01018) AS OA01020
		                                          FROM dbo.OA01 
		                                          JOIN dbo.CA01 ON OA01038 = CA01001
		                                          JOIN dbo.UA01 ON UA01001 = OA01013 	
		                                          JOIN dbo.OB01 ON OA01999 = OB01002
		                                          JOIN dbo.OC01 ON OB01999 = OC01003
		                                          WHERE OA01997 = 0 AND OA01003 <> 3  
			                                                 AND  (OA01016 <> 0 OR OA01018<>0)  {0}
                                        ) ABCD
                                    ) SalesYear ", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@" 
                                    (
                                        SELECT '' YEARS,COUNT(distinct CustomerNum) CUSTOMERCOUNT
                                            ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) OA01044
                                            ,COUNT(distinct OrderNum) ORDERCOUNT  ,SUM(OA01022) OA01022
                                            ,SUM(OA01020) OA01020
                                        FROM (
	                                         SELECT * 
	                                         FROM (
			                                        SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                           OA01038 AS CustomerNum,
			                                               OA01044 AS NewCustomerNum,
			                                               OA01002 AS OrderNum,
			                                               OA01022 ,OA01020,
			                                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
		                                            FROM dbo.OA01 
		                                            JOIN dbo.CA01 ON OA01038 = CA01001
		                                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                                            JOIN dbo.OB01 ON OA01999 = OB01002
		                                            JOIN dbo.OC01 ON OB01999 = OC01003
		                                            WHERE OA01997 = 0 AND OA01003 <> 3  
					                                        AND  (OA01015 = '' or OA01015 is null)  {0}
					                                        AND  (OA01017 = '' or OA01017 is null) 
                                                            AND  OA01016 = 0 AND OA01018 = 0    
	                                          ) A WHERE NUM =1
	                                          UNION ALL 
	                                          SELECT * 
	                                          FROM (
		                                            SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                           OA01038 AS  CustomerNum,
				                                           OA01044 AS NewCustomerNum,
				                                           OA01002 AS OrderNum,
				                                           (OA01022*OA01016) AS OA01022,
				                                           (OA01020*OA01016) AS OA01020,
					                                        ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
		                                            FROM dbo.OA01  
		                                            JOIN dbo.CA01 ON OA01038 = CA01001
		                                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                                            JOIN dbo.OB01 ON OA01999 = OB01002
		                                            JOIN dbo.OC01 ON OB01999 = OC01003
		                                            WHERE OA01997 = 0 AND OA01003 <> 3  
				                                        AND   (OA01015 <> '' AND OA01015 IS NOT NULL)  {0}
	                                         ) B WHERE NUM =1
	                                         UNION ALL 
	                                         SELECT * 
	                                         FROM (
	                                              SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                         OA01038 AS  CustomerNum,
				                                         OA01044 AS NewCustomerNum,
				                                         OA01002 AS OrderNum,
				                                         OA01022*OA01018 AS OA01022,
				                                         OA01020*OA01018 AS OA01020,
				                                         ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
	                                              FROM dbo.OA01 
	                                              JOIN dbo.CA01 ON OA01038 = CA01001
	                                              JOIN dbo.UA01 ON UA01004 = OA01017 	
	                                              JOIN dbo.OB01 ON OA01999 = OB01002
	                                              JOIN dbo.OC01 ON OB01999 = OC01003
	                                              WHERE OA01997 = 0 AND OA01003 <> 3  
		                                                   AND   (OA01017 <> '' AND OA01017 IS NOT NULL)  {0}
	                                         ) C WHERE NUM =1
	                                         UNION ALL 
	                                         SELECT * 
	                                         FROM (
		                                          SELECT DATEPART(YEAR,OA01009) AS YEARS , 
				                                         OA01038 AS  CustomerNum,
				                                         OA01044 AS NewCustomerNum,
				                                         OA01002 AS OrderNum,
				                                         OA01022*(1-OA01016-OA01018) AS OA01022,
				                                         OA01020*(1-OA01016-OA01018) AS OA01020,
				                                         ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) AS NUM
		                                          FROM dbo.OA01 
		                                          JOIN dbo.CA01 ON OA01038 = CA01001
		                                          JOIN dbo.UA01 ON UA01001 = OA01013 	
		                                          JOIN dbo.OB01 ON OA01999 = OB01002
		                                          JOIN dbo.OC01 ON OB01999 = OC01003
		                                          WHERE OA01997 = 0 AND OA01003 <> 3  
			                                                 AND  (OA01016 <> 0 OR OA01018<>0)  {0}
	                                         ) D WHERE NUM =1
                                        ) ABCD
                                    ) SalesYear ", strWhereAdd);
                }


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
        /// 未付款先发货
        /// </summary>
        /// <returns></returns>
        public DataTable SelectUnpaidFirstShipmentPage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = " * ,ROW_NUMBER() OVER(ORDER BY OA01009 DESC) AS RowNumber";
                string strTableName = string.Format(@" 
                                    (SELECT UA01004,UA01005,CA01003,CA01014,SUM(OA01020) OA01020, SUM(OP01016) OP01016,MAX(OA01009) OA01009,CB02002 
                                    FROM (SELECT UA01004 ,UA01005
                                    	 			 ,CA01003 
                                    	 			 ,CA01014 
                                    	 			 ,OA01020 
                                    	 			 ,OP01016
                                    	 			 ,DATEDIFF(DAY,OA01051,GETDATE()) OA01009
                                    	 			 ,CB02002
                                    	 			 ,ROW_NUMBER () OVER(PARTITION BY OA01038,OA01013,OA01002 ORDER BY OA01009) NUM
                                    	  FROM  OA01
                                    	  INNER JOIN CA01 ON CA01001 = OA01038 
                                    	  INNER JOIN UA01 ON UA01001 = OA01013
                                    	  INNER JOIN CB02 ON CB02001 = CA01018
                                          INNER JOIN GA03 ON GA03001 = CA01013
                                    	  INNER JOIN OP01 ON OA01999 = OP01003
                                    	  INNER JOIN OB01 on OB01002=OA01999 
                                    	  INNER JOIN OC01 on OC01003=OB01999
                                    	  WHERE OA01997 = 0 AND OA01003 <> 3 AND OC01010 =1 AND OP01016 > 0  AND OA01009 BETWEEN 
                                            DATEADD(MONTH,-12,GETDATE())AND GETDATE() {0} ) A 
                                    WHERE NUM=1
                                    GROUP BY CA01003,UA01004,CA01014,CB02002,UA01005
                                    ) UnpaidFirstShipment ", strWhereAdd);
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
        /// 按行业分类统计
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlyMDTPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY OA01022 DESC ) AS RowNumber  ";
                string strTableName;
                if (blInvoice)
                {
                    strTableName = string.Format(@"
                        (
                            SELECT CB03002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                            COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                            SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                            FROM (
		                            SELECT CB03002, OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002,CA01001,OA01044
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND  (OA01015 = '' or OA01015 is null)
			                            AND  (OA01017 = '' or OA01017 is null)
			                            AND  OA01016 = 0 AND OA01018 = 0
	                   
	                            UNION ALL 
	                 
		                            SELECT CB03002,OB01007*OB01008*OA01016 AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
			                            , OA01002,CA01001,OA01044
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01015
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3  {0}  
			                            AND (OA01015 <> '' and OA01015 is not null)
	                            UNION ALL 
		                            SELECT CB03002,OB01007*OB01008*OA01018 AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022
			                            , OA01002,CA01001,OA01044
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01017
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3    {0}
			                            AND (OA01017 <> '' and OA01017 is not null) 
	                            UNION ALL 
		                            SELECT CB03002,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022
			                            ,OA01002,CA01001,OA01044
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3     {0}
			                            AND (OA01016 <> 0 OR OA01018 <> 0) 
                            ) ABCD 
                            GROUP BY CB03002
                ) A", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@"
                        (
                            SELECT CB03002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                            COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                            SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                            FROM (
	                            SELECT CB03002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB03002,OA01020,OA01022,OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3     {0}
			                            AND  (OA01015 = '' or OA01015 is null)
			                            AND  (OA01017 = '' or OA01017 is null)
			                            AND  OA01016 = 0 AND OA01018 = 0
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL 
	                            SELECT CB03002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB03002,OA01020*OA01016 AS OA01020
			                            ,OA01022*OA01016 AS OA01022
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01015
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}  
			                            AND (OA01015 <> '' and OA01015 is not null)
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL 
	                            SELECT CB03002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB03002,OA01020*OA01018 AS OA01020
			                            ,OA01022*OA01018 AS OA01022
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01017
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3      {0}
			                            AND (OA01017 <> '' and OA01017 is not null) 
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL 
	                            SELECT CB03002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB03002,OA01020*(1-OA01016-OA01018) AS OA01020
			                            ,OA01022*(1-OA01016-OA01018) AS OA01022
			                            ,OA01002
			                            ,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB03 ON CB03001 = OA01040
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}
			                            AND (OA01016 <> 0 OR OA01018 <> 0) 
	                            ) A 
	                            WHERE A.NUM = 1 
                            ) ABCD 
                            GROUP BY CB03002
                ) A", strWhereAdd);
                }
                string strWhere = "";


                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
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
        /// 睡眠客户2
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectDormantCustomersTwoPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY UA01005 ASC ) AS RowNumber";

                string strTableName;

                if (blInvoice)
                {
                    //开发票统计
                    strTableName = string.Format(@"
                        (SELECT UA01005,SUM(OA01022)OA01022,SUM(OA01054) OA01054
							FROM(
								SELECT * 
									FROM(
										SELECT UA01005,SUM(OA01022) OA01022,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2) OA01022,OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01001 = OA01013 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND  (OA01015 = '' or OA01015 is null) and OA01054 = 1 {0}
												AND  (OA01017 = '' or OA01017 is null)  
                                                AND  OA01016 = 0 AND OA01018 = 0 
										)A 
										GROUP BY UA01005
									) A
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022) OA01022,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022,OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01015 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01015 <> '' and OA01015 is not null) and OA01054 = 1 {0}
										) B 
										GROUP BY UA01005	
									) B
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022) OA01022,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
									   FROM (
											SELECT  UA01005,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022,OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01017
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01017 <> '' and OA01017 is not null) and OA01054 = 1 {0}
										) C 
										GROUP BY UA01005
									) C
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022) OA01022,SUM(DISTINCT(CASE WHEN OA01054=1 THEN OA01054 ELSE 0 END )) OA01054
											FROM (
												SELECT  UA01005,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022,OA01054
												FROM OA01
												JOIN CA01 ON CA01001 = OA01038
												LEFT JOIN CB04 ON CB04001 = CA01020
												JOIN UA01 ON UA01001 = OA01013
												JOIN OB01 ON OA01999 = OB01002
												JOIN OC01 ON OB01999 = OC01003
												WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0) and OA01054 = 1 {0}
											) D
										GROUP BY UA01005	
									) D
								)TOTAL
							GROUP BY UA01005
                ) A", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@"
                        (SELECT UA01005,SUM(OA01022)OA01022,sum(OA01054) OA01054
							FROM(
								SELECT * 
									FROM(
										SELECT UA01005,SUM(OA01022) OA01022,SUM(OA01054) OA01054
									   FROM (
											SELECT  UA01005,OA01022,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM, OA01054
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01001 = OA01013 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND  (OA01015 = '' or OA01015 is null) and OA01054 = 1 {0}
												AND  (OA01017 = '' or OA01017 is null)  
                                                AND  OA01016 = 0 AND OA01018 = 0 
										)A WHERE NUM = 1 
										GROUP BY UA01005	
									) A
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022*OA01016) OA01022,SUM(OA01054) OA01054
									   FROM (
											SELECT  UA01005,OA01022,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM, OA01054,OA01016
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01015 
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01015 <> '' and OA01015 is not null) and OA01054 = 1 {0}
										) B WHERE NUM = 1 
										GROUP BY UA01005	
									) B
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022*OA01018) OA01022,SUM(OA01054) OA01054
									   FROM (
											SELECT  UA01005,OA01022,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM,OA01054,OA01018
											FROM OA01
											JOIN CA01 ON CA01001 = OA01038
											LEFT JOIN CB04 ON CB04001 = CA01020
											JOIN UA01 ON UA01004 = OA01017
											JOIN OB01 ON OA01999 = OB01002
											JOIN OC01 ON OB01999 = OC01003
											WHERE OA01997 = 0 AND (OA01017 <> '' and OA01017 is not null) and OA01054 = 1 {0}
										) C WHERE NUM = 1 
										GROUP BY UA01005
									) C
									UNION ALL
									SELECT * 
										FROM (
											SELECT UA01005,SUM(OA01022*(1-OA01016-OA01018)) OA01022,SUM(OA01054) OA01054
											FROM (
												SELECT  UA01005,OA01022,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 ASC ) NUM, OA01054,OA01016,OA01018
												FROM OA01
												JOIN CA01 ON CA01001 = OA01038
												LEFT JOIN CB04 ON CB04001 = CA01020
												JOIN UA01 ON UA01001 = OA01013
												JOIN OB01 ON OA01999 = OB01002
												JOIN OC01 ON OB01999 = OC01003
												WHERE OA01997 = 0 AND  (OA01016 <> 0 OR OA01018<>0) and OA01054 = 1 {0}
											) D WHERE NUM = 1 
										GROUP BY UA01005	
									) D
								)TOTAL
							GROUP BY UA01005
                ) A", strWhereAdd);
                }
                string strWhere = "";

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value=" ",DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 按客户代码统计
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlyCTCPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY OA01022 DESC ) AS RowNumber  ";
                string strTableName = string.Empty;

                if (blInvoice)
                {
                    //按开发票统计
                    strTableName = string.Format(@"
                        (
                            SELECT CB02002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                            COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                            SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                            FROM (
		                            SELECT CB02002, OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022,OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3     {0}
			                            AND  (OA01015 = '' or OA01015 is null)
			                            AND  (OA01017 = '' or OA01017 is null)
			                            AND  OA01016 = 0 AND OA01018 = 0
	                          
	                            UNION ALL 
		                            SELECT CB02002,OB01007*OB01008*OA01016 AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01015
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}  
			                            AND (OA01015 <> '' and OA01015 is not null)
	                            UNION ALL 
		                            SELECT CB02002,OB01007*OB01008*OA01018 AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01017
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3      {0}
			                            AND (OA01017 <> '' and OA01017 is not null) 
	                            UNION ALL 
		                            SELECT CB02002,OB01007*OB01008*(1-OA01016-OA01018) AS OA01020
			                            ,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022
			                            ,OA01002
			                            ,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}
			                            AND (OA01016 <> 0 OR OA01018 <> 0) 
                            ) ABCD 
                            GROUP BY CB02002
                ) A", strWhereAdd);
                }

                else
                {
                    strTableName = string.Format(@"
                        (
                            SELECT CB02002,SUM(OA01020) OA01020,SUM(OA01022) OA01022,
	                            COUNT(DISTINCT OA01002) OA01002,COUNT(DISTINCT CA01001) CA01001,
	                            SUM(CASE WHEN OA01044=1 THEN OA01044 ELSE 0 END ) as OA01044  
                            FROM (
	                            SELECT CB02002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB02002,OA01020,OA01022,OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3     {0}
			                            AND  (OA01015 = '' or OA01015 is null)
			                            AND  (OA01017 = '' or OA01017 is null)
			                            AND  OA01016 = 0 AND OA01018 = 0
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL 
	                            SELECT CB02002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB02002,OA01020*OA01016 AS OA01020
			                            ,OA01022*OA01016 AS OA01022
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01015
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}  
			                            AND (OA01015 <> '' and OA01015 is not null)
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL 
	                            SELECT CB02002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB02002,OA01020*OA01018 AS OA01020
			                            ,OA01022*OA01018 AS OA01022
			                            , OA01002,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01004 = OA01017
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3      {0}
			                            AND (OA01017 <> '' and OA01017 is not null) 
	                            ) A 
	                            WHERE A.NUM = 1 
	                            UNION ALL 
	                            SELECT CB02002,OA01020,OA01022,OA01002,CA01001,OA01044
	                            FROM (
		                            SELECT CB02002,OA01020*(1-OA01016-OA01018) AS OA01020
			                            ,OA01022*(1-OA01016-OA01018) AS OA01022
			                            ,OA01002
			                            ,CA01001,OA01044
			                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009 DESC) NUM
		                            FROM OA01 
		                            INNER JOIN OB01 ON OA01999 = OB01002
		                            INNER JOIN OC01 ON OB01999 = OC01003
		                            INNER JOIN CA01 ON CA01001 = OA01038                                       
		                            INNER JOIN UA01 ON UA01001 = OA01013
		                            LEFT JOIN CB02 ON CB02001 = CA01018
		                            WHERE OA01997 = 0 AND OA01003 <> 3       {0}
			                            AND (OA01016 <> 0 OR OA01018 <> 0) 
	                            ) A 
	                            WHERE A.NUM = 1 
                            ) ABCD 
                            GROUP BY CB02002
                ) A", strWhereAdd);
                }


                string strWhere = "";

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
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
        /// 按所属省份统计
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlyProvincePage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY Amout DESC ) AS RowNumber  ";
                string strTableName = string.Empty;
               
                    strTableName = string.Format(@"
                        (
                            SELECT GA03001,GA03002 ProvinceName,COUNT(distinct CustomerNum) CustomerNum
                                    ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) NewCustomerNum
                                    ,count(OrderNum) OrderNum  ,SUM(Amout) Amout
                            FROM (
	                            SELECT * FROM (
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
			                               OA01044 AS NewCustomerNum,
			                               OA01002 AS OrderNum,
			                               OA01022 AS Amout,
			                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001 = CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3  AND  OA01016 = 0 AND OA01018 = 0  {0}
	                            ) A WHERE NUM =1
	                            UNION ALL
	                            SELECT * FROM (
		                            SELECT OA01055 GA03001,OA01057 GA03002 , OA01038 AS  CustomerNum,
				                               OA01044 AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               (OA01022*OA01016) AS Amout,
				                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01  
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
		                            WHERE OA01997 = 0 AND OA01003 <> 3 and OA01055 <> '' and OA01016 <> 0 {0}
	                            ) B WHERE NUM =1
	                            UNION ALL
	                            SELECT * 
	                            FROM (
	                            SELECT OA01056 GA03001,OA01058 GA03002, OA01038 AS  CustomerNum,
				                               OA01044 AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               OA01022*OA01018 AS Amout,
				                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
	                                FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
		                            WHERE OA01997 = 0 AND OA01003 <> 3 and OA01056 <> '' and OA01018 <> 0 {0}
		                            AND   (OA01017 <> '' AND OA01017 IS NOT NULL) 
	                            ) C WHERE NUM =1
	                            UNION ALL
	                            SELECT * FROM 
	                            (
		                            SELECT  P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
					                               OA01044 AS NewCustomerNum,
					                               OA01002 AS OrderNum,
					                               OA01022*(1-OA01016-OA01018) AS Amout,
					                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
		                            WHERE OA01997 = 0 AND OA01003 <> 3 AND  (OA01016 <> 0 OR OA01018<>0) {0}
	                            ) D WHERE NUM =1
                            ) ABCD
                            GROUP BY GA03001,GA03002
                ) A", strWhereAdd);
                string strWhere = "";
                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
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
        /// 按所属省份统计
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlyProvincePage1(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY Amout DESC ) AS RowNumber  ";
                string strTableName = string.Empty;
                if (blInvoice)
                {
                    strTableName = string.Format(@"
                        (
                            SELECT GA03001,GA03002 ProvinceName,COUNT(distinct CustomerNum) CustomerNum
                                    ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) NewCustomerNum
                                    ,count(distinct OrderNum) OrderNum  ,SUM(Amout) Amout
                            FROM (
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
			                               OA01044 AS NewCustomerNum,
			                               OA01002 AS OrderNum,
			                               round((OB01009/OA01021/1.17),2) AS Amout
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3    {0}
			                            AND  (OA01015 = '' or OA01015 is null) 
			                            AND  (OA01017 = '' or OA01017 is null) 
                                        AND  OA01016 = 0 AND OA01018 = 0 
	                            UNION ALL
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
				                               OA01044 AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               round((OB01009/OA01021/1.17),2)*OA01016 AS Amout
		                            FROM dbo.OA01  
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND   (OA01015 <> '' AND OA01015 IS NOT NULL)
	                            UNION ALL
	                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
				                               OA01044 AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               round((OB01009/OA01021/1.17),2)*OA01018 AS Amout
	                            FROM dbo.OA01 
	                            JOIN dbo.CA01 ON OA01038 = CA01001
                                JOIN dbo.GA03 ON GA03001=CA01013
                                JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
	                            JOIN dbo.UA01 ON UA01004 = OA01017 	
	                            JOIN dbo.OB01 ON OA01999 = OB01002
	                            JOIN dbo.OC01 ON OB01999 = OC01003
	                            WHERE OA01997 = 0 AND OA01003 <> 3    {0}
		                            AND   (OA01017 <> '' AND OA01017 IS NOT NULL) 
	                            UNION ALL
		                            SELECT  P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
					                               OA01044 AS NewCustomerNum,
					                               OA01002 AS OrderNum,
					                               round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS Amout
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND  (OA01016 <> 0 OR OA01018<>0) 
                            ) ABCD
                            GROUP BY GA03001,GA03002
                ) A", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@"
                        (
                            SELECT GA03001,GA03002 ProvinceName,COUNT(distinct CustomerNum) CustomerNum
                                    ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) NewCustomerNum
                                    ,count(distinct OrderNum) OrderNum  ,SUM(Amout) Amout
                            FROM (
	                            SELECT * FROM (
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
			                               OA01044 AS NewCustomerNum,
			                               OA01002 AS OrderNum,
			                               OA01022 AS Amout,
			                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3    {0}
			                            AND  (OA01015 = '' or OA01015 is null) 
			                            AND  (OA01017 = '' or OA01017 is null) 
                                        AND  OA01016 = 0 AND OA01018 = 0 
	                            ) A WHERE NUM =1
	                            UNION ALL
	                            SELECT * FROM (
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
				                               OA01044 AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               (OA01022*OA01016) AS Amout,
				                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01  
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND   (OA01015 <> '' AND OA01015 IS NOT NULL)
	                            ) B WHERE NUM =1
	                            UNION ALL
	                            SELECT * 
	                            FROM (
	                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
				                               OA01044 AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               OA01022*OA01018 AS Amout,
				                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
	                            FROM dbo.OA01 
	                            JOIN dbo.CA01 ON OA01038 = CA01001
                                JOIN dbo.GA03 ON GA03001=CA01013
                                JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
	                            JOIN dbo.UA01 ON UA01004 = OA01017 	
	                            JOIN dbo.OB01 ON OA01999 = OB01002
	                            JOIN dbo.OC01 ON OB01999 = OC01003
	                            WHERE OA01997 = 0 AND OA01003 <> 3    {0}
		                            AND   (OA01017 <> '' AND OA01017 IS NOT NULL) 
	                            ) C WHERE NUM =1
	                            UNION ALL
	                            SELECT * FROM 
	                            (
		                            SELECT  P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
					                               OA01044 AS NewCustomerNum,
					                               OA01002 AS OrderNum,
					                               OA01022*(1-OA01016-OA01018) AS Amout,
					                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND  (OA01016 <> 0 OR OA01018<>0) 
	                            ) D WHERE NUM =1
                            ) ABCD
                            GROUP BY GA03001,GA03002
                ) A", strWhereAdd);
                }


                string strWhere = "";

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
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
        /// 按所属城市统计
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectMonthlyCityPage(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY Amout DESC ) AS RowNumber  ";
                string strTableName = string.Empty;
                if (blInvoice)
                {
                    strTableName = string.Format(@"
                        (
                            SELECT GA03001,GA03002 ProvinceName,CityName,count(distinct CustomerNum) CustomerNum
                                    ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) NewCustomerNum
                                    ,count(distinct OrderNum) OrderNum ,SUM(Amout) Amout
                            FROM (
		                            SELECT P.GA03001,P.GA03002, OA01038 AS  CustomerNum,
										   GA03.GA03002 CityName,
			                               OA01044 AS NewCustomerNum,
			                               OA01002 AS OrderNum,
			                               round((OB01009/OA01021/1.17),2) AS Amout
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3  
			                            AND  (OA01015 = '' or OA01015 is null)  
			                            AND  (OA01017 = '' or OA01017 is null)  {0}
                                        AND  OA01016 = 0 AND OA01018 = 0    
	                            UNION ALL 
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
											  GA03.GA03002 CityName,
				                                (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               round((OB01009/OA01021/1.17),2)*OA01016 AS Amout
		                            FROM dbo.OA01  
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND   (OA01015 <> '' AND OA01015 IS NOT NULL)
	                            UNION ALL 
	                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
											 GA03.GA03002 CityName,
				                              (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               round((OB01009/OA01021/1.17),2)*OA01018 AS Amout
	                            FROM dbo.OA01 
	                            JOIN dbo.CA01 ON OA01038 = CA01001
                                JOIN dbo.GA03 ON GA03001=CA01013
                                JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
	                            JOIN dbo.UA01 ON UA01004 = OA01017 	
	                            JOIN dbo.OB01 ON OA01999 = OB01002
	                            JOIN dbo.OC01 ON OB01999 = OC01003
	                            WHERE OA01997 = 0 AND OA01003 <> 3  
		                            AND   (OA01017 <> '' AND OA01017 IS NOT NULL) {0}
	                            UNION ALL 
		                            SELECT  P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
													GA03.GA03002 CityName,
					                               (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
					                               OA01002 AS OrderNum,
					                               round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS Amout
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
		                            WHERE OA01997 = 0 AND OA01003 <> 3  
			                            AND  (OA01016 <> 0 OR OA01018<>0) {0}
                            ) ABCD
                            GROUP BY GA03001,GA03002,CityName
                ) A", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@"
                        (
                            SELECT GA03001,GA03002 ProvinceName,CityName,count(distinct CustomerNum) CustomerNum
                                    ,SUM(CASE  WHEN NewCustomerNum = 1 THEN 1 ELSE 0 END ) NewCustomerNum
                                    ,count(distinct OrderNum) OrderNum ,SUM(Amout) Amout
                            FROM (
	                            SELECT * FROM (
		                            SELECT P.GA03001,P.GA03002, OA01038 AS  CustomerNum,
										   GA03.GA03002 CityName,
			                               OA01044 AS NewCustomerNum,
			                               OA01002 AS OrderNum,
			                               OA01022 AS Amout,
			                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3  
			                            AND  (OA01015 = '' or OA01015 is null)  
			                            AND  (OA01017 = '' or OA01017 is null)  {0}
                                        AND  OA01016 = 0 AND OA01018 = 0    
	                            ) A WHERE NUM =1
	                            UNION ALL 
	                            SELECT * FROM (
		                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
											  GA03.GA03002 CityName,
				                                (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               (OA01022*OA01016) AS Amout,
				                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01  
		                            JOIN dbo.CA01 ON OA01038 = CA01001
		                            JOIN dbo.UA01 ON UA01004 = OA01015 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            WHERE OA01997 = 0 AND OA01003 <> 3   {0}
			                            AND   (OA01015 <> '' AND OA01015 IS NOT NULL)
	                            ) B WHERE NUM =1
	                            UNION ALL 
	                            SELECT * 
	                            FROM (
	                            SELECT P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
											 GA03.GA03002 CityName,
				                              (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
				                               OA01002 AS OrderNum,
				                               OA01022*OA01018 AS Amout,
				                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
	                            FROM dbo.OA01 
	                            JOIN dbo.CA01 ON OA01038 = CA01001
                                JOIN dbo.GA03 ON GA03001=CA01013
                                JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
	                            JOIN dbo.UA01 ON UA01004 = OA01017 	
	                            JOIN dbo.OB01 ON OA01999 = OB01002
	                            JOIN dbo.OC01 ON OB01999 = OC01003
	                            WHERE OA01997 = 0 AND OA01003 <> 3  
		                            AND   (OA01017 <> '' AND OA01017 IS NOT NULL) {0}
	                            ) C WHERE NUM =1
	                            UNION ALL 
	                            SELECT * FROM 
	                            (
		                            SELECT  P.GA03001,P.GA03002 , OA01038 AS  CustomerNum,
													GA03.GA03002 CityName,
					                               (case when OA01044=1 then OA01044 else 0 end )  AS NewCustomerNum,
					                               OA01002 AS OrderNum,
					                               OA01022*(1-OA01016-OA01018) AS Amout,
					                               ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01002 ASC ) AS NUM
		                            FROM dbo.OA01 
		                            JOIN dbo.CA01 ON OA01038 = CA01001
                                    JOIN dbo.GA03 ON GA03001=CA01013
                                    JOIN dbo.GA03 P ON GA03.GA03003 = P.GA03001
		                            JOIN dbo.UA01 ON UA01001 = OA01013 	
		                            JOIN dbo.OB01 ON OA01999 = OB01002
		                            JOIN dbo.OC01 ON OB01999 = OC01003
		                            WHERE OA01997 = 0 AND OA01003 <> 3  
			                            AND  (OA01016 <> 0 OR OA01018<>0) {0}
	                            ) D WHERE NUM =1
                            ) ABCD
                            GROUP BY GA03001,GA03002,CityName
                ) A", strWhereAdd);
                }

                string strWhere = "";

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=PageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=PageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=strWhere,DbType=DbType.String},
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
        /// 重点客户统计
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectKeyCustomer(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = " * ,ROW_NUMBER() over(order by YEARS desc) AS RowNumber";

                #region 没有分享人 已注释掉
                //                string strTableName = string.Format(@" 
                //                    (SELECT tba.*,ISNULL(tba2.SUMUD,0) OLDSUMUD ,ISNULL(tba2.ORDERCOUNT,0) OLDORDERCOUNT,ISNULL(tba2.SUMRMB,0)OLDSUMRMB
                //                    FROM(
                //	                    SELECT CA01001,CA01003,UA01001,UA01004,UA01005,CB04002,CB02002,
                //								SUM(OA01020) SUMRMB,SUM(OA01022) SUMUD
                //                                ,SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
                //					        SELECT CA01001,CA01003,UA01001,UA01004,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,
                //					        DATEPART(YEAR,OA01009) YEARS ,
                //					        ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
                //					        FROM OA01
                //					        INNER JOIN OB01 on OB01002 = OA01999 
                //					        INNER JOIN OC01 on OC01003 = OB01999
                //					        INNER JOIN UA01 ON UA01001 = OA01013
                //					        INNER JOIN CA01 ON CA01001 = OA01038
                //					        INNER JOIN CB04 ON CB04001 = CA01020
                //					        INNER JOIN CB02 ON CB02001 = CA01018
                //					        WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1
                //					        AND DATEPART(YEAR,OA01009) =DATEPART(YEAR,GETDATE())
                //                            {0}
                //			            ) TopCustomerTwo 
                //                        WHERE NUM =1 
                //                        GROUP BY YEARS,CA01001,CA01003,UA01001,UA01004,UA01005,CB04002,CB02002
                //                    ) tba 
                //                    LEFT JOIN 
                //                    (SELECT CA01001,CA01003,UA01001,UA01004,CB04002,CB02002,SUM(OA01020) SUMRMB,SUM(OA01022) SUMUD,
                //                            SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
                //					    SELECT CA01001,CA01003,UA01001,UA01004,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,
                //					        DATEPART(YEAR,OA01009) YEARS ,
                //					        ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
                //					        FROM OA01
                //					        INNER JOIN OB01 on OB01002 = OA01999 
                //					        INNER JOIN OC01 on OC01003 = OB01999
                //					        INNER JOIN UA01 ON UA01001 = OA01013
                //					        INNER JOIN CA01 ON CA01001 = OA01038
                //					        INNER JOIN CB04 ON CB04001 = CA01020
                //					        INNER JOIN CB02 ON CB02001 = CA01018
                //					        WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1
                //					        AND DATEPART(YEAR,OA01009) = DATEPART(YEAR,DATEADD(YEAR,-1,GETDATE()))
                //                            {0}
                //			            ) TopCustomerTwo 
                //                    WHERE NUM =1 
                //                    GROUP BY YEARS,CA01001,CA01003,UA01001,UA01004,CB04002,CB02002 ) 
                //                tba2  on tba.CA01001=tba2.CA01001 AND tba.UA01001=tba2.UA01001
                //                ) a ", strWhereAdd);

                #endregion

                string strTableName = string.Format(@" 
                                (SELECT YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002
	                                ,SUM(SUMRMB) SUMRMB,SUM(SUMUD) SUMUD
	                                ,SUM(DISTINCT ORDERCOUNT) ORDERCOUNT
	                                ,SUM(DISTINCT OLDSUMUD) OLDSUMUD
	                                ,SUM(DISTINCT OLDORDERCOUNT) OLDORDERCOUNT
	                                ,SUM(DISTINCT OLDSUMRMB) OLDSUMRMB
                                FROM 
                                (
	                                SELECT tba.*,ISNULL(tba2.SUMUD,0) OLDSUMUD ,ISNULL(tba2.ORDERCOUNT,0) OLDORDERCOUNT,ISNULL(tba2.SUMRMB,0)OLDSUMRMB
						                                FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
									                                SUM(OA01020) SUMRMB,SUM(OA01022) SUMUD
									                                ,SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
								                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,
								                                DATEPART(YEAR,OA01009) YEARS ,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01001 = OA01013
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1
									                                 AND  (OA01015 = '' or OA01015 is null)
									                                 AND  (OA01017 = '' or OA01017 is null) 
                                                                     AND  OA01016 = 0 AND OA01018 = 0  {0}
								                                AND DATEPART(YEAR,OA01009) =DATEPART(YEAR,GETDATE())
							                                ) TopCustomerTwo 
							                                WHERE NUM =1 
							                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002
						                                ) tba 
						                                LEFT JOIN 
						                                (SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,SUM(OA01020) SUMRMB,SUM(OA01022) SUMUD,
								                                SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,
								                                DATEPART(YEAR,OA01009) YEARS ,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01001 = OA01013
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1
									                                 AND  (OA01015 = '' or OA01015 is null)
									                                 AND  (OA01017 = '' or OA01017 is null) 
                                                                     AND  OA01016 = 0 AND OA01018 = 0  {0}
								                                AND DATEPART(YEAR,OA01009) = DATEPART(YEAR,DATEADD(YEAR,-1,GETDATE()))
							                                ) TopCustomerTwo 
						                                WHERE NUM =1 
						                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002 ) 
					                                tba2  on tba.CA01001=tba2.CA01001 AND tba.UA01001=tba2.UA01001
	                                UNION ALL 
	                                SELECT tba.*,ISNULL(tba2.SUMUD,0) OLDSUMUD ,ISNULL(tba2.ORDERCOUNT,0) OLDORDERCOUNT,ISNULL(tba2.SUMRMB,0)OLDSUMRMB
						                                FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
									                                SUM(OA01020*OA01016) SUMRMB,SUM(OA01022*OA01016) SUMUD
									                                ,SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
								                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
								                                DATEPART(YEAR,OA01009) YEARS ,OA01020,OA01022,OA01002, OA01003,OA01016,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01004 = OA01015
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1
									                                AND  (OA01015 <> '' and OA01015 is not null)     {0}
									                                AND DATEPART(YEAR,OA01009) =DATEPART(YEAR,GETDATE())
							                                ) TopCustomerTwo 
							                                WHERE NUM =1 
							                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002
						                                ) tba 
						                                LEFT JOIN 
						                                (SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
								                                SUM(OA01022*OA01016) SUMUD,SUM(OA01020*OA01016) SUMRMB,
								                                SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,OA01016,
								                                DATEPART(YEAR,OA01009) YEARS ,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01004 = OA01015
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1
									                                AND  (OA01015 <> '' and OA01015 is not null)    {0} 
									                                AND DATEPART(YEAR,OA01009) = DATEPART(YEAR,DATEADD(YEAR,-1,GETDATE()))
							                                ) TopCustomerTwo 
						                                WHERE NUM =1 
						                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002 ) 
					                                tba2  on tba.CA01001=tba2.CA01001 AND tba.UA01001=tba2.UA01001
	                                 UNION ALL                
	                                 SELECT tba.*,ISNULL(tba2.SUMUD,0) OLDSUMUD ,ISNULL(tba2.ORDERCOUNT,0) OLDORDERCOUNT,ISNULL(tba2.SUMRMB,0)OLDSUMRMB
						                                FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
									                                SUM(OA01020*OA01018) SUMRMB,SUM(OA01022*OA01018) SUMUD
									                                ,SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
								                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01018
									                                ,OA01020,OA01022,OA01002, OA01003,DATEPART(YEAR,OA01009) YEARS 
									                                ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01004 = OA01017
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1  {0}
									                                AND DATEPART(YEAR,OA01009) =DATEPART(YEAR,GETDATE())
									                                AND (OA01017 <> '' and OA01017 is not null) 
							                                ) TopCustomerTwo 
							                                WHERE NUM =1 
							                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002
						                                ) tba 
						                                LEFT JOIN 
						                                (SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,SUM(OA01020*OA01018) SUMRMB,
								                                SUM(OA01022*OA01018) SUMUD,
								                                SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,OA01018,
								                                DATEPART(YEAR,OA01009) YEARS ,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01004 = OA01017
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1  {0}
									                                AND DATEPART(YEAR,OA01009) = DATEPART(YEAR,DATEADD(YEAR,-1,GETDATE()))
									                                AND (OA01017 <> '' and OA01017 is not null) 
							                                ) TopCustomerTwo 
						                                WHERE NUM =1 
						                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002 ) 
					                                tba2  on tba.CA01001=tba2.CA01001 AND tba.UA01001=tba2.UA01001
	                                UNION ALL                 
	                                SELECT tba.*,ISNULL(tba2.SUMUD,0) OLDSUMUD ,ISNULL(tba2.ORDERCOUNT,0) OLDORDERCOUNT,ISNULL(tba2.SUMRMB,0)OLDSUMRMB
						                                FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,
									                                SUM(OA01020*(1-OA01016-OA01018)) SUMRMB,SUM(OA01022*(1-OA01016-OA01018)) SUMUD
									                                ,SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
								                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,
								                                DATEPART(YEAR,OA01009) YEARS ,OA01016,OA01018,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01001 = OA01013
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1  {0}
								                                AND DATEPART(YEAR,OA01009) =DATEPART(YEAR,GETDATE())
								                                AND  (OA01016 <> 0 OR OA01018<>0)
							                                ) TopCustomerTwo 
							                                WHERE NUM =1 
							                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002
						                                ) tba 
						                                LEFT JOIN 
						                                (SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,SUM(OA01020*(1-OA01016-OA01018)) SUMRMB,
								                                SUM(OA01020*(1-OA01016-OA01018)) SUMUD,
								                                SUM(CASE OA01003 WHEN 1 THEN 1 ELSE 0 END) ORDERCOUNT,YEARS FROM(
							                                SELECT CA01001,CA01003,UA01001,UA01005,CB04002,CB02002,OA01020,OA01022,OA01002, OA01003,
								                                DATEPART(YEAR,OA01009) YEARS ,OA01018,OA01016,
								                                ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01009) NUM
								                                FROM OA01
								                                INNER JOIN OB01 on OB01002 = OA01999 
								                                INNER JOIN OC01 on OC01003 = OB01999
								                                INNER JOIN UA01 ON UA01001 = OA01013
								                                INNER JOIN CA01 ON CA01001 = OA01038
								                                LEFT JOIN CB04 ON CB04001 = CA01020
								                                LEFT JOIN CB02 ON CB02001 = CA01018
								                                WHERE OA01003<>3 AND OA01997=0 AND CA01047  =1  {0}
								                                AND DATEPART(YEAR,OA01009) = DATEPART(YEAR,DATEADD(YEAR,-1,GETDATE()))
								                                AND  (OA01016 <> 0 OR OA01018<>0)
							                                ) TopCustomerTwo 
						                                WHERE NUM =1 
						                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002 ) 
					                                tba2  on tba.CA01001=tba2.CA01001 AND tba.UA01001=tba2.UA01001            
                                 ) KeyCustomer
                                GROUP BY YEARS,CA01001,CA01003,UA01001,UA01005,CB04002,CB02002
                            ) a ", strWhereAdd);
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
        /// 按客户名称统计/By Customer Name
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhereAdd"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable SelectByCustomerName(int PageSize, int PageIndex, string strWhereAdd, bool blInvoice, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @" *,ROW_NUMBER() OVER(ORDER BY CA01003 ASC ) AS RowNumber  ";
                string strTableName = string.Empty;
                if (blInvoice)
                {
                    strTableName = string.Format(@"
                        (
                            SELECT CA01001,CA01003,SUM(OrderMoney) OrderMoney,
	                               COUNT(DISTINCT OrderCount) OrderCount,MAX(InvoicedCount) InvoicedCount
                            FROM (SELECT CA01001 , CA01003 ,
                                         SUM(round((OB01009/OA01021/1.17),2)) OrderMoney,
                                          OA01002 OrderCount,
                                         SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                  FROM ( SELECT CA01001,CA01003,OA01002,OC01014,OA01022,OB01009,OA01021 
                                         FROM	OA01
                                         JOIN dbo.CA01 ON OA01038 = CA01001
                                         JOIN dbo.UA01 ON UA01001 = OA01013
                                         JOIN dbo.OB01 ON OB01002 = OA01999
                                         JOIN dbo.OC01 ON OC01003 = OB01999
                                         WHERE OA01997 = 0 AND OA01003 = 1
			                                   AND  (OA01015 = '' or OA01015 is null)
                                               AND  (OA01017 = '' or OA01017 is null) 
                                               AND  OA01016 = 0 AND OA01018 = 0  {0}
                                       ) A
                                  GROUP BY CA01001 , CA01003 ,OA01002
                                  UNION ALL 
                                  SELECT CA01001 , CA01003 ,
                                         SUM(round((OB01009/OA01021/1.17),2)*OA01016) OrderMoney,
                                         OA01002 OrderCount,
                                         SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                  FROM ( SELECT CA01001,CA01003,OA01002,OC01014,OA01022,OB01009,OA01021,OA01016
                                         FROM	OA01
                                         JOIN dbo.CA01 ON OA01038 = CA01001
                                         JOIN dbo.UA01 ON UA01004 = OA01015
                                         JOIN dbo.OB01 ON OB01002 = OA01999
                                         JOIN dbo.OC01 ON OC01003 = OB01999
                                         WHERE OA01997 = 0 AND OA01003 = 1 
                                         AND (OA01015 <> '' and OA01015 is not null)  {0}
                                        ) B
                                   GROUP BY CA01001 , CA01003  ,OA01002   
	                               UNION ALL
                                   SELECT CA01001 , CA01003 ,
                                          SUM(round((OB01009/OA01021/1.17),2)*OA01018) OrderMoney,
                                          OA01002 OrderCount,
                                          SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                   FROM ( SELECT CA01001,CA01003,OA01002,OC01014,OA01022,OB01009,OA01021,OA01018
                                          FROM	OA01
                                          JOIN dbo.CA01 ON OA01038 = CA01001
                                          JOIN dbo.UA01 ON UA01004 = OA01017
                                          JOIN dbo.OB01 ON OB01002 = OA01999
                                          JOIN dbo.OC01 ON OC01003 = OB01999 
                                          WHERE OA01997 = 0 AND OA01003 = 1
                                            AND (OA01017 <> '' and OA01017 is not null)   {0}
                                         ) C
                                   GROUP BY CA01001 , CA01003 ,OA01002   
                                   UNION ALL                        
                                   SELECT CA01001 , CA01003 ,
                                          SUM(round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018)) OrderMoney,
                                          OA01002 OrderCount,
                                          SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                   FROM ( SELECT CA01001,CA01003,OA01002,OC01014,OA01022,OB01009,OA01021,OA01016,OA01018
                                          FROM      OA01
                                          JOIN dbo.CA01 ON OA01038 = CA01001
                                          JOIN dbo.UA01 ON UA01001 = OA01013
                                          JOIN dbo.OB01 ON OB01002 = OA01999
                                          JOIN dbo.OC01 ON OC01003 = OB01999
                                          WHERE OA01997 = 0 AND OA01003 = 1  {0}
                                            AND  (OA01016 <> 0 OR OA01018<>0)
                                         ) D
                                   GROUP BY CA01001 , CA01003 ,OA01002 
                             )A 
                             GROUP BY CA01001,CA01003 
                        ) A  ", strWhereAdd);
                }
                else
                {
                    strTableName = string.Format(@"
                        (
                            SELECT CA01001,CA01003,SUM(OrderMoney) OrderMoney,
	                               COUNT(DISTINCT OrderCount) OrderCount,MAX(InvoicedCount) InvoicedCount
                            FROM (SELECT CA01001 , CA01003 ,
                                         SUM( CASE WHEN vid = 1 THEN OA01022 ELSE 0 END) OrderMoney,
                                          OA01002 OrderCount,
                                         SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                  FROM ( SELECT * ,row_number() over (partition by OA01001 order by OA01001 desc) as vid
                                         FROM	OA01
                                         JOIN dbo.CA01 ON OA01038 = CA01001
                                         JOIN dbo.UA01 ON UA01001 = OA01013
                                         JOIN dbo.OB01 ON OB01002 = OA01999
                                         JOIN dbo.OC01 ON OC01003 = OB01999
                                         WHERE OA01997 = 0 AND OA01003 = 1
			                                   AND  (OA01015 = '' or OA01015 is null)
                                               AND  (OA01017 = '' or OA01017 is null) 
                                               AND  OA01016 = 0 AND OA01018 = 0  {0}
                                       ) A
                                  GROUP BY CA01001 , CA01003 ,OA01002
                                  UNION ALL 
                                  SELECT CA01001 , CA01003 ,
                                         SUM( CASE WHEN vid = 1 THEN OA01022*isnull(OA01016,0) ELSE 0 END) OrderMoney,
                                         OA01002 OrderCount,
                                         SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                  FROM ( SELECT * ,row_number() over (partition by OA01001 order by OA01001 desc) as vid
                                         FROM	OA01
                                         JOIN dbo.CA01 ON OA01038 = CA01001
                                         JOIN dbo.UA01 ON UA01004 = OA01015
                                         JOIN dbo.OB01 ON OB01002 = OA01999
                                         JOIN dbo.OC01 ON OC01003 = OB01999
                                         WHERE OA01997 = 0 AND OA01003 = 1 
                                         AND (OA01015 <> '' and OA01015 is not null)  {0}
                                        ) B
                                   GROUP BY CA01001 , CA01003  ,OA01002   
	                               UNION ALL
                                   SELECT CA01001 , CA01003 ,
                                          SUM( CASE WHEN vid = 1 THEN OA01022*isnull(OA01018,0) ELSE 0 END) OrderMoney,
                                          OA01002 OrderCount,
                                          SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                   FROM ( SELECT * ,row_number() over (partition by OA01001 order by OA01001 desc) as vid
                                          FROM	OA01
                                          JOIN dbo.CA01 ON OA01038 = CA01001
                                          JOIN dbo.UA01 ON UA01004 = OA01017
                                          JOIN dbo.OB01 ON OB01002 = OA01999
                                          JOIN dbo.OC01 ON OC01003 = OB01999 
                                          WHERE OA01997 = 0 AND OA01003 = 1
                                            AND (OA01017 <> '' and OA01017 is not null)   {0}
                                         ) C
                                   GROUP BY CA01001 , CA01003 ,OA01002   
                                   UNION ALL                        
                                   SELECT CA01001 , CA01003 ,
                                          SUM( CASE WHEN vid = 1 THEN OA01022*(1-isnull(OA01016,0)-isnull(OA01018,0)) ELSE 0 END) OrderMoney,
                                          OA01002 OrderCount,
                                          SUM(CASE WHEN OC01014 = 1 THEN 1 ELSE 0 END) InvoicedCount
                                   FROM ( SELECT * ,row_number() over (partition by OA01001 order by OA01001 desc) as vid
                                          FROM      OA01
                                          JOIN dbo.CA01 ON OA01038 = CA01001
                                          JOIN dbo.UA01 ON UA01001 = OA01013
                                          JOIN dbo.OB01 ON OB01002 = OA01999
                                          JOIN dbo.OC01 ON OC01003 = OB01999
                                          WHERE OA01997 = 0 AND OA01003 = 1  {0}
                                            AND  (OA01016 <> 0 OR OA01018<>0)
                                         ) D
                                   GROUP BY CA01001 , CA01003 ,OA01002 
                             )A 
                             GROUP BY CA01001,CA01003 
                        ) A  ", strWhereAdd);
                }




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

        #endregion

        #region 根据订单号查询开票金额

        public DataTable GetOA01020andOA01022ByOA01002(string oa01002)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"
                    select SUM(OA01020) OA01020,SUM(OA01022) OA01022
                    from (
	                    SELECT OB01007*OB01008 OA01020,round((OB01009/OA01021/1.17),2) OA01022
	                    FROM OA01 
	                    INNER JOIN OB01 ON OA01999 = OB01002
	                    INNER JOIN OC01 ON OB01999 = OC01003
	                    INNER JOIN CA01 ON CA01001 = OA01038                                       
	                    INNER JOIN UA01 ON UA01001 = OA01013
	                    LEFT JOIN CB03 ON CB03001 = OA01040
	                    WHERE OA01997 = 0 AND OA01003 <> 3 AND  (OA01015 = '' or OA01015 is null)
		                    AND  (OA01017 = '' or OA01017 is null) AND  OA01016 = 0 AND OA01018 = 0 and OA01002 = '{0}'
	                    UNION ALL 
	                    SELECT OB01007*OB01008*OA01016 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01016 AS OA01022
	                    FROM OA01 
	                    INNER JOIN OB01 ON OA01999 = OB01002
	                    INNER JOIN OC01 ON OB01999 = OC01003
	                    INNER JOIN CA01 ON CA01001 = OA01038                                       
	                    INNER JOIN UA01 ON UA01004 = OA01015
	                    LEFT JOIN CB03 ON CB03001 = OA01040
	                    WHERE OA01997 = 0 AND OA01003 <> 3 AND (OA01015 <> '' and OA01015 is not null)  and OA01002 = '{0}'
	                    UNION ALL 
	                    SELECT OB01007*OB01008*OA01018 AS OA01020,round((OB01009/OA01021/1.17),2)*OA01018 AS OA01022
	                    FROM OA01 
	                    INNER JOIN OB01 ON OA01999 = OB01002
	                    INNER JOIN OC01 ON OB01999 = OC01003
	                    INNER JOIN CA01 ON CA01001 = OA01038                                       
	                    INNER JOIN UA01 ON UA01004 = OA01017
	                    LEFT JOIN CB03 ON CB03001 = OA01040
	                    WHERE OA01997 = 0 AND OA01003 <> 3 AND (OA01017 <> '' and OA01017 is not null)  and OA01002 = '{0}'
	                    UNION ALL 
	                    SELECT OB01007*OB01008*(1-OA01016-OA01018) AS OA01020,round((OB01009/OA01021/1.17),2)*(1-OA01016-OA01018) AS OA01022
	                    FROM OA01 
	                    INNER JOIN OB01 ON OA01999 = OB01002
	                    INNER JOIN OC01 ON OB01999 = OC01003
	                    INNER JOIN CA01 ON CA01001 = OA01038                                       
	                    INNER JOIN UA01 ON UA01001 = OA01013
	                    LEFT JOIN CB03 ON CB03001 = OA01040
	                    WHERE OA01997 = 0 AND OA01003 <> 3 AND (OA01016 <> 0 OR OA01018 <> 0)  and OA01002 = '{0}' 
                    )tab", oa01002);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        #endregion

        #region 打印
        /// <summary>
        /// 客户信息和付款信息
        /// </summary>
        /// <param name="OA01002"></param>
        /// <returns></returns>
        public DataTable SelectCustomerPayment(string OA01002)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@" SELECT CA01003,CA01009,CA01011,OP01005
                                                 FROM OA01
                                                 INNER JOIN OP01 ON OA01999 = OP01003
                                                 INNER JOIN CA01 ON CA01001 = OA01038
                                                 WHERE OA01002 = '{0}'
                                                ", OA01002);
                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 商品发票
        /// </summary>
        /// <param name="OA01002"></param>
        /// <returns></returns>
        public DataTable SelectProductInvoice(string OA01002)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@" SELECT OA01009,OC01009,OA01002,OC01002,
                                                OB01005,OB01007,OB01008,OB01009
                                                 FROM OA01
                                                 INNER JOIN OB01 ON OB01002=OA01999 
                                                 INNER JOIN OC01 ON OC01003=OB01999 
                                                 WHERE OA01002 = '{0}'
                                                ", OA01002);
                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }



        #endregion
    }
}
