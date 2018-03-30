using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading; 

namespace Sinoo.Common
{
	/// <summary>
	/// 导入数据验证帮助类
	/// </summary>
	public class ImportValidate
	{ 
		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="connString">连接字符串</param>
		public ImportValidate(string connString)
		{
			this.connectionString = connString;
		}

		/// <summary>
		/// 数据库连接字符串
		/// </summary>
		private string connectionString;
	
        Dictionary<string, string> dicTempTablesName = new Dictionary<string, string>();
		/// <summary>
		/// 将需要验证的数据写入到数据库
		/// 同时返回写入到数据库的所有临时表的表名
		/// </summary>
		/// <param name="dicValidate">需要写入的数据源</param>
		/// <param name="strErrorInfo">错误信息</param>
		/// <returns>所有临时表的表名</returns>
		internal bool SqlBulkCopyInsertDataForValidate(Dictionary<string, TableValidate> dicValidate)
		{
            dicTempTablesName.Clear();
			bool isSuccess = true;
			string tempTableName = string.Empty;
			List<string> sqlStringList = new List<string>();
			string strTemp = string.Empty;
			//定义表名
            Random rdm = new Random(99);
			for (int i = 0; i < dicValidate.Count; i++)
			{
                Thread.Sleep(1);
				tempTableName = String.Format("TempImport{0}{1}{2}{3}{4}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond,rdm.Next(999));
				strTemp = string.Format(@"USE tempdb if object_id(N'{0}',N'U') is not null begin drop TABLE {0} end ", tempTableName);
				sqlStringList.Add(strTemp);
				strTemp = string.Empty;
				TableValidate entity = dicValidate.Values.ElementAt(i);
				//DataTable dt = entity.SourceTable;
				//foreach (DataColumn c in dt.Columns)
				//{
				if (!string.IsNullOrEmpty(entity.PK))
				{
					strTemp = string.Format(", [{0}] varchar(8000)", entity.PK);
				}
				if (!string.IsNullOrEmpty(entity.FK))
				{
					strTemp += string.Format(", [{0}] varchar(8000)", entity.FK);
				}
				//}
                if (!string.IsNullOrEmpty(strTemp))
                {
                    strTemp = strTemp.Remove(0, 2);
                }
				strTemp = string.Format("USE tempdb  CREATE TABLE {0}  ([Id] [int] IDENTITY(2,1) NOT NULL,{1}) ", tempTableName, strTemp);
				sqlStringList.Add(strTemp);
				dicTempTablesName.Add(dicValidate.Keys.ElementAt(i), tempTableName);
			}
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				ExecuteSqlList(sqlStringList, sqlConnection);//批量执行SQL语句
				SqlTransaction trans = sqlConnection.BeginTransaction();
				using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, trans))
				{
					try
					{
						for (int i = 0; i < dicValidate.Count; i++)
						{
                            sqlBulkCopy.ColumnMappings.Clear();
							sqlBulkCopy.DestinationTableName = dicTempTablesName.Values.ElementAt(i);
							TableValidate mapEntity = dicValidate.Values.ElementAt(i);
							if (!string.IsNullOrEmpty(mapEntity.PK) || !string.IsNullOrEmpty(mapEntity.FK))
							{
								if (!string.IsNullOrEmpty(mapEntity.PK))
								{
									sqlBulkCopy.ColumnMappings.Add(mapEntity.PK, mapEntity.PK);
								}
								if (!string.IsNullOrEmpty(mapEntity.FK))
								{
									sqlBulkCopy.ColumnMappings.Add(mapEntity.FK, mapEntity.FK);
								}
								sqlBulkCopy.BatchSize = dicValidate.Values.ElementAt(i).SourceTable.Rows.Count;
								if (dicValidate.Values.ElementAt(i).SourceTable != null && dicValidate.Values.ElementAt(i).SourceTable.Rows.Count != 0)
								{
									sqlBulkCopy.WriteToServer(dicValidate.Values.ElementAt(i).SourceTable);
								}
							}
						}
						trans.Commit();
					}
					catch (SqlException ex)
					{
						trans.Rollback();
						sqlBulkCopy.Close();
						isSuccess = false; 
					}
					finally
					{
						sqlBulkCopy.Close();
						sqlConnection.Close();
					}
				}
			}
			return isSuccess;
		}

        StringBuilder deleteSql = new StringBuilder();


		/// <summary>
		/// 批量写入数据到数据库
		/// 【新方法】
		/// </summary>
		/// <param name="dtInsert">要写入的数据表</param>
		/// <param name="xmlDic">导入表的XML配置文件数据字典</param>	
		public bool InsertData(DataSet dsInsert, Dictionary<string, XmlHandler> xmlDic)
		{
			bool flag = true;
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				SqlTransaction trans = sqlConnection.BeginTransaction();
				try
				{
                    //如果删除语句不为空则先执行删除语句
                    if (deleteSql.Length > 0)
                    {
                        SqlCommand cmd = new SqlCommand() {Connection=sqlConnection,CommandText=deleteSql.ToString(),Transaction=trans };
                        cmd.ExecuteNonQuery();
                        this.DeleteTables(this.dicTempTablesName);
                    }
                    //将数据插入到数据库中
					using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, trans))
					{
						try
						{
							string tableName = string.Empty;
							for (int j = 0; j < dsInsert.Tables.Count; j++)
							{
								sqlBulkCopy.ColumnMappings.Clear();
								DataTable dtXml = new DataTable();
								for (int i = 0; i < xmlDic.Count; i++)
								{
									if (xmlDic.Keys.ElementAt(i) == dsInsert.Tables[j].TableName)
									{
										tableName = xmlDic.Values.ElementAt(i).GetRootNodeAttr("tableName");
										dtXml = xmlDic.Values.ElementAt(i).GetAllData();
										break;
									}
								}
								sqlBulkCopy.DestinationTableName = tableName;
								foreach (DataColumn c in dsInsert.Tables[j].Columns)
								{
                                    DataRow[] drs = dtXml.Select(string.Format("cn='{0}'", c.ColumnName));
                                    if (drs != null && drs.Length > 0)
                                    {
                                        sqlBulkCopy.ColumnMappings.Add(c.ColumnName, drs[0]["InnerText"].ToString());
                                    }                                 
								}
								sqlBulkCopy.BatchSize = dsInsert.Tables[j].Rows.Count;
								if (dsInsert.Tables[j] != null && dsInsert.Tables[j].Rows.Count != 0)
								{
									sqlBulkCopy.WriteToServer(dsInsert.Tables[j]);
								}

							}
						}
						catch (SqlException ex)
						{
							flag = false;
							sqlBulkCopy.Close();
							throw new Exception(ex.Message);
						}
						trans.Commit();
					}
				}
				catch (Exception ex)
				{
					trans.Rollback();
					sqlConnection.Close();
					throw ex;
				}
			}
			return flag;
		}
		 
		/// <summary>
		/// 验证数据看的表数据有效性
		/// </summary>
		/// <param name="sourceList">验证的表数据集合</param>
		/// <param name="dicTempTablesName">所有表与临时表键值对的集合</param>
		/// <param name="mainTableName">主表名</param>
		/// <returns></returns>
		internal DataSet DatabaseDataValidate(Dictionary<string, TableValidate> sourceList, string mainTableName, bool isDeleteExists = false)
		{
            deleteSql.Clear();
			DataSet dsError = new DataSet();
			Dictionary<string, string> sqlStringList = new Dictionary<string, string>();
			//临时表中的主表		
			string strTemp = string.Empty;
			StringBuilder strSql = new StringBuilder();
			string tempMainTableName = dicTempTablesName[mainTableName];
			TableValidate entityMain = sourceList[mainTableName];
            strSql.Append(" IF(object_id('tempdb..#Temp') is not null) DROP TABLE #Temp  CREATE table #Temp(	[数据表名] varchar(200),[验证方式] varchar(200),[行号] varchar(200),[错误原因] varchar(200),[主键] varchar(200))");
			sqlStringList.Add("create", strSql.ToString());
			if (!string.IsNullOrEmpty(entityMain.PK))
			{
				strSql.Clear();
				//主表sheet中主键为空
                strSql.AppendFormat(" INSERT into #Temp  SELECT  '{2}' 表名,'主键为空验证' 验证方式,a.Id 行号,'[{1}]不能为空' 错误原因,{1} 主键 from tempdb..{0} a where isnull({1}, '')='' ", tempMainTableName, entityMain.PK, mainTableName);
				//主表sheet的主键重复
				strSql.AppendFormat(" INSERT into #Temp  SELECT  '{2}' 表名,'主键重复验证' 验证方式,a.Id 行号,'[{1}]不能重复' 错误原因,{1} 主键 from tempdb..{0} a where EXISTS(SELECT TOP 1 1 from tempdb..{0} where  id<>a.id AND {1} = a.{1})", tempMainTableName, entityMain.PK, mainTableName);
                if (isDeleteExists)
                {
                    deleteSql.AppendFormat(" delete a from [{1}] a where EXISTS(SELECT TOP 1 1 from  tempdb..{0} where  {2} = a.{3} )", tempMainTableName, entityMain.DbTableName, entityMain.PK, entityMain.DbPK, mainTableName);
                }
                else
                {
                    //根据主键在数据库中验证是否已经存在
                    strSql.AppendFormat(" INSERT into #Temp SELECT  '{4}' 表名,'已存在数据验证' 验证方式,a.Id 行号,'[{2}]在数据库中已存在' 错误原因,{2} 主键 from tempdb..{0} a where EXISTS(SELECT TOP 1 1 from {1} where  a.{2} = {3} )", tempMainTableName, entityMain.DbTableName, entityMain.PK, entityMain.DbPK, mainTableName);
                }
				strSql.Append(" SELECT * FROM #Temp  ORDER BY [行号]");
				strSql.Append(" TRUNCATE TABLE #Temp");
				sqlStringList.Add(mainTableName, strSql.ToString());
			}
			if (dicTempTablesName.Count > 1)
			{
				for (int i = 0; i < dicTempTablesName.Count; i++)
				{
                    if (dicTempTablesName.Keys.ElementAt(i)==mainTableName)
                    {
                        continue;
                    }
					strSql.Clear();
					string sourceTableName = dicTempTablesName.Keys.ElementAt(i);
					string tempTableName = dicTempTablesName.Values.ElementAt(i);
					TableValidate entity = sourceList[sourceTableName];
					//主键不为空的时候
					if (!string.IsNullOrEmpty(entity.PK))
					{
					}
					//外键FK不为空的时候
					if (!string.IsNullOrEmpty(entity.FK))
                    {
                        if (isDeleteExists)
                        {
                            deleteSql.Insert(0, string.Format(" delete a from [{1}] a where EXISTS(SELECT TOP 1 1 from tempdb..{0}  where {2} = a.{3} )", tempMainTableName, entity.DbTableName, entityMain.PK, entity.DbFK));
                        }
						//子表中的fk在主表中不存在
                        strSql.AppendFormat(" INSERT into #Temp SELECT  '{4}' 表名,'数据关系验证' 验证方式,a.Id 行号,'[{2}]在主表中不存在' 错误原因,{3} 主键  from tempdb..{0} a where not EXISTS(SELECT TOP 1 1 from tempdb..{1} where a.{2} = {3}  )", tempTableName, tempMainTableName, entity.FK, entityMain.PK, sourceTableName);
					}
					//pk和fk都不为空
					if (!string.IsNullOrEmpty(entity.FK) && !string.IsNullOrEmpty(entity.PK))
                    {
                        //子表sheet中pk主键为空
                        strSql.AppendFormat(" INSERT into #Temp  SELECT  '{2}' 表名,'主键为空验证' 验证方式,a.Id 行号,'[{1}和{3}]不能为空' 错误原因,{1} 主键 from tempdb..{0} a where isnull({1}, '')='' or isnull({3}, '')='' ", tempTableName, entity.PK, sourceTableName, entity.FK);
                        //子表sheet的主键重复
                        strSql.AppendFormat(" INSERT into #Temp  SELECT   '{2}' 表名,'主键重复验证' 验证方式,a.Id 行号,'[{1}]+[{3}]不能重复' 错误原因,{1} 主键  from tempdb..{0} a where EXISTS(SELECT TOP 1 1 from tempdb..{0} where  id<>a.id AND {1} = a.{1} and {3} = a.{3})", tempTableName, entity.PK, sourceTableName, entity.FK);
                        if (!isDeleteExists)                       
                        {
                            //根据pk+fk在数据库中验证是否已经存在
                            strSql.AppendFormat(" INSERT into #Temp SELECT '{6}' 表名,'已存在数据验证' 验证方式,a.Id 行号,'[{2}+{4}]在数据库中已存在' 错误原因,{2}+'+'+{4} 主键 from tempdb..{0} a where EXISTS(SELECT TOP 1 1 from {1} where a.{2} = {3} and a.{4} = {5} )", tempTableName, entity.DbTableName, entity.FK, entity.DbFK, entity.PK, entity.DbPK, sourceTableName);
                        }
					}
					strSql.Append(" select * FROM #Temp  order BY [行号]");
					strSql.Append(" TRUNCATE TABLE #Temp");
					sqlStringList.Add(sourceTableName, strSql.ToString());
				}
			}
			
			sqlStringList.Add("end", "DROP TABLE #Temp");
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				try
				{
					for (int n = 0; n < sqlStringList.Count; n++)
                    {
						string strsql = sqlStringList.Values.ElementAt(n);
						if (strsql.Trim().Length > 1)
						{
							if (sqlConnection.State != ConnectionState.Open)
							{
								sqlConnection.Open();
							}
							SqlCommand cmd = new SqlCommand() { Connection = sqlConnection, CommandText = strsql };
							if (n == 0||n==sqlStringList.Count-1)
							{
								cmd.ExecuteNonQuery();
								//sqlConnection.Close();
							}
							else
							{
								using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
								{
                                    DataSet ds = new DataSet();
									adapter.Fill(ds);
									if (ds.Tables[0] != null)
									{
										DataTable dt = ds.Tables[0].Copy();
										if (dt.Rows.Count>0)
										{
											dt.TableName = sqlStringList.Keys.ElementAt(n);
											dsError.Tables.Add(dt);
										}										
									}
								}
								//sqlConnection.Close();
							}
						}
					}
				}
				catch (Exception ex)
				{
					SqlCommand cmd = new SqlCommand() { Connection = sqlConnection, CommandText = sqlStringList.Values.ElementAt(sqlStringList.Count - 1) };
					cmd.ExecuteNonQuery();
					sqlConnection.Close();
					sqlConnection.Dispose();
                    throw ex;
				}
				//finally
				//{
				//    using (SqlConnection conn = new SqlConnection(connectionString))
				//    {
				//        conn.Open();
				//        try
				//        {
				//            SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = sqlStringList.Values.ElementAt(sqlStringList.Count - 1) };
				//            cmd.ExecuteNonQuery();
				//            conn.Close();
				//        }
				//        catch
				//        {
				//            conn.Close();
				//            conn.Dispose();
				//        }						
				//    }
				//}
			}
			return dsError;
		}

		/// <summary>
		/// 删除数据库中临时表
		/// </summary>
		/// <param name="tempTablesName">临时表集合</param>
		/// <returns>执行结果</returns>
		internal bool DeleteTables(Dictionary<string, string> tempTablesName)
		{
			bool flag = true;
			List<string> sqlStringList = new List<string>();

			for (int i = 0; i < tempTablesName.Count; i++)
			{
				string strTemp = string.Empty;
				strTemp = string.Format(@"USE tempdb if object_id(N'{0}',N'U') is not null begin drop TABLE {0} end ", tempTablesName.Values.ElementAt(i));
				sqlStringList.Add(strTemp);
			}
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				try
				{
					ExecuteSqlList(sqlStringList, sqlConnection);//批量执行SQL语句，带事务	
				}
				catch (Exception)
				{
					flag = false;
				}
			}
			return flag;
		}

		/// <summary>
		/// 执行多条SQL语句,此方法中没有事务,如需事务请在调用的时候编写
		/// </summary>
		/// <param name="sqlStringList">批量执行的SQL语句</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>受影响的行数</returns>
		internal int ExecuteSqlList(List<String> sqlStringList, SqlConnection conn)
		{
			SqlCommand cmd = new SqlCommand() { Connection = conn };
			//SqlTransaction tx = conn.BeginTransaction();
			//cmd.Transaction = tx;
			try
			{
				int count = 0;
				for (int n = 0; n < sqlStringList.Count; n++)
				{
					string strsql = sqlStringList[n];
					if (strsql.Trim().Length > 1)
					{
						cmd.CommandText = strsql;
						count += cmd.ExecuteNonQuery();
					}
				}
				//tx.Commit();
				return count;
			}
			catch
			{
				//tx.Rollback();
				return 0;
			}
		}
                
		 
	}

	
}
