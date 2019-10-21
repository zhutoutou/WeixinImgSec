using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

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

        public T Get<T>(string key) where T : class
        {
            return _cache.Get<T>(key);
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken) where T : class
        {
            return await Task.Run(() => _cache.Get<T>(key), cancellationToken).ConfigureAwait(false);
        }

        public bool Set<T>(string key, T value) where T : class
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

        public async Task<bool> SetAsync<T>(string key, T value, CancellationToken cancellationToken) where T : class
        {
            return await Task.Run(() => Set(key, value), cancellationToken).ConfigureAwait(false);
        }

        public T GetOrCreate<T>(string key, Func<T> createFactory) where T : class
        {
            return GetOrCreate(key, createFactory, null);
        }

        public T GetOrCreate<T>(string key, Func<T> createFactory, DateTimeOffset? offset) where T : class
        {
            return _cache.GetOrCreate(key, entry =>
            {
                var result = createFactory();
                if (offset.HasValue)
                    entry.SetAbsoluteExpiration(offset.Value);
                return result;
            });
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory, CancellationToken cancellationToken) where T : class
        {
            return await GetOrCreateAsync(key, createFactory, null, cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> createFactory, DateTimeOffset? offset, CancellationToken cancellationToken) where T : class
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