using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sinoo.DAL
{
    /// <summary>
    /// SqlServer数据提供者
    /// </summary>
    public class SqlServerProvider : IDisposable
    {
        #region IDisposable 成员

        /// <summary>
        /// 回收垃圾 
        /// </summary>
        public void Dispose()
        {
            GC.Collect();//垃圾回收
        }

        #endregion

        #region 定义私有参数或保护参数

        /// <summary>
        /// 定义返回参数
        /// </summary>
        private readonly string ReturnValue = @"output";

        /// <summary>
        /// 定义连接字符串键值对
        /// </summary>
        private readonly string ConString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

        /// <summary>
        /// 定义链接字符串
        /// </summary>
        protected SqlConnection con;

        /// <summary>
        /// 数据库名称
        /// </summary>
        private string DataBaseName = String.Empty;

        #endregion

        #region 定义构造函数

        /// <summary>
        /// 构造函数实例化DBheler类库(不带参数)
        /// </summary>
        public SqlServerProvider()
        {
            con = new SqlConnection(ConString);
        }

        /// <summary>
        /// 构造函数实例化DBheler类库(带参数)
        /// </summary>
        public SqlServerProvider(string DBKey)
        {
            ConString = ConfigurationManager.ConnectionStrings[DBKey].ConnectionString;
            con = new SqlConnection(ConString);
        }

        #endregion

        #region 定义析构函数

        /// <summary>
        /// 析构函数用于回收资源
        /// </summary>
        ~SqlServerProvider()
        {
            try
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            catch
            {
                GC.Collect();//强制进行垃圾回收
            }
        }

        #endregion

        #region 定义公共方法

        /// <summary>
        /// 打开方法
        /// </summary>
        private void Open()
        {
            if (con == null)
            {
                con = new SqlConnection(ConString);
            }
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            if (DataBaseName != String.Empty) //动态换库功能
            {
                con.ChangeDatabase(DataBaseName);////转换数据库名称
            }
        }

        /// <summary>
        /// 关闭方法
        /// </summary>
        private void Close()
        {
            if (con != null)
            {
                if (con.State.Equals(ConnectionState.Open))
                    con.Close();
            }
            if (DataBaseName != String.Empty) //动态换库功能取消
            {
                DataBaseName = String.Empty;
            }
        }

        /// <summary>
        /// 创建SqlDataAdapter
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回SqlDataAdapter</returns>
        private SqlDataAdapter CreateDataAdapter(string ProName, SqlParameter[] Param, int Type)
        {
            //打开数据链接
            Open();
            //定义适配器
            SqlDataAdapter da = new SqlDataAdapter(ProName, con);
            //判断用存储过程还是sql命令
            if (Type == 1)
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                da.SelectCommand.CommandType = CommandType.Text;
            }
            //相适配器中添加参数
            if (Param != null)
            {
                foreach (SqlParameter p in Param)
                {
                    da.SelectCommand.Parameters.Add(p);
                }
            }
            //添加返回参数
            da.SelectCommand.Parameters.Add(new SqlParameter(ReturnValue, SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null));
            return da;
        }

        /// <summary>
        /// 定义sqlCommand对象
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回sqlCommand对象</returns>
        private SqlCommand CreateSqlCommand(string ProName, SqlParameter[] param, int Type)
        {
            //打开字符串
            Open();
            //定义命令对象
            SqlCommand cmd = new SqlCommand(ProName, con);
            if (Type == 1)
            { cmd.CommandType = CommandType.StoredProcedure; }
            else
            { cmd.CommandType = CommandType.Text; }
            //添加参数
            if (param != null)
            {
                foreach (SqlParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
            }
            //添加返回参数
            cmd.Parameters.Add(new SqlParameter(ReturnValue, SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null));
            return cmd;
        }

        /// <summary>
        /// 定义DataReader
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回DataReader</returns>
        private SqlDataReader CreateDataReader(string ProName, SqlParameter[] param, int Type, ref object retValue)
        {
            //打开链接
            Open();
            SqlCommand cmd = CreateSqlCommand(ProName, param, Type);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            retValue = cmd.Parameters[ReturnValue].Value;
            return dr;
        }

        #endregion

        #region 方法转换类

        /// <summary>
        /// DataSet和DataReader转换
        /// </summary>
        /// <param name="DataReader">读取数据</param>
        /// <returns>返回数据集</returns>
        public DataSet ReaderTable(SqlDataReader DataReader)
        {
            if (DataReader == null) return null;
            if (DataReader.IsClosed) return null;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //循环dataReader/动态添加表的数据列
            for (int i = 0; i < DataReader.FieldCount; i++)
            {
                DataColumn dc = new DataColumn();
                dc.DataType = DataReader.GetFieldType(i);
                dc.ColumnName = DataReader.GetName(i);
                dt.Columns.Add(dc);
            }
            //向表里面添加数据
            while (DataReader.Read())
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < DataReader.FieldCount; j++)
                {
                    dr[j] = DataReader[j].ToString();
                }
                dt.Rows.Add(dr);
            }
            //关闭dataReader读取器
            DataReader.Close();
            ds.Tables.Add(dt);
            Close();
            return ds;
        }

        #endregion

        #region 数据通讯方法

        /// <summary>
        /// 切换数据库名称
        /// </summary>
        /// <param name="BaseName">数据库名称</param>
        /// <returns>成功返回true失败返回false</returns>
        public bool ChangeDataBase(string BaseName)
        {
            bool b = false;
            try
            {
                if (con != null)
                {
                    Open();
                    con.ChangeDatabase(BaseName); //转换数据库名称
                    b = true;
                    Close();
                }
            }
            catch
            {
                b = false;
            }
            return b;
        }

        /// <summary>
        /// 受影响行数
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQuery(string proName, int Type,params SqlParameter[] Param)
        {
            //打开数据库
            Open();
            int num = 0;
            //命令对象
            SqlCommand cmd = CreateSqlCommand(proName, Param, Type);
            try
            {
                num = cmd.ExecuteNonQuery();//返回收影响行数
                cmd.Dispose();
            }
            catch
            {
                GC.Collect();//垃圾回收
            }
            //关闭数据库
            Close();
            return num;
        }

        /// <summary>
        /// 执行多条sql语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns>受影响行数</returns>
        public int TranExecuteNonQuerys(List<string> Sqls,List<SqlParameter[]> Prams)
        {
            using (con)
            {
                int cnt = 0;
                Open();
                SqlCommand cmd = new SqlCommand(); //定义命令对象
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                SqlTransaction sqlaTran = con.BeginTransaction(); //定义事务
                cmd.Transaction = sqlaTran;
                try
                {
                    for (int i = 0; i < Sqls.Count; i++)
                    {
                        string ExcuSql = Sqls[i].Trim();

                        if (ExcuSql == null || ExcuSql.Length <= 0) continue;
                        cmd.CommandText = ExcuSql; //执行的sql语句
                        if (Prams != null)//如果不等于空循环
                        {
                            if (Sqls.Count.Equals(Prams.Count))
                            {
                                cmd.Parameters.Clear();
                                foreach (SqlParameter item in Prams[i])
                                {
                                    if (item != null)
                                    {
                                        cmd.Parameters.Add(item);
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("参数对象的个数和sql语句的个数不相同！");
                            }
                        }
                        cnt += cmd.ExecuteNonQuery();
                    }
                    sqlaTran.Commit();
                    return cnt;
                }
                catch
                {
                    sqlaTran.Rollback();
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    Close();
                }

            }
        }


        /// <summary>
        /// 最后第一行第一列的值
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回最后第一行第一列的值</returns>
        public object ExecuteScalar(string proName, int Type,params SqlParameter[] Param)
        {
            //打开数据库
            Open();
            //定义命令对象
            SqlCommand cmd = CreateSqlCommand(proName, Param, Type);
            //返回对象
            object obj = null;
            try
            {
                obj = cmd.ExecuteScalar();
                cmd.Dispose();
            }
            catch
            {
                GC.Collect();
            }
            Close();
            return obj;
        }

        /// <summary>
        /// 最后第一行第一列的值
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回最后第一行第一列的值</returns>
        public object TranExecuteScalar(List<string> Sqls, List<SqlParameter[]> Prams)
        {
            using (con)
            {
                object cnt = 0;
                Open();
                SqlCommand cmd = new SqlCommand(); //定义命令对象
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                SqlTransaction sqlaTran = con.BeginTransaction(); //定义事务
                cmd.Transaction = sqlaTran;
                try
                {
                    for (int i = 0; i < Sqls.Count; i++)
                    {
                        string ExcuSql = Sqls[i].Trim();
                        if (ExcuSql == null || ExcuSql.Length <= 0) continue;
                        cmd.CommandText = ExcuSql; //执行的sql语句
                        if (Prams != null)
                        {
                            if (Sqls.Count.Equals(Prams.Count))
                            {
                                cmd.Parameters.Clear();
                                foreach (SqlParameter item in Prams[i])
                                {
                                    if (item != null)
                                    {
                                        cmd.Parameters.Add(item);
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("参数对象的个数和sql语句的个数不相同！");
                            }
                        }
                        cnt = cmd.ExecuteScalar();
                    }
                    sqlaTran.Commit();
                    return cnt;
                }
                catch
                {
                    sqlaTran.Rollback();
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    Close();
                }

            }
        }

        /// <summary>
        /// 返回DataSet通过DataAdapter
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="tableName">表名</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <param name="retValue">返回值(外部必须赋值,内部不一定赋值)—一般返回查询表的总行数</param>
        /// <returns>返回DataSet</returns>
        public DataSet ReturnDataSetByDataAdapter(string proName, string tableName, int Type, ref object retValue,params SqlParameter[] Param)
        {
            //打开数据库
            Open();
            //定义Command命令
            SqlCommand cmd = CreateSqlCommand(proName, Param, Type);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, tableName);
            retValue = cmd.Parameters[ReturnValue].Value;
            Close();
            return ds;
        }

        /// <summary>
        /// 返回DataSet通过DataAdapter
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <param name="retValue">返回值(外部必须赋值,内部不一定赋值)—一般返回查询表的总行数</param>
        /// <returns>返回DataSet</returns>
        public DataSet ReturnDataSetByDataAdapter(string proName, int Type, ref object retValue,params SqlParameter[] Param)
        {
            //打开数据库
            Open();
            //定义Command命令
            SqlCommand cmd = CreateSqlCommand(proName, Param, Type);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            retValue = cmd.Parameters[ReturnValue].Value;
            Close();
            return ds;
        }

        /// <summary>
        /// 通过DataReader返回数据集
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>通过DataReader返回数据集</returns>
        public DataSet ReturnDataSetByDataReader(string ProName, int Type, ref object obj,params SqlParameter[] param)
        {
            SqlDataReader ReaderResult = CreateDataReader(ProName, param, Type, ref obj);
            return ReaderTable(ReaderResult);
        }

        /// <summary>
        /// 分页重载函数，返回当前页
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="TableName">表名</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageSize">页面大小</param>
        /// <returns>分页重载函数，返回当前页</returns>
        public DataSet PageDataSet(string ProName, string TableName, int Type, int CurrentPage, int PageSize,params SqlParameter[] Param)
        {
            //打开数据库
            Open();
            //定义命令
            SqlCommand cmd = CreateSqlCommand(ProName, Param, Type);
            DataSet ds = new DataSet();//实例化DataSet
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, CurrentPage, PageSize, TableName);
            Close();
            return ds;
        }

        #endregion

        #region 序列化的表

        /// <summary>
        /// 返回序列化的DataSet
        /// </summary>
        /// <param name="proName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="tableName">表名</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回序列化的DataSet</returns>
        public object ReturnDataSetBySerializer(string proName, int Type, ref object retValue,params SqlParameter[] Param)
        {
            DataSet ds = ReturnDataSetByDataAdapter(proName, Type, ref retValue, Param);
            IFormatter formatter = new BinaryFormatter();//定义BinaryFormatter以序列化object对象  

            MemoryStream ms = new MemoryStream();//创建内存流对象  

            formatter.Serialize(ms, ds);//把object对象序列化到内存流  

            byte[] buffer = ms.ToArray();//把内存流对象写入字节数组  

            ms.Close();//关闭内存流对象  

            ms.Dispose();//释放资源  

            MemoryStream msNew = new MemoryStream();

            GZipStream gzipStream = new GZipStream(msNew, CompressionMode.Compress, true);//创建压缩对象  

            gzipStream.Write(buffer, 0, buffer.Length);//把压缩后的数据写入文件  

            gzipStream.Close();//关闭压缩流,这里要注意：一定要关闭，要不然解压缩的时候会出现小于4K的文件读取不到数据，大于4K的文件读取不完整              

            gzipStream.Dispose();//释放对象  

            msNew.Close();

            msNew.Dispose();

            return msNew.ToArray();

        }

        #endregion
    }
}
