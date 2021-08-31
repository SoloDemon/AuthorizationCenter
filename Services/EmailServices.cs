using FrameworkCore.Helper;
using Interfaces;
using Interfaces.Execute;
using Interfaces.Validation.RequestValidator;
using Models;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Services
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailHelper _emailHelper;
        private readonly IEmailRequestValidator _emailRequestValidator;
        private readonly IEmailExecute _emailExecute;

        public EmailServices(EmailHelper emailHelper,
            IEmailRequestValidator emailRequestValidator,
            IEmailExecute emailExecute)
        {
            _emailHelper = emailHelper;
            _emailRequestValidator = emailRequestValidator;
            _emailExecute = emailExecute;
        }

        /// <summary>
        /// 发送重置密码邮件
        /// </summary>
        /// <param name="parameter">集合参数</param>
        /// <returns></returns>
        public async Task<ApiResultMessage> SendEmailAsync(NameValueCollection parameter)
        {
            //验证注册请求
            var requestResult = await _emailRequestValidator.ValidateRequestAsync(parameter);
            if (requestResult.IsError)
                return new ApiResultMessage
                {
                    Msg = requestResult.Error,
                    Status = 200,
                    Success = false
                };
            return await _emailExecute.SendReSetPasswordEmailAsync(parameter);

        }
    }
}
