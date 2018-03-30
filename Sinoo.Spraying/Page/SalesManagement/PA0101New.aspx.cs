using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using Sinoo.Model;

namespace Sinoo.Spraying.Page.SalesManagement
{
    public partial class PA0101New : IsRole
    {

        ProductBLL _ProductBLL = new ProductBLL();

        /// <summary>
        /// 保存数据
        /// </summary>
        private void DataSave()
        {
            if (!string.IsNullOrEmpty(this.txtPA01003.Text))
            {
                ProductBase _ProductBase = new ProductBase();
                _ProductBase.PA01003 = this.txtPA01003.Text.Trim();
                _ProductBase.PA01005 = this.txtPA01005.Text.Trim();
                _ProductBase.Netprice = this.txtNetPrice.Text.Trim();
                _ProductBase.Priceone = this.txtPriceOne.Text.Trim();
                _ProductBase.Pricetwo = this.txtPriceTwo.Text.Trim();
                _ProductBase.Pricethree = this.txtPriceThree.Text.Trim();
                _ProductBase.Pricefour = this.txtPriceFour.Text.Trim();

                int num = Math.Abs(_ProductBLL.InsertProductBase(_ProductBase));
                new Sinoo.Common.MessageShow().InsertMessage(this, num, "DataClear();");
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
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.DataSave();
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