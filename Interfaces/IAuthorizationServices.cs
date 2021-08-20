using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IAuthorizationServices
    {
        Task<IEndpointResult> LoginAsync(NameValueCollection queryList);
    }
}