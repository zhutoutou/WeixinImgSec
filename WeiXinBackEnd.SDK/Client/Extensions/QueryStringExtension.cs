using System.Collections.Generic;
using System.Linq;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    public static class QueryStringExtension
    {
        public static string ToQueryString(this IDictionary<string, string> parameters)
        {
            return string.Join("&", parameters.Select(v => $"{v.Key}={v.Value}"));
        }
    }
}