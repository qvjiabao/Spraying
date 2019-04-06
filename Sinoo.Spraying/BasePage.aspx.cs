
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Sinoo.BLL;

namespace Sinoo.Spraying
{
    public partial class BasePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 获取团队数据源
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, string> GetTeamData()
        {

            Dictionary<string,string> table = new Dictionary<string,string>();
            table.Add("", "请选择");
            table.Add("全区域", "AllAera");
            table.Add("北京", "Beijing");
            table.Add("天津", "TianJin");
            table.Add("西安", "XiAn");
            table.Add("沈阳", "ShenYang");
            table.Add("钢铁", "Steelteam");
            table.Add("Fluid Air", "Fluid Air");
            table.Add("BOF", "BOF");
            table.Add("North", "North");

            return table;
        }


        public void IsRose()
        {
            string strPath = Server.HtmlEncode(Request.FilePath); //请求的虚拟路径
            if (strPath != "/Index.aspx")
            {
                Model.UserBase _UserBase = Session["USER_SESSION"] as Model.UserBase; //获取当前登录人能访问的页面权限

                DataTable dt = new UserBLL().SelectSystemRole(Convert.ToInt32(_UserBase.UA01024));
                bool bl = false;
                foreach (DataRow item in dt.Rows)
                {
                    if (item["GA01004"].ToString() == strPath)
                    {
                        bl = true;
                        break;
                    }
                }

                if (!bl)
                {
                    Response.Redirect("/Login.aspx?NotLogin=Yes");
                }
            }
        }
    }
}