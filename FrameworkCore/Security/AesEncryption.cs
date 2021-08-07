using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Models.Options;

namespace FrameworkCore.Security
{
    public class AesEncryption
    {
        private readonly SecurityOption _securityOption;

        public AesEncryption(IOptionsMonitor<SecurityOption> securityOption)
        {
            _securityOption = securityOption.CurrentValue;
        }

        /// <summary>
        ///     Aes解密
        /// </summary>
        /// <param name="decryptContent">解密内容</param>
        /// <returns></returns>
        public string AesDecrypt(string decryptContent)
        {
            return AesDecrypt(decryptContent, _securityOption.AesKey);
        }

        /// <summary>
        ///     Aes解密
        /// </summary>
        /// <param name="decryptContent">解密内容</param>
        /// <returns></returns>
        public Task<string> AesDecryptAsync(string decryptContent)
        {
            return Task.FromResult(AesDecrypt(decryptContent, _securityOption.AesKey));
        }

        /// <summary>
        ///     Aes解密
        /// </summary>
        /// <param name="decryptContent">解密内容</param>
        /// <param name="key">加密Key</param>
        /// <returns></returns>
        public string AesDecrypt(string decryptContent, string key)
        {
            try
            {
                var fullCipher = Convert.FromBase64String(decryptContent);
                var iv = new byte[16];
                var cipher = new byte[fullCipher.Length - iv.Length];
                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
                var decryptKey = Encoding.UTF8.GetBytes(key);
                using var aesAlg = Aes.Create();
                using var decryption = aesAlg.CreateDecryptor(decryptKey, iv);
                using var msDecrypt = new MemoryStream(cipher);
                using var csDecrypt = new CryptoStream(msDecrypt,
                    decryption, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                var result = srDecrypt.ReadToEnd();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        ///     Aes解密
        /// </summary>
        /// <param name="decryptContent">解密内容</param>
        /// <param name="key">加密Key</param>
        /// <returns></returns>
        public Task<string> AesDecryptAsync(string decryptContent, string key)
        {
            return Task.FromResult(AesDecrypt(decryptContent, key));
        }

        /// <summary>
        ///     AES加密
        /// </summary>
        /// <param name="encryptContent">加密内容</param>
        /// <returns></returns>
        public string AesEncrypt(string encryptContent)
        {
            return AesEncrypt(encryptContent, _securityOption.AesKey);
        }

        /// <summary>
        ///     AES加密
        /// </summary>
        /// <param name="encryptContent">加密内容</param>
        /// <returns></returns>
        public Task<string> AesEncryptAsync(string encryptContent)
        {
            return Task.FromResult(AesEncrypt(encryptContent, _securityOption.AesKey));
        }

        /// <summary>
        ///     AES加密
        /// </summary>
        /// <param name="encryptContent">加密内容</param>
        /// <param name="key">加密Key</param>
        /// <returns></returns>
        public string AesEncrypt(string encryptContent, string key)
        {
            try
            {
                var encryptKey = Encoding.UTF8.GetBytes(key);
                using var aesAlg = Aes.Create();
                using var encryption = aesAlg.CreateEncryptor(encryptKey, aesAlg.IV);
                using var msEncrypt = new MemoryStream();
                using (var csEncrypt = new CryptoStream(msEncrypt, encryption,
                    CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(encryptContent);
                }

                var iv = aesAlg.IV;
                var decryptedContent = msEncrypt.ToArray();
                var result = new byte[iv.Length + decryptedContent.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(decryptedContent, 0, result,
                    iv.Length, decryptedContent.Length);
                return Convert.ToBase64String(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///     AES加密
        /// </summary>
        /// <param name="encryptContent">加密内容</param>
        /// <param name="key">加密Key</param>
        /// <returns></returns>
        public Task<string> AesEncryptAsync(string encryptContent, string key)
        {
            return Task.FromResult(AesEncrypt(encryptContent, key));
        }

        /// <summary>
        ///     生成AesKey
        /// </summary>
        /// <param name="keyBit">生成key位数</param>
        /// <returns></returns>
        public string GenerateAesKey(int keyBit)
        {
            using var aes = new AesCryptoServiceProvider {KeySize = keyBit};
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key, 0, aes.Key.Length);
        }

        /// <summary>
        ///     生成AesKey异步
        /// </summary>
        /// <param name="keyBit">生成key位数(最大位数256)</param>
        /// <returns></returns>
        public Task<string> GenerateAesKeyAsync(int keyBit)
        {
            return Task.FromResult(GenerateAesKey(keyBit));
        }
    }
}