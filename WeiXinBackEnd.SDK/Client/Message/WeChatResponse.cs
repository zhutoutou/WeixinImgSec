namespace WeiXinBackEnd.SDK.Client.Message
{
    public class WeChatResponse:ProtocolResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrCode { get; set; }


        public string ErrMsg { get; set; }
    }
}