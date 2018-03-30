using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Saas.Common
{
    /// <summary>
    /// 安全加密函数
    /// </summary>
    public class SafetyHandler
    {
        #region 对称加密算法类

          
        private SymmetricAlgorithm mobjCryptoService;
        private string Key;
        /// <summary>    
        /// 对称加密类的构造函数    
        /// </summary>    
        public SafetyHandler()
        {
            
            mobjCryptoService = new RijndaelManaged();
            Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";

            //Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH783#$%$#%FSSWESDsdfwewrfd%^#@$DSSF22#@!!@45612(3)sjlksdi2jiopfs$hujfdslkfdshu923802-043kjd^$%&%ipoumt7[-L;FJ08LK;RTWEQ#%#$jklfs#$3jkl543$#%#$4334FDSsdfew^#@fdf#$fs";
        }
        /// <summary>    
        /// 获得密钥    
        /// </summary>    
        /// <returns>密钥</returns>    
        private byte[] GetLegalKey()
        {
            string sTemp = Key;
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>    
        /// 获得初始向量IV    
        /// </summary>    
        /// <returns>初试向量IV</returns>    
        private byte[] GetLegalIV()
        {
            string sTemp = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
            //string sTemp = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk32jkl29I(IOJ()89%$$@jsfUHOJKL;#@#424^^#%fsdf@#@dfw@#@$jkljl%43#$2&&@$232^%$^%$lfs$#@AD12156df1235jklfs%$8704378954hjisdjk%$$%#$%$#%%$%$#kljfdsklfjdsi2u0903290fhbllkzjiofsd$#%$@#";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>    
        /// 加密方法    
        /// </summary>    
        /// <param name="Source">待加密的串</param>    
        /// <returns>经过加密的串</returns>    
        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public  string Encrypto(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }
        /// <summary>    
        /// 解密方法    
        /// </summary>    
        /// <param name="Source">待解密的串</param>    
        /// <returns>经过解密的串</returns>    
        public string Decrypto(string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }


        #endregion

        #region  DES加密

        //默认密钥向量
        //公用密钥（xml）
        private static byte[] PubDESKey = Encoding.UTF8.GetBytes("cf43qbhs");
        private static byte[] PubDESIV = Encoding.UTF8.GetBytes("bhscf43q");
        //私有密钥
        private static byte[] PriDESKey = Encoding.UTF8.GetBytes("fg435whs");
        private static byte[] PriDESIV = Encoding.UTF8.GetBytes("67suj753");
        private static byte[] PriMyKey = { 0x45, 0x34, 0x72, 0x34, 0x56, 0x1A, 0xCD, 0x90 };

        /// <summary>
        /// 公用加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string PubEncrypt(string encryptString)
        {
            return EncryptDES(EncryptDES(encryptString, PubDESKey, PubDESIV), PubDESKey, PubDESIV);
        }
        /// <summary>
        /// 公用解密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string PubDecrypt(string encryptString)
        {
            return DecryptDES(DecryptDES(encryptString, PubDESKey, PubDESIV), PubDESKey, PubDESIV);
        }

        /// <summary>
        /// 私有加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string PriEncrypt(string encryptString)
        {
            return EncryptDES(EncryptMy(EncryptDES(encryptString, PriDESKey, PriDESIV), PriMyKey), PriDESKey, PriDESIV);
        }
        /// <summary>
        /// 私有解密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string PriDecrypt(string encryptString)
        {
            return DecryptDES(DecryptMy(DecryptDES(encryptString, PriDESKey, PriDESIV), PriMyKey), PriDESKey, PriDESIV);
        }
        ///// <summary>
        ///// md5加密
        ///// </summary>
        ///// <param name="encryptString"></param>
        ///// <returns></returns>
        //public static string MD5(string encryptString)
        //{
        //    return FormsAuthentication.HashPasswordForStoringInConfigFile(encryptString, "MD5");
        //}

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="Key">密钥</param>
        /// <param name="IV">初始向量</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, byte[] Key, byte[] IV)
        {
            try
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, byte[] Key, byte[] IV)
        {
            try
            {
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, des.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        /// <summary>
        /// 我的加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string EncryptMy(string encryptString, byte[] Key)
        {
            try
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                for (int i = 0; i < inputByteArray.Length; i++)
                {
                    inputByteArray[i] = Convert.ToByte((inputByteArray[i] + Key[i % 8]) % 0xff);
                }
                return Convert.ToBase64String(inputByteArray.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }
        /// <summary>
        /// 我的解密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string DecryptMy(string encryptString, byte[] Key)
        {
            try
            {
                byte[] inputByteArray = Convert.FromBase64String(encryptString);
                for (int i = 0; i < inputByteArray.Length; i++)
                {
                    inputByteArray[i] = Convert.ToByte((inputByteArray[i] + 0xff - Key[i % 8]) % 0xff);
                }
                return Encoding.UTF8.GetString(inputByteArray.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        #endregion

        #region DES + Base64 加密

        /// <summary>
        /// DES + Base64 加密
        /// </summary>
        /// <param name="input">明文字符串</param>
        /// <returns>已加密字符串</returns>
        public static string DesBase64EncryptForID5(string input)
        {
            System.Security.Cryptography.DES des = System.Security.Cryptography.DES.Create();
            des.Mode = System.Security.Cryptography.CipherMode.CBC;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            byte[] Key = new byte[8] { 56, 50, 55, 56, 56, 55, 49, 49 };
            byte[] IV = new byte[8] { 56, 50, 55, 56, 56, 55, 49, 49 };

            ct = des.CreateEncryptor(Key, IV);

            byt = Encoding.GetEncoding("GB2312").GetBytes(input); //根据 GB2312 编码对字符串处理，转换成 byte 数组

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            byte[] answer = ms.ToArray();
            for (int j = 0; j < answer.Length; j++)
            {
                Console.Write(answer[j].ToString() + " ");
            }
            Console.WriteLine();
            return Convert.ToBase64String(ms.ToArray()); // 将加密的 byte 数组依照 Base64 编码转换成字符串
        }


        /// <summary>
        /// DES + Base64 解密
        /// </summary>
        /// <param name="input">密文字符串</param>
        /// <returns>解密字符串</returns>
        public static string DesBase64DecryptForID5(string input)
        {
            System.Security.Cryptography.DES des = System.Security.Cryptography.DES.Create();
            des.Mode = System.Security.Cryptography.CipherMode.CBC;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            byte[] Key = new byte[8] { 56, 50, 55, 56, 56, 55, 49, 49 };
            byte[] IV = new byte[8] { 56, 50, 55, 56, 56, 55, 49, 49 };

            ct = des.CreateDecryptor(Key, IV);
            byt = Convert.FromBase64String(input); // 将 密文 以 Base64 编码转换成 byte 数组

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Encoding.GetEncoding("GB2312").GetString(ms.ToArray()); // 将 明文 以 GB2312 编码转换成字符串
        }

        #endregion

        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static string GetMD5(string input)
        {

            MD5 md5 = MD5.Create();
            string result = "";
            byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(input));
            for (int i = 0; i < data.Length; i++)
            {
                result += data[i].ToString("x2");
            }
            return result;
        }
        #endregion
    }
}
