using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FrameworkCore.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void SetNoCache(this HttpResponse response)
        {
            if (!response.Headers.ContainsKey("Cache-Control"))
                response.Headers.Add("Cache-Control", "no-store, no-cache, max-age=0");
            else
                response.Headers["Cache-Control"] = "no-store, no-cache, max-age=0";

            if (!response.Headers.ContainsKey("Pragma")) response.Headers.Add("Pragma", "no-cache");
        }

        public static async Task WriteJsonAsync(this HttpResponse response, object o, string contentType = null)
        {
            var json = JsonSerializer.Serialize(o, new JsonSerializerOptions
            {
                IgnoreNullValues = true
            });
            await response.WriteJsonAsync(json, contentType);
            await response.Body.FlushAsync();
        }
    }
}