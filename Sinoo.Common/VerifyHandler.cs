using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Saas.Common
{
	/// <summary>
	/// 验证函数
	/// </summary>
	public class VerifyHandler {

		/// <summary>
		/// 判断文件名是否为浏览器可以直接显示的图片文件名
		/// </summary>
		/// <param name="filename">文件名</param>
		/// <returns>是否可以直接显示</returns>
		public static bool IsImgFilename(string filename) {
			filename = filename.Trim();
			if (filename.EndsWith(".") || filename.IndexOf(".") == -1) {
				return false;
			}
			string extname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();

			return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
		}

		/// <summary>
		/// 检测是否符合email格式
		/// </summary>
		/// <param name="strEmail">要判断的email字符串</param>
		/// <returns>判断结果</returns>
		public static bool IsValidEmail(string strEmail) {
			return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
		}

		public static bool IsValidDoEmail(string strEmail) {
			return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
		}

		/// <summary>
		/// 检测是否是正确的Url
		/// </summary>
		/// <param name="strUrl">要验证的Url</param>
		/// <returns>判断结果</returns>
		public static bool IsURL(string strUrl) {
			return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
		}

		/// <summary>
		/// 判断是否为base64字符串
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool IsBase64String(string str) {
			//A-Z, a-z, 0-9, +, /, =
			return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
		}

		/// <summary>
		/// 检测是否有Sql危险字符
		/// </summary>
		/// <param name="str">要判断字符串</param>
		/// <returns>判断结果</returns>
		public static bool IsSafeSqlString(string str) {

			return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
		}

		/// <summary>
		/// 是否为ip
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static bool IsIP(string ip) {
			return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
		}

		public static bool IsIPSect(string ip) {
			return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");
		}

		/// <summary>
		/// 验证是否为正整数
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool IsInt(string expression) {
			return Regex.IsMatch(expression, @"^[0-9]*$");
		}

		/// <summary>
		/// 判断对象是否为 Int32 类型
		/// </summary>
		/// <param name="Expression"></param>
		/// <returns></returns>
		public static bool IsNumeric(object Expression) {
			if (Expression != null) {
				string str = Expression.ToString();
				if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$")) {
					if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1')) {
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// 判断对象是否为 Double 类型
		/// </summary>
		/// <param name="Expression"></param>
		/// <returns></returns>
		public static bool IsDouble(string expression) {
			if (expression != null) {
				return Regex.IsMatch(expression, @"^([0-9])[0-9]*(\.\w*)?$");
			}

			return false;
		}

		/// <summary>
		/// 判断字符串是否是yyyy-mm-dd字符串
		/// </summary>
		public static bool IsDateString(string expression) {
			return Regex.IsMatch(expression, @"(\d{4})-(\d{1,2})-(\d{1,2})");
		}

		/// <summary>
		/// 是否时间字符串
		/// </summary>
		/// <returns></returns>
		public static bool IsTime(string expression) {
			return Regex.IsMatch(expression, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
		}

		/// <summary>
		/// 判断对象是否为 DateTime 类型
		/// </summary>
		/// <param name="Expression"></param>
		/// <returns></returns>
		public static bool IsDateTime(string expression) {
			try {
				System.DateTime.Parse(expression);
			}
			catch {
				return false;
			}

			return true;
		}

		private static Regex guidreg = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}[\-\.]{0,1}[0-9a-fA-F]{4}[\-\.]{0,1}[0-9a-fA-F]{4}[\-\.]{0,1}[0-9a-fA-F]{4}[\-\.]{0,1}[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
		/// <summary>
		/// 判断对象是否为 Guid 类型
		/// </summary>
		/// <param name="Expression"></param>
		/// <returns></returns>
		public static bool IsGUID(string expression) {
			if (expression == null || expression.Length < 32)
				return false;

			return guidreg.IsMatch(expression);
		}

		/// <summary>
		/// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
		/// </summary>
		/// <param name="strNumber">要确认的字符串数组</param>
		/// <returns>是则返加true 不是则返回 false</returns>
		public static bool IsNumericArray(string[] strNumber) {
			if (strNumber == null || strNumber.Length < 1)
				return false;

			foreach (string id in strNumber) {
				if (!IsNumeric(id))
					return false;
			}

			return true;
		}

	}
}
