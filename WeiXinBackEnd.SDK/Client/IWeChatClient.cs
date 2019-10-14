using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeiXinBackEnd.SDK.Client.Message;
using WeiXinBackEnd.SDK.Client.Message.WeChatLogin;

namespace WeiXinBackEnd.SDK.Client
{
    public interface IWeChatClient
    {
        Task<ProtocolResponse<WeChatLoginResponse>> RequestLoginAsync(WeChatLoginInput input,
            CancellationToken cancellationToken = default);
    }
}