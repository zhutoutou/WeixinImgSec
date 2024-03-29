﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeiXinBackEnd.Application.UserAccount;
using WeiXinBackEnd.Application.UserAccount.Dto;
using WeiXinBackEnd.SDK.Client;

namespace WeiXinBackEnd.Controllers
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    [Route("api/[controller]")]
    public class UserAccountController:Controller
    {

        private readonly IUserAccountApp _userAccount;

        public UserAccountController(IUserAccountApp userAccount)
        {
            _userAccount = userAccount;
        }

        [HttpGet("Login")]
        public async Task<ActionResult<LoginResponse>> Login(Application.UserAccount.Dto.LoginInput input)
        {
            var result = await _userAccount.Login(input).ConfigureAwait(false);
            return Ok(result);
        }
    }
}