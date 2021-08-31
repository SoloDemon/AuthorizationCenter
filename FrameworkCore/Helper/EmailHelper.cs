using Microsoft.Extensions.Options;
using MimeKit;
using Models.Options;
using System;
using System.Text;
using MailKit.Net.Smtp;

namespace FrameworkCore.Helper
{
    /// <summary>
    /// 电子邮件助手类
    /// </summary>
    public class EmailHelper
    {
        private readonly EmailConfigOption _emailConfigOption;

        public EmailHelper(IOptions<EmailConfigOption> emailConfigOption)
        {
            _emailConfigOption = emailConfigOption.Value;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="emailAddress">接收邮件地址</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public bool Send(string emailAddress, string subject, string content)
        {
            try
            {
                var message = new MimeMessage
                {
                    From = { new MailboxAddress(Encoding.UTF8, "饥荒管理控制系统", _emailConfigOption.UserName) }
                };
                message.To.Add(new MailboxAddress(Encoding.UTF8, "饥荒管理控制系统", emailAddress));
                message.Subject = subject;
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = content
                };
                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient
                {
                    ServerCertificateValidationCallback = (_, _, _, _) => true
                };
                client.Connect(_emailConfigOption.SmtpHost, 465, true);
                client.Authenticate(_emailConfigOption.UserName, _emailConfigOption.Password);
                client.Send(message);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}