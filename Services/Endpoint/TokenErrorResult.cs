using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FrameworkCore.Extensions;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;

namespace Services.Endpoint
{
    public class TokenErrorResult : IEndpointResult
    {
        public TokenErrorResult(TokenErrorResponse response)
        {
            Response = response;
        }

        /// <summary>
        ///     token请求错误响应
        /// </summary>
        public TokenErrorResponse Response { get; }

        /// <summary>
        ///     执行http响应
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <returns></returns>
        public async Task ExecuteAsync(HttpContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.SetNoCache();

            await context.Response.WriteAsJsonAsync(new ApiResultMessage<ResultDto>
            {
                Response = new ResultDto
                {
                    ErrorDescription = Response.ErrorDescription,
                    Custom = Response.Custom
                },
                Msg = Response.Error,
                Status = 200,
                Success = false
            }, new JsonSerializerOptions {PropertyNamingPolicy = null});
        }

        internal class ResultDto
        {
            public string ErrorDescription { get; set; }

            [JsonExtensionData] public Dictionary<string, object> Custom { get; set; }
        }
    }
}