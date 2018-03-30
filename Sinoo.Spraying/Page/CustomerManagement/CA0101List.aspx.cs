using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Sinoo.BLL;
using Sinoo.Common;

namespace Sinoo.Spraying.Page.CustomerManagement
{
    public partial class CA0101List : System.Web.UI.Page
    {
        //实例化逻辑层
        CustomerBLL _CustomerBLL = new CustomerBLL();

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
            string strWhere = string.Empty;
            //行业分类
            strWhere += (this.ddlCA01019.SelectedValue == "" ? "" : string.Format(" AND CB03001 = {0}", this.ddlCA01019.SelectedValue));
            //客户代码
            strWhere += (this.ddlCA01018.SelectedValue == "" ? "" : string.Format(" AND CB02001 = {0}", this.ddlCA01018.SelectedValue));
            //客户类型
            strWhere += (this.ddlCA01016.SelectedValue == "" ? "" : string.Format(" AND CB01001 = {0}", this.ddlCA01016.SelectedValue));
            //所属省份
            strWhere += (this.ddlGA03Province.SelectedValue == "" ? "" : string.Format(" AND ProvinceId = '{0}'", this.ddlGA03Province.SelectedValue));
            //所属城市
            strWhere += (string.IsNullOrEmpty(Request.Form["ddlGA03City"]) ? "" : string.Format(" AND CityId = '{0}'", Request.Form["ddlGA03City"]));
            //客户编码
            strWhere += (this.txtCA01002.Text.Trim() == "" ? "" : string.Format(" AND CA01002 LIKE '%{0}%'", this.txtCA01002.Text.Trim()));
            //客户名称
            strWhere += (this.txtCA01003.Text.Trim() == "" ? "" : string.Format(" AND CA01003 LIKE '%{0}%'", this.txtCA01003.Text.Trim()));
            //行业代码
            strWhere += (this.ddlCA01020.SelectedValue == "" ? "" : string.Format(" AND CA01020 = '{0}'", this.ddlCA01020.SelectedValue));

            if (this.rbtnCA01024Yes.Checked) strWhere += " AND CA01024 = '1' ";
            if (this.rbtnCA01024No.Checked) strWhere += " AND CA01024 = '0' ";
            if (this.rbtnCA01025Yes.Checked) strWhere += " AND CA01025 = '1' ";
            if (this.rbtnCA01025No.Checked) strWhere += " AND CA01025 = '0' ";
            if (this.rbtnCA01047Yes.Checked) strWhere += " AND CA01047 = '1' ";
            if (this.rbtnCA01047No.Checked) strWhere += " AND CA01047 = '0' ";

            return strWhere;
        }

        /// <summary>
        /// 根据查询条件绑定数据
        /// </summary>
        private void InitData(int Index, bool bl)
        {
            string strWhere = GetSqlWhere();

            //dt = _CustomerBLL.SelectCustomerBaseForList(strWhere);

            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;


            ViewState["PageIndex"] = Index;

            object RowsCount = null;
            DataTable dt = _CustomerBLL.SelectCustomerBase(AspNetPager1.PageSize, Index, strWhere, ref RowsCount);
            if (dt.Rows.Count > 0)
            {
                DataCount = string.Empty;
            }
            else
            {
                if (bl)
                {
                    DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"9\">系统暂无数据显示</td></tr>" : DataCount;

                }
                else
                {
                    DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"9\">没有符合查询条件的数据</td></tr>" : DataCount;
                }
            }
            this.repUA01.DataSource = dt;

            //设置翻页控件的总行数
            AspNetPager1.RecordCount = Convert.ToInt32(RowsCount);

            AspNetPager1.CurrentPageIndex = Index;

            this.repUA01.DataBind();
        }

        /// <summary>
        /// 新增页面
        /// </summary>
        private void LinkNew()
        {
            Response.Redirect(string.Format("CA0101New.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }

        /// <summary>
        /// 绑定页面查询条件Dll
        /// </summary>
        private void BindDll()
        {
            //获取省
            DataTable dtGA03 = _AreaBLL.SelectSystemAreaForProvince();

            this.ddlGA03Province.DataSource = dtGA03;
            this.ddlGA03Province.DataTextField = "GA03002";
            this.ddlGA03Province.DataValueField = "GA03001";
            this.ddlGA03Province.DataBind();
            this.ddlGA03Province.Items.Insert(0, new ListItem("请选择", ""));

            //为市添加请选择选项
            //this.ddlGA03City.Items.Insert(0, new ListItem("请选择", ""));

            //行业代码
            DataTable dtCB04 = _CustomerBLL.SelectCustomerTradeCode();
            this.ddlCA01020.DataSource = dtCB04;
            this.ddlCA01020.DataTextField = "CB04002";
            this.ddlCA01020.DataValueField = "CB04001";
            this.ddlCA01020.DataBind();
            this.ddlCA01020.Items.Insert(0, new ListItem("请选择", ""));
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
            Response.AddHeader("Content-Disposition", "attachment;  filename="
                + HttpUtility.UrlEncode(strExcelName, System.Text.Encoding.UTF8));
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
            if (Session["USER_SESSION"] != null)
            {
                Model.UserBase _UserBase = Session["USER_SESSION"] as Model.UserBase;

                DataTable dt = _UserBLL.SelectSystemRole(Convert.ToInt32(_UserBase.UA01024));
                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("GA01003 = 'CA01btnNew'"); //新增按钮
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript1", "<script>RemoveNewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'CA01btnExport'");  //导出
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript2", "<script>RemoveExportCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'CA01btnImport'");  //导入
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript3", "<script>RemoveImportCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'CA01btnView'");  //浏览
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript4", "<script>RemoveViewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'CA01btnEdit'");  //编辑
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript5", "<script>RemoveEditCss();</script>");
                    }

                    dr = dt.Select("GA01003= 'CA01btnDel'");  //删除
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript6", "<script>RemoveDelCss();</script>");
                    }

                    dr = dt.Select("GA01003= 'CA01btnAddOrder'");  //添加订单
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript7", "<script>RemoveAddOrderCss();</script>");
                    }
                }
            }

            if (!IsPostBack)
            {
                #region 绑定 MDT：行业分类

                DataTable DataCB03 = _CustomerBLL.SelectCustomerTradeTypeByCustomerTradeCode();
                this.ddlCA01019.DataSource = DataCB03;
                this.ddlCA01019.DataTextField = "CB03002";
                this.ddlCA01019.DataValueField = "CB03001";
                this.ddlCA01019.DataBind();
                this.ddlCA01019.Items.Insert(0, new ListItem("请选择", ""));
                #endregion

                #region 绑定 CTC：客户代码
                DataTable DataCB02 = _CustomerBLL.SelectCustomerCode();
                this.ddlCA01018.DataSource = DataCB02;
                this.ddlCA01018.DataTextField = "CB02002";
                this.ddlCA01018.DataValueField = "CB02001";
                this.ddlCA01018.DataBind();
                this.ddlCA01018.Items.Insert(0, new ListItem("请选择", ""));
                #endregion

                #region 绑定 CTC：客户类型
                DataTable DataCB01 = _CustomerBLL.SelectCustomerType();
                this.ddlCA01016.DataSource = DataCB01;
                this.ddlCA01016.DataTextField = "CB01002";
                this.ddlCA01016.DataValueField = "CB01001";
                this.ddlCA01016.DataBind();
                this.ddlCA01016.Items.Insert(0, new ListItem("请选择", ""));
                #endregion

                BindDll();
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
            InitData(1, false);
        }

        /// <summary>
        /// 新增点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            LinkNew();
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
                int result = _CustomerBLL.RemoveCustomerBaseById(e.CommandArgument.ToString());

                //结果提示
                new Sinoo.Common.MessageShow().RemoveMessage(this.Page, result, "");

                if (result < 1)
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
        /// 分布事件
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
            DataSet ds = _ExcelBLL.ExportCustomerBase(GetSqlWhere());
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DownloadFile(ds, "客户资料.xls", "CA01.xml");
            }
            else
            {
                new Sinoo.Common.MessageShow().ExportErrorMessage(this);
            }
        }

    }
}