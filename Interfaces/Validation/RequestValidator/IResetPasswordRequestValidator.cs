using System.Collections.Specialized;
using System.Threading.Tasks;
using Models.Validation;

namespace Interfaces.Validation.RequestValidator
{
    public interface IResetPasswordRequestValidator
    {

        /// <summary>
        /// 验证请求
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <returns></returns>
        Task<ValidationRequestResult> ValidateRequestAsync(NameValueCollection parameters);
    }
}