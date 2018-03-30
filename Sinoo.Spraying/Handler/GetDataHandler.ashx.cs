using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Sinoo.BLL;
using System.Text;
using Sinoo.Model;
using System.Web.SessionState;

namespace Sinoo.Spraying.Handler
{
    /// <summary>
    /// GetDataHandler 的摘要说明
    /// </summary>
    public class GetDataHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string value = context.Request.Form["value"];

            DataTable dt = null;
            if (context.Request.Form["Type"] == "OB01")
            {
                dt = GetOB01(value);
            }
            else if (context.Request.Form["Type"] == "CA01")
            {
                dt = GetCA01(value, context.Request.Form["value1"], context.Request.Form["value2"]);
            }
            else if (context.Request.Form["Type"] == "OA01SalesName")
            {
                dt = GetOA01SalesName(value);
            }
            else if (context.Request.Form["Type"] == "OB01Tui")
            {
                dt = GetOB01Tui(value, context.Request.Form["id"]);
            }
            else if (context.Request.Form["Type"] == "MessageArrived")
            {
                if (context.Session["USER_SESSION"] != null && (context.Session["USER_SESSION"] as UserBase).UA01024 != 43)
                {
                    dt = GetMessageArrived(Convert.ToInt32((context.Session["USER_SESSION"] as UserBase).UA01001));
                }
            }
            else if (context.Request.Form["Type"] == "MessageDelivered")
            {
                if (context.Session["USER_SESSION"] != null && (context.Session["USER_SESSION"] as UserBase).UA01024 != 43)
                {
                    dt = GetMessageDelivered(Convert.ToInt32((context.Session["USER_SESSION"] as UserBase).UA01001));
                }
            }
            else if (context.Request.Form["Type"] == "NoPayment" && (context.Session["USER_SESSION"] as UserBase).UA01024 != 43)
            {
                if (context.Session["USER_SESSION"] != null)
                {
                    dt = GetMessageNoPayment(Convert.ToInt32((context.Session["USER_SESSION"] as UserBase).UA01001));
                }
            }



            //将DataTable序列化为Json格式
            string strJson = ToJson(dt);

            //将结果打回客户端
            context.Response.Write(strJson);
        }

        /// <summary>
        /// 根据产品编号获取产品信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable GetOB01(string value)
        {
            ProductBLL _ProductBLL = new ProductBLL();
            DataTable dt = _ProductBLL.SelectProductBaseByName(value);
            return dt;
        }


        /// <summary>
        /// 根据用户ID查询到货提醒
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable GetMessageArrived(int id)
        {
            OrderBLL _OrderBLL = new OrderBLL();
            DataTable dt = _OrderBLL.SelectMessageArrival(id);
            return dt;
        }

        /// <summary>
        /// 根据用户ID查询发货提醒
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable GetMessageDelivered(int id)
        {
            OrderBLL _OrderBLL = new OrderBLL();
            DataTable dt = _OrderBLL.SelectMessageDelivered(id);
            return dt;
        }

        /// <summary>
        /// 根据用户ID查询发货提醒
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable GetMessageNoPayment(int id)
        {
            OrderBLL _OrderBLL = new OrderBLL();
            DataTable dt = _OrderBLL.SelectMessageNoPayment(id);
            return dt;
        }



        /// <summary>
        /// 根据产品编号获取产品信息(退货单)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable GetOB01Tui(string value, string id)
        {
            ProductBLL _ProductBLL = new ProductBLL();
            DataTable dt = _ProductBLL.SelectProductBaseByName(value, id);
            return dt;
        }

        /// <summary>
        /// 根据订单编号获取订单信息（退单所用 显示销售员）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable GetOA01SalesName(string value)
        {
            OrderBLL _OrderBLL = new OrderBLL();
            DataTable dt = _OrderBLL.SelectOA01SalesName(value);
            return dt;
        }

        /// <summary>
        /// 根据条件查询客户
        /// </summary>
        /// <param name="GA03001">城市ID</param>
        private DataTable GetCA01(string ProvinceID, string CityID, string CustomerName)
        {
            //实例化省市逻辑层
            CustomerBLL _CustomerBLL = new CustomerBLL();
            if (ProvinceID.Trim() == "" && CityID.Trim() == "" && CustomerName == "")
            {
                return new DataTable();
            }
            string strWhere = "";
            if (ProvinceID.Trim() != "")
            {
                strWhere += string.Format(" AND ProvinceID = '{0}' ", ProvinceID);
            }
            if (CityID.Trim() != "")
            {
                strWhere += string.Format(" AND  CityID = '{0}' ", CityID);
            }
            if (CustomerName.Trim() != "")
            {
                strWhere += string.Format(" AND  CA01003 like '%{0}%' ", CustomerName);
            }
            DataTable dt = _CustomerBLL.SelectCustomerBaseForList(strWhere);

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

            if (dt != null && dt.Rows.Count > 0)
            {
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
            }
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
