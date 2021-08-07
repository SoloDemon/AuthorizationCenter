using System.Threading.Tasks;

namespace FrameworkCore.Helper.Sms
{
    /// <summary>
    ///     短信服务接口
    /// </summary>
    public interface ISmsHelper
    {
        /// <summary>
        ///     发送验证码
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="caption">验证码</param>
        public void SendCaption(string phoneNumber, string caption);

        /// <summary>
        ///     异步发送验证码
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="caption">验证码</param>
        public Task SendCaptionAsync(string phoneNumber, string caption);
    }
}