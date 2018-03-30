using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.Model;
using Sinoo.BLL;
using System.Data;
using Saas.Common;

namespace Sinoo.Spraying.Page.UserManagement
{
    public partial class UA0101New : IsRole
    {
        //实例化逻辑层
        UserBLL _UserBLL = new UserBLL();
        AreaBLL _AreaBLL = new AreaBLL();
        /// <summary>
        /// 保存数据方法
        /// </summary>
        private void DataSave()
        {
            UserBase _UserBase = new UserBase();
            _UserBase.UA01002 = this.txtUA01002.Text.Trim();
            _UserBase.UA01003 = SafetyHandler.GetMD5(this.txtUA01003.Text.Trim());
            _UserBase.UA01004 = this.txtUA01004.Text.Trim();
            _UserBase.UA01005 = this.txtUA01005.Text.Trim();
            _UserBase.UA01006 = this.txtUA01006.Text.Trim();
            _UserBase.UA01007 = this.txtUA01007.Text.Trim();
            if (!string.IsNullOrEmpty(this.ddlUA01009.SelectedValue))
                _UserBase.UA01009 = Convert.ToInt32(this.ddlUA01009.SelectedValue);
            _UserBase.UA01010 = this.txtUA01010.Text.Trim();
            _UserBase.UA01011 = this.txtUA01011.Text.Trim();
            _UserBase.UA01012 = this.txtUA01012.Text.Trim();
            _UserBase.UA01013 = this.ddlUA01013.SelectedValue;
            _UserBase.UA01014 = this.txtUA01014.Text.Trim();
            _UserBase.UA01015 = this.txtUA01015.Text.Trim();
            _UserBase.UA01018 = this.txtUA01018.Text.Trim();
            _UserBase.UA01021 = this.txtUA01021.Text.Trim();
            _UserBase.UA01023 = true;
            _UserBase.UA01024 = Convert.ToInt32(this.ddlUA01024.SelectedValue);

            _UserBase.UA01025 = this.ddlUA01025.SelectedValue;

            if (this.rbtnUA01008Famale.Checked) { _UserBase.UA01008 = false; } else { _UserBase.UA01008 = true; }
            if (!string.IsNullOrEmpty(this.txtUA01016.Text.Trim())) { _UserBase.UA01016 = Convert.ToDateTime(this.txtUA01016.Text.Trim()); }
            if (!string.IsNullOrEmpty(this.txtUA01017.Text.Trim())) { _UserBase.UA01017 = Convert.ToDateTime(this.txtUA01017.Text.Trim()); }
            if (!string.IsNullOrEmpty(this.txtUA01019.Text.Trim())) { _UserBase.UA01019 = Convert.ToDateTime(this.txtUA01019.Text.Trim()); }
            if (!string.IsNullOrEmpty(this.txtUA01020.Text.Trim())) { _UserBase.UA01020 = Convert.ToDateTime(this.txtUA01020.Text.Trim()); }
            if (!string.IsNullOrEmpty(this.txtUA01022.Text.Trim())) { _UserBase.UA01022 = Convert.ToDateTime(this.txtUA01022.Text.Trim()); }


            int result = _UserBLL.InsertUserBase(_UserBase);

            //成功失败提示
            new Sinoo.Common.MessageShow().InsertMessage(this.Page, result, "DataClear();");

        }

        /// <summary>
        /// 绑定页面的DropDownList
        /// </summary>
        private void BindDll()
        {
            #region 省市

            //获取职位
            DataTable dtUA01 = _UserBLL.SelectUserJob();
            this.ddlUA01009.DataSource = dtUA01;
            this.ddlUA01009.DataTextField = "UB01002";
            this.ddlUA01009.DataValueField = "UB01001";
            this.ddlUA01009.DataBind();
            this.ddlUA01009.Items.Insert(0, new ListItem("请选择", ""));

            //获取市
            //DataTable dtGA03 = _AreaBLL.SelectSystemAreaForCity();

            //this.ddlUA01013.DataSource = dtGA03;
            //this.ddlUA01013.DataTextField = "GA03002";
            //this.ddlUA01013.DataValueField = "GA03001";
            //this.ddlUA01013.DataBind();
            //this.ddlUA01013.Items.Insert(0, new ListItem("请选择", ""));

            //获取角色
            DataTable dtGA02 = _UserBLL.SelectSystemRole();

            this.ddlUA01024.DataSource = dtGA02;
            this.ddlUA01024.DataTextField = "GA02002";
            this.ddlUA01024.DataValueField = "GA02001";
            this.ddlUA01024.DataBind();
            this.ddlUA01024.Items.Insert(0, new ListItem("请选择", ""));

            #endregion
        }

        /// <summary>
        /// 返回跳转
        /// </summary>
        private void LinkReturn()
        {
            Response.Redirect(string.Format(string.Format("UA0101List.aspx?PageIndex={0}", Request.QueryString["PageIndex"])));
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

                BindDll();  //绑定下拉列表数据
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