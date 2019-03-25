using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Sinoo.Model;
using Sinoo.BLL;

namespace Sinoo.Spraying.Page.SalesManagement
{
    public partial class OA0101Edit : IsRole
    {
        #region 实例化逻辑层

        CustomerBLL _CustomerBLL = new CustomerBLL();

        OrderBLL _OrderBLL = new OrderBLL();
        //实例化省市逻辑层
        AreaBLL _AreaBLL = new AreaBLL();

        #endregion

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            #region 加载用户名称

            if (string.IsNullOrEmpty(Request.QueryString["CA01001"]))   //当前退货单数据
            {
                return;
            }
            DataTable dtCA01 = _CustomerBLL.SelectCustomerBaseByID(Convert.ToInt32(Request.QueryString["CA01001"]));
            if (dtCA01.Rows.Count < 1)   //当前退货单数据
            {
                return;
            }
            this.labCA01003.Text = dtCA01.Rows[0]["CA01003"].ToString();

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
            ViewState["OA01001"] = dt.Rows[0]["OA01001"].ToString();
            this.ID.Value = dt.Rows[0]["OA01001"].ToString();
            this.txtOA01002.Text = dt.Rows[0]["OA01002"].ToString();
            if (dt.Rows[0]["OA01005"].ToString() == "0")
            {
                this.rbtnOA01005No.Checked = true;
            }
            else
            {
                this.rbtnOA01005Yes.Checked = true;
            }
            if (dt.Rows[0]["OA01003"].ToString() == "3")
            {
                this.rbtnOA01003Yes.Checked = true;
            }
            else if (dt.Rows[0]["OA01003"].ToString() == "1")
            {
                this.rbtnOA01003No.Checked = true;
            }
            this.txtOA01008.Text = dt.Rows[0]["OA01008"].ToString();
            this.txtOA01009.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01009"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["OA01009"]).ToString("yyyy-MM-dd");
            this.txtOA01010.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01010"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["OA01010"]).ToString("yyyy-MM-dd");
            this.txtOA01011.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01011"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["OA01011"]).ToString("yyyy-MM-dd");
            this.txtOA01012.Text = dt.Rows[0]["OA01012"].ToString();
            this.ddlUA01.SelectedValue = dt.Rows[0]["UA01001"].ToString();
            this.txtOA01014.Text = dt.Rows[0]["OA01014"].ToString();
            this.txtOA01015.Text = dt.Rows[0]["OA01015"].ToString();
            this.ddlOA01016.SelectedValue = dt.Rows[0]["OA01016"].ToString() == "0" ? "" : dt.Rows[0]["OA01016"].ToString();
            this.txtOA01017.Text = dt.Rows[0]["OA01017"].ToString();
            this.ddlOA01018.SelectedValue = dt.Rows[0]["OA01018"].ToString() == "0" ? "" : dt.Rows[0]["OA01018"].ToString();
            this.txtOA01019.Text = dt.Rows[0]["OA01019"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dt.Rows[0]["OA01019"]), 2).ToString();
            this.txtOA01020.Text = dt.Rows[0]["OA01020"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dt.Rows[0]["OA01020"]), 2).ToString();
            this.txtOA01021.Text = dt.Rows[0]["OA01021"].ToString();
            this.txtOA01022.Text = dt.Rows[0]["OA01022"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dt.Rows[0]["OA01022"]), 2).ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["OA01023"].ToString()))
                this.txtOA01023.Text = Math.Round(Convert.ToDecimal(dt.Rows[0]["OA01023"].ToString()) * 100, 2) + "%";
            if (string.IsNullOrEmpty(dt.Rows[0]["OA01024"].ToString()))
            {
                this.rbtnOA01024No.Checked = true;
            }
            else
            {
                this.rbtnOA01024Yes.Checked = true;
            }
            this.txtOA01024.Text = dt.Rows[0]["OA01024"].ToString();
            this.ddlOA01025.SelectedValue = dt.Rows[0]["OB02001"].ToString();
            this.txtOA01026.Text = dt.Rows[0]["OA01026"].ToString();
            this.txtOA01027.Text = dt.Rows[0]["OA01027"].ToString();
            this.txtOA01028.Text = dt.Rows[0]["OA01028"].ToString();
            this.txtOA01029.Text = dt.Rows[0]["OA01029"].ToString();
            this.txtOA01030.Text = dt.Rows[0]["OA01030"].ToString();
            this.txtOA01031.Text = dt.Rows[0]["OA01031"].ToString();
            this.txtOA01032.Text = dt.Rows[0]["OA01032"].ToString();
            this.txtOA01033.Text = dt.Rows[0]["OA01033"].ToString();
            this.txtOA01034.Text = dt.Rows[0]["OA01034"].ToString();
            this.txtOA01035.Text = dt.Rows[0]["OA01035"].ToString();
            if (string.IsNullOrEmpty(dt.Rows[0]["OA01044"].ToString()) || dt.Rows[0]["OA01044"].ToString() == "0")
            {
                this.rbtnOA01044No.Checked = true;
            }
            else
            {
                this.rbtnOA01044Yes.Checked = true;
            }
            if (dt.Rows[0]["OA01045"].ToString() == "0")
            {
                this.rbtnOA01045No.Checked = true;
            }
            else
            {
                this.rbtnOA01045Yes.Checked = true;
            }
            this.txtOA01046.Text = dt.Rows[0]["OA01046"].ToString();
            this.ddlOA01047.SelectedValue = dt.Rows[0]["OA01047"].ToString();
            if (string.IsNullOrEmpty(dt.Rows[0]["OA01048"].ToString()))
            {
                this.rbtnOA01048No.Checked = true;
            }
            else
            {
                this.rbtnOA01048Yes.Checked = true;
            }
            this.txtOA01048.Text = dt.Rows[0]["OA01048"].ToString();

            this.txtOA01049.Text = dt.Rows[0]["OA01049"].ToString();
            this.txtOA01050.Text = dt.Rows[0]["OA01050"].ToString();
            this.txtOA01051.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01051"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["OA01051"]).ToString("yyyy-MM-dd");
            this.txtOA01053.Text = string.IsNullOrEmpty(dt.Rows[0]["OA01053"].ToString()) ? "" : dt.Rows[0]["OA01053"].ToString();

            this.ddlOA01054.SelectedValue = dt.Rows[0]["OA01054"].ToString();
            ViewState["OA01999"] = dt.Rows[0]["OA01999"].ToString();
            this.ddlOA01055.SelectedValue = dt.Rows[0]["OA01055"].ToString();
            this.ddlOA01056.SelectedValue = dt.Rows[0]["OA01056"].ToString();

            #endregion

            #region 付款信息

            DataTable dtOP01 = _OrderBLL.SelectOrderPayment(string.Format(" AND OP01003 = '{0}'", dt.Rows[0]["OA01999"].ToString()));

            if (dtOP01.Rows.Count > 0)
            {
                this.labOP01002.InnerText = dtOP01.Rows[0]["OP01002"].ToString();
                this.labOP01003.InnerText = this.txtOA01002.Text;
                this.labOP01004.InnerText = dtOP01.Rows[0]["OP01004"] is DBNull ? "" : Convert.ToDateTime(dtOP01.Rows[0]["OP01004"]).ToString("yyyy-MM-dd");
                this.labOP01005.InnerText = dtOP01.Rows[0]["OP01005"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01005"]), 2).ToString();
                this.txtOP01007.Text = dtOP01.Rows[0]["OP01007"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01007"]), 2).ToString();
                this.txtOP01008.Text = dtOP01.Rows[0]["OP01008"].ToString();
                this.txtOP01009.Text = dtOP01.Rows[0]["OP01009"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01009"]), 2).ToString();
                this.txtOP01010.Text = dtOP01.Rows[0]["OP01010"].ToString();
                this.txtOP01011.Text = dtOP01.Rows[0]["OP01011"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01011"]), 2).ToString();
                this.txtOP01012.Text = dtOP01.Rows[0]["OP01012"].ToString();
                this.txtOP01013.Text = dtOP01.Rows[0]["OP01013"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01013"]), 2).ToString();
                this.txtOP01014.Text = dtOP01.Rows[0]["OP01014"].ToString();
                #region 添加 5 6 7 付款

                this.txtOP01017.Text = dtOP01.Rows[0]["OP01017"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01017"]), 2).ToString();
                this.txtOP01018.Text = dtOP01.Rows[0]["OP01018"].ToString();
                this.txtOP01019.Text = dtOP01.Rows[0]["OP01019"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01019"]), 2).ToString();
                this.txtOP01020.Text = dtOP01.Rows[0]["OP01020"].ToString();
                this.txtOP01021.Text = dtOP01.Rows[0]["OP01021"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01021"]), 2).ToString();
                this.txtOP01022.Text = dtOP01.Rows[0]["OP01022"].ToString();

                #endregion

                this.labOP01015.InnerText = dtOP01.Rows[0]["OP01015"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01015"]), 2).ToString();
                this.labOP01016.InnerText = dtOP01.Rows[0]["OP01016"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01016"]), 2).ToString();
                this.txtOP01015.Value = dtOP01.Rows[0]["OP01015"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01015"]), 2).ToString();
                this.txtOP01016.Value = dtOP01.Rows[0]["OP01016"] is DBNull ? "" : Math.Round(Convert.ToDecimal(dtOP01.Rows[0]["OP01016"]), 2).ToString();
                //this.ddlOP01006.SelectedValue = dtOP01.Rows[0]["OP01006"].ToString();
            }

            #endregion

            #region 销售单商品明细

            DataTable dt1 = _OrderBLL.SelectOrderProductByProductNo(dt.Rows[0]["OA01999"].ToString(), 1);
            if (dt1.Rows.Count <= 0)
            {
                return;
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
        /// 保存数据方法
        /// </summary>
        private void DataSave()
        {
            OrderBase _OrderBase = new OrderBase();

            #region 销售单实体赋值
            _OrderBase.OA01001 = Convert.ToInt32(ViewState["OA01001"]);
            _OrderBase.OA01002 = this.txtOA01002.Text.Trim();
            _OrderBase.OA01003 = this.rbtnOA01003Yes.Checked == true ? 3 : 1;
            _OrderBase.OA01004 = 1;                     //下单
            _OrderBase.OA01005 = this.rbtnOA01005Yes.Checked ? 1 : 0;
            _OrderBase.OA01006 = "";                    //系统订单号
            _OrderBase.OA01007 = "";                    //喷嘴订单号
            _OrderBase.OA01008 = this.txtOA01008.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtOA01009.Text.Trim()))
            {
                _OrderBase.OA01009 = Convert.ToDateTime(this.txtOA01009.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOA01010.Text.Trim()))
            {
                _OrderBase.OA01010 = Convert.ToDateTime(this.txtOA01010.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOA01011.Text.Trim()))
            {
                _OrderBase.OA01011 = Convert.ToDateTime(this.txtOA01011.Text.Trim());
            }
            _OrderBase.OA01012 = this.txtOA01012.Text.Trim();
            _OrderBase.OA01013 = this.ddlUA01.SelectedValue;
            _OrderBase.OA01014 = this.txtOA01014.Text.Trim();
            _OrderBase.OA01015 = Request.Form["txtOA01015"].Trim();
            if (!string.IsNullOrEmpty(this.ddlOA01016.SelectedValue))
            {
                _OrderBase.OA01016 = Convert.ToDecimal(this.ddlOA01016.SelectedValue);
            }
            else
            {
                _OrderBase.OA01016 = 0;
            }
            _OrderBase.OA01017 = Request.Form["txtOA01017"].Trim();
            if (!string.IsNullOrEmpty(this.ddlOA01018.SelectedValue))
            {
                _OrderBase.OA01018 = Convert.ToDecimal(this.ddlOA01018.SelectedValue);
            }
            else
            {
                _OrderBase.OA01018 = 0;
            }
            if (!string.IsNullOrEmpty(Request.Form["txtOA01019"].Trim()))
            {
                _OrderBase.OA01019 = Convert.ToDecimal(Request.Form["txtOA01019"].Trim());
            }
            if (!string.IsNullOrEmpty(Request.Form["txtOA01020"].Trim()))
            {
                _OrderBase.OA01020 = Convert.ToDecimal(Request.Form["txtOA01020"].Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOA01021.Text.Trim()))
            {
                _OrderBase.OA01021 = Convert.ToDecimal(this.txtOA01021.Text.Trim());
            }
            if (!string.IsNullOrEmpty(Request.Form["txtOA01022"].Trim()))
            {
                _OrderBase.OA01022 = Convert.ToDecimal(Request.Form["txtOA01022"].Trim());
            }
            if (!string.IsNullOrEmpty(Request.Form["txtOA01023"].Trim()))
            {
                _OrderBase.OA01023 = Convert.ToDecimal(Request.Form["txtOA01023"].Trim().Replace('%', '0')) / 100;
            }
            _OrderBase.OA01024 = this.rbtnOA01024Yes.Checked ? this.txtOA01024.Text.Trim() : string.Empty;
            _OrderBase.OA01025 = Convert.ToInt32(this.ddlOA01025.SelectedValue);
            _OrderBase.OA01026 = this.txtOA01026.Text.Trim();
            _OrderBase.OA01027 = this.txtOA01027.Text.Trim();
            _OrderBase.OA01028 = this.txtOA01028.Text.Trim();
            _OrderBase.OA01029 = this.txtOA01029.Text.Trim();
            _OrderBase.OA01030 = this.txtOA01030.Text.Trim();
            _OrderBase.OA01031 = this.txtOA01031.Text.Trim();
            _OrderBase.OA01032 = this.txtOA01032.Text.Trim();
            _OrderBase.OA01033 = this.txtOA01033.Text.Trim();
            _OrderBase.OA01034 = this.txtOA01034.Text.Trim();
            _OrderBase.OA01035 = this.txtOA01035.Text.Trim();

            #region 获取客户的信息
            DataTable dt = new CustomerBLL().SelectCustomerBase(string.Format(" AND CA01001 = '{0}'", Request.QueryString["CA01001"]));
            if (dt.Rows.Count < 1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('数据异常！');window.location.href='/Index.aspx';</script>");
                return;
            }
            #endregion

            _OrderBase.OA01036 = dt.Rows[0]["CA01002"].ToString();            //客户编码
            _OrderBase.OA01037 = (Session["USER_SESSION"] as UserBase).UA01001;
            _OrderBase.OA01038 = Convert.ToInt32(dt.Rows[0]["CA01001"]);
            _OrderBase.OA01039 = "";                                         //主订单编号
            if (!string.IsNullOrEmpty(dt.Rows[0]["CA01019"].ToString()))
            {
                _OrderBase.OA01040 = Convert.ToInt32(dt.Rows[0]["CA01019"]);
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["CA01020"].ToString()))
            {
                _OrderBase.OA01041 = Convert.ToInt32(dt.Rows[0]["CA01020"]);
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["CA01022"].ToString()))
            {
                _OrderBase.OA01042 = Convert.ToInt32(dt.Rows[0]["CA01022"]);
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["CA01023"].ToString()))
            {
                _OrderBase.OA01043 = Convert.ToInt32(dt.Rows[0]["CA01023"]);
            }
            _OrderBase.OA01044 = this.rbtnOA01044Yes.Checked ? 1 : 0;
            _OrderBase.OA01045 = this.rbtnOA01045Yes.Checked ? 1 : 0;
            _OrderBase.OA01046 = this.txtOA01046.Text.Trim();
            _OrderBase.OA01047 = this.ddlOA01047.SelectedValue;
            _OrderBase.OA01048 = this.txtOA01048.Text.Trim();
            _OrderBase.OA01049 = this.txtOA01049.Text.Trim();
            _OrderBase.OA01050 = this.txtOA01050.Text.Trim();
            _OrderBase.OA01053 = this.txtOA01053.Text.Trim();
            _OrderBase.OA01054 = int.Parse(this.ddlOA01054.SelectedValue);
            if (!string.IsNullOrEmpty(this.txtOA01051.Text.Trim()))
                _OrderBase.OA01051 = Convert.ToDateTime(this.txtOA01051.Text.Trim());
            _OrderBase.OA01999 = ViewState["OA01999"].ToString();
            _OrderBase.OA01055 = this.ddlOA01055.SelectedValue;
            _OrderBase.OA01056 = this.ddlOA01056.SelectedValue;
            _OrderBase.OA01057 = this.ddlOA01055.SelectedItem.Text.Equals("请选择") ? string.Empty : this.ddlOA01055.SelectedItem.Text;
            _OrderBase.OA01058 = this.ddlOA01056.SelectedItem.Text.Equals("请选择") ? string.Empty : this.ddlOA01056.SelectedItem.Text;

            #endregion

            #region 销售单明细
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
                                _OrderProduct.OB01002 = _OrderBase.OA01999; //订单编号
                                _OrderProduct.OB01003 = 1;
                                _OrderProduct.OB01004 = Convert.ToInt32(strcolumns[10].Trim()); //商品Id
                                _OrderProduct.OB01005 = strcolumns[0].Trim();  //商品型号
                                _OrderProduct.OB01006 = strcolumns[1].Trim();  //商品描述
                                if (!string.IsNullOrEmpty(strcolumns[2].Trim()))
                                    _OrderProduct.OB01007 = Convert.ToInt32(strcolumns[2].Trim());  //商品数量
                                if (!string.IsNullOrEmpty(strcolumns[3].Trim()))
                                    _OrderProduct.OB01008 = Convert.ToDecimal(strcolumns[3].Trim());  //含税单价（含税价格）
                                if (!string.IsNullOrEmpty(strcolumns[4].Trim()))
                                    _OrderProduct.OB01009 = Convert.ToDecimal(strcolumns[4].Trim());  //含税总价（含税总计）
                                if (!string.IsNullOrEmpty(strcolumns[5].Trim()))
                                    _OrderProduct.OB01010 = Convert.ToDecimal(strcolumns[5].Trim());  //不含税单价（不含税价格）
                                if (!string.IsNullOrEmpty(strcolumns[6].Trim()))
                                    _OrderProduct.OB01011 = Convert.ToDecimal(strcolumns[6].Trim());  //不含税总价（不含税总计）
                                if (!string.IsNullOrEmpty(strcolumns[7].Trim()))
                                    _OrderProduct.OB01012 = Convert.ToDecimal(strcolumns[7].Trim());  //税额
                                if (!string.IsNullOrEmpty(strcolumns[8].Trim()))
                                    _OrderProduct.OB01013 = Convert.ToDecimal(strcolumns[8].Trim());  //单位成本
                                if (!string.IsNullOrEmpty(strcolumns[9].Trim()))
                                    _OrderProduct.OB01014 = Convert.ToDecimal(strcolumns[9].Trim());  //合计成本
                                _OrderProduct.OB01999 = Guid.NewGuid().ToString();   //GUID
                                listOrderProduct.Add(_OrderProduct);
                            }
                        }
                    }
                }
            }

            #endregion

            #region 销售发票
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
                            if (!string.IsNullOrEmpty(strcolumns[0].Trim()))
                                _OrderInvoice.OC01002 = Convert.ToInt32(strcolumns[0].Trim());  //序号
                            _OrderInvoice.OC01003 = listOrderProduct[i].OB01999.ToString();//商品GUID;
                            _OrderInvoice.OC01004 = 1; //销售单
                            _OrderInvoice.OC01005 = strcolumns[1].Trim();  //型号
                            if (!string.IsNullOrEmpty(strcolumns[2].Trim()))
                                _OrderInvoice.OC01006 = Convert.ToInt32(strcolumns[2].Trim());  //数量
                            if (!string.IsNullOrEmpty(strcolumns[3].Trim()))
                                _OrderInvoice.OC01007 = Convert.ToInt32(strcolumns[3].Trim() == "N" ? "0" : "1");  //是否到货
                            if (!string.IsNullOrEmpty(strcolumns[4].Trim()))
                                _OrderInvoice.OC01008 = strcolumns[4].Trim();  //到货批次
                            if (!string.IsNullOrEmpty(strcolumns[5].Trim()))
                                _OrderInvoice.OC01009 = Convert.ToDateTime(strcolumns[5].Trim()); //到货时间
                            if (!string.IsNullOrEmpty(strcolumns[6].Trim()))
                                _OrderInvoice.OC01010 = Convert.ToInt32(strcolumns[6].Trim() == "N" ? "0" : "1");  //是否到货
                            if (!string.IsNullOrEmpty(strcolumns[7].Trim()))
                                _OrderInvoice.OC01011 = Convert.ToDateTime(strcolumns[7].Trim()); //发货时间
                            _OrderInvoice.OC01012 = strcolumns[8].Trim();
                            _OrderInvoice.OC01013 = strcolumns[9].Trim();

                            if (!string.IsNullOrEmpty(strcolumns[11].Trim()))
                                _OrderInvoice.OC01014 = Convert.ToInt32(strcolumns[11].Trim() == "N" ? "0" : "1");  //是否开发票
                            if (!string.IsNullOrEmpty(strcolumns[12].Trim()))
                                _OrderInvoice.OC01015 = Convert.ToDateTime(strcolumns[12].Trim());//开发票时间
                            _OrderInvoice.OC01017 = strcolumns[14].Trim();
                            _OrderInvoice.OC01018 = strcolumns[13].Trim();

                            _OrderInvoice.OC01019 = strcolumns[10].Trim();
                            _OrderInvoice.OC01020 = strcolumns[15].Trim();

                            listOrderInvoice.Add(_OrderInvoice);
                            i++;
                        }
                    }
                }
            }

            #endregion

            #region 付款信息

            OrderPayment _OrderPayment = new OrderPayment();
            _OrderPayment.OP01002 = this.labOP01002.InnerText.Trim();
            _OrderPayment.OP01003 = _OrderBase.OA01999; //订单GUID
            if (!string.IsNullOrEmpty(this.txtOA01009.Text.Trim())) _OrderPayment.OP01004 = Convert.ToDateTime(this.txtOA01009.Text.Trim());
            if (!string.IsNullOrEmpty(Request.Form["txtOA01020"])) _OrderPayment.OP01005 = Convert.ToDecimal(Request.Form["txtOA01020"]);
            //if (!string.IsNullOrEmpty(this.ddlOP01006.SelectedValue)) _OrderPayment.OP01006 = Convert.ToInt32(this.ddlOP01006.SelectedValue);
            if (!string.IsNullOrEmpty(this.txtOP01007.Text.Trim()))
            {
                _OrderPayment.OP01007 = Convert.ToDecimal(this.txtOP01007.Text.Trim());
            }

            _OrderPayment.OP01008 = this.txtOP01008.Text.Trim();

            if (!string.IsNullOrEmpty(this.txtOP01009.Text.Trim()))
            {
                _OrderPayment.OP01009 = Convert.ToDecimal(this.txtOP01009.Text.Trim());
            }
            _OrderPayment.OP01010 = this.txtOP01010.Text.Trim();

            if (!string.IsNullOrEmpty(this.txtOP01011.Text.Trim()))
            {
                _OrderPayment.OP01011 = Convert.ToDecimal(this.txtOP01011.Text.Trim());
            }

            _OrderPayment.OP01012 = this.txtOP01012.Text.Trim();


            if (!string.IsNullOrEmpty(this.txtOP01013.Text.Trim()))
            {
                _OrderPayment.OP01013 = Convert.ToDecimal(this.txtOP01013.Text.Trim());
            }

            _OrderPayment.OP01014 = this.txtOP01014.Text.Trim();

            if (!string.IsNullOrEmpty(this.txtOP01015.Value.Trim())) _OrderPayment.OP01015 = Convert.ToDecimal(this.txtOP01015.Value.Trim());

            if (!string.IsNullOrEmpty(this.txtOP01016.Value.Trim())) _OrderPayment.OP01016 = Convert.ToDecimal(this.txtOP01016.Value.Trim());


            #region 添加5 6 7付款

            if (!string.IsNullOrEmpty(this.txtOP01017.Text.Trim()))
            {
                _OrderPayment.OP01017 = Convert.ToDecimal(this.txtOP01017.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOP01018.Text.Trim()))
            {
                _OrderPayment.OP01018 = this.txtOP01018.Text.Trim();
            }
            if (!string.IsNullOrEmpty(this.txtOP01019.Text.Trim()))
            {
                _OrderPayment.OP01019 = Convert.ToDecimal(this.txtOP01019.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOP01020.Text.Trim()))
            {
                _OrderPayment.OP01020 = this.txtOP01020.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.txtOP01021.Text.Trim()))
            {
                _OrderPayment.OP01021 = Convert.ToDecimal(this.txtOP01021.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOP01022.Text.Trim()))
            {
                _OrderPayment.OP01022 = this.txtOP01022.Text.Trim();
            }

            #endregion

            #endregion

            int result = Math.Abs(_OrderBLL.ModOrderBase(_OrderBase, listOrderProduct, listOrderInvoice, _OrderPayment));
            new Sinoo.Common.MessageShow().InsertMessage(this, result, string.Format("window.location.href='{0}'", LinkReturn()));

        }

        /// <summary>
        /// 返回跳转
        /// </summary>
        private string LinkReturn()
        {
            string url = string.Empty;
            if (Request.QueryString["source"] == "View")
            {
                url = string.Format("OA0101View.aspx?PageIndex={0}&OA01001={1}", Request.QueryString["PageIndex"], Request.QueryString["OA01001"]);
            }
            else
            {
                url = string.Format("OA0101List.aspx?PageIndex={0}", Request.QueryString["PageIndex"]);
            }
            return url;
        }

        /// <summary>
        /// 绑定页面的DropDownList
        /// </summary>
        private void BindPage()
        {

            #region 分享省份

            DataTable dtGA03 = _AreaBLL.SelectSystemAreaForProvince();
            this.ddlOA01055.DataSource = dtGA03;
            this.ddlOA01055.DataTextField = "GA03002";
            this.ddlOA01055.DataValueField = "GA03001";
            this.ddlOA01055.DataBind();
            this.ddlOA01055.Items.Insert(0, new ListItem("请选择", ""));

            this.ddlOA01056.DataSource = dtGA03;
            this.ddlOA01056.DataTextField = "GA03002";
            this.ddlOA01056.DataValueField = "GA03001";
            this.ddlOA01056.DataBind();
            this.ddlOA01056.Items.Insert(0, new ListItem("请选择", ""));

            #endregion

            #region 销售员

            UserBase _UserBase = Session["USER_SESSION"] as UserBase;
            DataTable dt1;
            if (_UserBase.UA01013 != "全区域")
            {
                dt1 = _OrderBLL.SelectUserBaseByEara(_UserBase.UA01013); //所属区域
            }
            else
            {
                dt1 = _OrderBLL.SelectUserBaseByEara();
            }

            if (dt1.Rows.Count > 0)
            {
                this.ddlUA01.DataSource = dt1;
                this.ddlUA01.DataTextField = "UA01004";
                this.ddlUA01.DataValueField = "UA01001";
                this.ddlUA01.DataBind();
                this.ddlUA01.Items.Insert(0, new ListItem("请选择", ""));
            }

            #endregion

            #region 应用代码

            DataTable dt = _OrderBLL.SelectACCode();   //查询应用代码
            if (dt.Rows.Count > 0)
            {
                this.ddlOA01025.DataSource = dt;
                this.ddlOA01025.DataTextField = "OB02002";
                this.ddlOA01025.DataValueField = "OB02001";
                this.ddlOA01025.DataBind();
                this.ddlOA01025.Items.Insert(0, new ListItem("请选择", ""));
            }

            #endregion

            #region 应用描述 、客户姓名

            DataTable dtCA01 = _CustomerBLL.SelectCustomerBase(string.Format(" AND CA01001 = '{0}'", Request.QueryString["CA01001"]));
            if (dtCA01.Rows.Count < 1) { throw new Exception(); }

            this.labOP01002.InnerText = dtCA01.Rows[0]["CA01003"].ToString();

            DataTable dtOD01 = _OrderBLL.SelectApplicationDescription(string.Format(" AND OD01002 = '{0}'", dtCA01.Rows[0]["CA01020"]));
            this.ddlOA01047.DataSource = dtOD01;
            this.ddlOA01047.DataTextField = "OD01003";
            this.ddlOA01047.DataValueField = "OD01001";
            this.ddlOA01047.DataBind();
            this.ddlOA01047.Items.Insert(0, new ListItem("请选择", ""));

            #endregion

            #region 加载 系统/喷嘴-最后一张订单号

            DataTable dtAutojet = _OrderBLL.SelectOrderBaseByAutojet((Session["USER_SESSION"] as UserBase).UA01013);
            this.labOA01006.Text = dtAutojet.Rows.Count > 0 ? dtAutojet.Rows[0]["OA01002"].ToString() : "无";

            DataTable dtNozzle = _OrderBLL.SelectOrderBaseByNozzle((Session["USER_SESSION"] as UserBase).UA01013);
            this.labOA01007.Text = dtNozzle.Rows.Count > 0 ? dtNozzle.Rows[0]["OA01002"].ToString() : "无";

            #endregion


        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            this.IsRose(); //验证权限
            if (!IsPostBack)
            {

                BindPage();     //绑定页面下拉框
                InitData();
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataSave();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkReturn());
        }
    }
}