using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeiXinBackEnd.SDK.Default.WeChatLogin.Dto;

namespace WeiXinBackEnd.SDK.Default.WeChatLogin
{
    /// <summary>
    /// Models making HTTP requests for back-end code login notification.
    /// </summary>
    public class WeChatLoginHttpClient
    {
        private HttpClient _client;
        private ILogger<WeChatLoginHttpClient> _logger;

        public WeChatLoginHttpClient(
            HttpClient client, 
            ILogger<WeChatLoginHttpClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<WeChatLoginResponse> PostWeChatLoginAsync(WeChatLoginInput input)
        {

        }
    }
}
