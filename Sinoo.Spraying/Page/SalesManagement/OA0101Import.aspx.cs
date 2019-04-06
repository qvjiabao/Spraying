using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Sinoo.BLL;
using Sinoo.Model;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace Sinoo.Spraying.Page.SalesManagement
{
    public partial class OA0101Import : BasePage
    {
        ExcelBLL _ExcelBLL = new ExcelBLL();
        UserBLL _UserBll = new UserBLL();
        CustomerBLL _CustomerBLL = new CustomerBLL();
        OrderBLL _OrderBLL = new OrderBLL();
        ProductBLL _ProductBLL = new ProductBLL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导入订单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImportOA01_Click(object sender, EventArgs e)
        {
            #region 效率低导入
            //if (FileUpload1.HasFile)
            //{
            //    string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);

            //    if (fileExt == ".xlsx" || fileExt == ".xls")
            //    {
            //        DataSet dt = Sinoo.Common.Excel.ExcelHandler.GetDataSetFromExcel(FileUpload1.FileContent);
            //        if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            //        {
            //            UserBase _UserBase = Session["USER_SESSION"] as UserBase;
            //            Dictionary<string, string> dictionary = _ExcelBLL.ImportOrderBase(dt, _UserBase);
            //            this.Label1.Text = string.Format("提示:{0}", dictionary["error"]);

            //        }
            //        else
            //        {
            //            this.Label1.Text = "导入失败";
            //        }
            //    }
            //    else
            //    {
            //        this.Label1.Text = "格式不正确";
            //    }
            //}

            #endregion

            bool chkIsReWrite = false;
            //验证Excel
            if (!this.IsValidator(this.FileUpload1))
            {
                return;
            }

            ImportHelper importHelper = new ImportHelper(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);

            string tableHtml = string.Empty; //返回到前台的错误信息列表
            const string ExcelFilePath = "PM";//excel模板所在的文件夹  


            #region 导入Excel
            Stream stream = Request.Files[0].InputStream;  //this.FileUpload1.FileContent;
            //获取excel中的数据
            importHelper.GetExcel(stream);
            //进行数据验证
            importHelper.ValidateData(ExcelFilePath, "订单信息", chkIsReWrite);
            #endregion

            if (importHelper.ErrorInfo != null && importHelper.ErrorInfo.Rows.Count > 0)
            {
                tableHtml = DataTableHelper.DataTableToString(importHelper.ErrorInfo, 100);
                this.Label1.Text = tableHtml;
                //this.ErrorDiv.InnerHtml = tableHtml;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入失败');Location();</script>");
                return;
            }
            else
            {
                DataTable dt = importHelper.AllData.Tables[0];
                DataTable dt5 = _UserBll.SelectUserBasePosition("");  //所有的销售人. （根据用户登录的所属区域）
                DataTable dt6 = _CustomerBLL.SelectAC();  //所有应用代码 
                DataTable dt7 = _CustomerBLL.SelectCustomerBase();  //客户资料
                dt.Columns.Add("GUID");

                foreach (DataRow item in dt.Rows)
                {
                    #region 验证销售员
                    DataRow[] dr = dt5.Select(string.Format(" UA01005 = '{0}' ", item["销售员"].ToString()));
                    if (dr.Length > 0)
                    {
                        item["销售员"] = dr[0]["UA01001"];
                    }
                    else
                    {

                        item["销售员"] = 0;
                    }
                    #endregion

                    #region 验证应用代码

                    DataRow[] dr1 = dt6.Select(string.Format(" OB02002 = '{0}' ", item["应用代码"].ToString()));
                    if (dr1.Length > 0)
                    {
                        item["应用代码"] = dr1[0]["OB02001"];
                    }
                    else
                    {
                        item["应用代码"] = 0;
                    }

                    #endregion

                    #region 验证客户名称

                    DataRow[] dr7 = dt7.Select(string.Format(" CA01003 = '{0}' ", item["客户名称"].ToString()));
                    if (dr7.Length > 0)
                    {
                        item["客户名称"] = dr7[0]["CA01001"];
                    }
                    else
                    {
                        item["客户名称"] = 0;
                    }

                    #endregion

                    item["GUID"] = Guid.NewGuid();
                }
                Stopwatch stop = new Stopwatch();
                long millCount = 0;
                stop.Start();
                if (importHelper.InsertAllData())
                {
                    stop.Stop();
                    millCount = stop.ElapsedMilliseconds;
                    //表示成功
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入成功')</script>");
                }
                else
                {
                    stop.Stop();
                    millCount = stop.ElapsedMilliseconds;
                    //执行失败
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入失败');Location();</script>");
                }
            }
        }

        /// <summary>
        /// 导入商品明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImportOB01_Click(object sender, EventArgs e)
        {
            bool chkIsReWrite = false;
            //验证Excel
            if (!this.IsValidator(this.FileUpload2))
            {
                return;
            }

            ImportHelper importHelper = new ImportHelper(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);

            string tableHtml = string.Empty; //返回到前台的错误信息列表
            const string ExcelFilePath = "PM";//excel模板所在的文件夹  


            #region 导入Excel
            Stream stream = this.FileUpload2.FileContent;   //Request.Files[0].InputStream;
            //获取excel中的数据
            importHelper.GetExcel(stream);
            //进行数据验证
            importHelper.ValidateData(ExcelFilePath, "订单明细", chkIsReWrite);
            #endregion

            if (importHelper.ErrorInfo != null && importHelper.ErrorInfo.Rows.Count > 0)
            {
                tableHtml = DataTableHelper.DataTableToString(importHelper.ErrorInfo, 100);
                this.Label1.Text = tableHtml;
                //this.ErrorDiv.InnerHtml = tableHtml;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入失败');Location();</script>");
                return;
            }
            else
            {
                DataTable dt = importHelper.AllData.Tables[0]; //导入的Excel订单明细
                DataTable dt1 = _OrderBLL.SelectOrderBaseForList();  //所有订单号
                DataTable dt2 = _ProductBLL.SelectProductBase();
                dt.Columns.Add("商品ID");
                dt.Columns.Add("GUID");
                foreach (DataRow item in dt.Rows)
                {
                    DataRow[] dr = dt1.Select(string.Format(" OA01002 = '{0}' "
                                    , item["订单号"].ToString()));
                    if (dr.Length > 0)
                    {
                        item["订单号"] = dr[0]["OA01999"].ToString();  //关联订单GUID
                    }

                    DataRow[] dr1 = dt2.Select(string.Format(" PA01003 = '{0}' "
                                  , item["商品编号(型号)"].ToString()));
                    if (dr1.Length > 0)
                    {
                        item["商品ID"] = dr1[0]["PA01001"].ToString();  //关联订单GUID
                    }
                    else 
                    {
                        item["商品ID"] = 0;  //关联订单GUID
                    }

                    item["GUID"] = Guid.NewGuid();
                }
                Stopwatch stop = new Stopwatch();
                long millCount = 0;
                stop.Start();
                if (importHelper.InsertAllData())
                {
                    stop.Stop();
                    millCount = stop.ElapsedMilliseconds;
                    //表示成功
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入成功')</script>");
                }
                else
                {
                    stop.Stop();
                    millCount = stop.ElapsedMilliseconds;
                    //执行失败
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入失败');Location();</script>");
                }

            }
        }

        /// <summary>
        /// 发票发货信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImportOC01_Click(object sender, EventArgs e)
        {
            bool chkIsReWrite = false;
            //验证Excel
            if (!this.IsValidator(this.FileUpload3))
            {
                return;
            }

            ImportHelper importHelper = new ImportHelper(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);

            string tableHtml = string.Empty; //返回到前台的错误信息列表
            const string ExcelFilePath = "PM";//excel模板所在的文件夹  


            #region 导入Excel
            Stream stream = this.FileUpload3.FileContent;   //Request.Files[0].InputStream;
            //获取excel中的数据
            importHelper.GetExcel(stream);
            //进行数据验证
            importHelper.ValidateData(ExcelFilePath, "发票发货信息", chkIsReWrite);
            #endregion

            if (importHelper.ErrorInfo != null && importHelper.ErrorInfo.Rows.Count > 0)
            {
                tableHtml = DataTableHelper.DataTableToString(importHelper.ErrorInfo, 100);
                this.Label1.Text = tableHtml;
                //this.ErrorDiv.InnerHtml = tableHtml;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入失败');Location();</script>");
                return;
            }
            else
            {
                DataTable dt = importHelper.AllData.Tables[0]; //导入的Excel订单明细
                DataTable dt1 = _OrderBLL.SelectOrderBaseAndOrderProductAll();  //所有订单号
                dt.Columns.Add("GUID");

                foreach (DataRow item in dt.Rows)
                {
                    DataRow[] dr = dt1.Select(string.Format(" OA01002 = '{0}' AND OB01005 = '{1}' "
                                    , item["订单号"].ToString(), item["型号"].ToString()));
                    if (dr.Length > 0)
                    {
                        item["订单号"] = dr[0]["OB01999"].ToString();  //关联订单GUID
                    }

                    item["GUID"] = Guid.NewGuid();
                }

                Stopwatch stop = new Stopwatch();
                long millCount = 0;
                stop.Start();
                if (importHelper.InsertAllData())
                {
                    stop.Stop();
                    millCount = stop.ElapsedMilliseconds;
                    //表示成功
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入成功')</script>");
                }
                else
                {
                    stop.Stop();
                    millCount = stop.ElapsedMilliseconds;
                    //执行失败
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入失败');Location();</script>");
                }

            }
        }

        /// <summary>
        /// 付款信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImportOP01_Click(object sender, EventArgs e)
        {
            bool chkIsReWrite = false;
            //验证Excel
            if (!this.IsValidator(this.FileUpload4))
            {
                return;
            }

            ImportHelper importHelper = new ImportHelper(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);

            string tableHtml = string.Empty; //返回到前台的错误信息列表
            const string ExcelFilePath = "PM";//excel模板所在的文件夹  


            #region 导入Excel
            Stream stream = this.FileUpload4.FileContent;   //Request.Files[0].InputStream;
            //获取excel中的数据
            importHelper.GetExcel(stream);
            //进行数据验证
            importHelper.ValidateData(ExcelFilePath, "付款信息", chkIsReWrite);
            #endregion

            if (importHelper.ErrorInfo != null && importHelper.ErrorInfo.Rows.Count > 0)
            {
                tableHtml = DataTableHelper.DataTableToString(importHelper.ErrorInfo, 100);
                this.Label1.Text = tableHtml;
                //this.ErrorDiv.InnerHtml = tableHtml;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入失败');Location();</script>");
                return;
            }
            else
            {
                DataTable dt = importHelper.AllData.Tables[0]; //导入的Excel订单明细
                DataTable dt1 = _OrderBLL.SelectOrderBaseForList();  //所有订单号
                dt.Columns.Add("GUID");

                foreach (DataRow item in dt.Rows)
                {
                    DataRow[] dr = dt1.Select(string.Format(" OA01002 = '{0}' "
                                    , item["订单号"].ToString()));
                    if (dr.Length > 0)
                    {
                        item["订单号"] = dr[0]["OA01999"].ToString();  //关联订单GUID
                    }

                    item["GUID"] = Guid.NewGuid();
                }

                Stopwatch stop = new Stopwatch();
                long millCount = 0;
                stop.Start();
                if (importHelper.InsertAllData())
                {
                    stop.Stop();
                    millCount = stop.ElapsedMilliseconds;
                    //表示成功
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入成功')</script>");
                }
                else
                {
                    stop.Stop();
                    millCount = stop.ElapsedMilliseconds;
                    //执行失败
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('导入失败');Location();</script>");
                }

            }
        }

        /// <summary>
        /// 验证Excel
        /// </summary>
        /// <param name="_FileUpload"></param>
        /// <returns></returns>
        private bool IsValidator(FileUpload _FileUpload)
        {
            bool bl = true;

            if (!_FileUpload.HasFile)
            {
                bl = false;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择上传文件')</script>");

            }
            else
            {
                //DataTable dtErrorInfo = new DataTable();//存储错误信息的DataTable
                //DataSet ds = new DataSet();//把读取出的数据转换成DataSet
                string extend = _FileUpload.FileName.Substring(_FileUpload.FileName.LastIndexOf('.'));
                if (extend != ".xls" && extend != ".xlsx")
                {
                    bl = false;
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('选择文件的格式不对')</script>");
                }
            }
            return bl;
        }
    }
}