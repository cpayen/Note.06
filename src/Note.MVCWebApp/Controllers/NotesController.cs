﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Note.Core.Services;
using System;
using System.Threading.Tasks;

namespace Note.MVCWebApp.Controllers
{
    [AllowAnonymous]
    [Route("notes")]
    public class NotesController : Controller
    {
        protected readonly Books _books;
        protected readonly Pages _pages;
        protected readonly ILogger<ManageBooksController> _logger;

        public NotesController(Books books, Pages pages, ILogger<ManageBooksController> logger)
        {
            _books = books;
            _pages = pages;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _books.GetAllAsync();
            return View(model);
        }

        [HttpGet("{bookSlug}")]
        public async Task<IActionResult> BookAsync(string bookSlug)
        {
            var model = await _books.FindAsync(bookSlug);
            return View(model);
        }

        [HttpGet("{bookSlug}/{pageSlug}")]
        public async Task<IActionResult> PageAsync(string bookSlug, string pageSlug)
        {
            var model = await _pages.FindAsync(pageSlug);
            return View(model);
        }
    }
}