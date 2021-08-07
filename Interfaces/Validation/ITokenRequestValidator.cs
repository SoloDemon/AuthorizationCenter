using System.Collections.Specialized;
using System.Threading.Tasks;
using Models.Validation;

namespace Interfaces.Validation
{
    /// <summary>
    ///     请求token验证
    /// </summary>
    public interface ITokenRequestValidator
    {
        /// <summary>
        ///     验证请求
        /// </summary>
        /// <param name="parameters">请求参数</param>
        /// <returns></returns>
        Task<TokenRequestValidationResult> ValidateRequestAsync(NameValueCollection parameters);
    }
}