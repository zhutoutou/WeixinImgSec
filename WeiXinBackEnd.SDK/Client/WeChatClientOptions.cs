using System.Collections.Generic;

namespace WeiXinBackEnd.SDK.Client
{
    public class WeChatClientOptions
    {
        public WeChatClientOptions(string appId, string appSecret)
        {
            AppId = appId;
            AppSecret = appSecret;
        }

        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; }

    }
}