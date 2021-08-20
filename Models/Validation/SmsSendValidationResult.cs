namespace Models.Validation
{
    /// <summary>
    ///     短信发送验证结果
    /// </summary>
    public class SmsSendValidationResult : ValidationResultBase
    {
        /// <summary>
        ///     失败
        /// </summary>
        /// <param name="error">错误</param>
        /// <param name="errorDescription">错误描述</param>
        public SmsSendValidationResult(string error, string errorDescription) : base(error, errorDescription)
        {
        }

        /// <summary>
        ///     成功
        /// </summary>
        public SmsSendValidationResult()
        {
        }
    }
}