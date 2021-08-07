using FrameworkCore.Extensions;
using Interfaces;
using Interfaces.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Options;
using Services;
using Services.Validation;

namespace AuthorizationCenter.Extensions
{
    /// <summary>
    ///     用于注册其他服务的生成器扩展方法
    /// </summary>
    public static class IdentityServerBuilderExtensionsAdditional
    {
        /// <summary>
        ///     添加缓存
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IAuthorizationCenterBuild AddCachingContainer(this IAuthorizationCenterBuild builder,
            IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettingsOption>();
            if (appSettings.MemoryCaching)
            {
                //使用内存缓存
                builder.Services.AddMemoryCache();
                builder.Services.AddTransient<ICachingServices, MemoryCaching>();
            }
            //redis缓存
            else if (appSettings.RedisCaching)
            {
                builder.Services.AddTransient<ICachingServices, RedisCaching>();
            }

            return builder;
        }

        public static IAuthorizationCenterBuild AddExtensionGrantValidator<T>(this IAuthorizationCenterBuild builder)
            where T : class, IExtensionGrantValidator
        {
            builder.Services.AddTransient<IExtensionGrantValidator, T>();
            return builder;
        }

        public static IAuthorizationCenterBuild UseExtensionGrantValidator(this IAuthorizationCenterBuild builder)
        {
            builder.Services.AddTransient<ExtensionGrantValidator>();
            builder.Services.AddTransient<ITokenRequestValidator, TokenRequestValidator>();
            builder.Services.AddTransient<IRegisterRequestValidator, RegisterRequestValidator>();
            builder.Services.AddTransient<IRegisterValidator, RegisterValidator>();
            builder.Services.AddTransient<ISmsSendValidator, SmsSendValidator>();
            return builder;
        }
    }
}