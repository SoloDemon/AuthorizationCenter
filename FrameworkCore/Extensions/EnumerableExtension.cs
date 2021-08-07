using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace FrameworkCore.Extensions
{
    public static class EnumerableExtension
    {
        public static NameValueCollection AsNameValueCollection(
            this IEnumerable<KeyValuePair<string, StringValues>> collection)
        {
            var nv = new NameValueCollection();

            foreach (var field in collection)
                nv.Add(field.Key, field.Value.First());

            return nv;
        }
    }
}