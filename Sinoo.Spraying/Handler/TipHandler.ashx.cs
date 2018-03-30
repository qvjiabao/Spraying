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
            else if (flag.Equals("pending"))
                GetPendingList(context);
            else if (flag.Equals("checkOrders"))
                CheckOrders(context);
        }

        public void CheckOrders(HttpContext context)
        {
            var user = (context.Session["USER_SESSION"] as UserBase);
            if (user.UA01024 != 42 && user.UA01024 != 43 && user.UA01024 != 44)
            {
                context.Response.Write("0");
                return;
            }
            string where = user.UA01024 == 42 ? "and OA01013 = '" + user.UA01001 + "'" : "and UA01013 = '" + user.UA01013 + "'";
            if (user.UA01013 == "全区域")
            {
                where = string.Empty;
            }
            object RowCount = null;
            var dtDebts = new OrderBLL().GetDebtsList(1, 1, where, ref RowCount);
            var debtsCount = dtDebts.Rows.Count;
            var dtPending = new OrderBLL().GetPengdingList(1, 1, where, ref RowCount);
            var pendingCount = dtPending.Rows.Count;
            if (debtsCount > 0 && pendingCount > 0)
            {
                context.Response.Write("1,2");
                return;
            }
            if (debtsCount > 0 && pendingCount == 0)
            {
                context.Response.Write("1");
                return;
            }
            if (debtsCount == 0 && pendingCount > 0)
            {
                context.Response.Write("2");
            }
            else
            {
                context.Response.Write("0");
            }
        }

        /// <summary>
        /// 欠款订单
        /// </summary>
        /// <param name="context"></param>
        public void GetDebtsList(HttpContext context)
        {
            var pageSize = 5;
            var pageIndex = 1;
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                pageSize = int.Parse(context.Request["limit"]);
            }
            if (!string.IsNullOrEmpty(context.Request["offset"]))
            {
                pageIndex = int.Parse(context.Request["offset"]) + 1;
            }
            object RowCount = null;
            var user = (context.Session["USER_SESSION"] as UserBase);
            if (user.UA01024 != 42 && user.UA01024 != 43 && user.UA01024 != 44)
            {
                context.Response.Write("0");
                return;
            }
            string where = user.UA01024 == 42 ? "and OA01013 = '" + user.UA01001 + "'" : "and UA01013 = '" + user.UA01013 + "'";
            if (user.UA01013 == "全区域")
            {
                where = string.Empty;
            }
            DataTable dt = new OrderBLL().GetDebtsList(pageIndex, pageSize, where, ref RowCount);
            var list = new DataTableHandler().ToList<Debts>(dt);
            Hashtable tab = new Hashtable();
            tab["total"] = RowCount;
            tab["rows"] = list;
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
                pageIndex = int.Parse(context.Request["offset"]) + 1;
            }
            object RowCount = null;
            var user = (context.Session["USER_SESSION"] as UserBase);
            if (user.UA01024 != 42 && user.UA01024 != 43 && user.UA01024 != 44)
            {
                context.Response.Write("0");
                return;
            }
            string where = user.UA01024 == 42 ? "and OA01013 = '" + user.UA01001 + "'" : "and UA01013 = '" + user.UA01013 + "'";
            if (user.UA01013 == "全区域")
            {
                where = string.Empty;
            }
            DataTable dt = new OrderBLL().GetPengdingList(pageIndex, pageSize, where, ref RowCount);
            var list = new DataTableHandler().ToList<Pending>(dt);
            Hashtable tab = new Hashtable();
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