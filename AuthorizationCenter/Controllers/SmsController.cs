using System.Threading.Tasks;
using FrameworkCore.Extensions;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ISmsServices _smsServices;

        public SmsController(ISmsServices smsServices)
        {
            _smsServices = smsServices;
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