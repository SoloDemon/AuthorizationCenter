using System.Collections.Specialized;
using System.Threading.Tasks;
using Models;

namespace Interfaces.UserManager
{
    public interface IResetPasswordServices
    {
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="parameter">参数集合</param>
        /// <returns></returns>
        Task<ApiResultMessage> ResetPasswordAsync(NameValueCollection parameter);
    }
}