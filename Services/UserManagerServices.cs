using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Interfaces;
using Interfaces.Validation;
using Interfaces.Validation.RequestValidator;
using Microsoft.AspNetCore.Identity;
using Models;
using Services.Endpoint;

namespace Services
{
    /// <summary>
    ///     用户管理服务
    /// </summary>
    public class UserManagerServices : IUserManagerServices
    {
        private readonly IChangePasswordRequestValidator _changePasswordRequestValidator;
        private readonly IChangePasswordValidator _changePasswordValidator;

        private readonly IRegisterRequestValidator _registerRequestValidator;
        private readonly IRegisterValidator _registerValidator;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerServices(IRegisterRequestValidator registerRequestValidator,
            IRegisterValidator registerValidator,
            UserManager<ApplicationUser> userManager,
            IChangePasswordRequestValidator changePasswordRequestValidator,
            IChangePasswordValidator changePasswordValidator)
        {
            _registerRequestValidator = registerRequestValidator;
            _registerValidator = registerValidator;
            _userManager = userManager;
            _changePasswordRequestValidator = changePasswordRequestValidator;
            _changePasswordValidator = changePasswordValidator;
        }


        /// <summary>
        ///     注册
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IEndpointResult> RegisterAsync(NameValueCollection parameters)
        {
            try
            {
                //验证注册请求
                var requestResult = await _registerRequestValidator.ValidateRequestAsync(parameters);
                if (requestResult.IsError)
                    return RequestError(requestResult.Error, requestResult.ErrorDescription);
                //验证注册信息并注册
                var registerResult = await _registerValidator.ValidateRegisterAsync(parameters);
                if (registerResult.IsError)
                    return RequestError(registerResult.Error, registerResult.ErrorDescription);
                return new RegisterResult();
            }
            catch (Exception e)
            {
                return RequestError(e.Message);
            }
        }

        /// <summary>
        ///     修改密码
        /// </summary>
        /// <param name="parameter">参数集合</param>
        /// <returns></returns>
        public async Task<IEndpointResult> ChangePassword(NameValueCollection parameter)
        {
            var requestResult = await _changePasswordRequestValidator.ValidateRequestAsync(parameter);
            if (requestResult.IsError)
                return RequestError(requestResult.Error, requestResult.ErrorDescription);
            var changePasswordResult = await _changePasswordValidator.ValidateAsync(parameter);
            if (changePasswordResult.IsError)
                return RequestError(changePasswordResult.Error, changePasswordResult.ErrorDescription);
            return new SuccessResult();
        }

        /// <summary>
        ///     失败响应
        /// </summary>
        /// <param name="error">错误</param>
        /// <param name="errorDescription">错误描述</param>
        /// <returns></returns>
        private static ErrorResult RequestError(string error, string errorDescription = null)
        {
            var response = new ErrorResponse
            {
                Error = error,
                ErrorDescription = errorDescription
            };

            return new ErrorResult(response);
        }
    }
}