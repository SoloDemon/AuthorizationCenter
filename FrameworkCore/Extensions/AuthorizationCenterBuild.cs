using System;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworkCore.Extensions
{
    /// <summary>
    ///     DI配置的AuthorizationCenter助手类
    /// </summary>
    public class AuthorizationCenterBuild : IAuthorizationCenterBuild
    {
        public AuthorizationCenterBuild(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        ///     获取 services.
        /// </summary>
        /// <value>
        ///     The services.
        /// </value>
        public IServiceCollection Services { get; }
    }
}