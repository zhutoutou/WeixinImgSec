namespace WeiXinBackEnd.SDK.Client.Message.WeChatImgSec.Profile
{
    public class WeChatImgSecProfile:AutoMapper.Profile
    {
        public WeChatImgSecProfile()
        {
            CreateMap<WeChatImgSecInput, WeChatImgSecRequest>();
            CreateMap<WeChatClientOptions, WeChatImgSecRequest>();
        }
    }
}