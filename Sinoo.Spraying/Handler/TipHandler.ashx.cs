using System;
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
            else if (flag.Equals("customerTip"))
                CustomerTip(context);


        }

        /// <summary>
        /// 客户下单时提醒
        /// </summary>
        /// <param name="context"></param>
        public void CustomerTip(HttpContext context)
        {
            var customerId = context.Request["customerId"];
            if (string.IsNullOrEmpty(customerId))
            {
                context.Response.Write("0");
                return;
            }
            var customerModel = new CustomerBLL().SelectCustomerBaseByID(int.Parse(customerId));
            var customerName = customerModel.Rows[0]["CA01003"].ToString();
            var html = "<p style='margin: 20px 0 10px 20px;text-align:center;'>" + customerName + "</p>";
            //查询客户是否黑名单
            var balckList = customerModel.Rows[0]["CA01052"].ToString();
            html += "<p style='margin: 20px 0 10px 20px;'>黑名单：" + "<span style='color:red;margin-right:30%'>" + (balckList == "0" ? "否" : "是") + "</span>";
            //未提货订单
            int count = new OrderBLL().GetFailDelivery(int.Parse(customerId));
            html += "未提货订单：" + "<span style='color:red;'>" + (count > 0 ? "是" : "否") + "</span></p>";
            //信用金额
            var ca01014 = customerModel.Rows[0]["CA01014"].ToString() == "" ? 0 : Convert.ToInt32(customerModel.Rows[0]["CA01014"].ToString());
            var ca01015 = customerModel.Rows[0]["CA01015"].ToString() == "" ? 0 : Convert.ToDecimal(customerModel.Rows[0]["CA01015"]);
            var deliveredAmount = new OrderBLL().GetDeliveredAmount(int.Parse(customerId));
            var ob01009 = deliveredAmount.Rows[0]["OB01009"].ToString() == "" ? 0 : Convert.ToDecimal(deliveredAmount.Rows[0]["OB01009"]);
            var debtsDays = deliveredAmount.Rows[0]["DebtsDays"].ToString() == "" ? 0 : Convert.ToInt32(deliveredAmount.Rows[0]["DebtsDays"]);
            html += "<p style='margin: 20px 0 10px 20px;'>信用金额：<span style='color:red;'>" + Math.Round((ca01015), 2) + "</span>，信用天数：<span style='color:red;'>" + ca01014 + "</span>，";
            html += "剩余信用金额：<span style='color:red;'>" + Math.Round((ca01015 - ob01009), 2) + "</span>，剩余信用天数：<span style='color:red;'>" + (ca01014 - debtsDays) + "</span></p>";

            //欠款金额   
            var where = " and CA01001 = " + customerId;
            var debts = new OrderBLL().GetDebtsAmountAndDay(where);
            html += "<p style='margin: 20px 0 10px 20px;'>欠款总金额：<span style='color:red;'>" + Convert.ToDouble(debts.Rows[0]["OA01022"]) + "</span>，欠款最长天数："
                + "<span style='color:red;'>" + debts.Rows[0]["DebtsDays"] + "</span></p>";
            context.Response.Write(html);
        }

        public void CheckOrders(HttpContext context)
        {
            var user = (context.Session["USER_SESSION"] as UserBase);
            if (user.UA01024 != 42 && user.UA01024 != 43 && user.UA01024 != 44 && user.UA01024 != 47)
            {
                context.Response.Write("0");
                return;
            }
            string where = user.UA01024 == 43 ? "and OA01013 = '" + user.UA01001 + "'" : "and UA01013 = '" + user.UA01013 + "'";
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
                pageIndex = int.Parse(context.Request["offset"]);
            }
            object RowCount = null;
            var user = (context.Session["USER_SESSION"] as UserBase);
            if (user.UA01024 != 42 && user.UA01024 != 43 && user.UA01024 != 44 && user.UA01024 != 47)
            {
                context.Response.Write("0");
                return;
            }
            string where = user.UA01024 == 43 ? "and OA01013 = '" + user.UA01001 + "'" : "and UA01013 = '" + user.UA01013 + "'";
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
                pageIndex = int.Parse(context.Request["offset"]);
            }
            object RowCount = null;
            var user = (context.Session["USER_SESSION"] as UserBase);
            if (user.UA01024 != 42 && user.UA01024 != 43 && user.UA01024 != 44 && user.UA01024 != 47)
            {
                context.Response.Write("0");
                return;
            }
            string where = user.UA01024 == 43 ? "and OA01013 = '" + user.UA01001 + "'" : "and UA01013 = '" + user.UA01013 + "'";
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