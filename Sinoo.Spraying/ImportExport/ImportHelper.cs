using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
using NPOI;
using NPOI.SS.UserModel;
//using NPOI.HSSF.UserModel;
//using NPOI.HPSF;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data.SqlClient;
using Sinoo.Common;

namespace Sinoo.Spraying
{

    /// <summary>
    /// 导入数据帮助类
    /// <remarks>用于常用的数据导入，从文件导入到数据库。包括：EXCEL 、ACCESS导入；数据库目前只是用SQL SERVER</remarks>
    /// </summary>
    public class ImportHelper
	{
		/// <summary>
		/// 获取到的所有数据
		/// </summary>
		public DataSet AllData 
		{
			get { return _allData;}
            set { _allData = value; }//此处修改，增加set
		}
		/// <summary>
		/// 导入时验证未通过的错误信息
		/// </summary>
		public DataTable ErrorInfo 
		{
			get { return _errorInfo; }	
		}
		/// <summary>
		/// 验证通过的数据
		/// 2013.11.27 by sang: 暂时未用到, 预留以后扩展(如客户想只导入正确的数据忽略错误的数据时)
		/// </summary>
		public DataSet ValidatedData
		{
			get { return _validatedData; }	
		}
		/// <summary>
		/// 验证错误的数据
		/// 2013.11.27 by sang: 暂时未用到,备用.
		/// </summary>
		//public DataSet ErrorData;

		#region 构造函数和内部对象
		/// <summary>
		/// 数据验证类,数据库操作相关方法都在此类中
		/// </summary>
		private ImportValidate _importValidate;
		/// <summary>
		/// 正则验证配置表
		/// </summary>
		private DataTable _dtRegexConfig;
		/// 存储表名和表相关信息
		/// <中文表名，表实体类>
		/// </summary>
		private Dictionary<string, TableValidate> _dicValidate = new Dictionary<string, TableValidate>();
		/// <summary>
		/// xml集合
		/// </summary>
		private Dictionary<string, XmlTwoHandler> _dicXmls = new Dictionary<string, XmlTwoHandler>();
		/// <summary>
		/// 获取到的全部数据
		/// </summary>
		/// <summary>
		private DataSet _allData = new DataSet();
		/// <summary>
		/// 验证通过的数据
		/// </summary>
		private DataSet _validatedData = new DataSet();
		/// <summary>
		/// 错误信息
		/// </summary>
		private DataTable _errorInfo = new DataTable();


        /// <summary>
        /// 构造函数
        /// <param name="connectionString">连接数据库字符串</param>
        /// </summary>
        public ImportHelper(string connectionString)
        {
            this._importValidate = new ImportValidate(connectionString);
            XmlTwoHandler xmlRegex = new XmlTwoHandler("RegexConfig.xml");//读取正则配置文件
            if (xmlRegex != null)
            {
                _dtRegexConfig = xmlRegex.GetAllData();
			}
			#region 1、存储错误信息的表结构
			this._errorInfo = new DataTable();
			this._errorInfo.Columns.Add("数据表名", typeof(System.String));
			this._errorInfo.Columns.Add("验证方式", typeof(System.String));
			this._errorInfo.Columns.Add("行号", typeof(System.String));
			this._errorInfo.Columns.Add("错误原因", typeof(System.String));
			this._errorInfo.Columns.Add("主键", typeof(System.String));
			#endregion
        }
		#endregion

		#region 获取Excel中的数据到DataSet中
		/// <summary>
		/// 获取EXCEL表数据
		/// 返回为datatable
		/// </summary>
		/// <param name="stream">文件流</param>
		/// <returns>Excel中的所有数据</returns>
		public DataSet GetExcel(Stream stream)
		{
			this._allData.Clear();
			IWorkbook workbook;
			if (stream.Length > 0)
			{
				workbook = WorkbookFactory.Create(stream);
				stream.Close();
				for (int i = 0; i < workbook.NumberOfSheets; i++)
				{
					ISheet sheet = workbook.GetSheetAt(i);
					DataTable dt = new DataTable();
					dt.TableName = sheet.SheetName;
					IRow excelColumnRow = sheet.GetRow(0);
                    if (excelColumnRow != null)
                    {
						//获取列头, 现仅支持第一行作为列头
                        for (int k = 0; k < excelColumnRow.LastCellNum; k++)
                        {
                            DataColumn col;
                            ICell cell = excelColumnRow.GetCell(k);
                            object v = GetValueFromICell(cell);
                            if (null != v && DBNull.Value != v)
                            {
                                col = new DataColumn(v.ToString(), typeof(string));
                            }
                            else
                            {
                                //DataRow dr = this._errorInfo.NewRow();
                                //dr[0] = sheet.SheetName;
                                //dr[1] = "列头验证";
                                //dr[2] = i + 1;
                                //dr[3] = String.Format("工作表【{0}】第【{1}】行,列名为空", sheet.SheetName, i + 1);
                                //this._errorInfo.Rows.Add(dr);
                                col = new DataColumn("col" + k.ToString(), typeof(string));
                            }
							//如果列头已存在就返回错误信息
							if (!dt.Columns.Contains(col.ColumnName))
							{
								dt.Columns.Add(col);
							}
							else
							{
								DataRow dr = this._errorInfo.NewRow();
								dr[0] = sheet.SheetName;
								dr[1] = "列头验证";
								dr[2] = 1;
								dr[3] = String.Format("工作表【{0}】第【{1}】列列头重复", sheet.SheetName, k);
								this._errorInfo.Rows.Add(dr);
								return null;

							}
                        }
                        IRow iRow; DataRow row;
                        //获取行数据,从第2行开始获取
                        for (int j = 1; j <= sheet.LastRowNum; j++)
                        {
                            row = dt.NewRow();
                            iRow = sheet.GetRow(j);
                            ICell cell;
                            if (iRow != null && iRow.Cells.Count == excelColumnRow.LastCellNum)
                            {
                                bool isHasValue = false;
                                for (int f = 0; f < iRow.LastCellNum; f++)
                                {
                                    cell = iRow.GetCell(f);
                                    object v = GetValueFromICell(cell);
									if (null != v && DBNull.Value != v)
									{
										row[f] = v.ToString().Trim() ;
                                        isHasValue = true;
									}
									else
									{
										row[f] = DBNull.Value;
									}
                                }
                                if (isHasValue)
                                {
                                    dt.Rows.Add(row);
                                }
                                
                            }
                            else
                            {
                                DataRow dr = this._errorInfo.NewRow();
                                dr[0] = sheet.SheetName;
                                dr[1] = "数据内容验证";
                                dr[2] = j+1;
                                dr[3] = String.Format("工作表【{0}】第【{1}】行数据内容错误", sheet.SheetName, j);
                                this._errorInfo.Rows.Add(dr);
                                return null;
                            }
                        }
                        this._allData.Tables.Add(dt);
                    }
                    else
                    {
						//DataRow dr = this._errorInfo.NewRow();
						//dr[0] = sheet.SheetName;
						//dr[1] = "列头验证";
						//dr[2] = 1;
						//dr[3] = String.Format("获取工作表【{0}】列头错误", sheet.SheetName);
						//this._errorInfo.Rows.Add(dr);
						//this._allData = null;
						//return null;
                    }
				}
			}
			return this._allData;
		}

		/// <summary>
		/// 根据ICell获取单元格中的数值
		/// </summary>
		/// <param name="cell">ICell</param>
		/// <returns>object</returns>
		private object GetValueFromICell(ICell cell)
		{
			object value = DBNull.Value;
			if (cell != null)
			{
				switch (cell.CellType)
				{
					case CellType.BLANK:
						value = DBNull.Value;
						break;
					case CellType.BOOLEAN:
						value = cell.BooleanCellValue;
						break;
					case CellType.NUMERIC:
						value = cell.NumericCellValue;
						if (DateUtil.IsCellDateFormatted(cell))
							value = DateUtil.GetJavaDate((double)value);
						break;
					case CellType.STRING:
						if (!string.IsNullOrEmpty(cell.StringCellValue))
						{
							value = cell.StringCellValue;
						}
						else
						{
							value = DBNull.Value;
						}
						break;
					case CellType.ERROR:
						value = cell.ErrorCellValue.ToString();
						break;
					case CellType.FORMULA:
						value = "=" + cell.CellFormula;
						break;
					default:
						break;
				}
			}
			return value;
		}
		#endregion

		#region 获取Access数据到DataSet

		/// <summary>
		/// 获取access数据库表中所有数据
		/// </summary>
		/// <returns>数据字典，包含表名和表的结果集</returns>
		public DataSet GetAccess(string accessFilePath)
		{
			this._allData.Clear();
		
			var tableNames = GetAllTablesName(accessFilePath);
			tableNames.ForEach((arg) =>
			{
				string sqlString = string.Format("SELECT * FROM [{0}]", arg);
				DataTable tempDt = GetDataTable(sqlString, accessFilePath, null);
				tempDt.TableName = arg;
				_allData.Tables.Add(tempDt.Copy());
			});
			return _allData;
		}

		/// <summary>
		/// 获取数据库中所有的表名
		/// </summary>
		/// <param name="connString"></param>
		/// <param name="sqlString"></param>
		/// <returns></returns>
		private List<string> GetAllTablesName(string accessFilePath)
		{
			var list = new List<string>();
			string strConn=GetConnectionString(accessFilePath);
			using (OleDbConnection conn = new OleDbConnection(strConn))
			{
				try
				{
					if (conn.State != ConnectionState.Open)
						conn.Open();
					DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "Table" });
					string tableName = string.Empty;
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						tableName = dt.Rows[i]["TABLE_NAME"].ToString();
						list.Add(tableName);
					}
				}
				catch (OleDbException ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					conn.Close();
					conn.Dispose();
				}
			}
			return list;
		}

		/// <summary>
		/// 获取连接到Access数据库的字符串
		/// 串接用户导入文件是的文件地址
		/// </summary>
		private string GetConnectionString(string accessFilePath)
		{
			return string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data source={0};User Id=admin;Password=;", accessFilePath);
		}
		/// <summary>
		/// 执行查询语句，返回DataSet
		/// </summary>
		/// <param name="sqlString">查询语句</param>
		/// <returns>DataSet</returns>
		private DataTable GetDataTable(string sqlString, string accessFilePath,params OleDbParameter[] cmdParms)
		{
			string strConn=GetConnectionString(accessFilePath);
			using (OleDbConnection conn = new OleDbConnection(strConn))
			{
				OleDbCommand cmd = new OleDbCommand();
				PrepareCommand(cmd, conn, null, sqlString, cmdParms);
				using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
				{
					DataSet ds = new DataSet();
					try
					{
						da.Fill(ds);
						cmd.Parameters.Clear();
						return ds.Tables[0];
					}
					catch (OleDbException ex)
					{
						throw new Exception(ex.Message);
					}
					finally
					{
						conn.Close();
						conn.Dispose();
					}
				}
			}
		}

				/// <summary>
		/// 预备SQL执行的命令相关条件
		/// </summary>
		/// <param name="cmd">执行命令</param>
		/// <param name="conn">数据连接</param>
		/// <param name="trans">事务</param>
		/// <param name="cmdText">执行的SQL语句</param>
		/// <param name="cmdParms">参数</param>
		private void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, string cmdText, OleDbParameter[] cmdParms)
		{
			if (conn.State != ConnectionState.Open)
				conn.Open();
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			if (trans != null)
				cmd.Transaction = trans;
			cmd.CommandType = CommandType.Text;//cmdType;
			if (cmdParms != null)
			{
				foreach (OleDbParameter parameter in cmdParms)
				{
					if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
						(parameter.Value == null))
					{
						parameter.Value = DBNull.Value;
					}
					cmd.Parameters.Add(parameter);
				}
			}
		}

		#endregion

		#region 进行数据验证

		/// <summary>
		/// 从数据源中获取有效的数据集
		/// </summary>
		/// <param name="dtList">数据源</param>
		/// <param name="xmlPath">XML文件路径</param>
		/// <param name="mainTableName">数据源中的主表名</param>
		/// <param name="isDeleteExists">是否删除数据库中已存在的数据</param>
		/// <returns>有效的数据集信息</returns>
		public DataSet ValidateData(string xmlPath, string mainTableName, bool isDeleteExists)
		{
			if (null == this._allData)
			{
				return null;
			}
			#region 2、验证是否有主表
			//如果主表名为空就把第一个数据表作为主表
			if (string.IsNullOrEmpty(mainTableName))
			{
				mainTableName = this._allData.Tables[0].TableName;
			}
			#endregion

			#region 3、数据验证
			/*
				1. 主表中的主键在sheet中重复
				2. 所有表中主键pk或主外键组合的PK+FK为空的数据忽略，并做提示
				6. 根据主键在数据库中验证是否已经存在
				2. 子表中的fk在主表中不存在
				3. 子表中的fk+pk在sheet中重复
				=====以上数据库处理======
			 
				4. 每个表需要进行数据格式验证的列中的数据是否符合正则
				5. 每个表的代码列将文本替换为代码（如浙江省替换为330000）
			 */
			//读取xml,保存每个表的基础信息
			for (int i = 0; i < this._allData.Tables.Count; i++)
			{
				XmlTwoHandler XmlTwoHandler = new XmlTwoHandler(string.Format(@"{0}\{1}.xml", xmlPath, this._allData.Tables[i].TableName));
				_dicXmls.Add(this._allData.Tables[i].TableName, new XmlTwoHandler(String.Format("{0}\\{1}.xml", xmlPath, this._allData.Tables[i].TableName)));      //获取XMl
				DataTable dtXml = XmlTwoHandler.GetAllData();
				TableValidate validate = GetTableValidate(XmlTwoHandler, dtXml, this._allData.Tables[i]);
				_dicValidate.Add(this._allData.Tables[i].TableName, validate);
			}
            if (_dicValidate.Count > 0 && _dicValidate.Keys.Contains(mainTableName))
			{
                //todo:判断sheet中的主表是否有主键列， 判断从表中有没有主键列和外键列
				//如果主表主键不为空时，才进行数据库数据的匹配验证
				if (!string.IsNullOrEmpty(_dicValidate[mainTableName].PK) )
				{
					//执行数据库验证
					if (!_importValidate.SqlBulkCopyInsertDataForValidate(_dicValidate))
					{
						//执行数据库临时表写入失败
						DataRow dr = this._errorInfo.NewRow();
						dr[0] = "数据库临时表";
						dr[1] = "写入比对数据";
						dr[3] = "写入比对数据失败";
						this._errorInfo.Rows.Add(dr);
					}
					else
					{
						//插入数据库临时表成功后执行数据库已存在记录的比对验证
						DataSet dsError = _importValidate.DatabaseDataValidate(_dicValidate, mainTableName, isDeleteExists);
						if (dsError.Tables.Count > 0)
						{
							for (int i = 0; i < dsError.Tables.Count; i++)
							{
								this._errorInfo.Merge(dsError.Tables[i]);
							}
						}
					}
				}
				//如果数据库验证通过,则开始做正则验证
				if (this._errorInfo.Rows.Count == 0)
				{
					for (int i = 0; i < this._allData.Tables.Count; i++)
					{
						XmlTwoHandler XmlTwoHandler = new XmlTwoHandler(string.Format(@"{0}\{1}.xml", xmlPath, this._allData.Tables[i].TableName));
						DataTable dtXml = XmlTwoHandler.GetAllData();
						Dictionary<string, string> dicRegex = GetDicRegexData(dtXml);//获取xml配置表中所有需要验证正则的数据 <"样本数据","001">
						Dictionary<string, Dictionary<string, string>> dicPath = GetDicXmlPathData(dtXml);//获取xml配置表中所有需要通过path获取的对应xml文件数据<列名，<cn名,值>>	

						//如果有需要进行正则验证的列则进行验证,否则跳过
						if (dicRegex.Count > 0 || dicPath.Count > 0)
						{
							ValidateDataTableData(this._allData.Tables[i], dicRegex, dicPath);
						}
					}
				}
			}
			#endregion

			return this._allData; //2013.11.27 by sang 暂时返回的是全部数据
		}

		/// <summary>
		/// 获取验证实体类
		/// </summary>
		/// <param name="XmlTwoHandler"></param>
		/// <param name="dtXml"></param>
		/// <param name="sourceDataTable"></param>
		/// <returns></returns>
		private TableValidate GetTableValidate(XmlTwoHandler XmlTwoHandler, DataTable dtXml, DataTable sourceDataTable)
		{
			string pkName = XmlTwoHandler.GetRootNodeAttr("pk");
			string fkName = XmlTwoHandler.GetRootNodeAttr("fk");
			string dbTableName = XmlTwoHandler.GetRootNodeAttr("tableName");
			string dbPkName = string.IsNullOrWhiteSpace(pkName) ? string.Empty : dtXml.Select(string.Format("cn='{0}'", pkName))[0]["InnerText"].ToString();
			string dbFkName = string.IsNullOrWhiteSpace(fkName) ? string.Empty : dtXml.Select(string.Format("cn='{0}'", fkName))[0]["InnerText"].ToString();

			TableValidate validate = new TableValidate()
			{
				SourceTable = sourceDataTable,
				PK = pkName,
				FK = fkName,
				DbPK = dbPkName,
				DbFK = dbFkName,
				DbTableName = dbTableName
			};
			return validate;
		}
		
		/// <summary>
		/// 获取一个表对应XML配置文件需要正则验证的数据
		/// </summary>
		/// <param name="dtXmlData">获取到的xml配置文件数据表</param>
		/// <returns>键值对集合《列名，正则编号》</returns>
		private Dictionary<string, string> GetDicRegexData(DataTable dtXmlData)
		{
			var dic = new Dictionary<string, string>();
			for (int i = 0; i < dtXmlData.Rows.Count; i++)
			{
				if (dtXmlData.Rows[i]["regex"] != null && !string.IsNullOrEmpty(dtXmlData.Rows[i]["regex"].ToString()))
				{
					dic.Add(dtXmlData.Rows[i]["cn"].ToString(), dtXmlData.Rows[i]["regex"].ToString());
				}
			}
			return dic;
		}

		/// <summary>
		/// 获取一个XML配置中有Path路径
		/// 对应的XML文件的内容
		/// </summary>
		/// <param name="dtXmlData">获取到的xml配置文件数据表</param>
		/// <returns>键值对集合《列名，path对应的xml文件的值<cn名称，值>》</returns>
		private Dictionary<string, Dictionary<string, string>> GetDicXmlPathData(DataTable dtXmlData)
		{
			XmlTwoHandler XmlTwoHandler;
			DataTable dtPathXml;
			var dic = new Dictionary<string, Dictionary<string, string>>();
			for (int i = 0; i < dtXmlData.Rows.Count; i++)
			{
				if ((dtXmlData.Rows[i]["valid"] != null && Convert.ToBoolean(dtXmlData.Rows[i]["valid"]))
					&& (dtXmlData.Rows[i]["path"] != null && !string.IsNullOrWhiteSpace(dtXmlData.Rows[i]["path"].ToString())))
				{
					XmlTwoHandler = new XmlTwoHandler(dtXmlData.Rows[i]["path"].ToString());
					dtPathXml = XmlTwoHandler.GetAllData();
					Dictionary<string, string> dicPath = new Dictionary<string, string>();
					foreach (DataRow item in dtPathXml.Rows)
					{
						dicPath.Add(item["cn"].ToString(), item["InnerText"].ToString());
					}
					dic.Add(dtXmlData.Rows[i]["cn"].ToString(), dicPath);
				}
			}
			return dic;
		}

		/// <summary>
		/// 验证dataset中所有表的有效性
		/// 得到正确的所有表数据
		/// </summary>
		/// <param name="dtSource">需要验证的表数据</param>
		/// <param name="dicRegex">所有表对应的xml正则验证数据</param>
		/// <param name="dicPath">所有表对对应的xml匹配文件数据</param> 
		private void ValidateDataTableData(DataTable dtSource, Dictionary<string, string> dicRegex, Dictionary<string, Dictionary<string, string>> dicPath)
		{
			//DataTable dt = new DataTable();  //备用,保存正确的行
			string pkName = string.Empty;
			for (int i = 0; i < dtSource.Rows.Count; i++)
			{
				bool correctRow = true;  //该行验证通过
				pkName = this._dicValidate[dtSource.TableName].PK;
				if (dicRegex.Count > 0)
				{
					object obj;
					for (int j = 0; j < dicRegex.Count; j++)
					{
						string colName = dicRegex.Keys.ElementAt(j);
						string regexNo = dicRegex.Values.ElementAt(j);

                        //此处增加了XML文件中有的节点，但是excel中没有节点的处理
                        //修改人：李士刚
                        //修改时间：2013-12-31 17:26:35
                        if(!dtSource.Columns.Contains(colName))
                        {
                            continue;
                        }
						obj = dtSource.Rows[i][colName];
						if (!CheckRegex(obj, regexNo))
						{
							correctRow = false;
							DataRow dr = this._errorInfo.NewRow();
							dr[0] = dtSource.TableName;
							dr[1] = "数据内容验证";
							dr[2] = i + 1;
							dr[3] = String.Format("列【{0}】的值【{1}】不符合数据规则", colName, dtSource.Rows[i][colName]);
							dr[4] = dtSource.Rows[i][pkName];
							this._errorInfo.Rows.Add(dr);
						}
					}
				}
				if (dicPath.Count > 0)
				{
					object obj;
					for (int j = 0; j < dicPath.Count; j++)
					{
						string colName = dicPath.Keys.ElementAt(j);
						Dictionary<string, string> dicPathValues = dicPath.Values.ElementAt(j);
						obj = dtSource.Rows[i][colName];
						if (obj != null && obj.ToString() != "")
						{
							if (dicPathValues.ContainsKey(obj.ToString()))
							{
								dtSource.Rows[i][colName] = dicPathValues[obj.ToString()];
							}
							else
							{
								correctRow = false;
								DataRow dr = this._errorInfo.NewRow();
								dr[0] = dtSource.TableName;
								dr[1] = "状态文件匹配验证";
								dr[2] = i + 1;
								dr[3] = String.Format("工作表【{0}】第【{1}】行,列名为【{2}】的列状态值匹配错误", dtSource.TableName, i, j);
								this._errorInfo.Rows.Add(dr);
							}
						}
						else
						{
							dtSource.Rows[i][colName] = DBNull.Value;
						}
					}
				}
				if (correctRow)
				{
					//保存验证通过的行
				}
			} 
		}

		/// <summary>
		/// 正则验证
		/// </summary>
		/// <param name="CellVale">单元格的值</param>
		/// <param name="RegexNum">xml里正则验证编号</param>
		/// <returns></returns>
		private bool CheckRegex(object CellVale, string RegexNum)
		{
			bool checkResult = false;
			string regexexpression = string.Empty; //正则表达式		

			//如果有正则列则取出正则公式进行验证, 验证失败记录到失败列表                 
			if (!string.IsNullOrEmpty(RegexNum))
			{
				DataRow regexRow = null;
				if (0 != _dtRegexConfig.Select(String.Format("InnerText='{0}'", RegexNum)).Length)
				{
					regexRow = _dtRegexConfig.Select(String.Format("InnerText='{0}'", RegexNum))[0];
				}
				//DataRow regexRow = regexTable.Select(String.Format("InnerText='{0}'", regexnumber))[0];
				//如果存在text列则获取正则表达式
				if (_dtRegexConfig.Columns.Contains("text") && null != regexRow)
				{
					regexexpression = regexRow["text"].ToString();
				}
				string regexCN = "";
				if (_dtRegexConfig.Columns.Contains("cn"))
				{
					regexCN = regexRow["cn"].ToString();
				}
				//如果有正则表达式则进行验证,验证失败记录到失败列表
				if (!string.IsNullOrEmpty(regexexpression))
				{
					Regex tempregex = new Regex(regexexpression, RegexOptions.Multiline);
					if (CellVale != null)
					{
						if (tempregex.IsMatch(CellVale.ToString()))
						{
							checkResult = true;
						}
					}
					else
					{
						if (tempregex.IsMatch(string.Empty))
						{
							checkResult = true;
						}
					}
				}
			}
			return checkResult;
		}
		#endregion


		/// <summary>
		/// 将dataset中的数据插入数据库中
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public bool InsertAllData()
		{
			bool result = false;
			if (null != this._allData && this.AllData.Tables.Count > 0)
			{
				result = _importValidate.InsertData(this._allData, _dicXmls);
			} 
			return result;
		}


	}

    /// <summary>
    /// 验证冲突数据的自定义类
    /// </summary>
    public class TableValidate
    {
        /// <summary>
        /// 源数据表
        /// </summary>
        public DataTable SourceTable { get; set; }
        /// <summary>
        /// 源主键
        /// </summary>
        public string PK { get; set; }
        /// <summary>
        /// 源外键
        /// </summary>
        public string FK { get; set; }
        /// <summary>
        /// 数据库表名
        /// </summary>
        public string DbTableName { get; set; }
        /// <summary>
        /// 数据库表主键
        /// </summary>
        public string DbPK { get; set; }
        /// <summary>
        /// 数据库表外键
        /// </summary>
        public string DbFK { get; set; }


    }
}
