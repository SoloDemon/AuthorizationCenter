using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Register
{
    /// <summary>
    ///     注册用户输入
    /// </summary>
    public class RegisterInput : BaseRegisterInput
    {
        /// <summary>
        ///     昵称
        /// </summary>
        [Required(ErrorMessage = "昵称不能为空")]
        [Display(Name = "昵称")]
        public string NickName { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        ///     确认密码
        /// </summary>
        [Required(ErrorMessage = "确认密码不能为空")]
        [Compare("Password", ErrorMessage = "两次密码输入不一致")]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     电子邮箱
        /// </summary>
        [Required(ErrorMessage = "电子邮箱不能为空")]
        [Display(Name = "电子邮箱")]
        [EmailAddress(ErrorMessage = "电子邮件地址不正确")]
        public string Email { get; set; }
    }
}