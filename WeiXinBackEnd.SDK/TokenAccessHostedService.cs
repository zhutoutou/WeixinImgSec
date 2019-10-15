using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeiXinBackEnd.SDK.Client;

namespace WeiXinBackEnd.SDK
{
    /// <summary>
    /// Token刷新后台服务
    /// </summary>
    public class TokenAccessHostedService : IHostedService, IDisposable
    {

        private readonly ILogger _logger;
        private readonly WeChatClientOptions _options;
        private Timer _timer;
        private readonly IWeChatClient _weChatClient;

        public TokenAccessHostedService(
            ILogger<TokenAccessHostedService> logger, 
            WeChatClientOptions options, 
            IWeChatClient weChatClient)
        {
            _logger = logger;
            _options = options;
            _weChatClient = weChatClient;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TokenAccess Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(70));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("TokenAccess Background Service is working.");
            _options.AccessToken = _weChatClient.RefreshToken();
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