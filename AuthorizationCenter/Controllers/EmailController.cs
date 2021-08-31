using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FrameworkCore.Extensions;
using Interfaces;

namespace AuthorizationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailServices _emailServices;

        public EmailController(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }

        /// <summary>
        /// 发送重置密码邮件
        /// </summary>
        /// <param name="email">email地址</param>
        /// <param name="subject">邮件主题</param>
        /// <returns></returns>
        [HttpGet]
        [Route("SendResetPasswordEmail")]
        public async Task<IActionResult> SendResetPasswordEmailAsync(string email, string subject)
        {
            var queryList = HttpContext.Request.Query.AsNameValueCollection();
            const string reg = @"(?=htt).*//[a-zA-Z0-9:]+(?=/)";
            var url = Regex.Match(Request.Headers["Referer"], reg);
            queryList.Set("Referer", url.Value);
            queryList.Set("sendEmailType", "ReSetPassword");
            var result = await _emailServices.SendEmailAsync(queryList);
            return new JsonResult(result);
        }
    }
}
