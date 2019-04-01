using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sinoo.DAL;
using Sinoo.Model;
using System.Data.SqlClient;
using System.Data;

namespace Sinoo.BLL
{
    /// <summary>
    /// 基本资料管理逻辑
    /// </summary>
    public class AreaBLL
    {
        #region 实例化数据访问类

        SqlServerProvider Provider = new SqlServerProvider();

        #endregion

        #region 系统区域表

        /// <summary>
        /// 获取汇率
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetSettings()
        {
            DataSet ds;
            try
            {
                object obj = null;

                string strsql = " select top 1 SettingId, Value from Settings order by ModifyOn desc";

                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds.Tables[0];
        }

        /// <summary>
        /// 更新汇率
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int UpdateSettings(string id, string value)
        {
            var sql = string.Format(" update  Settings set ModifyOn = GETDATE(),Value = '{0}' where SettingId = '{1}' ",value,id);

            try
            {
                int num = Provider.ExecuteNonQuery(sql, 0, null);
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 查询全部省市
        /// </summary>
        /// <param name="PageSize">每页显示数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="strWhereAdd">Where语句</param>
        /// <param name="obj">数据总数</param>
        /// <returns></returns>
        public DataTable SelectSystemAreaAll()
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + "  A.*, ISNULL(B.GA03002 ,'无') PNAME "
                      + " FROM GA03 A LEFT JOIN dbo.GA03 B ON A.GA03003 = B.GA03001 "
                      + " WHERE A.GA03997 = 0 ");
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
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
        public DataTable SelectSystemAreaProvincePage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"[GA03001] , [GA03002] , [GA03003] ,[GA03004] ,
                                    [GA03997] , [GA03998] ,
                                   ROW_NUMBER() OVER(ORDER BY GA03998 ASC ) AS RowNumber";
                string strTableName = @" [dbo].[GA03] ";
                string strWhere = @" WHERE  GA03003=0 AND GA03997 = 0 " + strWhereAdd;

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
        /// 省分页
        /// </summary>
        /// <param name="PageSize">每页显示数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="strWhereAdd">Where语句</param>
        /// <param name="obj">数据总数</param>
        /// <returns></returns>
        public DataTable SelectSystemAreaCityPage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"GA03001 , GA03002 , GA03003 ,GA03004 ,
                                   GA03997 , GA03998 ,dbo.FX_GetProvinceByCityId(GA03001) as Province ,
                                   ROW_NUMBER() OVER(ORDER BY GA03998 ASC ) AS RowNumber ";
                string strTableName = @" GA03 ";
                string strWhere = @" WHERE GA03003 <> 0 AND  GA03997 = 0 " + strWhereAdd;

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
        /// 新增检查省份是否存在
        /// </summary>
        /// <param name="GA03002">省份名称</param>
        /// <returns></returns>
        public DataTable SelectSystemAreaByProvinceName(string GA03002)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + " GA03001 "
                      + " FROM GA03 "
                      + " WHERE GA03997 = 0  AND GA03003 = 0 AND GA03002 = '{0}' ", GA03002);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 修改检查省份是否存在
        /// </summary>
        /// <param name="GA03002">省份名称</param>
        /// <param name="GA03001">省份Id</param>
        /// <returns></returns>
        public DataTable SelectSystemAreaByProvinceName(string GA03002, string GA03001)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + " GA03001 "
                      + " FROM GA03 "
                      + " WHERE GA03997 = 0 "
                      + " AND GA03003 = 0  "
                      + " AND GA03002 = '{0}' "
                      + " AND GA03001 <> {1}  ", GA03002, GA03001);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 修改检查城市是否存在
        /// </summary>
        /// <param name="GA03002">城市名称</param>
        /// <param name="GA03001">城市Id</param>
        /// <returns></returns>
        public DataTable SelectSystemAreaByCityName(string GA03002, string GA03001)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + " GA03001 "
                      + " FROM GA03 "
                      + " WHERE GA03997 = 0 "
                      + " AND GA03003 <> 0  "
                      + " AND GA03002 = '{0}' "
                      + " AND GA03001 <> {1}  ", GA03002, GA03001);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 新增检查城市是否存在
        /// </summary>
        /// <param name="GA03002">城市名称</param>
        /// <returns></returns>
        public DataTable SelectSystemAreaByCityName(string GA03002)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                      + " GA03001 "
                      + " FROM GA03 "
                      + " WHERE GA03997 = 0  AND GA03003 <> 0 AND GA03002 = '{0}' ", GA03002);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据编码获取系统区域表资料
        /// </summary>
        /// <param name="_GA03001"></param>
        /// <returns></returns>
        public DataTable SelectSystemAreaById(string _GA03001)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format(" SELECT GA03001,GA03002,GA03003,GA03004,GA03997,GA03998 FROM GA03 WHERE GA03001 ={0}", _GA03001);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询全部系统区域表资料(市)
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectSystemAreaForCity()
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(" SELECT B.GA03001,B.GA03002,B.GA03003,B.GA03004, "
                    + " B.GA03997,B.GA03998,A.GA03002 as Province "
                    + " FROM GA03 A "
                    + " INNER JOIN GA03 B ON(A.GA03001=B.GA03003) "
                    + " WHERE B.GA03997 =0  "
                    + " ORDER BY B.GA03998 ASC ", 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据省编码获取全部系统区域表资料(市)
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectSystemAreaForCity(string GA03001)
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(string.Format(@" SELECT [GA03001] ,[GA03002] ,[GA03003]
                                                                   ,[GA03004] ,[GA03997] ,[GA03998]
                                                            FROM [dbo].[GA03] WHERE GA03003 = '{0}' AND GA03997=0 ", GA03001), 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取全部系统区域表资料（省）
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectSystemAreaForProvince()
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(" SELECT GA03001,GA03002,GA03003,GA03004,GA03997,GA03998 FROM GA03 WHERE GA03997=0 AND GA03003=0 ORDER BY GA03998 ASC ", 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取系统区域表资料省Id
        /// </summary>
        /// <returns></returns>
        public string CreateSystemAreaProvinceId()
        {
            object strId;
            try
            {
                string strsql = string.Format(" SELECT GA03001 FROM GA03 WHERE GA03998=(SELECT MAX(GA03998) FROM GA03 WHERE GA03003=0 ) ");
                strId = Provider.ExecuteScalar(strsql, 0, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (strId == null)
            {
                strId = "100000";
            }
            else
            {
                strId = (Convert.ToInt32(strId) + 10000).ToString();
            }
            return strId.ToString();
        }

        /// <summary>
        /// 根据父级ID获取系统区域表资料市ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string CreateSystemAreaCityId(string id)
        {
            object strId;
            try
            {
                string strsql = string.Format(" SELECT GA03001 "
                    + " FROM GA03 "
                    + " WHERE GA03998 = "
                    + "(SELECT MAX(GA03998) FROM GA03 WHERE GA03003 = '{0}') ",
                    id);
                strId = Provider.ExecuteScalar(strsql, 0, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (strId == null)
            {
                strId = id.Substring(0, 3) + "100";
            }
            else
            {
                strId = (Convert.ToInt32(strId) + 100).ToString();
            }
            return strId.ToString();
        }

        /// <summary>
        /// 新增系统区域表资料
        /// </summary>
        /// <param name="_GA03">实体类</param>
        /// <returns>影响行数</returns>
        public int InsertSystemArea(SystemArea _SystemArea)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_GA03", 1, new SqlParameter[]{
                  new SqlParameter(){ParameterName=@"GA03001",Value=_SystemArea.GA03001, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03002",Value=_SystemArea.GA03002, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03003", Value=_SystemArea.GA03003,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03004", Value=_SystemArea.GA03004,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03997", Value=0,DbType=DbType.Int32},
                  new SqlParameter(){ParameterName=@"Order", Value="GA03_Insert",DbType=DbType.String}
              });
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改系统区域表资料
        /// </summary>
        /// <param name="_GA03">实体类</param>
        /// <returns>影响行数</returns>
        public int UpdateSystemArea(SystemArea _SystemArea)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_GA03", 1, new SqlParameter[]{
                  new SqlParameter(){ParameterName=@"GA03001",Value=_SystemArea.GA03001, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03002",Value=_SystemArea.GA03002, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03003", Value=_SystemArea.GA03003,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03004", Value=_SystemArea.GA03004,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03997", Value=0,DbType=DbType.Int32},
                  new SqlParameter(){ParameterName=@"Order", Value="GA03_Update",DbType=DbType.String}
              });
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除系统区域表资料
        /// </summary>
        /// <param name="_GA03">实体类</param>
        /// <returns>影响行数</returns>
        public int RemoveSystemArea(SystemArea _SystemArea)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_GA03", 1, new SqlParameter[]{
                  new SqlParameter(){ParameterName=@"GA03001",Value=_SystemArea.GA03001, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03002",Value="", DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03003", Value="",DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03004", Value="",DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA03997", Value=_SystemArea.GA03997,DbType=DbType.Int32},
                  new SqlParameter(){ParameterName=@"Order", Value="GA03_Remove",DbType=DbType.String}
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
