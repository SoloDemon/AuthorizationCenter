namespace Models
{
    /// <summary>
    ///     Token request errors
    /// </summary>
    public enum TokenRequestErrors
    {
        /// <summary>
        ///     无效请求
        /// </summary>
        InvalidRequest,

        /// <summary>
        ///     无效的授权
        /// </summary>
        InvalidGrant,

        /// <summary>
        ///     不支持的授权类型
        /// </summary>
        UnsupportedGrantType
    }
}