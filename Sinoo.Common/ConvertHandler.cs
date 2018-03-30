using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Common
{
    /// <summary>
    /// 转换类
    /// </summary>
    public abstract class ConvertHandler
    {
        /// <summary>
        /// 数字转换成中文大写数字（例如：零、三、五）
        /// </summary>
        /// <param name="Serial">阿拉伯数字</param>
        /// <returns>中文大写数字</returns>
        public static string ConvertToChinese(int Serial)
        {
            string str = string.Empty;
            switch (Serial)
            {
                case 0: str = "零"; break;
                case 1: str = "一"; break;
                case 2: str = "二"; break;
                case 3: str = "三"; break;
                case 4: str = "四"; break;
                case 5: str = "五"; break;
                case 6: str = "六"; break;
                case 7: str = "七"; break;
                case 8: str = "八"; break;
                case 9: str = "九"; break;
                default: str = ""; break;
            }
            return str;
        }

        /// <summary>
        /// 将StringBuilder数组转化为string泛型集合
        /// </summary>
        /// <param name="sbs"></param>
        /// <returns>string集合</returns>
        public static List<string> GetListString(StringBuilder[] sbs)
        {
            List<string> listString = new List<string>();
            foreach (StringBuilder sb in sbs)
            {
                listString.Add(sb.ToString());
            }
            return listString;
        }

        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="BJstr"></param>
        /// <returns></returns>
        static public string GetQuanJiao(string BJstr)
        {
            #region
            char[] c = BJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 0)
                    {
                        b[0] = (byte)(b[0] - 32);
                        b[1] = 255;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }

            string strNew = new string(c);
            return strNew;

            #endregion
        }

        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="QJstr"></param>
        /// <returns></returns>
        static public string GetBanJiao(string QJstr)
        {
            #region
            char[] c = QJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            string strNew = new string(c);
            return strNew;
            #endregion
        }
    }
}
