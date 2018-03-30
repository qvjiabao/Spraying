using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;
using Sinoo.Model;

namespace Sinoo.Spraying.Page.SalesManagement
{
    public partial class PA0101Edit : IsRole
    {

        ProductBLL _ProductBLL = new ProductBLL();

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PA01001"]))
            {
                DataTable dt = _ProductBLL.SelectProductBaseById(Request.QueryString["PA01001"]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.txtPA01003.Text = dt.Rows[0]["PA01003"].ToString();
                    this.txtPA01005.Text = dt.Rows[0]["PA01005"].ToString();
                    this.txtNetPrice.Text = dt.Rows[0]["Netprice"].ToString();
                    this.txtPriceOne.Text = dt.Rows[0]["One"].ToString();
                    this.txtPriceTwo.Text = dt.Rows[0]["Two"].ToString();
                    this.txtPriceThree.Text = dt.Rows[0]["Three"].ToString();
                    this.txtPriceFour.Text = dt.Rows[0]["Four"].ToString();
                    this.ID.Value = dt.Rows[0]["PA01001"].ToString();
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void SaveData()
        {
            if (!string.IsNullOrEmpty(this.txtPA01003.Text))
            {

                ProductBase _ProductBase = new ProductBase();
                _ProductBase.PA01001 = Convert.ToInt32(Request.QueryString["PA01001"]);
                _ProductBase.PA01003 = this.txtPA01003.Text.Trim();
                _ProductBase.PA01005 = this.txtPA01005.Text.Trim();
                _ProductBase.Netprice = this.txtNetPrice.Text.Trim();
                _ProductBase.Priceone = this.txtPriceOne.Text.Trim();
                _ProductBase.Pricetwo = this.txtPriceTwo.Text.Trim();
                _ProductBase.Pricethree = this.txtPriceThree.Text.Trim();
                _ProductBase.Pricefour = this.txtPriceFour.Text.Trim();
                int num = Math.Abs(_ProductBLL.UpdateProductBase(_ProductBase));  //执行保存方法
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
            if (!string.IsNullOrEmpty(Request.QueryString["type"])  //判断返回页面
                && Request.QueryString["type"] == "list")
            {

                url = string.Format("PA0101List.aspx?PageIndex={0}", ViewState["PageIndex"]);
            }
            else
            {
                url = string.Format("PA0101View.aspx?PageIndex={0}&PA01001={1}"
                    , ViewState["PageIndex"], Request.QueryString["PA01001"]);
            }
            return url;
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
            Response.Redirect(LinkReturn());
        }
    }
}