using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Sinoo.BLL;
using Sinoo.Common;

namespace Sinoo.Spraying.Page.ReportManagement.DailyReport
{
    /// <summary>
    /// 到货统计
    /// </summary>
    public partial class OrderList : System.Web.UI.Page
    {

        ReportBLL _ReportBLL = new ReportBLL();

        ExcelBLL _ExcelBLL = new ExcelBLL();

        public string DataCount = string.Empty;

        /// <summary>
        /// 获取查询Sql语句
        /// </summary>
        /// <returns></returns>
        private string getSqlStr()
        {
            string sqlWhere = string.Empty;

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
                sqlWhere += string.Format(" AND CA01020 in({0})", _strjoin);
            }
            //拼接行业分类
            if (!string.IsNullOrEmpty(_strjoin1))
            {
                _strjoin1 = _strjoin1.Substring(0, _strjoin1.Length - 1);
                sqlWhere += string.Format(" AND CA01019  in({0})", _strjoin1);
            }
            if (!string.IsNullOrEmpty(OA01025))
            {
                OA01025 = OA01025.Substring(0, OA01025.Length - 1);
                sqlWhere += string.Format(" AND OA01025 in({0})", OA01025);
            }
            #endregion

            Model.UserBase _UserBase = Session["USER_SESSION"] as Model.UserBase;
            if (_UserBase.UA01009 == 1)  //如果是1 则为销售员
            {
                sqlWhere += string.Format(" AND UA01001 = '{0}'", _UserBase.UA01001);
            }
            if (!string.IsNullOrEmpty(this.txtOC01011Start.Text.Trim()))
            {
                sqlWhere += string.Format(" AND OA01009  >= '{0}'", this.txtOC01011Start.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOC01011End.Text.Trim()))
            {
                sqlWhere += string.Format(" AND OA01009 <= '{0}'", this.txtOC01011End.Text.Trim());
            }
            if (string.IsNullOrEmpty(this.txtOC01011Start.Text.Trim())
                && string.IsNullOrEmpty(this.txtOC01011End.Text.Trim())
                && string.IsNullOrEmpty(this.txtOC01998Start.Text.Trim())
                && string.IsNullOrEmpty(this.txtOC01998End.Text.Trim()))
            {
                sqlWhere += "AND DATEPART(DAY,GETDATE()) = DATEPART(DAY,OA01009) AND DATEPART(yy,GETDATE()) = DATEPART(yy,OA01009) AND DATEPART(month,GETDATE()) = DATEPART(month,OA01009)";

            }
            if (!string.IsNullOrEmpty(this.txtOC01998Start.Text.Trim()))
            {
                sqlWhere += string.Format(" AND OC01015  >= '{0}'", this.txtOC01998Start.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOC01998End.Text.Trim()))
            {
                sqlWhere += string.Format(" AND OC01015 <= '{0}'", this.txtOC01998End.Text.Trim());
            }


            if (this.rbtnOA01044Yes.Checked)
            {
                sqlWhere += "AND OA01044 = '1' ";
            }
            if (this.rbtnOA01044No.Checked)
            {
                sqlWhere += "AND OA01044 = '0' ";
            }
            if (!string.IsNullOrEmpty(this.ddlUA01013.SelectedValue))
            {
                sqlWhere += string.Format(" AND UA01013 = '{0}'", this.ddlUA01013.SelectedValue);
            }
            if (!string.IsNullOrEmpty(Request.Form["ddlUA01"]))
            {
                sqlWhere += string.Format(" AND UA01001 = '{0}'", Request.Form["ddlUA01"]);
            }
            if (this.rbtnOA010031.Checked == true)
            {
                sqlWhere += string.Format(" AND OA01002 NOT LIKE 'A%'");
            }
            if (this.rbtnOA010032.Checked == true)
            {
                sqlWhere += string.Format(" AND OA01002 LIKE 'A%'");
            }

            if (_UserBase.UA01024 == 44) //Leader权限：customer file、order file、arrival notice、order list、pending alert、weekly pending只能看到自己区域内的
            {
                if (!_UserBase.UA01013.Equals("全区域"))
                    sqlWhere += string.Format(" AND UA01013 = '{0}' ", _UserBase.UA01013);
            }

            return sqlWhere;
        }

        /// <summary>
        /// 加载页面数据
        /// </summary>
        private void InitData(int Index, bool bl)
        {
            string strSqlWhere = getSqlStr();

            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;

            ViewState["PageIndex"] = Index;

            object RowsCount = null;//存储过程返回总行数
            DataTable dt = _ReportBLL.SelectOrderList(AspNetPager1.PageSize, Index, strSqlWhere, ref RowsCount);

            if (dt.Rows.Count > 0)
            {
                DataCount = string.Empty;
            }
            else
            {
                if (bl)
                {
                    DataCount = "<tr><td colspan=\"35\">系统暂无数据显示</td></tr>";

                }
                else
                {
                    DataCount = "<tr><td colspan=\"35\">没有符合查询条件的数据</td></tr>";
                }
            }
            this.repOrderList.DataSource = dt;

            //设置翻页控件的总行数
            AspNetPager1.RecordCount = Convert.ToInt32(RowsCount);

            AspNetPager1.CurrentPageIndex = Index;

            this.repOrderList.DataBind();
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
        /// 加载事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime now = DateTime.Now;
                this.txtOC01011Start.Text = now.ToString("yyyy-MM-dd");
                this.txtOC01011End.Text = now.ToString("yyyy-MM-dd");
                if (string.IsNullOrEmpty(Request["PageIndex"]))  //判断当前页
                {
                    InitData(1, true);
                }
                else
                {
                    InitData(Convert.ToInt32(Request["PageIndex"]), true);
                }
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            InitData(1, false);
        }

        /// <summary>
        /// 翻页
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
            DataSet ds = _ExcelBLL.ExportOrderList(getSqlStr());
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DownloadFile(ds, "OrderList.xls", "OrderList.xml");
            }
            else
            {
                new Sinoo.Common.MessageShow().ExportErrorMessage(this);
            }
        }
    }
}