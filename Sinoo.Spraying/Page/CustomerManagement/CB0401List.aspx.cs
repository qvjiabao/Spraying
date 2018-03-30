using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;
using Sinoo.Model;

namespace Sinoo.Spraying.Page.CustomerManagement
{
    public partial class CB0401List : System.Web.UI.Page
    {

        CustomerBLL _CustomerBLL = new CustomerBLL();
        UserBLL _UserBLL = new UserBLL();
        public string DataCount = string.Empty;

        /// <summary>
        /// 初始化加载数据
        /// </summary>
        /// <param name="Index"></param>
        private void InitData(int Index)
        {

            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;
            ViewState["PageIndex"] = Index;

            string strWhere = string.Empty;  //Where语句
            object RowsCount = null;
            DataTable dt = _CustomerBLL.SelectCustomerTradeCodePage(AspNetPager1.PageSize, Index, strWhere, ref RowsCount);
            if (string.IsNullOrEmpty(strWhere))
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"4\">系统暂无数据显示</td></tr>" : DataCount;
            else
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"4\">没有符合查询条件的数据</td></tr>" : DataCount;

            rpCB04.DataSource = dt;
            //设置翻页控件的总行数
            AspNetPager1.RecordCount = Convert.ToInt32(RowsCount);
            AspNetPager1.CurrentPageIndex = Index;
            rpCB04.DataBind();
        }

        /// <summary>
        /// 窗体加载
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
                    DataRow[] dr = dt.Select("GA01003 = 'CB04btnNew'"); //新增按钮
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript1", "<script>RemoveNewCss();</script>");
                    }

                    dr = dt.Select("GA01003= 'CB04btnDel'");  //删除
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript4", "<script>RemoveDelCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'CB04btnEdit'");  //编辑
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript5", "<script>RemoveEditCss();</script>");
                    }

                }
            }

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["PageIndex"]))  //判断当前页
                {
                    this.InitData(1);
                }
                else
                {
                    this.InitData(Convert.ToInt32(Request["PageIndex"]));
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveData(object sender, CommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))  //判断是否有Id值
            {
                CustomerTradeCode _CustomerTradeCode = new CustomerTradeCode();
                _CustomerTradeCode.CB04001 = Convert.ToInt32(e.CommandArgument.ToString());
                _CustomerTradeCode.CB04997 = 1;
                int num = Math.Abs(_CustomerBLL.RemoveCustomerTradeCode(_CustomerTradeCode));
                new Sinoo.Common.MessageShow().RemoveMessage(this.Page, num, string.Empty);


                if (num < 1)
                {
                    return;
                }

                int CurrentPageIndex = this.AspNetPager1.CurrentPageIndex;//当前页
                int PageCount = this.AspNetPager1.PageCount;//页总数
                int RecordCount = this.AspNetPager1.RecordCount;//记录数
                int PageSize = this.AspNetPager1.PageSize;//第页条数

                int PageIndex = CurrentPageIndex == PageCount ? (PageCount * PageSize - RecordCount == (PageSize - 1) ? CurrentPageIndex - 1 : CurrentPageIndex) : CurrentPageIndex;

                InitData(PageIndex);

            }
        }

        /// <summary>
        /// 跳转新增页面
        /// </summary>
        private void LinkNew()
        {
            Response.Redirect(string.Format("CB0401New.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            LinkNew();
        }

        /// <summary>
        /// 分页时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            this.InitData(AspNetPager1.CurrentPageIndex);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            this.InitData(AspNetPager1.CurrentPageIndex);
        }
    }
}