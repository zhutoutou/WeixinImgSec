using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using WeiXinBackEnd.SDK.Client.Extensions;

namespace WeiXinBackEnd.SDK.Client.Message
{
    public class WeChatRequest:HttpRequestMessage
    {

        /// <summary>
        /// Gets or sets the endpoint address (you can also set the RequestUri instead or leave blank to use the HttpClient base address).
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// Gets or sets additional protocol parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public IDictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public WeChatRequest Clone()
        {
            return Clone<WeChatRequest>();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public T Clone<T>()
            where T : WeChatRequest, new()
        {
            var clone = new T
            {
                RequestUri = RequestUri,
                Version = Version,
                Method = Method,
                Address = Address,
                AppId = AppId,
                AppSecret = AppSecret,
                Parameters = new Dictionary<string, string>(),
            };

            if (Parameters != null)
            {
                foreach (var item in Parameters) clone.Parameters.Add(item);
            }

            clone.Headers.Clear();
            foreach (var header in Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            if (Properties != null && Properties.Any())
            {
                foreach (var property in Properties)
                {
                    clone.Properties.Add(property);
                }
            }

            return clone;
        }

        public virtual void Prepare()
        {
            if (Address.IsPresent())
            {
                RequestUri = new Uri(Address);
            }

            if (Parameters.Any())
            {
                Content = new FormUrlEncodedContent(Parameters);
            }
        }
    }
}