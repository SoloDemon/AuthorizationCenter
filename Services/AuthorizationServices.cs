using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Interfaces;
using Interfaces.Validation;
using Interfaces.Validation.RequestValidator;
using Models;
using Models.Validation;
using Services.Endpoint;

namespace Services
{
    /// <summary>
    ///     授权服务
    /// </summary>
    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly IJwtServices _jwtServices;
        private readonly ISmsSendValidator _smsSendValidator;
        private readonly ITokenRequestValidator _tokenRequestValidator;

        public AuthorizationServices(
            IJwtServices jwtServices,
            ITokenRequestValidator tokenRequestValidator,
            ISmsSendValidator smsSendValidator)
        {
            _jwtServices = jwtServices;
            _tokenRequestValidator = tokenRequestValidator;
            _smsSendValidator = smsSendValidator;
        }

        /// <summary>
        ///     登录
        /// </summary>
        /// <paramref name="queryList">请求参数列表</paramref>
        /// <returns></returns>
        public async Task<IEndpointResult> LoginAsync(NameValueCollection queryList)
        {
            if (queryList == null) throw new ArgumentNullException(nameof(queryList));

            #region 查询claims

            ////获取用户claims
            //var claimsList = await _userManager.GetClaimsAsync(user);
            ////添加用户到role
            //await _userManager.AddToRoleAsync(user, "systemmanager");
            ////获取用户role
            //var roles = await _userManager.GetRolesAsync(user);
            ////获取role claims
            //var aaa = await _roleManager.GetClaimsAsync(new ApplicationRole { Name = roles.First() });

            #endregion

            #region 初始化用户角色

            //var users = await _userManager.GetUsersForClaimAsync(new Claim("role", "User"));
            //await _roleManager.CreateAsync(new ApplicationRole
            //{
            //    CreateBy = "system",
            //    CreateTime = DateTime.Now,
            //    Enabled = true,
            //    Description = "来宾用户",
            //    IsDeleted = false,
            //    Name = "Guest",
            //    OrderSort = 2
            //});
            //foreach (var applicationUser in users)
            //{
            //    await _userManager.AddToRoleAsync(applicationUser, "Guest");
            //}

            #endregion

            var requestResult = await _tokenRequestValidator.ValidateRequestAsync(queryList);
            if (requestResult.IsError)
                return TokenRequestError(requestResult.Error, requestResult.ErrorDescription,
                    requestResult.CustomResponse);
            return await TokenRequestSuccess(requestResult);
        }

        /// <summary>
        ///     成功响应
        /// </summary>
        /// <param name="requestValidationResult">请求验证结果</param>
        /// <returns></returns>
        private async Task<TokenSuccessResult> TokenRequestSuccess(TokenRequestValidationResult requestValidationResult)
        {
            var tokenResponse = await _jwtServices.GetTokenAsync(requestValidationResult);
            return new TokenSuccessResult(tokenResponse);
        }

        /// <summary>
        ///     失败响应
        /// </summary>
        /// <param name="error">错误</param>
        /// <param name="errorDescription">错误描述</param>
        /// <param name="custom">自定义响应</param>
        /// <returns></returns>
        private static TokenErrorResult TokenRequestError(string error, string errorDescription = null,
            Dictionary<string, object> custom = null)
        {
            var response = new TokenErrorResponse
            {
                Error = error,
                ErrorDescription = errorDescription,
                Custom = custom
            };

            return new TokenErrorResult(response);
        }
    }
}