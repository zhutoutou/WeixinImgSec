using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using WeiXinBackEnd.SDK.Client.Extensions;
using WeiXinBackEnd.SDK.Client.Message.Base;
using WeiXinBackEnd.SDK.Client.Message.Base.Attributes;

namespace WeiXinBackEnd.SDK.Client.Message.WeChatImgSec
{
    public class WeChatImgSecRequest:WeChatRequest
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        [Required]
        public byte[] File { get; set; }

        /// <summary>
        /// access_token
        /// </summary>
        [Required]
        [WeChatParameterName("access_token")]
        public string AccessToken { get; set; }

        public override void Prepare()
        {
            // 验证参数
            CheckValidation();
            // 构建参数
            ConstructParam();

            Method = HttpMethod.Post;

            var boundary = Guid.NewGuid().ToString();
            var boundaryParameter = new NameValueHeaderValue("boundary", boundary);
            MultipartFormDataContent formData = new MultipartFormDataContent(boundary);

            var imageContent = new ByteArrayContent(File);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(WeChatImgSecConstants.ImageMediaType);
            formData.Add(imageContent, "\"media\"", $"\"{FileName}\"");
            formData.Headers.ContentType.Parameters.Clear();
            formData.Headers.ContentType.Parameters.Add(boundaryParameter);
            Content = formData;

            Address = $"{Address}?{Parameters.ToQueryString()}";
        }
    }
}