using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace WeiXinBackEnd.SDK.Client.Message.WeChatLogin
{
    public class WeChatLoginRequest: WeChatRequest
    {

        /// <summary>
        /// AppId
        /// </summary>
        [Required]
        [WeChatParameterName("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        [Required]
        [WeChatParameterName("secret")]
        public string AppSecret { get; set; }

        /// <summary>
        /// 登陆Code
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

        /// <summary>
        /// 请求前准备
        /// </summary>
        public override void Prepare()
        {
            Method = HttpMethod.Get;
            GrantType = WeChatConstants.WeChatLoginRequest.LoginGrantType;
            Address = WeChatConstants.WeChatLoginRequest.Address;
            base.Prepare();
        }
    }
}