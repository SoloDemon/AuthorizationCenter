using System.ComponentModel.DataAnnotations;

namespace Models.Dtos
{
    /// <summary>
    /// 电邮邮箱输入
    /// </summary>
    public class EmailInput
    {
        /// <summary>
        ///     电子邮箱
        /// </summary>
        [Required(ErrorMessage = "电子邮箱不能为空")]
        [Display(Name = "电子邮箱")]
        [EmailAddress(ErrorMessage = "电子邮件地址不正确")]
        public string Email { get; set; }
    }
}