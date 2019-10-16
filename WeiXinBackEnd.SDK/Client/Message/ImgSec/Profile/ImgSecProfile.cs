using AutoMapper;

namespace WeiXinBackEnd.SDK.Client.Message.ImgSec.Profile
{
    internal class ImgSecProfile:AutoMapper.Profile
    {
        public ImgSecProfile()
        {
            CreateMap<WeChatImgSecInput, ImgSecRequest>(MemberList.None);
            CreateMap<WeChatClientOptions, ImgSecRequest>(MemberList.None);
        }
    }
}