namespace WeiXinBackEnd.SDK.Client.Message.ImgSec
{
    public class WeChatImgSecInput
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        public byte[] File { get; set; }
    }
}