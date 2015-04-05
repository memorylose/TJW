/* ======================================================================== 
* Author：Cass 
* Time：8/8/2014 3:23:15 PM 
* Description:  Encrypt
* ======================================================================== 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TJW.Utils
{
    public class MyEncrypt
    {
        #region Encrypt and decrypt

        /// <summary>
        /// 创建HASH密码 形式为： [跌代数：随机加密盐：HASH密码]
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string CreateHash(string password)
        {
            // 使用加密服务提供程序 (CSP) 提供的实现来实现加密随机数生成器 (RNG)。 
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[ConstValue.SALT_BYTE_SIZE];
            //加密盐
            csprng.GetBytes(salt);

            // 加密HASH
            byte[] hash = PBKDF2(password, salt, ConstValue.PBKDF2_ITERATIONS, ConstValue.HASH_BYTE_SIZE);

            //跌代数：随机加密盐：HASH密码
            return ConstValue.PBKDF2_ITERATIONS + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        /// <summary>
        /// 密码验证
        /// </summary>
        /// <param name="password">输入密码</param>
        /// <param name="correctHash">原始HASH密码</param>
        /// <returns>TRUE/FALSE</returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            // 用：来分解已有密码
            char[] delimiter = { ':' };
            string[] split = correctHash.Split(delimiter);

            //分解出跌代数
            int iterations = Int32.Parse(split[ConstValue.ITERATION_INDEX]);

            //分解出随机加密盐
            byte[] salt = Convert.FromBase64String(split[ConstValue.SALT_INDEX]);

            //分解出HASH密码
            byte[] hash = Convert.FromBase64String(split[ConstValue.PBKDF2_INDEX]);

            //加密需要验证的密码
            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);

            //比较
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// 慢比较
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        /// <summary>
        /// Md5
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            StringBuilder sBuilder = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="outputBytes"></param>
        /// <returns></returns>
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            //通过使用密码和 salt 值派生密钥，初始化 Rfc2898DeriveBytes 类的新实例
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);

            //设置操作的迭代数。
            pbkdf2.IterationCount = iterations;

            //生成密钥
            return pbkdf2.GetBytes(16);
        }
        #endregion

        #region SHA1

        public static string GetSHA1(string str)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();

            //将str转换成byte[]
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(str);

            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);

            //将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");

            return hash;
        }

        #endregion
    }
}
