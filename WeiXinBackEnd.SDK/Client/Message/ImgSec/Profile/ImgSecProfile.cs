using AutoMapper;
using WeiXinBackEnd.SDK.Configuration;

namespace WeiXinBackEnd.SDK.Client.Message.ImgSec.Profile
{
    internal class ImgSecProfile:AutoMapper.Profile
    {
        public ImgSecProfile()
        {
            CreateMap<WeChatImgSecInput, ImgSecRequest>(MemberList.None);
            CreateMap<WeChatConfig, ImgSecRequest>(MemberList.None);
        }
    }
}