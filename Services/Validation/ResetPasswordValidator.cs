using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Validation;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Validation;

namespace Services.Validation
{
    /// <summary>
    /// 重置密码验证器
    /// </summary>
    public class ResetPasswordValidator : IResetPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 验证重置密码
        /// </summary>
        /// <param name="parameter">参数集合</param>
        /// <returns></returns>
        public async Task<ValidationRequestResult> ValidationResetPasswordAsync(NameValueCollection parameter)
        {
            return parameter["resetPasswordType"] switch
            {
                "Email" => await EmailValidationResetPasswordAsync(parameter),
                "Caption" => await PhoneNumberValidationResetPasswordAsync(parameter),
                _ => new ValidationRequestResult("无效的重置密码类型", "无效的重置密码类型")
            };
        }

        /// <summary>
        /// 验证码重置密码
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task<ValidationRequestResult> PhoneNumberValidationResetPasswordAsync(NameValueCollection parameter)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == parameter["phoneNumber"] && x.PhoneNumberConfirmed);
                if (user is null)
                    throw new Exception("手机号不存在");
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, parameter["password"]);
                return result.Succeeded
                 ? new ValidationRequestResult()
                 : new ValidationRequestResult(result.Errors.First().Code, result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        /// <summary>
        /// 电子邮件重置密码
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task<ValidationRequestResult> EmailValidationResetPasswordAsync(NameValueCollection parameter)
        {
            var user = await _userManager.FindByEmailAsync(parameter["email"]);
            var result = await _userManager.ResetPasswordAsync(user, parameter["token"], parameter["password"]);
            return result.Succeeded
                ? new ValidationRequestResult()
                : new ValidationRequestResult(result.Errors.First().Code, result.Errors.First().Description);
        }
    }
}
