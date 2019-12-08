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
    [Route("books")]
    public class BooksController : Controller
    {
        protected readonly Books _books;
        protected readonly ILogger<BooksController> _logger;

        public BooksController(Books books, ILogger<BooksController> logger)
        {
            _books = books;
            _logger = logger;
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(BookFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
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
                //TODO: Gérer les exceptions argument etc.
                _logger.LogError(ex, ex.Message);
                return View();
            }
        }

        [HttpPost("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, BookFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
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
                var book = await _books.DeleteAsync(id);
                return RedirectToAction("Index", "Notes");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View();
            }
        }
    }
}