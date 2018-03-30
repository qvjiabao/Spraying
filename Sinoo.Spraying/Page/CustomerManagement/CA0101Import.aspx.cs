using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Sinoo.BLL;

namespace Sinoo.Spraying.Page.CustomerManagement
{
    public partial class CA0101Import : System.Web.UI.Page
    {
        ExcelBLL _ExcelBLL = new ExcelBLL();  //实例化

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);

                if (fileExt == ".xlsx" || fileExt == ".xls")
                {
                    DataSet dt = Sinoo.Common.Excel.ExcelHandler.GetDataSetFromExcel(FileUpload1.FileContent);
                    if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
                    {
                        Dictionary<string, string> dictionary = _ExcelBLL.ImportCustomerBase(dt.Tables[0]);
                        this.Label1.Text = string.Format("提示:成功{0}条数据,失败{1}条数据,重复{2}条,数据失败行号{3}"
                            , dictionary["count"]
                            , dictionary["errorNum"]
                            , dictionary["existsNum"]
                            , string.IsNullOrEmpty(dictionary["_str"]) ? "暂无" : dictionary["_str"]);

                    }
                    else
                    {
                        this.Label1.Text = "导入失败";
                    }
                }
                else
                {
                    this.Label1.Text = "格式不正确";
                }
            }
        }
    }
}