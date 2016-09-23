using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Inventec.Common
{
    /// <summary>
    /// <para> 加密类型</para>
    /// </summary>
    public enum EncryptionAlgorithm
    {
        /// <summary>
        /// <c> 1</c>
        /// </summary>
        Des = 1,

        /// <summary>
        /// <para>2</para>
        /// </summary>
        Rc2,

        /// <summary>
        /// <para>3</para>
        /// </summary>
        Rijndael,

        /// <summary>
        /// <para>4</para>
        /// </summary>
        TripleDes
    }

    public class Encryption
    {

        private static string _QueryStringKey = "admin!@#";
        private static string _UserPassword = "Admin!@#";
        /// <summary>
        /// <para> 以 MD5 加密字符串</para> 
        /// </summary>
        /// <param name="original"><para> 原文</para></param>
        /// <returns>密文</returns>
        public static string MD5Encrypt(string original)
        {
            // 原文的 bytes型
            byte[] bytesOriginal = Encoding.ASCII.GetBytes(original);

            MD5 md5 = new MD5CryptoServiceProvider();

            return Convert.ToBase64String(md5.ComputeHash(bytesOriginal));
        }

        /// <summary>
        /// <para> 以 SHA1 加密字符串</para>
        /// </summary>
        /// <param name="original"><para> 原文</para></param>
        /// <returns>密文</returns>
        public static string SHA1Encrypt(string original)
        {
            // 原文的 bytes型
            byte[] bytesOriginal = Encoding.ASCII.GetBytes(original);

            SHA1 sha1 = new SHA1Managed();

            return Convert.ToBase64String(sha1.ComputeHash(bytesOriginal));
        }

        /// <summary>
        /// <para> 以 SHA256 加密字符串</para>
        /// </summary>
        /// <param name="original"><para> 原文</para></param>
        /// <returns>密文</returns>
        public static string SHA256Encrypt(string original)
        {
            // 原文的 bytes型
            byte[] bytesOriginal = Encoding.ASCII.GetBytes(original);

            SHA256 sha256 = new SHA256Managed();

            return Convert.ToBase64String(sha256.ComputeHash(bytesOriginal));
        }

        /// <summary>
        /// <para> 以 SHA512 加密字符串</para>
        /// </summary>
        /// <param name="original"><para> 原文</para></param>
        /// <returns>密文</returns>
        public static string SHA512Encrypt(string original)
        {
            // 原文的 bytes型
            byte[] bytesOriginal = Encoding.ASCII.GetBytes(original);

            SHA512 sha512 = new SHA512Managed();

            return Convert.ToBase64String(sha512.ComputeHash(bytesOriginal));
        }

        /// <summary>
        /// <para> 获取加密解密支持对象</para>
        /// </summary>
        /// <param name="IsEncrypt">是否是加密</param>
        /// <param name="algId">类型</param>
        /// <param name="initVec">初始化向量</param>
        /// <param name="key">密钥</param>
        /// <returns>ICryptoTransform</returns>
        /// //时间就是生命，
        protected static ICryptoTransform GetCryptoServiceProvider(bool IsEncrypt, EncryptionAlgorithm algId, byte[] initVec, byte[] key)
        {
            // 选取提供程序。
            switch (algId)
            {
                case EncryptionAlgorithm.Des:
                    {
                        DES des = new DESCryptoServiceProvider();
                        des.Mode = CipherMode.CBC;
                        if (IsEncrypt)
                            return des.CreateEncryptor(key, initVec);
                        else
                            return des.CreateDecryptor(key, initVec);
                    }
                case EncryptionAlgorithm.TripleDes:
                    {
                        TripleDES des3 = new TripleDESCryptoServiceProvider();
                        des3.Mode = CipherMode.CBC;
                        if (IsEncrypt)
                            return des3.CreateEncryptor(key, initVec);
                        else
                            return des3.CreateDecryptor(key, initVec);
                    }
                case EncryptionAlgorithm.Rc2:
                    {
                        RC2 rc2 = new RC2CryptoServiceProvider();
                        rc2.Mode = CipherMode.CBC;
                        if (IsEncrypt)
                            return rc2.CreateEncryptor(key, initVec);
                        else
                            return rc2.CreateDecryptor(key, initVec);
                    }
                case EncryptionAlgorithm.Rijndael:
                    {
                        Rijndael rijndael = new RijndaelManaged();
                        rijndael.Mode = CipherMode.CBC;
                        if (IsEncrypt)
                            return rijndael.CreateEncryptor(key, initVec);
                        else
                            return rijndael.CreateDecryptor(key, initVec);
                    }
                default:
                    {
                        throw new CryptographicException("算法 ID '" + algId + "' 不支持。");
                    }
            }
        } //end GetCryptoServiceProvider


        /// <summary>
        /// <para> 加密字符串</para>
        /// </summary>
        /// <param name="algId"><para>加密类型</para></param>
        /// <param name="initVec">初始化向量</param>
        /// <param name="key">密钥</param>
        /// <param name="original">原文</param>
        /// <returns>密文</returns>
        public static string Encrypt(EncryptionAlgorithm algId, string initVec, string key, string original)
        {
            //验证初始化向量和密钥
            switch (algId)
            {
                case EncryptionAlgorithm.Rc2:
                    {
                        if (initVec.Length != 8 || key.Length != 8) throw new CryptographicException("RC2算法需要8位的初始化向量和密钥");
                        break;
                    }
                case EncryptionAlgorithm.Des:
                    {
                        if (initVec.Length != 16 || key.Length != 16) throw new CryptographicException("DES算法需要16位的初始化向量和密钥");
                        break;
                    }
                case EncryptionAlgorithm.TripleDes:
                    {
                        if (initVec.Length != 24 || key.Length != 24) throw new CryptographicException("TripleDes算法需要24位的初始化向量和密钥");
                        break;
                    }
                case EncryptionAlgorithm.Rijndael:
                    {
                        if (initVec.Length != 32 || key.Length != 32) throw new CryptographicException("Rijndael算法需要32位的初始化向量和密钥");
                        break;
                    }
                default:
                    {
                        throw new CryptographicException(string.Format("算法 ID '{0}' 不支持。", algId));
                    }
            }

            //取得初始化向量、密钥和原文的 bytes型
            byte[] bytesVec = Encoding.ASCII.GetBytes(initVec);

            byte[] bytesKey = Encoding.ASCII.GetBytes(key);
            byte[] bytesOriginal = Encoding.ASCII.GetBytes(original);
            //那天晚上真的很难受，没人陪我！孤苦伶仃，街头飘零，我要活出人样出来

            //设置将保存加密数据的流。
            MemoryStream memStreamEncryptedData = new MemoryStream();

            ICryptoTransform transform = GetCryptoServiceProvider(true, algId, bytesVec, bytesKey);
            CryptoStream encStream = new CryptoStream(memStreamEncryptedData, transform, CryptoStreamMode.Write);

            try
            {
                //加密数据，并将它们写入内存流。
                encStream.Write(bytesOriginal, 0, bytesOriginal.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("将加密数据写入流时出错： {0}", ex.Message));
            }

            encStream.FlushFinalBlock();

            //发送回数据。
            //return Encoding.ASCII.GetString( memStreamEncryptedData.ToArray() );
            byte[] srcBytes = memStreamEncryptedData.ToArray();

            // Convert the binary input into Base64 UUEncoded output.
            // Each 3 byte sequence in the source data becomes a 4 byte
            // sequence in the character array. 
            long arrayLength = (long)((4.0d / 3.0d) * srcBytes.Length);

            // If array length is not divisible by 4, go up to the next
            // multiple of 4.
            if (arrayLength % 4 != 0)
            {
                arrayLength += 4 - arrayLength % 4;
            }

            char[] base64CharArray = new char[arrayLength];
            Convert.ToBase64CharArray(srcBytes, 0, srcBytes.Length, base64CharArray, 0);
            return new string(base64CharArray);
        }//end Encrypt

        /// <summary>
        /// <para> 解密字符串</para>
        /// </summary>
        /// <param name="algId">解密类型</param>
        /// <param name="initVec">初始化向量</param>
        /// <param name="key">密钥</param>
        /// <param name="cryptograph">密文</param>
        /// <returns>原文</returns>

        public static string Decrypt(EncryptionAlgorithm algId, string initVec, string key, string cryptograph)
        {
            //验证初始化向量和密钥
            switch (algId)
            {
                case EncryptionAlgorithm.Des:
                    {
                        if (initVec.Length != 16 || key.Length != 16) throw new CryptographicException("DES算法需要16位的初始化向量和密钥");
                        break;
                    }
                case EncryptionAlgorithm.Rc2:
                    {
                        if (initVec.Length != 8 || key.Length != 8) throw new CryptographicException("RC2算法需要8位的初始化向量和密钥");
                        break;
                    }
                case EncryptionAlgorithm.Rijndael:
                    {
                        if (initVec.Length != 32 || key.Length != 32) throw new CryptographicException("Rijndael算法需要32位的初始化向量和密钥");
                        break;
                    }
                case EncryptionAlgorithm.TripleDes:
                    {
                        if (initVec.Length != 24 || key.Length != 24) throw new CryptographicException("TripleDes算法需要24位的初始化向量和密钥");
                        break;
                    }
                default:
                    {
                        throw new CryptographicException(string.Format("算法 ID '{0}' 不支持。", algId));
                    }
            }

            //取得初始化向量、密钥和原文的 bytes型
            byte[] bytesVec = Encoding.ASCII.GetBytes(initVec);

            byte[] bytesKey = Encoding.ASCII.GetBytes(key);
            byte[] bytesCryptograph = Convert.FromBase64CharArray(cryptograph.ToCharArray(), 0, cryptograph.Length);

            //为解密数据设置内存流。
            MemoryStream memStreamDecryptedData = new MemoryStream(bytesCryptograph);

            //传递初始化向量。
            ICryptoTransform transform = GetCryptoServiceProvider(false, algId, bytesVec, bytesKey);
            CryptoStream decStream = new CryptoStream(memStreamDecryptedData, transform, CryptoStreamMode.Read);

            byte[] fromEncrypt = new byte[bytesCryptograph.Length];

            try
            {
                decStream.Read(fromEncrypt, 0, fromEncrypt.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("将加密数据写入流时出错： {0}", ex.Message));
            }

            // 发送回数据。
            return Encoding.ASCII.GetString(fromEncrypt).Replace("\0", "");
        } //end Decrypt


        public static string EncryptQueryString(string QueryString)
        {
            return Encrypt(QueryString, _QueryStringKey);
        }


        /// <summary>
        /// 解密URL传输的字符串
        /// </summary>
        /// <param name="QueryString"></param>
        /// <returns></returns>
        public static string DecryptQueryString(string QueryString)
        {
            return Decrypt(QueryString, _QueryStringKey);
        }

        public static string EncryptPassword(string PasswordString)
        {
            return Encrypt(PasswordString, _UserPassword);
        }

        public static string DecryptPassword(string PasswordString)
        {
            return Decrypt(PasswordString, _UserPassword);
        }

        #region 加密过程
        /// <summary>
        /// DEC 加密过程
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            if (String.IsNullOrEmpty(pToEncrypt))
            {
                return String.Empty;
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();　////把字符串放到byte数组中

            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);　////建立加密对象的密钥和偏移量
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);　 //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            MemoryStream ms = new MemoryStream();　　 //使得输入密码必须输入英文文本
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        #endregion

        #region 解密过程
        /// <summary>
        ///  DEC 解密过程
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            if (String.IsNullOrEmpty(pToDecrypt))
            {
                return String.Empty;
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);　////建立加密对象的密钥和偏移量，此值重要，不能修改
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();　////建立StringBuilder对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        #endregion

    }
}
