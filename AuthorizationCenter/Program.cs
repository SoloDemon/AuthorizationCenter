using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Models.Options;

namespace AuthorizationCenter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IConfiguration configuration = null;
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => { configuration = config.Build(); })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        var kestrelListen = configuration.GetSection("KestrelConfig").Get<KestrelOption>();
                        serverOptions.Limits.MaxRequestBodySize = 9223372036854775807;
                        serverOptions.Limits.MaxRequestBufferSize = 9223372036854775807;
                        serverOptions.Limits.MaxRequestLineSize = 2147483647;
                        serverOptions.ListenAnyIP(kestrelListen.ApiPort, o => o.Protocols = HttpProtocols.Http1);
                        if (kestrelListen.GrpcPort != 0)
                            serverOptions.ListenAnyIP(kestrelListen.GrpcPort, o => o.Protocols = HttpProtocols.Http2);
                    });
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}