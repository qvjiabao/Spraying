using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Newtonsoft.Json;
using System.Web.SessionState;
using Sinoo.BLL;
using Sinoo.Common;
using Sinoo.Model;

namespace Sinoo.Spraying.Handler
{
    /// <summary>
    /// TipHandler 的摘要说明
    /// </summary>
    public class TipHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var flag = context.Request["flag"];
            if (flag.Equals("debts"))
                GetDebtsList(context);
            else
                GetPendingList(context);
        }

        /// <summary>
        /// 欠款订单
        /// </summary>
        /// <param name="context"></param>
        public void GetDebtsList(HttpContext context)
        {


            var user = (context.Session["USER_SESSION"] as UserBase);
            Hashtable tab = new Hashtable();
            tab["total"] = 3;
            tab["rows"] = new List<object>() { new { TITLE = "未发货订单1" }, new { TITLE = "未发货订单2" } };
            var json = JsonConvert.SerializeObject(tab);
            context.Response.Write(json);

        }

        /// <summary>
        /// 未发货订单
        /// </summary>
        /// <param name="context"></param>
        public void GetPendingList(HttpContext context)
        {
            var pageSize = 5;
            var pageIndex = 1;
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                pageSize = int.Parse(context.Request["limit"]);
            }
            if (!string.IsNullOrEmpty(context.Request["offset"]))
            {
                pageIndex = int.Parse(context.Request["offset"])+1;
            }
            object RowCount = null;
            var user = (context.Session["USER_SESSION"] as UserBase);
            Hashtable tab = new Hashtable();
            DataTable dt = new OrderBLL().GetDebtsList(pageIndex, pageSize, user.UA01001, ref RowCount);
            var list = new DataTableHandler().ToList<Pending>(dt);
            tab["total"] = RowCount;
            tab["rows"] = list;
            var json = JsonConvert.SerializeObject(tab);
            context.Response.Write(json);
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