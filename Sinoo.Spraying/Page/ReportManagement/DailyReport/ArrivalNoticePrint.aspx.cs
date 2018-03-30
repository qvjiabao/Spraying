using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Sinoo.BLL;
using Sinoo.Model;

namespace Sinoo.Spraying.Page.ReportManagement.DailyReport
{
    public partial class ArrivalNoticePrint : System.Web.UI.Page
    {
        ReportBLL _ReportBLL = new ReportBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["PageIndex"] = string.IsNullOrEmpty(Request.QueryString["PageIndex"])
                    ? "1"
                    : Request.QueryString["PageIndex"];

                string OrderId = Request.QueryString["OA01002"]; //客户ID
                //订单号查询商品 发票信息
                DataTable dt1 = _ReportBLL.SelectProductInvoice(OrderId);
                if (dt1.Rows.Count > 0)
                {
                    this.Repeater1.DataSource = dt1;
                    this.Repeater1.DataBind();
                }


                //订单号查询付款客户信息
                DataTable dt2 = _ReportBLL.SelectCustomerPayment(OrderId);
                if (dt2.Rows.Count > 0)
                {
                    this.labCA01003.Text = dt2.Rows[0]["CA01003"].ToString();
                    this.labCA01009.Text = dt2.Rows[0]["CA01009"].ToString();
                    this.labCA01011.Text = dt2.Rows[0]["CA01011"].ToString();
                    this.labOP01005.Text = Math.Round(Convert.ToDecimal(dt2.Rows[0]["OP01005"]), 2).ToString();
                    this.labDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }

                UserBase _UserBase = Session["USER_SESSION"] as UserBase;
                switch (_UserBase.UA01013)
                {
                    case "北京":
                        this.labTel.Text = "010-68562800";
                        break;
                    case "天津":
                        this.labTel.Text = "022-27126918";
                        break;
                    case "西安":
                        this.labTel.Text = "029-88360145";
                        break;
                    case "沈阳":
                        this.labTel.Text = "024-23286590";
                        break;
                    default:
                        this.labTel.Text = "010-68562800";
                        break;
                }
            }
        }
    }
}