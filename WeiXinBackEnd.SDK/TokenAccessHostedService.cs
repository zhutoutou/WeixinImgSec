using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeiXinBackEnd.SDK.Client;
using WeiXinBackEnd.SDK.Client.Message.RefreshToken;
using WeiXinBackEnd.SDK.Configuration;

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

        public TokenAccessHostedService(
            ILogger<TokenAccessHostedService> logger,
            IWeChatClient weChatClient,
            WeChatConfiguration config)
        {
            _logger = logger;
            _weChatClient = weChatClient;
            _config = config;
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
            var result = await _weChatClient.RequestRefreshTokenAsync(new WeChatRefreshTokenInput()).ConfigureAwait(false);
            if (result.IsError)
            {
                _logger.LogError(result.Exception, $"刷新Token失败{result.Error}");
            }
            else
            {
                _config.AppConfig.AccessToken = result.Result.AccessToken;
            }
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