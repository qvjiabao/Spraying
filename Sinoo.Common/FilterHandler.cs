using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Sinoo.Common
{
    /// <summary>
    /// 过滤类公用（危险字符、js、sql）
    /// </summary>
    public abstract class FilterHandler
    {
        /// <summary>
        /// url查询参数过滤
        /// </summary>
        /// <param name="Param">传入参数</param>
        /// <returns>过滤后的参数</returns>
        public static string UrlFilter(string Param)
        {
            if (string.IsNullOrEmpty(Param)) return "";
            Param = Param.Replace("'", "＇");
            Param = Param.Replace("\"", " ");
            Param = Param.Replace("&", "&amp");
            Param = Param.Replace("<", "&lt");
            Param = Param.Replace(">", "&gt");
            Param = Param.Replace("--", "－－");
            Param = Param.Replace(";", "；");
            Param = Param.Replace("(", "（");
            Param = Param.Replace(")", "）");
            Param = Param.Replace("0x", "0 x");    //防止16进制注入

            Param = Param.Replace("Exec", "");
            Param = Param.Replace("Execute", "");

            Param = Param.Replace("xp_", "x p_");
            Param = Param.Replace("sp_", "s p_");
            return Param;
        }

        /// <summary>
        /// 过滤危险字符
        /// </summary>
        /// <param name="sql">需要过滤的sql语句</param>
        /// <returns>过滤后的sql语句</returns>
        public static string safety(string sql)
        {
            if (string.IsNullOrEmpty(sql)) return string.Empty;
            sql = sql.Trim(new char[] { ' ' });
            sql = sql.Replace("<", "＜");
            sql = sql.Replace(">", "＞");
            sql = sql.Replace("'", "＇");
            sql = sql.Replace("--", "－－");
            sql = sql.Replace(";", "；");
            //sql = sql.Replace("%", "");
            //sql = sql.Replace(" ", "");
            //sql = sql.Replace("*", "");
            return sql;
        }

        /// <summary>
        /// 去除HTML过滤
        /// </summary>
        /// <param name="strHtml">文本</param>
        /// <returns> 去除HTML的文本</returns>
        public static string StripHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = Regex.Replace(Htmlstring, @"\s+", " ");//多个空格替换成一个
            Htmlstring = Htmlstring.Trim();
            return Htmlstring;
        }


        /// <summary>
        /// 过滤查询中关键词中的特殊符号
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>过滤后的文本</returns>
        public static string safeSql(string sql)
        {
            if (string.IsNullOrEmpty(sql)) return string.Empty;
            sql = sql.Trim();
            sql = sql.Replace("<", " ");
            sql = sql.Replace(">", " ");
            sql = sql.Replace("%", " ");
            sql = sql.Replace("*", " ");
            sql = sql.Replace("'", " ");
            sql = sql.Replace("--", " ");
            sql = sql.Replace(";", " ");
            string str = "[]|［］（）〔〕【】〈〉「」『』﹙﹚﹛﹜﹝﹞、。，；:\"\"?";
            for (int i = 0; i < str.Length; i++)
            {
                sql = sql.Replace(str[i].ToString(), " ");
            }
            sql = sql.Replace(" ", "%");
            return sql;
        }

        /// <summary>
        /// 返回文本编辑器替换后的字符串
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <returns>返回文本编辑器替换后的字符串</returns>
        static public string GetHtmlEditReplace(string str)
        {
            #region
            return str.Replace("'", "''").Replace("&nbsp;", " ").Replace(",", "，").Replace("%", "％").Replace("script", "").Replace(".js", "");
            #endregion
        }
    }
}
