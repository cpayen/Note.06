using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Exceptions;
using Note.Core.Identity;
using Note.Core.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class Books
    {
        #region Props

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAuth _auth;

        #endregion

        #region Ctor

        public Books(IUnitOfWork unitOfWork, IAuth auth)
        {
            _unitOfWork = unitOfWork;
            _auth = auth;
        }

        #endregion

        #region CRUD

        public async Task<List<Book>> GetAllAsync()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync(_auth.Login, _auth.IsAdmin);
            return books.ToList();
        }

        public async Task<Book> FindAsync(Guid id)
        {
            var book = await _unitOfWork.BookRepository.FindAsync(id) ?? throw new NotFoundException(nameof(Book), id);

            if (!_auth.CanRead(book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Book), id);
            }

            return book;
        }

        public async Task<Book> FindAsync(string slug)
        {
            var book = await _unitOfWork.BookRepository.FindAsync(slug) ?? throw new NotFoundException(nameof(Book), slug);

            if (!_auth.CanRead(book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Book), slug);
            }

            return book;
        }

        public async Task<Book> CreateAsync(CreateBookCommand cmd)
        {
            if(!cmd.IsValid)
            {
                throw new InvalidCommandException(nameof(CreateBookCommand), cmd);
            }

            var book = new Book
            {
                Title = cmd.Title,
                Slug = cmd.Slug,
                Description = cmd.Description,
                ReadAccess = cmd.ReadAccess,
                WriteAccess = cmd.WriteAccess,
                Owner = await _auth.GetCurrentUserEntityAsync(),
                CreatedAt = DateTime.Now
            };

            _unitOfWork.BookRepository.Create(book);
            await _unitOfWork.SaveAsync();
            
            return book;
        }

        public async Task<Book> UpdateAsync(UpdateBookCommand cmd)
        {
            if (!cmd.IsValid)
            {
                throw new InvalidCommandException(nameof(UpdateBookCommand), cmd);
            }

            var book = await _unitOfWork.BookRepository.FindAsync(cmd.Id) ?? throw new NotFoundException(nameof(Book), cmd.Id);

            if(!_auth.CanWrite(book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Book), cmd.Id);
            }

            book.Title = cmd.Title;
            book.Slug = cmd.Slug;
            book.Description = cmd.Description;
            book.ReadAccess = cmd.ReadAccess;
            book.WriteAccess = cmd.WriteAccess;
            book.UpdatedAt = DateTime.Now;

            _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.SaveAsync();

            return book;
        }

        public async Task<Book> DeleteAsync(Guid id)
        {
            var book = await _unitOfWork.BookRepository.FindAsync(id) ?? throw new NotFoundException(nameof(Book), id);

            if (!_auth.CanWrite(book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Book), id);
            }

            _unitOfWork.BookRepository.Delete(id);
            await _unitOfWork.SaveAsync();

            return book;
        }

        #endregion

        #region Utils

        public async Task<bool> CheckSlugUnicityAsync(string slug)
        {
            var exists = await _unitOfWork.BookRepository.FindByAsync(o => o.Slug == slug);
            return exists.Any() ? false : true;
        }

        #endregion
    }
}
