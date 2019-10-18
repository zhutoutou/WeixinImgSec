using System;

namespace WeiXinBackEnd.SDK.Core.Cache
{
    /// <summary>
    /// MsCache实体装饰器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WeChatCacheEntry<T>
    {
        public DateTime? ExpireTime { get; set; }

        public T Value { get; set; }

        public TimeSpan? GetExpireOffset => ExpireTime?.Subtract(DateTime.Now);
    }
}