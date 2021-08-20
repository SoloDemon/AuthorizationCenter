namespace Models
{
    /// <summary>
    ///     授权类型
    /// </summary>
    public class GrantType
    {
        /// <summary>
        ///     GrantType - 自定义微信授权
        /// </summary>
        public const string ResourceWeChat = "WeChat";

        /// <summary>
        ///     默认授权
        /// </summary>
        public const string ResourceDefault = "Default";

        /// <summary>
        ///     GrantType - 自定义手机验证码授权
        /// </summary>
        public const string ResourcePhoneCaption = "PhoneCaption";

        /// <summary>
        ///     GrantType - 自定义手机号授权
        /// </summary>
        public const string ResourcePhoneNumber = "PhoneNumber";
    }
}