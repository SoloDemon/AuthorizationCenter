using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace FrameworkCore.Extensions.Store
{
    /// <summary>
    ///     默认验证密钥存储区
    /// </summary>
    /// <seealso cref="IValidationKeysStore" />
    public class InMemoryValidationKeysStore : IValidationKeysStore
    {
        private readonly IEnumerable<SecurityKeyInfo> _keys;

        /// <summary>
        ///     默认初始化一个 <see cref="InMemoryValidationKeysStore" /> 类的新实例.
        /// </summary>
        /// <param name="keys">keys.</param>
        /// <exception cref="System.ArgumentNullException">keys</exception>
        public InMemoryValidationKeysStore(IEnumerable<SecurityKeyInfo> keys)
        {
            _keys = keys ?? throw new ArgumentNullException(nameof(keys));
        }

        /// <summary>
        ///     获取所有验证 keys.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<SecurityKeyInfo>> GetValidationKeysAsync()
        {
            return Task.FromResult(_keys);
        }
    }
}