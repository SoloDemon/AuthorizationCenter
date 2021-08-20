using System;
using System.Collections.Generic;
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
    ///     默认扩展授权验证
    /// </summary>
    public class DefaultExtensionGrantValidator : IExtensionGrantValidator
    {
        private readonly AesEncryption _aesEncryption;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DefaultExtensionGrantValidator(
            UserManager<ApplicationUser> userManager,
            AesEncryption aesEncryption,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _aesEncryption = aesEncryption;
            _signInManager = signInManager;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            try
            {
                var userName = context.Request.Raw["username"];
                var password = context.Request.Raw["password"];

                var user = await _userManager.FindByNameAsync(userName);
                //用户是否存在
                if (user != null)
                {
                    //用户是否删除
                    if (!user.IsDelete)
                    {
                        var result = await _signInManager.PasswordSignInAsync(userName, password, true, true);
                        //登录是否成功
                        if (result.Succeeded)
                        {
                            var roles = await _userManager.GetRolesAsync(user);
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
                        }
                        else
                        {
                            context.Result = new GrantValidationResult
                            {
                                IsError = true,
                                Error = "用户名或密码错误,登陆失败!"
                            };
                        }
                    }
                }
                else
                {
                    context.Result = new GrantValidationResult
                    {
                        IsError = true,
                        Error = "用户不存在"
                    };
                }
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
        public string GrantType => Models.GrantType.ResourceDefault;
    }
}