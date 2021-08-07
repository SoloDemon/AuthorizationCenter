using System.Collections.Specialized;
using System.Threading.Tasks;
using Models.Validation;

namespace Interfaces.Validation
{
    public interface ISmsSendValidator
    {
        /// <summary>
        ///     验证请求
        /// </summary>
        /// <returns></returns>
        Task<SmsSendValidationResult> ValidateRequestAsync(NameValueCollection parameters);
    }
}