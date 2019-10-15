using AutoMapper;

namespace WeiXinBackEnd.SDK.Client.Message.WeChatRefreshToken.Profile
{
    public class WeChatRefreshTokenProfile:AutoMapper.Profile
    {
        public WeChatRefreshTokenProfile()
        {
            CreateMap<WeChatRefreshTokenInput, WeChatRefreshTokenRequest>(MemberList.None);
            CreateMap<WeChatClientOptions, WeChatRefreshTokenRequest>(MemberList.None);
        }
    }
}