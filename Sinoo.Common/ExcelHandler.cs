using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;

namespace Sinoo.Common
{
    /// <summary>
    /// Excel操作类
    /// </summary>
    public class ExcelHandler
    {
        ExcelBLL _ExcelBll = new ExcelBLL();

        /// <summary>
        /// 单SheetExcel
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="xmlNames"></param>
        /// <returns></returns>
        public byte[] ExportExcel(DataSet ds, string xmlNames)
        {

            //获取文件流
            MemoryStream stream = Sinoo.Common.ExportHelper.ExportExcelByDataset(ds, "",new string[] { xmlNames });


            byte[] bytes = new byte[(int)stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();

            return bytes;
        }

        /// <summary>
        /// 多SheetExcel
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="xmlNames"></param>
        /// <returns></returns>
        public byte[] ExportExcel(DataSet ds, string[] xmlNames)
        {

            //获取文件流
            MemoryStream stream = Sinoo.Common.ExportHelper.ExportExcelByDataset(ds,"",xmlNames);
                //.Excel.ExcelHandler.ExportExcelByDataset(ds, xmlNames);
            byte[] bytes = new byte[(int)stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();

            return bytes;
        }

    }
}