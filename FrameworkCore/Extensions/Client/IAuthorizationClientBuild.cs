using Microsoft.Extensions.DependencyInjection;

namespace FrameworkCore.Extensions.Client
{
    /// <summary>
    ///     授权客户端接口
    /// </summary>
    public interface IAuthorizationClientBuild
    {
        /// <summary>
        ///     获取 services.
        /// </summary>
        /// <value>
        ///     The services.
        /// </value>
        public IServiceCollection Services { get; }
    }
}