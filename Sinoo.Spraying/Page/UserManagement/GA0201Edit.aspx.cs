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
    public partial class GA0201Edit : BasePage
    {

        UserBLL _UserBLL = new UserBLL();  //实例化

        /// <summary>
        /// 初始化信息
        /// </summary>
        private void InitData()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GA02001"]))
            {
                DataTable dt = _UserBLL.SelectSystemRoleById(Request.QueryString["GA02001"]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.ID.Value = dt.Rows[0]["GA02001"].ToString();
                    this.txtGA02002.Text = dt.Rows[0]["GA02002"].ToString();
                    this.txtGA02003.Text = dt.Rows[0]["GA02003"].ToString();

                }
            }
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        private void SaveData()
        {
            if (!string.IsNullOrEmpty(this.txtGA02002.Text))
            {

                SystemRole _SystemRole = new SystemRole();
                _SystemRole.GA02001 = Convert.ToInt32(Request.QueryString["GA02001"]);
                _SystemRole.GA02002 = this.txtGA02002.Text.Trim();
                _SystemRole.GA02003 = this.txtGA02003.Text.Trim();
                if (this.txtMenu.Value == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('请选择菜单！')</script>");
                    return;
                }
                _SystemRole.MenuId = this.txtMenu.Value;//判断是否有选中菜单
                _SystemRole.PowerId = string.IsNullOrEmpty(this.txtPower.Value) ? "" : this.txtPower.Value;//判断是否有选中菜单
                int num = Math.Abs(_UserBLL.UpdateSystemRole(_SystemRole));  //执行更新操作
                new Sinoo.Common.MessageShow().UpdateMessage(this
                    , num
                    , string.Format("location.href='{0}'", LinkReturn()));

            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <returns></returns>
        public string LinkReturn()
        {
            string url = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["type"])   //判断返回是列表还是浏览
                && Request.QueryString["type"] == "list")
            {

                url = string.Format("GA0201List.aspx?PageIndex={0}", ViewState["PageIndex"]);
            }
            else
            {
                url = string.Format("GA0201View.aspx?PageIndex={0}&GA02001={1}"
                    , ViewState["PageIndex"], Request.QueryString["GA02001"]);
            }
            return url;
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
                this.InitData();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData();
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkReturn());
        }
    }
}