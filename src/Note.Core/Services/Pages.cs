﻿using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Exceptions;
using Note.Core.Helpers;
using Note.Core.Identity;
using Note.Core.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class Pages
    {
        #region Props

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAuth _auth;

        #endregion

        #region Ctor

        public Pages(IUnitOfWork unitOfWork, IAuth auth)
        {
            _unitOfWork = unitOfWork;
            _auth = auth;
        }

        #endregion

        #region CRUD

        public async Task<List<Page>> GetLatestPagesAsync(int pagesCount)
        {
            var pages = await _unitOfWork.PageRepository.FindAllowedLatestAsync(pagesCount, _auth.Login, _auth.IsAdmin);
            return pages.ToList();
        }

        public async Task<Page> FindAsync(Guid id)
        {
            var page = await _unitOfWork.PageRepository.FindAsync(id) ?? throw new NotFoundException(nameof(Page), id);

            if (!_auth.CanRead(page.Book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Book), id);
            }

            return page;
        }

        public async Task<Page> FindAsync(string bookSlug, string pageSlug)
        {
            var page = await _unitOfWork.PageRepository.FindAsync(bookSlug, pageSlug) ?? throw new NotFoundException(nameof(Page), $"{bookSlug}/{pageSlug}");

            if (!_auth.CanRead(page.Book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Book), $"{bookSlug}/{pageSlug}");
            }

            return page;
        }

        public async Task<Page> CreateAsync(CreatePageCommand cmd)
        {
            if (!cmd.IsValid)
            {
                throw new InvalidCommandException(nameof(CreatePageCommand), cmd);
            }

            var book = await _unitOfWork.BookRepository.FindAsync(cmd.BookId);

            if(book == null)
            {
                throw new NotFoundException(nameof(Book), cmd.BookId);
            }

            if(!_auth.CanWrite(book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Book), cmd.BookId);
            }
            
            var page = new Page
            {
                Book = book,
                Title = cmd.Title,
                Slug = await GetUniqueSlugAsync(cmd.Slug, book.Id),
                Type = cmd.Type,
                State = cmd.State,
                Owner = await _auth.GetCurrentUserEntityAsync(),
                CreatedAt = DateTime.Now
            };

            _unitOfWork.PageRepository.Create(page);
            await _unitOfWork.SaveAsync();

            return page;
        }

        public async Task<Page> UpdateAsync(UpdatePageCommand cmd)
        {
            if (!cmd.IsValid)
            {
                throw new InvalidCommandException(nameof(UpdatePageCommand), cmd);
            }

            var page = await _unitOfWork.PageRepository.FindAsync(cmd.Id) ?? throw new NotFoundException(nameof(Page), cmd.Id);

            if (!_auth.CanWrite(page.Book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Page), page.Id);
            }

            page.Title = cmd.Title;
            page.Slug = await GetUniqueSlugAsync(cmd.Slug, page.Book.Id, page.Id);
            page.Type = cmd.Type;
            page.State = cmd.State;
            page.UpdatedAt = DateTime.Now;

            _unitOfWork.PageRepository.Update(page);
            await _unitOfWork.SaveAsync();

            return page;
        }

        public async Task<Page> UpdateContentAsync(UpdatePageContentCommand cmd)
        {
            if (!cmd.IsValid)
            {
                throw new InvalidCommandException(nameof(UpdatePageContentCommand), cmd);
            }

            var page = await _unitOfWork.PageRepository.FindAsync(cmd.Id) ?? throw new NotFoundException(nameof(Page), cmd.Id);

            if (!_auth.CanWrite(page.Book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Page), cmd.Id);
            }

            page.Content = cmd.Content;
            page.UpdatedAt = DateTime.Now;

            _unitOfWork.PageRepository.Update(page);
            await _unitOfWork.SaveAsync();

            return page;
        }

        public async Task<Page> DeleteAsync(Guid id)
        {
            var page = await _unitOfWork.PageRepository.FindAsync(id) ?? throw new NotFoundException(nameof(Page), id);

            if (!_auth.CanWrite(page.Book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Page), id);
            }

            _unitOfWork.PageRepository.Delete(id);
            await _unitOfWork.SaveAsync();

            return page;
        }

        #endregion

        #region Utils

        public async Task<string> GetUniqueSlugAsync(string slug, Guid bookId, Guid? pageId = null)
        {
            var pagesWithSameSlugs = await _unitOfWork.PageRepository.FindByAsync(o => o.Book.Id == bookId && o.Id != pageId && o.Slug.StartsWith(slug));
            var existingSlugs = pagesWithSameSlugs.Select(o => o.Slug);

            if(!existingSlugs.Any(o => o == slug))
            {
                return slug;
            }

            return SlugHelper.GetUniqueSlug(slug, existingSlugs);
        }

        #endregion
    }
}
