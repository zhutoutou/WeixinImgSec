namespace WeiXinBackEnd.SDK.Default.WeChatLogin.Dto
{
    public class WeChatLoginResponse
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
    }
}