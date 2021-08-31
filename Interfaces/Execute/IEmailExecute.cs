using System.Collections.Specialized;
using System.Threading.Tasks;
using Models;

namespace Interfaces.Execute
{
    public interface IEmailExecute
    {

        /// <summary>
        /// 发送重置密码邮件
        /// </summary>
        /// <param name="parameter">参数集合</param>
        /// <returns></returns>
        Task<ApiResultMessage> SendReSetPasswordEmailAsync(NameValueCollection parameter);
    }
}