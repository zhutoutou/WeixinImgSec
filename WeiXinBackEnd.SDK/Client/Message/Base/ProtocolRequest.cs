using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace WeiXinBackEnd.SDK.Client.Message.Base
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

   
    }
}