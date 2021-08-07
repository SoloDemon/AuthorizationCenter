namespace Models
{
    /// <summary>
    ///     Rsa秘钥
    /// </summary>
    public class RsaSecretModel
    {
        /// <summary>
        ///     公钥
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        ///     私钥
        /// </summary>
        public string PrivateKey { get; set; }
    }
}