using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrameworkCore.Extensions;
using Interfaces;

namespace AuthorizationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthorizationServices _authorizationService;

        public LoginController(IAuthorizationServices authorizationService)
        {
            _authorizationService = authorizationService;
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

    }
}
