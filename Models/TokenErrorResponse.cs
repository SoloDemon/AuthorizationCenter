using System.Collections.Generic;

namespace Models
{
    /// <summary>
    ///     token错误响应模型
    /// </summary>
    public class TokenErrorResponse : ErrorResponse
    {
        /// <summary>
        ///     获取或设置自定义条目。
        /// </summary>
        /// <value>
        ///     自定义条目
        /// </value>
        public Dictionary<string, object> Custom { get; set; } = new();
    }
}