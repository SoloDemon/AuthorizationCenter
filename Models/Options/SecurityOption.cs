namespace Models.Options
{
    /// <summary>
    ///     秘钥选项
    /// </summary>
    public class SecurityOption
    {
        /// <summary>
        ///     AES加密解密key
        /// </summary>
        public string AesKey { get; set; }

        /// <summary>
        ///     rsa公钥
        /// </summary>
        public string RsaPublish { get; set; }

        /// <summary>
        ///     rsa私钥
        /// </summary>
        public string RsaPrivate { get; set; }
    }
}