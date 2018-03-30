using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Sinoo.BLL;

namespace Sinoo.Spraying.Page.UserManagement
{
    public partial class UA0101List : System.Web.UI.Page
    {

        // 实例化用户逻辑层
        UserBLL _UserBLL = new UserBLL();

        public string DataCount = string.Empty;

        /// <summary>
        /// 初次数据绑定
        /// </summary>
        private void InitData(int Index)
        {
            string strWhere = string.Empty;
            //用户中文名
            strWhere += (this.txtUA01004.Text.Trim() == "" ? "" : string.Format(" AND UA01004 LIKE '%{0}%'", this.txtUA01004.Text.Trim()));
            //用户英文名
            strWhere += (this.txtUA01005.Text.Trim() == "" ? "" : string.Format(" AND UA01005 LIKE '%{0}%'", this.txtUA01005.Text.Trim()));
            //用户中文名
            strWhere += (this.txtUA01006.Text.Trim() == "" ? "" : string.Format(" AND UA01006 LIKE '%{0}%'", this.txtUA01006.Text.Trim()));
            //用户职位
            strWhere += (this.ddlUA01009.SelectedValue == "" ? "" : string.Format(" AND UA01009 = {0}", this.ddlUA01009.SelectedValue));

            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;

            ViewState["PageIndex"] = Index;

            object RowsCount = null;
            DataTable dt = _UserBLL.SelectUserBase(AspNetPager1.PageSize, Index, strWhere, ref RowsCount);
            if (strWhere != "")
            {
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"9\">没有符合查询条件的数据</td></tr>" : DataCount;
            }
            else
            {
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"9\">系统暂无数据显示</td></tr>" : DataCount;
            }

            repUA01.DataSource = dt;

            //设置翻页控件的总行数
            AspNetPager1.RecordCount = Convert.ToInt32(RowsCount);

            AspNetPager1.CurrentPageIndex = Index;
            this.repUA01.DataBind();
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
                    DataRow[] dr = dt.Select("GA01003 = 'UA01btnNew'"); //新增按钮
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript1", "<script>RemoveNewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'UA01btnView'");  //浏览
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript4", "<script>RemoveViewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'UA01btnEdit'");  //编辑
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript5", "<script>RemoveEditCss();</script>");
                    }

                    dr = dt.Select("GA01003= 'UA01btnDel'");  //删除
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
        /// 新增事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("UA0101New.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            InitData(1);
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
                int result = _UserBLL.RemoveUserBase(e.CommandArgument.ToString());

                //成功失败提示
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

                InitData(PageIndex);
            }
        }

        /// <summary>
        /// 页改变
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            this.InitData(AspNetPager1.CurrentPageIndex);
        }
    }
}