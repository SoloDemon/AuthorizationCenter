using FrameworkCore.Extensions;
using IdentityModel;
using Interfaces.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.UserManager;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private readonly IChangePasswordServices _changePasswordServices;

        public ChangePasswordController(IChangePasswordServices changePasswordServices)
        {
            _changePasswordServices = changePasswordServices;
        }

        /// <summary>
        ///     修改密码
        /// </summary>
        /// <param name="input">修改密码输入参数</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "guest")]
        public async Task Post(ChangePasswordInput input)
        {
            var queryList = HttpContext.Request.Query.AsNameValueCollection();
            queryList.Set("userId", User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value);
            var result = await _changePasswordServices.ChangePassword(queryList);
            await result.ExecuteAsync(HttpContext);
        }
    }
}