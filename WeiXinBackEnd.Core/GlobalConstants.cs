namespace WeiXinBackEnd.Core
{
    public static class GlobalConstants
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        public const string TokenUrlFormat = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        /// <summary>
        /// 图片验证
        /// </summary>
        public const string ImgSecUrlFormat = "https://api.weixin.qq.com/wxa/img_sec_check?access_token={0}";
        
        /// <summary>
        /// Image的ContentType
        /// </summary>
        public const string ImageMediaType = "image/jpeg";


    }
}   