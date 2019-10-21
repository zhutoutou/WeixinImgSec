using System;
using System.Net.Http;
using WeiXinBackEnd.SDK.Client.Message.Base.Enum;
using WeiXinBackEnd.SDK.Core.Cache.MSCache;

namespace WeiXinBackEnd.SDK.Configuration
{
    public class WeChatOptions
    {
        public WeChatOptions()
        {
            CacheType = typeof(DefaultWeChatCache);
            RefreshTimeSpan = WeChatConfigurationConstants.DefaultRefreshTimeSpan;
        }
        /// <summary>
        /// 小程序信息配置构造函数
        /// </summary>
        public WeChatConfig WeChatConfig { get; set; }

        /// <summary>
        /// Token刷新间隔 应低于25分钟
        /// </summary>
        public int RefreshTimeSpan { get; set; }

        /// <summary>
        /// 是否开启支付
        /// </summary>
        public bool UseTransaction { get; set; }

        /// <summary>
        /// 用于客户端的自定义
        /// </summary>
        public Func<HttpMessageInvoker> ClientFactory {get;set;}

        /// <summary>
        ///  缓存类型
        /// </summary>
        public Type CacheType { get; set; }

        /// <summary>
        /// 验证AppConfig的必要性
        /// </summary>
        public void AssertAppConfigIsValid()
        {
            var requiredType = RequiredType.App;
            if (UseTransaction)
                requiredType = requiredType | RequiredType.Mch;
            WeChatConfig.Assert(assertType: requiredType);
         
        }
    }
}