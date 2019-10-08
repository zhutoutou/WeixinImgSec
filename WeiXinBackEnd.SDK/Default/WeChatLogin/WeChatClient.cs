using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeiXinBackEnd.SDK.Default.WeChatLogin.Dto;

namespace WeiXinBackEnd.SDK.Default.WeChatLogin
{
    /// <summary>
    /// Models making HTTP requests for back-end code login notification.
    /// </summary>
    public class WeChatClient
    {
        private readonly Func<HttpMessageInvoker> _client;
        private readonly WeChatClientOptions _options;
        private readonly ILogger<WeChatClient> _logger;

        public WeChatClient(
            Func<HttpMessageInvoker> client, 
            ILogger<WeChatClient> logger, WeChatClientOptions options)
        {
            _client = client;
            _logger = logger;
            _options = options;
        }

        public async Task<WeChatLoginResponse> PostWeChatLoginAsync(WeChatLoginInput input)
        {

        }
    }
}
