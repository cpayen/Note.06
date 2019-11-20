using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Note.Core.Enums;
using Note.Core.Services;
using Note.Core.Services.Commands;
using Note.MVCWebApp.Models.Book;
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

        [AllowAnonymous]
        [HttpGet()]
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _books.GetAllAsync();
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> DetailsAsync(Guid id)
        {
            var model = await _books.FindAsync(id);
            return View(model);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new CreateBookViewModel
            {
                PublicRead = true
            });
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CreateBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var book = await _books.CreateAsync(
                    new CreateBookCommand(
                        vm.Name,
                        vm.Description,
                        vm.PublicRead ? Access.Public : Access.Private,
                        vm.PublicWrite ? Access.Public : Access.Private));

                return RedirectToAction("Details", new { id = book.Id });
            }
            catch (Exception ex)
            {
                //TODO: Gérer les exceptions argument etc.
                _logger.LogError(ex, ex.Message);
                return View();
            }
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var book = await _books.FindAsync(id);
            var model = new UpdateBookViewModel
            {
                Id = book.Id,
                Name = book.Name,
                Description = book.Description,
                PublicRead = book.ReadAccess == Access.Public,
                PublicWrite = book.WriteAccess == Access.Public
            };
            return View(model);
        }

        [HttpPost("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, UpdateBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var book = await _books.UpdateAsync(
                    new UpdateBookCommand(
                        id,
                        vm.Name,
                        vm.Description,
                        vm.PublicRead ? Access.Public : Access.Private,
                        vm.PublicWrite ? Access.Public : Access.Private));

                return RedirectToAction("Details", new { id = book.Id });
            }
            catch (Exception ex)
            {
                //TODO: Gérer les exceptions argument etc.
                _logger.LogError(ex, "Error");
                return View();
            }
        }

        [HttpGet("{id}/delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var model = await _books.FindAsync(id);
            return View(model);
        }

        [HttpPost("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExecDeleteAsync(Guid id)
        {
            try
            {
                await _books.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View();
            }
        }
    }
}