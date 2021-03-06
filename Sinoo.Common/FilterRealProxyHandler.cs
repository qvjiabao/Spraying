﻿using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Reflection;

namespace Saas.Common
{
    /// <summary>
    ///  FilterRealProxy类：一个真实代理, 拦截它所代理对象中方法的返回值，并对需要过滤的返回值进行过滤。
    /// </summary>
    public class FilterRealProxyHandler : RealProxy
    {
        private MarshalByRefObject target;
        public FilterRealProxyHandler(MarshalByRefObject target)
            : base(target.GetType())
        {
            this.target = target;
        }
        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage callMsg = msg as IMethodCallMessage;
            IMethodReturnMessage returnMsg = RemotingServices.ExecuteMessage(target, callMsg);
            //检查返回值是否为String,如果不是String,就没必要进行过滤
            if (this.IsMatchType(returnMsg.ReturnValue))
            {
                string returnValue = this.Filter(returnMsg.ReturnValue.ToString(), returnMsg.MethodName);
                return new ReturnMessage(returnValue, null, 0, null, callMsg);
            }
            return returnMsg;
        }
        protected string Filter(string ReturnValue, string MethodName)
        {
            MethodInfo methodInfo = target.GetType().GetMethod(MethodName);
            object[] attributes = methodInfo.GetCustomAttributes(typeof(StringFilter), true);
            foreach (object attrib in attributes)
            {
                return FilterHandler.Process(((StringFilter)attrib).FilterType, ReturnValue);
            }
            return ReturnValue;
        }
        protected bool IsMatchType(object obj)
        {
            return obj is System.String;
        }
    }

    ///<summary>
    ///  StringFilter类：自定义属性类, 定义目标元素的过滤类型
    ///</summary>
    public class StringFilter : Attribute
    {
        protected FilterType _filterType;

        public StringFilter(FilterType filterType)
        {
            this._filterType = filterType;
        }
        public FilterType FilterType
        {
            get
            {
                return _filterType;
            }
        }
    }

    /// <summary>
    /// 枚举类：用于指定过滤类型，例如：对script过滤还是对html进行过滤？
    /// </summary>
    [Flags()]
    public enum FilterType
    {
        Script = 1,             //"Script脚本"
        Html = 2,              //"Html代码"
        Object = 3,           //"Object物件"
        AHrefScript = 4,   //"Iframe内框架"
        Iframe = 5,           //"Frameset框架"
        Frameset = 6,       //"Src插入性脚本"
        Src = 7,                //"非法字符"
        BadWords = 8,     //"Include包含文件"
        //Include=9,
        All = 16                //"以上所有"
    }

    ///<summary>
    /// 过滤处理类：根据过滤类型，调用相应的过滤处理方法。
    ///</summary>

    public class FilterHandler
    {
        private FilterHandler()
        {
        }
        public static string Process(FilterType filterType, string filterContent)
        {
            switch (filterType)
            {
                case FilterType.Script:
                    filterContent = FilterScript(filterContent);
                    break;
                case FilterType.Html:
                    filterContent = FilterHtml(filterContent);
                    break;
                case FilterType.Object:
                    filterContent = FilterObject(filterContent);
                    break;
                case FilterType.AHrefScript:
                    filterContent = FilterAHrefScript(filterContent);
                    break;
                case FilterType.Iframe:
                    filterContent = FilterIframe(filterContent);
                    break;
                case FilterType.Frameset:
                    filterContent = FilterFrameset(filterContent);
                    break;
                case FilterType.Src:
                    filterContent = FilterSrc(filterContent);
                    break;
                //case FilterType.Include:
                // filterContent=FilterInclude(filterContent);
                // break;
                case FilterType.BadWords:
                    filterContent = FilterBadWords(filterContent);
                    break;
                case FilterType.All:
                    filterContent = FilterAll(filterContent);
                    break;
                default:
                    //do nothing
                    break;
            }
            return filterContent;
        }

        public static string FilterScript(string content)
        {
            string commentPattern = @"(?'comment'<!--.*?--[ \n\r]*>)";
            string embeddedScriptComments = @"(\/\*.*?\*\/|\/\/.*?[\n\r])";
            string scriptPattern = String.Format(@"(?'script'<[ \n\r]*script[^>]*>(.*?{0}?)*<[ \n\r]*/script[^>]*>)", embeddedScriptComments);
            // 包含注释和Script语句
            string pattern = String.Format(@"(?s)({0}|{1})", commentPattern, scriptPattern);

            return StripScriptAttributesFromTags(Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase));
        }

        private static string StripScriptAttributesFromTags(string content)
        {
            string eventAttribs = @"on(blur|c(hange|lick)|dblclick|focus|keypress|(key|mouse)(down|up)|(un)?load
                    |mouse(move|o(ut|ver))|reset|s(elect|ubmit))";

            string pattern = String.Format(@"(?inx)
        \<(\w+)\s+
            (
                (?'attribute'
                (?'attributeName'{0})\s*=\s*
                (?'delim'['""]?)
                (?'attributeValue'[^'"">]+)
                (\3)
            )
            |
            (?'attribute'
                (?'attributeName'href)\s*=\s*
                (?'delim'['""]?)
                (?'attributeValue'javascript[^'"">]+)
                (\3)
            )
            |
            [^>]
        )*
    \>", eventAttribs);
            Regex re = new Regex(pattern);
            // 使用MatchEvaluator的委托
            return re.Replace(content, new MatchEvaluator(StripAttributesHandler));
        }

        private static string StripAttributesHandler(Match m)
        {
            if (m.Groups["attribute"].Success)
            {
                return m.Value.Replace(m.Groups["attribute"].Value, "");
            }
            else
            {
                return m.Value;
            }
        }

        public static string FilterAHrefScript(string content)
        {
            string newstr = FilterScript(content);
            string regexstr = @" href[ ^=]*= *[\s\S]*script *:";
            return Regex.Replace(newstr, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string FilterSrc(string content)
        {
            string newstr = FilterScript(content);
            string regexstr = @" src *= *['""]?[^\.]+\.(js|vbs|asp|aspx|php|jsp)['""]";
            return Regex.Replace(newstr, regexstr, @"", RegexOptions.IgnoreCase);
        }
        /*
          public static string FilterInclude(string content)
          {
           string newstr=FilterScript(content);
           string regexstr=@"<[\s\S]*include *(file|virtual) *= *[\s\S]*\.(js|vbs|asp|aspx|php|jsp)[^>]*>";
           return Regex.Replace(newstr,regexstr,string.Empty,RegexOptions.IgnoreCase);
          }
        */
        public static string FilterHtml(string content)
        {
            string newstr = FilterScript(content);
            string regexstr = @"<[^>]*>";
            return Regex.Replace(newstr, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string FilterObject(string content)
        {
            string regexstr = @"(?i)<Object([^>])*>(\w|\W)*</Object([^>])*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string FilterIframe(string content)
        {
            string regexstr = @"(?i)<Iframe([^>])*>(\w|\W)*</Iframe([^>])*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string FilterFrameset(string content)
        {
            string regexstr = @"(?i)<Frameset([^>])*>(\w|\W)*</Frameset([^>])*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        //移除非法或不友好字符
        private static string FilterBadWords(string chkStr)
        {
            //这里的非法和不友好字符由你任意加，用“|”分隔，支持正则表达式,由于本Blog禁止贴非法和不友好字符，所以这里无法加上。
            string BadWords = @"...";
            if (chkStr == "")
            {
                return "";
            }

            string[] bwords = BadWords.Split('#');
            int i, j;
            string str;
            StringBuilder sb = new StringBuilder();
            for (i = 0; i < bwords.Length; i++)
            {
                str = bwords[i].ToString().Trim();
                string regStr, toStr;
                regStr = str;
                Regex r = new Regex(regStr, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
                Match m = r.Match(chkStr);
                if (m.Success)
                {
                    j = m.Value.Length;
                    sb.Insert(0, "*", j);
                    toStr = sb.ToString();
                    chkStr = Regex.Replace(chkStr, regStr, toStr, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
                }
                sb.Remove(0, sb.Length);
            }
            return chkStr;
        }

        public static string FilterAll(string content)
        {
            content = FilterHtml(content);
            content = FilterScript(content);
            content = FilterAHrefScript(content);
            content = FilterObject(content);
            content = FilterIframe(content);
            content = FilterFrameset(content);
            content = FilterSrc(content);
            content = FilterBadWords(content);
            //content = FilterInclude(content);
            return content;
        }
    }
}