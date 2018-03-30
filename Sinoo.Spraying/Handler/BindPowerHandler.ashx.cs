using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Sinoo.Spraying.Handler
{
    /// <summary>
    /// BindPowerHandler 的摘要说明
    /// </summary>
    public class BindPowerHandler : IHttpHandler
    {

        BLL.UserBLL us = new BLL.UserBLL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            DataTable dtAll = us.BindPowerByJosn();  //查询全部权限
            string strjson = string.Empty;

            if (context.Request.QueryString["Type"] == "View"
                && !string.IsNullOrEmpty(context.Request.QueryString["GA02001"])) //判断是否是查看
            {
                DataTable dtPart = new BLL.UserBLL().SelectSystemPowerBySystemRoleId(context.Request.QueryString["GA02001"]); //根绝角色id查询菜单
                strjson = ToViewJson(dtAll, dtPart);
            }
            else if (context.Request.QueryString["Type"] == "Edit"
               && !string.IsNullOrEmpty(context.Request.QueryString["GA02001"]))  //判断是否是编辑
            {
                DataTable dtPart = new BLL.UserBLL().SelectSystemPowerBySystemRoleId(context.Request.QueryString["GA02001"]); //根绝角色id查询菜单
                strjson = ToEditJson(dtAll, dtPart);
            }
            else
            {
                strjson = ToInitJson(dtAll);
            }
            context.Response.Write(strjson);
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
                DataRow[] dr = dtpart.Select(string.Format(" GC01002 ='{0}' ", dtAll.Rows[i]["id"].ToString()));
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