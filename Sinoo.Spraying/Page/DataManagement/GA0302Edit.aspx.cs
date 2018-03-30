using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;
using Sinoo.Model;

namespace Sinoo.Spraying.Page.DataManagement
{
    public partial class GA0302Edit : IsRole
    {

        AreaBLL _AreaBLL = new AreaBLL(); //实例化

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GA03001"]))
            {
                DataTable dt = _AreaBLL.SelectSystemAreaById(Request.QueryString["GA03001"]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataTable dt1 = _AreaBLL.SelectSystemAreaForProvince();
                    this.drpProvince.DataSource = dt1;
                    this.drpProvince.DataValueField = "GA03001";
                    this.drpProvince.DataTextField = "GA03002";
                    this.drpProvince.DataBind();
                    this.drpProvince.Items.Insert(0, new ListItem("请选择", ""));
                    this.txtGA03002.Text = dt.Rows[0]["GA03002"].ToString();
                    this.txtGA03004.Text = dt.Rows[0]["GA03004"].ToString();
                    this.drpProvince.SelectedValue = dt.Rows[0]["GA03003"].ToString();
                    this.ID.Value = dt.Rows[0]["GA03001"].ToString();
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        private void SaveData()
        {
            if (!string.IsNullOrEmpty(this.txtGA03002.Text))
            {
                SystemArea _SystemArea = new SystemArea();
                _SystemArea.GA03001 = Request.QueryString["GA03001"];
                _SystemArea.GA03002 = this.txtGA03002.Text.Trim();
                _SystemArea.GA03003 = this.drpProvince.SelectedValue;
                _SystemArea.GA03004 = this.txtGA03004.Text.Trim();
                int num = Math.Abs(_AreaBLL.UpdateSystemArea(_SystemArea));
                new Sinoo.Common.MessageShow().UpdateMessage(this, num
                   , string.Format("location.href='{0}'", LinkReturn()));

            }
        }

        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <returns></returns>
        public string LinkReturn()
        {
            string url = string.Empty;

            url = string.Format("GA0302List.aspx?PageIndex={0}", ViewState["PageIndex"]);

            return url;
        }

        /// <summary>
        /// 窗体初始化
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
            Response.Redirect(this.LinkReturn());
        }

    }
}