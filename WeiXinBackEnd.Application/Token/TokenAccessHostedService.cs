using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WeiXinBackEnd.Application.Token
{
    public class TokenAccessHostedService: IHostedService, IDisposable
    {

        private readonly ILogger _logger;
        private readonly TokenManager _tokenManager;
        private Timer _timer;

        public TokenAccessHostedService(ILogger<TokenAccessHostedService> logger, TokenManager tokenManager)
        {
            _logger = logger;
            _tokenManager = tokenManager;
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
            _ = _tokenManager.RefreshToken();
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