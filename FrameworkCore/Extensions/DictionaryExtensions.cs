using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace FrameworkCore.Extensions
{
    /// <summary>
    ///     字典扩展
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     名称值集合
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns></returns>
        public static NameValueCollection AsNameValueCollection(this IDictionary<string, StringValues> collection)
        {
            var nv = new NameValueCollection();
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            foreach (var (key, value) in collection)
                nv.Add(key, value.First());

            return nv;
        }
    }
}