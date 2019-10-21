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
        /// 采用属性注入
        /// </summary>
        internal IWeChatCache CacheManager { get; set; }

        public static WeChatConfig GetOptionsFromConfig([NotNull]Microsoft.Extensions.Configuration.IConfiguration configuration,[NotNull]string schema)
        {
            var section = configuration.GetSection(schema);
            var configJson = section.ToJObject().ToString();
            return JsonConvert.DeserializeObject<WeChatConfig>(configJson);
        }


        public WeChatConfig([NotNull]string appId, [NotNull]string appSecret)
        {
            AppId = appId;
            AppSecret = appSecret;
        }

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
        public string AccessToken => CacheManager.Get<WeChatCacheEntry<string>>(WeChatRefreshTokenConstants.AccessTokenCacheKey).Value;

        #region Mch Config
        /// <summary>
        /// 商户Id
        /// </summary>
        [WeChatRequired(RequiredType.Mch)]
        public string MchId { get; set; }

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