using System.Collections.Specialized;
using System.Threading.Tasks;
using Models.Validation;

namespace Interfaces.Validation.RequestValidator
{
    public interface IChangePasswordRequestValidator
    {
        /// <summary>
        ///     验证请求
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<ValidationResultBase> ValidateRequestAsync(NameValueCollection parameters);
    }
}