using System;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworkCore.Extensions.Client
{
    /// <summary>
    ///     授权客户端
    /// </summary>
    public class AuthorizationClientBuild : IAuthorizationClientBuild
    {
        /// <summary>
        ///     构造注入
        /// </summary>
        /// <param name="services"></param>
        public AuthorizationClientBuild(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        ///     DI服务
        /// </summary>
        public IServiceCollection Services { get; }
    }
}