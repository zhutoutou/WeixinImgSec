using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using WeiXinBackEnd.SDK.Client.Extensions;

namespace WeiXinBackEnd.SDK.Client.Message
{
    public abstract class ProtocolRequest : HttpRequestMessage
    {
        /// <summary>
        /// Gets or sets additional protocol parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public IDictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the endpoint address (you can also set the RequestUri instead or leave blank to use the HttpClient base address).
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        public abstract void Prepare();

        protected string ToQueryString(IDictionary<string, string> parameters)
        {
            return string.Join("&", parameters.Select(v => $"{v.Key}={v.Value}"));
        }
    }
}