using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;
using Sinoo.Model;

namespace Sinoo.Spraying.Page.UserManagement
{
    public partial class GA0201List : System.Web.UI.Page
    {

        UserBLL _UserBLL = new UserBLL();  //实例化对象

        public string DataCount = string.Empty;

        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="Index">当前页</param>
        private void InitData(int Index)
        {

            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;
            ViewState["PageIndex"] = Index;

            string strwhere = string.Empty; //查询条件

            if (!string.IsNullOrEmpty(this.txtGA02002.Text.Trim()))  //判断是否有查询条件
            {
                strwhere = string.Format(" AND GA02002 LIKE '%{0}%' ", this.txtGA02002.Text.Trim());
            }
            object RowsCount = null;  //数据总行数
            DataTable dt = _UserBLL.SelectSystemRolePage(AspNetPager1.PageSize, Index, strwhere, ref RowsCount);
            if (string.IsNullOrEmpty(strwhere))
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"4\">系统暂无数据显示</td></tr>" : DataCount;
            else
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"4\">没有符合查询条件的数据</td></tr>" : DataCount;

            rpGA02.DataSource = dt;
            AspNetPager1.RecordCount = Convert.ToInt32(RowsCount);
            AspNetPager1.CurrentPageIndex = Index;
            rpGA02.DataBind();
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
                    DataRow[] dr = dt.Select("GA01003 = 'GA02btnNew'"); //新增按钮
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript1", "<script>RemoveNewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'GA02btnView'");  //浏览
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript4", "<script>RemoveViewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'GA02btnEdit'");  //编辑
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript5", "<script>RemoveEditCss();</script>");
                    }

                    dr = dt.Select("GA01003= 'GA02btnDel'");  //删除
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
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                SystemRole _SystemRole = new SystemRole();
                _SystemRole.GA02001 = Convert.ToInt32(e.CommandArgument.ToString());
                _SystemRole.GA02997 = 1;
                int num = Math.Abs(_UserBLL.RemoveSystemRole(_SystemRole));
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

                InitData(PageIndex);
            }
        }

        /// <summary>
        /// 跳转新增页面
        /// </summary>
        private void LinkGA0201New()
        {
            Response.Redirect(string.Format("GA0201New.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            LinkGA0201New();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            this.InitData(AspNetPager1.CurrentPageIndex);
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            this.InitData(AspNetPager1.CurrentPageIndex);
        }
    }
}