using System.Collections.Specialized;
using System.Threading.Tasks;
using Models.Validation;

namespace Interfaces.Validation
{
    public interface IChangePasswordValidator
    {
        /// <summary>
        ///     修改密码
        /// </summary>
        /// <param name="parameters">请求参数列表</param>
        /// <returns></returns>
        Task<ValidationRequestResult> ValidateAsync(NameValueCollection parameters);
    }
}