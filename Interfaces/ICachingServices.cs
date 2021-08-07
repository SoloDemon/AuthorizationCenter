using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICachingServices
    {
        /// <summary>
        ///     获取缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public T Get<T>(string key);

        /// <summary>
        ///     获取缓存异步
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public Task<T> GetAsync<T>(string key);

        /// <summary>
        ///     获取缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public string Get(string key);

        /// <summary>
        ///     获取缓存异步
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public Task<string> GetAsync(string key);

        /// <summary>
        ///     删除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public bool Delete(string key);

        /// <summary>
        ///     删除缓存异步
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(string key);

        /// <summary>
        ///     缓存是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public bool Exist(string key);

        /// <summary>
        ///     缓存是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public Task<bool> ExistAsync(string key);

        /// <summary>
        ///     添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        public bool Set(string key, string value);

        /// <summary>
        ///     添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        public Task<bool> SetAsync(string key, string value);

        /// <summary>
        ///     添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expirationTime">过期时间</param>
        /// <param name="isSlidingExpiration">滑动过期时间，3秒后,即三秒钟内被访问，则重新刷新缓存时间为3秒后</param>
        /// <returns></returns>
        public bool Set(string key, string value, int expirationTime, bool isSlidingExpiration = false);

        /// <summary>
        ///     添加缓存异步
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expirationTime">过期时间</param>
        /// <param name="isSlidingExpiration">滑动过期时间，3秒后,即三秒钟内被访问，则重新刷新缓存时间为3秒后</param>
        /// <returns></returns>
        public Task<bool> SetAsync(string key, string value, int expirationTime, bool isSlidingExpiration = false);
    }
}