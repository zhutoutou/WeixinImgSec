using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace WeiXinBackEnd.SDK.Client.Message.WeChatRefreshToken
{
    public class WeChatRefreshTokenRequest:WeChatRequest
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

        public override void Prepare()
        {
            Method = HttpMethod.Get;
            Address = WeChatLoginConstants.Address;
            base.Prepare();
        }
    }
}