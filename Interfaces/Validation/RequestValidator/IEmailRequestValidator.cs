using System.Collections.Specialized;
using System.Threading.Tasks;
using Models.Validation;

namespace Interfaces.Validation.RequestValidator
{
    public interface IEmailRequestValidator
    {
        /// <summary>
        /// 验证Email请求
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<ValidationRequestResult> ValidateRequestAsync(NameValueCollection parameter);
    }
}