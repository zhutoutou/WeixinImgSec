using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeiXinBackEnd.SDK.Client.Message;
using WeiXinBackEnd.SDK.Client.Message.WeChatLogin;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    public static class HttpClientWeChatRequestExtension
    {
        /// <summary>
        /// 微信登陆
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<ProtocolResponse<WeChatLoginResponse>> RequestLoginAsync(this HttpMessageInvoker client,
            WeChatLoginRequest request, CancellationToken cancellationToken = default)
        {
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