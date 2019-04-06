using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sinoo.DAL;
using Sinoo.Model;
using System.Data;
using System.Data.SqlClient;
using Sinoo.Sql;

namespace Sinoo.BLL
{
    /// <summary>
    /// 用户管理逻辑
    /// </summary>
    public class UserBLL
    {
        #region 实例化数据访问类

        SqlServerProvider Provider = new SqlServerProvider();

        #endregion

        #region 系统角色

        /// <summary>
        /// 获取全部系统角色资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectSystemRole()
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(" SELECT "
                    + " GA02001, "
                    + " GA02002, "
                    + " GA02003, "
                    + " GA02997, "
                    + " GA02998 "
                    + " FROM GA02 "
                    + " WHERE GA02997=0 "
                    + " ORDER BY GA02998 DESC ", 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 角色Id查询权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public DataTable SelectSystemRole(int RoleId)
        {
            DataSet ds;
            try
            {
                object obj = null;
                ds = Provider.ReturnDataSetByDataAdapter(string.Format(@"
                        SELECT GC01001,GA01001,GA01002,GA01003,GA01004 
                        FROM GC01 
	                    INNER JOIN GA01 ON GC01002=GA01001
                        WHERE GC01001 = {0}", RoleId), 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }


        /// <summary>
        /// 根据ID获取系统角色资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectSystemRoleById(string _GA02001)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                    + " GA02001, "
                    + " GA02002, "
                    + " GA02003, "
                    + " GA02997, "
                    + " GA02998  "
                    + " FROM GA02  "
                    + " WHERE GA02997 = 0 AND GA02001 ={0}", _GA02001);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据名称获取系统角色资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectSystemRoleByName(string _GA02002)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                    + " GA02001, "
                    + " GA02002, "
                    + " GA02003, "
                    + " GA02997, "
                    + " GA02998  "
                    + " FROM GA02  "
                    + " WHERE GA02997 = 0 AND GA02002 = '{0}' ", _GA02002);
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据角色ID获取菜单
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectSystemMenuBySystemRoleId(string _GA02001)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                    + " GC02002 "
                    + " FROM GC02  "
                    + " WHERE GC02001 = {0} ", Convert.ToInt32(_GA02001));
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据角色ID获取权限
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectSystemPowerBySystemRoleId(string _GA02001)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                    + " GC01002 "
                    + " FROM GC01  "
                    + " WHERE GC01001 = {0} ", Convert.ToInt32(_GA02001));
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据名称获取系统角色资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectSystemRoleByIdName(string _GA02001, string _GA02002)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                    + " GA02001, "
                    + " GA02002, "
                    + " GA02003, "
                    + " GA02997, "
                    + " GA02998  "
                    + " FROM GA02  "
                    + " WHERE GA02997 = 0 AND GA02001 <> {0} AND GA02002 = '{1}' ", _GA02001, _GA02002);
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
        public DataTable SelectSystemRolePage(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"[GA02001] , [GA02002] , [GA02003] ,[GA02997] ,
                                   [GA02998] , 
                                   ROW_NUMBER() OVER(ORDER BY GA02998 DESC ) AS RowNumber";

                string strTableName = @" [dbo].[GA02] ";
                string strWhere = @" WHERE  GA02001<>20 AND GA02997 = 0 " + strWhereAdd;

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
        /// 查询所有菜单
        /// </summary>
        /// <returns></returns>
        public DataTable BindMenuByJosn()
        {
            string str = string.Empty;
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                    + " GA04001 as id , "
                    + " GA04002 as name , "
                    + " GA04003 as pId  "
                    + " FROM GA04  "
                    + " WHERE GA04997 = 0 AND GA04006 = 1 ");
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            DataTable dt = ds.Tables[0];

            return dt;
        }


        /// <summary>
        /// 绑定权限
        /// </summary>
        /// <returns></returns>
        public DataTable BindPowerByJosn()
        {
            string str = string.Empty;
            DataSet ds;
            try
            {
                object obj = null;
                string strsql = string.Format("SELECT  "
                    + " GA01001 as id , "
                    + " GA01002 as name , "
                    + " GA01003   "
                    + " FROM GA01  "
                    + " WHERE GA01997 = 0 ");
                ds = Provider.ReturnDataSetByDataAdapter(strsql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            DataTable dt = ds.Tables[0];

            return dt;
        }

        /// <summary>
        /// 新增系统角色资料
        /// </summary>
        /// <param name="_GA02">实体类</param>
        /// <returns>影响行数</returns>
        public int InsertSystemRole(SystemRole _SystemRole)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_GA02", 1, new SqlParameter[]{
                  new SqlParameter(){ParameterName=@"GA02001",Value=0, DbType=DbType.Int32},
                  new SqlParameter(){ParameterName=@"GA02002",Value=_SystemRole.GA02002, DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA02003", Value=_SystemRole.GA02003,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"GA02997", Value=0,DbType=DbType.Int32},
                  new SqlParameter(){ParameterName=@"MenuId", Value=_SystemRole.MenuId,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"PowerId", Value=_SystemRole.PowerId,DbType=DbType.String},
                  new SqlParameter(){ParameterName=@"Order", Value="GA02_Insert",DbType=DbType.String}
              });
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改系统角色资料
        /// </summary>
        /// <param name="_GA02">实体类</param>
        /// <returns>影响行数</returns>
        public int UpdateSystemRole(SystemRole _SystemRole)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_GA02", 1, new SqlParameter[]{
                   new SqlParameter(){ParameterName=@"GA02001",Value=_SystemRole.GA02001, DbType=DbType.Int32},
                   new SqlParameter(){ParameterName=@"GA02002",Value=_SystemRole.GA02002, DbType=DbType.String},
                   new SqlParameter(){ParameterName=@"GA02003", Value=_SystemRole.GA02003,DbType=DbType.String},
                   new SqlParameter(){ParameterName=@"GA02997", Value=0,DbType=DbType.Int32},
                   new SqlParameter(){ParameterName=@"MenuId", Value=_SystemRole.MenuId,DbType=DbType.String},
                   new SqlParameter(){ParameterName=@"PowerId", Value=_SystemRole.PowerId,DbType=DbType.String},
                   new SqlParameter(){ParameterName=@"Order", Value="GA02_Update",DbType=DbType.String}
              });
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除系统角色资料
        /// </summary>
        /// <param name="_GA02">实体类</param>
        /// <returns>影响行数</returns>
        public int RemoveSystemRole(SystemRole _SystemRole)
        {
            try
            {
                int num = Provider.ExecuteNonQuery("PRO_GA02", 1, new SqlParameter[]{
                new SqlParameter(){ParameterName=@"GA02001",Value=_SystemRole.GA02001, DbType=DbType.Int32},
                new SqlParameter(){ParameterName=@"GA02002",Value="", DbType=DbType.String},
                new SqlParameter(){ParameterName=@"GA02003", Value="",DbType=DbType.String},
                new SqlParameter(){ParameterName=@"GA02997", Value=_SystemRole.GA02997,DbType=DbType.Int32},
                   new SqlParameter(){ParameterName=@"MenuId", Value="",DbType=DbType.String},
                   new SqlParameter(){ParameterName=@"PowerId", Value="",DbType=DbType.String},
                new SqlParameter(){ParameterName=@"Order", Value="GA02_Remove",DbType=DbType.String}
              });
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 用户资料

        /// <summary>
        /// 根据所属区域查询销售员
        /// </summary>
        /// <param name="Area"></param>
        /// <returns></returns>
        public DataTable SelectUserBasePosition(string Area)
        {
            DataSet ds;
            try
            {

                //                string strSql = string.Format(@"
                //                            SELECT UA01001,UA01004,UA01005,UA01013 FROM UA01
                //                            WHERE  UA01009 = 1  AND UA01013 = '{0}' ", Area);
                string strSql = string.Format(@"
                            SELECT UA01001,UA01004,UA01005,UA01013 FROM UA01
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
        /// 根据所属区域查询销售员
        /// </summary>
        /// <param name="Area"></param>
        /// <returns></returns>
        public DataTable SelectUserBasePosition()
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"
                            SELECT UA01001,UA01004,UA01013 FROM UA01
                            WHERE UA01997 = 0");

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
        /// 获取全部用户资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectUserBase()
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT  [UA01001] , [UA01002] , [UA01003] , [UA01004] ,
                                                        [UA01005] , [UA01006] , [UA01007] , [UA01008] ,
                                                        [UA01009] , [UA01010] , [UA01011] , [UA01012] ,
                                                        [UA01013] , [UA01014] , [UA01015] , [UA01016] ,
                                                        [UA01017] , [UA01018] , [UA01019] , [UA01020] ,
                                                        [UA01021] , [UA01022] , [UA01023] , [UA01024] ,
                                                        [UA01997] , [UA01998] , [UA01999] , [UA01025]
                                                 FROM  [dbo].[UA01] WHERE UA01997 = 0  
                                                 ORDER BY UA01998 DESC");

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
        /// 根据条件获取全部用户资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectUserBase(string strWhere)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [UA01001] , [UA01002] , [UA01003] , [UA01004] ,
                                                       [UA01005] , [UA01006] , [UA01007] , [UA01008] ,
                                                       [UA01009] , [UA01010] , [UA01011] , [UA01012] ,
                                                       [UA01013] , [UA01014] , [UA01015] , [UA01016] ,
                                                       [UA01017] , [UA01018] , [UA01019] , [UA01020] ,
                                                       [UA01021] , [UA01022] , [UA01023] , [UA01024] ,
                                                       [UA01997] , [UA01998] , [UA01999] , [UB01002] ,
                                                       [GA02002] , [UA01025]
                                                  FROM [dbo].[UA01] 
                                                 LEFT JOIN dbo.UB01 ON dbo.UA01.UA01009 = dbo.UB01.UB01001
                                                 LEFT JOIN dbo.GA02 ON dbo.GA02.GA02001 = dbo.UA01.UA01024
                                                 WHERE UA01997 = 0 {0} 
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
        /// 根据条件获取全部用户资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectUserBaseByAera(string strWhere)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [UA01001] , [UA01002] , [UA01003] , [UA01004] ,
                                                       [UA01005] , [UA01006] , [UA01007] , [UA01008] ,
                                                       [UA01009] , [UA01010] , [UA01011] , [UA01012] ,
                                                       [UA01013] , [UA01014] , [UA01015] , [UA01016] ,
                                                       [UA01017] , [UA01018] , [UA01019] , [UA01020] ,
                                                       [UA01021] , [UA01022] , [UA01023] , [UA01024] ,
                                                       [UA01997] , [UA01998] , [UA01999] , [UA01025]
                                                  FROM [dbo].[UA01] 
                                               
                                                 WHERE UA01997 = 0 {0} 
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
        /// 根据ID获取用户资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectUserBaseByID(int UserId, string pwd)
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [UA01001] , [UA01002] , [UA01003] , [UA01004] ,
                                                       [UA01005] , [UA01006] , [UA01007] , [UA01008] ,
                                                       [UA01009] , [UA01010] , [UA01011] , [UA01012] ,
                                                       [UA01013] , [UA01014] , [UA01015] , [UA01016] ,
                                                       [UA01017] , [UA01018] , [UA01019] , [UA01020] ,
                                                       [UA01021] , [UA01022] , [UA01023] , [UA01024] ,
                                                       [UA01997] , [UA01998] , [UA01999] , [UA01025]
                                                  FROM [dbo].[UA01] 
                                                 WHERE UA01997 = 0 AND UA01001={0}AND UA01003='{1}' 
                                                 ", UserId, pwd);

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
        /// 分页获取全部用户资料数据
        /// </summary>
        /// <param name="PageSize">每页显示行数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="strWhere">Where语句（不加WHERE关键词）</param>
        /// <returns></returns>
        public DataTable SelectUserBase(int PageSize, int PageIndex, string strWhereAdd, ref object obj)
        {
            DataSet ds;
            try
            {
                string strColumn = @"[UA01001] , [UA01002] , [UA01003] , [UA01004] ,
                                     [UA01005] , [UA01006] , [UA01007] , [UA01008] ,
                                     [UA01009] , [UA01010] , [UA01011] , [UA01012] ,
                                     [UA01013] , [UA01014] , [UA01015] , [UA01016] ,
                                     [UA01017] , [UA01018] , [UA01019] , [UA01020] ,
                                     [UA01021] , [UA01022] , [UA01023] , [UA01024] ,
                                     [UA01997] , [UA01998] , [UA01999] , [UB01002] ,
                                     [GA02002] , [UA01025] ,
                                   ROW_NUMBER() OVER(ORDER BY UA01998 DESC ) AS RowNumber";

                string strTableName = @" [dbo].[UA01] 
                                         LEFT JOIN dbo.UB01 ON dbo.UA01.UA01009 = dbo.UB01.UB01001
                                         LEFT JOIN dbo.GA02 ON dbo.GA02.GA02001 = dbo.UA01.UA01024 ";

                string strWhere = @" WHERE UA01001<>19 AND UA01997 = 0 " + strWhereAdd;

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
        /// 新增用户资料数据
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int InsertUserBase(UserBase _UserBase)
        {
            try
            {

                CreateSqlHandler<UserBase> _CreateSqlHandler = new CreateSqlHandler<UserBase>();

                //生成Sql语句
                string strSql = _CreateSqlHandler.Insert(_UserBase, "UA01");

                int num = Math.Abs(Provider.ExecuteNonQuery(strSql, 0, null));

                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改用户资料数据
        /// </summary>
        /// <param name="_UA01">实体类</param>
        /// <returns>影响行数</returns>
        public int UpdateUserBase(UserBase _UserBase)
        {
            try
            {
                CreateSqlHandler<UserBase> _CreateSqlHandler = new CreateSqlHandler<UserBase>();

                //生成Sql语句
                string strSql = _CreateSqlHandler.Update(_UserBase, "UA01", string.Format(" WHERE UA01001 = '{0}' ", _UserBase.UA01001));

                int num = Math.Abs(Provider.ExecuteNonQuery(strSql, 0, null));

                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据ID删除用户资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public int RemoveUserBase(string ID)
        {
            int result = 0;
            try
            {
                string strSql = string.Format(" UPDATE UA01 SET UA01997 = 1 WHERE UA01001 = {0}", ID);
                result = Math.Abs(Provider.ExecuteNonQuery(strSql, 0, null));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 根据所属区域查询查询职位为销售的用户
        /// </summary>
        /// <param name="UA01013">所属区域</param>
        /// <returns></returns>
        public DataTable SelectUserBaseByArea(string UA01013)
        {
            DataSet ds;
            try
            {
                System.Text.StringBuilder sb = new StringBuilder();
                sb.Append("SELECT  UA01001 , UA01004, UA01005    FROM  UA01  WHERE UA01997 = 0 ORDER BY UA01005 ");
                                                          
                if (UA01013 != "全区域")
                {
                    sb.Append(string.Format(" AND UA01013='{0}'", UA01013));
                }

                object obj = null;//用于接收存储过程返回值
                ds = Provider.ReturnDataSetByDataAdapter(sb.ToString(), 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        #endregion

        #region 用户职位

        /// <summary>
        /// 获取全部用户职位资料数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectUserJob()
        {
            DataSet ds;
            try
            {

                string strSql = string.Format(@"SELECT [UB01001]
                                                      ,[UB01002]
                                                  FROM [dbo].[UB01]");
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

        #region 角色菜单

        /// <summary>
        /// 根据条件获取用户菜单权限数据
        /// </summary>
        /// <returns>结果数据</returns>
        public DataTable SelectRoleMenu(string strWhere)
        {
            DataSet ds;
            try
            {
                object obj = null;
                string strSql = string.Format(@"SELECT [GC02001] , GA1.[GA04001] , GA1.[GA04002] , GA1.[GA04003]
                                                       ,GA1.[GA04004] , GA1.[GA04005] , GA1.[GA04006] , GA1.[GA04997]
                                                       ,GA1.[GA04998] ,  GA2.[GA04001] AS FId,GA2.[GA04002] AS FName
                                                FROM dbo.GA04 GA1 JOIN ga04 GA2 ON GA1.GA04003 = GA2.GA04001 
                                                JOIN [dbo].[GC02] ON [dbo].[GC02].GC02002 = GA1.GA04001 
                                                WHERE GA1.GA04997 = 0 {0}", strWhere);

                ds = Provider.ReturnDataSetByDataAdapter(strSql, 0, ref obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds.Tables[0];
        }

        #endregion

        #region 修改密码

        /// <summary>
        /// 根据条件获取用户菜单权限数据
        /// </summary>
        /// <returns>结果数据</returns>
        public int UpdatePwd(UserBase _UserBase)
        {
            try
            {
                CreateSqlHandler<UserBase> _CreateSqlHandler = new CreateSqlHandler<UserBase>();

                //生成Sql语句
                string strSql = _CreateSqlHandler.Update(_UserBase, "UA01", string.Format(" WHERE UA01001 = '{0}' ", _UserBase.UA01001));

                int num = Math.Abs(Provider.ExecuteNonQuery(strSql, 0, null));

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
