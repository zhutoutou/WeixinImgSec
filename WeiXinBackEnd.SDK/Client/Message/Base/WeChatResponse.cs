using Newtonsoft.Json;

namespace WeiXinBackEnd.SDK.Client.Message.Base
{
    public class WeChatResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("errcode")]
        public int ErrCode { get; set; }

        /// <summary>
        /// 错误内容
        /// </summary>
        [JsonProperty("errmsg")]
        public string ErrMsg { get; set; }
    }
}