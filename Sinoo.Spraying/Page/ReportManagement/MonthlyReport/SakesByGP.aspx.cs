using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;
using Sinoo.Common;

namespace Sinoo.Spraying.Page.ReportManagement.MonthlyReport
{
    public partial class SakesByGP : BasePage
    {
        ReportBLL _ReportBLL = new ReportBLL();  //对象实例

        ExcelBLL _ExcelBLL = new ExcelBLL();

        public string DataCount = string.Empty;

        /// <summary>
        /// 获取页面条件Where语句
        /// </summary>
        /// <returns></returns>
        public string GetSqlWhere()
        {
            string strWhereAdd = string.Empty;
            List<string> _list1 = new List<string>();//行业分类集合
            string SIC = this.UserControl.CB03001;  //行业分类和代码
            string OA01025 = this.UserControl.OB02001;//应用代码
            List<string> _list = new List<string>();
            string _strjoin = string.Empty; //行业代码
            string _strjoin1 = string.Empty; //行业分类
            if (!string.IsNullOrEmpty(SIC))
            {

                string[] sic = SIC.Trim().Split(',');
                for (int i = 0; i < sic.Length; i++)
                {
                    if (!string.IsNullOrEmpty(sic[i]))
                    {
                        int num = sic[i].IndexOf("child");
                        if (num == 0)
                        {
                            _list.Add(sic[i]);
                        }
                        else
                        {
                            _list1.Add(sic[i]);
                        }
                    }
                }

                foreach (string item in _list)
                {
                    _strjoin += item.Replace("child", " ").Trim() + ",";
                }
                foreach (string item1 in _list1)
                {
                    _strjoin1 += item1 + ",";
                }
            }
            //拼接行业代码
            if (!string.IsNullOrEmpty(_strjoin))
            {
                _strjoin = _strjoin.Substring(0, _strjoin.Length - 1);
                strWhereAdd += string.Format(" AND CA01020 in({0})", _strjoin);
            }
            //拼接行业分类
            if (!string.IsNullOrEmpty(_strjoin1))
            {
                _strjoin1 = _strjoin1.Substring(0, _strjoin1.Length - 1);
                strWhereAdd += string.Format(" AND CA01019  in({0})", _strjoin1);
            }
            if (!string.IsNullOrEmpty(OA01025))
            {
                OA01025 = OA01025.Substring(0, OA01025.Length - 1);
                strWhereAdd += string.Format(" AND OA01025 in({0})", OA01025);
            }
            if (!string.IsNullOrEmpty(this.txtBeginOrderTime.Text.Trim()))
            {
                strWhereAdd += string.Format(" AND OA01009 >= '{0}'", txtBeginOrderTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtEndOrderTime.Text.Trim()))
            {
                strWhereAdd += string.Format(" AND OA01009 <= '{0}'", txtEndOrderTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtBeginInvoiceTime.Text.Trim()))
            {
                strWhereAdd += string.Format(" AND OC01015 >='{0}' ", txtBeginInvoiceTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtEndInvoiceTime.Text.Trim()))
            {
                strWhereAdd += string.Format(" AND OC01015  <='{0}' ", txtEndInvoiceTime.Text.Trim());
            }
            //if (string.IsNullOrEmpty(this.txtBeginOrderTime.Text.Trim())
            //    && string.IsNullOrEmpty(this.txtEndOrderTime.Text.Trim())
            //    && string.IsNullOrEmpty(this.txtBeginInvoiceTime.Text.Trim())
            //    && string.IsNullOrEmpty(this.txtEndInvoiceTime.Text.Trim()))
            //{
            //    strWhereAdd += string.Format("AND OA01009 between {0} and {1}", "DATEADD(mm, DATEDIFF(mm,0,getdate()), 0)", "DATEADD(Day,-1,CONVERT(char(8),DATEADD(Month,1,GETDATE()),120)+'1')"); ;
            //}
            if (!string.IsNullOrEmpty(this.txtOA01002.Text.Trim()))
            {
                strWhereAdd += string.Format(" AND OA01002 like '%{0}%' ", txtOA01002.Text.Trim());
            }
            if (this.rbtnOA010031.Checked == true)
            {
                strWhereAdd += string.Format(" AND OA01002 NOT LIKE 'A%'");
            }
            if (this.rbtnOA010032.Checked == true)
            {
                strWhereAdd += string.Format(" AND OA01002 LIKE 'A%'");
            }
            if (this.rbtnOA01044Yes.Checked)
            {
                strWhereAdd += "AND OA01044 = '1' ";
            }
            if (this.rbtnOA01044No.Checked)
            {
                strWhereAdd += "AND OA01044 = '0' ";
            }
            if (!string.IsNullOrEmpty(Request.Form["ddlUA01"]))
            {
                strWhereAdd += string.Format(" AND OA01013 = {0}", Request.Form["ddlUA01"]);
            }

            if (!string.IsNullOrEmpty(this.ddlUA01013.SelectedValue) && this.ddlUA01013.SelectedValue != "全区域")
            {
                if (this.ddlUA01013.SelectedValue == "North")
                {
                    strWhereAdd += " AND UA01013 not in ('Fluid Air','BOF')";
                }
                else
                {
                    strWhereAdd += string.Format(" AND UA01013 = '{0}'", this.ddlUA01013.SelectedValue);
                }
            }

            return strWhereAdd;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="Index">当前页</param>
        private void InitData(int Index,bool bl)
        {
            string strWhere = GetSqlWhere();
            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;
            object RowCount = null; 
            bool blInvoice = (!string.IsNullOrEmpty(this.txtBeginInvoiceTime.Text.Trim()) || !string.IsNullOrEmpty(this.txtEndInvoiceTime.Text.Trim()));
            DataTable dt = _ReportBLL.SelectMonthlySakesByGPPage(AspNetPager1.PageSize, Index, strWhere,blInvoice,ref RowCount);
            if (dt.Rows.Count > 0)
            {
                DataCount = string.Empty;
            }
            else
            {
                if (bl)
                {
                    DataCount = "<tr><td colspan=\"6\">系统暂无数据显示</td></tr>";
                }
                else
                {
                    DataCount = "<tr><td colspan=\"6\">没有符合查询条件的数据</td></tr>";
                }
            }
            repSIC.DataSource = dt;
            AspNetPager1.RecordCount = Convert.ToInt32(RowCount);
            AspNetPager1.CurrentPageIndex = Index;

            repSIC.DataBind();
        }

        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            bool blInvoice = (!string.IsNullOrEmpty(this.txtBeginInvoiceTime.Text.Trim()) || !string.IsNullOrEmpty(this.txtEndInvoiceTime.Text.Trim()));
            DataSet ds = _ExcelBLL.ExportSakesByGP(GetSqlWhere(), blInvoice);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DownloadFile(ds, "SakesByGP.xls", "Profit.xml");
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
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //获取团队
                this.ddlUA01013.DataSource = this.GetTeamData();
                this.ddlUA01013.DataTextField = "Value";
                this.ddlUA01013.DataValueField = "Key";
                this.ddlUA01013.DataBind();

                ViewState["PageIndex"] = string.IsNullOrEmpty(Request["PageIndex"])
                    ? "1"
                    : Request["PageIndex"];
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                DateTime d2 = d1.AddMonths(1).AddDays(-1);
                this.txtBeginOrderTime.Text = d1.ToString("yyyy-MM-dd");
                this.txtEndOrderTime.Text = d2.ToString("yyyy-MM-dd");

                this.InitData(Convert.ToInt32(ViewState["PageIndex"]), true);
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
            this.InitData(AspNetPager1.CurrentPageIndex,false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            this.InitData(Convert.ToInt32(ViewState["PageIndex"]),false);
        }
    }
}