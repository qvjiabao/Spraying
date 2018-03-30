using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;
using Sinoo.Model;

namespace Sinoo.Spraying.Page.CustomerManagement
{
    public partial class CA0101Edit : IsRole
    {
        #region 实例化逻辑层

        //实例化客户逻辑层
        CustomerBLL _CustomerManagementLogic = new CustomerBLL();

        //实例化区域逻辑层
        AreaBLL _DataManagementLogic = new AreaBLL();

        #endregion

        /// <summary>
        /// 数据初始化绑定
        /// </summary>
        private void DataBind()
        {
            if (string.IsNullOrEmpty(Request.QueryString["CA01001"]))
            {
                Response.Redirect("CA0101List.aspx");
            }
            DataTable dt = _CustomerManagementLogic.SelectCustomerBase(string.Format(" AND CA01001 = '{0}'", Request.QueryString["CA01001"]));
            try
            {
                this.ID.Value = dt.Rows[0]["CA01001"].ToString();

                this.txtCA01002.Text = dt.Rows[0]["CA01002"].ToString();
                this.txtCA01003.Text = dt.Rows[0]["CA01003"].ToString();
                this.txtCA01004.Text = dt.Rows[0]["CA01004"].ToString().Replace("’", "'");
                this.txtCA01005.Text = dt.Rows[0]["CA01005"].ToString();
                this.txtCA01006.Text = dt.Rows[0]["CA01006"].ToString();
                this.txtCA01007.Text = dt.Rows[0]["CA01007"].ToString();
                this.txtCA01008.Text = dt.Rows[0]["CA01008"].ToString();
                this.txtCA01009.Text = dt.Rows[0]["CA01009"].ToString();
                this.txtCA01010.Text = dt.Rows[0]["CA01010"].ToString();
                this.txtCA01011.Text = dt.Rows[0]["CA01011"].ToString();
                this.txtCA01012.Text = dt.Rows[0]["CA01012"].ToString();
                this.txtCA01014.Text = dt.Rows[0]["CA01014"].ToString();
                this.txtCA01015.Text = dt.Rows[0]["CA01015"].ToString();
                this.txtCA01017.Text = dt.Rows[0]["CA01017"].ToString();
                this.txtCA01021.Text = dt.Rows[0]["CA01021"].ToString();
                this.txtCA01026.Text = dt.Rows[0]["CA01026"].ToString();
                this.txtCA01027.Text = dt.Rows[0]["CA01027"].ToString();
                this.txtCA01028.Text = dt.Rows[0]["CA01028"].ToString();
                this.txtCA01029.Text = dt.Rows[0]["CA01029"].ToString();
                this.txtCA01030.Text = dt.Rows[0]["CA01030"].ToString();
                this.txtCA01031.Text = dt.Rows[0]["CA01031"].ToString();
                this.txtCA01032.Text = dt.Rows[0]["CA01032"].ToString();
                this.txtCA01033.Text = dt.Rows[0]["CA01033"].ToString();
                this.txtCA01034.Text = dt.Rows[0]["CA01034"].ToString();
                this.txtCA01035.Text = dt.Rows[0]["CA01035"].ToString();
                this.txtCA01036.Text = dt.Rows[0]["CA01036"].ToString();
                this.txtCA01037.Text = dt.Rows[0]["CA01037"].ToString();
                this.txtCA01038.Text = dt.Rows[0]["CA01038"].ToString();
                this.txtCA01039.Text = dt.Rows[0]["CA01039"].ToString();
                this.txtCA01040.Text = dt.Rows[0]["CA01040"].ToString();
                this.txtCA01041.Text = dt.Rows[0]["CA01041"].ToString();
                this.txtCA01042.Text = dt.Rows[0]["CA01042"].ToString();
                this.txtCA01043.Text = dt.Rows[0]["CA01043"].ToString();
                this.txtCA01044.Text = dt.Rows[0]["CA01044"].ToString();
                this.txtCA01045.Text = dt.Rows[0]["CA01045"].ToString();
                this.txtCA01046.Text = dt.Rows[0]["CA01046"].ToString();
                this.txtCA01048.Text = dt.Rows[0]["CA01048"].ToString();
                this.txtCA01049.Text = dt.Rows[0]["CA01049"].ToString();
                this.txtCA01050.Text = dt.Rows[0]["CA01050"].ToString();
                this.txtCA01051.Text = dt.Rows[0]["CA01051"].ToString();

                //拿出区域上级编码
                if (!string.IsNullOrEmpty(dt.Rows[0]["CA01013"].ToString()))
                {
                    DataTable dtGA03 = _DataManagementLogic.SelectSystemAreaById(dt.Rows[0]["CA01013"].ToString());
                    this.ddlGA03Province.SelectedValue = dtGA03.Rows[0]["GA03003"].ToString();

                    //绑定此省下所有的市（区域）
                    DataTable dtGA0301 = _DataManagementLogic.SelectSystemAreaForCity(dtGA03.Rows[0]["GA03003"].ToString());

                    this.ddlGA03City.DataSource = dtGA0301;
                    this.ddlGA03City.DataTextField = "GA03002";
                    this.ddlGA03City.DataValueField = "GA03001";
                    this.ddlGA03City.DataBind();
                    //设置市下拉选中数据库中对应的区域编码
                    this.ddlGA03City.Value = dt.Rows[0]["CA01013"].ToString();

                }


                //根据行业分类，获取行业分类下所有的行业代码
                DataTable dtCB04 = _CustomerManagementLogic.SelectCustomerTradeCodeByCB04003(dt.Rows[0]["CA01019"].ToString());
                this.ddlCA01020.DataSource = dtCB04;
                this.ddlCA01020.DataTextField = "CB04002";
                this.ddlCA01020.DataValueField = "CB04001";
                this.ddlCA01020.DataBind();
                this.ddlCA01020.Items.Insert(0, new ListItem("请选择", ""));
                //设置行业代码下拉选中数据库中对应的行业编码
                this.ddlCA01020.Value = dt.Rows[0]["CA01020"].ToString();

                this.ddlCA01016.SelectedValue = dt.Rows[0]["CA01016"].ToString();
                this.ddlCA01018.SelectedValue = dt.Rows[0]["CA01018"].ToString();

                this.ddlCA01019.SelectedValue = dt.Rows[0]["CA01019"].ToString();
                this.ddlCA01022.SelectedValue = dt.Rows[0]["CA01022"].ToString();
                this.ddlCA01023.SelectedValue = dt.Rows[0]["CA01023"].ToString();

                if (dt.Rows[0]["CA01024"].ToString().Trim() == "1")
                {
                    this.rbtnCA01024Yes.Checked = true;
                }
                else if (dt.Rows[0]["CA01024"].ToString().Trim() == "0")
                {
                    this.rbtnCA01024No.Checked = true;
                }

                if (dt.Rows[0]["CA01025"].ToString().Trim() == "1")
                {
                    this.rbtnCA01025Yes.Checked = true;
                }
                else if (dt.Rows[0]["CA01025"].ToString().Trim() == "0")
                {
                    this.rbtnCA01025No.Checked = true;
                }

                if (dt.Rows[0]["CA01047"].ToString().Trim() == "1")
                {
                    this.rbtnCA01047Yes.Checked = true;
                }
                else if (dt.Rows[0]["CA01047"].ToString().Trim() == "0")
                {
                    this.rbtnCA01047No.Checked = true;
                }
            }
            catch
            {
                Response.Write("<script>alert('数据异常！');window.location.href = '/Page/CustomerManagement/CA0101List.aspx';</script>");
            }

        }

        /// <summary>
        /// 绑定页面的DropDownList
        /// </summary>
        private void BindDll()
        {
            #region 省市
            //获取省
            DataTable dtGA03 = _DataManagementLogic.SelectSystemAreaForProvince();

            this.ddlGA03Province.DataSource = dtGA03;
            this.ddlGA03Province.DataTextField = "GA03002";
            this.ddlGA03Province.DataValueField = "GA03001";
            this.ddlGA03Province.DataBind();
            this.ddlGA03Province.Items.Insert(0, new ListItem("请选择", ""));

            //为市添加请选择选项
            this.ddlGA03City.Items.Insert(0, new ListItem("请选择", ""));

            #endregion

            #region 客户类型
            DataTable dtCB01 = _CustomerManagementLogic.SelectCustomerType();
            this.ddlCA01016.DataSource = dtCB01;
            this.ddlCA01016.DataTextField = "CB01002";
            this.ddlCA01016.DataValueField = "CB01001";
            this.ddlCA01016.DataBind();
            this.ddlCA01016.Items.Insert(0, new ListItem("请选择", ""));

            #endregion

            #region 客户代码
            DataTable dtCB02 = _CustomerManagementLogic.SelectCustomerCode();
            this.ddlCA01018.DataSource = dtCB02;
            this.ddlCA01018.DataTextField = "CB02002";
            this.ddlCA01018.DataValueField = "CB02001";
            this.ddlCA01018.DataBind();
            this.ddlCA01018.Items.Insert(0, new ListItem("请选择", ""));

            #endregion

            #region 行业分类代码、行业代码
            //行业分类
            DataTable dtCB03 = _CustomerManagementLogic.SelectCustomerTradeType();
            this.ddlCA01019.DataSource = dtCB03;
            this.ddlCA01019.DataTextField = "CB03002";
            this.ddlCA01019.DataValueField = "CB03001";
            this.ddlCA01019.DataBind();
            this.ddlCA01019.Items.Insert(0, new ListItem("请选择", ""));
            //行业代码
            this.ddlCA01020.Items.Insert(0, new ListItem("请选择", ""));
            #endregion

            #region 240电子
            DataTable dtGA05 = _CustomerManagementLogic.SelectElectronType();
            this.ddlCA01022.DataSource = dtGA05;
            this.ddlCA01022.DataTextField = "GA05003";
            this.ddlCA01022.DataValueField = "GA05001";
            this.ddlCA01022.DataBind();
            this.ddlCA01022.Items.Insert(0, new ListItem("请选择", ""));

            #endregion

            #region 220汽车
            DataTable dtGA06 = _CustomerManagementLogic.SelectAuteType();
            this.ddlCA01023.DataSource = dtGA06;
            this.ddlCA01023.DataTextField = "GA06003";
            this.ddlCA01023.DataValueField = "GA06001";
            this.ddlCA01023.DataBind();
            this.ddlCA01023.Items.Insert(0, new ListItem("请选择", ""));

            #endregion
        }

        /// <summary>
        /// 保存数据方法
        /// </summary>
        private void DataSave()
        {
            this.txtCA01002.Focus();
            CustomerBase _CustomerBase = new CustomerBase();
            _CustomerBase.CA01001 = Convert.ToInt32(this.ID.Value);
            _CustomerBase.CA01002 = this.txtCA01002.Text.Trim();
            _CustomerBase.CA01003 = this.txtCA01003.Text.Trim();
            _CustomerBase.CA01004 = this.txtCA01004.Text.Trim();
            _CustomerBase.CA01005 = this.txtCA01005.Text.Trim();
            _CustomerBase.CA01006 = this.txtCA01006.Text.Trim();
            _CustomerBase.CA01007 = this.txtCA01007.Text.Trim();
            _CustomerBase.CA01008 = this.txtCA01008.Text.Trim();
            _CustomerBase.CA01009 = this.txtCA01009.Text.Trim();
            _CustomerBase.CA01010 = this.txtCA01010.Text.Trim();
            _CustomerBase.CA01011 = this.txtCA01011.Text.Trim();
            _CustomerBase.CA01012 = this.txtCA01012.Text.Trim();
            _CustomerBase.CA01013 = Request.Form["ddlGA03City"]; //html控件
            _CustomerBase.CA01014 = this.txtCA01014.Text.Trim();
            _CustomerBase.CA01015 = this.txtCA01015.Text.Trim();
            _CustomerBase.CA01016 = Convert.ToInt32(this.ddlCA01016.SelectedValue);
            _CustomerBase.CA01017 = this.txtCA01017.Text.Trim();
            _CustomerBase.CA01018 = this.ddlCA01018.SelectedValue;
            _CustomerBase.CA01019 = Convert.ToInt32(this.ddlCA01019.SelectedValue);
            _CustomerBase.CA01020 = Convert.ToInt32(Request.Form["ddlCA01020"]);//html控件
            _CustomerBase.CA01021 = this.txtCA01021.Text.Trim();
            _CustomerBase.CA01022 = this.ddlCA01022.SelectedValue;
            _CustomerBase.CA01023 = this.ddlCA01023.SelectedValue;
            if (this.rbtnCA01024Yes.Checked) _CustomerBase.CA01024 = 1;
            if (this.rbtnCA01024No.Checked) _CustomerBase.CA01024 = 0;
            if (this.rbtnCA01025Yes.Checked) _CustomerBase.CA01025 = 1;
            if (this.rbtnCA01025No.Checked) _CustomerBase.CA01025 = 0;
            _CustomerBase.CA01026 = this.txtCA01026.Text.Trim();

            _CustomerBase.CA01027 = this.txtCA01027.Text.Trim();
            _CustomerBase.CA01028 = this.txtCA01028.Text.Trim();
            _CustomerBase.CA01029 = this.txtCA01029.Text.Trim().StartsWith("http://") ? this.txtCA01029.Text.Trim() : "http://" + this.txtCA01029.Text.Trim();
            _CustomerBase.CA01030 = this.txtCA01030.Text.Trim();
            _CustomerBase.CA01031 = this.txtCA01031.Text.Trim();
            _CustomerBase.CA01032 = this.txtCA01032.Text.Trim();
            _CustomerBase.CA01033 = this.txtCA01033.Text.Trim();
            _CustomerBase.CA01034 = this.txtCA01034.Text.Trim();
            _CustomerBase.CA01035 = this.txtCA01035.Text.Trim();
            _CustomerBase.CA01036 = this.txtCA01036.Text.Trim();
            _CustomerBase.CA01037 = this.txtCA01037.Text.Trim();
            _CustomerBase.CA01038 = this.txtCA01038.Text.Trim();
            _CustomerBase.CA01039 = this.txtCA01039.Text.Trim();
            _CustomerBase.CA01040 = this.txtCA01040.Text.Trim();
            _CustomerBase.CA01041 = this.txtCA01041.Text.Trim();
            _CustomerBase.CA01042 = this.txtCA01042.Text.Trim();
            _CustomerBase.CA01043 = this.txtCA01043.Text.Trim();
            _CustomerBase.CA01044 = this.txtCA01044.Text.Trim();
            _CustomerBase.CA01045 = this.txtCA01045.Text.Trim();
            _CustomerBase.CA01046 = this.txtCA01046.Text.Trim();
            _CustomerBase.CA01048 = this.txtCA01048.Text.Trim();
            _CustomerBase.CA01049 = this.txtCA01049.Text.Trim();
            _CustomerBase.CA01050 = this.txtCA01050.Text.Trim();
            _CustomerBase.CA01051 = this.txtCA01051.Text.Trim();
            if (this.rbtnCA01047Yes.Checked) _CustomerBase.CA01047 = 1;
            if (this.rbtnCA01047No.Checked) _CustomerBase.CA01047 = 0;

            //成功失败提示
            new Sinoo.Common.MessageShow().UpdateMessage(this, Math.Abs(_CustomerManagementLogic.UpdateCustomerBase(_CustomerBase)), string.Format("window.location.href='{0}'", LinkReturn()));
        }

        /// <summary>
        /// 返回
        /// </summary>
        public string LinkReturn()
        {
            string strUrl = string.Empty;

            //判断从那个页面进入此页
            if (Request.QueryString["source"] == "View")
            {
                return string.Format("CA0101View.aspx?CA01001={0}&PageIndex={1}", Request.QueryString["CA01001"], Request.QueryString["PageIndex"]);
            }
            else
            {
                return string.Format("CA0101List.aspx?PageIndex={0}", Request.QueryString["PageIndex"]);
            }
        }

        /// <summary>
        /// 初始化加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.IsRose(); //验证权限
            if (!IsPostBack)
            {
                BindDll();//绑定所有下拉
                DataBind();
            }
        }

        /// <summary>
        /// 保存
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