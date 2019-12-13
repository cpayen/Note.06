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
    [Route("manage/books")]
    public class ManageBooksController : ManageController
    {
        protected readonly Books _books;
        protected readonly ILogger<ManageBooksController> _logger;

        public ManageBooksController(Books books, ILogger<ManageBooksController> logger)
        {
            _books = books;
            _logger = logger;
        }

        [HttpGet("create"), ActionName("Create")]
        public IActionResult GetCreate()
        {
            return PartialView("_BookForm", new BookFormViewModel
            {
                PublicRead = true
            });
        }

        [HttpPost("create"), ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostCreateAsync(BookFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BackWithError("Error", "An error occurred while processing your request.");
            }

            try
            {
                var book = await _books.CreateAsync(
                    new CreateBookCommand(
                        vm.Title,
                        vm.Slug,
                        vm.Description,
                        vm.PublicRead ? Access.Public : Access.Private,
                        vm.PublicWrite ? Access.Public : Access.Private));
                
                return RedirectToAction("Book", "Notes", new { bookSlug = book.Slug });
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
            var book = await _books.FindAsync(id);
            return PartialView("_BookForm", new BookFormViewModel(book));
        }

        [HttpPost("{id}/edit"), ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostEditAsync(Guid id, BookFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BackWithError("Error", "An error occurred while processing your request.");
            }

            try
            {
                var book = await _books.UpdateAsync(
                    new UpdateBookCommand(
                        id,
                        vm.Title,
                        vm.Slug,
                        vm.Description,
                        vm.PublicRead ? Access.Public : Access.Private,
                        vm.PublicWrite ? Access.Public : Access.Private));

                return RedirectToAction("Book", "Notes", new { bookSlug = book.Slug });
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
            var model = await _books.FindAsync(id);
            return PartialView("_DeleteBookForm", model);
        }

        [HttpPost("{id}/delete"), ActionName("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDeleteAsync(Guid id)
        {
            try
            {
                var book = await _books.DeleteAsync(id);
                return RedirectToAction("Index", "Notes");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BackWithError("Error", "An error occurred while processing your request.");
            }
        }
    }
}