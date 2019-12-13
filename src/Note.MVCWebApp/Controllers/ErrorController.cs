using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Note.MVCWebApp.Models;
using System;
using System.Diagnostics;
using System.Net;

namespace Note.MVCWebApp.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        protected readonly ILogger<HomeController> _logger;

        public ErrorController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Unhandled application exceptions are sent here by "UseExceptionHandler" middleware. 
        /// Provides a generic error page for each status. 
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionFeature != null)
            {
                _logger.LogError(exceptionFeature.Error, exceptionFeature.Error.Message);
            }

            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Errors are sent here by "UseStatusCodePagesWithReExecute" middleware. 
        /// Provides a generic error page for each status. 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [Route("Error/{status}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StatusError(int status)
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionFeature != null)
            {
                _logger.LogError(exceptionFeature.Error, exceptionFeature.Error.Message);
            }

            Response.StatusCode = status;
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}