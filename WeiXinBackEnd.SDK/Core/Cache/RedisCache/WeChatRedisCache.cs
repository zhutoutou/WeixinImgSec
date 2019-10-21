using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeiXinBackEnd.SDK.Core.Cache.RedisCache
{
    public class WeChatRedisCache:IWeChatCache
    {
        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync<T>(string key, T value, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public T GetOrCreate<T>(string key, Func<T> createFactory)
        {
            throw new NotImplementedException();
        }

        public T GetOrCreate<T>(string key, Func<T> createFactory, DateTimeOffset? offset)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory, DateTimeOffset? offset, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}