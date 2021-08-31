using System.Collections.Specialized;
using System.Threading.Tasks;
using FrameworkCore.Helper;
using FrameworkCore.Validation;
using Interfaces;
using Interfaces.Validation;
using Models.Validation;

namespace Services.Validation
{
    public class SmsSendValidator : ISmsSendValidator
    {
        private readonly ICachingServices _cachingServices;

        public SmsSendValidator(ICachingServices cachingServices)
        {
            _cachingServices = cachingServices;
        }

        public async Task<SmsSendValidationResult> ValidateRequestAsync(NameValueCollection parameters)
        {
            var phoneNumber = parameters["phoneNumber"];
            if (phoneNumber == "" || string.IsNullOrEmpty(phoneNumber))
                return new SmsSendValidationResult("手机号为空", "手机号不能为空");
            if (!ValidationHelper.IsPhoneNumber(phoneNumber))
                return new SmsSendValidationResult($"手机号{phoneNumber},格式不正确", $"手机号{phoneNumber},格式不正确");
            if (!await _cachingServices.ExistAsync($"{phoneNumber}_CaptionNumber"))
                return new SmsSendValidationResult();
            var captionNumber = int.Parse(await _cachingServices.GetAsync($"{phoneNumber}_CaptionNumber"));
            return captionNumber < 3
                ? new SmsSendValidationResult()
                : new SmsSendValidationResult("短信发送次数超过", $"手机号:{phoneNumber}短信发送次数超过3次!无法发送短信验证码！");
        }
    }
}