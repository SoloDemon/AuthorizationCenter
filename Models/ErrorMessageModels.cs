namespace Models
{
    /// <summary>
    ///     错误信息模型
    /// </summary>
    public class ErrorMessageModels
    {
        /// <summary>
        ///     错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     内部异常信息
        /// </summary>
        public string InnerException { get; set; }
    }
}