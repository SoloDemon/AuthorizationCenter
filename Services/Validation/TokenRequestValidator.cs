using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Threading.Tasks;
using Interfaces.Validation;
using Models.Validation;

namespace Services.Validation
{
    /// <summary>
    ///     请求token验证
    /// </summary>
    public class TokenRequestValidator : ITokenRequestValidator
    {
        private readonly ExtensionGrantValidator _extensionGrantValidator;
        private ValidatedTokenRequest _validatedRequest;

        public TokenRequestValidator(ExtensionGrantValidator extensionGrantValidator)
        {
            _extensionGrantValidator = extensionGrantValidator;
        }

        /// <summary>
        ///     验证请求
        /// </summary>
        /// <param name="parameters">请求参数</param>
        /// <returns></returns>
        public async Task<TokenRequestValidationResult> ValidateRequestAsync(NameValueCollection parameters)
        {
            _validatedRequest = new ValidatedTokenRequest
            {
                Raw = parameters ?? throw new ArgumentNullException(nameof(parameters)),
                GrantType = parameters["grantType"] ?? "Default"
            };
            //return _validatedRequest.GrantType switch
            //{
            //    GrantType.ResourceDefault => await RunValidationAsync(ValidateExtensionGrantRequestAsync, parameters),
            //    GrantType.ResourceWeChat => await RunValidationAsync(ValidateExtensionGrantRequestAsync, parameters),
            //    _ => await RunValidationAsync(ValidateExtensionGrantRequestAsync, parameters)
            //};
            return await RunValidationAsync(ValidateExtensionGrantRequestAsync, parameters);
        }

        /// <summary>
        ///     运行验证方法,可以在这里加入一些额外操作
        /// </summary>
        /// <param name="validationFunc"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static async Task<TokenRequestValidationResult> RunValidationAsync(
            Func<NameValueCollection, Task<TokenRequestValidationResult>> validationFunc,
            NameValueCollection parameters)
        {
            return await validationFunc(parameters);
        }

        /// <summary>
        ///     验证扩展授权请求
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task<TokenRequestValidationResult> ValidateExtensionGrantRequestAsync(
            NameValueCollection parameter)
        {
            var result = await _extensionGrantValidator.ValidateAsync(_validatedRequest);
            if (result == null)
                return Invalid("无效的授权");
            return result.IsError
                ? Invalid(result.Error, result.ErrorDescription)
                : Valid(result.Claims, result.CustomResponse);
        }

        /// <summary>
        ///     验证-添加自定义响应
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="customResponse"></param>
        /// <returns></returns>
        private TokenRequestValidationResult Valid(IEnumerable<Claim> claims,
            Dictionary<string, object> customResponse = null)
        {
            return new(_validatedRequest, claims, customResponse);
        }

        /// <summary>
        ///     无效的验证结果
        /// </summary>
        /// <param name="error">错误</param>
        /// <param name="errorDescription">错误描述</param>
        /// <param name="customResponse">自定义响应</param>
        /// <returns></returns>
        private TokenRequestValidationResult Invalid(string error, string errorDescription = null,
            Dictionary<string, object> customResponse = null)
        {
            return new(_validatedRequest, error, errorDescription, customResponse);
        }
    }
}