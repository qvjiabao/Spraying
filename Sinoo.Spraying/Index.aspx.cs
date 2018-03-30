using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sinoo.BLL;
using System.Data;
using Sinoo.Model;
using Saas.Common;

namespace Sinoo.Spraying
{
    public partial class Index : System.Web.UI.Page
    {
        //实例化用户逻辑层
        UserBLL _UserBLL = new UserBLL();

        /// <summary>
        /// 生成菜单
        /// </summary>
        /// <returns></returns>
        public string CreateMenu()
        {
            string strHtml = "";
            DataTable dt = _UserBLL.SelectRoleMenu(string.Format(" AND GC02001 = {0}", (Session["USER_SESSION"] as UserBase).UA01024));
            Dictionary<string, List<SystemMenu>> dictionaryMenu = new Dictionary<string, List<SystemMenu>>();

            foreach (DataRow item in dt.Rows)
            {
                if (!dictionaryMenu.Keys.Contains(item["FName"]))
                {

                    dictionaryMenu.Add(item["FName"].ToString(), new List<SystemMenu>());
                }

                SystemMenu _SystemMenu = new SystemMenu();
                _SystemMenu.GA04002 = item["GA04002"].ToString();
                _SystemMenu.GA04004 = item["GA04004"].ToString();

                dictionaryMenu[item["FName"].ToString()].Add(_SystemMenu);
            }


            foreach (var item in dictionaryMenu)
            {
                strHtml += string.Format("<li class=\"submenu\"><a href=\"#\"><i class=\"icon icon-align-justify\"></i><span>{0}</span></a>", item.Key);
                if (item.Value.Count > 0)
                {
                    strHtml += "<ul>";
                    foreach (SystemMenu menu in item.Value)
                    {
                        strHtml += string.Format("<li><a href=\"{0}\" target=\"iframe\">{1}</a></li>", menu.GA04004, menu.GA04002);
                    }
                    strHtml += "</ul>";
                }
                strHtml += "</li>";
            }
            return strHtml;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = _UserBLL.SelectRoleMenu(string.Format(" AND GC02001 = {0}", (Session["USER_SESSION"] as UserBase).UA01001));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserBase _UserBase = new UserBase();
            _UserBase.UA01001 = (Session["USER_SESSION"] as UserBase).UA01001;
            _UserBase.UA01003 = SafetyHandler.GetMD5(this.txtNewUA01003.Text);
            int num = _UserBLL.UpdatePwd(_UserBase);
            new Sinoo.Common.MessageShow().UpdateMessage(this, num, string.Format("window.location.href='{0}'", "Login.aspx"));
        }
    }
}