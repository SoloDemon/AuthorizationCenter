using Models;

namespace Services.Endpoint
{
    public class RegisterErrorResult : ErrorResult
    {
        public RegisterErrorResult(ErrorResponse response) : base(response)
        {
        }
    }
}