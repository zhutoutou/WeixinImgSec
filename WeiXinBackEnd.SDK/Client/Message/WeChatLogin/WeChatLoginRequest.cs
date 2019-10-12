using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace WeiXinBackEnd.SDK.Client.Message.WeChatLogin
{
    public class WeChatLoginRequest: WeChatRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [WeChatParameterName("js_code")]
        public string LoginCode { get; set; }

        /// <summary>
        /// 授权方式 默认值为authorization_code
        /// </summary>
        [Required]
        [WeChatParameterName("grant_type")]
        public string GrantType { get; set; }

        public override void Prepare(HttpMethod method)
        {
            Method = HttpMethod.Get;
            GrantType = WeChatConstants.WeChatLoginRequest.LoginGrantType;

            
        }
    }
}