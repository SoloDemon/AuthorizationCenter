using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FrameworkCore.Extensions.Swagger
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize =
                context.MethodInfo.DeclaringType != null && (context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                                                 .OfType<AuthorizeAttribute>().Any()
                                                             || context.MethodInfo.GetCustomAttributes(true)
                                                                 .OfType<AuthorizeAttribute>().Any());
            if (!hasAuthorize) return;
            operation.Responses.Add("401", new OpenApiResponse {Description = "很抱歉，您无权访问该接口，请确保已经登录!"});
            operation.Responses.Add("403", new OpenApiResponse {Description = "很抱歉，您的访问权限等级不够，联系管理员!"});

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    [
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        }
                    ] = new[] {"Client.Api"}
                }
            };
        }
    }
}