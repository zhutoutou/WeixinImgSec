﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeiXinBackEnd.Application.ImgSec;
using WeiXinBackEnd.Application.ImgSec.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeiXinBackEnd.Controllers
{
    /// <summary>
    /// 图片安全
    /// </summary>
    [Route("api/[controller]")]
    public class ImgSecController : Controller
    {
        private readonly IImgSecApp _imgSec;

        public ImgSecController(IImgSecApp imgSec)
        {
            _imgSec = imgSec;
        }


        //// POST api/<controller>
        //[HttpPost]
        //public async Task<ImgSecAuthResponse> Post([FromBody]ImgSecAuthInput input)
        //{
        //    return await _imgSec.AuthAsync(input);
        //}

        // POST api/<controller>

        /// <summary>
        /// 文件上传审核
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ImgSecAuthResponse> UpLoad()
        {
            var files = Request.Form.Files;
            if (!files.Any()) 
                throw new Exception("没有上传任何文件");
            
            byte[] buffer = new byte[65530];
            var file = files.First();
            await file.OpenReadStream().ReadAsync(buffer);
            
            return await _imgSec.AuthAsync(new ImgSecAuthInput(file.FileName, buffer));
        }

    }
}
