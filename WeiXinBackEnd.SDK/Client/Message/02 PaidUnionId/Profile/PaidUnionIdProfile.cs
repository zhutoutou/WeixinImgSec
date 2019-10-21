using AutoMapper;
using WeiXinBackEnd.SDK.Configuration;

namespace WeiXinBackEnd.SDK.Client.Message._02_PaidUnionId.Profile
{
    public class PaidUnionIdProfile : AutoMapper.Profile
    {
        public PaidUnionIdProfile()
        {
            CreateMap<WeChatPaidUnionIdInput, PaidUnionIdRequest>(MemberList.None);
            CreateMap<WeChatConfig, PaidUnionIdRequest>(MemberList.None);
        }
    }
}