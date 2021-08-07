namespace Models.Options
{
    public class KestrelOption
    {
        /// <summary>
        ///     api监听地址
        /// </summary>
        public int ApiPort { get; set; }

        /// <summary>
        ///     Grpc监听地址
        /// </summary>
        public int GrpcPort { get; set; }
    }
}