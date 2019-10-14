namespace WeiXinBackEnd.SDK.Client
{
    public static class WeChatConstants
    {

        public static class WeChatResponse
        {
            public const string Error = "errmsg";
            public const string ErrorCode = "errcode";
        }

        public static class WeChatLoginRequest
        {
            public const string LoginGrantType = "authorization_code";
            public const string Address = "https://api.weixin.qq.com/sns/jscode2session";
        }

    }
}