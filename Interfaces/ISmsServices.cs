using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Interfaces
{
    /// <summary>
    ///     短信服务接口
    /// </summary>
    public interface ISmsServices
    {
        /// <summary>
        ///     发送短信验证码
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <returns></returns>
        public Task<IEndpointResult> SendCaption(NameValueCollection parameters);
    }
}