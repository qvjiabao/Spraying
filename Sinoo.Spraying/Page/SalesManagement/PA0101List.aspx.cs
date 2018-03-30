using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;
using Sinoo.Model;
using Sinoo.Common;

namespace Sinoo.Spraying.Page.SalesManagement
{
    public partial class PA0101List : System.Web.UI.Page
    {

        ProductBLL _ProductBLL = new ProductBLL();
        UserBLL _UserBLL = new UserBLL();
        public string DataCount = string.Empty;

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="Index"></param>
        private void InitData(int Index, bool bl)
        {
            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;

            ViewState["PageIndex"] = Index;
            string strWhereAdd = string.Empty;
            if (!string.IsNullOrEmpty(this.txtPA01003.Text.Trim()))
            {
                strWhereAdd = string.Format(" AND PA01003 LIKE '%{0}%' ", this.txtPA01003.Text.Trim());
            }
            object RowsCount = null;
            DataTable dt = _ProductBLL.SelectProductBasePage(AspNetPager1.PageSize, Index, strWhereAdd, ref RowsCount);
            if (dt.Rows.Count > 0)
            {
                DataCount = string.Empty;
            }
            else
            {
                if (bl)
                {
                    DataCount = "<tr><td colspan=\"7\">系统暂无数据显示</td></tr>";
                }
                else
                {
                    DataCount = "<tr><td colspan=\"7\">没有符合查询条件的数据</td></tr>";

                }
            }

            AspNetPager1.RecordCount = Convert.ToInt32(RowsCount);
            AspNetPager1.CurrentPageIndex = Index;
            rpPA01.DataSource = dt;
            rpPA01.DataBind();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveData(object sender, CommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                ProductBase _PA01 = new ProductBase();
                _PA01.PA01001 = Convert.ToInt32(e.CommandArgument.ToString());
                _PA01.PA01997 = 1;
                int num = Math.Abs(_ProductBLL.RemoveProductBase(_PA01));
                new Sinoo.Common.MessageShow().RemoveMessage(this, num, string.Empty);

                if (num < 1)
                {
                    return;
                }

                int CurrentPageIndex = this.AspNetPager1.CurrentPageIndex;//当前页
                int PageCount = this.AspNetPager1.PageCount;//页总数
                int RecordCount = this.AspNetPager1.RecordCount;//记录数
                int PageSize = this.AspNetPager1.PageSize;//第页条数

                int PageIndex = CurrentPageIndex == PageCount ? (PageCount * PageSize - RecordCount == (PageSize - 1) ? CurrentPageIndex - 1 : CurrentPageIndex) : CurrentPageIndex;

                InitData(PageIndex, false);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="strExcelName">下载的Excel文件名</param>
        /// <param name="strXmlName">xml文件名</param>
        private void DownloadFile(DataSet ds, string strExcelName, string strXmlName)
        {
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(strExcelName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(new ExcelHandler().ExportExcel(ds, strXmlName));
            Response.Flush();
            Response.End();
        }

        /// <summary>
        /// 跳转新增页面
        /// </summary>
        private void LinkPA0101New()
        {
            Response.Redirect(string.Format("PA0101New.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            LinkPA0101New();
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_SESSION"] != null)
            {
                Model.UserBase _UserBase = Session["USER_SESSION"] as Model.UserBase;

                DataTable dt = _UserBLL.SelectSystemRole(Convert.ToInt32(_UserBase.UA01024));
                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("GA01003 = 'PA01btnNew'"); //新增按钮
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript1", "<script>RemoveNewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'PA01btnExport'");  //导出
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript2", "<script>RemoveExportCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'PA01btnImport'");  //导入
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript3", "<script>RemoveImportCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'PA01btnView'");  //浏览
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript4", "<script>RemoveViewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'PA01btnEdit'");  //编辑
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript5", "<script>RemoveEditCss();</script>");
                    }

                    dr = dt.Select("GA01003= 'PA01btnDel'");  //删除
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript6", "<script>RemoveDelCss();</script>");
                    }
                }
            }

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["PageIndex"]))  //判断当前页
                {
                    this.InitData(1, true);
                }
                else
                {
                    this.InitData(Convert.ToInt32(Request["PageIndex"]), true);
                }
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            this.InitData(AspNetPager1.CurrentPageIndex, false);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            this.InitData(AspNetPager1.CurrentPageIndex, false);
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExPort_Click(object sender, EventArgs e)
        {
            string strWhereAdd = string.Empty;
            if (!string.IsNullOrEmpty(this.txtPA01003.Text.Trim()))
            {
                strWhereAdd = string.Format(" AND PA01003 LIKE '%{0}%' ", this.txtPA01003.Text.Trim());
            }
            DataSet ds = _ProductBLL.ExportProductBasePage(strWhereAdd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DownloadFile(ds, "产品资料.xls", "PA01.xml");
            }
            else
            {
                new Sinoo.Common.MessageShow().ExportErrorMessage(this);
            }
        }

    }
}