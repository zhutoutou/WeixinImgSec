using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeiXinBackEnd.SDK.Client.Message.Base;
using WeiXinBackEnd.SDK.Client.Message.Base.Enum;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    public static class HttpClientWeChatRequestExtension
    {
        /// <summary>
        /// Http请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<ProtocolResponse<TV>> RequestAsync<TV, TK>(this HttpMessageInvoker client,
            TK request,  CancellationToken cancellationToken = default)
            where TV : WeChatResponse 
            where TK : WeChatRequest
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            client = client ?? throw new ArgumentNullException(nameof(client));
            try
            {
                request.Prepare();
            }
            catch (Exception ex)
            {
                return ProtocolResponse<TV>.FromException<ProtocolResponse<TV>>(ex, ResponseErrorType.Prepare);
            }


            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ProtocolResponse<TV>.FromException<ProtocolResponse<TV>>(ex, ResponseErrorType.Exception);
            }

            return await ProtocolResponse<TV>.FromHttpResponseAsync<ProtocolResponse<TV>>(response).ConfigureAwait(false);
        }

    }
}