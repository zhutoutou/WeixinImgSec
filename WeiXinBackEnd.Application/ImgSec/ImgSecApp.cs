using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeiXinBackEnd.Application.ImgSec.Dto;
using WeiXinBackEnd.Core.Extension;
using WeiXinBackEnd.SDK.Client;
using WeiXinBackEnd.SDK.Client.Message.ImgSec;

namespace WeiXinBackEnd.Application.ImgSec
{
    public class ImgSecApp : IImgSecApp
    {
        private readonly IWeChatClient _weChatClient;

        public ImgSecApp(IWeChatClient weChatClient)
        {
            _weChatClient = weChatClient;
        }
        /// <summary>
        /// 图片安全认证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ImgSecAuthResponse> AuthAsync(ImgSecAuthInput input)
        {
            if (input.File == null)
            {
                if (input.FileUrl.IsNullOrWhiteSpace())
                    throw new Exception($"{nameof(input.File)},{nameof(input.FileUrl)}不能同时为空");
                using var imgClient = new HttpClient();
                input.File = await (await imgClient.GetAsync(input.FileUrl)).Content.ReadAsByteArrayAsync();
            }
            var result = await _weChatClient.RequestImgSecAsync(new WeChatImgSecInput()
            {
                File =  input.File,
                FileName = input.FileName
            }).ConfigureAwait(false);
            return new ImgSecAuthResponse()
            {
                ErrCode = result.ErrorCode?.ToString(),
                ErrMsg = result.Error,
            };
        }
    }
}
