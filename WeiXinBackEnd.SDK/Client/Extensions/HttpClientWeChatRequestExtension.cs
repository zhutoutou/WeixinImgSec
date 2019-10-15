using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeiXinBackEnd.SDK.Client.Message;
using WeiXinBackEnd.SDK.Client.Message.WeChatLogin;
using WeiXinBackEnd.SDK.Client.Message.WeChatRefreshToken;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    public static class HttpClientWeChatRequestExtension
    {
        /// <summary>
        /// Token获取
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<ProtocolResponse<WeChatRefreshTokenResponse>> RequestRefreshTokenAsync(this HttpMessageInvoker client,
            WeChatRefreshTokenRequest request, WeChatClientOptions options, CancellationToken cancellationToken = default)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            client = client ?? throw new ArgumentNullException(nameof(client));
            try
            {
                request.Prepare();
            }
            catch (Exception ex)
            {
                return ProtocolResponse<WeChatRefreshTokenResponse>.FromException<ProtocolResponse<WeChatRefreshTokenResponse>>(ex, ResponseErrorType.Prepare);
            }


            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ProtocolResponse<WeChatRefreshTokenResponse>.FromException<ProtocolResponse<WeChatRefreshTokenResponse>>(ex, ResponseErrorType.Exception);
            }

            return await ProtocolResponse<WeChatRefreshTokenResponse>.FromHttpResponseAsync<ProtocolResponse<WeChatRefreshTokenResponse>>(response).ConfigureAwait(false);
        }

        /// <summary>
        /// 微信登陆
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<ProtocolResponse<WeChatLoginResponse>> RequestLoginAsync(this HttpMessageInvoker client,
            WeChatLoginRequest request, WeChatClientOptions options, CancellationToken cancellationToken = default)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            client = client ?? throw new ArgumentNullException(nameof(client));
            try
            {
                request.Prepare();
            }
            catch (Exception ex)
            {
                return ProtocolResponse<WeChatLoginResponse>.FromException<ProtocolResponse<WeChatLoginResponse>>(ex, ResponseErrorType.Prepare);
            }


            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ProtocolResponse<WeChatLoginResponse>.FromException<ProtocolResponse<WeChatLoginResponse>>(ex, ResponseErrorType.Exception);
            }

            return await ProtocolResponse<WeChatLoginResponse>.FromHttpResponseAsync<ProtocolResponse<WeChatLoginResponse>>(response).ConfigureAwait(false);
        }
    }
}