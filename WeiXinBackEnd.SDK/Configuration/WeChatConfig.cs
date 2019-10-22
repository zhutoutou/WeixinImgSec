using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Newtonsoft.Json;
using WeiXinBackEnd.SDK.Client.Extensions;
using WeiXinBackEnd.SDK.Client.Message.Base.Attributes;
using WeiXinBackEnd.SDK.Client.Message.Base.Enum;
using WeiXinBackEnd.SDK.Core.Cache;

namespace WeiXinBackEnd.SDK.Configuration
{
    public class WeChatConfig
    {
        /// <summary>
        /// 属性注入
        /// </summary>
        internal IWeChatCache CacheManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="schemaSection"></param>
        /// <returns></returns>
        public static WeChatConfig GetOptionsFromConfig([NotNull]Microsoft.Extensions.Configuration.IConfiguration configuration, [NotNull]string schemaSection)
        {
            schemaSection = schemaSection ?? throw new ArgumentNullException(nameof(schemaSection));

            var section = configuration.GetSection(schemaSection);
            var configJson = section.ToJObject().ToString();
            var config = JsonConvert.DeserializeObject<WeChatConfig>(configJson);
            config.SchemaSection = schemaSection;
            return config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="schemaSection"></param>
        public WeChatConfig([NotNull]string appId, [NotNull]string appSecret, [NotNull]string schemaSection)
        {
            AppId = appId;
            AppSecret = appSecret;
            SchemaSection = schemaSection;
        }


        /// <summary>
        /// SchemaSection
        /// </summary>
        [WeChatRequired]
        [JsonIgnore]
        public string SchemaSection { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        [WeChatRequired]
        public string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        [WeChatRequired]
        public string AppSecret { get; set; }

        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken => CacheManager.Get<WeChatCacheEntry<string>>(WeChatCacheConstants.GetCacheKey(WeChatRefreshTokenConstants.AccessTokenKey)).Value;

        #region Mch Config
        /// <summary>
        /// 商户Id
        /// </summary>
        [WeChatRequired(RequiredType.Mch)]
        public string MchId { get; set; }

        #endregion

        #region Cache

        /// <summary>
        /// 缓存连接字符串
        /// </summary>
        public string CacheConnectionStringSchemaSection { get; set; }

        #endregion

        /// <summary>
        /// 必要性验证
        /// </summary>
        /// <param name="assertType"></param>
        public void Assert(RequiredType assertType)
        {
            GetType().GetProperties().ToList().ForEach(prop =>
            {
                if (assertType.HasFlag(prop.GetCustomAttribute<WeChatRequiredAttribute>()?.WeChatRequiredType ?? RequiredType.None))
                {
                    prop.GetCustomAttribute<WeChatRequiredAttribute>().Valid(prop.GetValue(this));
                }
            });
        }
    }
}