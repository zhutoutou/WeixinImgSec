using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using WeiXinBackEnd.SDK.Client.Extensions;
using WeiXinBackEnd.SDK.Configuration;
using WeiXinBackEnd.SDK.Core.Cache;
using WeiXinBackEnd.SDK.Core.Cache.MSCache;
using WeiXinBackEnd.SDK.Core.Cache.RedisCache;

namespace WeiXinBackEnd.SDK
{
    public static class WeChatConstants
    {
        /// <summary>
        /// ����WeChat����
        /// </summary>

        public static Action<IServiceCollection, IConfiguration, WeChatConfig> UseMSCache =
            (services, configuration, config) =>
            {
                services.AddSingleton(new MemoryCache(new MemoryCacheOptions()));
                services.AddTransient(typeof(IWeChatCache), typeof(DefaultWeChatCache));
            };

        /// <summary>
        /// ����Redis����
        /// </summary>
        public static Action<IServiceCollection, IConfiguration, WeChatConfig> UseRedisCache =
            (services, configuration, config) =>
            {
                var redisSection = string.IsNullOrWhiteSpace(config.CacheConnectionStringSchemaSection)
                    ? throw new ArgumentNullException(nameof(config.CacheConnectionStringSchemaSection))
                    : string.Join(":", config.SchemaSection, config.CacheConnectionStringSchemaSection);
                string redisConfigJsonString = configuration.GetSection(redisSection).ToJObject().ToString();
                if (string.IsNullOrWhiteSpace(redisConfigJsonString))
                    throw new ArgumentNullException(nameof(redisConfigJsonString));
                var redisConfig = JsonConvert.DeserializeObject<RedisConfiguration>(redisConfigJsonString);
                services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfig);
                services.AddTransient(typeof(IWeChatCache), typeof(WeChatRedisCache));
            };
    }

    internal static class WeChatConfigurationConstants
    {
        public const int DefaultRefreshTimeSpan = 70;
    }

    internal static class WeChatCacheConstants
    {
        public const string DefaultSetFailErrorMessage = "�������ʧ��,�������{0}";
        public const string LockKeyPrefix = "lock_";
        public const string CacheKeyPrefix = "cache_";
        public const int DefaultLockTimeSpan = 5000;

        #region KeyConcat

        public static string GetLockKey(string key)
        {
            return LockKeyPrefix + key;
        }

        public static string GetCacheKey(string key)
        {
            return CacheKeyPrefix + key;
        }

        #endregion

    }
    /// <summary>
    /// ΢�ŵ�½Constants
    /// </summary>
    internal static class WeChatLoginConstants
    {
        public const string Address = "https://api.weixin.qq.com/sns/jscode2session";
        public const string GrantType = "authorization_code";
        public const string SuccessLog = "RequestLoginAsync����ɹ�";
    }

    /// <summary>
    /// ΢�ŵ�½Constants
    /// </summary>
    internal static class WeChatRefreshTokenConstants
    {
        public const string Address = "https://api.weixin.qq.com/cgi-bin/token";
        public const string GrantType = "client_credential";
        public const string SuccessLog = "RequestRefreshToken����ɹ�";

        #region Cache

        public const string AccessTokenKey = "access_token";
        public const string RefreshAccessTokenCacheSuccess = "�������ɹ�,�ɹ�ˢ��Token";
        public const string RefreshAccessTokenCacheCancel = "������ʧ��,ȡ��ˢ��Token";

        #endregion
    }

    /// <summary>
    /// ͼƬ���
    /// </summary>
    internal static class WeChatImgSecConstants
    {
        public const string Address = "https://api.weixin.qq.com/wxa/img_sec_check";
        public const string ImageMediaType = "image/jpeg";
        public const string SuccessLog = "RequestImgSec����ɹ�";
    }

}