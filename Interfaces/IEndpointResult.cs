using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Interfaces
{
    public interface IEndpointResult
    {
        /// <summary>
        ///     Executes the result.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns></returns>
        Task ExecuteAsync(HttpContext context);
    }
}