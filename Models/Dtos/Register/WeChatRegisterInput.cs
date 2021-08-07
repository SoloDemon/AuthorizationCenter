using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Register
{
    public class WeChatRegisterInput : BaseRegisterInput
    {
        /// <summary>
        ///     手机号
        /// </summary>
        [Required(ErrorMessage = "请授权获取手机号")]
        [RegularExpression(@"^1[3456789]\d{9}$", ErrorMessage = "手机号格式不正确")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     微信OpenId
        /// </summary>
        [Required(ErrorMessage = "openId获取失败")]
        public string WeChatOpenId { get; set; }

        /// <summary>
        ///     省份
        /// </summary>
        [Required(ErrorMessage = "省份获取失败")]
        public string Province { get; set; }

        /// <summary>
        ///     城市
        /// </summary>
        [Required(ErrorMessage = "城市获取失败")]
        public string City { get; set; }

        /// <summary>
        ///     国家
        /// </summary>
        [Required(ErrorMessage = "国家获取失败")]
        public string Country { get; set; }

        /// <summary>
        ///     昵称
        /// </summary>
        [Required(ErrorMessage = "昵称获取失败")]
        public string NickName { get; set; }

        /// <summary>
        ///     性别 0:未知 1:男 2:女
        /// </summary>
        [Required(ErrorMessage = "头像获取失败")]
        public byte Gender { get; set; }

        /// <summary>
        ///     头像
        /// </summary>
        [Required(ErrorMessage = "头像获取失败")]
        public string Portrait { get; set; }
    }
}