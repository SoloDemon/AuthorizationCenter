using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworkCore.Extensions.Client
{
    /// <summary>
    ///     授权客户端服务集合扩展
    /// </summary>
    public static class AuthorizationClientServiceCollectionExtensions
    {
        /// <summary>
        ///     添加授权客户端build
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IAuthorizationClientBuild AddAuthorizationClientBuild(this IServiceCollection services)
        {
            return new AuthorizationClientBuild(services);
        }

        /// <summary>
        ///     添加授权客户端
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IAuthorizationClientBuild AddAuthorizationClient(this IServiceCollection services)
        {
            var builder = AddAuthorizationClientBuild(services);
            //不加上以下内容,.netcore会自动映射一些claimtype为其他类型
            //这是因为获取声明的方式默认是走微软定义的一套映射方式，如果我们想要走JWT映射声明，那么我们需要将默认映射方式给移除掉
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            return builder;
        }
    }
}