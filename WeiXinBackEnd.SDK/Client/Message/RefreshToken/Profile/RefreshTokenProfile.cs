using AutoMapper;

namespace WeiXinBackEnd.SDK.Client.Message.RefreshToken.Profile
{
    internal class RefreshTokenProfile:AutoMapper.Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<WeChatRefreshTokenInput, RefreshTokenRequest>(MemberList.None);
            CreateMap<WeChatClientOptions, RefreshTokenRequest>(MemberList.None);
        }
    }
}