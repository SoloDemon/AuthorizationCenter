using System;
using System.IO;
using System.Threading.Tasks;
using FrameworkCore.Extensions;
using Interfaces.UserManager;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationCenter.Controllers
{
    /// <summary>
    ///     重置密码
    /// </summary>
    [Route("api/[controller]")]
    public class RestPasswordController : Controller
    {
        private readonly IResetPasswordServices _iResetPasswordServices;


        public RestPasswordController(IResetPasswordServices iResetPasswordServices)
        {
            _iResetPasswordServices = iResetPasswordServices;
        }

        /// <summary>
        ///     通过短信验证码重置密码
        /// </summary>
        /// <param name="caption">验证码</param>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="confirmPassword">确认密码</param>
        [HttpPost]
        [Route("Caption")]
        public async Task<IActionResult> Caption(string caption, string phoneNumber, string password, string confirmPassword)
        {
            using var reader = new StreamReader(Request.Body);
            var content = await reader.ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));
            var queryList = content.AsNameValueCollection();
            queryList.Set("resetPasswordType", "Caption");
            queryList.Set("role", null);
            var result =await _iResetPasswordServices.ResetPasswordAsync(queryList);
            return new JsonResult(result);
        }

        /// <summary>
        /// 通过电子邮箱重置密码
        /// </summary>
        /// <param name="email">电子邮箱</param>
        /// <param name="token">重置密码Token</param>
        /// <param name="password">密码</param>
        /// <param name="confirmPassword">确认密码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Email")]
        public async Task<IActionResult> Email(string email,string token, string password, string confirmPassword)
        {
            using var reader = new StreamReader(Request.Body);
            var content = await reader.ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));
            var queryList = content.AsNameValueCollection();
            queryList.Set("resetPasswordType", "Email");
            var result = await _iResetPasswordServices.ResetPasswordAsync(queryList);
            return new JsonResult(result);
        }
    }
}