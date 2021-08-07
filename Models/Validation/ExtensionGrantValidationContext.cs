namespace Models.Validation
{
    /// <summary>
    ///     扩展授权验证上下文
    /// </summary>
    public class ExtensionGrantValidationContext : ValidatedTokenRequest
    {
        /// <summary>
        ///     Gets or sets the request.
        /// </summary>
        /// <value>
        ///     The request.
        /// </value>
        public ValidatedTokenRequest Request { get; set; }

        /// <summary>
        ///     Gets or sets the result.
        /// </summary>
        /// <value>
        ///     The result.
        /// </value>
        public GrantValidationResult Result { get; set; } = new(TokenRequestErrors.InvalidGrant);
    }
}