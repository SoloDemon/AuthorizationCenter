using System.Text.Json;
using System.Threading.Tasks;
using FrameworkCore.Extensions;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Models;

namespace Services.Endpoint
{
    public class ErrorResult : IEndpointResult
    {
        public ErrorResult(ErrorResponse response)
        {
            Response = response;
        }

        /// <summary>
        ///     token请求错误响应
        /// </summary>
        public ErrorResponse Response { get; }

        /// <summary>
        ///     执行http响应
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <returns></returns>
        public virtual async Task ExecuteAsync(HttpContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.SetNoCache();
            await context.Response.WriteAsJsonAsync(new ApiResultMessage<ResultDto>
            {
                Response = new ResultDto
                {
                    ErrorDescription = Response.ErrorDescription
                },
                Msg = Response.Error,
                Status = 200,
                Success = false
            }, new JsonSerializerOptions {PropertyNamingPolicy = null});
        }

        internal class ResultDto
        {
            public string ErrorDescription { get; set; }
        }
    }
}