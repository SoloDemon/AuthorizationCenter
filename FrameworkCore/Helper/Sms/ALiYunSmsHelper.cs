using System;
using System.Threading.Tasks;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using Microsoft.Extensions.Options;
using Models.Options;

namespace FrameworkCore.Helper.Sms
{
    public class ALiYunSmsHelper : ISmsHelper
    {
        private readonly ALiYunSmsOption _smsOptions;

        public ALiYunSmsHelper(IOptions<ALiYunSmsOption> smsOptions)
        {
            _smsOptions = smsOptions.Value;
        }

        /// <summary>
        ///     发送验证码
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="caption">验证码</param>
        public void SendCaption(string phoneNumber, string caption)
        {
            var smsConfig = new SmsConfig
            {
                TemplateCode = "SMS_133080063",
                TemplateParam = "{\"code\":\"" + caption + "\"}",
                PhoneNumbers = phoneNumber
            };
            //发送短信
            SendCodeSms(smsConfig);
        }

        /// <summary>
        ///     异步发送验证码
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="caption">验证码</param>
        public Task SendCaptionAsync(string phoneNumber, string caption)
        {
            SendCaption(phoneNumber, caption);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     发送短信
        /// </summary>
        /// <param name="phoneNumber"></param>
        public void Send(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     异步发送短信
        /// </summary>
        /// <param name="phoneNumber"></param>
        public void SendAsync(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     创建发送客户端
        /// </summary>
        /// <returns></returns>
        private IAcsClient BuidClient()
        {
            IClientProfile profile = DefaultProfile.GetProfile(_smsOptions.Endpoint, _smsOptions.AccessKeyId,
                _smsOptions.AccessKeySecret);
            return new DefaultAcsClient(profile);
        }

        /// <summary>
        ///     发送短信
        /// </summary>
        /// <param name="smsConfig">短信配置</param>
        private void SendCodeSms(SmsConfig smsConfig)
        {
            try
            {
                var acsClient = BuidClient();
                var request = new CommonRequest
                {
                    Method = MethodType.POST,
                    Domain = _smsOptions.Domain,
                    Version = _smsOptions.Version,
                    Action = "SendSms"
                };
                request.AddQueryParameters("PhoneNumbers", smsConfig.PhoneNumbers);
                request.AddQueryParameters("SignName", _smsOptions.SignName);
                request.AddQueryParameters("TemplateCode", smsConfig.TemplateCode);
                request.AddQueryParameters("TemplateParam", smsConfig.TemplateParam);
                //TODO 正式使用请把这里注释取消,这里是短信发送
                //CommonResponse response = acsClient.GetCommonResponse(request);
                //Console.WriteLine(System.Text.Encoding.Default.GetString(response.HttpResponse.Content));
            }
            catch (ServerException e)
            {
                throw new Exception($"to:{smsConfig.PhoneNumbers},result.errMsg:{e.Message}");
            }
            catch (ClientException e)
            {
                throw new Exception($"to:{smsConfig.PhoneNumbers},result.errMsg:{e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"发送短信失败:{e.Message}");
            }
        }

        /// <summary>
        ///     异步发送短信
        /// </summary>
        /// <param name="smsConfig">短信配置</param>
        private Task SendCodeSmsAsync(SmsConfig smsConfig)
        {
            SendCodeSms(smsConfig);
            return Task.CompletedTask;
        }
    }
}