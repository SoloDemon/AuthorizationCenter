using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace FrameworkCore.Extensions.Store
{
    /// <summary>
    ///     用于签名凭据存储的接口
    /// </summary>
    public interface ISigningCredentialStore
    {
        /// <summary>
        ///     获取签名凭据
        /// </summary>
        /// <returns></returns>
        Task<SigningCredentials> GetSigningCredentialsAsync();
    }
}