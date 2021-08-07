using Microsoft.IdentityModel.Tokens;

namespace Models
{
    /// <summary>
    ///     关于安全密钥的信息
    /// </summary>
    public class SecurityKeyInfo
    {
        /// <summary>
        ///     key
        /// </summary>
        public SecurityKey Key { get; set; }

        /// <summary>
        ///     签名算法
        /// </summary>
        public string SigningAlgorithm { get; set; }
    }
}