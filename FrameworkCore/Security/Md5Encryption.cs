using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCore.Security
{
    public class Md5Encryption
    {
        /// <summary>
        ///     16位md5加密
        /// </summary>
        /// <param name="encryptContent">加密内容</param>
        /// <returns></returns>
        public string Md5Encrypt16(string encryptContent)
        {
            var md5 = new MD5CryptoServiceProvider();
            var t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(encryptContent)), 4, 8);
            t2 = t2.Replace("-", string.Empty);
            return t2;
        }

        /// <summary>
        ///     16位md5加密
        /// </summary>
        /// <param name="encryptContent">加密内容</param>
        /// <returns></returns>
        public Task<string> Md5Encrypt16Async(string encryptContent)
        {
            return Task.FromResult(Md5Encrypt16(encryptContent));
        }

        /// <summary>
        ///     32位md5加密
        /// </summary>
        /// <param name="encryptContent">加密内容</param>
        /// <returns></returns>
        public string Md5Encrypt32(string encryptContent)
        {
            var pwd = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(encryptContent) && !string.IsNullOrWhiteSpace(encryptContent))
                {
                    var md5 = MD5.Create(); //实例化一个md5对像
                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                    var s = md5.ComputeHash(Encoding.UTF8.GetBytes(encryptContent));
                    // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                    pwd = s.Aggregate(pwd, (current, item) => string.Concat(current, item.ToString("X2")));
                }
            }
            catch
            {
                throw new Exception($"错误的 password 字符串:【{encryptContent}】");
            }

            return pwd;
        }

        /// <summary>
        ///     32位md5加密
        /// </summary>
        /// <param name="encryptContent">加密内容</param>
        /// <returns></returns>
        public Task<string> Md5Encrypt32Async(string encryptContent)
        {
            return Task.FromResult(Md5Encrypt32(encryptContent));
        }

        /// <summary>
        ///     64位md5加密
        /// </summary>
        /// <param name="encryptContent">加密内容</param>
        /// <returns></returns>
        public string Md5Encrypt64(string encryptContent)
        {
            // 实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            var md5 = MD5.Create();
            var s = md5.ComputeHash(Encoding.UTF8.GetBytes(encryptContent));
            return Convert.ToBase64String(s);
        }

        /// <summary>
        ///     64位md5加密
        /// </summary>
        /// <param name="encryptContent">加密内容</param>
        /// <returns></returns>
        public Task<string> Md5Encrypt64Async(string encryptContent)
        {
            return Task.FromResult(Md5Encrypt64(encryptContent));
        }
    }
}