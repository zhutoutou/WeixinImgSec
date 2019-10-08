using Microsoft.AspNetCore.Mvc;

namespace WeiXinBackEnd.Controllers
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    [Route("api/[controller]")]
    public class UserAccountController:Controller
    {
        [HttpGet("Login")]
        public async IActionResult Login(string loginCode)
        {

        }
    }
}