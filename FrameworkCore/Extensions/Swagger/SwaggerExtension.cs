using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Models.Options;

namespace FrameworkCore.Extensions.Swagger
{
    /// <summary>
    ///     Swagger扩展
    /// </summary>
    public static class SwaggerExtension
    {
        public static void AddSwaggerForJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerConfigOption = configuration.GetSection("Swagger").Get<SwaggerConfigOption>();
            services.AddSwaggerGen(options =>
            {
                //获取xml文档路径
                var xmlFile = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath ?? string.Empty,
                    $"{Assembly.Load(swaggerConfigOption.AssemblyName).GetName().Name}.xml");
                //配置swagger文档
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = swaggerConfigOption.Title,
                        Version = swaggerConfigOption.Version,
                        Description = swaggerConfigOption.Description,
                        Contact = new OpenApiContact
                        {
                            Email = swaggerConfigOption.Email,
                            Name = swaggerConfigOption.Name,
                            Url = new Uri(swaggerConfigOption.Url)
                        }
                    });
                //加载xml文档
                options.IncludeXmlComments(xmlFile);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "在下框中输入Token",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}