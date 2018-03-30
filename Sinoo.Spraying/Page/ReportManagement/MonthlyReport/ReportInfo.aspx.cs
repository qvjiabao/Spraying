using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using Sinoo.Common;
using Sinoo.Model;

namespace Sinoo.Spraying.Page.ReportManagement.MonthlyReport
{
    public partial class ReportInfo : System.Web.UI.Page
    {
        //实例化逻辑层
        OrderBLL _OrderBLL = new OrderBLL();

        //实例化省市逻辑层
        AreaBLL _AreaBLL = new AreaBLL();

        ExcelBLL _ExcelBLL = new ExcelBLL();

        UserBLL _UserBLL = new UserBLL();

        public string DataCount = string.Empty;

        /// <summary>
        /// 获取页面条件Where语句
        /// </summary>
        /// <returns></returns>
        public string GetSqlWhere()
        {

            return Session["Report"].ToString();
        }

        /// <summary>
        /// 根据查询条件绑定数据
        /// </summary>
        private void InitData(int Index, bool bl)
        {
            string strWhere = GetSqlWhere();
            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;
            ViewState["PageIndex"] = Index;
            object RowsCount = null;
            DataTable dt = _OrderBLL.SelectOrderBaseReportForList(AspNetPager1.PageSize, Index, strWhere, ref RowsCount);
            if (dt.Rows.Count == 0)
            {
                DataCount = "<tr><td colspan=\"11\">系统暂无数据显示</td></tr>";
            }
            this.repOA01.DataSource = dt;
            //设置翻页控件的总行数
            AspNetPager1.RecordCount = Convert.ToInt32(RowsCount);
            AspNetPager1.CurrentPageIndex = Index;
            this.repOA01.DataBind();
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
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
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            this.InitData(1, false);
        }


        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            this.InitData(AspNetPager1.CurrentPageIndex, false);
        }

        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            List<string> _list = new List<string>();
            //string[] strXmlName = { "OA01.xml", "OB01.xml", "OC01.xml", "OP01.xml" };
            //string[] strXmlName = { };
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable dtOA01 = _OrderBLL.ExportSelectOrderBaseReport(GetSqlWhere()).Tables[0]; //订单
            dtOA01.TableName = "OA01";

            string OA01Guid = "'AA',"; //订单GUID
            if (dtOA01.Rows.Count > 0)
            {
                ds.Tables.Add(dtOA01.Copy()); //添加订单数据
                _list.Add("OA01.xml");
                foreach (DataRow item in dtOA01.Rows)
                {
                    OA01Guid += "'" + item["OA01999"].ToString() + "',";
                }
            }

            OA01Guid = OA01Guid.Substring(0, OA01Guid.Length - 1);
            DataTable dtOB01 = _OrderBLL.ExportSelectProduct(OA01Guid).Tables[0];
            dtOB01.TableName = "OB01";
            string OB01Guid = "'AA',"; //商品GUID
            if (dtOB01.Rows.Count > 0)
            {
                ds.Tables.Add(dtOB01.Copy()); //添加商品数据
                _list.Add("OB01.xml");
                foreach (DataRow item in dtOB01.Rows)
                {
                    OB01Guid += "'" + item["OB01999"].ToString() + "',";
                }

            }
            OB01Guid = OB01Guid.Substring(0, OB01Guid.Length - 1);
            DataTable dtOC01 = _OrderBLL.ExportSelectInvoice(OB01Guid).Tables[0]; //发票
            dtOC01.TableName = "OC01";
            if (dtOC01.Rows.Count > 0)
            {
                ds.Tables.Add(dtOC01.Copy());
                _list.Add("OC01.xml");
            }


            DataTable dtOP01 = _OrderBLL.ExportSelectPayment(OA01Guid).Tables[0]; //付款
            dtOP01.TableName = "OP01";
            if (dtOP01.Rows.Count > 0)
            {
                ds.Tables.Add(dtOP01.Copy());
                _list.Add("OP01.xml");
            }

            string[] strXmlName = new string[_list.Count];
            int i = 0;
            foreach (string item in _list)
            {
                if (i < _list.Count)
                {
                    strXmlName[i] = item;
                    i++;
                }
            }


            if (ds.Tables.Count > 0)
            {
                this.DownloadFile(ds, "订单资料.xls", strXmlName);
            }
            else
            {
                new Sinoo.Common.MessageShow().ExportErrorMessage(this);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="strExcelName">下载的Excel文件名</param>
        /// <param name="strXmlName">xml文件名</param>
        private void DownloadFile(DataSet ds, string strExcelName, string[] strXmlName)
        {

            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(strExcelName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(new ExcelHandler().ExportExcel(ds, strXmlName));
            Response.Flush();
            Response.End();
        }

    }
}