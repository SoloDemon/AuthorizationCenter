using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Interfaces.UserManager
{
    /// <summary>
    /// 用户管理接口
    /// </summary>
    public interface IUserManagerServices : IRegisterServices, IResetPasswordServices, IChangePasswordServices
    {
    }
}