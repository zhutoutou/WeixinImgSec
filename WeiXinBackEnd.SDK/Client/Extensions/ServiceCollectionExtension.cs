using System;
using System.Collections.Concurrent;
using System.Net.Http;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Nito.AsyncEx;
using WeiXinBackEnd.SDK.Configuration;
using WeiXinBackEnd.SDK.Core.Cache;
using WeiXinBackEnd.SDK.Core.Cache.MSCache;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWeChatCore(this IServiceCollection services,[NotNull]Action<WeChatConfiguration> options)
        {
            #region Construct Config
            var config = new WeChatConfiguration();
            options(config);
            if (config.AppConfig == null)
                throw new ArgumentNullException(nameof(config.AppConfig));
            config.AssertAppConfigIsValid();
            services.AddSingleton(config);
            #endregion

            services.AddWeChatCache(config);
            services.AddWeChatHttpClient(config);
            services.AddTransient<IWeChatClient, WeChatClient>();
            services.AddHostedService<TokenAccessHostedService>();
            return services;
        }

        /// <summary>
        /// 定义WeChat缓存
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        private static void AddWeChatCache(this IServiceCollection services, WeChatConfiguration config)
        {
            if (config.CacheType == typeof(DefaultWeChatCache))
            {
                services.AddSingleton(new MemoryCache(new MemoryCacheOptions()));
                services.AddSingleton(new ConcurrentDictionary<string, AsyncLock>());
            }
       
            services.AddTransient(typeof(IWeChatCache),config.CacheType);
        }

        /// <summary>
        /// 定义HttpMessageInvoker
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static void AddWeChatHttpClient(this IServiceCollection services, WeChatConfiguration config)
        {
            var httpFunc = config.ClientFactory;
            if (httpFunc != null)
            {
                services.AddTransient(ioc=>httpFunc);
            }
            else
            {
                services.AddHttpClient();
                services.AddTransient<Func<HttpMessageInvoker>>(ioc => {
                    return () =>
                    {
                        var factory = ioc.GetService<IHttpClientFactory>();
                        return factory.CreateClient();
                    };
                });

            }
        }
    }
}