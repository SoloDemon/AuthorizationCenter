using System.Collections.Specialized;

namespace Models.Validation
{
    /// <summary>
    ///     验证授权或令牌请求的基类
    /// </summary>
    public class ValidatedRequest
    {
        /// <summary>
        ///     获取或设置原始请求数据
        /// </summary>
        /// <value>
        ///     The raw.
        /// </value>
        public NameValueCollection Raw { get; set; }
    }
}