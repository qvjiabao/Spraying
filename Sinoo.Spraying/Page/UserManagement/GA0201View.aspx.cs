using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.Model;
using Sinoo.BLL;
using System.Data;

namespace Sinoo.Spraying.Page.UserManagement
{
    public partial class GA0201View : IsRole
    {

        UserBLL _UserBLL = new UserBLL(); //实例化

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            DataTable dt = _UserBLL.SelectSystemRoleById(ViewState["GA02001"].ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                this.txtGA02001.Value = dt.Rows[0]["GA02001"].ToString();
                this.labGA02002.Text = dt.Rows[0]["GA02002"].ToString();
                this.labGA02003.Text = dt.Rows[0]["GA02003"].ToString();
            }

        }

        /// <summary>
        /// 跳转编辑页面
        /// </summary>
        private void LinkUpdate()
        {
            Response.Redirect(string.Format("GA0201Edit.aspx?GA02001={0}&PageIndex={1}"
                , ViewState["GA02001"].ToString(), ViewState["PageIndex"].ToString()));
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            this.IsRose(); //验证权限
            if (!IsPostBack)
            {
                ViewState["PageIndex"] = Request["PageIndex"];

                if (!string.IsNullOrEmpty(Request.QueryString["GA02001"]))
                {
                    ViewState["GA02001"] = Request.QueryString["GA02001"];
                    this.InitData();
                }

            }
        }

        /// <summary>
        /// 跳转编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            this.LinkUpdate();
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("GA0201List.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }
    }
}