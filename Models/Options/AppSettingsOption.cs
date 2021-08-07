namespace Models.Options
{
    public class AppSettingsOption
    {
        /// <summary>
        ///     Redis缓存是否开启
        /// </summary>
        public bool RedisCaching { get; set; }

        /// <summary>
        ///     Memory缓存是否开启
        /// </summary>
        public bool MemoryCaching { get; set; }
    }
}