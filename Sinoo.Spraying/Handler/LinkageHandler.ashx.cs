using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sinoo.BLL;
using System.Data;
using System.Web.Script.Serialization;
using System.Text;

namespace Sinoo.Spraying.Handler
{
    /// <summary>
    /// 级联
    /// </summary>
    public class LinkageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string FatherId = context.Request.Form["Fid"];

            DataTable dt = null;

            if (context.Request.Form["Type"] == "GA03")
            {
                dt = GetGA03(FatherId);
            }
            else if (context.Request.Form["Type"] == "CB04")
            {
                dt = GetCB04(FatherId);
            }
            else if (context.Request.Form["Type"] == "UA01")
            {
                dt = GetUA01(FatherId);
            }
            else if (context.Request.Form["Type"] == "UA01ByArea")
            {
                dt = GetMarketByArea(FatherId);
            }
            else if (context.Request.Form["Type"] == "OA01")
            {
                dt = GetOA01(context.Request.Form["CA01003"], context.Request.Form["OA01002"]);
            }
            else if (context.Request.Form["Type"] == "UA01SalesName")
            {
                dt = UA01SalesName(FatherId); ;
            }
            //将DataTable序列化为Json格式
            string strJson = ToJson(dt);

            //将结果打回客户端
            context.Response.Write(strJson);
        }

        /// <summary>
        /// 省市级联
        /// </summary>
        /// <param name="GA03001">父级ID</param>
        private DataTable GetGA03(string GA03001)
        {
            //实例化省市逻辑层
            AreaBLL _AreaBLL = new AreaBLL();
            DataTable dt = _AreaBLL.SelectSystemAreaForCity(GA03001);
            return dt;
        }

        private DataTable GetCB04(string CB04003)
        {
            //实例化行业代码逻辑层
            CustomerBLL _CustomerBLL = new CustomerBLL();
            DataTable dt = _CustomerBLL.SelectCustomerTradeCodeByCB04003(CB04003);
            return dt;
        }

        /// <summary>
        /// 根据地域获取用户
        /// </summary>
        /// <param name="UA01013"></param>
        /// <returns></returns>
        private DataTable GetUA01(string UA01013)
        {
            //实例化行业代码逻辑层
            UserBLL _UserBLL = new UserBLL();
            DataTable dt = _UserBLL.SelectUserBase(string.Format(" AND UA01013 = '{0}'", UA01013));
            return dt;
        }

        /// <summary>
        /// 根据地域获取用户
        /// </summary>
        /// <param name="UA01013"></param>
        /// <returns></returns>
        private DataTable UA01SalesName(string UA01013)
        {
            //实例化行业代码逻辑层
            UserBLL _UserBLL = new UserBLL();
            DataTable dt = _UserBLL.SelectUserBase(string.Format(" AND UA01013 = '{0}' ", UA01013));
            return dt;
        }

        /// <summary>
        /// 根据地域获取用户(销售人员)
        /// </summary>
        /// <param name="UA01013"></param>
        /// <returns></returns>
        private DataTable GetMarketByArea(string UA01013)
        {
            //实例化行业代码逻辑层
            UserBLL _UserBLL = new UserBLL();
            DataTable dt = _UserBLL.SelectUserBaseByAera(string.Format("  AND UA01997 = 0 AND UA01013 = '{0}'", UA01013));
            return dt;
        }
        /// <summary>
        /// 根据客户名称和订单号查询附件
        /// </summary>
        /// <param name="UA01013"></param>
        /// <returns></returns>
        private DataTable GetOA01(string CA01003, string OA01002)
        {
            //实例化行业代码逻辑层
            OrderBLL _OrderBLL = new OrderBLL();
            DataTable dt = _OrderBLL.SelectOrderByCustomerOrderID(CA01003, OA01002);
            return dt;
        }
        /// <summary>
        /// 将DataTable转换成Json格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ToJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
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