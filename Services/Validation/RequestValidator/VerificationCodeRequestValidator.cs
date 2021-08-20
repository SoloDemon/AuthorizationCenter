using System.Threading.Tasks;
using Interfaces;
using Interfaces.Validation.RequestValidator;

namespace Services.Validation.RequestValidator
{
    public class VerificationCodeRequestValidator : IVerificationCodeRequestValidator
    {
        private readonly ICachingServices _cachingServices;

        public VerificationCodeRequestValidator(ICachingServices cachingServices)
        {
            _cachingServices = cachingServices;
        }

        /// <summary>
        ///     验证码验证
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="captcha"></param>
        /// <returns></returns>
        public async Task<bool> SmsVerificationCodeValidateRequest(string phoneNumber, string captcha)
        {
            var captionCache = await _cachingServices.GetAsync($"{phoneNumber}_Caption");
            if (captcha != captionCache)
                return false;
            //删除验证码缓存
            await _cachingServices.DeleteAsync($"{phoneNumber}_Caption");
            return true;
        }
    }
}