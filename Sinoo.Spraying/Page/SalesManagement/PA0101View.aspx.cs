using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;

namespace Sinoo.Spraying.Page.SalesManagement
{
    public partial class PA0101View : BasePage
    {

        ProductBLL _ProductBLL = new ProductBLL();

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            DataTable dt = _ProductBLL.SelectProductBaseById(ViewState["PA01001"].ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                this.labPA01003.Text = dt.Rows[0]["PA01003"].ToString();
                this.labPA01005.Text = dt.Rows[0]["PA01005"].ToString();
                this.labNetPrice.Text = dt.Rows[0]["Netprice"].ToString();
                this.labPriceOne.Text = dt.Rows[0]["One"].ToString();
                this.labPriceTwo.Text = dt.Rows[0]["Two"].ToString();
                this.labPriceThree.Text = dt.Rows[0]["Three"].ToString();
                this.labPriceFour.Text = dt.Rows[0]["Four"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            this.IsRose(); //验证权限
            if (!IsPostBack)
            {
                ViewState["PageIndex"] = Request["PageIndex"];

                if (!string.IsNullOrEmpty(Request.QueryString["PA01001"]))
                {
                    ViewState["PA01001"] = Request.QueryString["PA01001"];
                    this.InitData();
                }

            }
        }

        /// <summary>
        /// 跳转编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("PA0101Edit.aspx?PA01001={0}&PageIndex={1}"
                , ViewState["PA01001"].ToString(), ViewState["PageIndex"].ToString()));
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("PA0101List.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }
    }
}