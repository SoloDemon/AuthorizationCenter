using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace FrameworkCore.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     指示此字符串是null还是System.String。空字符串。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static NameValueCollection AsNameValueCollection(this string str)
        {
            var obj = JsonConvert.DeserializeObject<IDictionary<string, StringValues>>(str);
            return obj.AsNameValueCollection();
        }

        #region UrlOpertion

        /// <summary>
        ///     是否本地地址
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns></returns>
        public static bool IsLocalUrl(this string url)
        {
            if (string.IsNullOrEmpty(url)) return false;

            // Allows "/" or "/foo" but not "//" or "/\".
            if (url[0] == '/')
            {
                // url is exactly "/"
                if (url.Length == 1) return true;

                // url doesn't start with "//" or "/\"
                return url[1] != '/' && url[1] != '\\';
            }

            // Allows "~/" or "~/foo" but not "~//" or "~/\".
            if (url[0] != '~' || url.Length <= 1 || url[1] != '/') return false;
            // url is exactly "~/"
            if (url.Length == 2) return true;

            // url doesn't start with "~//" or "~/\"
            return url[2] != '/' && url[2] != '\\';
        }

        /// <summary>
        ///     读取查询字符串作为名称值集合
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static NameValueCollection ReadQueryStringAsNameValueCollection(this string url)
        {
            if (url != null)
            {
                var idx = url.IndexOf('?');
                if (idx >= 0) url = url.Substring(idx + 1);
                var query = QueryHelpers.ParseNullableQuery(url);
                if (query != null) return query.AsNameValueCollection();
            }

            return new NameValueCollection();
        }

        #endregion
    }
}