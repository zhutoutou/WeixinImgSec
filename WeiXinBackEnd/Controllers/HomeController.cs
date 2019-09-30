using System;
using Microsoft.AspNetCore.Mvc;

namespace WeiXinBackEnd.Controllers
{
    public class HomeController:Controller
    {
        [HttpGet("Home/Index")]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}