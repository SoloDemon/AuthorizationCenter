namespace Models
{
    /// <summary>
    /// 验证模型
    /// </summary>
    public class CaptionModel
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsActive { get; set; }
    }
}