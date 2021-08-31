using System.Collections.Specialized;
using System.Threading.Tasks;
using Models;

namespace Interfaces
{
    /// <summary>
    /// 邮件服务接口
    /// </summary>
    public interface IEmailServices
    {

        /// <summary>
        /// 发送重置密码邮件
        /// </summary>
        /// <param name="parameter">集合参数</param>
        /// <returns></returns>
       Task<ApiResultMessage> SendEmailAsync(NameValueCollection parameter);
    }
}