using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using Sinoo.Model;
using System.Data;

namespace Sinoo.Spraying.Page.SalesManagement
{
    public partial class OA0102New : BasePage
    {
        OrderBLL _OrderBLL = new OrderBLL();

        /// <summary>
        /// 初始化信息
        /// </summary>
        private void DataInit()
        {

            DataTable dt = _OrderBLL.SelectACCode();   //查询应用代码
            if (dt.Rows.Count > 0)
            {
                this.ddlOA01025.DataSource = dt;
                this.ddlOA01025.DataTextField = "OB02002";
                this.ddlOA01025.DataValueField = "OB02001";
                this.ddlOA01025.DataBind();
            }
            this.ddlOA01025.Items.Insert(0, new ListItem("请选择", ""));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void DataSave()
        {

            #region 退货订单

            DataTable dt = _OrderBLL.SelectOrderBaseByCustomerId(this.txtOA01039.Text.Trim()); //根据订单查询客户编码

            OrderBase _OrderBase = new OrderBase();
            _OrderBase.OA01002 = this.txtOA01002.Text.Trim();
            _OrderBase.OA01003 = 2;  //退货单
            _OrderBase.OA01004 = 1;  //下单
            _OrderBase.OA01008 = this.txtOA01008.Text.Trim();
            _OrderBase.OA01009 = Convert.ToDateTime(this.txtOA01009.Text.Trim());
            _OrderBase.OA01015 = Request.Form["txtOA01015"];
            _OrderBase.OA01016 = 0;
            _OrderBase.OA01017 = Request.Form["txtOA01017"];
            _OrderBase.OA01018 = 0;
            _OrderBase.OA01020 = string.IsNullOrEmpty(this.txtOA01020.Value.Trim()) || this.txtOA01020.Value.Trim() == ""
                ? 0
                : Convert.ToDecimal("-" + this.txtOA01020.Value.Trim());
            _OrderBase.OA01021 = string.IsNullOrEmpty(this.txtOA01021.Value.Trim()) || this.txtOA01021.Value.Trim() == ""
                ? 0
                : Convert.ToDecimal(this.txtOA01021.Value.Trim());
            _OrderBase.OA01022 = string.IsNullOrEmpty(this.txtOA01022.Value.Trim()) || this.txtOA01022.Value.Trim() == ""
                ? 0
                : Convert.ToDecimal("-" + this.txtOA01022.Value.Trim());
            _OrderBase.OA01027 = this.txtOA01027.Text.Trim();
            _OrderBase.OA01036 = dt.Rows[0]["OA01036"].ToString();  //客户编码
            _OrderBase.OA01038 = Convert.ToInt32(dt.Rows[0]["OA01038"]);  //客户Id
            _OrderBase.OA01039 = dt.Rows[0]["OA01039"].ToString();       //注订单编号
            if (!(dt.Rows[0]["OA01040"] is DBNull))
                _OrderBase.OA01040 = Convert.ToInt32(dt.Rows[0]["OA01040"]); //行业分类
            if (!(dt.Rows[0]["OA01041"] is DBNull))
                _OrderBase.OA01041 = Convert.ToInt32(dt.Rows[0]["OA01041"]); //行业代码
            if (!(dt.Rows[0]["OA01042"] is DBNull))
                _OrderBase.OA01042 = Convert.ToInt32(dt.Rows[0]["OA01042"]);  //204电子分类
            if (!(dt.Rows[0]["OA01043"] is DBNull))
                _OrderBase.OA01043 = Convert.ToInt32(dt.Rows[0]["OA01043"]);  //220汽车分类
            if (!(dt.Rows[0]["OA01044"] is DBNull))
                _OrderBase.OA01044 = Convert.ToInt32(dt.Rows[0]["OA01044"]);  //是否为新客户
            _OrderBase.OA01039 = this.txtOA01039.Text.Trim();
            _OrderBase.OA01049 = this.txtOA01049.Text.Trim();
            _OrderBase.OA01050 = this.txtOA01050.Text.Trim();
            _OrderBase.OA01013 = this.txtOA01013.Value;
            if (ddlOA01016.SelectedValue != "")
                _OrderBase.OA01016 = Convert.ToDecimal(ddlOA01016.SelectedValue);
            if (ddlOA01018.SelectedValue != "")
                _OrderBase.OA01018 = Convert.ToDecimal(ddlOA01018.SelectedValue);
            _OrderBase.OA01025 = Convert.ToInt32(ddlOA01025.SelectedValue);

            _OrderBase.OA01052 = (Session["USER_SESSION"] as UserBase).UA01001;
            _OrderBase.OA01999 = Guid.NewGuid().ToString();
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
                                _OrderProduct.OB01002 = _OrderBase.OA01999; //订单GUID
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

            int num = Math.Abs(_OrderBLL.AddOrderBase(_OrderBase, listOrderProduct, listOrderInvoice));
            new Sinoo.Common.MessageShow().InsertMessage(this, num, "DataClear()");
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
                this.DataInit();
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
            Response.Redirect(string.Format("OA0102List.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }
    }
}