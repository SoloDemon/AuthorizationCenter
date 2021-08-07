namespace Models.Options
{
    /// <summary>
    ///     阿里云短信配置
    /// </summary>
    public class ALiYunSmsOption
    {
        /// <summary>
        ///     cn-hangzhou
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        ///     短信签名名称
        /// </summary>
        public string SignName { get; set; }

        /// <summary>
        ///     短信API产品域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        ///     accessKeyId
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        ///     accessKeySecret
        /// </summary>
        public string AccessKeySecret { get; set; }

        /// <summary>
        ///     Version
        /// </summary>
        public string Version { get; set; }
    }
}