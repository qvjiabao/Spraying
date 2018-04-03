using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sinoo.DAL;
using System.Data;
using Sinoo.Model;
using System.Data.SqlClient;
using Sinoo.Sql;

namespace Sinoo.BLL
{
    /// <summary>
    /// 客户管理逻辑
    /// </summary>
    public class CustomerBLL
    {
        #region 实例化数据访问类

        SqlServerProvider Provider = new SqlServerProvider();

        #endregion

        #region 客户资料

        /// <summary>
        /// 获取全部客户资料表基本数据
        /// </summary>
        /// <returns>客户表基础数据DataTable</returns>
        public DataTable SelectCustomerBase()
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [CA01001] ,[CA01002] ,[CA01003] ,[CA01004] ,[CA01005]
                                                      ,[CA01006] ,[CA01007] ,[CA01008] ,[CA01009] ,[CA01010]
                                                      ,[CA01011] ,[CA01012] ,[CA01013] ,[CA01014] ,[CA01015]
                                                      ,[CA01016] ,[CA01017] ,[CA01018] ,[CA01019] ,[CA01020]
                                                      ,[CA01021] ,[CA01022] ,[CA01023] ,[CA01024] ,[CA01025]
                                                      ,[CA01026] ,[CA01027] ,[CA01028] ,[CA01029]
                                                      ,[CA01030] ,[CA01031] ,[CA01032] ,[CA01033]
                                                      ,[CA01034] ,[CA01035] ,[CA01036] ,[CA01037]
                                                      ,[CA01038] ,[CA01039] ,[CA01040] ,[CA01041]
                                                      ,[CA01042] ,[CA01043] ,[CA01044] ,[CA01045]
                                                      ,[CA01048] ,[CA01049] ,[CA01050] ,[CA01051]
                                                      ,[CA01046],[CA01047] ,[CA01997] ,[CA01998] ,[CA01999]
                                                 FROM [dbo].[CA01] WHERE CA01997 = 0
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
        /// 根据ID获取客户资料基本数据
        /// </summary>
        /// <returns>客户表基础数据DataTable</returns>
        public DataTable SelectCustomerBaseByID(int ID)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [CA01001] ,[CA01002] ,[CA01003] ,[CA01004] ,[CA01005]
                                                      ,[CA01006] ,[CA01007] ,[CA01008] ,[CA01009] ,[CA01010]
                                                      ,[CA01011] ,[CA01012] ,[CA01013] ,[CA01014] ,[CA01015]
                                                      ,[CA01016] ,[CA01017] ,[CA01018] ,[CA01019] ,[CA01020]
                                                      ,[CA01021] ,[CA01022] ,[CA01023] ,[CA01024] ,[CA01025]
                                                      ,[CA01026] ,[CA01027] ,[CA01028] ,[CA01029]
                                                      ,[CA01030] ,[CA01031] ,[CA01032] ,[CA01033]
                                                      ,[CA01034] ,[CA01035] ,[CA01036] ,[CA01037]
                                                      ,[CA01038] ,[CA01039] ,[CA01040] ,[CA01041]
                                                      ,[CA01042] ,[CA01043] ,[CA01044] ,[CA01045]
                                                      ,[CA01048] ,[CA01049] ,[CA01050] ,[CA01051]
                                                      ,[CA01046],[CA01047] ,[CA01997] ,[CA01998] ,[CA01999]
                                                 FROM [dbo].[CA01] WHERE CA01997 = 0 AND CA01001={0}
                                                ", ID);
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
        /// 根据条件获取客户资料表基本数据
        /// </summary>
        /// <returns>客户表基础数据DataTable</returns>
        public DataTable SelectCustomerBase(string strWhere)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [CA01001] ,[CA01002] ,[CA01003] ,[CA01004] ,[CA01005]
                                                      ,[CA01006] ,[CA01007] ,[CA01008] ,[CA01009] ,[CA01010]
                                                      ,[CA01011] ,[CA01012] ,[CA01013] ,[CA01014] ,[CA01015]
                                                      ,[CA01016] ,[CA01017] ,[CA01018] ,[CA01019] ,[CA01020]
                                                      ,[CA01021] ,[CA01022] ,[CA01023] ,[CA01024] ,[CA01025]
                                                      ,[CA01026] ,[CA01027] ,[CA01028] ,[CA01029] ,[CA01052]
                                                      ,[CA01030] ,[CA01031] ,[CA01032] ,[CA01033]
                                                      ,[CA01034] ,[CA01035] ,[CA01036] ,[CA01037]
                                                      ,[CA01038] ,[CA01039] ,[CA01040] ,[CA01041]
                                                      ,[CA01042] ,[CA01043] ,[CA01044] ,[CA01045]
                                                      ,[CA01048] ,[CA01049] ,[CA01050] ,[CA01051]
                                                      ,[CA01046],[CA01047] ,[CA01997] ,[CA01998] ,[CA01999]
                                                 FROM [dbo].[CA01] WHERE CA01997 = 0 {0}  
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
        /// 根据条件获取客户资料表及链表数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable SelectCustomerBaseForList(string strWhere)
        {
            DataSet ds;
            try
            {
                string strSql = string.Format(@"SELECT  [CA01001] ,[CA01002] ,[CA01003] ,[CA01004] ,[CA01005]
                                                       ,[CA01006] ,[CA01007] ,[CA01008] ,[CA01009] ,[CA01010]
                                                       ,[CA01011] ,[CA01012] ,[CA01013] ,[CA01014] ,[CA01015]
                                                       ,[CA01016] ,[CA01017] ,[CA01018] ,[CA01019] ,[CA01020]
                                                       ,[CA01021] ,[CA01022] ,[CA01023] ,[CA01024] ,[CA01025]
                                                       ,[CA01026] ,[CA01027] ,[CA01028] ,[CA01029] ,[CA01052]
                                                       ,[CA01030] ,[CA01031] ,[CA01032] ,[CA01033]
                                                       ,[CA01034] ,[CA01035] ,[CA01036] ,[CA01037]
                                                       ,[CA01038] ,[CA01039] ,[CA01040] ,[CA01041]
                                                       ,[CA01048] ,[CA01049] ,[CA01050] ,[CA01051]
                                                       ,[CA01042] ,[CA01043] ,[CA01044] ,[CA01045],[CA01046]
                                                       ,[CA01047],[CA01997] ,[CA01998] ,[CA01999] ,[CD02002],
		                                                [CB01002] ,[CB02002] ,[CD01003] ,[CB04002] ,[GA05003],
		                                                [CB03002] ,[GA06003], [ProvinceID] ,[ProvinceName],
		                                                [CityID] ,[CityName]
                                                FROM    dbo.CA01
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
                                                WHERE CA01997 = 0 {0}
                                                ORDER BY CA01998 DESC
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
        /// 根据ID删除客户资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public int RemoveCustomerBaseById(string ID)
        {
            int result = 0;
            try
            {
                string strSql = string.Format(" UPDATE CA01 SET CA01997 = 1 WHERE CA01001 = {0}", ID);
                result = Math.Abs(Provider.ExecuteNonQuery(strSql, 0, null));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 分页获取客户资料数据
        /// </summary>
        /// <param name="PageSize">每页显示行数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="strWhere">Where语句（不加WHERE关键词）</param>
        /// <returns></returns>
        public DataTable SelectCustomerBase(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"[CA01001] ,[CA01002] ,[CA01003] ,[CA01004] ,[CA01005]
                                    ,[CA01006] ,[CA01007] ,[CA01008] ,[CA01009] ,[CA01010]
                                    ,[CA01011] ,[CA01012] ,[CA01013] ,[CA01014] ,[CA01015]
                                    ,[CA01016] ,[CA01017] ,[CA01018] ,[CA01019] ,[CA01020]
                                    ,[CA01021] ,[CA01022] ,[CA01023] ,[CA01024] ,[CA01025]
                                    ,[CA01026] ,[CA01027] ,[CA01028] ,[CA01029] ,[CA01030] 
                                    ,[CA01031] ,[CA01032] ,[CA01033] ,[CA01034] ,[CA01035]
                                    ,[CA01036] ,[CA01037] ,[CA01038] ,[CA01039] ,[CA01040] 
                                    ,[CA01041] ,[CA01042] ,[CA01043] ,[CA01044] ,[CA01045]
                                    ,[CA01048] ,[CA01049] ,[CA01050] ,[CA01051]
                                    ,[CA01046] ,[CA01997] ,[CA01998] ,[CA01999] ,[CD02002],
		                             [CB01002] ,[CB02002] ,[CD01003] ,[CB04002] ,[GA05002],
		                             [CB03002] ,[GA06002], [ProvinceID] ,[ProvinceName],
		                             [CityID] ,[CityName], ROW_NUMBER() OVER(ORDER BY CA01998 DESC ) AS RowNumber";

                string strTableName = @"dbo.CA01
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
                                             ) AS GA03 ON GA03.CityID = CA01013";
                string strWhere = @" WHERE CA01997 = 0 " + strWhereAdd;

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
        /// 新增客户资料数据
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int AddCustomerBase(CustomerBase _CustomerBase)
        {
            try
            {
                CreateSqlHandler<CustomerBase> _CreateSqlHandler = new CreateSqlHandler<CustomerBase>();
                //生成Sql语句
                string strSql = _CreateSqlHandler.Insert(_CustomerBase, "CA01");

                int num = Math.Abs(Provider.ExecuteNonQuery(strSql, 0, null));

                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 新增客户资料数据(包含文件上传)
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int AddCustomerBase(CustomerBase _CustomerBase, Appendix _Appendix)
        {
            try
            {
                CreateSqlHandler<Appendix> _CreateSqlHandlerAppendix = new CreateSqlHandler<Appendix>();
                CreateSqlHandler<CustomerBase> _CreateSqlHandlerCustomerBase = new CreateSqlHandler<CustomerBase>();
                //生成CA01Sql语句
                string strCA01Sql = _CreateSqlHandlerCustomerBase.Insert(_CustomerBase, "CA01");
                //生成GA07Sql语句
                string strGA07Sql = _CreateSqlHandlerAppendix.Insert(_Appendix, "GA07");

                int num = Math.Abs(Provider.ExecuteNonQuery("PRO_CA01", 1, new SqlParameter[] { 
                new SqlParameter(){ParameterName=@"sqlCA01",Value=strCA01Sql, DbType=DbType.String},
                new SqlParameter(){ParameterName=@"sqlGA07",Value=strGA07Sql, DbType=DbType.String},
                new SqlParameter(){ParameterName=@"CA01999", Value=_CustomerBase.CA01999,DbType=DbType.String}
                }));

                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 新增客户资料数据(包含文件上传)
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int AddCustomerBase(CustomerBase _CustomerBase, List<Appendix> _Appendix)
        {

            string strGA07Sql = string.Empty;
            int num = 0;
            try
            {
                CreateSqlHandler<Appendix> _CreateSqlHandlerAppendix = new CreateSqlHandler<Appendix>();
                CreateSqlHandler<CustomerBase> _CreateSqlHandlerCustomerBase = new CreateSqlHandler<CustomerBase>();
                //生成CA01Sql语句
                string strCA01Sql = _CreateSqlHandlerCustomerBase.Insert(_CustomerBase, "CA01");
                if (_Appendix.Count > 0)
                {
                    //生成GA07Sql语句
                    for (int i = 0; i < _Appendix.Count; i++)
                    {
                        strGA07Sql += _CreateSqlHandlerAppendix.Insert(_Appendix[i], "GA07");
                    }
                    num = Math.Abs(Provider.ExecuteNonQuery("PRO_CA01", 1, new SqlParameter[] { 
                new SqlParameter(){ParameterName=@"sqlCA01",Value=strCA01Sql, DbType=DbType.String},
                new SqlParameter(){ParameterName=@"sqlGA07",Value=strGA07Sql, DbType=DbType.String},
                new SqlParameter(){ParameterName=@"CA01999", Value=_CustomerBase.CA01999,DbType=DbType.String}
                }));
                }
                else
                {
                    num = Math.Abs(Provider.ExecuteNonQuery(strCA01Sql, 0, null));
                }
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 新增客户资料数据(多条)
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int AddCustomerBase(List<CustomerBase> listCustomerBase)
        {
            try
            {
                CreateSqlHandler<CustomerBase> _CreateSqlHandler = new CreateSqlHandler<CustomerBase>();

                StringBuilder sb = new StringBuilder();
                //生成Sql语句
                foreach (CustomerBase _CustomerBase in listCustomerBase)
                {
                    string strSql = _CreateSqlHandler.Insert(_CustomerBase, "CA01");
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

        /// <summary>
        /// 修改客户资料数据
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int UpdateCustomerBase(CustomerBase _CustomerBase)
        {
            try
            {

                CreateSqlHandler<CustomerBase> _CreateSqlHandler = new CreateSqlHandler<CustomerBase>();
                //生成Sql语句
                string strSql = _CreateSqlHandler.Update(_CustomerBase, "CA01", string.Format(" WHERE CA01001 = '{0}' ", _CustomerBase.CA01001));

                int num = Math.Abs(Provider.ExecuteNonQuery(strSql, 0, null));

                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 客户文件资料


        /// <summary>
        /// 根据ID删除客户文件资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public int RemoveCustomerFileBaseById(string ID)
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
        /// 根据ID查询客户文件资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectCustomerFileByID(int ID)
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
        /// 分页获取客户文件资料数据
        /// </summary>
        /// <param name="PageSize">每页显示行数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="strWhere">Where语句（不加WHERE关键词）</param>
        /// <returns></returns>
        public DataTable SelectCustomerFileBase(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"[CA01002] ,[CA01003] ,[GA07001] ,[GA07002] ,[GA07003] 
                                    ,[GA07004] ,[GA07005] ,[GA07006] ,[GA07007] ,[GA07008] 
                                    ,[GA07997] ,[GA07998] 
		                            , ROW_NUMBER() OVER(ORDER BY GA07001 DESC ) AS RowNumber";

                string strTableName = @"dbo.GA07
                                        inner join OA01 on OA01001=GA07002
                                        LEFT join CA01 on CA01001=OA01038
                                        inner join UA01 ON UA01001 = OA01013 ";
                string strWhere = @" WHERE GA07997 = 0 AND GA07008=1 " + strWhereAdd;

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
        /// 新增客户文件资料数据(多条)
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int AddCustomerFileBase(List<Appendix> listAppendix)
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

        #region 客户行业代码资料

        /// <summary>
        /// 获取全部客户行业代码资料数据
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerTradeCode()
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(" SELECT "
                    + " CB04001, "
                    + " CB04002, "
                    + " CB04003, "
                    + " CB04005, "
                    + " CB04997, "
                    + " CB04998 "
                    + " FROM CB04 "
                    + " WHERE CB04997=0 "
                    + " ORDER BY CB04998 DESC ", 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取全部客户行业代码和行业分类资料数据
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerCB03()
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter("pro_CB03", 1, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据ID获取客户行业代码资料数据
        /// </summary>
        /// <param name="_CB04001">id</param>
        /// <returns></returns>
        public DataTable SelectCustomerTradeCodeById(string _CB04001)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                    + " CB04001, "
                    + " CB04002, "
                    + " CB04003, "
                    + " CB04005, "
                    + " CB04997, "
                    + " CB04998  "
                    + " FROM CB04  "
                    + " WHERE CB04001 ={0}", _CB04001);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }


        /// <summary>
        /// 根据名称获取客户行业代码资料数据
        /// </summary>
        /// <param name="_CB04001">id</param>
        /// <returns></returns>
        public DataTable SelectCustomerTradeCodeByName(string _CB04002)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                    + " CB04001, "
                    + " CB04002, "
                    + " CB04003, "
                    + " CB04005, "
                    + " CB04997, "
                    + " CB04998  "
                    + " FROM CB04  "
                    + " WHERE CB04997 = 0  AND  CB04002 = '{0}'", _CB04002);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据名称获取客户行业代码资料数据
        /// </summary>
        /// <param name="_CB04001">id</param>
        /// <returns></returns>
        public DataTable SelectCustomerTradeCodeByIDName(string _CB04001, string _CB04002)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                    + " CB04001, "
                    + " CB04002, "
                    + " CB04003, "
                    + " CB04005, "
                    + " CB04997, "
                    + " CB04998  "
                    + " FROM CB04  "
                    + " WHERE CB04997 = 0  AND CB04001 <> {0} AND CB04002 = '{1}' ", _CB04001, _CB04002);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据父级ID获取下级客户行业代码资料数据
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerTradeCodeByCB04003(string CB04003)
        {
            DataSet ds;
            try
            {
                if (CB04003 == "")
                {
                    return new DataTable();
                }
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(" SELECT "
                    + " CB04001, "
                    + " CB04002, "
                    + " CB04003, "
                    + " CB04005, "
                    + " CB04997, "
                    + " CB04998 "
                    + " FROM CB04 "
                    + " WHERE CB04997=0 AND CB04003 = "
                    + CB04003
                    + " ORDER BY CB04998 DESC ", 0, ref obj, null);
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
        public DataTable SelectCustomerTradeCodePage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {

                string strColumn = @"[CB04001] , [CB04002] , [CB04003] , 
                                     [CB04005] , [CB04997] , [CB04998] , [CB03002] , 
                                     ROW_NUMBER() OVER(ORDER BY CB04998 DESC ) AS RowNumber";

                string strTableName = @" [dbo].[CB04] INNER JOIN CB03 ON CB04003 = CB03001 ";
                string strWhere = @" WHERE  CB04997 = 0 " + strWhereAdd;

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
        /// 获取客户行业代码所属行业分类
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerTradeTypeByCustomerTradeCode()
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(" SELECT "
                    + " CB03001, "
                    + " CB03002, "
                    + " CB03997, "
                    + " CB03998 "
                    + " FROM CB03 "
                    + " WHERE CB03997=0 "
                    + " ORDER BY CB03998 DESC ", 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 新增客户行业代码资料数据
        /// </summary>
        /// <param name="_CB04">实体类</param>
        /// <returns>客户行业代码资料数据DataTable</returns>
        public int InsertCustomerTradeCode(CustomerTradeCode _CustomerTradeCode)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_CB04", 1, new SqlParameter[]{
                  new SqlParameter(){ParameterName=@"CB04001",Value=0, DbType=DbType.Int32},
                  new SqlParameter(){ParameterName=@"CB04002",Value=_CustomerTradeCode.CB04002, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"CB04003", Value=_CustomerTradeCode.CB04003,DbType=DbType.Int32},
                  new SqlParameter(){ParameterName=@"CB04005",Value=_CustomerTradeCode.CB04005, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"CB04997", Value=0,DbType=DbType.Int32},
                  new SqlParameter(){ParameterName=@"Order", Value="CB04_Insert",DbType=DbType.String}
              });
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改客户行业代码资料数据
        /// </summary>
        /// <param name="_CB04">实体类</param>
        /// <returns>结果数据</returns>
        public int UpdateCustomerTradeCode(CustomerTradeCode _CustomerTradeCode)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_CB04", 1, new SqlParameter[]{
                   new SqlParameter(){ParameterName=@"CB04001",Value=_CustomerTradeCode.CB04001, DbType=DbType.Int32},
                   new SqlParameter(){ParameterName=@"CB04002",Value=_CustomerTradeCode.CB04002, DbType=DbType.String},
                   new SqlParameter(){ParameterName=@"CB04003", Value=_CustomerTradeCode.CB04003,DbType=DbType.Int32},
                   new SqlParameter(){ParameterName=@"CB04005",Value=_CustomerTradeCode.CB04005, DbType=DbType.String},
                   new SqlParameter(){ParameterName=@"CB04997", Value=0,DbType=DbType.Int32},
                   new SqlParameter(){ParameterName=@"Order", Value="CB04_Update",DbType=DbType.String}
              });
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除客户行业代码资料数据
        /// </summary>
        /// <param name="_CB04">实体类</param>
        /// <returns>结果数据</returns>
        public int RemoveCustomerTradeCode(CustomerTradeCode _CustomerTradeCode)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_CB04", 1, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"CB04001",Value=_CustomerTradeCode.CB04001, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"CB04002",Value="", DbType=DbType.String},
                new SqlParameter(){ParameterName=@"CB04003", Value=0,DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"CB04005", Value="",DbType=DbType.String},
                new SqlParameter(){ParameterName=@"CB04997", Value=_CustomerTradeCode.CB04997,DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"Order", Value="CB04_Remove",DbType=DbType.String}
              });
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 客户行业分类代码资料

        /// <summary>
        /// 获取全部客户行业分类代码资料
        /// </summary>
        /// <returns>结果</returns>
        public DataTable SelectCustomerTradeType()
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [CB03001] ,[CB03002] ,
                                                       [CB03997] ,[CB03998]
                                                FROM [dbo].[CB03] WHERE CB03997 = 0 ");
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
        /// 根据条件获取客户行业分类代码资料
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectCustomerTradeType(string strWhere)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [CB03001] ,[CB03002] ,
                                                       [CB03997] ,[CB03998] 
                                                FROM [dbo].[CB03] WHERE CB03997 = 0 {0} ", strWhere);
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

        #region 客户代码资料

        /// <summary>
        /// 获取全部客户代码资料
        /// </summary>
        /// <returns>结果</returns>
        public DataTable SelectCustomerCode()
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [CB02001] ,[CB02002] ,
                                                       [CB02997] ,[CB02998]
                                                FROM [dbo].[CB02] WHERE CB02997 = 0 ");
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
        /// 根据条件获取全部客户代码资料
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectCustomerCode(string strWhere)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [CB02001] ,[CB02002] ,
                                                       [CB02997] ,[CB02998]
                                                FROM [dbo].[CB02] WHERE CB02997 = 0 {0} ", strWhere);
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

        #region 客户类型

        /// <summary>
        /// 获取全部客户类型资料
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectCustomerType()
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [CB01001] ,[CB01002] ,
                                                       [CB01997] ,[CB01998]
                                                FROM [dbo].[CB01] WHERE CB01997 = 0 ");
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
        /// 根据条件获取客户类型资料
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectCustomerType(string strWhere)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [CB01001] ,[CB01002] ,
                                                       [CB01997] ,[CB01998]
                                                FROM [dbo].[CB01] WHERE CB01997 = 0 {0} ", strWhere);
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

        #region 应用代码
        public DataTable SelectAC()
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@" SELECT OB02001,OB02002 FROM  OB02");
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
        public DataTable SelectAD(string SIC)
        {

            DataSet ds;
            try
            {

                string strSql = string.Format(@" 
                                    SELECT  OD01001,OD01002,OD01003 
                                    FROM OD01 WHERE OD01002 = {0}", SIC);
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

        #region 电子类别240

        /// <summary>
        /// 获取全部电子类别240资料
        /// </summary>
        /// <returns>结果</returns>
        public DataTable SelectElectronType()
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [GA05001] ,[GA05002] ,[GA05003] 
                                                      ,[GA05004] ,[GA05997] ,[GA05998]
                                                  FROM [dbo].[GA05] WHERE GA05997 = 0 ");
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
        /// 根据条件获取电子类别240资料
        /// </summary>
        /// <returns>结果</returns>
        public DataTable SelectElectronType(string strWhere)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [GA05001] ,[GA05002] ,[GA05003] 
                                                      ,[GA05004] ,[GA05997] ,[GA05998]
                                                  FROM [dbo].[GA05] WHERE GA05997 = 0 {0} ", strWhere);
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

        #region 汽车分类220

        /// <summary>
        /// 获取全部汽车分类220资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectAuteType()
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [GA06001] ,[GA06002] ,[GA06003]
                                                      ,[GA06004] ,[GA06997] ,[GA06998]
                                                  FROM [dbo].[GA06] WHERE GA06997 = 0 ");
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
        /// 根据条件获取汽车分类220资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectAuteType(string strWhere)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [GA06001] ,[GA06002] ,[GA06003]
                                                      ,[GA06004] ,[GA06997] ,[GA06998]
                                                  FROM [dbo].[GA06] {0} 1=1 AND GA06997 = 0 ", strWhere);
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

        #region 根据市ID查询客户
        /// <summary>
        /// 根据市ID查询客户资料
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectCustomerByShiID(string CA01013)
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(string.Format(" SELECT * FROM CA01 WHERE CA01013='{0}' ", CA01013), 0, ref obj, null);
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
