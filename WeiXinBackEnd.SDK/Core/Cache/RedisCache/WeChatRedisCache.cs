using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using WeiXinBackEnd.SDK.Core.Async;

namespace WeiXinBackEnd.SDK.Core.Cache.RedisCache
{
    public class WeChatRedisCache : IWeChatCache
    {
        private readonly RedisCacheClient _client;
        private readonly ILogger _logger;
        private TimeSpan _retryInterval = TimeSpan.FromMilliseconds(200);

        public WeChatRedisCache(
            RedisCacheClient client,
            ILogger<WeChatRedisCache> logger)
        {
            _client = client;
            _logger = logger;
        }

        public TResult Get<TResult>(string key) where TResult : class
        {
            return AsyncHelper.RunSync(async () => await _client.Db0.GetAsync<TResult>(key));
        }

        public async Task<TResult> GetAsync<TResult>(string key, CancellationToken cancellationToken = default) where TResult : class
        {
            return await Task.Run(async () => await _client.Db0.GetAsync<TResult>(key), cancellationToken);
        }

        public bool Set<TResult>(string key, TResult value, DateTimeOffset? offset = null) where TResult : class
        {
            return AsyncHelper.RunSync(async () => await _client.Db0.SetAddAsync(key, value));
        }

        public async Task<bool> SetAsync<TResult>(string key, TResult value, DateTimeOffset? offset = null, CancellationToken cancellationToken = default) where TResult : class
        {
            return await Task.Run(async () => await _client.Db0.AddAsync(key, value), cancellationToken);
        }

        public bool Remove(string key)
        {
            return AsyncHelper.RunSync(async () => await _client.Db0.RemoveAsync(key));
        }

        public async Task<bool> RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            return await Task.Run(async
                () => await _client.Db0.RemoveAsync(key), cancellationToken);
        }

        public TResult GetOrCreate<TResult>(string key, Func<TResult> createFactory, DateTimeOffset? offset = null) where TResult : class
        {
            return AsyncHelper.RunSync(async () =>
            {
                var value = await _client.Db0.GetAsync<TResult>(key);
                if (value == null)
                {
                    value = createFactory();
                    await SetAsync(key, value, offset, CancellationToken.None);
                }
                return value;
            });
        }

        public async Task<TResult> GetOrCreateAsync<TResult>(string key, Func<Task<TResult>> createFactory, DateTimeOffset? offset = null, CancellationToken cancellationToken = default) where TResult : class
        {
            return await Task.Run(async () =>
            {
                var value = await _client.Db0.GetAsync<TResult>(key);
                if (value == null)
                {
                    value = await createFactory();
                    await SetAsync(key, value, offset, CancellationToken.None);
                }

                return value;
            }, cancellationToken);

        }

        public async Task LockAndOperateAsync(string key, Func<Task> operateFactory, TimeSpan lockTimeSpan = default)
        {
            if (lockTimeSpan == default)
            {
                lockTimeSpan = TimeSpan.FromMilliseconds(WeChatCacheConstants.DefaultLockTimeSpan);
            }
            var count = 0;
            var tryMaxCount = (int)(lockTimeSpan.TotalMilliseconds / _retryInterval.TotalMilliseconds);
            RedisValue token = Environment.MachineName;

            while (count < tryMaxCount)
            {
                if (!await _client.Db0.Database.LockTakeAsync(key, token, lockTimeSpan))
                {
                    count++;
                    await Task.Delay(_retryInterval);
                    continue;
                }
                try
                {
                    await operateFactory();
                }
                finally
                {
                    await _client.Db0.Database.LockReleaseAsync(key, token);
                }

                return;
            }
        }
    }
}