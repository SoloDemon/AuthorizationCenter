using System.Text.RegularExpressions;

namespace FrameworkCore.Validation
{
    public static class ValidationHelper
    {
        /// <summary>
        ///     Email正则规则
        /// </summary>
        public const string EmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        /// <summary>
        ///     手机号正则规则
        /// </summary>
        public const string
            PhoneNumberRegex = @"^1[3456789]\d{9}$"; //^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$

        /// <summary>
        ///     验证邮箱是否正确
        /// </summary>
        /// <param name="value">验证的内容</param>
        public static bool IsEmail(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            var regex = new Regex(EmailRegex);
            return regex.IsMatch(value);
        }

        /// <summary>
        ///     验证手机号是否正确
        /// </summary>
        /// <param name="value">验证的内容</param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string value)
        {
            return !string.IsNullOrEmpty(value) && Regex.IsMatch(value, PhoneNumberRegex);
        }
    }
}