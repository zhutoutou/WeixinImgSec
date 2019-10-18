using System;
using System.Net.Http;
using WeiXinBackEnd.SDK.Client;
using WeiXinBackEnd.SDK.Client.Message.Base.Enum;
using WeiXinBackEnd.SDK.Core.Cache;
using WeiXinBackEnd.SDK.Core.Cache.MSCache;

namespace WeiXinBackEnd.SDK.Configuration
{
    public class WeChatConfiguration
    {
        /// <summary>
        /// 小程序信息配置
        /// </summary>
        public WeChatClientOptions AppConfig { get; set; }
        
        /// <summary>
        /// Token刷新间隔 应低于120分钟
        /// </summary>
        public int RefreshTimeSpan { get; set; } = WeChatConfigurationConstants.DefaultRefreshTimeSpan;

        /// <summary>
        /// 是否开启支付
        /// </summary>
        public bool UseTransaction { get; set; }

        /// <summary>
        /// 用于客户端的自定义
        /// </summary>
        public Func<HttpMessageInvoker> ClientFactory {get;set;}

        public Type CacheType { get; private set; }= typeof(DefaultWeChatCache);

        /// <summary>
        /// 验证AppConfig的必要性
        /// </summary>
        public void AssertAppConfigIsValid()
        {
            var requiredType = RequiredType.App;
            if (UseTransaction)
                requiredType = requiredType | RequiredType.Mch;
            AppConfig.Assert(assertType: requiredType);
         
        }

        /// <summary>
        /// 使用其他IWeChatCache实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void UseCache<T>() where T : IWeChatCache
        {
            CacheType = typeof(T);
        }
    }
}