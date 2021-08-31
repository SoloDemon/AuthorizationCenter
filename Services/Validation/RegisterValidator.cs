using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using FrameworkCore.Security;
using Interfaces.Validation;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Validation;

namespace Services.Validation
{
    /// <summary>
    ///     注册验证
    /// </summary>
    public class RegisterValidator : IRegisterValidator
    {
        private readonly AesEncryption _aesEncryption;

        /// <summary>
        ///     用户管理
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        ///     构造注入
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="aesEncryption"></param>
        public RegisterValidator(UserManager<ApplicationUser> userManager,
            AesEncryption aesEncryption)
        {
            _userManager = userManager;
            _aesEncryption = aesEncryption;
        }

        /// <summary>
        ///     验证注册
        /// </summary>
        /// <param name="parameters">请求参数列表</param>
        /// <returns></returns>
        public async Task<RegisterValidationResult> ValidateRegisterAsync(NameValueCollection parameters)
        {
            return parameters["registerType"] switch
            {
                "Default" => await DefaultValidateRegisterAsync(parameters),
                "WeChat" => await WeChatValidateRegisterAsync(parameters),
                "PhoneNumber" => await PhoneNumberRegisterAsync(parameters),
                _ => new RegisterValidationResult("无效的注册类型", "无效的注册类型"),
            };
        }

        /// <summary>
        ///     手机短信注册
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private async Task<RegisterValidationResult> PhoneNumberRegisterAsync(NameValueCollection parameters)
        {
            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                NickName = parameters["phoneNumber"],
                UserName = parameters["phoneNumber"],
                PhoneNumber = parameters["phoneNumber"],
                PhoneNumberConfirmed = true,
                IsDelete = false
            };
            return await RegisterAsync(newUser, parameters["role"]);
        }

        /// <summary>
        ///     微信小程序注册
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private async Task<RegisterValidationResult> WeChatValidateRegisterAsync(NameValueCollection parameters)
        {
            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = parameters["phoneNumber"],
                NickName = parameters["nickName"],
                Sex = byte.Parse(parameters["gender"] ?? "0"),
                Portrait = parameters["portrait"],
                City = parameters["city"],
                Country = parameters["country"],
                Province = parameters["province"],
                WeChatOpenId = await _aesEncryption.AesDecryptAsync(parameters["WeChatOpenId"]),
                PhoneNumber = parameters["phoneNumber"],
                PhoneNumberConfirmed = true,
                IsDelete = false
            };
            return await RegisterAsync(newUser, parameters["role"]);
        }

        /// <summary>
        ///     默认注册验证
        /// </summary>
        /// <param name="parameters">请求参数列表</param>
        /// <returns></returns>
        private async Task<RegisterValidationResult> DefaultValidateRegisterAsync(NameValueCollection parameters)
        {
            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                NickName = parameters["nickName"],
                UserName = parameters["userName"],
                Email = parameters["email"],
                IsDelete = false
            };
            return await RegisterAsync(newUser, parameters["password"], parameters["role"]);
        }

        /// <summary>
        ///     注册
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="role">角色</param>
        /// <returns></returns>
        private async Task<RegisterValidationResult> RegisterAsync(ApplicationUser user, string role = null)
        {
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
                return new RegisterValidationResult(result.Errors.First().Code, result.Errors.First().Description);
            result = await _userManager.AddToRoleAsync(user, role ?? "Guest");
            return result.Succeeded
                ? new RegisterValidationResult()
                : new RegisterValidationResult(result.Errors.First().Code, result.Errors.First().Description);
        }

        /// <summary>
        ///     注册
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="password">密码</param>
        /// <param name="role">角色</param>
        /// <returns></returns>
        private async Task<RegisterValidationResult> RegisterAsync(ApplicationUser user, string password,
            string role = null)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return new RegisterValidationResult(result.Errors.First().Code, result.Errors.First().Description);
            result = await _userManager.AddToRoleAsync(user, role ?? "Guest");
            return result.Succeeded
                ? new RegisterValidationResult()
                : new RegisterValidationResult(result.Errors.First().Code, result.Errors.First().Description);
        }
    }
}