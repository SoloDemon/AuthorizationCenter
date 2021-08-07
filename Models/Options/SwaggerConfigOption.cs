namespace Models.Options
{
    /// <summary>
    ///     Swagger配置
    /// </summary>
    public class SwaggerConfigOption
    {
        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     作者名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     网站地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     程序集名称
        /// </summary>
        public string AssemblyName { get; set; }
    }
}