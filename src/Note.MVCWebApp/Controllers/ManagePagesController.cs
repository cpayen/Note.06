using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Note.Core.Enums;
using Note.Core.Services;
using Note.Core.Services.Commands;
using Note.MVCWebApp.Controllers.Base;
using Note.MVCWebApp.Models;
using System;
using System.Threading.Tasks;

namespace Note.MVCWebApp.Controllers
{
    [Authorize]
    [Route("manage/pages")]
    public class ManagePagesController : ManageController
    {
        protected readonly Pages _pages;
        protected readonly ILogger<ManagePagesController> _logger;

        public ManagePagesController(Pages pages, ILogger<ManagePagesController> logger)
        {
            _pages = pages;
            _logger = logger;
        }

        [HttpGet("create"), ActionName("Create")]
        public IActionResult GetCreate(Guid bookId)
        {
            return PartialView("_PageForm", new PageFormViewModel
            {
                BookId = bookId,
                PublicRead = true
            });
        }

        [HttpPost("create"), ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostCreateAsync(PageFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BackWithError("Error", "An error occurred while processing your request.");
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
                _logger.LogError(ex, ex.Message);
                return BackWithError("Error", "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}/edit"), ActionName("Edit")]
        public async Task<IActionResult> GetEditAsync(Guid id)
        {
            var page = await _pages.FindAsync(id);
            return PartialView("_PageForm", new PageFormViewModel(page));
        }

        [HttpPost("{id}/edit"), ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostEditAsync(Guid id, PageFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BackWithError("Error", "An error occurred while processing your request.");
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
                _logger.LogError(ex, "Error");
                return BackWithError("Error", "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}/delete"), ActionName("delete")]
        public async Task<IActionResult> GetDeleteAsync(Guid id)
        {
            var model = await _pages.FindAsync(id);
            return PartialView("_DeletePageForm", model);
        }

        [HttpPost("{id}/delete"), ActionName("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDeleteAsync(Guid id)
        {
            try
            {
                var page = await _pages.DeleteAsync(id);
                return RedirectToAction("Book", "Notes", new { bookSlug = page.Book.Slug });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BackWithError("Error", "An error occurred while processing your request.");
            }
        }
    }
}