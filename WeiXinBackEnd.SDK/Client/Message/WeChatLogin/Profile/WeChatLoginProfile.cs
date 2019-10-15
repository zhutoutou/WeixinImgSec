using AutoMapper;

namespace WeiXinBackEnd.SDK.Client.Message.WeChatLogin.Profile
{
    public class WeChatLoginProfile:AutoMapper.Profile
    {
        public WeChatLoginProfile()
        {
            CreateMap<WeChatLoginInput, WeChatLoginRequest>(MemberList.None);
            CreateMap<WeChatClientOptions, WeChatLoginRequest>(MemberList.None);
        }

    }
}