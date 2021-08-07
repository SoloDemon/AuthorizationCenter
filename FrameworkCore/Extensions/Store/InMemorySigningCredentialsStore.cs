using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace FrameworkCore.Extensions.Store
{
    /// <summary>
    ///     在内存中存储签名凭证
    /// </summary>
    public class InMemorySigningCredentialsStore : ISigningCredentialStore
    {
        private readonly SigningCredentials _credential;

        /// <summary>
        ///     初始化 <see cref="InMemorySigningCredentialsStore" /> 类的新实例.
        /// </summary>
        /// <param name="credential">The credential.</param>
        public InMemorySigningCredentialsStore(SigningCredentials credential)
        {
            _credential = credential;
        }

        /// <summary>
        ///     获取签名凭据.
        /// </summary>
        /// <returns></returns>
        public Task<SigningCredentials> GetSigningCredentialsAsync()
        {
            return Task.FromResult(_credential);
        }
    }
}