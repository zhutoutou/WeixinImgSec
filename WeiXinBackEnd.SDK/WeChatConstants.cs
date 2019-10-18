namespace WeiXinBackEnd.SDK
{
    internal static class WeChatConfigurationConstants
    {
        public const int DefaultRefreshTimeSpan = 70;
    }

    internal static class DefaultWeChatCacheConstants
    {
        public const string DefaultSetFailErrorMessage = "MemoryCache插入失败";
    }
    /// <summary>
    /// 微信登陆Constants
    /// </summary>
    internal static class WeChatLoginConstants
    {
        public const string Address = "https://api.weixin.qq.com/sns/jscode2session";
        public const string GrantType = "authorization_code";
        public const string SuccessLog = "RequestLoginAsync请求成功";
    }

    /// <summary>
    /// 微信登陆Constants
    /// </summary>
    internal static class WeChatRefreshTokenConstants
    {
        public const string Address = "https://api.weixin.qq.com/cgi-bin/token";
        public const string GrantType = "client_credential";
        public const string SuccessLog = "RequestRefreshToken请求成功";
        #region Cache

        public const string AccessTokenCacheKey = "access_token";
        public const string RefreshAccessTokenCacheSuccess = "竞争锁成功,成功刷新Token";
        public const string RefreshAccessTokenCacheCancel = "竞争锁失败,取消刷新Token";

        #endregion
    }

    /// <summary>
    /// 图片审核
    /// </summary>
    internal static class WeChatImgSecConstants
    {
        public const string Address = "https://api.weixin.qq.com/wxa/img_sec_check";
        public const string ImageMediaType = "image/jpeg";
        public const string SuccessLog = "RequestImgSec请求成功";
    }

}