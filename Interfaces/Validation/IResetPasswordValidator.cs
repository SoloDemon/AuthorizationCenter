using System.Collections.Specialized;
using System.Threading.Tasks;
using Models.Validation;

namespace Interfaces.Validation
{
    public interface IResetPasswordValidator
    {
        /// <summary>
        /// 验证重置密码
        /// </summary>
        /// <param name="parameter">参数集合</param>
        /// <returns></returns>
        Task<ValidationRequestResult> ValidationResetPasswordAsync(NameValueCollection parameter);
    }
}