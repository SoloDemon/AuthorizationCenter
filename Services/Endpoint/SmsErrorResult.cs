using Models;

namespace Services.Endpoint
{
    public class SmsErrorResult : ErrorResult
    {
        public SmsErrorResult(ErrorResponse response) : base(response)
        {
        }
    }
}