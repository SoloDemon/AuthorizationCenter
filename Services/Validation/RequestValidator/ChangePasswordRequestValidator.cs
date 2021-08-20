using System.Collections.Specialized;
using System.Threading.Tasks;
using Interfaces.Validation.RequestValidator;
using Models.Validation;

namespace Services.Validation.RequestValidator
{
    public class ChangePasswordRequestValidator : IChangePasswordRequestValidator
    {
        /// <summary>
        ///     验证请求
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<ValidationResultBase> ValidateRequestAsync(NameValueCollection parameters)
        {
            var userId = parameters["userId"];
            return userId == null ? new ValidationResultBase("请登陆后修改密码。", "获取用户编号失败") : new ValidationResultBase();
        }
    }
}