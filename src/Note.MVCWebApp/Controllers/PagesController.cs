using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Note.Core.Services;
using Note.MVCWebApp.Models.Page;

namespace Note.MVCWebApp.Controllers
{
    [Authorize]
    [Route("books/{bookId}/pages")]
    public class PagesController : Controller
    {
        protected readonly Pages _pages;
        protected readonly ILogger<PagesController> _logger;

        public PagesController(Pages pages, ILogger<PagesController> logger)
        {
            _pages = pages;
            _logger = logger;
        }

        //[AllowAnonymous]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> DetailsAsync(Guid id)
        //{
        //    var model = await _books.FindAsync(id);
        //    return View(model);
        //}

        [HttpGet("create")]
        public IActionResult Create(Guid bookId)
        {
            return View(new CreatePageViewModel
            {
                PublicRead = true
            });
        }

        //[HttpPost("create")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateAsync(CreateBookViewModel vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    try
        //    {
        //        var book = await _books.CreateAsync(
        //            new CreateBookCommand(
        //                vm.Name,
        //                vm.Description,
        //                vm.PublicRead ? Access.Public : Access.Private,
        //                vm.PublicWrite ? Access.Public : Access.Private));

        //        return RedirectToAction("Details", new { id = book.Id });
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO: Gérer les exceptions argument etc.
        //        _logger.LogError(ex, ex.Message);
        //        return View();
        //    }
        //}
    }
}