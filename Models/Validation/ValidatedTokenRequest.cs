namespace Models.Validation
{
    /// <summary>
    ///     Models a validated request to the token endpoint.
    /// </summary>
    public class ValidatedTokenRequest : ValidatedRequest
    {
        /// <summary>
        ///     Gets or sets the type of the grant.
        /// </summary>
        /// <value>
        ///     The type of the grant.
        /// </value>
        public string GrantType { get; set; }

        /// <summary>
        ///     获取或设置请求中使用的用户名
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        public string UserName { get; set; }
    }
}