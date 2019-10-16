using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Newtonsoft.Json;
using WeiXinBackEnd.SDK.Client.Extensions;
using WeiXinBackEnd.SDK.Client.Message.Base.Attributes;
using WeiXinBackEnd.SDK.Client.Message.Base.Enum;

namespace WeiXinBackEnd.SDK.Client
{
    public class WeChatClientOptions
    {
        public static WeChatClientOptions GetOptionsFromConfig([NotNull]Microsoft.Extensions.Configuration.IConfiguration configuration,[NotNull]string schema)
        {
            var section = configuration.GetSection(schema);
            var configJson = section.ToJObject().ToString();
            return JsonConvert.DeserializeObject<WeChatClientOptions>(configJson);
        }


        public WeChatClientOptions([NotNull]string appId, [NotNull]string appSecret)
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
        public string AccessToken { get; set; }

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