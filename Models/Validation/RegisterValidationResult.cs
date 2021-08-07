namespace Models.Validation
{
    /// <summary>
    ///     注册验证结果
    /// </summary>
    public class RegisterValidationResult : ValidationResultBase
    {
        /// <summary>
        ///     失败
        /// </summary>
        /// <param name="error">错误</param>
        /// <param name="errorDescription">错误描述</param>
        public RegisterValidationResult(string error, string errorDescription)
        {
            IsError = true;
            Error = error;
            ErrorDescription = errorDescription;
        }

        /// <summary>
        ///     成功
        /// </summary>
        public RegisterValidationResult()
        {
            IsError = false;
        }
    }
}