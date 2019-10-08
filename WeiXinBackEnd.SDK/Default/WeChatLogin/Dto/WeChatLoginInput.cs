namespace WeiXinBackEnd.SDK.Default.WeChatLogin.Dto
{
    /// <summary>
    /// WeChat
    /// </summary>
    public class WeChatLoginInput
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 临时登录凭证code 
        /// </summary>
        public string LoginCode { get; set; }
    }
}