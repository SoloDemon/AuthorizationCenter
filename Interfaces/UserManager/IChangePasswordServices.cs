using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Interfaces.UserManager
{
    public interface IChangePasswordServices
    {
        /// <summary>
        ///     修改密码
        /// </summary>
        /// <param name="parameter">参数集合</param>
        /// <returns></returns>
        public Task<IEndpointResult> ChangePassword(NameValueCollection parameter);
    }
}