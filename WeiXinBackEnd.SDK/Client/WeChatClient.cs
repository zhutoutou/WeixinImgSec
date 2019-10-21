using System;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WeiXinBackEnd.SDK.Client.Extensions;
using WeiXinBackEnd.SDK.Client.Message._01_Login;
using WeiXinBackEnd.SDK.Client.Message.Base;
using WeiXinBackEnd.SDK.Client.Message.ImgSec;
using WeiXinBackEnd.SDK.Client.Message.RefreshToken;
using WeiXinBackEnd.SDK.Configuration;

namespace WeiXinBackEnd.SDK.Client
{
    /// <summary>
    /// Models making HTTP requests for back-end code login notification.
    /// </summary>
    public class WeChatClient : IWeChatClient
    {
        private readonly Func<HttpMessageInvoker> _client;
        private readonly WeChatOptions _config;
        private readonly ILogger _logger;
        private Mapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeChatClient"/> class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        /// <exception cref="ArgumentNullException">client</exception>
        public WeChatClient(
            HttpMessageInvoker client,
            ILogger<WeChatClient> logger,
            WeChatOptions config)
            : this(() => client, logger, config)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeChatClient"/> class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        /// <exception cref="ArgumentNullException">client</exception>
        public WeChatClient(
            Func<HttpMessageInvoker> client,
            ILogger<WeChatClient> logger,
            WeChatOptions config)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger;
            _config = config;

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
        public async Task<ProtocolResponse<WeChatRefreshTokenResponse>> RequestRefreshTokenAsync(WeChatRefreshTokenInput input, CancellationToken cancellationToken = default)
        {
            var request = _mapper.Map<WeChatRefreshTokenInput, WeChatConfig, RefreshTokenRequest>(input, _config.WeChatConfig);
            var result = await _client().RequestAsync<WeChatRefreshTokenResponse, RefreshTokenRequest>(request, cancellationToken).ConfigureAwait(false);
            if (result.IsError)
            {
                _logger.LogError(result.Exception, result.Error);
            }
            else
            {
                _logger.LogInformation(WeChatRefreshTokenConstants.SuccessLog);
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
            var request = _mapper.Map<WeChatLoginInput, WeChatConfig, LoginRequest>(input, _config.WeChatConfig);
            var result = await _client().RequestAsync<WeChatLoginResponse, LoginRequest>(request, cancellationToken).ConfigureAwait(false);
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
        /// 图片鉴定
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ProtocolResponse<WeChatImgSecResponse>> RequestImgSecAsync(WeChatImgSecInput input, CancellationToken cancellationToken = default)
        {
            var request = _mapper.Map<WeChatImgSecInput, WeChatConfig, ImgSecRequest>(input, _config.WeChatConfig);
            var result = await _client().RequestAsync<WeChatImgSecResponse, ImgSecRequest>(request, cancellationToken).ConfigureAwait(false);
            if (result.IsError)
            {
                _logger.LogError(result.Exception, result.Error);
            }
            else
            {
                _logger.LogInformation(WeChatImgSecConstants.SuccessLog);
            }

            return result;
        }
    }
}
