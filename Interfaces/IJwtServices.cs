using System.Threading.Tasks;
using Models;
using Models.Validation;

namespace Interfaces
{
    public interface IJwtServices
    {
        /// <summary>
        ///     获取Token
        /// </summary>
        /// <param name="tokenRequestValidationResult">token请求验证结果</param>
        /// <returns></returns>
        Task<TokenResponse> GetTokenAsync(TokenRequestValidationResult tokenRequestValidationResult);
    }
}