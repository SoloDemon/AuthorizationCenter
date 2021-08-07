using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace FrameworkCore.Extensions.Store
{
    /// <summary>
    ///     用于验证密钥存储区接口
    /// </summary>
    public interface IValidationKeysStore
    {
        /// <summary>
        ///     获取所有的验证 keys.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SecurityKeyInfo>> GetValidationKeysAsync();
    }
}