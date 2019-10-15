using Newtonsoft.Json;
using WeiXinBackEnd.SDK.Client.Message.Base;

namespace WeiXinBackEnd.SDK.Client.Message.WeChatRefreshToken
{
    public class WeChatRefreshTokenResponse:WeChatResponse
    {
        /// <summary>
        /// access_token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}