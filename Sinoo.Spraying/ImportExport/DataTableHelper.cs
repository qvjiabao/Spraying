using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Sinoo.Spraying
{
    public class DataTableHelper
    {
        /// <summary>
        /// 循环datatable里面的数据
        /// </summary>
        /// <param name="dt">datatable</param>
        /// <returns></returns>
        public static string DataTableToString(DataTable dt, int showCount)
        {
            int j = 1;//计数
            int ii = 0;//循环每行的每列数据
            int jj = 0;//改变循环列的初始值
            string str = string.Empty;//返回的字符串
            int color = 0;//改变行的颜色
            str += "<div id='content-bd' style='margin:20px 10px 0px 0px;'>符合条件数为： " + dt.Rows.Count.ToString() + "条</div>";
            str += "<table width=\"50%\" class='table table-bordered table-striped table-hover zhong '><tbody><tr>";
            //循环列标题
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str += "<th width=\"10%\" scope=\"col\">" + dt.Columns[i] + " </th>";
            }
            str += "</tr>";
            if (dt != null && dt.Rows.Count > 0)
            {

                //循环每行的数据
                //foreach (DataRow dr in dt.Rows)
                //{
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    DataRow dr = dt.Rows[k];
                    if (k == showCount)
                    {
                        break;
                    }
                    if (color % 2 == 0)
                    {
                        str += "<tr align='center'>";
                    }
                    else
                    {
                        str += "<tr align='center'>";
                    }
                    //判断是否有序号列
                    for (int iii = 0; iii < dt.Columns.Count; iii++)
                    {
                        if (dt.Columns[iii].ToString().Contains("序号"))
                        {
                            str += "<td>" + j + "</td>";
                            jj = 1;
                        }
                    }
                    for (ii = jj; ii < dt.Columns.Count; ii++)
                    {
                        str += " <td>" + (dr[ii] == null || dr[ii].ToString() == "" ? "0" : dr[ii].ToString()) + "</td>";
                    }
                    str += " </tr>";
                    j++;
                    color = color + 1;
                }

            }
            else
            {
                str += "<tr><td>暂无数据显示</td></tr>";
            }
            str += "</tbody></table>";
            return str;
        }

        /// <summary>
        /// 获取串接字段的字符串
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="feildName">字段名</param>
        /// <returns>串接逗号分隔的字符串</returns>
        public static string GetPKFieldString(DataTable dt, string feildName)
        {
            StringBuilder strFeilds = new StringBuilder();
            if (!string.IsNullOrEmpty(feildName))
            {
                if (null != dt && dt.Columns.Contains(feildName))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strFeilds.AppendFormat(",'{0}'", dt.Rows[i][feildName].ToString());
                    }
                    if (strFeilds.Length>0)
                    {
                        strFeilds = strFeilds.Remove(0, 1);
                    }                    
                }
            }
            return strFeilds.ToString();
        }
    }
}
