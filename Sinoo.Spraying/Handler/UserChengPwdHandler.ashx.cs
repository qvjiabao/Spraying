using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sinoo.BLL;
using Sinoo.Model;
using System.Data;
using System.Web.SessionState;
using Saas.Common;
using System.Web.Script.Serialization;
namespace Sinoo.Spraying.Handler
{
    /// <summary>
    /// UserChengPwdHandler 的摘要说明
    /// </summary>
    public class UserChengPwdHandler : IHttpHandler, IReadOnlySessionState
    {
        UserBLL _UserBLL = new UserBLL();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string value = context.Request.Form[0];

            Dictionary<string, string> d = new Dictionary<string, string>();

            if (context.Request.Form[1] == "CheckOldPwd")
            {
                d = CheckOldPwd(value, context);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                context.Response.Write(jss.Serialize(d));
            }
            //if (context.Request.Form[1] == "UpdatePwd")
            //{
            //    UserBase _UserBase = new UserBase();
            //    _UserBase.UA01001 = (context.Session["USER_SESSION"] as UserBase).UA01001;
            //    _UserBase.UA01003 = SafetyHandler.GetMD5(value);
            //    int num = _UserBLL.UpdatePwd(_UserBase);
            //    string resultbool = num == 1 ? "true" : "false";
            //    context.Session.Clear();
            //    context.Response.Write(resultbool);
            //}

        }

        /// <summary>
        /// 检查旧密码是否正确
        /// </summary>
        private Dictionary<string, string> CheckOldPwd(string value, HttpContext context)
        {

            Dictionary<string, string> d = new Dictionary<string, string>();
            string strMd5 = SafetyHandler.GetMD5(value);
            DataTable dt = _UserBLL.SelectUserBaseByID(Convert.ToInt32((context.Session["USER_SESSION"] as UserBase).UA01001), strMd5);

            if (dt.Rows.Count > 0)
            {
                d.Add("ok", "");
            }
            else
            {
                d.Add("error", "旧密码输入不正确");
            }
            return d;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}