using System.Collections.Specialized;
using System.Threading.Tasks;
using Models.Validation;

namespace Interfaces.Validation.RequestValidator
{
    /// <summary>
    ///     注册请求验证接口
    /// </summary>
    public interface IRegisterRequestValidator
    {
        /// <summary>
        ///     验证请求
        /// </summary>
        /// <returns></returns>
        Task<RegisterRequestValidationResult> ValidateRequestAsync(NameValueCollection parameters);
    }
}