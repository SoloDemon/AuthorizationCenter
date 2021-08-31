using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using AuthorizationCenter.Extensions;
using EntityFramework.Core;
using FrameworkCore.Extensions;
using FrameworkCore.Extensions.Swagger;
using FrameworkCore.Helper;
using FrameworkCore.Helper.Sms;
using FrameworkCore.Security;
using Interfaces;
using Interfaces.Execute;
using Interfaces.UserManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Models;
using Models.Options;
using Services;
using Services.Execute;
using Services.Validation;

namespace AuthorizationCenter
{
    /// <summary>
    ///     Remove-Migration -Context ApplicationDbContext
    ///     Add-Migration InitialApplicationDb -Context ApplicationDbContext -OutputDir Migrations/ApplicationDb
    ///     update-database -Context DynamicFormDbContext
    ///     Script-Migration -From migrationName1 -To migrationName2 -Context ApplicationDbContext
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });
            services.AddSwaggerForJwt(Configuration);

            #region 注册授权中心

            var path = Configuration["Certificates:Path"].Split(',');
            services.AddAuthorizationCenter()
                .AddSigningCredential(new X509Certificate2(
                    Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                        path[0], path[1]),
                    Configuration["Certificates:Password"]))
                .UseExtensionGrantValidator()
                .AddExtensionGrantValidator<DefaultExtensionGrantValidator>()
                .AddExtensionGrantValidator<WeChatExtensionGrantValidator>()
                .AddExtensionGrantValidator<PhoneCaptionExtensionGrantValidator>()
                .AddExtensionGrantValidator<PhoneNumberExtensionGrantValidator>()
                //添加缓存支持
                .AddCachingContainer(Configuration);

            #endregion

            #region 配置Identity

            #region MySql

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration["ConnectionStrings:IdentityServer"],
                        new MySqlServerVersion(new Version(8, 0, 21)))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

            #endregion

            //启用 Identity 服务 添加指定的用户和角色类型的默认标识系统配置
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    //密码设置
                    options.Password = new PasswordOptions
                    {
                        RequireDigit = true, //要求密码中的数字介于0-9 之间。
                        RequiredLength = 8, //密码的最小长度。
                        RequireNonAlphanumeric = false, //密码中需要一个非字母数字字符。
                        RequireLowercase = true, //密码中需要小写字符。
                        RequireUppercase = true, //密码中需要大写字符。
                        RequiredUniqueChars = 1 //需要密码中的非重复字符数。
                    };
                    //锁定设置
                    options.Lockout = new LockoutOptions
                    {
                        AllowedForNewUsers = true, //确定新用户是否可以锁定。
                        DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5), //	锁定发生时用户被锁定的时间长度。
                        MaxFailedAccessAttempts = 3 //如果启用了锁定，则在用户被锁定之前失败的访问尝试次数。
                    };
                    //登陆设置
                    options.SignIn = new SignInOptions
                    {
                        RequireConfirmedEmail = false, //需要确认电子邮件登录。
                        RequireConfirmedPhoneNumber = false //需要确认电话号码才能登录。
                    };
                    //用户设置
                    options.User = new UserOptions
                    {
                        AllowedUserNameCharacters =
                            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.-", //用户名中允许使用的字符。
                        RequireUniqueEmail = false //要求每个用户都有唯一的电子邮件。
                    };
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<CustomIdentityErrorDescriber>(); // Add this line;

            #endregion

            //services.AddAuthorizationClient().AddAuthentication();

            #region DI

            services.AddTransient<IAuthorizationServices, AuthorizationServices>();
            services.AddTransient<AesEncryption>();
            services.AddTransient<EmailHelper>();
            services.AddTransient<IJwtServices, JwtServices>();
            //注入阿里云短信服务，需要注入其他短信服务，可以自行实现短信服务。
            services.AddTransient<ISmsHelper, ALiYunSmsHelper>();
            services.AddTransient<ISmsServices, SmsServices>();
            services.AddTransient<IUserManagerServices, UserManagerServices>();
            services.AddTransient<IResetPasswordServices, UserManagerServices>();
            services.AddTransient<IChangePasswordServices, UserManagerServices>();
            services.AddTransient<IRegisterServices, UserManagerServices>();
            services.AddTransient<IResetPasswordServices, UserManagerServices>();
            services.AddTransient<IEmailServices, EmailServices>();
            services.AddTransient<IEmailExecute, EmailExecute>();

            #endregion

            #region options

            services.Configure<SecurityOption>(Configuration.GetSection("Security"));
            services.Configure<JwtOption>(Configuration.GetSection("JwtOption"));
            services.Configure<CertificatesOption>(Configuration.GetSection("CertificatesOption"));
            services.Configure<EmailConfigOption>(Configuration.GetSection("EmailConfig"));
            services.Configure<CorsOption>(Configuration.GetSection("CorsConfig"));
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});

            #endregion

            #region Cors

            services.AddCors(options =>
            {
                options.AddPolicy("authorization", x =>
                {
                    x.WithOrigins(Configuration.GetValue<string>("CorsConfig").Split(','))
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //初始化数据库启动时添加此命令 --InitDB=true.
            if (Configuration["InitDB"] == "true") app.InitializeSeedData();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthorizationCenter v1"));
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}