namespace WeiXinBackEnd.SDK.Client
{
    /// <summary>
    /// ΢�ŵ�½Constants
    /// </summary>
    public static class WeChatLoginConstants
    {
        public const string Address = "https://api.weixin.qq.com/sns/jscode2session";
        public const string LoginGrantType = "authorization_code";
        public const string SuccessLog = "RequestLoginAsync����ɹ�";
    }

    /// <summary>
    /// ΢�ŵ�½Constants
    /// </summary>
    public static class WeChatRefreshTokenConstants
    {
        public const string Address = "https://api.weixin.qq.com/cgi-bin/token";
        public const string SuccessLog = "RequestRefreshToken����ɹ�";
    }

}