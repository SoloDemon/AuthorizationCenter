using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FrameworkCore.Security;
using FrameworkCore.Validation;
using Interfaces.Validation.RequestValidator;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Dtos.Register;
using Models.Validation;

namespace Services.Validation.RequestValidator
{
    public class RegisterRequestValidator : IRegisterRequestValidator
    {
        private readonly AesEncryption _aesEncryption;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVerificationCodeRequestValidator _verificationCodeRequestValidator;

        public RegisterRequestValidator(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            AesEncryption aesEncryption,
            IVerificationCodeRequestValidator verificationCodeRequestValidator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _aesEncryption = aesEncryption;
            _verificationCodeRequestValidator = verificationCodeRequestValidator;
        }

        /// <summary>
        ///     验证请求
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<RegisterRequestValidationResult> ValidateRequestAsync(NameValueCollection parameters)
        {
            parameters["registerType"] ??= "Default";
            return parameters["registerType"] switch
            {
                "Default" => await DefaultValidateRequestAsync(parameters),
                "WeChat" => await WeChatValidateRequestAsync(parameters),
                "PhoneNumber" => await PhoneNumberValidateRequestAsync(parameters),
                _ => new RegisterRequestValidationResult("无效的注册类型", "无效的注册类型")
            };
        }

        /// <summary>
        ///     手机短信注册验证
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private async Task<RegisterRequestValidationResult> PhoneNumberValidateRequestAsync(
            NameValueCollection parameters)
        {
            var model = new PhoneNumberInput
            {
                Caption = parameters["caption"],
                PhoneNumber = parameters["phoneNumber"],
                Role = parameters["role"] ?? "Guest"
            };
            var validationResult = ValidatorProperty(model);
            if (validationResult != null)
                return validationResult;
            if (!await _verificationCodeRequestValidator.SmsVerificationCodeValidateRequest(model.PhoneNumber,
                model.Caption))
                return new RegisterRequestValidationResult("验证码不正确", "验证码不正确,请重新输入验证码.");
            var user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
            if (user is not null)
                return new RegisterRequestValidationResult("手机号已存在", "当前手机号已存在,请更换手机号注册.");
            if (!await _roleManager.RoleExistsAsync(model.Role))
                return new RegisterRequestValidationResult("角色不存在", "添加角色不存在");
            return new RegisterRequestValidationResult();
        }

        /// <summary>
        ///     微信注册验证
        /// </summary>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        private async Task<RegisterRequestValidationResult> WeChatValidateRequestAsync(NameValueCollection parameters)
        {
            var model = new WeChatRegisterInput
            {
                PhoneNumber = parameters["phoneNumber"],
                WeChatOpenId = await _aesEncryption.AesDecryptAsync(parameters["WeChatOpenId"]),
                Province = parameters["province"],
                City = parameters["city"],
                Country = parameters["country"],
                NickName = parameters["nickName"],
                Gender = byte.Parse(parameters["gender"] ?? "0"),
                Portrait = parameters["portrait"],
                Role = parameters["role"] ?? "Guest"
            };
            var validationResult = ValidatorProperty(model);
            if (validationResult != null)
                return validationResult;
            var user = _userManager.Users.FirstOrDefault(x => x.WeChatOpenId == model.WeChatOpenId);
            if (user is not null)
                return new RegisterRequestValidationResult("当前微信号已存在", "当前微信号已存在,请更换微信号");
            if (!await _roleManager.RoleExistsAsync(model.Role))
                return new RegisterRequestValidationResult("角色不存在", "角色不存在");
            return new RegisterRequestValidationResult();
        }

        /// <summary>
        ///     默认注册验证
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private async Task<RegisterRequestValidationResult> DefaultValidateRequestAsync(NameValueCollection parameters)
        {
            var model = new RegisterInput
            {
                NickName = parameters["nickName"],
                UserName = parameters["userName"],
                Password = parameters["password"],
                ConfirmPassword = parameters["confirmPassword"],
                Email = parameters["email"],
                Role = parameters["role"] ?? "Guest"
            };
            var validationResult = ValidatorProperty(model);
            if (validationResult != null)
                return validationResult;
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is not null)
                return new RegisterRequestValidationResult("用户已存在", "用户名已存在,请更换用户名");
            if (!await _roleManager.RoleExistsAsync(model.Role))
                return new RegisterRequestValidationResult("角色不存在", "角色不存在");
            return new RegisterRequestValidationResult();
        }


        /// <summary>
        ///     验证属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        private static RegisterRequestValidationResult ValidatorProperty<T>(T model)
        {
            //验证上下文
            var context = new ValidationContext(model, null, null);
            //验证结果
            var results = new List<ValidationResult>();
            //验证结果
            if (AttributeValidator.Validator(model, context, results)) return null;
            var errorMessage = results.FirstOrDefault()?.ErrorMessage;
            return new RegisterRequestValidationResult(errorMessage, errorMessage);
        }
    }
}