using System;
using System.Net.Http;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeiXinBackEnd.SDK.Configuration;
using WeiXinBackEnd.SDK.Core.Cache;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    /// <summary>
    /// ServiceCollection扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 定义WeChat功能
        /// </summary>
        /// <param name="services">注入服务</param>
        /// <param name="configuration">全局配置文件</param>
        /// <param name="options">配置参数</param>
        /// <returns></returns>
        public static IServiceCollection AddWeChatCore(this IServiceCollection services, [NotNull] IConfiguration configuration, [NotNull]Action<WeChatOptions> options)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            options = options ?? throw new ArgumentNullException(nameof(options));

            #region Construct Config
            var config = new WeChatOptions();
            options(config);
            config.AssertAppConfigIsValid();
            #endregion

            services.AddWeChatCache(configuration, config);
            services.AddWeChatHttpClient(config);
            services.AddTransient<IWeChatClient, WeChatClient>();
            services.AddHostedService<TokenAccessHostedService>();
            return services;
        }

        private static void AddWeChatCache(this IServiceCollection services, IConfiguration configuration, WeChatOptions config)
        {
            config.UseCache(services, configuration, config.WeChatConfig);
            services.AddSingleton(ioc =>
            {
                config.WeChatConfig.CacheManager = ioc.GetRequiredService<IWeChatCache>();
                return config;
            });
        }



        /// <summary>
        /// 定义HttpMessageInvoker
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static void AddWeChatHttpClient(this IServiceCollection services, WeChatOptions config)
        {
            var httpFunc = config.ClientFactory;
            if (httpFunc != null)
            {
                services.AddTransient(ioc => httpFunc);
            }
            else
            {
                services.AddHttpClient();
                services.AddTransient<Func<HttpMessageInvoker>>(ioc =>
                {
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