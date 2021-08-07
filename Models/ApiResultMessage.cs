namespace Models
{
    /// <summary>
    ///     通用返回信息类
    /// </summary>
    public class ApiResultMessage
    {
        /// <summary>
        ///     状态码
        /// </summary>
        public int Status { get; set; } = 200;

        /// <summary>
        ///     操作是否成功
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        ///     返回信息
        /// </summary>
        public string Msg { get; set; } = "服务器异常";
    }

    /// <summary>
    ///     通用返回信息类
    /// </summary>
    public class ApiResultMessage<T>
    {
        /// <summary>
        ///     状态码
        /// </summary>
        public int Status { get; set; } = 200;

        /// <summary>
        ///     操作是否成功
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        ///     返回信息
        /// </summary>
        public string Msg { get; set; } = "服务器异常";

        /// <summary>
        ///     返回数据对象
        /// </summary>
        public T Response { get; set; }
    }
}