using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Sinoo.BLL;

namespace Sinoo.Spraying
{
    public partial class IsRole : System.Web.UI.Page
    {

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