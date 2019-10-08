using System;
using Microsoft.AspNetCore.Mvc;

namespace WeiXinBackEnd.Controllers
{
    /// <summary>
    /// Api主页
    /// </summary>
    public class HomeController:Controller
    {
        /// <summary>
        /// Api文档
        /// </summary>
        /// <returns></returns>
        [HttpGet("Home/Index")]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}