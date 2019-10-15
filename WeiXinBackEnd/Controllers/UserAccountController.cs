using Microsoft.AspNetCore.Mvc;
using WeiXinBackEnd.SDK.Client;
using WeiXinBackEnd.SDK.Client.Message.WeChatLogin;

namespace WeiXinBackEnd.Controllers
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    [Route("api/[controller]")]
    public class UserAccountController:Controller
    {
        private readonly IWeChatClient _weChatClient;

        public UserAccountController(IWeChatClient weChatClient)
        {
            _weChatClient = weChatClient;
        }

        [HttpGet("Login")]
        public async IActionResult<WeChatLoginResponse> Login(string loginCode)
        {
            var input = new WeChatLoginInput() {LoginCode = loginCode};
            var result = await _weChatClient.RequestLoginAsync(input).ConfigureAwait(false);
            return Ok(result.Result);
        }
    }
}