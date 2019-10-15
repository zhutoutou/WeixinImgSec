using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using WeiXinBackEnd.SDK.Configuration;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static ServiceCollection AddWeChatService(this ServiceCollection services, WeChatClientOptions options,Action<WeChatConfiguration> action)
        {
            var config = new WeChatConfiguration();
            action?.Invoke(config);

            services.AddSingleton(config);
            services.AddSingleton(options);
            services.AddTransient<HttpMessageInvoker>();
            services.AddTransient<IWeChatClient, WeChatClient>();
            services.AddHostedService<TokenAccessHostedService>();
            return services;
        }
    }
}