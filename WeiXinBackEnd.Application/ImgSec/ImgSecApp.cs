using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeiXinBackEnd.Application.ImgSec.Dto;
using WeiXinBackEnd.Application.Token;
using WeiXinBackEnd.Core;
using WeiXinBackEnd.Core.Extension;

namespace WeiXinBackEnd.Application.ImgSec
{
    public class ImgSecApp : IImgSecApp
    {
        private readonly TokenManager _tokenManager;

        public ImgSecApp(TokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }
        /// <summary>
        /// 图片安全认证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ImgSecAuthResponse> AuthAsync(ImgSecAuthInput input)
        {
            using var client = new HttpClient();
            var boundary = Guid.NewGuid().ToString();
            var boundaryParameter = new NameValueHeaderValue("boundary", boundary);
            MultipartFormDataContent formData = new MultipartFormDataContent(boundary);

            if(input.File == null)
            {
                if (input.FileUrl.IsNullOrWhiteSpace())
                    throw new Exception($"{nameof(input.File)},{nameof(input.FileUrl)}不能同时为空");
                using var imgClient = new HttpClient(); 
                input.File = await (await imgClient.GetAsync(input.FileUrl)).Content.ReadAsByteArrayAsync();
            }
            var imageContent = new ByteArrayContent(input.File);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(GlobalConstants.ImageMediaType);
            formData.Add(imageContent, "\"media\"", $"\"{input.FileName}\"");
            formData.Headers.ContentType.Parameters.Clear();
            formData.Headers.ContentType.Parameters.Add(boundaryParameter);
            var accessToken = await _tokenManager.GetAccessToken();
            var response = await client.PostAsync(string.Format(GlobalConstants.ImgSecUrlFormat, accessToken),
                formData);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ImgSecAuthResponse>(result);
        }
    }
}
