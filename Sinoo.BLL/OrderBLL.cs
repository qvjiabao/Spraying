using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sinoo.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Sinoo.Model;
using Sinoo.Sql;

namespace Sinoo.BLL
{
    /// <summary>
    /// 订单
    /// </summary>
    public class OrderBLL
    {
        #region 实例化数据访问类

        SqlServerProvider Provider = new SqlServerProvider();

        #endregion

        #region 订单导出

        /// <summary>
        /// 导出订单信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportSelectOrderBase(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT [CA01001] ,[CA01002] ,[CA01003] ,[CA01004] ,[CA01005]
		                                                ,[CA01006] ,[CA01007] ,[CA01008] ,[CA01009] ,[CA01010]
		                                                ,[CA01011] ,[CA01012] ,[CA01013] ,[CA01014] ,[CA01015]
		                                                ,[CA01016] ,[CA01017] ,[CA01018] ,[CA01019] ,[CA01020]
		                                                ,[CA01021] ,[CA01022] ,[CA01023] ,[CA01024] ,[CA01025]
		                                                ,[CA01026] ,[CA01027] ,[CA01028] ,[CA01029] ,[CA01030]
                                                        ,[CA01031] ,[CA01032] ,[CA01033] ,[CA01034] ,[CA01035]
                                                        ,[CA01036] ,[CA01037] ,[CA01038] ,[CA01039] ,[CA01040]
                                                        ,[CA01041] ,[CA01042] ,[CA01043] ,[CA01044] ,[CA01045]
                                                        ,[CA01046] ,[CA01047] ,[CA01997] ,[CA01998] ,[CA01999]
                                                        ,[CD02002] ,[CB01002] ,[CB02002] ,[CD01003] ,[CB04002] 
                                                        ,[GA05002] ,[CB03002] ,[GA06002], [ProvinceID] ,[ProvinceName]
		                                                ,[OA01001] ,[OA01002] ,[OA01003] ,[OA01004] ,[OA01005]
		                                                ,[OA01006] ,[OA01007] ,[OA01008] ,[OA01009] ,[OA01010]
		                                                ,[OA01011] ,[OA01012] ,[OA01013] ,[OA01014] ,[OA01015]
		                                                ,[OA01016] ,[OA01017] ,[OA01018] ,[OA01019] ,[OA01020]
		                                                ,[OA01021] ,[OA01022] ,[OA01023] ,[OA01024] ,[OA01025]
		                                                ,[OA01026] ,[OA01027] ,[OA01028] ,[OA01029] ,[OA01030]
		                                                ,[OA01031] ,[OA01032] ,[OA01033] ,[OA01034] ,[OA01035]
		                                                ,[OA01036] ,[OA01037] ,[OA01038] ,[OA01039] ,[OA01040]
		                                                ,[OA01041] ,[OA01042] ,[OA01043] ,[OA01044] ,[OA01045] 
                                                        ,[OA01046] ,[OA01047] ,[OA01048] ,[OA01049] ,[OA01050] 
                                                        ,[OA01051] ,[OA01997] ,[OA01998] ,[OA01999]
		                                                ,[CityID] ,[CityName] ,[UA01001] ,[UA01002] ,[UA01003]
                                                        ,[UA01004] ,[UA01005] ,[UA01006] ,[UA01007] ,[UA01008]
                                                        ,[UA01009] ,[UA01010] ,[UA01011] ,[UA01012] ,[UA01013]
                                                        ,[UA01014] ,[UA01015] ,[UA01016] ,[UA01017] ,[UA01018]
                                                        ,[UA01019] ,[UA01020] ,[UA01021] ,[UA01022] ,[UA01023]
                                                        ,[UA01024] ,[UA01997] ,[UA01998] ,[UA01999]
                                                        ,ROW_NUMBER() OVER(ORDER BY OA01998 DESC ) AS RowNumber

                                                        FROM dbo.OA01 JOIN dbo.CA01 ON CA01001 = OA01.OA01038
		                                                LEFT JOIN dbo.CD02 ON CD02.CD02001 = CA01.CA01025
                                                        LEFT JOIN dbo.CB01 ON CB01.CB01001 = CA01.CA01016
                                                        LEFT JOIN dbo.CB02 ON CB02.CB02001 = CA01.CA01018
                                                        LEFT JOIN dbo.CD01 ON CD01.CD01001 = CA01.CA01024
                                                        LEFT JOIN dbo.CB04 ON CB04.CB04001 = CA01.CA01020
                                                        LEFT JOIN dbo.GA05 ON GA05.GA05001 = CA01.CA01022
                                                        LEFT JOIN dbo.CB03 ON CB03.CB03001 = CA01.CA01019
                                                        LEFT JOIN dbo.GA06 ON GA06.GA06001 = CA01.CA01023
                                                        LEFT JOIN ( SELECT   GProvince.GA03001 ProvinceID ,
                                                                        GProvince.GA03002 ProvinceName ,
                                                                        GCity.GA03001 CityID ,
                                                                        GCity.GA03002 CityName
                                                               FROM     dbo.GA03 GCity
                                                                        JOIN GA03 GProvince ON GCity.GA03003 = GProvince.GA03001
                                                             ) AS GA03 ON GA03.CityID = CA01013 
                                                        JOIN dbo.UA01 ON UA01001 = OA01.OA01013
                WHERE OA01997 = 0 {0} AND OA01003 != 2 ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 导出订单信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportSelectOrderBaseReport(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT [CA01001] ,[CA01002] ,[CA01003] ,[CA01004] ,[CA01005]
		                                                ,[CA01006] ,[CA01007] ,[CA01008] ,[CA01009] ,[CA01010]
		                                                ,[CA01011] ,[CA01012] ,[CA01013] ,[CA01014] ,[CA01015]
		                                                ,[CA01016] ,[CA01017] ,[CA01018] ,[CA01019] ,[CA01020]
		                                                ,[CA01021] ,[CA01022] ,[CA01023] ,[CA01024] ,[CA01025]
		                                                ,[CA01026] ,[CA01027] ,[CA01028] ,[CA01029] ,[CA01030]
                                                        ,[CA01031] ,[CA01032] ,[CA01033] ,[CA01034] ,[CA01035]
                                                        ,[CA01036] ,[CA01037] ,[CA01038] ,[CA01039] ,[CA01040]
                                                        ,[CA01041] ,[CA01042] ,[CA01043] ,[CA01044] ,[CA01045]
                                                        ,[CA01046] ,[CA01047] ,[CA01997] ,[CA01998] ,[CA01999]
                                                        ,[CD02002] ,[CB01002] ,[CB02002] ,[CD01003] ,[CB04002] 
                                                        ,[GA05002] ,[CB03002] ,[GA06002], [ProvinceID] ,[ProvinceName]
		                                                ,[OA01001] ,[OA01002] ,[OA01003] ,[OA01004] ,[OA01005]
		                                                ,[OA01006] ,[OA01007] ,[OA01008] ,[OA01009] ,[OA01010]
		                                                ,[OA01011] ,[OA01012] ,[OA01013] ,[OA01014] ,[OA01015]
		                                                ,[OA01016] ,[OA01017] ,[OA01018] ,[OA01019] ,[OA01020]
		                                                ,[OA01021] ,[OA01022] ,[OA01023] ,[OA01024] ,[OA01025]
		                                                ,[OA01026] ,[OA01027] ,[OA01028] ,[OA01029] ,[OA01030]
		                                                ,[OA01031] ,[OA01032] ,[OA01033] ,[OA01034] ,[OA01035]
		                                                ,[OA01036] ,[OA01037] ,[OA01038] ,[OA01039] ,[OA01040]
		                                                ,[OA01041] ,[OA01042] ,[OA01043] ,[OA01044] ,[OA01045] 
                                                        ,[OA01046] ,[OA01047] ,[OA01048] ,[OA01049] ,[OA01050] 
                                                        ,[OA01051] ,[OA01997] ,[OA01998] ,[OA01999]
		                                                ,[CityID] ,[CityName] ,[UA01001] ,[UA01002] ,[UA01003]
                                                        ,[UA01004] ,[UA01005] ,[UA01006] ,[UA01007] ,[UA01008]
                                                        ,[UA01009] ,[UA01010] ,[UA01011] ,[UA01012] ,[UA01013]
                                                        ,[UA01014] ,[UA01015] ,[UA01016] ,[UA01017] ,[UA01018]
                                                        ,[UA01019] ,[UA01020] ,[UA01021] ,[UA01022] ,[UA01023]
                                                        ,[UA01024] ,[UA01997] ,[UA01998] ,[UA01999]
                                                        ,ROW_NUMBER() OVER(ORDER BY OA01998 DESC ) AS RowNumber

                                                        FROM dbo.OA01 JOIN dbo.CA01 ON CA01001 = OA01.OA01038
		                                                LEFT JOIN dbo.CD02 ON CD02.CD02001 = CA01.CA01025
                                                        LEFT JOIN dbo.CB01 ON CB01.CB01001 = CA01.CA01016
                                                        LEFT JOIN dbo.CB02 ON CB02.CB02001 = CA01.CA01018
                                                        LEFT JOIN dbo.CD01 ON CD01.CD01001 = CA01.CA01024
                                                        LEFT JOIN dbo.CB04 ON CB04.CB04001 = CA01.CA01020
                                                        LEFT JOIN dbo.GA05 ON GA05.GA05001 = CA01.CA01022
                                                        LEFT JOIN dbo.CB03 ON CB03.CB03001 = CA01.CA01019
                                                        LEFT JOIN dbo.GA06 ON GA06.GA06001 = CA01.CA01023
                                                        LEFT JOIN ( SELECT   GProvince.GA03001 ProvinceID ,
                                                                        GProvince.GA03002 ProvinceName ,
                                                                        GCity.GA03001 CityID ,
                                                                        GCity.GA03002 CityName
                                                               FROM     dbo.GA03 GCity
                                                                        JOIN GA03 GProvince ON GCity.GA03003 = GProvince.GA03001
                                                             ) AS GA03 ON GA03.CityID = CA01013 
                                                        JOIN dbo.UA01 ON UA01001 = OA01.OA01013
                WHERE OA01997 = 0 AND OA01003 <> 3  {0} ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 导出商品信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportSelectProduct(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT     
                                                OB01001,  OB01003, OB01004, OB01005, OB01006, OB01007, OB01008,
                                                OB01009, OB01010, OB01011, OB01012, OB01013, OB01014, OB01015, OB01016,
                                                OB01017, OB01018, OB01997, OB01998, OB01999, OA01001, OA01002 OB01002 
                                          
                                                FROM OB01 INNER JOIN OA01 ON(OA01999 = OB01002) 
                                                WHERE OB01002 IN ({0})
                                                    ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 导出发票信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet ExportSelectInvoice(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT
                                        OA01001 , OA01002 ,OA01003 , OB01001 ,
                                        OB01002 , OB01003 , OB01004 ,OB01005 ,
                                        OB01016 , OB01017 , OB01018 , OB01997 , OB01998 , OB01999 ,  OC01001 ,
                                        OC01002 ,OC01003 ,OC01004 ,OC01005 ,OC01006 ,OC01007 ,OC01008 ,
                                        OC01009 ,OC01010 ,OC01011 ,OC01012 ,OC01013 ,OC01014 ,OC01015 , OC01016 , 
                                        OC01017 ,OC01018 , OC01997 , OC01998 , OC01999
                                        FROM OA01  
                                        INNER JOIN OB01 on OB01002=OA01999 
                                        INNER JOIN OC01 on OC01003=OB01999 WHERE OC01003 IN ({0})
                                                    ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ExportSelectPayment(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT 
                                                OA01002, CA01003, OP01001, OP01002, OP01003, OP01004, OP01005, OP01006, OP01007,
                                                OP01008, OP01009, OP01010, OP01011, OP01012, OP01013, OP01014, OP01015,
                                                OP01016, OP01997, OP01998, OP01999 FROM OA01
                                                INNER JOIN CA01 ON CA01001 = OA01038   
                                                INNER JOIN OP01 ON OA01999 = OP01003
                                                WHERE OP01003 IN ({0})
                                                    ", strWhere);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        #endregion

        #region 订单
        /// <summary>
        /// 查询所有订单信息订单详细信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOrderBaseAndOrderProductAll()
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT OA01002 ,OA01999,OB01005,OB01999 
                                                FROM OA01
                                                INNER JOIN OB01 ON OA01999 = OB01002
                                                WHERE OA01997 = 0  ");

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
        /// 根据用户所属区域id 查询所有销售
        /// </summary>
        /// <param name="_UA01013"></param>
        /// <returns></returns>
        public DataTable SelectUserBaseByEara(string _UA01013)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT UA01001 ,UA01004 
                                                FROM UA01
                                                WHERE UA01997 = 0 AND UA01013 = '{0}'
                                                ", _UA01013);

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
        /// 查询所有区域销售
        /// </summary>
        /// <returns></returns>
        public DataTable SelectUserBaseByEara()
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT UA01001 ,UA01004 
                                                FROM UA01
                                                WHERE UA01997 = 0 ");

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
        /// 获取 系统最后一张订单号
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOrderBaseByAutojet(string UA01013)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(string.Format(@" SELECT TOP 1 OA01002 
                                FROM dbo.OA01 JOIN dbo.UA01 ON UA01001 = OA01037 
                                WHERE OA01003 = 1 AND OA01002 LIKE 'A%' AND UA01013 = '{0}' 
                                ORDER BY OA01998 DESC ", UA01013));

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
        /// 获取客户最后一张订单号
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOrderBaseByCustomer(string OA01038)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(string.Format(@" SELECT TOP 1 Convert(varchar(15),OA01009,23) OA01009
                                FROM dbo.OA01 WHERE OA01003 = 1 AND OA01002 not LIKE 'A%' AND OA01038 = '{0}' 
                                ORDER BY OA01998 DESC ", OA01038));

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
        /// 获取销售员的客户
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerBaseByUserID(string UA01001)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(string.Format(@" SELECT OA01038 
                                FROM OA01
                                WHERE OA01013 = '{0}' 
                                GROUP BY OA01038 ", UA01001));

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
        /// 获取销售员的客户
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerBaseByoOrderID(string UA01001)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(string.Format(@" SELECT OA01001
                                FROM OA01
                                WHERE OA01013 = '{0}' ", UA01001));

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
        /// 获取 喷嘴最后一张订单号
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOrderBaseByNozzle(string UA01013)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(string.Format(@" 
                    SELECT TOP 1 OA01002 
                    FROM dbo.OA01 JOIN dbo.UA01 ON UA01001 = OA01037
                    WHERE  OA01003 = 1 AND  OA01002 NOT LIKE 'A%' AND UA01013 = '{0}' ORDER BY OA01998 DESC ", UA01013));

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
        /// 根据条件获取订单资料表及链表数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable SelectOrderBaseForList(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT   [CA01001] ,[CA01002] ,[CA01003] ,[CA01004] ,[CA01005]
		                                                ,[CA01006] ,[CA01007] ,[CA01008] ,[CA01009] ,[CA01010]
		                                                ,[CA01011] ,[CA01012] ,[CA01013] ,[CA01014] ,[CA01015]
		                                                ,[CA01016] ,[CA01017] ,[CA01018] ,[CA01019] ,[CA01020]
		                                                ,[CA01021] ,[CA01022] ,[CA01023] ,[CA01024] ,[CA01025]
		                                                ,[CA01026] ,[CA01027] ,[CA01028] ,[CA01029] ,[CA01030]
                                                        ,[CA01031] ,[CA01032] ,[CA01033] ,[CA01034] ,[CA01035]
                                                        ,[CA01036] ,[CA01037] ,[CA01038] ,[CA01039] ,[CA01040]
                                                        ,[CA01041] ,[CA01042] ,[CA01043] ,[CA01044] ,[CA01045]
                                                        ,[CA01046] ,[CA01047] ,[CA01997] ,[CA01998] ,[CA01999]
                                                        ,[CD02002] ,[CB01002] ,[CB02002] ,[CD01003] ,[CB04002] 
                                                        ,[GA05002] ,[CB03002] ,[GA06002], [ProvinceID] ,[ProvinceName]
		                                                ,[OA01001] ,[OA01002] ,[OA01003] ,[OA01004] ,[OA01005]
		                                                ,[OA01006] ,[OA01007] ,[OA01008] ,[OA01009] ,[OA01010]
		                                                ,[OA01011] ,[OA01012] ,[OA01013] ,[OA01014] ,[OA01015]
		                                                ,[OA01016] ,[OA01017] ,[OA01018] ,[OA01019] ,[OA01020]
		                                                ,[OA01021] ,[OA01022] ,[OA01023] ,[OA01024] ,[OA01025]
		                                                ,[OA01026] ,[OA01027] ,[OA01028] ,[OA01029] ,[OA01030]
		                                                ,[OA01031] ,[OA01032] ,[OA01033] ,[OA01034] ,[OA01035]
		                                                ,[OA01036] ,[OA01037] ,[OA01038] ,[OA01039] ,[OA01040]
		                                                ,[OA01041] ,[OA01042] ,[OA01997] ,[OA01998] ,[OA01999]
		                                                ,[CityID] ,[CityName] [UA01001] ,[UA01002] ,[UA01003]
                                                        ,[UA01004] ,[UA01005] ,[UA01006] ,[UA01007] ,[UA01008]
                                                        ,[UA01009] ,[UA01010] ,[UA01011] ,[UA01012] ,[UA01013]
                                                        ,[UA01014] ,[UA01015] ,[UA01016] ,[UA01017] ,[UA01018]
                                                        ,[UA01019] ,[UA01020] ,[UA01021] ,[UA01022] ,[UA01023]
                                                        ,[UA01024] ,[UA01997] ,[UA01998] ,[UA01999]
                                                FROM dbo.OA01 JOIN dbo.CA01 ON CA01001 = OA01035
		                                                LEFT JOIN dbo.CD02 ON CD02.CD02001 = CA01.CA01025
                                                        LEFT JOIN dbo.CB01 ON CB01.CB01001 = CA01.CA01016
                                                        LEFT JOIN dbo.CB02 ON CB02.CB02001 = CA01.CA01018
                                                        LEFT JOIN dbo.CD01 ON CD01.CD01001 = CA01.CA01024
                                                        LEFT JOIN dbo.CB04 ON CB04.CB04001 = CA01.CA01020
                                                        LEFT JOIN dbo.GA05 ON GA05.GA05001 = CA01.CA01022
                                                        LEFT JOIN dbo.CB03 ON CB03.CB03001 = CA01.CA01019
                                                        LEFT JOIN dbo.GA06 ON GA06.GA06001 = CA01.CA01023
                                                        LEFT JOIN ( SELECT   GProvince.GA03001 ProvinceID ,
                                                                        GProvince.GA03002 ProvinceName ,
                                                                        GCity.GA03001 CityID ,
                                                                        GCity.GA03002 CityName
                                                               FROM     dbo.GA03 GCity
                                                                        JOIN GA03 GProvince ON GCity.GA03003 = GProvince.GA03001
                                                             ) AS GA03 ON GA03.CityID = CA01013 
                                                        JOIN dbo.UA01 ON UA01001 = OA01.OA01012
					                                                WHERE OA01997 = 0 {0}
                                                                    ORDER BY OA01998 DESC
                                                    ", strWhere);

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
        /// 查询数据
        /// </summary>
        /// <param name="_OA01001">Id</param>
        /// <returns></returns>
        public DataTable SelectOrderBaseById(string _OA01001)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT
                                 OA01001,OA01002,OA01003,OA01004,OA01005
                                ,OA01006,OA01007,OA01008,OA01009,OA01010
                                ,OA01011,OA01012,OA01013,OA01014,OA01015
                                ,OA01016,OA01017,OA01018,OA01019,OA01020
                                ,OA01021,OA01022,OA01023,OA01024,OA01025
                                ,OA01026,OA01027,OA01028,OA01029,OA01030
                                ,OA01031,OA01032,OA01033,OA01034,OA01035
                                ,OA01036,OA01037,OA01038,OA01039,OA01040
                                ,OA01041,OA01042,OA01043,OA01044,OA01045
                                ,OA01046,OA01047,OA01048,OA01049,OA01050
                                ,OA01051,OA01997,OA01998,OA01999,OB02002
                                ,UA01004
                                FROM OA01
                                INNER JOIN OB02 ON OB02001 = OA01025
                                INNER JOIN UA01 ON UA01001 = OA01013
                                WHERE OA01001 = '{0}'", _OA01001);

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
        /// 查询出所有订单号（不包括退货）
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOrderBaseForList()
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT
                                 OA01001,OA01002,OA01003,OA01004,OA01005
                                ,OA01006,OA01007,OA01008,OA01009,OA01010
                                ,OA01011,OA01012,OA01013,OA01014,OA01015
                                ,OA01016,OA01017,OA01018,OA01019,OA01020
                                ,OA01021,OA01022,OA01023,OA01024,OA01025
                                ,OA01026,OA01027,OA01028,OA01029,OA01030
                                ,OA01031,OA01032,OA01033,OA01034,OA01035
                                ,OA01036,OA01037,OA01038,OA01039,OA01040
                                ,OA01041,OA01042,OA01043,OA01044,OA01045
                                ,OA01046,OA01047,OA01048,OA01049,OA01050
                                ,OA01051,OA01997,OA01998,OA01999
                                FROM OA01
                                WHERE OA01003 <> 2");

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
        /// 查询数据
        /// </summary>
        /// <param name="_OA01001">Id</param>
        /// <returns></returns>
        public DataTable SelectOrderBaseForListById(string _OA01001)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT
                                 OA01001,OA01002,OA01003,OA01004,OA01005
                                ,OA01006,OA01007,OA01008,OA01009,OA01010
                                ,OA01011,OA01012,OA01013,OA01014,OA01015
                                ,OA01016,OA01017,OA01018,OA01019,OA01020
                                ,OA01021,OA01022,OA01023,OA01024,OA01025
                                ,OA01026,OA01027,OA01028,OA01029,OA01030
                                ,OA01031,OA01032,OA01033,OA01034,OA01035
                                ,OA01036,OA01037,OA01038,OA01039,OA01040
                                ,OA01041,OA01042,OA01043,OA01044,OA01045
                                ,OA01046,OA01047,OA01048,OA01049,OA01050
                                ,OA01051,OA01053,OA01997,OA01998,OA01999
                                ,OB02001,OB02002,UA01001,UA01004,OD01003,OA01054,OA01055,OA01056,OA01057,OA01058
                                FROM OA01
                                INNER JOIN OB02 ON OB02001 = OA01025
                                INNER JOIN UA01 ON UA01001 = OA01013
                                LEFT JOIN OD01 ON OD01001 = OA01047
                                 WHERE OA01001 = '{0}'", _OA01001);

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
        /// 检查是否有当前订单编号(退货单)
        /// </summary>
        /// <param name="OA01002"></param>
        /// <returns></returns>
        public DataTable SelectOrderBaseByName(string OA01002)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + " [OA01001] "
                      + " FROM OA01 "
                      + " WHERE OA01997 = 0 AND OA01003 = 2 AND OA01002 = '{0}' ", OA01002);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 检查是否有当前订单编号（销售单）
        /// </summary>
        /// <param name="OA01002"></param>
        /// <returns></returns>
        public DataTable SelectOrderBaseByName2(string OA01002)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + " [OA01001] "
                      + " FROM OA01 "
                      + " WHERE OA01997 = 0 AND OA01003 = 1 AND OA01002 = '{0}' ", OA01002);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 修改时检查是否有当前订单编号（销售单）
        /// </summary>
        /// <param name="OA01002"></param>
        /// <returns></returns>
        public DataTable SelectOrderBaseByNameForEdit(string OA01002, string ID)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + " [OA01001] "
                      + " FROM OA01 "
                      + " WHERE OA01997 = 0 AND OA01003 = 1 AND OA01002 = '{0}' AND OA01001 <> '{1}' ", OA01002, ID);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 修改检查是否有当前订单编(退货单)
        /// </summary>
        /// <param name="OA01002"></param>
        /// <returns></returns>
        public DataTable SelectOrderBaseByName2(string OA01002, string ID)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + " [OA01001] "
                      + " FROM OA01 "
                      + " WHERE OA01997 = 0 AND OA01003 = 2 AND OA01001 <> {0} AND OA01002 = '{1}' ", ID, OA01002);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据产品型号获取订单GUID数据
        /// </summary>
        /// <param name="PartNo">型号(模糊查询）</param>
        /// <returns></returns>
        public DataTable SelectOrderGUIDByPartNo(string PartNo)
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(string.Format(" SELECT "
                    + " OB01002 "
                    + " FROM OB01 "
                    + " WHERE OB01997 = 0 "
                    + " AND OB01005 LIKE '%{0}%' ", PartNo), 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 分页获取获取订单资料表及链表数据
        /// </summary>
        /// <param name="PageSize">每页显示行数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="strWhere">Where语句（不加WHERE关键词）</param>
        /// <returns></returns>
        public DataTable SelectOrderBaseForList(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"*,ROW_NUMBER() OVER(ORDER BY OA01009 DESC ) AS RowNumber";

                string strTableName = string.Format(@" (
                                          SELECT * FROM (
                                                        SELECT  CA01002,OA01001,CA01003,OA01008,OA01002, OA01009,OA01020
                                                                    ,CA01001,UA01004,ProvinceName,CityName,OP01015
                                                                    ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01998 DESC ) AS NUM
                                                        FROM dbo.OA01 JOIN dbo.CA01 ON CA01001 = OA01.OA01038
		                                                LEFT JOIN dbo.CD02 ON CD02.CD02001 = CA01.CA01025
                                                        LEFT JOIN dbo.CB01 ON CB01.CB01001 = CA01.CA01016
                                                        LEFT JOIN dbo.CB02 ON CB02.CB02001 = CA01.CA01018
                                                        LEFT JOIN dbo.CD01 ON CD01.CD01001 = CA01.CA01024
                                                        LEFT JOIN dbo.CB04 ON CB04.CB04001 = CA01.CA01020
                                                        LEFT JOIN dbo.GA05 ON GA05.GA05001 = CA01.CA01022
                                                        LEFT JOIN dbo.CB03 ON CB03.CB03001 = CA01.CA01019
                                                        LEFT JOIN dbo.GA06 ON GA06.GA06001 = CA01.CA01023
                                                        LEFT JOIN OP01 ON OA01999 = OP01003
                                                        LEFT JOIN ( SELECT   GProvince.GA03001 ProvinceID ,
                                                                        GProvince.GA03002 ProvinceName ,
                                                                        GCity.GA03001 CityID ,
                                                                        GCity.GA03002 CityName
                                                               FROM     dbo.GA03 GCity
                                                                        JOIN GA03 GProvince ON GCity.GA03003 = GProvince.GA03001
                                                             ) AS GA03 ON GA03.CityID = CA01013 
                                                        JOIN dbo.UA01 ON UA01001 = OA01.OA01013
                                                        LEFT JOIN OB01 ON OB01002 = OA01999
                                                        LEFT JOIN OC01 ON OC01003 = OB01999 
                                                         WHERE OA01997 = 0  AND OA01003 != 2 {0} ) A WHERE NUM=1 
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
        /// 分页获取获取订单资料表及链表数据
        /// </summary>
        /// <param name="PageSize">每页显示行数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="strWhere">Where语句（不加WHERE关键词）</param>
        /// <returns></returns>
        public DataTable SelectOrderBaseReportForList(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"*,ROW_NUMBER() OVER(ORDER BY OA01009 DESC ) AS RowNumber";

                string strTableName = string.Format(@" (
                                          SELECT * FROM (
                                                        SELECT  CA01002,OA01001,CA01003,OA01008,OA01002, OA01009,OA01020
                                                                    ,CA01001,UA01004,ProvinceName,CityName,OP01015
                                                                    ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OA01998 DESC ) AS NUM
                                                        FROM dbo.OA01 JOIN dbo.CA01 ON CA01001 = OA01.OA01038
		                                                LEFT JOIN dbo.CD02 ON CD02.CD02001 = CA01.CA01025
                                                        LEFT JOIN dbo.CB01 ON CB01.CB01001 = CA01.CA01016
                                                        LEFT JOIN dbo.CB02 ON CB02.CB02001 = CA01.CA01018
                                                        LEFT JOIN dbo.CD01 ON CD01.CD01001 = CA01.CA01024
                                                        LEFT JOIN dbo.CB04 ON CB04.CB04001 = CA01.CA01020
                                                        LEFT JOIN dbo.GA05 ON GA05.GA05001 = CA01.CA01022
                                                        LEFT JOIN dbo.CB03 ON CB03.CB03001 = CA01.CA01019
                                                        LEFT JOIN dbo.GA06 ON GA06.GA06001 = CA01.CA01023
                                                        LEFT JOIN OP01 ON OA01999 = OP01003
                                                        LEFT JOIN ( SELECT   GProvince.GA03001 ProvinceID ,
                                                                        GProvince.GA03002 ProvinceName ,
                                                                        GCity.GA03001 CityID ,
                                                                        GCity.GA03002 CityName
                                                               FROM     dbo.GA03 GCity
                                                                        JOIN GA03 GProvince ON GCity.GA03003 = GProvince.GA03001
                                                             ) AS GA03 ON GA03.CityID = CA01013 
                                                        JOIN dbo.UA01 ON UA01001 = OA01.OA01013
                                                        LEFT JOIN OB01 ON OB01002 = OA01999
                                                        LEFT JOIN OC01 ON OC01003 = OB01999 
                                                         WHERE OA01997 = 0  AND OA01003 <> 3 {0} ) A WHERE NUM=1 
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
        /// 根据订单编号查询客户编码
        /// </summary>
        /// <param name="OA01001">订单编号</param>
        /// <returns></returns>
        public DataTable SelectOrderBaseByCustomerId(string OA01002)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT UA01004,OA01036,OA01037,OA01038,OA01039,OA01040,OA01041,OA01042,OA01043,OA01044
                                                FROM OA01
                                                inner join UA01 on(UA01001=OA01013)
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
        /// 查询商品详细GUID
        /// </summary>
        /// <param name="OA01999">订单GUID</param>
        /// <returns></returns>
        public DataTable SelectOrderProductByOrderCustomerGUID(string OA01999)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + " OB01999 "
                      + " FROM OB01 "
                      + " WHERE OB01997 = 0 AND OB01002 = '{0}' ", OA01999);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询订单及链表数据
        /// </summary>
        /// <param name="_OA01001">Id</param>
        /// <returns></returns>
        public DataTable SelectOrderBaseFoListById(string _OA01001)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT
                                 OA01001,OA01002,OA01003,OA01004,OA01005
                                ,OA01006,OA01007,OA01008,OA01009,OA01010
                                ,OA01011,OA01012,OA01013,OA01014,OA01015
                                ,OA01016,OA01017,OA01018,OA01019,OA01020
                                ,OA01021,OA01022,OA01023,OA01024,OA01025
                                ,OA01026,OA01027,OA01028,OA01029,OA01030
                                ,OA01031,OA01032,OA01033,OA01034,OA01035
                                ,OA01036,OA01037,OA01038,OA01039,OA01040
                                ,OA01041,OA01042,OA01043,OA01044,OA01045
                                ,OA01046,OA01047,OA01048,OA01049,OA01050
                                ,OA01051,OA01997,OA01998,OA01999,OB02002
                                ,UA01004,OA01053
                                FROM OA01
                                INNER JOIN OB02 ON OB02001 = OA01025
                                INNER JOIN UA01 ON UA01001 = OA01013
                                WHERE OA01001 = '{0}'", _OA01001);

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
        /// 省分页
        /// </summary>
        /// <param name="PageSize">每页显示数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="strWhereAdd">Where语句</param>
        /// <param name="obj">数据总数</param>
        /// <returns></returns>
        public DataTable SelectOrderBaseReturnPage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"OA01001,OA01002,OA01003,OA01004,OA01005
                        ,OA01006,OA01007,OA01008,OA01009,OA01010
                        ,OA01011,OA01012,OA01013,OA01014,OA01015
                        ,OA01016,OA01017,OA01018,OA01019,OA01020
                        ,OA01021,OA01022,OA01023,OA01024,OA01025
                        ,OA01026,OA01027,OA01028,OA01029,OA01030
                        ,OA01031,OA01032,OA01033,OA01034,OA01035
                        ,OA01036,OA01037,OA01038,OA01039,OA01040
                        ,OA01041,OA01042,OA01043,OA01044,OA01045
                        ,OA01046,OA01047,OA01048,OA01049,OA01050
                        ,OA01051,OA01053,OA01997,OA01998,OA01999,CA01003
                        ,dbo.FX_GetProvinceByCityId(CA01013) as ProvinceName
                        ,dbo.FX_GetProvinceIdByCityId(CA01013) as ProvinceId
                        ,UA01004,ROW_NUMBER() OVER(ORDER BY OA01998 DESC ) AS RowNumber
                        ,GA03002";
                string strTableName = @" OA01 
                        inner join CA01 on CA01001 = OA01038   
                        inner join UA01 on UA01001 = OA01013 
                        inner join GA03 on GA03001 = CA01013 ";
                string strWhere = @" WHERE OA01997=0 and OA01003 = 2  " + strWhereAdd;

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
        /// 新增订单(退单)
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int AddOrderBase(OrderBase _OrderBase, List<OrderProduct> listOrderProduct, List<OrderInvoice> listOrderInvoice)
        {
            try
            {
                CreateSqlHandler<OrderBase> _CreateSqlHandler = new CreateSqlHandler<OrderBase>();
                CreateSqlHandler<OrderProduct> _CreateSqlHandler1 = new CreateSqlHandler<OrderProduct>();
                CreateSqlHandler<OrderInvoice> _CreateSqlHandler2 = new CreateSqlHandler<OrderInvoice>();
                List<string> _liststring = new List<string>();
                //生成Sql语句
                string strSql = _CreateSqlHandler.Insert(_OrderBase, "OA01");  //订单
                _liststring.Add(strSql);
                foreach (OrderProduct item in listOrderProduct)  //商品详情
                {
                    strSql = _CreateSqlHandler1.Insert(item, "OB01");
                    _liststring.Add(strSql);
                }

                foreach (OrderInvoice item in listOrderInvoice)  //发票
                {
                    strSql = _CreateSqlHandler2.Insert(item, "OC01");
                    _liststring.Add(strSql);
                }

                int num = Math.Abs(Provider.TranExecuteNonQuerys(_liststring, null));

                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 新增订单(订单)
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int AddOrderBase(OrderBase _OrderBase, List<OrderProduct> listOrderProduct, List<OrderInvoice> listOrderInvoice, OrderPayment _OrderPayment)
        {
            try
            {
                CreateSqlHandler<OrderBase> _CreateSqlHandler = new CreateSqlHandler<OrderBase>();
                CreateSqlHandler<OrderProduct> _CreateSqlHandler1 = new CreateSqlHandler<OrderProduct>();
                CreateSqlHandler<OrderInvoice> _CreateSqlHandler2 = new CreateSqlHandler<OrderInvoice>();
                CreateSqlHandler<OrderPayment> _CreateSqlHandler3 = new CreateSqlHandler<OrderPayment>();
                List<string> _liststring = new List<string>();
                //生成Sql语句
                string strSql = _CreateSqlHandler.Insert(_OrderBase, "OA01");  //订单
                _liststring.Add(strSql);

                foreach (OrderProduct item in listOrderProduct)  //商品详情
                {
                    strSql = _CreateSqlHandler1.Insert(item, "OB01");
                    _liststring.Add(strSql);
                }

                foreach (OrderInvoice item in listOrderInvoice)  //发票
                {
                    strSql = _CreateSqlHandler2.Insert(item, "OC01");
                    _liststring.Add(strSql);
                }
                strSql = _CreateSqlHandler3.Insert(_OrderPayment, "OP01");
                _liststring.Add(strSql);

                int num = Math.Abs(Provider.TranExecuteNonQuerys(_liststring, null));

                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 编辑订单(退单)
        /// </summary>
        /// <param name="_OrderBase">订单</param>
        /// <param name="listOrderProduct">商品明细</param>
        /// <param name="listOrderInvoice">发票</param>
        /// <returns></returns>
        public int ModOrderBase(OrderBase _OrderBase, List<OrderProduct> listOrderProduct, List<OrderInvoice> listOrderInvoice)
        {
            try
            {
                CreateSqlHandler<OrderBase> _CreateSqlHandler = new CreateSqlHandler<OrderBase>();
                CreateSqlHandler<OrderProduct> _CreateSqlHandler1 = new CreateSqlHandler<OrderProduct>();
                CreateSqlHandler<OrderInvoice> _CreateSqlHandler2 = new CreateSqlHandler<OrderInvoice>();
                List<string> _liststring = new List<string>();
                //生成Sql语句
                string strSql = _CreateSqlHandler.Update(_OrderBase, "OA01", string.Format(" WHERE OA01001 = {0} ", _OrderBase.OA01001));  //订单
                _liststring.Add(strSql);

                DataTable dt = SelectOrderProductByOrderCustomerGUID(_OrderBase.OA01999); //查询商品详细GUID
                if (dt.Rows.Count > 0)
                {
                    string strguid = string.Empty;  //记录商品明细
                    foreach (DataRow item in dt.Rows)
                    {
                        strguid += "'" + item["OB01999"].ToString() + "',";
                    }
                    strguid = strguid.Substring(0, strguid.Length - 1);
                    strSql = string.Format(" DELETE OC01 WHERE OC01003 in ({0}) ", strguid);  //根据订单详情GUID 删除所有发票信息
                    _liststring.Add(strSql);
                }

                strSql = string.Format(" DELETE OB01 WHERE OB01002 = '{0}'", _OrderBase.OA01999);  //根据退货订单GUID 删除所有商品详情
                _liststring.Add(strSql);
                foreach (OrderProduct item in listOrderProduct)  //商品详情
                {
                    strSql = _CreateSqlHandler1.Insert(item, "OB01");
                    _liststring.Add(strSql);
                }


                foreach (OrderInvoice item in listOrderInvoice)  //发票
                {
                    strSql = _CreateSqlHandler2.Insert(item, "OC01");
                    _liststring.Add(strSql);
                }

                int num = Math.Abs(Provider.TranExecuteNonQuerys(_liststring, null));

                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 编辑订单(销售单)
        /// </summary>
        /// <param name="_OrderBase">订单</param>
        /// <param name="listOrderProduct">商品明细</param>
        /// <param name="listOrderInvoice">发票</param>
        /// <returns></returns>
        public int ModOrderBase(OrderBase _OrderBase, List<OrderProduct> listOrderProduct, List<OrderInvoice> listOrderInvoice, OrderPayment _OrderPayment)
        {
            try
            {
                CreateSqlHandler<OrderBase> _CreateSqlHandler = new CreateSqlHandler<OrderBase>();
                CreateSqlHandler<OrderProduct> _CreateSqlHandler1 = new CreateSqlHandler<OrderProduct>();
                CreateSqlHandler<OrderInvoice> _CreateSqlHandler2 = new CreateSqlHandler<OrderInvoice>();
                CreateSqlHandler<OrderPayment> _CreateSqlHandler3 = new CreateSqlHandler<OrderPayment>();
                List<string> _liststring = new List<string>();
                //生成Sql语句
                string strSql = _CreateSqlHandler.Update(_OrderBase, "OA01", string.Format(" WHERE OA01001 = {0} ", _OrderBase.OA01001));  //订单
                _liststring.Add(strSql);

                //在修改销售员的情况 退单也修改销售员
                OrderBase _ReturnOrderBase = new OrderBase();
                _ReturnOrderBase.OA01013 = _OrderBase.OA01013;

                strSql = _CreateSqlHandler.Update(_ReturnOrderBase, "OA01"
                    , string.Format(" WHERE OA01003 = 2 AND  OA01039='{0}'", _OrderBase.OA01002));  //退单

                _liststring.Add(strSql);

                DataTable dt = SelectOrderProductByOrderCustomerGUID(_OrderBase.OA01999); //查询商品详细GUID
                if (dt.Rows.Count > 0)
                {
                    string strguid = string.Empty;  //记录商品明细
                    foreach (DataRow item in dt.Rows)
                    {
                        strguid += "'" + item["OB01999"].ToString() + "',";
                    }
                    strguid = strguid.Substring(0, strguid.Length - 1);
                    strSql = string.Format(" DELETE OC01 WHERE OC01003 in ({0}) ", strguid);  //根据订单详情GUID 删除所有发票信息
                    _liststring.Add(strSql);
                }

                strSql = string.Format(" DELETE OB01 WHERE OB01002 = '{0}'", _OrderBase.OA01999);  //根据退货订单GUID 删除所有商品详情
                _liststring.Add(strSql);

                foreach (OrderProduct item in listOrderProduct)  //商品详情
                {
                    strSql = _CreateSqlHandler1.Insert(item, "OB01");
                    _liststring.Add(strSql);
                }


                foreach (OrderInvoice item in listOrderInvoice)  //发票
                {
                    strSql = _CreateSqlHandler2.Insert(item, "OC01");
                    _liststring.Add(strSql);
                }

                strSql = _CreateSqlHandler3.Update(_OrderPayment, "OP01", string.Format(" WHERE OP01003 = '{0}' ", _OrderPayment.OP01003));
                _liststring.Add(strSql);

                int num = Math.Abs(Provider.TranExecuteNonQuerys(_liststring, null));

                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 编辑订单退货
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int ModOrderBase02(OrderBase _OrderBase)
        {
            try
            {
                CreateSqlHandler<OrderBase> _CreateSqlHandler = new CreateSqlHandler<OrderBase>();
                //生成Sql语句
                string strWhere = string.Format("Where OA01001 = {0}", _OrderBase.OA01001);
                string strSql = _CreateSqlHandler.Update(_OrderBase, "OA01", strWhere);
                int num = Math.Abs(Provider.ExecuteNonQuery(strSql, 0, null));
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据订单编号查询商品明细
        /// </summary>
        /// <param name="ProductNo">订单编号</param>
        /// <param name="type">1商品订单 2 退货订单</param>
        /// <returns></returns>
        public DataTable SelectOrderProductByProductNo(string ProductGUID, int type)
        {

            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT                         
                    OB01001,OB01002,OB01003,OB01004,OB01005,
                    OB01006,OB01007,OB01008,OB01009,OB01010,
                    OB01011,OB01012,OB01013,OB01014,OB01015,
                    OB01016,OB01017,OB01018,OB01997,OB01998,
                    OB01999
                    FROM OB01
                    WHERE OB01002 = '{0}' AND OB01997 = 0 
                    ORDER BY OB01998 ASC", ProductGUID, type);
                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
                //WHERE OB01002 = '{0}' AND OB01997 = 0 AND OB01003 = {1}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据商品明细GUID查询发票信息
        /// </summary>
        /// <param name="GUID">GUID</param>
        ///<param name="type">1商品订单 2退货单</param>
        /// <returns></returns>
        public DataTable SelectOrderInvoiceByGUID(string GUID, int type)
        {

            DataSet ds;
            try
            {
                string strSql = string.Format(@" SELECT                         
                   OC01001,OC01002,OC01003,OC01004,OC01005,
                   OC01006,OC01007,OC01008,OC01009,OC01010,
                   OC01011,OC01012,OC01013,OC01014,OC01015,
                   OC01016,OC01017,OC01018,OC01019,OC01020,OC01997,OC01998,OC01999
                   FROM OC01
                   WHERE OC01997 = 0 AND OC01003 IN ({1})
                   ORDER BY OC01002 ASC", type, GUID);
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
        /// 根据订单查询销售人员
        /// </summary>
        /// <param name="GUID">GUID</param>
        ///<param name="type">1商品订单 2退货单</param>
        /// <returns></returns>
        public DataTable SelectOA01SalesName(string value)
        {

            DataSet ds;
            try
            {
                string strSql = string.Format(@" 
                    SELECT OA01013,UA01004 FROM OA01
                    INNER JOIN UA01 ON(UA01001=OA01013)
                    WHERE OA01997=0 AND OA01002 = '{0}'", value);
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
        /// 到货提醒 颜色红色 状态 red
        /// </summary>
        /// <returns></returns>
        public DataTable SelectMessageArrival(int UserId)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" 
                      SELECT * FROM (

                            SELECT OA01002,'Red' AS COLOR ,OA01010
                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OC01009 DESC) NUM
                            ,OC01009,OA01001
                            FROM OA01
                            INNER JOIN OB01 ON OB01002=OA01999
                            INNER JOIN OC01 ON OC01003=OB01999  
                            WHERE OA01002 in( 
                            SELECT 	OA01002
                            FROM OA01 
                            INNER JOIN OB01 ON OB01002=OA01999
                            INNER JOIN OC01 ON OC01003=OB01999
                            WHERE OC01007 =  0 AND (OA01013 = {0} OR OA01052 = {0})
                            GROUP BY OA01002
                            )
                            ) A
                      WHERE NUM =1 AND DATEDIFF(DAY,OA01010,GETDATE()) <= 0 AND DATEDIFF(DAY,OA01010,GETDATE()) >= -3 ", UserId);
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
        ///发货提醒 颜色蓝色 状态 blue
        /// </summary>
        /// <returns></returns>
        public DataTable SelectMessageDelivered(int UserId)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" 
                      SELECT * FROM 
                      (
			               SELECT OA01002,'Blue' AS COLOR 
                            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OC01009 DESC) NUM
                            ,OC01009,OA01001
                            FROM OA01
                            INNER JOIN OB01 ON OB01002=OA01999
                            INNER JOIN OC01 ON OC01003=OB01999  
                            WHERE OA01002 not in( 
                            SELECT 	OA01002
                            FROM OA01 
                            INNER JOIN OB01 ON OB01002=OA01999
                            INNER JOIN OC01 ON OC01003=OB01999
                            WHERE OC01007 =  0   AND (OA01013 = {0} OR OA01052 = {0}) OR OC01010 = 1
                            GROUP BY OA01002
                            )
                      ) A
                      WHERE A.NUM =1 AND DATEDIFF(DAY,A.OC01009,GETDATE()) > 15	", UserId);
                //                string strSql = string.Format(@" 
                //                      SELECT * FROM 
                //                      (
                //			                SELECT OA01002,'Blue' AS COLOR 
                //				                ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OC01009 DESC) NUM
                //				                ,OC01009,OA01001
                //			                FROM OA01
                //			                INNER JOIN OB01 ON OB01002=OA01999
                //			                INNER JOIN OC01 ON OC01003=OB01999  
                //			                WHERE OA01002 IN(     
                //				                SELECT OA01002	
                //				                FROM OA01
                //				                INNER JOIN OB01 ON OB01002=OA01999
                //				                INNER JOIN OC01 ON OC01003=OB01999 
                //				                GROUP BY OA01002
                //				                HAVING dbo.FX_GetArrivedState(COUNT(1),SUM(Case when OC01007= 1 then 1 else 0 end)) ='true') 
                //			                    AND (OA01013 = {0} OR OA01052 = {0})
                //                      ) A
                //                      WHERE NUM =1 AND DATEDIFF(DAY,OC01009,GETDATE()) > 15	", UserId);
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
        ///未付款提醒 颜色紫色 状态 Purple
        /// </summary>
        /// <returns></returns>
        public DataTable SelectMessageNoPayment(int UserId)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@" 
                      SELECT * FROM (
			                SELECT OA01002,'Purple' AS COLOR 
				            ,ROW_NUMBER() OVER(PARTITION BY OA01002 ORDER BY OC01011 DESC) NUM
				            ,OC01009,OC01011,OA01001,OP01016
			                FROM OA01
			                INNER JOIN OB01 ON OB01002=OA01999
			                INNER JOIN OC01 ON OC01003=OB01999
			                INNER JOIN OP01 ON OP01003 = OA01999
			                WHERE OA01002 NOT IN(     
				                SELECT OA01002	
				                FROM OA01
				                INNER JOIN OB01 ON OB01002=OA01999
				                INNER JOIN OC01 ON OC01003 = OB01999 
                                WHERE OC01007 = 0 OR OC01010 = 0
				                GROUP BY OA01002
                               ) 
                                AND (OA01013 = {0} OR OA01052 = {0})
			                ) A
                        WHERE NUM =1 AND DATEDIFF(DAY,OC01011,GETDATE()) > 30 AND OP01016 > 0", UserId);
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

        #region 订单打印

        //订单所有数据（不包括发票付款 商品详细）
        public DataTable SelectOrderAll(string OA01001)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT  OA01001, OA01002, OA01003, OA01004, OA01005, OA01006, OA01007, OA01008,
                                                     OA01009, OA01010, OA01011, OA01012, OA01013, OA01014, OA01019, OA01020, OA01021, OA01022, OA01023, OA01024,
                                                     OA01025, OA01026, OA01027, OA01028, OA01029, OA01030, OA01031, OA01032,
                                                     OA01033, OA01034, OA01035, OA01036, OA01037, OA01038, OA01039, OA01040,
                                                     OA01041, OA01042, OA01043, OA01044, OA01045, OA01046, OA01047, OA01048,
                                                     OA01049, OA01050, OA01051, OA01052, OA01997, OA01998, OA01999, CA01001,
                                                     CA01002, CA01003, CA01004, CA01005, CA01006, CA01007, CA01008, CA01009,
                                                     CA01010, CA01011, CA01012, CA01013, CA01014, CA01015, CA01016, CA01017,
                                                     CA01018, CA01019, CA01020, CA01021, CA01022, CA01023, CA01024, CA01025,
                                                     CA01026, CA01027, CA01028, CA01029, CA01030, CA01031, CA01032, CA01033,
                                                     CA01034, CA01035, CA01036, CA01037, CA01038, CA01039, CA01040, CA01041,
                                                     CA01042, CA01043, CA01044, CA01045, CA01046, CA01047, CA01997, CA01998,
                                                     CA01999, CB02001, CB02002, CB02997, CB02998, b.UA01001, b.UA01002, b.UA01003,
                                                     b.UA01004, b.UA01005, b.UA01006, b.UA01007, b.UA01008, b.UA01009, b.UA01010, b.UA01011,
                                                     b.UA01012, b.UA01013, b.UA01014, b.UA01015, b.UA01016, b.UA01017, b.UA01018, b.UA01019,
                                                     b.UA01020, b.UA01021, b.UA01022, b.UA01023, b.UA01024, b.UA01997, b.UA01998, b.UA01999,
                                                     OB02001, OB02002, OB02003, CB04001, CB04002, CB04003, CB04005, CB04997,
                                                     CB04998, ISNULL(A.UA01004,'赵佳')  TIANZHI,
                                                    (CASE WHEN OA01016 = 0 THEN '' ELSE CASE OA01015 WHEN '' THEN OA01049 ELSE OA01015 END  END ) OA01015
                                                    ,(CASE WHEN OA01018 = 0 THEN '' ELSE CASE OA01017 WHEN '' THEN OA01050 ELSE OA01017 END  END ) OA01017
                                                    ,(CASE OA01016 WHEN 0 THEN '' ELSE CAST(OA01016*100 AS VARCHAR(20)) +'%' END ) OA01016
                                                    ,(CASE OA01018 WHEN 0 THEN '' ELSE CAST(OA01018*100 AS VARCHAR(20)) +'%' END ) OA01018
                                            FROM    OA01
                                                    INNER JOIN CA01 ON OA01038 = CA01001
                                                    LEFT JOIN CB02 ON CB02001 = CA01018
                                                    INNER JOIN UA01 b ON b.UA01001 = OA01013
                                                    INNER JOIN OB02 ON OA01025 = OB02001
                                                    LEFT JOIN CB04 ON CB04001 = CA01020
                                                    LEFT JOIN UA01 A ON A.UA01001 = OA01052
                                                    WHERE OA01001 = '{0}'", OA01001);

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        //订单商品数据
        public DataTable SelectOrderProduct(string OA01001)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT OB01002,OB01003, OB01004,OB01005,OB01006,
                                            	   OB01007,OB01008,OB01009,OB01010,OB01011,
                                            	   OB01012,OB01013,OB01014,OB01015, OB01016,
                                            	   OB01017,OB01018, OB01997, OB01998,OB01999,
                                                   OC01002
                                                FROM OA01
                                                INNER JOIN OB01 ON OA01999 = OB01002
                                                INNER JOIN OC01 ON OB01999 = OC01003
                                                WHERE OA01001 = '{0}' 
                                                ORDER BY OC01002 ASC", OA01001);

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

        #region 客户文件资料


        /// <summary>
        /// 根据ID删除订单文件资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public int RemoveOrderFileBaseById(string ID)
        {
            int result = 0;
            try
            {
                string strSql = string.Format(" UPDATE GA07 SET GA07997 = 1 WHERE GA07001 = {0}", ID);
                result = Math.Abs(Provider.ExecuteNonQuery(strSql, 0, null));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        /// <summary>
        /// 根据ID查询订单文件资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectOrderFileByID(int ID)
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(string.Format(" SELECT * FROM GA07 WHERE GA07001={0} ", ID), 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据ID查询客户订单资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectOrderFileByID(string ID)
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(string.Format(@" select OA01001,CA01003,OA01002 from OA01
                                                            inner join CA01 on CA01001=OA01038 
                                                            where OA01001='{0}' ", ID), 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据客户名称和订单号查询订单数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectOrderByCustomerOrderID(string CA01003, string OA01001)
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(string.Format(@" select CA01003,OA01002,OA01001 from OA01
                                                                        inner join CA01 on CA01001=OA01038 
                                                                        where CA01003 like '%{0}%' AND OA01002 like '%{1}%'", CA01003, OA01001), 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 分页获取订单文件资料数据
        /// </summary>
        /// <param name="PageSize">每页显示行数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="strWhere">Where语句（不加WHERE关键词）</param>
        /// <returns></returns>
        public DataTable SelectOrderFileBase(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"[GA07001] ,[CA01003] ,[GA07006] ,[OA01002],[OA01009]
		                            , ROW_NUMBER() OVER(ORDER BY GA07001 DESC ) AS RowNumber";

                string strTableName = @"dbo.GA07
                                        inner join OA01 on OA01001=GA07002
                                        inner join CA01 on CA01001=OA01038
                                        inner join UA01 ON UA01001 = OA01013 
                                       ";
                string strWhere = @" where GA07997=0 AND GA07008=2 " + strWhereAdd;

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
        /// 新增订单文件资料数据(多条)
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int AddOrderFileBase(List<Appendix> listAppendix)
        {
            try
            {
                CreateSqlHandler<Appendix> _CreateSqlHandler = new CreateSqlHandler<Appendix>();

                StringBuilder sb = new StringBuilder();
                //生成Sql语句
                foreach (Appendix _CustomerFile in listAppendix)
                {
                    string strSql = _CreateSqlHandler.Insert(_CustomerFile, "GA07");
                    sb.AppendLine(strSql);
                }

                int num = Math.Abs(Provider.ExecuteNonQuery(sb.ToString(), 0, null));

                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region 应用代码OB02

        /// <summary>
        /// 获取全部应用代码数据
        /// </summary>
        /// <returns></returns>
        public DataTable SelectACCode()
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT [OB02001] 
                                                      ,[OB02002] 
                                                      ,[OB02003]
                                                  FROM [dbo].[OB02]
                                                    ");

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
        /// 获取全部应用代码数据
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOB02Code()
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT [OB02001] as id
                                                      ,[OB02002] as name
                                                      ,[OB02003]
                                                  FROM [dbo].[OB02]
                                                    ");

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
        /// 根据条件获取应用代码数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable SelectACCode(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT [OB02001]
                                                      ,[OB02002]
                                                      ,[OB02003]
                                                  FROM [dbo].[OB02]
                                                    ", strWhere);

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

        #region 应用描述

        /// <summary>
        /// 获取全部应用描述数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable SelectApplicationDescription()
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT [OD01001]
                                                      ,[OD01002]
                                                      ,[OD01003]
                                                      ,[OD01997]
                                                      ,[OD01998]
                                                  FROM [dbo].[OD01] WHERE OD01997 = 0
                                                 ");

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
        /// 根据条件获取应用描述数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable SelectApplicationDescription(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT [OD01001]
                                                      ,[OD01002]
                                                      ,[OD01003]
                                                      ,[OD01997]
                                                      ,[OD01998]
                                                  FROM [dbo].[OD01] WHERE OD01997 = 0 {0}
                                                    ", strWhere);

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

        #region 付款

        /// <summary>
        /// 获取全部付款数据
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOrderPayment()
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT [OP01001] ,[OP01002] ,[OP01003] ,[OP01004] ,[OP01005]
                                                      ,[OP01006] ,[OP01007] ,[OP01008] ,[OP01009] ,[OP01010]
                                                      ,[OP01011] ,[OP01012] ,[OP01013] ,[OP01014] ,[OP01015]
                                                      ,[OP01016] ,[OP01017] ,[OP01018] ,[OP01019] ,[OP01020]
                                                      ,[OP01021] ,[OP01022] ,[OP01997] ,[OP01998] ,[OP01999]
                                                  FROM [dbo].[OP01] WHERE OP01997 = 0
                                                    ");

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
        /// 根据条件获取付款数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable SelectOrderPayment(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT [OP01001] ,[OP01002] ,[OP01003] ,[OP01004] ,[OP01005]
                                                      ,[OP01006] ,[OP01007] ,[OP01008] ,[OP01009] ,[OP01010]
                                                      ,[OP01011] ,[OP01012] ,[OP01013] ,[OP01014] ,[OP01015]
                                                      ,[OP01016] ,[OP01017] ,[OP01018] ,[OP01019] ,[OP01020]
                                                      ,[OP01021] ,[OP01022] ,[OP01997] ,[OP01998] ,[OP01999]
                                                  FROM [dbo].[OP01] WHERE OP01997 = 0 {0}
                                                    ", strWhere);

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

        #region 2018-3-29 欠款订单、未发货订单

        /// <summary>
        /// 欠款订单
        /// </summary>
        /// <returns></returns>
        public DataTable GetDebtsList(int pageIndex, int pageSize, string where, ref object obj)
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
                                        WHERE OA01997 = 0 AND  OA01003 = 1 AND OP01016 > 0  " + where + "  ) A ");
                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=pageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=pageSize, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Column", Value=strColumn,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"TableName", Value=strTableName,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Where", Value=string.Empty,DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value="",DbType=DbType.String}
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            DataTable dt = ds.Tables[0];
            return dt;
        }


        /// <summary>
        /// 欠款订单
        /// </summary>
        /// <returns></returns>
        public DataTable GetDebtsAmountAndDay(string where)
        {
            object obj = null;
            DataSet ds = Provider.ReturnDataSetByDataAdapter(@" 
                SELECT ISNULL(SUM(OA01022),0) OA01022,ISNULL(MAX(DATEDIFF(day,b.DebtsDays,getdate())),0) DebtsDays
                FROM OA01 a
                INNER JOIN CA01 ON CA01001 = OA01038
                INNER JOIN UA01 ON UA01001 = OA01013
                INNER JOIN OP01 ON OA01999 = OP01003
                INNER JOIN(SELECT OA01999, MAX(OC01015) OC01015, MAX(OC01011) OC01011, MIN(OC01009) OC01009
                            , CASE WHEN MIN(OC01015) >= MIN(OC01011)
                                   THEN MIN(OC01011)
                                   WHEN MIN(OC01015) < MIN(OC01011)
                                   THEN MIN(OC01011)
                                   WHEN MIN(OC01015) IS NULL AND MIN(OC01011) IS NOT NULL
                                   THEN MIN(OC01011)
                                   WHEN MIN(OC01015) IS NOT NULL AND MIN(OC01011) IS NULL
                                   THEN MIN(OC01015) END DebtsDays
                        FROM OC01
                        INNER JOIN OB01 ON OB01999 = OC01003
                        INNER JOIN OA01 ON OA01999 = OB01002
                        GROUP BY oa01999
                        )b ON a.oa01999 = b.oa01999
                WHERE OA01997 = 0 AND  OA01003 = 1 AND OP01016 > 0 " + where, 0, ref obj, null);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取全部付款数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetPengdingList(int pageIndex, int pageSize, string where, ref object obj)
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
                                AND (OA01003 = 1 OR OA01003 =3)  " + where;

                ds = Provider.ReturnDataSetByDataAdapter("PRO_Page", 1, ref obj, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"PageIndex",Value=pageIndex, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"PageSize",Value=pageSize, DbType=DbType.Int32},
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
    }
}
