using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Note.Core.Data.Repositories;
using Note.Core.Entities;
using Note.Core.Entities.Base;
using Note.Core.Enums;
using Note.Infra.Data.SQLServer;

namespace Note.Infra.Data.SqlServer.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly Context _context;

        public BookRepository(Context context)
        {
            _context = context;
        }

        public Book Create(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _context.Books.Add(book);
            return book;
        }

        public void Delete(Guid id)
        {
            var book = _context.Books.Find(id);
            if(book != null)
            {
                _context.Books.Remove(book);
            }
        }

        public async Task<Book> FindAsync(Guid id)
        {
            return await BooksWithDependingEntities()
                .SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Book> FindAsync(string slug)
        {
            return await BooksWithDependingEntities()
                .SingleOrDefaultAsync(o => o.Slug == slug);
        }

        public async Task<ICollection<Book>> FindByAsync(Expression<Func<Book, bool>> predicate, string login, bool isAdmin = false)
        {
            return await AllowedBooksWithDependingEntities(login, isAdmin)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<ICollection<Book>> FindByAsync(Expression<Func<Book, bool>> predicate)
        {
            return await BooksWithDependingEntities()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<ICollection<Book>> GetAllAsync(string login, bool isAdmin = false)
        {
            return await AllowedBooksWithDependingEntities(login, isAdmin).ToListAsync();
        }

        public Book Update(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _context.Books.Update(book);
            return book;
        }

        #region Utils

        protected IQueryable<Book> BooksWithDependingEntities()
        {
            return _context
                .Books
                .Include(o => o.Owner)
                .Include(o => o.Pages)
                    .ThenInclude(o => o.Owner);
        }

        protected IQueryable<Book> AllowedBooksWithDependingEntities(string login, bool isAdmin = false)
        {
            return _context
                .Books.Where(o => HasAccess(o, login, isAdmin))
                .Include(o => o.Owner)
                .Include(o => o.Pages.Where(p => HasAccess(p, login, isAdmin)))
                    .ThenInclude(o => o.Owner);
        }

        protected static bool HasAccess(IOwned target, string login, bool isAdmin = false)
        {
            return isAdmin || target.ReadAccess == Access.Public || target.Owner.Login == login;
        }

        #endregion
    }
}
