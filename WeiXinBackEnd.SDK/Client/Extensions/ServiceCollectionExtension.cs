using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using WeiXinBackEnd.SDK.Configuration;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWeChatCore(this IServiceCollection services, WeChatClientOptions options, Action<WeChatConfiguration> action)
        {
            var config = new WeChatConfiguration();
            action?.Invoke(config);
            services.AddSingleton(config);
            services.AddSingleton(options);
            services.AddWeChatHttpClient();
            services.AddTransient<IWeChatClient, WeChatClient>();
            services.AddHostedService<TokenAccessHostedService>();
            return services;
        }

        /// <summary>
        /// 定义HttpMessageInvoker
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection AddWeChatHttpClient(this IServiceCollection services)
        {
            services.AddTransient(ioc =>
            {
                var options = ioc.GetService<WeChatConfiguration>();
                if (options.ClientFactory == null)
                {
                    services.AddHttpClient();
                    var factory = ioc.GetService<IHttpClientFactory>();
                    return () => factory.CreateClient();
                }

                return options.ClientFactory;
            });

            return services;
        }
    }
}