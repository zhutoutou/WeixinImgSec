using AutoMapper;
using WeiXinBackEnd.SDK.Configuration;

namespace WeiXinBackEnd.SDK.Client.Message.RefreshToken.Profile
{
    internal class RefreshTokenProfile:AutoMapper.Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<WeChatRefreshTokenInput, RefreshTokenRequest>(MemberList.None);
            CreateMap<WeChatConfig, RefreshTokenRequest>(MemberList.None);
        }
    }
}