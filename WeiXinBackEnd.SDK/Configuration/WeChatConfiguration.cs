using WeiXinBackEnd.SDK.Client;

namespace WeiXinBackEnd.SDK.Configuration
{
    public class WeChatConfiguration
    {
        /// <summary>
        /// Token刷新间隔 应低于120分钟
        /// </summary>
        public int RefreshTimeSpan { get; set; } = WeChatConfigurationConstants.DefaultRefreshTimeSpan;
    }
}