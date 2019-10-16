using System;
using System.Net.Http;
using WeiXinBackEnd.SDK.Client;

namespace WeiXinBackEnd.SDK.Configuration
{
    public class WeChatConfiguration
    {
        /// <summary>
        /// Token刷新间隔 应低于120分钟
        /// </summary>
        public int RefreshTimeSpan { get; set; } = WeChatConfigurationConstants.DefaultRefreshTimeSpan;

        /// <summary>
        /// 用于客户端的自定义
        /// </summary>
        public Func<HttpMessageInvoker> ClientFactory {get;set;}
    }
}