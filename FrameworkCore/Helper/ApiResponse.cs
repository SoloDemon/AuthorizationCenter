using System;
using Models;

namespace FrameworkCore.Helper
{
    public class ApiResponse
    {
        public ApiResultMessage MessageModel;

        public ApiResponse(StatusCode apiCode, string msg = null)
        {
            switch (apiCode)
            {
                case StatusCode.Code401:
                {
                    Status = 401;
                    Value = "很抱歉，您无权访问该接口，请确保已经登录!";
                }
                    break;
                case StatusCode.Code403:
                {
                    Status = 403;
                    Value = "很抱歉，您的访问权限等级不够，联系管理员!";
                }
                    break;
                case StatusCode.Code500:
                {
                    Status = 500;
                    Value = msg;
                }
                    break;
                case StatusCode.Code404:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(apiCode), apiCode, null);
            }

            MessageModel = new ApiResultMessage
            {
                Status = Status,
                Msg = Value,
                Success = false
            };
        }

        public int Status { get; set; } = 404;
        public string Value { get; set; } = "No Found";
    }

    public enum StatusCode
    {
        Code401,
        Code403,
        Code404,
        Code500
    }
}