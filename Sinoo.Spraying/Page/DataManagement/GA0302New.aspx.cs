using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using Sinoo.Model;
using System.Data;

namespace Sinoo.Spraying.Page.DataManagement
{
    public partial class GA0302New : BasePage
    {
        AreaBLL _AreaBLL = new AreaBLL(); //实例化

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            DataTable dt = _AreaBLL.SelectSystemAreaForProvince();
            this.drpProvince.DataSource = dt;
            this.drpProvince.DataValueField = "GA03001";
            this.drpProvince.DataTextField = "GA03002";
            this.drpProvince.DataBind();
            this.drpProvince.Items.Insert(0, new ListItem("请选择", ""));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            if (!string.IsNullOrEmpty(this.txtGA03002.Text))
            {

                string strid = _AreaBLL.CreateSystemAreaCityId(this.drpProvince.SelectedValue);  //获取ID

                SystemArea _SystemArea = new SystemArea();
                _SystemArea.GA03001 = strid;
                _SystemArea.GA03002 = this.txtGA03002.Text.Trim();
                _SystemArea.GA03003 = this.drpProvince.SelectedValue;
                _SystemArea.GA03004 = this.txtGA03004.Text.Trim();
                int num = Math.Abs(_AreaBLL.InsertSystemArea(_SystemArea));
                new Sinoo.Common.MessageShow().InsertMessage(this, num, "DataClear()");
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
                this.InitData();
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
            Response.Redirect(string.Format("GA0302List.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }

    }
}