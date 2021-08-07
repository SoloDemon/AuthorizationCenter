using System.Text.Json;
using System.Threading.Tasks;
using FrameworkCore.Extensions;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Models;

namespace Services.Endpoint
{
    /// <summary>
    ///     基类
    /// </summary>
    public class SuccessResult : IEndpointResult
    {
        public virtual async Task ExecuteAsync(HttpContext context)
        {
            context.Response.SetNoCache();

            await context.Response.WriteAsJsonAsync(new ApiResultMessage
            {
                Msg = "请求成功",
                Status = 200,
                Success = true
            }, new JsonSerializerOptions {PropertyNamingPolicy = null});
        }
    }
}