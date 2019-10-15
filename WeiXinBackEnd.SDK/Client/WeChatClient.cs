using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WeiXinBackEnd.SDK.Client.Extensions;
using WeiXinBackEnd.SDK.Client.Message;
using WeiXinBackEnd.SDK.Client.Message.WeChatLogin;
using WeiXinBackEnd.SDK.Client.Message.WeChatRefreshToken;

namespace WeiXinBackEnd.SDK.Client
{
    /// <summary>
    /// Models making HTTP requests for back-end code login notification.
    /// </summary>
    public class WeChatClient : IWeChatClient
    {
        private readonly Func<HttpMessageInvoker> _client;
        private readonly WeChatClientOptions _options;
        private readonly ILogger _logger;
        private Mapper _mapper;
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

            InitScopeAutoMapper();
        }

        private void InitScopeAutoMapper()
        {
            // AutoMapper 注册Profiles
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(WeChatClient).GetTypeInfo().Assembly));
            config.AssertConfigurationIsValid();
            _mapper = new Mapper(config);
        }

        /// <summary>
        ///  小程序Token获取
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ProtocolResponse<WeChatLoginResponse>> RequestRefreshTokenAsync(WeChatRefreshTokenInput input, CancellationToken cancellationToken = default)
        {
            var request = _mapper.Map<WeChatRefreshTokenInput, WeChatClientOptions, WeChatRefreshTokenRequest>(input, _options);
            var result = await _client().RequestRefreshTokenAsync(request, _options, cancellationToken).ConfigureAwait(false);
            if (result.IsError)
            {
                _logger.LogError(result.Exception, result.Error);
            }
            else
            {
                _logger.LogInformation(WeChatLoginConstants.SuccessLog);
            }

            return result;
        }

        /// <summary>
        /// 小程序登录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ProtocolResponse<WeChatLoginResponse>> RequestLoginAsync(WeChatLoginInput input, CancellationToken cancellationToken = default)
        {
            var request = _mapper.Map<WeChatLoginInput, WeChatClientOptions, WeChatLoginRequest>(input, _options);
            var result = await _client().RequestLoginAsync(request, _options, cancellationToken).ConfigureAwait(false);
            if (result.IsError)
            {
                _logger.LogError(result.Exception, result.Error);
            }
            else
            {
                _logger.LogInformation(WeChatLoginConstants.SuccessLog);
            }

            return result;
        }
    }
}
