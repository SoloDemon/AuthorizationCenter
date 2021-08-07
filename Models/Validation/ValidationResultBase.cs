namespace Models.Validation
{
    /// <summary>
    ///     最小验证结果类(用于更复杂的验证结果的基类)
    /// </summary>
    public class ValidationResultBase
    {
        /// <summary>
        ///     获取或设置一个值，该值指示验证是否成功。
        /// </summary>
        /// <value>
        ///     <c>true</c> 验证失败; 否则, <c>false</c>.
        /// </value>
        public bool IsError { get; set; } = true;

        /// <summary>
        ///     获取或设置错误
        /// </summary>
        /// <value>
        ///     错误
        /// </value>
        public string Error { get; set; }

        /// <summary>
        ///     获取或设置错误描述。
        /// </summary>
        /// <value>
        ///     错误描述。
        /// </value>
        public string ErrorDescription { get; set; }
    }
}