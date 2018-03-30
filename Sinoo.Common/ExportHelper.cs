using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data.OleDb;
//using Sinoo.Common.Document;

namespace Sinoo.Common
{
    /// <summary>
    /// 导出数据帮助类
    /// <remarks>用于常用的数据导出，从数据库导出到文件。包括：EXCEL 、ACCESS</remarks>
    /// </summary>
    public class ExportHelper
    {

        #region added by sang at 2013.09.17 : 导出多个sheet所用的方法
        /// <summary>
        /// 根据xml配置文件导出excel
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="PathName">配置文件路径</param>
        /// <param name="XmlNames">xml文件名列表</param>
        /// <returns></returns>
        public static MemoryStream ExportExcelByDataset(DataSet ds, string PathName, string[] XmlNames)
        {
            HSSFWorkbook workbook = ReBuildWorkbook();
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    string xmlName = "Sheet" + i.ToString();
                    if (i < XmlNames.Length)
                    {
                        xmlName = XmlNames[i];
                    }
                    CreateSheets(workbook, ds.Tables[i], PathName, xmlName);
                }
            }

            return GetExcelStream(workbook);
        }

        /// <summary>
        /// 创建workbook
        /// created by sang at 2013.09.17
        /// </summary>
        /// <param name="companyXmlFileName">公司信息配置文件</param>
        /// <returns></returns> 
        private static HSSFWorkbook ReBuildWorkbook(string companyXmlFileName = "CompanyConfig.xml")
        {
            //实例化工作薄            
            HSSFWorkbook _workbook = new HSSFWorkbook();

            //添加工作薄的属性（鼠标右键->属性->详细信息）
            //实例化公司信息的xml
            XmlHandler XmlCompany = new XmlHandler(companyXmlFileName);
            DataTable result = XmlCompany.GetAllData();
            //文件的大概信息
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = result.Select("InnerText='Company'")[0]["cn"].ToString();
            //主要信息
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = result.Select("InnerText='Author'")[0]["cn"].ToString(); //作者信息
            si.ApplicationName = result.Select("InnerText='ApplicationName'")[0]["cn"].ToString();            //填加xls文件创建程序信息   
            si.LastAuthor = result.Select("InnerText='LastAuthor'")[0]["cn"].ToString();           //填加xls文件最后保存者信息   
            si.Comments = result.Select("InnerText='Comments'")[0]["cn"].ToString();      //填加xls文件作者信息   
            si.Title = result.Select("InnerText='Title'")[0]["cn"].ToString();               //填加xls文件标题信息   
            si.Subject = result.Select("InnerText='Subject'")[0]["cn"].ToString();              //填加文件主题信息   
            si.CreateDateTime = DateTime.Now;

            _workbook.DocumentSummaryInformation = dsi;
            _workbook.SummaryInformation = si;
            return _workbook;
        }

        /// <summary>
        /// 为工作簿创建一个新的工作表
        /// Created by sang at 2013.09.17
        /// </summary>
        /// <param name="dtSource">数据源表</param>
        /// <param name="XmlName">配置文件</param>
        private static void CreateSheets(HSSFWorkbook workbook, DataTable dtSource, string PathName, string XmlName)
        {
            ISheet sheet;  //当前操作的sheet, 由AddNewSheet方法实例化          
            XmlHandler XmlH = new XmlHandler(PathName + XmlName);  //实例化XMl
            DataTable XmlTable = XmlH.GetAllData();     //获取XML中的数据            
            string sheetName = XmlH.GetRootNodeAttr("xmlns:p1");   //定义工作表名称         
            int rowIndex;   //定义行索引, 从第2行开始写入, 因为第一行是列头
            int rowStart;   //数据起始写入行, 创建sheet后输出
            sheet = AddNewSheet(workbook, XmlH, sheetName, out rowStart);
            rowIndex = rowStart + 1;  //从开始行的下一行开始写入数据
            int columnMaxWidth = 100 * 256;  //excel中列的最大宽度

            ICellStyle dateStyle = workbook.CreateCellStyle();//创建单元格显示样式
            IDataFormat format = workbook.CreateDataFormat();//创建数据信息
            foreach (DataRow row in dtSource.Rows)
            {
                //如果大于65535行则另外新建一个sheet
                if (rowIndex == 65535)
                {
                    sheet = AddNewSheet(workbook, XmlH, sheetName + ((int)rowIndex / 65535).ToString(), out rowStart);
                    rowIndex = rowStart + 1;
                }

                //填充内容
                IRow xlsNewRow = sheet.CreateRow(rowIndex);                
                for (int i = 0; i < XmlTable.Rows.Count; i++)
                {
                    ICell xlsNewCell = xlsNewRow.CreateCell(i);
                    string columnName = XmlTable.Rows[i]["InnerText"].ToString();
                    //获取当前行当前列的值
                    string drValue = row[columnName].ToString();
                    //当前列配置文件的路径
                    string path = Convert.ToString(XmlTable.Rows[i]["path"]);

                    if (!string.IsNullOrEmpty(path)&&!string.IsNullOrEmpty(drValue))
                    {    //当配置文件不为空时,将drValue转换为配置文件中的值
                        XmlHandler xmlconfig = new XmlHandler(path);
                        DataTable configtable = xmlconfig.GetAllData();
                        drValue = configtable.Select("InnerText='" + drValue + "'")[0]["cn"].ToString();
                        xlsNewCell.SetCellValue(drValue);//给数据当前行当前列赋值,无论列是什么类型都作为string写入
                    }
                    else
                    {   //没有配置文件时根据类型写入drValue值
                        #region 根据类型写入单元格数据
                        switch (dtSource.Columns[columnName].DataType.ToString())
                        {
                            case "System.String"://字符串类型   
                                xlsNewCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型   
                                if (!string.IsNullOrEmpty(drValue))
                                {
                                    DateTime dateV;
                                    DateTime.TryParse(drValue, out dateV);
                                    xlsNewCell.SetCellValue(dateV);
                                    //ICellStyle dateStyle = workbook.CreateCellStyle();//创建单元格显示样式
                                    //IDataFormat format = workbook.CreateDataFormat();//创建数据信息
                                    //dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd hh:mm:ss"); //带时分秒
                                    dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
                                    xlsNewCell.CellStyle = dateStyle;
                                }
                                break;
                            case "System.Boolean"://布尔型   
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                xlsNewCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型   
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                xlsNewCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型   
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                xlsNewCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理   
                                xlsNewCell.SetCellValue("");
                                break;
                            default:
                                xlsNewCell.SetCellValue("");
                                break;
                        }
                        #endregion
                    }
                    //根据数据重新设置列宽
                    int columnWidth = sheet.GetColumnWidth(i);
                    int dataWidth = (Encoding.GetEncoding(936).GetBytes(drValue).Length + 1) * 256;
                    if (columnWidth > 0 && dataWidth > columnWidth)
                    {
                        if (dataWidth > columnMaxWidth)
                        {
                            sheet.SetColumnWidth(i, columnMaxWidth);
                            //HSSFCellStyle cellStyle = (HSSFCellStyle)xlsNewCell.CellStyle;
                            //cellStyle.WrapText = true;
                            //xlsNewCell.CellStyle = cellStyle;
                        }
                        else
                        {
                            sheet.SetColumnWidth(i, dataWidth);
                        }
                    }
                }
                rowIndex++;
            }

        }


        /// <summary>
        /// 为工作簿增加一个工作表, 增加时会根据配置文件创建列头并根据列头设置列宽
        /// Added by sang at 2013.09.18
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="XmlH"></param>
        /// <param name="sheetName"></param>
        /// <param name="rowStart">数据开始行号</param>
        /// <returns>增加的工作表</returns>
        private static ISheet AddNewSheet(HSSFWorkbook workbook, XmlHandler XmlH, string sheetName, out int rowStart)
        {
            ISheet sheet;
            DataTable XmlTable = XmlH.GetAllData();

            //如果没有定义sheetName则默认为"Sheet"
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = "Sheet";
            }
            //如果工作表的名字已存在则在后面加(1)
            if (workbook.GetSheet(sheetName) != null)
            {
                string tempSheetName = sheetName;
                for (int i = 1; workbook.GetSheet(tempSheetName) != null; i++)
                {
                    tempSheetName = sheetName + "(" + i.ToString() + ")";
                }
                sheetName = tempSheetName;
            }
            //为工作簿新增工作表
            sheet = workbook.CreateSheet(sheetName);
            //数据起始写入行
            rowStart = 0;
            IRow headerRow = sheet.CreateRow(rowStart);
            //表头及样式
            if (XmlH.GetRootNodeAttr("xmlns:p2") != "无")
            {
                headerRow.HeightInPoints = 25;
                headerRow.CreateCell(0).SetCellValue(XmlH.GetRootNodeAttr("xmlns:p2"));
                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, XmlH.NodeCount() - 1));
                //定义数据开始写入行
                rowStart = 1;
            }
            //根据列头为工作表的每一列设置列宽
            ICellStyle headStyle = workbook.CreateCellStyle();
            IFont font = workbook.CreateFont();
            for (int columnCount = 0; columnCount < XmlH.NodeCount(); columnCount++)
            {
                string colName = XmlTable.Rows[columnCount]["InnerText"].ToString();
                int textLength = 0;
                //获取列头的字符串长度
                textLength = Encoding.GetEncoding(936).GetBytes(colName).Length;//获取当前字符串的长度；

                //列名和列头格式
                ICell cell = headerRow.CreateCell(columnCount);
                cell.SetCellValue(XmlTable.Rows[columnCount]["cn"].ToString());
                headStyle.Alignment = HorizontalAlignment.CENTER;
                //font.FontHeightInPoints = 12;
                font.Boldweight = (short)FontBoldWeight.BOLD;
                headStyle.SetFont(font);
                cell.CellStyle = headStyle;

                //隐藏列设置列宽为0, 否则为列指定一个长度
                if (Convert.ToBoolean(XmlTable.Rows[columnCount]["hidden"]))
                {
                    sheet.SetColumnWidth(columnCount, 0);
                }
                else
                {
                    sheet.SetColumnWidth(columnCount, (textLength + 1) * 256);
                }
            }

            return sheet;
        }
        /// <summary>
        /// 获取excel工作簿的流
        /// </summary>
        /// <returns></returns>
        private static MemoryStream GetExcelStream(HSSFWorkbook workbook)
        {
            //返回数据流
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            workbook = null;
            return ms;
        }
        #endregion




        #region  导出Access 方法


        /// <summary>
        /// 导出Access
        /// </summary>
        /// <param name="SourceData">数据源</param>
        /// <param name="AccessPath">access路径</param>
        /// <param name="SourceTableNames">数据表名字</param>
        /// <param name="SourceColumnNames">数据列名字</param>
        /// <param name="AccessTableNames">Access表名</param>
        /// <param name="AccessColumnNames">Access列名</param>
        /// <returns></returns>
        public static bool ExportAccessByDataset(DataSet SourceData, string AccessPath, string[] AccessTableNames, string[] AccessColumnNames)
        //DataSet ds, string Access_new_url, string[] Columns, string[] AccessColumns)
        {
            bool succeed = true;
            if (SourceData != null && SourceData.Tables.Count > 0)
            {
                string actableName;
                string acColumns;
                string strConn = GetConnectionString(AccessPath);
                StringBuilder sourceValues = new StringBuilder();

                OleDbDataAdapter adapter = new OleDbDataAdapter();



                OleDbCommand cmd;
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    //for (int i = 0; i < SourceData.Tables.Count; i++)
                    //{
                    //    UpdataDataSet(SourceData.Tables[i], conn, string.Format("SELECT * FROM [{0}]",AccessTableNames[i]));
                    //}

                    #region 暂时不用

                    for (int j = 0; j < SourceData.Tables.Count; j++)   //数据表的 个数
                    {
                        conn.Open();
                        actableName = AccessTableNames[j];
                        acColumns = AccessColumnNames[j];
                        for (int i = 0; i < SourceData.Tables[j].Rows.Count; i++)  //第J个表的行数
                        {
                            //if ((i + 1) % 500 == 1)
                            //{
                            //    conn.Open();

                            //}
                            sourceValues.Clear();
                            for (int k = 0; k < SourceData.Tables[j].Columns.Count; k++)  //第J个表的 列
                            {
                                if (SourceData.Tables[j].Rows[i][k] != null && SourceData.Tables[j].Rows[i][k] != DBNull.Value)
                                {
                                    sourceValues.AppendFormat(",'{0}'", SourceData.Tables[j].Rows[i][k].ToString().Replace("'", "''"));
                                }
                                else
                                {
                                    sourceValues.AppendFormat(",null");
                                }
                            }


                            sourceValues = sourceValues.Remove(0, 1);

                            string strCom = string.Format("insert into [{0}] ({1})  values({2}) ", actableName, acColumns, sourceValues.ToString());
                            cmd = new OleDbCommand(strCom, conn);

                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                succeed = false;
                            }
                            //if ((i + 1) % 500 == 0)
                            //{
                            //    conn.Close();

                            //}
                        }
                        conn.Close();
                    }
                    #endregion
                }
            }

            return succeed;

        }
        #endregion

        private static string GetConnectionString(string accessFilePath)
        {
            return string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data source={0};User Id=admin;Password=;", accessFilePath);
        }

        public static bool UpdataDataSet(DataTable dt, OleDbConnection conn, String sql)
        {

            using (OleDbDataAdapter dap = new OleDbDataAdapter(sql, conn))
            {
                OleDbCommandBuilder cb = new OleDbCommandBuilder(dap);
                try
                {
                    dap.Update(dt);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
        }

    }
}
