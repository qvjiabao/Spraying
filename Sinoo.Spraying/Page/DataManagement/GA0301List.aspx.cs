using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.Model;
using System.Data;
using Sinoo.BLL;

namespace Sinoo.Spraying.Page.DataManagement
{
    public partial class GA0301List : System.Web.UI.Page
    {
        AreaBLL _AreaBLL = new AreaBLL();  //对象实例
        UserBLL _UserBLL = new UserBLL();
        public string DataCount = string.Empty;

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="Index"></param>
        private void InitData(int Index)
        {
            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;
            ViewState["PageIndex"] = Index;
            object RowCount = null;
            string strWhereAdd = string.Empty;
            DataTable dt = _AreaBLL.SelectSystemAreaProvincePage(AspNetPager1.PageSize, Index, strWhereAdd, ref RowCount);
            if (string.IsNullOrEmpty(strWhereAdd))
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"4\">系统暂无数据显示</td></tr>" : DataCount;
            else
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"4\">没有符合查询条件的数据</td></tr>" : DataCount;

            rpGA03.DataSource = dt;
            AspNetPager1.RecordCount = Convert.ToInt32(RowCount);
            AspNetPager1.CurrentPageIndex = Index;

            rpGA03.DataBind();
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
                DataTable dt = _AreaBLL.SelectSystemAreaForCity(e.CommandArgument.ToString());
                if (dt.Rows.Count > 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType()
                       , "message"
                       , "<script language='javascript' defer> "
                       + "alert('被删除省份存在关联的城市，请先删除城市后，再尝试删除该省份'); "
                       + " </script>");
                }
                else
                {
                    SystemArea _SystemArea = new SystemArea();
                    _SystemArea.GA03001 = e.CommandArgument.ToString();
                    _SystemArea.GA03997 = 1;
                    int num = Math.Abs(_AreaBLL.RemoveSystemArea(_SystemArea));
                    new Sinoo.Common.MessageShow().RemoveMessage(this, num, "");

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
        }

        /// <summary>
        /// 跳转新增页面
        /// </summary>
        private void LinkNew()
        {
            Response.Redirect(string.Format("GA0301New.aspx?PageIndex={0}", ViewState["PageIndex"]));
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
                    DataRow[] dr = dt.Select("GA01003 = 'GA0301btnNew'"); //新增按钮
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript1", "<script>RemoveNewCss();</script>");
                    }

                    dr = dt.Select("GA01003= 'GA0301btnEdit'");  //编辑
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript5", "<script>RemoveEditCss();</script>");
                    }

                    dr = dt.Select("GA01003= 'GA0301btnDel'");  //删除
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
                    ViewState["PageIndex"] = 1;
                    this.InitData(1);
                }
                else
                {
                    this.InitData(Convert.ToInt32(Request["PageIndex"]));
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
            this.InitData(AspNetPager1.CurrentPageIndex);
        }

    }
}