using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Nito.AsyncEx;

namespace WeiXinBackEnd.SDK.Core.Cache.MSCache
{
    public class DefaultWeChatCache : IWeChatCache
    {
        private readonly MemoryCache _cache;
        private readonly ILogger _logger;


        public DefaultWeChatCache(
            MemoryCache cache,
            ILogger<DefaultWeChatCache> logger,
            ConcurrentDictionary<string, AsyncLock> mutexDictionary)
        {
            _cache = cache;
            _logger = logger;
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _cache.Get<T>(key), cancellationToken).ConfigureAwait(false);
        }

        public bool Set<T>(string key, T value)
        {
            try
            {
                _cache.Set(key, value);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, DefaultWeChatCacheConstants.DefaultSetFailErrorMessage);
                return false;
            }
        }

        public async Task<bool> SetAsync<T>(string key, T value, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Set(key, value), cancellationToken).ConfigureAwait(false);
        }

        public T GetOrCreate<T>(string key, Func<T> createFactory)
        {
            return GetOrCreate(key, createFactory, null);
        }

        public T GetOrCreate<T>(string key, Func<T> createFactory, DateTimeOffset? offset)
        {
            return _cache.GetOrCreate(key, entry =>
            {
                var result = createFactory();
                if (offset.HasValue)
                    entry.SetAbsoluteExpiration(offset.Value);
                return result;
            });
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory, CancellationToken cancellationToken)
        {
            return await GetOrCreateAsync(key, createFactory, null, cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory, DateTimeOffset? offset, CancellationToken cancellationToken)
        {
            return await _cache.GetOrCreateAsync(key, async entry =>
             {
                 var result = await createFactory(cancellationToken).ConfigureAwait(false);
                 if (offset.HasValue)
                    entry.SetAbsoluteExpiration(offset.Value);
                 return result;
             }).ConfigureAwait(false);
        }

        
    }
}