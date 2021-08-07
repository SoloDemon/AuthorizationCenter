using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Models.Options;

namespace FrameworkCore.Extensions.Client
{
    public static class AuthorizationClientBuildExtensions
    {
        public static IAuthorizationClientBuild AddAuthentication(this IAuthorizationClientBuild builder)
        {
            using var scope = builder.Services.BuildServiceProvider().CreateScope();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var jwtOption = configuration.GetSection("JwtOption").Get<JwtOption>();
            var securityOption = configuration.GetSection("Security").Get<SecurityOption>();
            var rsa = RSA.Create();
            rsa.ImportFromPem(securityOption.RsaPublish.AsSpan());
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, //是否验证Issuer
                        ValidateAudience = true, //是否验证Audience
                        ValidateLifetime = true, //是否验证失效时间
                        ValidateIssuerSigningKey = true, //是否验证SecurityKey
                        ValidAudience = jwtOption.Audience, //Audience
                        ValidIssuer = jwtOption.Issuer, //Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new RsaSecurityKey(rsa)
                        //IssuerSigningKeyValidator = (m, n, z) =>
                        // {
                        //     Console.WriteLine("This is IssuerValidator");
                        //     return true;
                        // },
                        //IssuerValidator = (m, n, z) =>
                        // {
                        //     Console.WriteLine("This is IssuerValidator");
                        //     return "http://localhost:5726";
                        // },
                        //AudienceValidator = (m, n, z) =>
                        //{
                        //    Console.WriteLine("This is AudienceValidator");
                        //    return true;
                        //    //return m != null && m.FirstOrDefault().Equals(this.Configuration["Audience"]);
                        //},//自定义校验规则，可以新登录后将之前的无效
                    };
                });
            return builder;
        }
    }
}