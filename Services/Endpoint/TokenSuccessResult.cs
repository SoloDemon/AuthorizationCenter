using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FrameworkCore.Extensions;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;

namespace Services.Endpoint
{
    public class TokenSuccessResult : SuccessResult
    {
        public TokenSuccessResult(TokenResponse response)
        {
            Response = response;
        }

        public TokenResponse Response { get; set; }

        public override async Task ExecuteAsync(HttpContext context)
        {
            context.Response.SetNoCache();

            await context.Response.WriteAsJsonAsync(new ApiResultMessage<ResultDto>
            {
                Response = new ResultDto
                {
                    access_token = Response.AccessToken,

                    Custom = Response.Custom
                },
                Msg = "请求成功",
                Status = 200,
                Success = true
            }, new JsonSerializerOptions {PropertyNamingPolicy = null});
        }

        internal class ResultDto
        {
            public string access_token { get; set; }

            [JsonExtensionData] public Dictionary<string, object> Custom { get; set; }
        }
    }
}