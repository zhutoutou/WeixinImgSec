using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WeiXinBackEnd.SDK.Client.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddWeChat(this ServiceCollection services)
        {
            services.AddTransient<HttpMessageInvoker>();
            services.AddSingleton<WeChatClientOptions>();
            services.AddTransient<IWeChatClient, WeChatClient>();
        }
    }
}