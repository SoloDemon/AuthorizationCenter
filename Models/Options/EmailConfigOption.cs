namespace Models.Options
{
    /// <summary>
    /// 电子邮件配置
    /// </summary>
    public class EmailConfigOption
    {
        /// <summary>
        /// Smtp服务器地址
        /// </summary>
        public string SmtpHost { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}