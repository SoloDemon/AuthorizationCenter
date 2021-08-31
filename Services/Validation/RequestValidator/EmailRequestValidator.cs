using FrameworkCore.Validation;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Dtos;
using Models.Validation;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Validation.RequestValidator;
using Microsoft.Extensions.Options;
using Models.Options;

namespace Services.Validation.RequestValidator
{
    /// <summary>
    /// 电子邮箱请求验证
    /// </summary>
    public class EmailRequestValidator : IEmailRequestValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CorsOption _corsOption;

        public EmailRequestValidator(UserManager<ApplicationUser> userManager,
            IOptions<CorsOption> corsOption)
        {
            _userManager = userManager;
            _corsOption = corsOption.Value;
        }

        /// <summary>
        /// 验证Email请求
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<ValidationRequestResult> ValidateRequestAsync(NameValueCollection parameter)
        {
            parameter["sendEmailType"] ??= "Default";
            return parameter["sendEmailType"] switch
            {
                "Default" => await DefaultValidateRequestAsync(parameter),
                "ReSetPassword" => await ReSetPasswordValidateRequestAsync(parameter),
                _ => new ValidationRequestResult { Error = "无效的发送邮件类型", ErrorDescription = "无效的发送邮件类型", IsError = true }
            };
        }

        private async Task<ValidationRequestResult> ReSetPasswordValidateRequestAsync(NameValueCollection parameter)
        {
            var model = new EmailInput
            {
                Email = parameter["email"]
            };

            var validationResult = ValidationHelper.ValidatorProperty(model);
            if (validationResult != null)
                return validationResult;
            var urlArray = _corsOption.CorsList.Split(',');
            if (urlArray.All(x => x != parameter["Referer"]))
                return new ValidationRequestResult("非法请求", "非法请求");
            var user = await _userManager.FindByEmailAsync(model.Email);
            return user is null ? new ValidationRequestResult($"邮箱{model.Email}不存在", $"当前邮箱{model.Email}不存在,请确认邮箱是否正确.")
                : new ValidationRequestResult();
        }

        /// <summary>
        /// 默认请求验证
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task<ValidationRequestResult> DefaultValidateRequestAsync(NameValueCollection parameter)
        {
            var model = new EmailInput
            {
                Email = parameter["email"]
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
