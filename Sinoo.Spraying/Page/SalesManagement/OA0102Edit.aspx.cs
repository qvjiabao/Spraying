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
    public partial class OA0102Edit : BasePage
    {

        OrderBLL _OrderBLL = new OrderBLL();

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {

            #region 退货订单

            DataTable dt = _OrderBLL.SelectACCode();   //查询应用代码
            if (dt.Rows.Count > 0)
            {
                this.ddlOA01025.DataSource = dt;
                this.ddlOA01025.DataTextField = "OB02002";
                this.ddlOA01025.DataValueField = "OB02001";
                this.ddlOA01025.DataBind();
            }
            this.ddlOA01025.Items.Insert(0, new ListItem("请选择", ""));


            if (string.IsNullOrEmpty(Request.QueryString["OA01001"]))   //当前退货单数据
            {
                return;
            }
            DataTable dt2 = _OrderBLL.SelectOrderBaseById(Request.QueryString["OA01001"]);
            this.txtOA01002.Text = dt2.Rows[0]["OA01002"].ToString();
            this.txtOA01008.Text = dt2.Rows[0]["OA01008"].ToString();
            this.txtOA01009.Text = Convert.ToDateTime(dt2.Rows[0]["OA01009"]).ToString("yyyy-MM-dd");
            this.txtOA01015.Text = dt2.Rows[0]["OA01015"].ToString();
            this.txtOA01017.Text = dt2.Rows[0]["OA01017"].ToString();
            if (dt2.Rows[0]["OA01020"] is DBNull)
            {
                this.txtOA01020.Value = "";
            }
            else
            {
                string str = dt2.Rows[0]["OA01020"].ToString();
                str = str.Substring(1);
                this.txtOA01020.Value = Math.Round(Convert.ToDecimal(str), 2).ToString();
            }

            //this.txtOA01020.Value = dt2.Rows[0]["OA01020"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dt2.Rows[0]["OA01020"]), 2).ToString();
            this.txtOA01021.Value = dt2.Rows[0]["OA01021"].ToString();
            if (dt2.Rows[0]["OA01022"] is DBNull)
            {
                this.txtOA01022.Value = "";
            }
            else
            {
                string str = dt2.Rows[0]["OA01022"].ToString();
                str = str.Substring(1);
                this.txtOA01022.Value = Math.Round(Convert.ToDecimal(str), 2).ToString();
            }
            //this.txtOA01022.Value = dt2.Rows[0]["OA01022"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dt2.Rows[0]["OA01022"]), 2).ToString();
            this.txtOA01027.Text = dt2.Rows[0]["OA01027"].ToString();
            this.txtOA01039.Text = dt2.Rows[0]["OA01039"].ToString();
            this.txtOA01049.Text = dt2.Rows[0]["OA01049"].ToString();
            this.txtOA01050.Text = dt2.Rows[0]["OA01050"].ToString();
            this.labOA01013.Text = dt2.Rows[0]["UA01004"].ToString();
            this.txtOA01013.Value = dt2.Rows[0]["OA01013"].ToString();
            ddlOA01016.SelectedValue = dt2.Rows[0]["OA01016"].ToString();
            ddlOA01018.SelectedValue = dt2.Rows[0]["OA01018"].ToString();
            ddlOA01025.SelectedValue = dt2.Rows[0]["OA01025"].ToString();
            this.ID.Value = dt2.Rows[0]["OA01001"].ToString();
            if (dt2.Rows[0]["OA01005"].ToString() == "0")
                rbtnOA01005No.Checked = true;
            if (dt2.Rows[0]["OA01005"].ToString() == "1")
                rbtnOA01005Yew.Checked = true;

            ViewState["OA01999"] = dt2.Rows[0]["OA01999"].ToString();
            #endregion

            #region 退单商品明细

            DataTable dt3 = _OrderBLL.SelectOrderProductByProductNo(dt2.Rows[0]["OA01999"].ToString(), 2);
            if (dt3.Rows.Count <= 0)
            {
                return;
            }
            this.rptmingxi.DataSource = dt3;   //绑定明细
            this.rptmingxi.DataBind();
            #endregion

            #region 退单发票
            string strguid = string.Empty;  //记录商品明细
            foreach (DataRow item in dt3.Rows)
            {
                strguid += "'" + item["OB01999"].ToString() + "',";
            }
            strguid = strguid.Substring(0, strguid.Length - 1);
            DataTable dt4 = _OrderBLL.SelectOrderInvoiceByGUID(strguid, 2);
            if (dt4.Rows.Count <= 0)
            {
                return;
            }
            this.rptfapiao.DataSource = dt4;   //绑定发票信息
            this.rptfapiao.DataBind();

            #endregion

        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void DataSave()
        {
            if (string.IsNullOrEmpty(Request.QueryString["OA01001"]))
            {
                return;
            }
            #region 退货订单

            DataTable dt = _OrderBLL.SelectOrderBaseByCustomerId(this.txtOA01039.Text.Trim()); //根据订单查询客户编码
            OrderBase _OrderBase = new OrderBase();
            _OrderBase.OA01001 = Convert.ToInt32(Request.QueryString["OA01001"]);
            _OrderBase.OA01002 = this.txtOA01002.Text.Trim();
            _OrderBase.OA01003 = 2;  //退货单
            _OrderBase.OA01004 = 1;  //下单
            _OrderBase.OA01008 = this.txtOA01008.Text.Trim();
            _OrderBase.OA01009 = Convert.ToDateTime(this.txtOA01009.Text.Trim());
            _OrderBase.OA01015 = Request.Form["txtOA01015"];
            _OrderBase.OA01017 = Request.Form["txtOA01017"];
            _OrderBase.OA01020 = string.IsNullOrEmpty(this.txtOA01020.Value.Trim()) || this.txtOA01020.Value.Trim() == ""
                ? 0
                : Convert.ToDecimal("-" + this.txtOA01020.Value.Trim());
            _OrderBase.OA01021 = string.IsNullOrEmpty(this.txtOA01021.Value.Trim()) || this.txtOA01020.Value.Trim() == ""
                ? 0
                : Convert.ToDecimal(this.txtOA01021.Value.Trim());
            _OrderBase.OA01022 = string.IsNullOrEmpty(this.txtOA01020.Value.Trim()) || this.txtOA01020.Value.Trim() == ""
                ? 0
                : Convert.ToDecimal("-" + this.txtOA01022.Value.Trim());
            _OrderBase.OA01027 = this.txtOA01027.Text.Trim();
            _OrderBase.OA01036 = dt.Rows[0]["OA01036"].ToString();  //客户编码
            _OrderBase.OA01039 = this.txtOA01039.Text.Trim();
            _OrderBase.OA01049 = this.txtOA01049.Text.Trim();
            _OrderBase.OA01050 = this.txtOA01050.Text.Trim();
            _OrderBase.OA01013 = this.txtOA01013.Value;
            if (ddlOA01016.SelectedValue != "")
                _OrderBase.OA01016 = Convert.ToDecimal(ddlOA01016.SelectedValue);
            if (ddlOA01018.SelectedValue != "")
                _OrderBase.OA01018 = Convert.ToDecimal(ddlOA01018.SelectedValue);
            _OrderBase.OA01025 = Convert.ToInt32(ddlOA01025.SelectedValue);
            _OrderBase.OA01999 = ViewState["OA01999"].ToString();
            if (rbtnOA01005No.Checked)
                _OrderBase.OA01005 = 0;
            if (rbtnOA01005Yew.Checked)
                _OrderBase.OA01005 = 1;

            #endregion

            #region 退货订单明细

            List<OrderProduct> listOrderProduct = new List<OrderProduct>();
            if (!string.IsNullOrEmpty(this.txtmingxi.Value)) //判断是否有订单明细数据
            {
                string strmingxi = this.txtmingxi.Value; //获取明细数据
                string[] strrow = strmingxi.Split(new[] { "_&_" }, StringSplitOptions.None);
                if (strrow.Length > 0)
                {
                    foreach (string itemrow in strrow)
                    {
                        string[] strcolumns = itemrow.Split(new[] { "_^_" }, StringSplitOptions.None);
                        if (strcolumns.Length > 0)
                        {
                            if (strcolumns[0].Trim() != "")
                            {
                                OrderProduct _OrderProduct = new OrderProduct();
                                _OrderProduct.OB01002 = ViewState["OA01999"].ToString(); //订单编号
                                _OrderProduct.OB01003 = 2;
                                _OrderProduct.OB01004 = Convert.ToInt32(strcolumns[10].Trim()); //商品Id
                                _OrderProduct.OB01005 = strcolumns[0].ToString().Trim();  //商品型号
                                _OrderProduct.OB01006 = strcolumns[1].ToString().Trim();  //商品描述
                                if (strcolumns[2].Trim() != "")
                                    _OrderProduct.OB01007 = Convert.ToInt32(strcolumns[2].Trim());  //商品数量
                                if (strcolumns[3].Trim() != "")
                                    _OrderProduct.OB01008 = Convert.ToDecimal(strcolumns[3].Trim());  //含税单价（含税价格）
                                if (strcolumns[4].Trim() != "")
                                    _OrderProduct.OB01009 = Convert.ToDecimal(strcolumns[4].Trim());  //含税总价（含税总计）
                                if (strcolumns[5].Trim() != "")
                                    _OrderProduct.OB01010 = Convert.ToDecimal(strcolumns[5].Trim());  //不含税单价（不含税价格
                                if (strcolumns[6].Trim() != "")
                                    _OrderProduct.OB01011 = Convert.ToDecimal(strcolumns[6].Trim());  //不含税总价（不含税总计）
                                if (strcolumns[7].Trim() != "")
                                    _OrderProduct.OB01012 = Convert.ToDecimal(strcolumns[7].Trim());  //税额
                                if (strcolumns[8].Trim() != "")
                                    _OrderProduct.OB01013 = Convert.ToDecimal(strcolumns[8].Trim());  //单位成本
                                if (strcolumns[9].Trim() != "")
                                    _OrderProduct.OB01014 = Convert.ToDecimal(strcolumns[9].Trim());  //合计成本
                                _OrderProduct.OB01999 = Guid.NewGuid().ToString();   //GUID
                                listOrderProduct.Add(_OrderProduct);
                            }
                        }
                    }
                }
            }

            #endregion

            #region 退货发票
            List<OrderInvoice> listOrderInvoice = new List<OrderInvoice>();
            if (!string.IsNullOrEmpty(this.txtfapiao.Value)) //判断是否有发票数据
            {

                string strfapiao = this.txtfapiao.Value; //获取发票数据
                string[] strrow1 = strfapiao.Split('&');
                if (strrow1.Length > 0)
                {
                    int i = 0;
                    foreach (string itemrow in strrow1)
                    {
                        string[] strcolumns = itemrow.Split('^');
                        if (strcolumns.Length > 0)
                        {
                            OrderInvoice _OrderInvoice = new OrderInvoice();

                            _OrderInvoice.OC01002 = Convert.ToInt32(strcolumns[0].Trim());  //序号
                            _OrderInvoice.OC01003 = listOrderProduct[i].OB01999.ToString();//商品GUID;
                            _OrderInvoice.OC01004 = 2; //退货单
                            if (strcolumns[1].Trim() != "")
                                _OrderInvoice.OC01005 = strcolumns[1].ToString().Trim();  //型号
                            if (strcolumns[2].Trim() != "")
                                _OrderInvoice.OC01006 = Convert.ToInt32(strcolumns[2].Trim());  //数量
                            if (strcolumns[3] == "N")
                                strcolumns[3] = "0";
                            else
                                strcolumns[3] = "1";
                            _OrderInvoice.OC01007 = Convert.ToInt32(strcolumns[3].Trim());  //是否收货
                            if (strcolumns[4].Trim() != "")
                                _OrderInvoice.OC01009 = Convert.ToDateTime(strcolumns[4].Trim()); //收获时间
                            if (strcolumns[5] == "N")
                                strcolumns[5] = "0";
                            else
                                strcolumns[5] = "1";
                            _OrderInvoice.OC01014 = Convert.ToInt32(strcolumns[5].Trim());  //是否开发票
                            if (strcolumns[6].Trim() != "")
                                _OrderInvoice.OC01016 = Convert.ToDateTime(strcolumns[6].Trim()); //退发票时间
                            listOrderInvoice.Add(_OrderInvoice);
                            i++;
                        }
                    }
                }
            }

            #endregion

            int num = Math.Abs(_OrderBLL.ModOrderBase(_OrderBase, listOrderProduct, listOrderInvoice));
            new Sinoo.Common.MessageShow().UpdateMessage(this
                   , num
                   , string.Format("location.href='{0}'", LinkReturn()));
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

                url = string.Format("OA0102List.aspx?PageIndex={0}", ViewState["PageIndex"]);
            }
            else
            {
                url = string.Format("OA0102View.aspx?PageIndex={0}&OA01001={1}"
                    , ViewState["PageIndex"], Request.QueryString["OA01001"]);
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
            this.DataSave();
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