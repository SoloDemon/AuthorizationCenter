using System.Threading.Tasks;

namespace Interfaces.Validation.RequestValidator
{
    public interface IVerificationCodeRequestValidator
    {
        /// <summary>
        ///     验证码验证
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="captcha">验证码</param>
        /// <returns></returns>
        Task<bool> SmsVerificationCodeValidateRequest(string phoneNumber, string captcha);
    }
}