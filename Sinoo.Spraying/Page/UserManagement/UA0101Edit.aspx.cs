using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Sinoo.BLL;
using Sinoo.Model;
using Saas.Common;

namespace Sinoo.Spraying.Page.UserManagement
{
    public partial class UA0101Edit : IsRole
    {

        //实例化逻辑层
        UserBLL _UserBLL = new UserBLL();
        AreaBLL _AreaBLL = new AreaBLL();

        /// <summary>
        /// 数据初始化绑定
        /// </summary>
        private void DataBind()
        {
            if (string.IsNullOrEmpty(Request.QueryString["UA01001"]))
            {
                Response.Redirect("UA0101List.ASPX");
            }
            DataTable dt = _UserBLL.SelectUserBase(string.Format(" AND UA01001 = '{0}'", Request.QueryString["UA01001"]));
            try
            {
                this.UA01001.Value = dt.Rows[0]["UA01001"].ToString();
                this.txtUA01002.Text = dt.Rows[0]["UA01002"].ToString();
                this.txtUA01003.Text = dt.Rows[0]["UA01003"].ToString();
                ViewState["password"] = dt.Rows[0]["UA01003"].ToString();
                this.txtUA01004.Text = dt.Rows[0]["UA01004"].ToString();
                this.txtUA01005.Text = dt.Rows[0]["UA01005"].ToString();
                this.txtUA01006.Text = dt.Rows[0]["UA01006"].ToString();
                this.txtUA01007.Text = dt.Rows[0]["UA01007"].ToString();
                this.ddlUA01009.SelectedValue = dt.Rows[0]["UA01009"].ToString();
                this.txtUA01010.Text = dt.Rows[0]["UA01010"].ToString();
                this.txtUA01011.Text = dt.Rows[0]["UA01011"].ToString();
                this.txtUA01012.Text = dt.Rows[0]["UA01012"].ToString();
                this.ddlUA01013.Text = dt.Rows[0]["UA01013"].ToString();
                this.txtUA01014.Text = dt.Rows[0]["UA01014"].ToString();
                this.txtUA01015.Text = dt.Rows[0]["UA01015"].ToString();
                this.txtUA01018.Text = dt.Rows[0]["UA01018"].ToString();
                this.txtUA01021.Text = dt.Rows[0]["UA01021"].ToString();
                this.ddlUA01024.SelectedValue = dt.Rows[0]["UA01024"].ToString();
                this.ddlUA01025.SelectedValue = dt.Rows[0]["UA01025"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["UA01008"]))
                {
                    this.rbtnUA01008Famale.Checked = false;
                    this.rbtnUA01008Male.Checked = true;
                }
                else
                {
                    this.rbtnUA01008Famale.Checked = true;
                    this.rbtnUA01008Male.Checked = false;
                }
                this.txtUA01016.Text = string.IsNullOrEmpty(dt.Rows[0]["UA01016"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["UA01016"]).ToString("yyyy-MM-dd");
                this.txtUA01017.Text = string.IsNullOrEmpty(dt.Rows[0]["UA01017"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["UA01017"]).ToString("yyyy-MM-dd");
                this.txtUA01019.Text = string.IsNullOrEmpty(dt.Rows[0]["UA01019"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["UA01019"]).ToString("yyyy-MM-dd");
                this.txtUA01020.Text = string.IsNullOrEmpty(dt.Rows[0]["UA01020"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["UA01020"]).ToString("yyyy-MM-dd");
                this.txtUA01022.Text = string.IsNullOrEmpty(dt.Rows[0]["UA01022"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["UA01016"]).ToString("yyyy-MM-dd");
            }
            catch
            {
                Response.Write("<script>alert('数据异常！');window.location.href = '/Page/UserManagement/CA0101List.aspx';</script>");
            }

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
        /// 编辑保存
        /// </summary>
        private void DataSave()
        {
            UserBase _UserBase = new UserBase();
            _UserBase.UA01001 = Convert.ToInt32(Request.QueryString["UA01001"]);
            _UserBase.UA01002 = this.txtUA01002.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtUA01003.Text.Trim()))
            {
                _UserBase.UA01003 = SafetyHandler.GetMD5(this.txtUA01003.Text.Trim());
            }
            else
            {
                _UserBase.UA01003 = ViewState["password"].ToString();
            }
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
            _UserBase.UA01024 = Convert.ToInt32(this.ddlUA01024.SelectedValue);
            _UserBase.UA01025 = this.ddlUA01025.SelectedValue;

            if (this.rbtnUA01008Famale.Checked) { _UserBase.UA01008 = false; } else { _UserBase.UA01008 = true; }

            if (!string.IsNullOrEmpty(this.txtUA01016.Text.Trim())) { _UserBase.UA01016 = Convert.ToDateTime(this.txtUA01016.Text.Trim()); }
            if (!string.IsNullOrEmpty(this.txtUA01017.Text.Trim())) { _UserBase.UA01017 = Convert.ToDateTime(this.txtUA01017.Text.Trim()); }
            if (!string.IsNullOrEmpty(this.txtUA01019.Text.Trim())) { _UserBase.UA01019 = Convert.ToDateTime(this.txtUA01019.Text.Trim()); }
            if (!string.IsNullOrEmpty(this.txtUA01020.Text.Trim())) { _UserBase.UA01020 = Convert.ToDateTime(this.txtUA01020.Text.Trim()); }
            if (!string.IsNullOrEmpty(this.txtUA01022.Text.Trim())) { _UserBase.UA01022 = Convert.ToDateTime(this.txtUA01022.Text.Trim()); }


            int result = _UserBLL.UpdateUserBase(_UserBase);

            //成功失败提示
            new Sinoo.Common.MessageShow().UpdateMessage(this.Page, result, string.Format("window.location.href='{0}'", GetReturnUrl()));

        }

        /// <summary>
        /// 获取返回的链接
        /// </summary>
        /// <returns></returns>
        public string GetReturnUrl()
        {
            string strUrl = string.Empty;

            //判断从那个页面进入此页
            if (Request.QueryString["source"] == "View")
            {
                strUrl = string.Format("UA0101View.aspx?UA01001={0}&PageIndex={1}", Request.QueryString["UA01001"], Request.QueryString["PageIndex"]);
            }
            else
            {
                strUrl = string.Format(string.Format("UA0101List.aspx?PageIndex={0}", Request.QueryString["PageIndex"]));
            }
            return strUrl;
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

                BindDll();//绑定页面下拉列表

                DataBind();//绑定数据
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
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetReturnUrl());
        }
    }
}