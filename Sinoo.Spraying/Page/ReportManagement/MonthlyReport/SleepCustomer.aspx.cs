using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Sinoo.BLL;
using Sinoo.Common;

namespace Sinoo.Spraying.Page.ReportManagement.MonthlyReport
{
    public partial class SleepCustomer : System.Web.UI.Page
    {


        ReportBLL _ReportBLL = new ReportBLL();  //对象实例

        UserBLL _UserBLL = new UserBLL();

        ExcelBLL _ExcelBLL = new ExcelBLL();

        public string DataCount = string.Empty;

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="Index">当前页</param>
        private void InitData(int Index, bool bl)
        {

            //保存下拉框查询条件
            if (!string.IsNullOrEmpty(Request.Form["ddlProvince"]) && Request.Form["ddlProvince"] != "")
            {
                DataTable dtUserBaseByAera = _UserBLL.SelectUserBaseByAera(string.Format("  AND UA01009 = 1 AND UA01013 = '{0}'", Request.Form["ddlProvince"]));
                this.ddlUA01004.DataSource = dtUserBaseByAera;
                this.ddlUA01004.DataTextField = "UA01004";
                this.ddlUA01004.DataValueField = "UA01001";
                this.ddlUA01004.DataBind();
                this.ddlUA01004.Items.Insert(0, new ListItem("请选择", ""));

                if (!string.IsNullOrEmpty(Request.Form["ddlUA01004"]) && Request.Form["ddlUA01004"] != "")
                {
                    this.ddlUA01004.Value = Request.Form["ddlUA01004"];
                }
            }


            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;
            object RowCount = null;
            string strWhereAdd = this.GetWhere(bl); //查询条件


            DataTable dt = _ReportBLL.SelectSleepCustomerPage(AspNetPager1.PageSize, Index, strWhereAdd, ref RowCount);
            if (dt.Rows.Count > 0)
            {
                DataCount = string.Empty;
            }
            else
            {
                if (bl)
                {
                    DataCount = "<tr><td colspan=\"9\">系统暂无数据显示</td></tr>";

                }
                else
                {
                    DataCount = "<tr><td colspan=\"9\">没有符合查询条件的数据</td></tr>";
                }
            }
            rpGA03.DataSource = dt;
            AspNetPager1.RecordCount = Convert.ToInt32(RowCount);
            AspNetPager1.CurrentPageIndex = Index;

            rpGA03.DataBind();
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
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        private string GetWhere(bool bl)
        {
            string strWhereAdd = string.Empty;

            #region 高级查询
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
            #endregion


            if (!string.IsNullOrEmpty(this.txtOrderNo.Text.Trim()))
            {
                strWhereAdd += string.Format(" AND OA01002 like '%{0}%'", txtOrderNo.Text.Trim());
            }

            if (!string.IsNullOrEmpty(Request.Form["OA01044"]) && Request.Form["OA01044"] != "all")
            {
                string str = Request.Form["OA01044"] == "Y" ? "1" : "0";
                strWhereAdd += string.Format(" AND OA01044 = {0}", str);
            }
            if (!string.IsNullOrEmpty(Request.Form["ddlUA01004"]) && Request.Form["ddlUA01004"] != "")
            {
                strWhereAdd += string.Format(" AND UA01001 = {0}", Request.Form["ddlUA01004"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["ddlProvince"]) && Request.Form["ddlProvince"] != "")
            {
                strWhereAdd += string.Format(" AND UA01013 = '{0}'", Request.Form["ddlProvince"]);
            }
            if (bl)
            {
                strWhereAdd += string.Format(" AND OA01002 NOT LIKE 'A%' ");
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.Form["OrderType"]) && Request.Form["OrderType"] != "all")
                {
                    string str = Request.Form["OrderType"] == "Nozzle" ? " NOT LIKE 'A%'" : " LIKE 'A%'";
                    strWhereAdd += string.Format(" AND OA01002  {0}", str);
                }
            }
            return strWhereAdd;
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

                ViewState["PageIndex"] = string.IsNullOrEmpty(Request["PageIndex"])
                    ? "1"
                    : Request["PageIndex"];

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
            this.InitData(AspNetPager1.CurrentPageIndex, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            this.InitData(Convert.ToInt32(ViewState["PageIndex"]), false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExPort_Click(object sender, EventArgs e)
        {
            string strWhereAdd = this.GetWhere(false); //获取查询条件
            DataSet dt = _ExcelBLL.ExportSleepCustomerPage(strWhereAdd);
            if (dt.Tables[0].Rows.Count > 0)
            {
                this.DownloadFile(dt, "SleepCustomer.xls", "SleepCustomer.xml");
            }
            else
            {
                new Sinoo.Common.MessageShow().ExportErrorMessage(this);
            }
        }

    }
}