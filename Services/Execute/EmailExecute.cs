using System.Collections.Specialized;
using System.Threading.Tasks;
using FrameworkCore.Helper;
using Interfaces.Execute;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models;
using Models.Options;
using Models.StaticString;

namespace Services.Execute
{
    public class EmailExecute: IEmailExecute
    {
        private readonly EmailHelper _emailHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOption _jwtOption;

        public EmailExecute(EmailHelper emailHelper,
            UserManager<ApplicationUser> userManager,
            IOptions<JwtOption> jwtOption)
        {
            _emailHelper = emailHelper;
            _userManager = userManager;
            _jwtOption = jwtOption.Value;
        }

        /// <summary>
        /// 发送重置密码邮件
        /// </summary>
        /// <param name="parameter">参数集合</param>
        /// <returns></returns>
        public async Task<ApiResultMessage> SendReSetPasswordEmailAsync(NameValueCollection parameter)
        {
            var user = await _userManager.FindByEmailAsync(parameter["email"]);
            var token =await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = $"{parameter["Referer"]}/RestPassword/Email?token={token}&email={parameter["email"]}";
            var content = EmailTemplates.ReSetPassword.Replace("ReSetPasswordUrl", url)
                .Replace("EmailAddress", parameter["email"]);
            //验证注册信息并注册
            var result = _emailHelper.Send(parameter["email"], parameter["subject"], content);
            if (!result)
                return new ApiResultMessage
                {
                    Msg = "发送邮件失败",
                    Status = 200,
                    Success = false
                };
            return new ApiResultMessage
            {
                Msg = "发送邮件成功",
                Status = 200,
                Success = true
            };
        }
    }
}