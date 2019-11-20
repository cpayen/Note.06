using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Note.MVCWebApp.Models;

namespace Note.MVCWebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //throw new Exception("Oh noooooon");
            //Response.StatusCode = (int)HttpStatusCode.NotFound;
            //return StatusCode((int)HttpStatusCode.InternalServerError);
            //return NotFound();
            //return Forbid();
            //Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
