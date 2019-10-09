using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeiXinBackEnd.SDK.Client.Message;
using WeiXinBackEnd.SDK.Client.WeChatLogin.Dto;

namespace WeiXinBackEnd.SDK.Client
{
    /// <summary>
    /// Models making HTTP requests for back-end code login notification.
    /// </summary>
    public class WeChatClient
    {
        private readonly Func<HttpMessageInvoker> _client;
        private readonly WeChatClientOptions _options;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeChatClient"/> class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException">client</exception>

        public WeChatClient(
            HttpMessageInvoker client,
            WeChatClientOptions options,
            ILogger logger)
            : this(() => client, options, logger)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeChatClient"/> class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException">client</exception>

        public WeChatClient(
            Func<HttpMessageInvoker> client,
            WeChatClientOptions options,
            ILogger logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger;

        }

        /// <summary>
        /// Sets request parameters from the options.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="parameters">The parameters.</param>
        internal void ApplyRequestParameters(WeChatRequest request, IDictionary<string, string> parameters)
        {
            request.AppId = _options.AppId;
            request.AppSecret = _options.AppSecret;
            request.Parameters = _options.Parameters;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.Parameters.Add(parameter);
                }
            }
        }


        public async Task<WeChatLoginResponse> RequestLoginAsync(IDictionary<string, string> parameters = null, CancellationToken cancellationToken = default)
        {
            var request = new WeChatRequest();
            ApplyRequestParameters(request, parameters);
        }
    }
}
