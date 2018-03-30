using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.Model;
using Sinoo.BLL;
using System.Data;

namespace Sinoo.Spraying.Page.CustomerManagement
{
    public partial class CB0401Edit : IsRole
    {
        CustomerBLL _CustomerBLL = new CustomerBLL(); //实例对象

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {

            this.drpCB04003.DataSource = _CustomerBLL.SelectCustomerTradeType();
            this.drpCB04003.DataValueField = "CB03001";
            this.drpCB04003.DataTextField = "CB03002";
            this.drpCB04003.DataBind();
            this.drpCB04003.Items.Insert(0, new ListItem("请选择", ""));        //所属行业分类

            if (!string.IsNullOrEmpty(Request.QueryString["CB04001"]))
            {
                DataTable dt = _CustomerBLL.SelectCustomerTradeCodeById(Request.QueryString["CB04001"]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.txtCB04002.Text = dt.Rows[0]["CB04002"].ToString();
                    this.drpCB04003.SelectedValue = dt.Rows[0]["CB04003"].ToString();
                    this.txtCB04005.Text = dt.Rows[0]["CB04005"].ToString();
                    this.ID.Value = dt.Rows[0]["CB04001"].ToString();
                }
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            if (!string.IsNullOrEmpty(this.txtCB04002.Text))
            {

                CustomerTradeCode _CustomerTradeCode = new CustomerTradeCode();
                _CustomerTradeCode.CB04001 = Convert.ToInt32(Request.QueryString["CB04001"]);
                _CustomerTradeCode.CB04002 = this.txtCB04002.Text.Trim();
                _CustomerTradeCode.CB04003 = Convert.ToInt32(this.drpCB04003.SelectedValue);
                _CustomerTradeCode.CB04005 = this.txtCB04005.Text.Trim();
                int num = Math.Abs(_CustomerBLL.UpdateCustomerTradeCode(_CustomerTradeCode));  //执行更新操作
                new Sinoo.Common.MessageShow().UpdateMessage(this
                    , num
                    , string.Format("location.href='{0}'", LinkReturn()));

            }
        }

        /// <summary>
        /// 返回路径
        /// </summary>
        /// <returns></returns>
        public string LinkReturn()
        {
            string url = string.Empty;

            url = string.Format("CB0401List.aspx?PageIndex={0}", ViewState["PageIndex"]);

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
        /// 编辑
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