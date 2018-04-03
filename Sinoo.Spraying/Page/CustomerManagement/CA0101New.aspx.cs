using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using Sinoo.Model;
using System.Data;
using System.IO;

namespace Sinoo.Spraying.Page.CustomerManagement
{
    public partial class CA0101New : IsRole
    {
        #region 实例化逻辑层

        //实例化客户逻辑层
        CustomerBLL _CustomerBLL = new CustomerBLL();

        //实例化省市逻辑层
        AreaBLL _AreaBLL = new AreaBLL();

        #endregion

        /// <summary>
        /// 保存数据方法
        /// </summary>
        private void DataSave()
        {

            CustomerBase _CustomerBase = new CustomerBase();
            _CustomerBase.CA01002 = this.txtCA01002.Text.Trim();
            _CustomerBase.CA01003 = this.txtCA01003.Text.Trim();
            _CustomerBase.CA01004 = this.txtCA01004.Text.Trim().Replace("'","’");
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

            if (this.rbtnCA01052Yes.Checked) _CustomerBase.CA01052 = 1;
            if (this.rbtnCA01052No.Checked) _CustomerBase.CA01052 = 0;
            _CustomerBase.CA01999 = Guid.NewGuid().ToString();
            int result = 0;
            try
            {
                result = _CustomerBLL.AddCustomerBase(_CustomerBase);//执行保存文件和保存用户基础资料的存储过程
            }
            catch (System.Exception Ex)
            {
                throw Ex;
            }

            //成功失败提示
            new Sinoo.Common.MessageShow().InsertMessage(this, result, "DataClear();");

        }

        /// <summary>
        /// 返回跳转
        /// </summary>
        private void LinkReturn()
        {
            Response.Redirect(string.Format("CA0101List.aspx?PageIndex={0}", Request.QueryString["PageIndex"]));
        }


        /// <summary>
        /// 绑定页面的DropDownList
        /// </summary>
        private void BindDll()
        {
            #region 省市
            //获取省
            DataTable dtGA03 = _AreaBLL.SelectSystemAreaForProvince();

            this.ddlGA03Province.DataSource = dtGA03;
            this.ddlGA03Province.DataTextField = "GA03002";
            this.ddlGA03Province.DataValueField = "GA03001";
            this.ddlGA03Province.DataBind();
            this.ddlGA03Province.Items.Insert(0, new ListItem("请选择", ""));

            //为市添加请选择选项
            this.ddlGA03City.Items.Insert(0, new ListItem("请选择", ""));

            #endregion

            #region 客户类型
            DataTable dtCB01 = _CustomerBLL.SelectCustomerType();
            this.ddlCA01016.DataSource = dtCB01;
            this.ddlCA01016.DataTextField = "CB01002";
            this.ddlCA01016.DataValueField = "CB01001";
            this.ddlCA01016.DataBind();
            this.ddlCA01016.Items.Insert(0, new ListItem("请选择", ""));

            #endregion

            #region 客户代码
            DataTable dtCB02 = _CustomerBLL.SelectCustomerCode();
            this.ddlCA01018.DataSource = dtCB02;
            this.ddlCA01018.DataTextField = "CB02002";
            this.ddlCA01018.DataValueField = "CB02001";
            this.ddlCA01018.DataBind();
            this.ddlCA01018.Items.Insert(0, new ListItem("请选择", ""));

            #endregion

            #region 行业分类代码、行业代码
            //行业分类
            DataTable dtCB03 = _CustomerBLL.SelectCustomerTradeType();
            this.ddlCA01019.DataSource = dtCB03;
            this.ddlCA01019.DataTextField = "CB03002";
            this.ddlCA01019.DataValueField = "CB03001";
            this.ddlCA01019.DataBind();
            this.ddlCA01019.Items.Insert(0, new ListItem("请选择", ""));
            //行业代码
            this.ddlCA01020.Items.Insert(0, new ListItem("请选择", ""));
            #endregion

            #region 240电子
            DataTable dtGA05 = _CustomerBLL.SelectElectronType();
            this.ddlCA01022.DataSource = dtGA05;
            this.ddlCA01022.DataTextField = "GA05003";
            this.ddlCA01022.DataValueField = "GA05001";
            this.ddlCA01022.DataBind();
            this.ddlCA01022.Items.Insert(0, new ListItem("请选择", ""));

            #endregion

            #region 220汽车
            DataTable dtGA06 = _CustomerBLL.SelectAuteType();
            this.ddlCA01023.DataSource = dtGA06;
            this.ddlCA01023.DataTextField = "GA06003";
            this.ddlCA01023.DataValueField = "GA06001";
            this.ddlCA01023.DataBind();
            this.ddlCA01023.Items.Insert(0, new ListItem("请选择", ""));

            #endregion
        }


        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            this.IsRose();  //验证权限
            if (!IsPostBack)
            {
                //绑定页面下拉框
                BindDll();
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
            LinkReturn();
        }
    }
}