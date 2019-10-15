using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeiXinBackEnd.SDK.Client.Message;
using WeiXinBackEnd.SDK.Client.Message.Base;
using WeiXinBackEnd.SDK.Client.Message.WeChatImgSec;
using WeiXinBackEnd.SDK.Client.Message.WeChatLogin;
using WeiXinBackEnd.SDK.Client.Message.WeChatRefreshToken;

namespace WeiXinBackEnd.SDK.Client
{
    public interface IWeChatClient
    {
        /// <summary>
        ///  小程序Token获取
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ProtocolResponse<WeChatRefreshTokenResponse>> RequestRefreshTokenAsync(WeChatRefreshTokenInput input,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 小程序登录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ProtocolResponse<WeChatLoginResponse>> RequestLoginAsync(WeChatLoginInput input,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 图片鉴定
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ProtocolResponse<WeChatImgSecResponse>> RequestImgSecAsync(WeChatImgSecInput input,
            CancellationToken cancellationToken = default);


    }
}