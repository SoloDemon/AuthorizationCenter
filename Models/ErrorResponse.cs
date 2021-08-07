namespace Models
{
    public class ErrorResponse
    {
        /// <summary>
        ///     获取或设置错误响应
        /// </summary>
        /// <value>
        ///     错误信息
        /// </value>
        public string Error { get; set; } = "无效的请求";

        /// <summary>
        ///     获取或设置错误描述
        /// </summary>
        /// <value>
        ///     错误描述信息
        /// </value>
        public string ErrorDescription { get; set; }
    }
}