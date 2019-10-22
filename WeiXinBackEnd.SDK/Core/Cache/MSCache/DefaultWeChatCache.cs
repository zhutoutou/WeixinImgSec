using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WeiXinBackEnd.SDK.Core.Async;

namespace WeiXinBackEnd.SDK.Core.Cache.MSCache
{
    public class DefaultWeChatCache : IWeChatCache
    {
        private readonly MemoryCache _cache;
        private readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="logger"></param>
        public DefaultWeChatCache(
            MemoryCache cache,
            ILogger<DefaultWeChatCache> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public TResult Get<TResult>(string key) where TResult : class
        {
            return _cache.Get<TResult>(key);
        }

        public async Task<TResult> GetAsync<TResult>(string key, CancellationToken cancellationToken = default) where TResult : class
        {
            return await Task.Run(() => _cache.Get<TResult>(key), cancellationToken).ConfigureAwait(false);
        }

        public bool Set<TResult>(string key, TResult value, DateTimeOffset? offset = null) where TResult : class
        {
            try
            {
                if (offset.HasValue)
                {
                    _cache.Set(key, value, offset.Value);
                }
                else
                {
                    _cache.Set(key, value);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(WeChatCacheConstants.DefaultSetFailErrorMessage, key));
                return false;
            }
        }

        public async Task<bool> SetAsync<TResult>(string key, TResult value, DateTimeOffset? offset = null, CancellationToken cancellationToken = default) where TResult : class
        {
            return await Task.Run(() => Set(key, value, offset), cancellationToken).ConfigureAwait(false);
        }

        public bool Remove(string key)
        {
            try
            {
                _cache.Remove(key);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, WeChatCacheConstants.DefaultSetFailErrorMessage);
                return false;
            }
        }

        public async Task<bool> RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => Remove(key), cancellationToken);
        }

        public TResult GetOrCreate<TResult>(string key, Func<TResult> createFactory, DateTimeOffset? offset = null) where TResult : class
        {
            return _cache.GetOrCreate(key, entry =>
            {
                var result = createFactory();
                if (offset.HasValue)
                    entry.SetAbsoluteExpiration(offset.Value);
                return result;
            });
        }

        public async Task<TResult> GetOrCreateAsync<TResult>(string key, Func<Task<TResult>> createFactory, DateTimeOffset? offset = null, CancellationToken cancellationToken = default) where TResult : class
        {
            return await _cache.GetOrCreateAsync(key, async entry =>
             {
                 var result = await createFactory().ConfigureAwait(false);
                 if (offset.HasValue)
                     entry.SetAbsoluteExpiration(offset.Value);
                 return result;
             }).ConfigureAwait(false);
        }

        public async Task LockAndOperateAsync(string key, Func<Task> operateFactory, TimeSpan lockTimeSpan = default)
        {
            if (lockTimeSpan == default)
            {
                lockTimeSpan = TimeSpan.FromMilliseconds(WeChatCacheConstants.DefaultLockTimeSpan);
            }
            var lockCancellationToken = new CancellationTokenSource(lockTimeSpan).Token;
            await AsyncHelper.GetLockAsync(key, operateFactory, lockCancellationToken);
        }
    }
}