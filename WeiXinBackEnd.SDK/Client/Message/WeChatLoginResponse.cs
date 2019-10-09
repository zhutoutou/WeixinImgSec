namespace WeiXinBackEnd.SDK.Client.Message
{
    public class WeChatLoginResponse: WeChatResponse
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 会话密钥
        /// 话密钥 session_key 是对用户数据进行 加密签名 的密钥
        /// </summary>
        public string SessionKey { get; set; }

        /// <summary>
        /// 用户在开放平台的唯一标识符，在满足 UnionID 下发条件的情况下会返回
        /// </summary>
        public string UnionId { get; set; }
    }
}