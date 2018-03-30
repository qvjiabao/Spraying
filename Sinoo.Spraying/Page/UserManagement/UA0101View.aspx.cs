using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;

namespace Sinoo.Spraying.Page.UserManagement
{
    public partial class UA0101View : IsRole
    {
        //实例化逻辑层
        UserBLL _UserManagementLogic = new UserBLL();


        /// <summary>
        /// 数据初始化绑定
        /// </summary>
        private void DataBind()
        {
            if (string.IsNullOrEmpty(Request.QueryString["UA01001"]))
            {
                Response.Redirect("UA0101List.ASPX");
            }
            DataTable dt = _UserManagementLogic.SelectUserBase(string.Format(" AND UA01001 = '{0}'", Request.QueryString["UA01001"]));

            this.labUA01002.Text = dt.Rows[0]["UA01002"].ToString();
            ViewState["password"] = dt.Rows[0]["UA01003"].ToString();
            this.labUA01004.Text = dt.Rows[0]["UA01004"].ToString();
            this.labUA01005.Text = dt.Rows[0]["UA01005"].ToString();
            this.labUA01006.Text = dt.Rows[0]["UA01006"].ToString();
            this.labUA01007.Text = dt.Rows[0]["UA01007"].ToString();
            this.labUA01009.Text = dt.Rows[0]["UB01002"].ToString();
            this.labUA01010.Text = dt.Rows[0]["UA01010"].ToString();
            this.labUA01011.Text = dt.Rows[0]["UA01011"].ToString();
            this.labUA01012.Text = dt.Rows[0]["UA01012"].ToString();
            this.labUA01013.Text = dt.Rows[0]["UA01013"].ToString();
            this.labUA01014.Text = dt.Rows[0]["UA01014"].ToString();
            this.labUA01015.Text = dt.Rows[0]["UA01015"].ToString();
            this.labUA01018.Text = dt.Rows[0]["UA01018"].ToString();
            this.labUA01021.Text = dt.Rows[0]["UA01021"].ToString();
            this.labUA01024.Text = dt.Rows[0]["GA02002"].ToString();

            this.labUA01008.Text = Convert.ToBoolean(dt.Rows[0]["UA01008"]) ? "男" : "女";
            this.labUA01016.Text = string.IsNullOrEmpty(dt.Rows[0]["UA01016"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["UA01016"]).ToString("yyyy-MM-dd");
            this.labUA01017.Text = string.IsNullOrEmpty(dt.Rows[0]["UA01017"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["UA01017"]).ToString("yyyy-MM-dd") + "  " + dt.Rows[0]["UA01025"].ToString();
            this.labUA01019.Text = string.IsNullOrEmpty(dt.Rows[0]["UA01019"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["UA01019"]).ToString("yyyy-MM-dd");
            this.labUA01020.Text = string.IsNullOrEmpty(dt.Rows[0]["UA01020"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["UA01020"]).ToString("yyyy-MM-dd");
            this.labUA01022.Text = string.IsNullOrEmpty(dt.Rows[0]["UA01022"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["UA01016"]).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 编辑页面跳转
        /// </summary>
        private void LinkEdit()
        {
            Response.Redirect(string.Format("UA0101Edit.aspx?UA01001={0}&source=View&PageIndex={1}", Request.QueryString["UA01001"], Request.QueryString["PageIndex"]));
        }

        /// <summary>
        /// 列表页面跳转
        /// </summary>
        private string LinkReturn()
        {
            return string.Format(string.Format("UA0101List.aspx?PageIndex={0}", Request.QueryString["PageIndex"]));
        }

        /// <summary>
        /// 页面加载 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            this.IsRose(); //验证权限
            if (!IsPostBack)
            {
                DataBind();
            }
        }

        /// <summary>
        /// 返回跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkReturn());
        }

        /// <summary>
        /// 进入编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkEdit();
        }
    }
}