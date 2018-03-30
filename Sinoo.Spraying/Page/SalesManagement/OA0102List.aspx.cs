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
    public partial class OA0102List : System.Web.UI.Page
    {

        OrderBLL _OrderBLL = new OrderBLL();
        UserBLL _UserBLL = new UserBLL();
        AreaBLL _AreaBLL = new AreaBLL();

        public string DataCount = string.Empty;

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="Index"></param>
        private void InitData()
        {

            //获取省
            DataTable dtGA03 = _AreaBLL.SelectSystemAreaForProvince();
            this.ddlProvince.DataSource = dtGA03;
            this.ddlProvince.DataTextField = "GA03002";
            this.ddlProvince.DataValueField = "GA03001";
            this.ddlProvince.DataBind();
            this.ddlProvince.Items.Insert(0, new ListItem("请选择", ""));

            //获取销售员
            UserBase _UserBase = new UserBase();
            _UserBase = Session["USER_SESSION"] as UserBase;
            DataTable dtUA01 = _UserBLL.SelectUserBaseByArea(_UserBase.UA01013);
            this.ddltSalesName.DataSource = dtUA01;
            this.ddltSalesName.DataTextField = "UA01004";
            this.ddltSalesName.DataValueField = "UA01001";
            this.ddltSalesName.DataBind();
            this.ddltSalesName.Items.Insert(0, new ListItem("请选择", ""));
        }

        /// <summary>
        /// 查询绑定数据
        /// </summary>
        /// <param name="Index">当前页</param>
        private void SelectDate(int Index)
        {
            AspNetPager1.AlwaysShow = false;
            AspNetPager1.PageSize = 15;

            string strWhereAdd = string.Empty;

            UserBase _UserBase = Session["USER_SESSION"] as UserBase;
            if (_UserBase.UA01013 != "全区域")
            {
                strWhereAdd += string.Format(" AND UA01013 = '{0}' ", _UserBase.UA01013);
            }
            if (_UserBase.UA01001 == 44)  //陈广琴：订单、退单权限只能看到autojet订单
            {
                strWhereAdd += string.Format(" AND OA01002 LIKE 'A%'");
            }
            //else
            //{
            //    strWhereAdd += string.Format(" AND OA01002 NOT LIKE 'A%'");  //Order中默认为nozzle类型
            //}


            if (!string.IsNullOrEmpty(this.txtCustomerCode.Text.Trim()))   //客户编码
            {
                strWhereAdd += string.Format(" AND OA01036 LIKE '%{0}%' ", this.txtCustomerCode.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtCustomerName.Text.Trim()))   //客户名称
            {
                strWhereAdd += string.Format(" AND CA01003 LIKE '%{0}%' ", this.txtCustomerName.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOrderNO.Text.Trim()))  //订单编号
            {
                strWhereAdd += string.Format(" AND OA01002 LIKE '%{0}%' ", this.txtOrderNO.Text.Trim());
            }
            if (_UserBase.UA01024 == 43)  //登录人是销售员只能看到自己的订单  --按销售员查询不好使
            {
                strWhereAdd += string.Format(" AND OA01013 = '{0}' ", _UserBase.UA01001);
            }
            else
            {
                if (this.ddltSalesName.SelectedValue != "")  //销售人
                {
                    strWhereAdd += string.Format(" AND OA01013 = '{0}' ", ddltSalesName.SelectedValue);
                }
            }
            if (this.ddlProvince.SelectedValue != "")   //客户所属省
            {
                strWhereAdd += string.Format(" AND dbo.FX_GetProvinceIdByCityId(CA01013) = '{0}' ", this.ddlProvince.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.txtBeginTime.Text.Trim())) //退单时间起始
            {
                strWhereAdd += string.Format(" AND OA01009 >= '{0}' ", this.txtBeginTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))  //退单时间截至
            {
                strWhereAdd += string.Format(" AND OA01009 <= '{0}' ", this.txtEndTime.Text.Trim());
            }

            if (this.txtPartNo.Text.Trim() != "")
            {
                DataTable dtOB01 = _OrderBLL.SelectOrderGUIDByPartNo(this.txtPartNo.Text.Trim());
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
                strWhereAdd += strOA01GUIDWhere;
            }
            object RowsCount = null;
            DataTable dt = _OrderBLL.SelectOrderBaseReturnPage(AspNetPager1.PageSize, Index, strWhereAdd, ref RowsCount);
            if (string.IsNullOrEmpty(strWhereAdd))
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"8\">系统暂无数据显示</td></tr>" : DataCount;
            else
                DataCount = dt.Rows.Count == 0 ? "<tr><td colspan=\"8\">没有符合查询条件的数据</td></tr>" : DataCount;

            AspNetPager1.RecordCount = Convert.ToInt32(RowsCount);
            AspNetPager1.CurrentPageIndex = Index;
            rpPA01.DataSource = dt;
            rpPA01.DataBind();
        }

        /// <summary>
        /// 跳转新增页面
        /// </summary>
        private void LinkPA0101New()
        {
            Response.Redirect(string.Format("OA0102New.aspx?PageIndex={0}", ViewState["PageIndex"]));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            LinkPA0101New();
        }

        /// <summary>
        /// 窗体初始化
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
                    DataRow[] dr = dt.Select("GA01003 = 'OA0102btnNew'"); //新增按钮
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript1", "<script>RemoveNewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'OA0102btnView'");  //浏览
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript4", "<script>RemoveViewCss();</script>");
                    }
                    dr = dt.Select("GA01003= 'OA0102btnEdit'");  //编辑
                    if (!(dr.Length > 0))
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript5", "<script>RemoveEditCss();</script>");
                    }
                }
            }
            if (!IsPostBack)
            {
                this.InitData();
                ViewState["PageIndex"] = string.IsNullOrEmpty(Request["PageIndex"])
                    ? 1
                    : Convert.ToInt32(Request["PageIndex"]);

                this.SelectDate(Convert.ToInt32(ViewState["PageIndex"]));
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            this.SelectDate(AspNetPager1.CurrentPageIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            this.SelectDate(Convert.ToInt32(ViewState["PageIndex"]));
        }
    }
}