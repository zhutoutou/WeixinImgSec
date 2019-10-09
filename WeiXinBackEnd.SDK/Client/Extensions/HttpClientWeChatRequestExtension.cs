using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeiXinBackEnd.SDK.Client.Message;
using WeiXinBackEnd.SDK.Client.WeChatLogin.Dto;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    public static class HttpClientWeChatRequestExtension
    {
        public static async Task<WeChatLoginResponse> RequestLoginAsync(this HttpMessageInvoker client,
            WeChatLoginRequest request, CancellationToken cancellationToken = default)
        {
            return await client.RequestLoginAsync(request,cancellationToken);
        }


        internal static async Task<WeChatLoginResponse> RequestLoginAsync(this HttpMessageInvoker client, WeChatRequest request, CancellationToken cancellationToken = default)
        {
            request.Prepare();
            request.Method = HttpMethod.Get;

            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ProtocolResponse.FromException<WeChatResponse>(ex);
            }

            return await ProtocolResponse.FromHttpResponseAsync<WeChatResponse>(response).ConfigureAwait();
        }
    }
}