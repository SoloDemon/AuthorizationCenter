using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FrameworkCore.Security;
using Interfaces.Validation;
using Interfaces.Validation.RequestValidator;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Validation;

namespace Services.Validation
{
    /// <summary>
    ///     手机号验证码登录扩展验证
    /// </summary>
    public class PhoneCaptionExtensionGrantValidator : IExtensionGrantValidator
    {
        private readonly AesEncryption _aesEncryption;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVerificationCodeRequestValidator _verificationCodeRequestValidator;

        public PhoneCaptionExtensionGrantValidator(UserManager<ApplicationUser> userManager,
            AesEncryption aesEncryption,
            IVerificationCodeRequestValidator verificationCodeRequestValidator)
        {
            _userManager = userManager;
            _aesEncryption = aesEncryption;
            _verificationCodeRequestValidator = verificationCodeRequestValidator;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            try
            {
                var phoneNumber = context.Request.Raw["phoneNumber"];
                var caption = context.Request.Raw["caption"];
                if (!await _verificationCodeRequestValidator.SmsVerificationCodeValidateRequest(phoneNumber, caption))
                {
                    context.Result = new GrantValidationResult
                    {
                        IsError = true,
                        Error = "验证码不正确"
                    };
                    return;
                }

                var user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
                var roles = await _userManager.GetRolesAsync(user);
                if (user != null)
                    //授权通过返回
                    context.Result = new GrantValidationResult
                    (
                        GrantType,
                        role: roles,
                        user: user,
                        claims: new List<Claim>
                        {
                            new("WeChatOpenId",
                                $"{await _aesEncryption.AesEncryptAsync(user.Id.ToString())}")
                        }
                    );
                else
                    context.Result = new GrantValidationResult
                    {
                        IsError = true,
                        Error = "用户不存在"
                    };
            }
            catch (Exception e)
            {
                context.Result = new GrantValidationResult
                {
                    IsError = true,
                    Error = e.Message
                };
            }
        }

        /// <summary>
        ///     授权类型
        /// </summary>
        public string GrantType => Models.GrantType.ResourcePhoneCaption;
    }
}