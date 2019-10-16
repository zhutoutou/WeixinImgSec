using System.Threading.Tasks;
using WeiXinBackEnd.Application.UserAccount.Dto;
using WeiXinBackEnd.SDK.Client;
using WeiXinBackEnd.SDK.Client.Message._01_Login;

namespace WeiXinBackEnd.Application.UserAccount
{
    public class UserAccountApp: IUserAccountApp
    {
        private readonly IWeChatClient _weChatClient;

        public UserAccountApp(IWeChatClient weChatClient)
        {
            _weChatClient = weChatClient;
        }

        public async Task<LoginResponse> Login(Dto.LoginInput input)
        {
            var result = await _weChatClient.RequestLoginAsync(new WeChatLoginInput() { LoginCode = input.LoginCode }).ConfigureAwait(false);
            return (LoginResponse) result.Result;
        }
    }
}