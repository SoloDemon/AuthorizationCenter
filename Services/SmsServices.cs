using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using FrameworkCore.Helper.Sms;
using Interfaces;
using Interfaces.Validation;
using Models;
using Newtonsoft.Json;
using Services.Endpoint;

namespace Services
{
    /// <summary>
    ///     短信服务
    /// </summary>
    public class SmsServices : ISmsServices
    {
        private readonly ICachingServices _cachingServices;
        private readonly ISmsHelper _smsHelper;
        private readonly ISmsSendValidator _smsSendValidator;

        public SmsServices(ISmsSendValidator smsSendValidator,
            ICachingServices cachingServices,
            ISmsHelper smsHelper)
        {
            _smsSendValidator = smsSendValidator;
            _cachingServices = cachingServices;
            _smsHelper = smsHelper;
        }

        /// <summary>
        ///     发送短信验证码
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IEndpointResult> SendCaption(NameValueCollection parameters)
        {
            try
            {
                var requestResult = await _smsSendValidator.ValidateRequestAsync(parameters);
                if (requestResult.IsError)
                    return SendCaptionError(requestResult.Error, requestResult.ErrorDescription);
                await SendAsync(parameters["phoneNumber"]);
                return new SmsResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        ///     发送验证码
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <returns></returns>
        private async Task SendAsync(string phoneNumber)
        {
            var captionNumber = await _cachingServices.GetAsync($"{phoneNumber}_CaptionNumber");
            var caption = new Random((int)(DateTime.Now.Ticks - 621356256000000000) / 10000).Next(100000, 999999)
                .ToString();
            await _cachingServices.SetAsync($"{phoneNumber}_CaptionNumber", captionNumber ?? "0",60 * 60 * 24);
            await _smsHelper.SendCaptionAsync(phoneNumber, caption);
            await _cachingServices.SetAsync($"{phoneNumber}_Caption", caption, 60 * 60);
        }


        /// <summary>
        ///     失败响应
        /// </summary>
        /// <param name="error">错误</param>
        /// <param name="errorDescription">错误描述</param>
        /// <returns></returns>
        private static SmsErrorResult SendCaptionError(string error, string errorDescription = null)
        {
            var response = new ErrorResponse
            {
                Error = error,
                ErrorDescription = errorDescription
            };

            return new SmsErrorResult(response);
        }
    }
}