using System.Collections.Specialized;
using System.Threading.Tasks;
using Models.Validation;

namespace Interfaces.Validation
{
    /// <summary>
    ///     注册验证接口
    /// </summary>
    public interface IRegisterValidator
    {
        /// <summary>
        ///     验证注册
        /// </summary>
        /// <param name="parameters">请求参数列表</param>
        /// <returns></returns>
        Task<RegisterValidationResult> ValidateRegisterAsync(NameValueCollection parameters);
    }
}