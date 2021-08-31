using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Interfaces.UserManager
{
    /// <summary>
    /// 注册接口
    /// </summary>
    public interface IRegisterServices
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEndpointResult> RegisterAsync(NameValueCollection parameters);
    }
}