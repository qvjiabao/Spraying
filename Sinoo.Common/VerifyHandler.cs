using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Saas.Common
{
	/// <summary>
	/// ��֤����
	/// </summary>
	public class VerifyHandler {

		/// <summary>
		/// �ж��ļ����Ƿ�Ϊ���������ֱ����ʾ��ͼƬ�ļ���
		/// </summary>
		/// <param name="filename">�ļ���</param>
		/// <returns>�Ƿ����ֱ����ʾ</returns>
		public static bool IsImgFilename(string filename) {
			filename = filename.Trim();
			if (filename.EndsWith(".") || filename.IndexOf(".") == -1) {
				return false;
			}
			string extname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();

			return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
		}

		/// <summary>
		/// ����Ƿ����email��ʽ
		/// </summary>
		/// <param name="strEmail">Ҫ�жϵ�email�ַ���</param>
		/// <returns>�жϽ��</returns>
		public static bool IsValidEmail(string strEmail) {
			return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
		}

		public static bool IsValidDoEmail(string strEmail) {
			return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
		}

		/// <summary>
		/// ����Ƿ�����ȷ��Url
		/// </summary>
		/// <param name="strUrl">Ҫ��֤��Url</param>
		/// <returns>�жϽ��</returns>
		public static bool IsURL(string strUrl) {
			return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
		}

		/// <summary>
		/// �ж��Ƿ�Ϊbase64�ַ���
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool IsBase64String(string str) {
			//A-Z, a-z, 0-9, +, /, =
			return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
		}

		/// <summary>
		/// ����Ƿ���SqlΣ���ַ�
		/// </summary>
		/// <param name="str">Ҫ�ж��ַ���</param>
		/// <returns>�жϽ��</returns>
		public static bool IsSafeSqlString(string str) {

			return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
		}

		/// <summary>
		/// �Ƿ�Ϊip
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
		/// ��֤�Ƿ�Ϊ������
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool IsInt(string expression) {
			return Regex.IsMatch(expression, @"^[0-9]*$");
		}

		/// <summary>
		/// �ж϶����Ƿ�Ϊ Int32 ����
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
		/// �ж϶����Ƿ�Ϊ Double ����
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
		/// �ж��ַ����Ƿ���yyyy-mm-dd�ַ���
		/// </summary>
		public static bool IsDateString(string expression) {
			return Regex.IsMatch(expression, @"(\d{4})-(\d{1,2})-(\d{1,2})");
		}

		/// <summary>
		/// �Ƿ�ʱ���ַ���
		/// </summary>
		/// <returns></returns>
		public static bool IsTime(string expression) {
			return Regex.IsMatch(expression, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
		}

		/// <summary>
		/// �ж϶����Ƿ�Ϊ DateTime ����
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
		/// �ж϶����Ƿ�Ϊ Guid ����
		/// </summary>
		/// <param name="Expression"></param>
		/// <returns></returns>
		public static bool IsGUID(string expression) {
			if (expression == null || expression.Length < 32)
				return false;

			return guidreg.IsMatch(expression);
		}

		/// <summary>
		/// �жϸ������ַ�������(strNumber)�е������ǲ��Ƕ�Ϊ��ֵ��
		/// </summary>
		/// <param name="strNumber">Ҫȷ�ϵ��ַ�������</param>
		/// <returns>���򷵼�true �����򷵻� false</returns>
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
