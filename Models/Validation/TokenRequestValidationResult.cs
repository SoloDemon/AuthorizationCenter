using System.Collections.Generic;
using System.Security.Claims;

namespace Models.Validation
{
    /// <summary>
    ///     Validation result for token requests
    /// </summary>
    public class TokenRequestValidationResult : ValidationRequestResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TokenRequestValidationResult" /> class.
        /// </summary>
        /// <param name="validatedRequest">The validated request.</param>
        /// <param name="claims">获取声明列表</param>
        /// <param name="customResponse">The custom response.</param>
        public TokenRequestValidationResult(ValidatedTokenRequest validatedRequest, IEnumerable<Claim> claims,
            Dictionary<string, object> customResponse = null)
        {
            IsError = false;
            Claims = claims;
            ValidatedRequest = validatedRequest;
            CustomResponse = customResponse;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TokenRequestValidationResult" /> class.
        /// </summary>
        /// <param name="validatedRequest">The validated request.</param>
        /// <param name="error">The error.</param>
        /// <param name="errorDescription">The error description.</param>
        /// <param name="customResponse">The custom response.</param>
        public TokenRequestValidationResult(ValidatedTokenRequest validatedRequest, string error,
            string errorDescription = null, Dictionary<string, object> customResponse = null)
        {
            IsError = true;

            Error = error;
            ErrorDescription = errorDescription;
            ValidatedRequest = validatedRequest;
            CustomResponse = customResponse;
        }

        /// <summary>
        ///     Gets the validated request.
        /// </summary>
        /// <value>
        ///     The validated request.
        /// </value>
        public ValidatedTokenRequest ValidatedRequest { get; }

        /// <summary>
        ///     获取声明列表
        /// </summary>
        public IEnumerable<Claim> Claims { get; }

        /// <summary>
        ///     Gets or sets the custom response.
        /// </summary>
        /// <value>
        ///     The custom response.
        /// </value>
        public Dictionary<string, object> CustomResponse { get; set; }
    }
}