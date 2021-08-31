namespace Models.Validation
{
    /// <summary>
    ///     注册用户请求验证结果
    /// </summary>
    public class RegisterRequestValidationResult : ValidationRequestResult
    {
        /// <summary>
        ///     失败
        /// </summary>
        /// <param name="error">错误</param>
        /// <param name="errorDescription">错误描述</param>
        public RegisterRequestValidationResult(string error, string errorDescription) : base(error, errorDescription)
        {
        }

        /// <summary>
        ///     成功
        /// </summary>
        public RegisterRequestValidationResult()
        {
        }
    }
}