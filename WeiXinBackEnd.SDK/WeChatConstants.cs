namespace WeiXinBackEnd.SDK
{
    public static class WeChatConfigurationConstants
    {
        public const int DefaultRefreshTimeSpan = 70;

    }
    /// <summary>
    /// ΢�ŵ�½Constants
    /// </summary>
    public static class WeChatLoginConstants
    {
        public const string Address = "https://api.weixin.qq.com/sns/jscode2session";
        public const string GrantType = "authorization_code";
        public const string SuccessLog = "RequestLoginAsync����ɹ�";
    }

    /// <summary>
    /// ΢�ŵ�½Constants
    /// </summary>
    public static class WeChatRefreshTokenConstants
    {
        public const string Address = "https://api.weixin.qq.com/cgi-bin/token";
        public const string GrantType = "client_credential";
        public const string SuccessLog = "RequestRefreshToken����ɹ�";
        
    }

    /// <summary>
    /// ͼƬ���
    /// </summary>
    public static class WeChatImgSecConstants
    {
        public const string Address = "https://api.weixin.qq.com/wxa/img_sec_check";
        public const string ImageMediaType = "image/jpeg";
        public const string SuccessLog = "RequestImgSec����ɹ�";


    }

}