using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Sinoo.BLL;

namespace Sinoo.Spraying.Handler
{
    /// <summary>
    /// CommonHandler 的摘要说明
    /// </summary>
    public class CommonHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Request["orderNo"]))
            {
                context.Response.Write(GetExchangeRate());
            }
            else
            {
                context.Response.Write(GetExchangeRateByOrderNo(context.Request["orderNo"]));
            }
        }

        /// <summary>
        /// 根据订单编号获取税率
        /// </summary>
        /// <returns></returns>
        public string GetExchangeRateByOrderNo(string orderNo)
        {
            DataTable dt = new AreaBLL().GetSettings(orderNo);
            return dt.Rows[0]["Value"].ToString();
        }

        /// <summary>
        /// 获取税率
        /// </summary>
        /// <returns></returns>
        public string GetExchangeRate()
        {
            DataTable dt = new AreaBLL().GetSettings();
            return dt.Rows[0]["Value"].ToString();
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