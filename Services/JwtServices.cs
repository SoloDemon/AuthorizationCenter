using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using FrameworkCore.Extensions.Store;
using Interfaces;
using Microsoft.Extensions.Options;
using Models;
using Models.Options;
using Models.Validation;

namespace Services
{
    public class JwtServices : IJwtServices
    {
        private readonly JwtOption _jwtOption;
        private readonly ISigningCredentialStore _signingCredentialStore;

        public JwtServices(
            IOptions<JwtOption> jwtOption,
            ISigningCredentialStore signingCredentialStore)
        {
            _signingCredentialStore = signingCredentialStore;
            _jwtOption = jwtOption.Value;
        }

        /// <summary>
        ///     获取Token
        /// </summary>
        /// <param name="tokenRequestValidationResult">请求token验证结果</param>
        /// <returns></returns>
        public async Task<TokenResponse> GetTokenAsync(TokenRequestValidationResult tokenRequestValidationResult)
        {
            var credentials = await _signingCredentialStore.GetSigningCredentialsAsync();
            var token = new JwtSecurityToken(
                _jwtOption.Issuer,
                _jwtOption.Audience,
                tokenRequestValidationResult.Claims,
                expires: DateTime.Now.AddMinutes(_jwtOption.Expires), //有效期
                signingCredentials: credentials);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(token);
            return new TokenResponse {AccessToken = tokenString, Custom = tokenRequestValidationResult.CustomResponse};
        }
    }
}