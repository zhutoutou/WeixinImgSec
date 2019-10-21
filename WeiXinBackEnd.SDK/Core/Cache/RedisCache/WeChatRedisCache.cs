using System;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis.Extensions.Core.Abstractions;
using WeiXinBackEnd.SDK.Core.Async;

namespace WeiXinBackEnd.SDK.Core.Cache.RedisCache
{
    public class WeChatRedisCache : IWeChatCache
    {
        private readonly RedisCacheClient _client;

        public WeChatRedisCache(RedisCacheClient client)
        {
            _client = client;
        }

        public T Get<T>(string key) where T : class
        {
            return AsyncHelper.RunSync(async () => await _client.Db0.GetAsync<T>(key));
        }

        public Task<T> GetAsync<T>(string key, CancellationToken cancellationToken) where T : class
        {
            return Task.Run(async () => await _client.Db0.GetAsync<T>(key), cancellationToken);
        }

        public bool Set<T>(string key, T value) where T : class
        {
            return AsyncHelper.RunSync(async () => await _client.Db0.SetAddAsync<T>(key, value));
        }

        public Task<bool> SetAsync<T>(string key, T value, CancellationToken cancellationToken) where T : class
        {
            return Task.Run(async () => await _client.Db0.AddAsync(key,value), cancellationToken);
        }
        
        public T GetOrCreate<T>(string key, Func<T> createFactory) where T : class
        {
            return AsyncHelper.RunSync(async ()=>
            {
                 var value = await _client.Db0.GetAsync<T>(key);
                 if (value == null)
                 {
                     value = createFactory();
                     await SetAsync(key, value,CancellationToken.None);
                 }
                 return value;
            });
        }

        public T GetOrCreate<T>(string key, Func<T> createFactory, DateTimeOffset? offset) where T : class
        {
            return AsyncHelper.RunSync(async () =>
            {
                var value = await _client.Db0.GetAsync<T>(key);
                if (value == null)
                {
                    value = createFactory();
                    await SetAsync(key, value, CancellationToken.None);
                }
                return value;
            });
        }

        public Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory, CancellationToken cancellationToken) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory, DateTimeOffset? offset, CancellationToken cancellationToken) where T : class
        {
            throw new NotImplementedException();
        }
    }
}