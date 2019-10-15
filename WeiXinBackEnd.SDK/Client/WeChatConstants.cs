namespace WeiXinBackEnd.SDK.Client
{
    /// <summary>
    /// 微信登陆Constants
    /// </summary>
    public static class WeChatLoginConstants
    {
        public const string Address = "https://api.weixin.qq.com/sns/jscode2session";
        public const string LoginGrantType = "authorization_code";
        public const string SuccessLog = "RequestLoginAsync请求成功";
    }

    /// <summary>
    /// 微信登陆Constants
    /// </summary>
    public static class WeChatRefreshTokenConstants
    {
        public const string Address = "https://api.weixin.qq.com/cgi-bin/token";
        public const string SuccessLog = "RequestRefreshToken请求成功";
    }

}