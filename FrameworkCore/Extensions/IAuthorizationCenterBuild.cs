using Microsoft.Extensions.DependencyInjection;

namespace FrameworkCore.Extensions
{
    /// <summary>
    ///     AuthorizationCenterBuild 接口
    /// </summary>
    public interface IAuthorizationCenterBuild
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