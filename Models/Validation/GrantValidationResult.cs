using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityModel;

namespace Models.Validation
{
    /// <summary>
    ///     身份验证结果
    /// </summary>
    public class GrantValidationResult : ValidationResultBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GrantValidationResult" /> class with an error and description.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="errorDescription">The error description.</param>
        public GrantValidationResult(TokenRequestErrors error, string errorDescription = null)
        {
            Error = ConvertTokenErrorEnumToString(error);
            ErrorDescription = errorDescription;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GrantValidationResult" /> class with an error and description.
        /// </summary>
        public GrantValidationResult()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GrantValidationResult" /> class.
        /// </summary>
        /// <param name="user">User is the information of the logged in user</param>
        /// <param name="claims">Additional claims that will be maintained in the principal.</param>
        /// <param name="authenticationMethod">The authentication method which describes the custom grant type.</param>
        /// <param name="role">The role claim used to role identifier the user.</param>
        public GrantValidationResult(string authenticationMethod,
            ApplicationUser user,
            IEnumerable<Claim> claims,
            IEnumerable<string> role)
        {
            IsError = false;
            var resultClaims = new List<Claim>
            {
                new(JwtClaimTypes.Subject, user.Id.ToString()),
                new(JwtClaimTypes.NickName, user.NickName),
                new(JwtClaimTypes.Email, user.Email ?? ""),
                new(JwtClaimTypes.EmailVerified, user.EmailConfirmed.ToString(), ClaimValueTypes.Boolean),
                new(JwtClaimTypes.PhoneNumber, user.PhoneNumber ?? ""),
                new(JwtClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed.ToString(), ClaimValueTypes.Boolean)
            };
            var enumerable = role.ToList();
            if (enumerable.Any()) resultClaims.AddRange(enumerable.Select(item => new Claim(JwtClaimTypes.Role, item)));
            resultClaims.AddRange(claims);
            Claims = resultClaims;
        }

        /// <summary>
        ///     声明
        /// </summary>
        public IEnumerable<Claim> Claims { get; set; }

        /// <summary>
        ///     自定义响应
        /// </summary>
        public Dictionary<string, object> CustomResponse { get; set; }


        /// <summary>
        ///     enum转字符串
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        private static string ConvertTokenErrorEnumToString(TokenRequestErrors error)
        {
            return error switch
            {
                TokenRequestErrors.InvalidGrant => "无效的授权",
                TokenRequestErrors.InvalidRequest => "无效的请求",
                TokenRequestErrors.UnsupportedGrantType => "不支持的授权类型",
                _ => throw new InvalidOperationException("无效的Token")
            };
        }
    }
}