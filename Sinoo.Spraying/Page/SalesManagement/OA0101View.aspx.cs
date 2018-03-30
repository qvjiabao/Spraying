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
    public partial class OA0101View : IsRole
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

            #region 加载 系统/喷嘴-最后一张订单号

            DataTable dtAutojet = _OrderBLL.SelectOrderBaseByAutojet((Session["USER_SESSION"] as UserBase).UA01013);
            this.labOA01006.Text = dtAutojet.Rows.Count > 0 ? dtAutojet.Rows[0]["OA01002"].ToString() : "无";

            DataTable dtNozzle = _OrderBLL.SelectOrderBaseByNozzle((Session["USER_SESSION"] as UserBase).UA01013);
            this.labOA01007.Text = dtNozzle.Rows.Count > 0 ? dtNozzle.Rows[0]["OA01002"].ToString() : "无";

            #endregion

            if (string.IsNullOrEmpty(Request.QueryString["OA01001"]))   //当前退货单数据
            {
                return;
            }
            #region  销售单

            DataTable dt = _OrderBLL.SelectOrderBaseForListById(Request.QueryString["OA01001"]);
            if (dt.Rows.Count <= 0)
            {
                return;
            }
            ViewState["CA01001"] = dt.Rows[0]["OA01038"].ToString();
            this.labOA01002.Text = dt.Rows[0]["OA01002"].ToString();
            this.labOA01003.Text = dt.Rows[0]["OA01003"].ToString() == "3" ? "是" : "否";
            this.labOA01005.Text = dt.Rows[0]["OA01005"].ToString() == "1" ? "是" : "否";
            this.labOA01008.Text = dt.Rows[0]["OA01008"].ToString();
            this.labOA01009.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01009"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["OA01009"]).ToString("yyyy-MM-dd");
            this.labOA01010.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01010"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["OA01010"]).ToString("yyyy-MM-dd");
            this.labOA01011.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01011"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["OA01011"]).ToString("yyyy-MM-dd");
            this.labOA01012.Text = dt.Rows[0]["OA01012"].ToString();
            this.labOA01013.Text = dt.Rows[0]["UA01004"].ToString();
            this.labOA01014.Text = dt.Rows[0]["OA01014"].ToString();
            this.labOA01015.Text = dt.Rows[0]["OA01015"].ToString();
            this.labOA01016.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01016"].ToString()) ? "" : dt.Rows[0]["OA01016"].ToString() == "0" ? "" : Convert.ToDecimal(dt.Rows[0]["OA01016"]) * 100 + "%";
            this.labOA01017.Text = dt.Rows[0]["OA01017"].ToString();
            this.labOA01018.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01018"].ToString()) ? "" : dt.Rows[0]["OA01018"].ToString() == "0" ? "" : Convert.ToDecimal(dt.Rows[0]["OA01018"]) * 100 + "%";
            //为true 显示成本
            if (total) this.labOA01019.Text = dt.Rows[0]["OA01019"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dt.Rows[0]["OA01019"]), 2).ToString();
            else this.labOA01019.Text = "0";
            this.labOA01020.Text = dt.Rows[0]["OA01020"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dt.Rows[0]["OA01020"]), 2).ToString();
            this.labOA01021.Text = dt.Rows[0]["OA01021"].ToString();
            this.labOA01022.Text = dt.Rows[0]["OA01022"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dt.Rows[0]["OA01022"]), 2).ToString();
            //为true 显示成本
            if (total)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["OA01023"].ToString()))
                    this.labOA01023.Text = Math.Round(Convert.ToDecimal(dt.Rows[0]["OA01023"].ToString()) * 100, 2) + "%";
            }
            else this.labOA01023.Text = "0";

            this.labOA01024.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01024"].ToString()) ? "否" : "是";
            this.labOA0102401.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01024"].ToString()) ? "" : "内容：" + dt.Rows[0]["OA01024"].ToString();
            this.labOA01025.Text = dt.Rows[0]["OB02002"].ToString();
            this.labOA01026.Text = dt.Rows[0]["OA01026"].ToString();
            this.labOA01027.Text = dt.Rows[0]["OA01027"].ToString();
            this.labOA01028.Text = dt.Rows[0]["OA01028"].ToString();
            this.labOA01029.Text = dt.Rows[0]["OA01029"].ToString();
            this.labOA01030.Text = dt.Rows[0]["OA01030"].ToString();
            this.labOA01031.Text = dt.Rows[0]["OA01031"].ToString();
            this.labOA01032.Text = dt.Rows[0]["OA01032"].ToString();
            this.labOA01033.Text = dt.Rows[0]["OA01033"].ToString();
            this.labOA01034.Text = dt.Rows[0]["OA01034"].ToString();
            this.labOA01035.Text = dt.Rows[0]["OA01035"].ToString();
            this.labOA01044.Text = dt.Rows[0]["OA01044"].ToString() == "0" ? "否" : "是";
            this.labOA01045.Text = dt.Rows[0]["OA01045"].ToString() == "0" ? "否" : "是";
            this.labOA01046.Text = dt.Rows[0]["OA01046"].ToString();
            this.labOA01047.Text = dt.Rows[0]["OD01003"].ToString();
            this.labOA01048.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01048"].ToString()) ? "否" : "是";
            this.labOA0104801.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01048"].ToString()) ? "" : "内容：" + dt.Rows[0]["OA01048"].ToString();
            this.labOA01049.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01049"].ToString()) ? "" : "其它人：" + dt.Rows[0]["OA01049"].ToString();
            this.labOA01050.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01050"].ToString()) ? "" : "其它人：" + dt.Rows[0]["OA01050"].ToString();
            this.labOA01051.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01051"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["OA01051"]).ToString("yyyy-MM-dd");
            this.labOA01053.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01053"].ToString()) ? "" : dt.Rows[0]["OA01053"].ToString();
            this.labOA01054.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01054"].ToString()) || dt.Rows[0]["OA01054"].ToString() == "0" ? "否" : "是";
            #endregion

            #region 付款信息

            DataTable dtOP01 = _OrderBLL.SelectOrderPayment(string.Format(" AND OP01003 = '{0}'", dt.Rows[0]["OA01999"].ToString()));

            if (dtOP01.Rows.Count > 0)
            {
                this.labOP01002.Text = dtOP01.Rows[0]["OP01002"].ToString();
                this.labOP01003.Text = this.labOA01002.Text;
                this.labOP01004.Text = dtOP01.Rows[0]["OP01004"] is DBNull ? "" : Convert.ToDateTime(dtOP01.Rows[0]["OP01004"]).ToString("yyyy-MM-dd");
                this.labOP01005.Text = dtOP01.Rows[0]["OP01005"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01005"]), 2).ToString();
                this.labOP01007.Text = dtOP01.Rows[0]["OP01007"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01007"]), 2).ToString();
                this.labOP01008.Text = dtOP01.Rows[0]["OP01008"].ToString();
                this.labOP01009.Text = dtOP01.Rows[0]["OP01009"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01009"]), 2).ToString();
                this.labOP01010.Text = dtOP01.Rows[0]["OP01010"].ToString();
                this.labOP01011.Text = dtOP01.Rows[0]["OP01011"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01011"]), 2).ToString();
                this.labOP01012.Text = dtOP01.Rows[0]["OP01012"].ToString();
                this.labOP01013.Text = dtOP01.Rows[0]["OP01013"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01013"]), 2).ToString();
                this.labOP01014.Text = dtOP01.Rows[0]["OP01014"].ToString();
                this.labOP01015.Text = dtOP01.Rows[0]["OP01015"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01015"]), 2).ToString();
                this.labOP01016.Text = dtOP01.Rows[0]["OP01016"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01016"]), 2).ToString();
                this.labOP01017.Text = dtOP01.Rows[0]["OP01017"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01017"]), 2).ToString();
                this.labOP01018.Text = dtOP01.Rows[0]["OP01018"].ToString();
                this.labOP01019.Text = dtOP01.Rows[0]["OP01019"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01019"]), 2).ToString();
                this.labOP01020.Text = dtOP01.Rows[0]["OP01020"].ToString();
                this.labOP01021.Text = dtOP01.Rows[0]["OP01021"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01021"]), 2).ToString();
                this.labOP01022.Text = dtOP01.Rows[0]["OP01022"].ToString();
                //this.labOP01006.Text = dtOP01.Rows[0]["OP01006"].ToString();
            }

            #endregion

            #region 销售单商品明细

            DataTable dt1 = _OrderBLL.SelectOrderProductByProductNo(dt.Rows[0]["OA01999"].ToString(), 1);
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

            #region 销售单发票

            string strguid = string.Empty;  //记录商品明细
            foreach (DataRow item in dt1.Rows)
            {
                strguid += "'" + item["OB01999"].ToString() + "',";
            }
            strguid = strguid.Substring(0, strguid.Length - 1);
            DataTable dt2 = _OrderBLL.SelectOrderInvoiceByGUID(strguid, 1);
            if (dt2.Rows.Count <= 0)
            {
                return;
            }
            this.rptfapiao.DataSource = dt2;   //绑定发票信息
            this.rptfapiao.DataBind();

            #endregion


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
                ViewState["PageIndex"] = string.IsNullOrEmpty(Request["PageIndex"]) ? "1" : Request["PageIndex"];

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
            Response.Redirect(string.Format("OA0101Edit.aspx?OA01001={0}&PageIndex={1}&CA01001={2}&source=View"
                , ViewState["OA01001"].ToString(), ViewState["PageIndex"].ToString(), ViewState["CA01001"].ToString()));
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("OA0101List.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }
    }
}