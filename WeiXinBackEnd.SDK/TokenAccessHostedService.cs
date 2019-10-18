using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeiXinBackEnd.SDK.Client;
using WeiXinBackEnd.SDK.Client.Message.RefreshToken;
using WeiXinBackEnd.SDK.Configuration;
using WeiXinBackEnd.SDK.Core.Async;
using WeiXinBackEnd.SDK.Core.Cache;

namespace WeiXinBackEnd.SDK
{
    /// <summary>
    /// Token刷新后台服务
    /// </summary>
    public sealed class TokenAccessHostedService : IHostedService, IDisposable
    {

        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IWeChatClient _weChatClient;
        private readonly WeChatConfiguration _config;
        private readonly IWeChatCache _cache;
        private readonly WeChatAsyncEx _asyncEx;
        private readonly int _allowRefreshTimeOffset;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="weChatClient"></param>
        /// <param name="config"></param>
        /// <param name="cache"></param>
        /// <param name="asyncEx"></param>
        public TokenAccessHostedService(
            ILogger<TokenAccessHostedService> logger,
            IWeChatClient weChatClient,
            WeChatConfiguration config,
            IWeChatCache cache,
            WeChatAsyncEx asyncEx)
        {
            _logger = logger;
            _weChatClient = weChatClient;
            _config = config;
            _cache = cache;
            _asyncEx = asyncEx;
            // 刷新时间剩余不足 1/5 时运行刷新
            _allowRefreshTimeOffset = _config.RefreshTimeSpan * 60 / 5 * 1;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TokenAccess Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(_config.RefreshTimeSpan));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            _logger.LogInformation("TokenAccess Background Service is working.");
            CancellationToken lockToken = new CancellationTokenSource(TimeSpan.FromSeconds(2)).Token;
            await _asyncEx.GetLockAsync(WeChatRefreshTokenConstants.AccessTokenCacheKey, async () =>
            {
                CancellationToken cacheToken = new CancellationTokenSource(TimeSpan.FromMilliseconds(500)).Token;
                var entry = await _cache.GetAsync<WeChatCacheEntry<string>>(
                    WeChatRefreshTokenConstants.AccessTokenCacheKey, cacheToken);
                if ((entry.GetExpireOffset?.TotalMilliseconds ?? 0) < _allowRefreshTimeOffset)
                {
                    var result = await _weChatClient.RequestRefreshTokenAsync(new WeChatRefreshTokenInput(), new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token).ConfigureAwait(false);
                    if (result.IsError)
                    {
                        _logger.LogError(result.Exception, $"刷新Token失败{result.Error}");
                    }
                    else
                    {
                        await _cache.SetAsync(WeChatRefreshTokenConstants.AccessTokenCacheKey, new WeChatCacheEntry<string>()
                        {
                            Value = result.Result.AccessToken,
                            ExpireTime = DateTime.Now.Add(TimeSpan.FromMinutes(_config.RefreshTimeSpan)),
                        }, cacheToken);
                        _logger.LogInformation(WeChatRefreshTokenConstants.RefreshAccessTokenCacheSuccess);
                    }
                }
                else
                {
                    _logger.LogInformation(WeChatRefreshTokenConstants.RefreshAccessTokenCacheCancel);
                }

            }, lockToken);

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TokenAccess Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}