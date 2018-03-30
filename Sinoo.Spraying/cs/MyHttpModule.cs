using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Saas.Common;
using Sinoo.BLL;
using Sinoo.Model;

namespace Sinoo.Spraying.cs
{
    public class MyHttpModule : IHttpModule
    {
        public void Dispose() { }


        public void Init(HttpApplication context)
        {

            context.AcquireRequestState += new EventHandler(context_AcquireRequestState);
            context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication ha = (HttpApplication)sender;//获得发生该事件的对象

            string path = ha.Context.Request.Url.ToString();//获得请求的url路径

            int n = path.IndexOf("Login.aspx"); //判断是否是登陆页面
            int m = path.IndexOf("aspx");//判断是否是page

            if (n == -1)//判断不是登陆页面
            {
                if (m > 0)//在发现不是登陆页面的基础上判断是否是page
                {
                    if (ha.Context.Session["USER_SESSION"] == null)//在发现是page的基础上判断有没有session
                    {
                        if (HttpContext.Current.Request.Cookies["UserName"] != null && HttpContext.Current.Request.Cookies["PassWord"] != null)
                        {

                            DataTable dt = new UserBLL().SelectUserBase(string.Format(" AND UA01002 = '{0}' AND UA01003 = '{1}'"
                                , HttpContext.Current.Request.Cookies["UserName"].Value, HttpContext.Current.Request.Cookies["PassWord"].Value));
                            if (dt.Rows.Count > 0)
                            {
                                UserBase _UserBase = new UserBase();
                                _UserBase.UA01001 = Convert.ToInt32(dt.Rows[0]["UA01001"]);
                                _UserBase.UA01004 = dt.Rows[0]["UA01004"].ToString();
                                _UserBase.UA01005 = dt.Rows[0]["UA01005"].ToString();
                                _UserBase.UA01024 = Convert.ToInt32(dt.Rows[0]["UA01024"]);
                                _UserBase.UA01013 = dt.Rows[0]["UA01013"].ToString();
                                _UserBase.UA01009 = Convert.ToInt32(dt.Rows[0]["UA01009"]); //用户职位
                                HttpContext.Current.Session["USER_SESSION"] = _UserBase;
                                ha.Response.Write("</script>window.parent.location.href = '/Index.aspx';</script>");      //发现没有session的时候我重定向页面
                            }

                        }
                        else
                        {
                            ha.Response.Write("<script>alert('用户未登录或登录状态已失效,请重新登录！');window.parent.location.href = '/Login.aspx';</script>");      //发现没有session的时候我重定向页面
                            ha.Response.Redirect("/Login.aspx?NotLogin=Yes");
                        }

                    }
                }
            }
            else
            {
                ha.Context.Session["USER_SESSION"] = null;//清空Session
            }
        }

        public void context_AcquireRequestState(object sender, EventArgs e)
        {


        }
    }
}