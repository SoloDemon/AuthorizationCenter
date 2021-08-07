using System.Threading.Tasks;
using Models.Validation;

namespace Interfaces.Validation
{
    /// <summary>
    ///     使用自定义授权类型处理令牌请求的验证
    /// </summary>
    public interface IExtensionGrantValidator
    {
        /// <summary>
        ///     返回此验证器可以处理的授权类型
        /// </summary>
        /// <value>
        ///     验证器类型
        /// </value>
        string GrantType { get; }

        /// <summary>
        ///     验证自定义授权请求
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        ///     A principal
        /// </returns>
        Task ValidateAsync(ExtensionGrantValidationContext context);
    }
}