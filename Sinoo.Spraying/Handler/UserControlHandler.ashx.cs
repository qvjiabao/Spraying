using System;
using System.Collections.Generic;
using System.Web;
using Sinoo.BLL;
using Sinoo.Model;
using System.Data;
using System.Text;
namespace Sinoo.Spraying.Handler
{
    /// <summary>
    /// UserControlHandler 的摘要说明
    /// </summary>
    public class UserControlHandler : IHttpHandler
    {
        UserBLL us = new UserBLL();
        CustomerBLL _CustomerBLL = new CustomerBLL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            DataTable dtAll = _CustomerBLL.SelectCustomerCB03();
            string strjson = string.Empty;
            if (context.Request["Type"] == "CB03" && string.IsNullOrEmpty(context.Request.Form["OB04001"])) //判断是否是行业分类
            {
                strjson = ToInitJson(dtAll);
            }
            else if (context.Request["Type"] == "CB03" && !string.IsNullOrEmpty(context.Request.Form["OB04001"]))
            {
                string strOB04001 = context.Request.Form["OB04001"].ToString();
                strOB04001 = strOB04001.Substring(0, strOB04001.Length - 1);
                string[] OB04001 = strOB04001.Split(',');
                DataTable dtPart = ConvertTable("OB04001", OB04001);
                strjson = ToEditJson(dtAll, dtPart);
            }
            context.Response.Write(strjson);
        }
        /// <summary>
        /// 把数组转换成DataTable
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <param name="Array"></param>
        /// <returns></returns>
        public DataTable ConvertTable(string ColumnName, string[] Array)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(ColumnName, typeof(string));

            for (int i = 0; i < Array.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr[ColumnName] = Array[i].ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 将DataTable转换成Json格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ToInitJson(DataTable dt)
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

                string strChecked = "";
                
                if (dt.Rows[i]["pId"].ToString() == "0")
                {
                    jsonBuilder.Append(",\"chkDisabled\" : \"false\" " + strChecked + " , \"open\":\"true\"  },");
                }
                else
                {
                    jsonBuilder.Append(" ,\"chkDisabled\" : \"false\" " + strChecked + " },");
                }

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// 将DataTable转换成Json格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ToViewJson(DataTable dtAll, DataTable dtpart)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dtAll.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dtAll.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dtAll.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dtAll.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);

                string strChecked = "";
                DataRow[] dr = dtpart.Select(string.Format(" GC01002 ='{0}' ", dtAll.Rows[i]["id"].ToString()));
                if (dr.Length > 0)  //判断当前角色是否有当前菜单权限
                {
                    strChecked = " ,\"checked\" : \"true\" ";
                }

                jsonBuilder.Append(" ,\"chkDisabled\" : \"true\" " + strChecked + " },");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// 将DataTable转换成Json格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ToEditJson(DataTable dtAll, DataTable dtpart)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dtAll.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dtAll.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dtAll.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dtAll.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);

                string strChecked = "";
                DataRow[] dr = dtpart.Select(string.Format(" OB04001 ='{0}' ", dtAll.Rows[i]["id"].ToString()));
                if (dr.Length > 0)
                {
                    strChecked = " ,\"checked\" : \"true\" ";
                }

                jsonBuilder.Append(" ,\"chkDisabled\" : \"false\" " + strChecked + ",\"open\":\"true\" },");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}