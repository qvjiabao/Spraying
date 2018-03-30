using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Sinoo.Common;
using Sinoo.BLL;
using Sinoo.Model;

namespace Sinoo.Spraying.Page.SalesManagement
{
    public partial class OA0101List : System.Web.UI.Page
    {
        //实例化逻辑层
        OrderBLL _OrderBLL = new OrderBLL();

        //实例化省市逻辑层
        AreaBLL _AreaBLL = new AreaBLL();

        ExcelBLL _ExcelBLL = new ExcelBLL();

        UserBLL _UserBLL = new UserBLL();

        public string DataCount = string.Empty;

        /// <summary>
        /// 获取页面条件Where语句
        /// </summary>
        /// <returns></returns>
        public string GetSqlWhere()
        {
            string strWhere = string.Empty;

            UserBase _UserBase = Session["USER_SESSION"] as UserBase;
            if (_UserBase.UA01013 != "全区域")
            {
                strWhere += string.Format(" AND UA01013 = '{0}' ", _UserBase.UA01013);
            }
            if (_UserBase.UA01009 == 1)  //登录人为销售人员只显示自己的销售订单
            {
                strWhere += string.Format(" AND OA01013 =  '{0}'", _UserBase.UA01001);
            }
            if (_UserBase.UA01001 == 44)  //陈广琴：订单、退单权限只能看到autojet订单
            {
                strWhere += string.Format(" AND OA01002 LIKE 'A%'");
            }

            //Customer Code客户编号
            strWhere += (this.txtCA01002.Text.Trim() == "" ? "" : string.Format(" AND CA01002 LIKE '%{0}%'", this.txtCA01002.Text.Trim()));
            //Customer Name客户名称
            strWhere += (this.txtCA01003.Text.Trim() == "" ? "" : string.Format(" AND CA01003 LIKE '%{0}%'", this.txtCA01003.Text.Trim()));
            //Order NO订单号
            strWhere += (this.txtOA01002.Text.Trim() == "" ? "" : string.Format(" AND OA01002 LIKE '%{0}%'", this.txtOA01002.Text.Trim()));
            if (this.txtPA01003.Text.Trim() != "")
            {
                DataTable dtOB01 = _OrderBLL.SelectOrderGUIDByPartNo(this.txtPA01003.Text.Trim());
                var strOA01GUIDWhere = string.Empty;
                if (dtOB01.Rows.Count > 0)
                {
                    strOA01GUIDWhere += " AND OA01999 IN ( ";
                    foreach (DataRow item in dtOB01.Rows)
                    {
                        strOA01GUIDWhere += string.Format("'{0}',", item["OB01002"]);
                    }
                    strOA01GUIDWhere = strOA01GUIDWhere.Substring(0, strOA01GUIDWhere.Length - 1);
                    strOA01GUIDWhere += " ) ";
                }
                else
                {
                    strOA01GUIDWhere = " AND 1 = 0 ";
                }
                strWhere += strOA01GUIDWhere;
            }

            //Province所属省份
            strWhere += (this.ddlGA03Province.SelectedValue == "" ? "" : string.Format(" AND ProvinceId = '{0}'", this.ddlGA03Province.SelectedValue));
            //City所属城市
            strWhere += (string.IsNullOrEmpty(Request.Form["ddlGA03City"]) ? "" : string.Format(" AND CityId = '{0}'", Request.Form["ddlGA03City"]));
            //Sales Name销售员
            strWhere += (string.IsNullOrEmpty(this.ddlUA01.SelectedValue) ? "" : string.Format(" AND UA01001 = '{0}'", this.ddlUA01.SelectedValue));
            //AC
            strWhere += (this.ddlOB02.SelectedValue == "" ? "" : string.Format(" AND OA01025 = '{0}'", this.ddlOB02.SelectedValue));
            //Date Start订单日期区间
            strWhere += (this.txtOA01009Start.Text.Trim() == "" ? "" : string.Format(" AND OA01009 >= '{0}'", this.txtOA01009Start.Text.Trim()));
            //Date End订单日期区间
            strWhere += (this.txtOA01009End.Text.Trim() == "" ? "" : string.Format(" AND OA01009 <= '{0}'", this.txtOA01009End.Text.Trim()));
            //Customer Request Delivery Date客户要求交货日期
            strWhere += (this.txtOA01010.Text.Trim() == "" ? "" : string.Format(" AND OA01010 = '{0}'", this.txtOA01010.Text.Trim()));
            //Minifogger
            strWhere += (this.rbtnOA01046Yes.Checked ? string.Format(" AND OA01045 = 1 ") : "");
            strWhere += (this.rbtnOA01046No.Checked ? string.Format(" AND OA01045 = 0 ") : "");
            //End User最终用户
            strWhere += (this.txtOA01046.Text.Trim() == "" ? "" : string.Format(" AND OA01046 LIKE '%{0}%'", this.txtOA01046.Text.Trim()));
            //Application Description应用描述
            strWhere += (this.txtOA01047.Text.Trim() == "" ? "" : string.Format(" AND OA01047 LIKE '%{0}%'", this.txtOA01047.Text.Trim()));
            //New Application新应用
            strWhere += (this.txtOA01024.Text.Trim() == "" ? "" : string.Format(" AND OA01024 LIKE '%{0}%'", this.txtOA01024.Text.Trim()));
            //Application Share是否为应用分享
            strWhere += (this.txtOA01048.Text.Trim() == "" ? "" : string.Format(" AND OA01048 LIKE '%{0}%'", this.txtOA01048.Text.Trim()));
            //Agreement No：
            strWhere += (this.txtOA01053.Text.Trim() == "" ? "" : string.Format(" AND OA01053 LIKE '%{0}%'", this.txtOA01053.Text.Trim()));

            //发票时间
            if (!string.IsNullOrEmpty(this.txtOC01015Start.Text.Trim()))
            {
                strWhere += string.Format(" AND OC01015 >= '{0}'", txtOC01015Start.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOC01015End.Text.Trim()))
            {
                strWhere += string.Format(" AND OC01015 <= '{0}'", txtOC01015End.Text.Trim());
            }

            return strWhere;
        }

        /// <summary>
        /// 根据查询条件绑定数据
        /// </summary>
        private void InitData(int Index, bool bl)
        {
            string strWhere = GetSqlWhere();

            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;


            ViewState["PageIndex"] = Index;

            object RowsCount = null;
            DataTable dt = _OrderBLL.SelectOrderBaseForList(AspNetPager1.PageSize, Index, strWhere, ref RowsCount);
            if (dt.Rows.Count > 0)
            {
                DataCount = string.Empty;
                dt.Columns.Add(new DataColumn("color")); //提醒颜色
                int uid = Convert.ToInt32((Session["USER_SESSION"] as UserBase).UA01001);  //登录人id
                DataTable dtRed = _OrderBLL.SelectMessageArrival(uid);  //提示红色	到货提醒
                foreach (DataRow item in dt.Rows)
                {
                    DataRow[] dr = dtRed.Select(string.Format("OA01002 = '{0}'", item["OA01002"]));
                    if (dr.Length > 0)
                    {
                        item["color"] = dr[0]["COLOR"].ToString();
                    }
                }

                DataTable dtBlue = _OrderBLL.SelectMessageDelivered(uid);  //提示蓝色 发货提醒
                foreach (DataRow item in dt.Rows)
                {
                    DataRow[] dr = dtBlue.Select(string.Format("OA01002 = '{0}'", item["OA01002"]));
                    if (dr.Length > 0)
                    {
                        item["color"] = dr[0]["COLOR"].ToString();
                    }
                }

                DataTable dtPurple = _OrderBLL.SelectMessageNoPayment(uid);  //提示紫色 	未付款提醒
                foreach (DataRow item in dt.Rows)
                {
                    DataRow[] dr = dtPurple.Select(string.Format("OA01002 = '{0}'", item["OA01002"]));
                    if (dr.Length > 0)
                    {
                        item["color"] = dr[0]["COLOR"].ToString();
                    }
                }
            }
            else
            {
                if (bl)
                {
                    DataCount = "<tr><td colspan=\"8\">系统暂无数据显示</td></tr>";
                }
                else
                {
                    DataCount = "<tr><td colspan=\"8\">没有符合查询条件的数据</td></tr>";
                }
            }

            this.repOA01.DataSource = dt;
            //设置翻页控件的总行数
            AspNetPager1.RecordCount = Convert.ToInt32(RowsCount);
            AspNetPager1.CurrentPageIndex = Index;
            this.repOA01.DataBind();
        }

        /// <summary>
        /// 新增页面
        /// </summary>
        private void LinkNew()
        {
            Response.Redirect(string.Format("OA0101New.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }

        /// <summary>
        /// 绑定页面查询条件Dll
        /// </summary>
        private void BindDll()
        {
            //获取省
            DataTable dtGA03 = _AreaBLL.SelectSystemAreaForProvince();

            this.ddlGA03Province.DataSource = dtGA03;
            this.ddlGA03Province.DataTextField = "GA03002";
            this.ddlGA03Province.DataValueField = "GA03001";
            this.ddlGA03Province.DataBind();
            this.ddlGA03Province.Items.Insert(0, new ListItem("请选择", ""));

            //为新增弹层的省市下拉绑定
            this.ddlGA03ProvinceDialog.DataSource = dtGA03;
            this.ddlGA03ProvinceDialog.DataTextField = "GA03002";
            this.ddlGA03ProvinceDialog.DataValueField = "GA03001";
            this.ddlGA03ProvinceDialog.DataBind();
            this.ddlGA03ProvinceDialog.Items.Insert(0, new ListItem("请选择", ""));

            this.ddlGA03CityDialog.Items.Insert(0, new ListItem("请选择", ""));

            ////为市添加请选择选项
            //if (!string.IsNullOrEmpty(Request.QueryString["ProvinceId"]))
            //{
            //    //获取省
            //    DataTable dtGA03City = _AreaBLL.SelectSystemAreaForCity(Request.QueryString["ProvinceId"]);

            //    this.ddlGA03City.DataSource = dtGA03City;
            //    this.ddlGA03City.DataTextField = "GA03002";
            //    this.ddlGA03City.DataValueField = "GA03001";
            //    this.ddlGA03City.DataBind();

            //    this.ddlGA03Province.SelectedValue = Request.QueryString["ProvinceId"];
            //    this.ddlGA03City.SelectedValue = Request.QueryString["cityId"];
            //}
            //this.ddlGA03City.Items.Insert(0, new ListItem("请选择", ""));

            //实例化行业代码逻辑层
            UserBLL _UserBLL = new UserBLL();

            //获取销售员
            UserBase _UserBase = new UserBase();
            _UserBase = Session["USER_SESSION"] as UserBase;
            DataTable dtUA01 = _UserBLL.SelectUserBaseByArea(_UserBase.UA01013);
            this.ddlUA01.DataSource = dtUA01;
            this.ddlUA01.DataTextField = "UA01004";
            this.ddlUA01.DataValueField = "UA01001";
            this.ddlUA01.DataBind();
            this.ddlUA01.Items.Insert(0, new ListItem("请选择", ""));


            //获取AC选择
            DataTable dtOB02 = _OrderBLL.SelectACCode();
            this.ddlOB02.DataSource = dtOB02;
            this.ddlOB02.DataTextField = "OB02002";
            this.ddlOB02.DataValueField = "OB02001";
            this.ddlOB02.DataBind();
            this.ddlOB02.Items.Insert(0, new ListItem("请选择", ""));
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_SESSION"] != null)
            {
                Model.UserBase _UserBase = Session["USER_SESSION"] as Model.UserBase;

                DataTable dt = _UserBLL.SelectSystemRole(Convert.ToInt32(_UserBase.UA01024));
                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("GA01003 = 'OA0101btnNew'"); //新增按钮
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript1", "<script>RemoveNewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'OA0101btnExport'");  //导出
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript2", "<script>RemoveExportCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'OA0101btnImport'");  //导入
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript3", "<script>RemoveImportCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'OA010btnView'");  //浏览
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript4", "<script>RemoveViewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'OA0101btnEdit'");  //编辑
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript5", "<script>RemoveEditCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'OA0101Print'");  //打印订单
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript6", "<script>RemovePrintCss();</script>");
                    }
                }
            }
            if (!IsPostBack)
            {
                BindDll();
                if (string.IsNullOrEmpty(Request["PageIndex"]))  //判断当前页
                {
                    this.InitData(1, true);
                }
                else
                {
                    this.InitData(Convert.ToInt32(Request["PageIndex"]), true);
                }
            }
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            this.InitData(1, false);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveData(object sender, CommandEventArgs e)
        {
            //if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            //{
            //    int result = _CustomerBLL.RemoveCustomerBaseById(e.CommandArgument.ToString());

            //    //结果提示
            //    new Sinoo.Common.MessageShow().RemoveMessage(this.Page, result, "");

            //    if (result < 1)
            //    {
            //        return;
            //    }

            //    int CurrentPageIndex = this.AspNetPager1.CurrentPageIndex;//当前页
            //    int PageCount = this.AspNetPager1.PageCount;//页总数
            //    int RecordCount = this.AspNetPager1.RecordCount;//记录数
            //    int PageSize = this.AspNetPager1.PageSize;//第页条数

            //    int PageIndex = CurrentPageIndex == PageCount ? (PageCount * PageSize - RecordCount == (PageSize - 1) ? CurrentPageIndex - 1 : CurrentPageIndex) : CurrentPageIndex;

            //    InitData(PageIndex);
            //}
        }

        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            this.InitData(AspNetPager1.CurrentPageIndex, false);
        }

        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            List<string> _list = new List<string>();
            //string[] strXmlName = { "OA01.xml", "OB01.xml", "OC01.xml", "OP01.xml" };
            //string[] strXmlName = { };
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable dtOA01 = _OrderBLL.ExportSelectOrderBase(GetSqlWhere()).Tables[0];  //订单
            dtOA01.TableName = "OA01";

            string OA01Guid = "'AA',";  //订单GUID
            if (dtOA01.Rows.Count > 0)
            {
                ds.Tables.Add(dtOA01.Copy());//添加订单数据
                _list.Add("OA01.xml");
                foreach (DataRow item in dtOA01.Rows)
                {
                    OA01Guid += "'" + item["OA01999"].ToString() + "',";
                }
            }

            OA01Guid = OA01Guid.Substring(0, OA01Guid.Length - 1);
            DataTable dtOB01 = _OrderBLL.ExportSelectProduct(OA01Guid).Tables[0];
            dtOB01.TableName = "OB01";
            string OB01Guid = "'AA',";  //商品GUID
            if (dtOB01.Rows.Count > 0)
            {
                ds.Tables.Add(dtOB01.Copy()); //添加商品数据
                _list.Add("OB01.xml");
                foreach (DataRow item in dtOB01.Rows)
                {
                    OB01Guid += "'" + item["OB01999"].ToString() + "',";
                }

            }
            OB01Guid = OB01Guid.Substring(0, OB01Guid.Length - 1);
            DataTable dtOC01 = _OrderBLL.ExportSelectInvoice(OB01Guid).Tables[0]; //发票
            dtOC01.TableName = "OC01";
            if (dtOC01.Rows.Count > 0)
            {
                ds.Tables.Add(dtOC01.Copy());
                _list.Add("OC01.xml");
            }


            DataTable dtOP01 = _OrderBLL.ExportSelectPayment(OA01Guid).Tables[0]; //付款
            dtOP01.TableName = "OP01";
            if (dtOP01.Rows.Count > 0)
            {
                ds.Tables.Add(dtOP01.Copy());
                _list.Add("OP01.xml");
            }

            string[] strXmlName = new string[_list.Count];
            int i = 0;
            foreach (string item in _list)
            {
                if (i < _list.Count)
                {
                    strXmlName[i] = item;
                    i++;
                }
            }


            if (ds.Tables.Count > 0)
            {
                this.DownloadFile(ds, "订单资料.xls", strXmlName);
            }
            else
            {
                new Sinoo.Common.MessageShow().ExportErrorMessage(this);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="strExcelName">下载的Excel文件名</param>
        /// <param name="strXmlName">xml文件名</param>
        private void DownloadFile(DataSet ds, string strExcelName, string[] strXmlName)
        {

            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(strExcelName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(new ExcelHandler().ExportExcel(ds, strXmlName));
            Response.Flush();
            Response.End();
        }

    }
}