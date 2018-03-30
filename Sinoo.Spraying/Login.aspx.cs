using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using Saas.Common;
using System.Data;
using Sinoo.Model;

namespace Sinoo.Spraying
{
    public partial class Login : System.Web.UI.Page
    {

        UserBLL _UserBLL = new UserBLL();

        /// <summary>
        /// 用户登录
        /// </summary>
        private void LoginOn()
        {
            DataTable dt = _UserBLL.SelectUserBase(string.Format(" AND UA01002 = '{0}' AND UA01003 = '{1}'", this.txtUserName.Text.Trim(), SafetyHandler.GetMD5(this.txtPassWord.Text.Trim())));
            if (dt.Rows.Count > 0)
            {
                UserBase _UserBase = new UserBase();
                _UserBase.UA01001 = Convert.ToInt32(dt.Rows[0]["UA01001"]);
                _UserBase.UA01004 = dt.Rows[0]["UA01004"].ToString();
                _UserBase.UA01005 = dt.Rows[0]["UA01005"].ToString();
                _UserBase.UA01024 = Convert.ToInt32(dt.Rows[0]["UA01024"]);
                _UserBase.UA01013 = dt.Rows[0]["UA01013"].ToString();
                _UserBase.UA01009 = Convert.ToInt32(dt.Rows[0]["UA01009"]); //用户职位

                HttpCookie cookie = new HttpCookie("UserName");
                cookie.Value = this.txtUserName.Text.Trim();
                cookie.Expires = DateTime.Now.AddDays(10);
                HttpContext.Current.Response.Cookies.Add(cookie);

                cookie = new HttpCookie("PassWord");
                cookie.Value = SafetyHandler.GetMD5(this.txtPassWord.Text.Trim());
                cookie.Expires = DateTime.Now.AddDays(10);
                HttpContext.Current.Response.Cookies.Add(cookie);
                
                Session["USER_SESSION"] = _UserBase;
                Response.Redirect("/Index.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('用户名或密码不正确！');</script>");
            }
        }
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        /// <summary>
        /// 登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginOn();
        }
    }
}