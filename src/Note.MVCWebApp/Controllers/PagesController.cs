using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Note.Core.Enums;
using Note.Core.Services;
using Note.Core.Services.Commands;
using Note.MVCWebApp.Models;
using System;
using System.Threading.Tasks;

namespace Note.MVCWebApp.Controllers
{
    [Authorize]
    [Route("pages")]
    public class PagesController : Controller
    {
        protected readonly Pages _pages;
        protected readonly ILogger<PagesController> _logger;

        public PagesController(Pages pages, ILogger<PagesController> logger)
        {
            _pages = pages;
            _logger = logger;
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(PageFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var page = await _pages.CreateAsync(
                    new CreatePageCommand(
                        vm.BookId,
                        vm.Title,
                        vm.Slug,
                        vm.PublicRead ? Access.Public : Access.Private,
                        vm.PublicWrite ? Access.Public : Access.Private));

                return RedirectToAction("Page", "Notes", new { bookSlug = page.Book.Slug, pageSlug = page.Slug });
            }
            catch (Exception ex)
            {
                //TODO: Gérer les exceptions argument etc.
                _logger.LogError(ex, ex.Message);
                return View();
            }
        }

        [HttpPost("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, PageFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var page = await _pages.UpdateAsync(
                    new UpdatePageCommand(
                        id,
                        vm.Title,
                        vm.Slug,
                        vm.PublicRead ? Access.Public : Access.Private,
                        vm.PublicWrite ? Access.Public : Access.Private));

                return RedirectToAction("Page", "Notes", new { bookSlug = page.Book.Slug, pageSlug = page.Slug });
            }
            catch (Exception ex)
            {
                //TODO: Gérer les exceptions argument etc.
                _logger.LogError(ex, "Error");
                return View();
            }
        }

        [HttpPost("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var page = await _pages.DeleteAsync(id);
                return RedirectToAction("Book", "Notes", new { bookSlug = page.Book.Slug });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View();
            }
        }
    }
}