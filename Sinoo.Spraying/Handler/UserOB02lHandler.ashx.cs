using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sinoo.BLL;
using System.Data;
using System.Text;

namespace Sinoo.Spraying.Handler
{
    /// <summary>
    /// UserOB02lHandler 的摘要说明
    /// </summary>
    public class UserOB02lHandler : IHttpHandler
    {
        OrderBLL _OrderBLL = new OrderBLL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            DataTable dtOB02 = _OrderBLL.SelectOB02Code(); //查询全部应用代码 
            string strjson = string.Empty;
            if (context.Request["Type"]== "OB02" &&string.IsNullOrEmpty(context.Request.Form["OB02001"]))  
            {
                strjson = ToInitJson(dtOB02);
            }
            else if (context.Request["Type"] == "OB02" && !string.IsNullOrEmpty(context.Request.Form["OB02001"]))
            {
                string strOB02001 = context.Request.Form["OB02001"].ToString();
                strOB02001 = strOB02001.Substring(0, strOB02001.Length - 1);
                string[] OB02001 = strOB02001.Split(',');
                DataTable dtPart = ConvertTable("OB02001", OB02001);
                strjson = ToEditJson(dtOB02, dtPart);
            }
            context.Response.Write(strjson);
        }
        /// <summary>
        /// 
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

                jsonBuilder.Append(" },");


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
                DataRow[] dr = dtpart.Select(string.Format("OB02001 ='{0}' ", dtAll.Rows[i]["id"].ToString()));
                if (dr.Length > 0)  //判断当前角色是否有当前菜单权限
                {
                    strChecked = " ,\"checked\" : \"true\" ";
                }

                jsonBuilder.Append(" ,\"chkDisabled\" : \"false\" " + strChecked + " },");

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