using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using FrameworkCore.Validation;
using Interfaces.Validation.RequestValidator;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Dtos;
using Models.Dtos.UserManager;
using Models.Validation;

namespace Services.Validation.RequestValidator
{
    /// <summary>
    /// 重置密码请求验证
    /// </summary>
    public class ResetPasswordRequestValidator: IResetPasswordRequestValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVerificationCodeRequestValidator _verificationCodeRequestValidator;

        public ResetPasswordRequestValidator(UserManager<ApplicationUser> userManager,
            IVerificationCodeRequestValidator verificationCodeRequestValidator)
        {
            _userManager = userManager;
            _verificationCodeRequestValidator = verificationCodeRequestValidator;
        }

        /// <summary>
        /// 验证请求
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <returns></returns>
        public async Task<ValidationRequestResult> ValidateRequestAsync(NameValueCollection parameters)
        {
            parameters["resetPasswordType"] ??= "Email";
            return parameters["resetPasswordType"] switch
            {
                "Email" => await EmailValidateRequestAsync(parameters),
                "Caption" => await CaptionValidateRequestAsync(parameters),
                _ => new ValidationRequestResult("无效的重置密码类型", "无效的重置密码类型")
            };
        }

        /// <summary>
        /// 验证码重置密码
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <returns></returns>
        private async Task<ValidationRequestResult> CaptionValidateRequestAsync(NameValueCollection parameters)
        {
            var model = new ReSetPasswordPhoneNumberInput
            {
                Caption = parameters["caption"],
                PhoneNumber = parameters["phoneNumber"],
                Role = parameters["role"] ?? "Guest",
                Password = parameters["password"],
                ConfirmPassword = parameters["confirmPassword"]
            };

            var validationResult = ValidationHelper.ValidatorProperty(model);
            if (validationResult != null)
                return validationResult;
            if (!await _verificationCodeRequestValidator.SmsVerificationCodeValidateRequest(model.PhoneNumber,
                model.Caption))
                return new ValidationRequestResult("验证码不正确", "验证码不正确,请重新输入验证码.");
            var user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
            return user is null ? new ValidationRequestResult($"手机号{model.PhoneNumber}不存在", $"当前手机号{model.PhoneNumber}不存在,请确认手机号是否正确.")
                : new ValidationRequestResult();
        }

        /// <summary>
        /// 电子邮箱重置密码
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <returns></returns>
        private async Task<ValidationRequestResult> EmailValidateRequestAsync(NameValueCollection parameters)
        {
            var model = new ReSetPasswordEmailInput
            {
                Email = parameters["email"],
                Password = parameters["password"],
                ConfirmPassword = parameters["confirmPassword"]
            };

            var validationResult = ValidationHelper.ValidatorProperty(model);
            if (validationResult != null)
                return validationResult;
            var user = await _userManager.FindByEmailAsync(model.Email);
            return user is null ? new ValidationRequestResult($"邮箱{model.Email}不存在", $"当前邮箱{model.Email}不存在,请确认邮箱是否正确.")
                : new ValidationRequestResult();
        }
    }
}