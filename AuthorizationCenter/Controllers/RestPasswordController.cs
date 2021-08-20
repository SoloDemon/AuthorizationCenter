using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthorizationCenter.Controllers
{
    /// <summary>
    ///     重置密码
    /// </summary>
    [Route("api/[controller]")]
    public class RestPasswordController : Controller
    {
        /// <summary>
        ///     通过短信验证码重置密码
        /// </summary>
        /// <param name="caption">验证码</param>
        [HttpPost]
        [Route("Caption")]
        public void Caption([FromBody] string caption)
        {
        }
    }
}