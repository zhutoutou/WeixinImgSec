using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WeiXinBackEnd.Core;

namespace WeiXinBackEnd.Application.Token
{
    public class TokenManager
    {
        private readonly string _appId;
        private readonly string _appSecret;

        private string _accessToken;

        public TokenManager(string appId, string appSecret)
        {
            _appId = appId;
            _appSecret = appSecret;
        }

        /// <summary>
        /// 获取AccessToken  
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessToken()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                await RefreshToken();
            return _accessToken;
        }

        /// <summary>
        /// 刷新AccessToken
        /// </summary>
        /// <returns></returns>
        public async Task RefreshToken()
        {
            using var client = new HttpClient();
            var requestUri = string.Format(GlobalConstants.TokenUrlFormat, _appId, _appSecret);
            var response = await client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            var accessToken = JObject.Parse(result)["access_token"].Value<string>();
            _accessToken = accessToken;
        }
    }
}