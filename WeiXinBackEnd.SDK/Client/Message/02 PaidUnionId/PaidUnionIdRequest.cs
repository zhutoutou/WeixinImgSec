using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using WeiXinBackEnd.SDK.Client.Message.Base;
using WeiXinBackEnd.SDK.Client.Message.Base.Attributes;

namespace WeiXinBackEnd.SDK.Client.Message._02_PaidUnionId
{
    public class PaidUnionIdRequest:WeChatRequest
    {
        /// <summary>
        /// access_token
        /// </summary>
        [Required]
        [WeChatParameterName("access_token")]
        public string AccessToken { get; set; }

        public override void Prepare()
        {
            Method = HttpMethod.Get;
            base.Prepare();
        }
    }
}