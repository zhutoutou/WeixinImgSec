namespace WeiXinBackEnd.SDK
{
    internal static class WeChatConfigurationConstants
    {
        public const int DefaultRefreshTimeSpan = 70;
    }

    internal static class DefaultWeChatCacheConstants
    {
        public const string DefaultSetFailErrorMessage = "MemoryCache����ʧ��";
    }
    /// <summary>
    /// ΢�ŵ�½Constants
    /// </summary>
    internal static class WeChatLoginConstants
    {
        public const string Address = "https://api.weixin.qq.com/sns/jscode2session";
        public const string GrantType = "authorization_code";
        public const string SuccessLog = "RequestLoginAsync����ɹ�";
    }

    /// <summary>
    /// ΢�ŵ�½Constants
    /// </summary>
    internal static class WeChatRefreshTokenConstants
    {
        public const string Address = "https://api.weixin.qq.com/cgi-bin/token";
        public const string GrantType = "client_credential";
        public const string SuccessLog = "RequestRefreshToken����ɹ�";
        #region Cache

        public const string AccessTokenCacheKey = "access_token";
        public const string RefreshAccessTokenCacheSuccess = "�������ɹ�,�ɹ�ˢ��Token";
        public const string RefreshAccessTokenCacheCancel = "������ʧ��,ȡ��ˢ��Token";

        #endregion
    }

    /// <summary>
    /// ͼƬ���
    /// </summary>
    internal static class WeChatImgSecConstants
    {
        public const string Address = "https://api.weixin.qq.com/wxa/img_sec_check";
        public const string ImageMediaType = "image/jpeg";
        public const string SuccessLog = "RequestImgSec����ɹ�";
    }

}