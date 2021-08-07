using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Register
{
    public class PhoneNumberInput : BaseRegisterInput
    {
        /// <summary>
        ///     手机号
        /// </summary>
        [Required(ErrorMessage = "请输入手机号")]
        [RegularExpression(@"^1[3456789]\d{9}$", ErrorMessage = "手机号格式不正确")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     验证码
        /// </summary>
        [Required(ErrorMessage = "请输入验证码")]
        [MinLength(5, ErrorMessage = "验证码不正确")]
        [MaxLength(6, ErrorMessage = "验证码不正确")]
        public string Caption { get; set; }
    }
}