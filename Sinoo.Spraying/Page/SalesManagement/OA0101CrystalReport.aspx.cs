using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Sinoo.BLL;
using System.IO;
using System.Threading;

namespace Sinoo.Spraying.Page.SalesManagement
{
    public partial class OA0101CrystalReport : System.Web.UI.Page
    {
        OrderBLL _OrderBLL = new OrderBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool total = true;  //是否显示成本
                Model.UserBase _UserBase = Session["USER_SESSION"] as Model.UserBase;
                #region 如果为销售员、主管、系统调试不显示成本
                if (_UserBase.UA01024 == 43 || _UserBase.UA01024 == 46 || _UserBase.UA01024 == 44)
                {
                    total = false;
                }
                #endregion


                ViewState["PageIndex"] = string.IsNullOrEmpty(Request.QueryString["PageIndex"])
                    ? "1"
                    : Request.QueryString["PageIndex"];

                string OrderId = Request.QueryString["OA01001"]; //客户ID
                //订单号查询商品 发票信息
                DataTable dt1 = _OrderBLL.SelectOrderAll(OrderId);
                if (dt1.Rows.Count > 0)
                {
                    this.labCA01003.Text = dt1.Rows[0]["CA01003"].ToString();
                    this.labCA01004.Text = dt1.Rows[0]["CA01004"].ToString();
                    this.labCA01005.Text = dt1.Rows[0]["CA01005"].ToString();
                    this.labCA01006.Text = dt1.Rows[0]["CA01006"].ToString();
                    this.labCA01007.Text = dt1.Rows[0]["CA01007"].ToString();
                    this.labCA01008.Text = dt1.Rows[0]["CA01008"].ToString();
                    this.labCA01009.Text = dt1.Rows[0]["CA01009"].ToString();
                    this.labCA01010ANDCA01011.Text = dt1.Rows[0]["CA01010"].ToString() + "  " + dt1.Rows[0]["CA01011"].ToString();
                    this.labCA01014.Text = dt1.Rows[0]["CA01014"].ToString();
                    this.labCA01015.Text = dt1.Rows[0]["CA01015"].ToString();
                    this.labCA01021.Text = dt1.Rows[0]["CA01021"].ToString();
                    this.labCB02002.Text = dt1.Rows[0]["CB02002"].ToString();
                    if (dt1.Rows[0]["CB04002"].ToString() != "")
                        this.labCB04002.Text = dt1.Rows[0]["CB04002"].ToString().Substring(0, 3); ;
                    this.labOA01002.Text = dt1.Rows[0]["OA01002"].ToString();
                    this.labOA01002Two.Text = Convert.ToDateTime(dt1.Rows[0]["OA01009"]).ToString("yyyy-MM-dd");
                    this.labOA01010.Text = dt1.Rows[0]["OA01010"] is DBNull ? "" : Convert.ToDateTime(dt1.Rows[0]["OA01010"]).ToString("yyyy-MM-dd");
                    this.labOA01011.Text = dt1.Rows[0]["OA01011"] is DBNull ? "" : Convert.ToDateTime(dt1.Rows[0]["OA01011"]).ToString("yyyy-MM-dd");
                    this.labOA01029ANDOA01030.Text = dt1.Rows[0]["OA01029"].ToString() + "  " + dt1.Rows[0]["OA01030"].ToString();
                    this.labOA01033ANDOA01034.Text = dt1.Rows[0]["OA01033"].ToString() + "  " + dt1.Rows[0]["OA01034"].ToString();
                    this.labOA01031.Text = dt1.Rows[0]["OA01031"].ToString();
                    this.labOA01035.Text = dt1.Rows[0]["OA01035"].ToString();
                    this.labOA01012.Text = dt1.Rows[0]["OA01012"].ToString();
                    this.labOA01014.Text = dt1.Rows[0]["OA01014"].ToString();
                    this.labOA01020.Text = dt1.Rows[0]["OA01020"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dt1.Rows[0]["OA01020"].ToString()), 2).ToString();
                    //隐藏成本
                    if (total)
                    {
                        if (dt1.Rows[0]["OA01023"].ToString() != "")
                            this.labOA01023.Text = Math.Round(Convert.ToDecimal(dt1.Rows[0]["OA01023"].ToString()) * 100, 2).ToString() + "%";
                    }
                    this.labOA01032.Text = dt1.Rows[0]["OA01032"].ToString();
                    this.labOA01028.Text = dt1.Rows[0]["OA01028"].ToString();
                    this.labUA01004.Text = dt1.Rows[0]["UA01004"].ToString();
                    if (dt1.Rows[0]["OB02002"].ToString() != "")
                        this.labOB02002.Text = dt1.Rows[0]["OB02002"].ToString().Substring(0, 3);

                    this.labOA01008.Text = dt1.Rows[0]["OA01008"].ToString();
                    this.labCA01002.Text = dt1.Rows[0]["CA01002"].ToString();
                    this.labShare.Text = dt1.Rows[0]["OA01015"].ToString() + dt1.Rows[0]["OA01016"].ToString() + " "
                        + dt1.Rows[0]["OA01017"].ToString() + dt1.Rows[0]["OA01018"].ToString();


                    this.labSessionUser.Text = dt1.Rows[0]["TIANZHI"].ToString();
                }


                //订单号查询付款客户信息
                DataTable dt2 = _OrderBLL.SelectOrderProduct(OrderId);
                if (dt2.Rows.Count > 0)
                {
                    //隐藏成本
                    if (!total)
                    {
                        foreach (DataRow item in dt2.Rows)
                        {
                            item["OB01013"] = "0";
                            item["OB01014"] = "0";
                        }
                    }
                    this.Repeater1.DataSource = dt2;
                    this.Repeater1.DataBind();
                }
            }

        }
    }
}