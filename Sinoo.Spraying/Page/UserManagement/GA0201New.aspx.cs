using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using Sinoo.Model;
using System.Data.SqlClient;

namespace Sinoo.Spraying.Page.UserManagement
{
    public partial class GA0201New : BasePage
    {
        UserBLL _UserBLL = new UserBLL();  //实例化

        /// <summary>
        /// 保存
        /// </summary>
        private void SaveData()
        {
            if (!string.IsNullOrEmpty(this.txtGA02002.Text))
            {
                SystemRole _SystemRole = new SystemRole();
                _SystemRole.GA02002 = this.txtGA02002.Text.Trim();
                _SystemRole.GA02003 = this.txtGA02003.Text.Trim();
                if (this.txtMenu.Value == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('请选择菜单！')</script>");
                    return;
                }
                _SystemRole.MenuId = this.txtMenu.Value;//判断是否有选中菜单
                _SystemRole.PowerId = string.IsNullOrEmpty(this.txtPower.Value) ? "" : this.txtPower.Value;//判断是否有选中菜单

                int num = Math.Abs(_UserBLL.InsertSystemRole(_SystemRole));  //执行新增操作
                new Sinoo.Common.MessageShow().InsertMessage(this, num, "DataClear();");
            }
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
            }
        }

        /// <summary>
        /// 保存
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
            Response.Redirect(string.Format("GA0201List.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }


    }
}