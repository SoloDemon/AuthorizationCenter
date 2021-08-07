using System.IO;
using System.Threading.Tasks;
using FrameworkCore.Extensions;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationCenter.Controllers
{
    /// <summary>
    ///     授权控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationServices _authorizationService;
        private readonly ICachingServices _cachingServices;
        private readonly HttpContext _httpContext;
        private readonly ISmsServices _smsServices;


        public AuthorizationController(IAuthorizationServices authorizationService,
            IHttpContextAccessor httpContext,
            ICachingServices cachingServices,
            ISmsServices smsServices)
        {
            _authorizationService = authorizationService;
            _httpContext = httpContext.HttpContext;
            _cachingServices = cachingServices;
            _smsServices = smsServices;
        }

        /// <summary>
        ///     登录-授权类型:Default|WeChat
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Login")]
        public async Task Login()
        {
            var queryList = HttpContext.Request.Query.AsNameValueCollection();
            var result = await _authorizationService.LoginAsync(queryList);
            await result.ExecuteAsync(HttpContext);
        }


        /// <summary>
        ///     默认登录-测试用，正式使用请取消
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DefaultLogin")]
        public async Task Login(string username, string password)
        {
            var queryList = HttpContext.Request.Query.AsNameValueCollection();
            queryList.Set("grantType", "Default");
            var result = await _authorizationService.LoginAsync(queryList);
            await result.ExecuteAsync(HttpContext);
        }


        /// <summary>
        ///     微信登录-测试用，正式使用请取消
        /// </summary>
        /// <param name="openid">微信Openid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("WeChatLogin")]
        public async Task Login(string openid)
        {
            var queryList = HttpContext.Request.Query.AsNameValueCollection();
            queryList.Set("grantType", "WeChat");
            var result = await _authorizationService.LoginAsync(queryList);
            await result.ExecuteAsync(HttpContext);
        }


        /// <summary>
        ///     手机验证码登录-测试用，正式使用请取消
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="caption">验证码</param>
        /// <param name="i">重载使用参数...</param>
        /// <returns></returns>
        [HttpGet]
        [Route("PhoneCaptionLogin")]
        public async Task Login(string phoneNumber, string caption, int i = 1)
        {
            var queryList = HttpContext.Request.Query.AsNameValueCollection();
            queryList.Set("grantType", "PhoneCaption");
            var result = await _authorizationService.LoginAsync(queryList);
            await result.ExecuteAsync(HttpContext);
        }

        /// <summary>
        ///     手机登录-测试用，正式使用请取消
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="i">重载使用参数</param>
        /// <returns></returns>
        [HttpGet]
        [Route("PhoneLogin")]
        public async Task Login(string phoneNumber, string password, bool i = false)
        {
            var queryList = HttpContext.Request.Query.AsNameValueCollection();
            queryList.Set("grantType", "PhoneNumber");
            var result = await _authorizationService.LoginAsync(queryList);
            await result.ExecuteAsync(HttpContext);
        }


        /// <summary>
        ///     注册用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task Register()
        {
            #region 无参数时取传递的参数

            using var reader = new StreamReader(Request.Body);
            var content = await reader.ReadToEndAsync();
            //if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));
            var queryList = content.AsNameValueCollection();
            queryList.Set("role", null);
            if (queryList.Get("registerType") == null)
                queryList.Set("registerType", null);

            #endregion

            var result = await _authorizationService.RegisterAsync(queryList);
            await result.ExecuteAsync(HttpContext);
        }

        /// <summary>
        ///     默认注册用户-测试用，正式使用请取消
        /// </summary>
        /// <returns></returns>
        /// <param name="nickName">昵称</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="confirmPassword">确认密码</param>
        /// <param name="email">电子邮件</param>
        [HttpPost]
        [Route("DefaultRegister")]
        public async Task Register(string nickName, string userName, string password, string confirmPassword,
            string email)
        {

            var queryList = HttpContext.Request.Query.AsNameValueCollection();
            queryList.Set("registerType", "Default");
            queryList.Set("role", null);
            var result = await _authorizationService.RegisterAsync(queryList);
            await result.ExecuteAsync(HttpContext);
        }

        /// <summary>
        ///     微信注册用户-测试用，正式使用请取消
        /// </summary>
        /// <returns></returns>
        /// <param name="country">国家</param>
        /// <param name="nickName">昵称</param>
        /// <param name="portrait">头像</param>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="weChatOpenId">OpenId</param>
        /// <param name="province">省份</param>
        /// <param name="city">城市</param>
        /// <param name="gender">性别</param>
        [HttpPost]
        [Route("WeChatRegister")]
        public async Task Register(string phoneNumber, string weChatOpenId, string province,
            string city, string country, string nickName, string gender, string portrait)
        {
            var queryList = HttpContext.Request.Query.AsNameValueCollection();
            queryList.Set("registerType", "WeChat");
            queryList.Set("role", null);
            var result = await _authorizationService.RegisterAsync(queryList);
            await result.ExecuteAsync(HttpContext);
        }

        /// <summary>
        ///     手机号注册用户-测试用，正式使用请取消
        /// </summary>
        /// <returns></returns>
        /// <param name="caption">验证码</param>
        /// <param name="phoneNumber">手机号</param>
        [HttpPost]
        [Route("PhoneRegister")]
        public async Task Register(string phoneNumber, string caption)
        {
            var queryList = HttpContext.Request.Query.AsNameValueCollection();
            queryList.Set("registerType", "PhoneNumber");
            queryList.Set("role", null);
            var result = await _authorizationService.RegisterAsync(queryList);
            await result.ExecuteAsync(HttpContext);
        }

        /// <summary>
        ///     发送短信验证码
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <returns></returns>
        [HttpGet]
        [Route("SendCaption")]
        public async Task SendCaption(string phoneNumber)
        {
            var queryList = HttpContext.Request.Query.AsNameValueCollection();
            var result = await _smsServices.SendCaption(queryList);
            await result.ExecuteAsync(HttpContext);
        }
    }
}