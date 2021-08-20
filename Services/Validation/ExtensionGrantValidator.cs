using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Validation;
using Models;
using Models.Validation;

namespace Services.Validation
{
    /// <summary>
    ///     使用自定义授权类型处理令牌请求的验证
    /// </summary>
    public class ExtensionGrantValidator
    {
        private readonly IEnumerable<IExtensionGrantValidator> _validators;

        public ExtensionGrantValidator(IEnumerable<IExtensionGrantValidator> validators)
        {
            _validators = validators ?? Enumerable.Empty<IExtensionGrantValidator>();
        }

        /// <summary>
        ///     验证
        /// </summary>
        /// <param name="request">验证token请求</param>
        /// <returns>授权验证结果</returns>
        public async Task<GrantValidationResult> ValidateAsync(ValidatedTokenRequest request)
        {
            var validator =
                _validators.FirstOrDefault(v => v.GrantType.Equals(request.GrantType, StringComparison.Ordinal));

            if (validator == null) return new GrantValidationResult(TokenRequestErrors.UnsupportedGrantType);

            try
            {
                var context = new ExtensionGrantValidationContext
                {
                    Request = request
                };

                await validator.ValidateAsync(context);
                return context.Result;
            }
            catch (Exception)
            {
                return new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            }
        }

        public IEnumerable<string> GetAvailableGrantTypes()
        {
            return _validators.Select(x => x.GrantType);
        }
    }
}