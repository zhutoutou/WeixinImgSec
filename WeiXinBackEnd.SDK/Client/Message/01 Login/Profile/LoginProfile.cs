using AutoMapper;

namespace WeiXinBackEnd.SDK.Client.Message._01_Login.Profile
{
    internal class LoginProfile:AutoMapper.Profile
    {
        public LoginProfile()
        {
            CreateMap<WeChatLoginInput, LoginRequest>(MemberList.None);
            CreateMap<WeChatClientOptions, LoginRequest>(MemberList.None);
        }

    }
}