using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Validation;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Validation;

namespace Services.Validation
{
    public class ChangePasswordValidator : IChangePasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        ///     修改密码
        /// </summary>
        /// <param name="parameters">请求参数列表</param>
        /// <returns></returns>
        public async Task<ValidationRequestResult> ValidateAsync(NameValueCollection parameters)
        {
            var user = await _userManager.FindByIdAsync(parameters["userId"]);
            if (user is null)
                return new ValidationRequestResult("用户不存在", "用户不存在，请更换确认用户名是否正确!");
            var result =
                await _userManager.ChangePasswordAsync(user, parameters["oldPassword"], parameters["newPassword"]);
            if (result.Succeeded)
                return new ValidationRequestResult();
            var identityError = result.Errors.FirstOrDefault();
            return identityError is not null
                ? new ValidationRequestResult(identityError.Description, identityError.Description)
                : new ValidationRequestResult("修改密码错误", "修改密码错误，请重新尝试");
        }
    }
}