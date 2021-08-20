namespace Models.Dtos.UserManager
{
    /// <summary>
    ///     修改密码输入
    /// </summary>
    public class ChangePasswordInput
    {
        /// <summary>
        ///     老密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        ///     新密码
        /// </summary>
        public string NewPassword { get; set; }
    }
}