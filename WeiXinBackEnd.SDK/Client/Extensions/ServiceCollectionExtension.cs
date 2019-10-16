using System;
using System.Net.Http;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using WeiXinBackEnd.SDK.Configuration;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWeChatCore(this IServiceCollection services,[NotNull]Action<WeChatConfiguration> options)
        {
            #region Construct Config
            var config = new WeChatConfiguration();
            options?.Invoke(config);
            if (config.AppConfig == null)
                throw new ArgumentNullException(nameof(config.AppConfig));
            config.AssertAppConfigIsValid();
            #endregion
            
            services.AddSingleton(config);
            services.AddWeChatHttpClient(config.ClientFactory);
            services.AddTransient<IWeChatClient, WeChatClient>();
            services.AddHostedService<TokenAccessHostedService>();
            return services;
        }

        /// <summary>
        /// 定义HttpMessageInvoker
        /// </summary>
        /// <param name="services"></param>
        /// <param name="httpFunc"></param>
        /// <returns></returns>
        internal static IServiceCollection AddWeChatHttpClient(this IServiceCollection services,Func<HttpMessageInvoker> httpFunc)
        {
            if (httpFunc != null)
            {
                services.AddTransient<Func<HttpMessageInvoker>>((ioc)=>httpFunc);
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
            return services;
        }
    }
}