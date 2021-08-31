namespace Models.Validation
{
    /// <summary>
    ///     注册验证结果
    /// </summary>
    public class RegisterValidationResult : ValidationRequestResult
    {
        /// <summary>
        ///     失败
        /// </summary>
        /// <param name="error">错误</param>
        /// <param name="errorDescription">错误描述</param>
        public RegisterValidationResult(string error, string errorDescription) : base(error, errorDescription)
        {
        }

        /// <summary>
        ///     成功
        /// </summary>
        public RegisterValidationResult()
        {
        }
    }
}