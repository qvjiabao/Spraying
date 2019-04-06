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
    public partial class OA0102View : BasePage
    {

        OrderBLL _OrderBLL = new OrderBLL();

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            bool total = true;  //是否显示成本
            Model.UserBase _UserBase = Session["USER_SESSION"] as Model.UserBase;
            #region 如果为销售员、主管、系统调试不显示成本
            if (_UserBase.UA01024 == 43 || _UserBase.UA01024 == 46 || _UserBase.UA01024 == 44)
            {
                total = false;
            }
            #endregion

            if (!string.IsNullOrEmpty(Request.QueryString["OA01001"]))   //当前退货单数据
            {
                #region  退货单

                DataTable dt = _OrderBLL.SelectOrderBaseById(Request.QueryString["OA01001"]);
                if (dt.Rows.Count <= 0)
                {
                    return;
                }
                this.labOA01002.Text = dt.Rows[0]["OA01002"].ToString();
                this.labOA01008.Text = dt.Rows[0]["OA01008"].ToString();
                this.labOA01009.Text = Convert.ToDateTime(dt.Rows[0]["OA01009"]).ToString("yyyy-MM-dd");
                this.labOA01015.Text = dt.Rows[0]["OA01015"].ToString();
                this.labOA01017.Text = dt.Rows[0]["OA01017"].ToString();
                if (dt.Rows[0]["OA01020"] is DBNull)
                {
                    this.labOA01020.Text = "";
                }
                else
                {
                    string str=dt.Rows[0]["OA01020"].ToString();
                    str=str.Substring(1);
                    this.labOA01020.Text=Math.Round(Convert.ToDecimal(str), 2).ToString();
                }
                this.labOA01021.Text = dt.Rows[0]["OA01021"].ToString();
                if (dt.Rows[0]["OA01022"] is DBNull)
                {
                    this.labOA01022.Text = "";
                }
                else
                {
                    string str = dt.Rows[0]["OA01022"].ToString();
                    str = str.Substring(1);
                    this.labOA01022.Text = Math.Round(Convert.ToDecimal(str), 2).ToString();
                }
                this.labOA01027.Text = dt.Rows[0]["OA01027"].ToString();
                this.labOA01039.Text = dt.Rows[0]["OA01039"].ToString();
                this.labOA01049.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01049"].ToString()) ? "" : "其它人1：" + dt.Rows[0]["OA01049"].ToString();
                this.labOA01050.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01050"].ToString()) ? "" : "其它人2：" + dt.Rows[0]["OA01050"].ToString();
                this.labOA01013.Text = dt.Rows[0]["UA01004"].ToString();
                if (!(dt.Rows[0]["OA01016"] == "" || dt.Rows[0]["OA01016"] is DBNull))
                    this.labOA01016.Text = Convert.ToInt32((Convert.ToDecimal(dt.Rows[0]["OA01016"]) * 100)).ToString() + "%";
                if (!(dt.Rows[0]["OA01018"] == "" || dt.Rows[0]["OA01018"] is DBNull))
                    this.labOA01018.Text = Convert.ToInt32((Convert.ToDecimal(dt.Rows[0]["OA01018"]) * 100)).ToString() + "%";
                this.labOA01025.Text = dt.Rows[0]["OB02002"].ToString();
                if (dt.Rows[0]["OA01005"].ToString() == "0")
                    this.labOA01005.Text = "否";
                if (dt.Rows[0]["OA01005"].ToString() == "1")
                    this.labOA01005.Text = "是";

                #endregion

                #region 退单商品明细

                DataTable dt1 = _OrderBLL.SelectOrderProductByProductNo(dt.Rows[0]["OA01999"].ToString(), 2);
                if (dt1.Rows.Count <= 0)
                {
                    return;
                }
                //隐藏成本
                if (!total)
                {
                    foreach (DataRow item in dt1.Rows)
                    {
                        item["OB01013"] = "0";
                        item["OB01014"] = "0";
                    }
                }

                this.rptmingxi.DataSource = dt1;   //绑定明细
                this.rptmingxi.DataBind();
                #endregion

                #region 退单发票
                string strguid = string.Empty;  //记录商品明细
                foreach (DataRow item in dt1.Rows)
                {
                    strguid += "'" + item["OB01999"].ToString() + "',";
                }
                strguid = strguid.Substring(0, strguid.Length - 1);
                DataTable dt2 = _OrderBLL.SelectOrderInvoiceByGUID(strguid, 2);
                if (dt2.Rows.Count <= 0)
                {
                    return;
                }
                this.rptfapiao.DataSource = dt2;   //绑定发票信息
                this.rptfapiao.DataBind();

                #endregion
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

                if (!string.IsNullOrEmpty(Request.QueryString["OA01001"]))
                {
                    ViewState["OA01001"] = Request.QueryString["OA01001"];
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
            Response.Redirect(string.Format("OA0102Edit.aspx?OA01001={0}&PageIndex={1}"
                , ViewState["OA01001"].ToString(), ViewState["PageIndex"].ToString()));
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("OA0102List.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }
    }
}