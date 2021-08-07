using Microsoft.Extensions.DependencyInjection;

namespace FrameworkCore.Extensions
{
    public static class AuthorizationCenterServiceCollectionExtensions
    {
        /// <summary>
        ///     创建授权中心build实例
        /// </summary>
        /// <param name="services">services</param>
        /// <returns></returns>
        private static IAuthorizationCenterBuild AddAuthorizationCenterBuild(this IServiceCollection services)
        {
            return new AuthorizationCenterBuild(services);
        }

        /// <summary>
        ///     添加授权中心
        /// </summary>
        /// <param name="services">services</param>
        /// <returns></returns>
        public static IAuthorizationCenterBuild AddAuthorizationCenter(this IServiceCollection services)
        {
            var builder = AddAuthorizationCenterBuild(services);
            //通过builder可以调用其他需要的方法,暂时没有
            return builder;
        }
    }
}