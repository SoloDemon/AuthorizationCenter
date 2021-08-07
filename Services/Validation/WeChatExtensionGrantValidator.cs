using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FrameworkCore.Security;
using Interfaces.Validation;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Validation;

namespace Services.Validation
{
    /// <summary>
    ///     微信扩展授权验证
    /// </summary>
    public class WeChatExtensionGrantValidator : IExtensionGrantValidator
    {
        private readonly AesEncryption _aesEncryption;
        private readonly UserManager<ApplicationUser> _userManager;

        public WeChatExtensionGrantValidator(UserManager<ApplicationUser> userManager, AesEncryption aesEncryption)
        {
            _userManager = userManager;
            _aesEncryption = aesEncryption;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            try
            {
                var openId = context.Request.Raw["openid"];
                var user = _userManager.Users.FirstOrDefault(x => x.WeChatOpenId == _aesEncryption.AesDecrypt(openId));
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
                            new("WeChatOpenId", $"{openId}")
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
        public string GrantType => Models.GrantType.ResourceWeChat;
    }
}