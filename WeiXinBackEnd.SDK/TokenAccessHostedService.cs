using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeiXinBackEnd.SDK.Client;
using WeiXinBackEnd.SDK.Client.Message.RefreshToken;
using WeiXinBackEnd.SDK.Configuration;
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
        private readonly WeChatOptions _config;
        private readonly IWeChatCache _cacheManager;
        private readonly int _expireTime;
        private readonly int _allowRefreshTimeOffset;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="weChatClient"></param>
        /// <param name="config"></param>
        /// <param name="cache"></param>
        public TokenAccessHostedService(
            ILogger<TokenAccessHostedService> logger,
            IWeChatClient weChatClient,
            WeChatOptions config,
            IWeChatCache cache)
        {
            _logger = logger;
            _weChatClient = weChatClient;
            _config = config;
            _cacheManager = cache;

            // 过期策略 1/4过期生效，1/5刷新间隔 刷新必定命中过期片段
            _expireTime = _config.RefreshTimeSpan * 60 * 5;
            _allowRefreshTimeOffset = _config.RefreshTimeSpan * 60 * 5 / 4;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TokenAccess Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(_config.RefreshTimeSpan));

            return Task.CompletedTask;
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="state"></param>
        private async void DoWork(object state)
        {
            _logger.LogInformation("TokenAccess Background Service is working.");
            var cacheKey = WeChatCacheConstants.GetCacheKey(WeChatRefreshTokenConstants.AccessTokenKey);
            var lockKey = WeChatCacheConstants.GetLockKey(WeChatRefreshTokenConstants.AccessTokenKey);

            await _cacheManager.LockAndOperateAsync(lockKey, async () =>
            {
                var entry = await _cacheManager.GetAsync<WeChatCacheEntry<string>>(
                    cacheKey);
                if (entry == null || (entry.GetExpireOffset?.TotalSeconds ?? 0) < _allowRefreshTimeOffset)
                {
                    var result = await _weChatClient.RequestRefreshTokenAsync(new WeChatRefreshTokenInput(), new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token).ConfigureAwait(false);
                    if (result.IsError)
                    {
                        _logger.LogError(result.Exception, $"刷新Token失败{result.Error}");
                    }
                    else
                    {
                        var expireTime = DateTime.Now.Add(TimeSpan.FromSeconds(_expireTime));
                        await _cacheManager.SetAsync(cacheKey, new WeChatCacheEntry<string>
                        {
                            Value = result.Result.AccessToken,
                            ExpireTime = expireTime
                        }, expireTime);
                        _logger.LogInformation(WeChatRefreshTokenConstants.RefreshAccessTokenCacheSuccess);
                    }
                }
                else
                {
                    _logger.LogInformation(WeChatRefreshTokenConstants.RefreshAccessTokenCacheCancel);
                }

            }, TimeSpan.FromSeconds(5));
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