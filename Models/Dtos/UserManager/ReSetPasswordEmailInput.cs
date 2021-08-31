using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.UserManager
{
    public class ReSetPasswordEmailInput:EmailInput
    {
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
    }
}