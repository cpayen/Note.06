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
                .Select(o => new Book
                {
                    Id = o.Id,
                    Owner = o.Owner,
                    Title = o.Title,
                    Slug = o.Slug,
                    Description = o.Description,
                    ReadAccess = o.ReadAccess,
                    WriteAccess = o.WriteAccess,
                    CreatedAt = o.CreatedAt,
                    UpdatedAt = o.UpdatedAt,
                    Pages = o.Pages.Select(p => new Page
                    {
                        Id = p.Id,
                        Book = o,
                        Owner = p.Owner,
                        Title = p.Title,
                        Slug = p.Slug,
                        Description = p.Description,
                        ReadAccess = p.ReadAccess,
                        WriteAccess = p.WriteAccess,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt,
                        State = p.State
                    })
                });
        }

        protected IQueryable<Book> AllowedBooksWithDependingEntities(string login, bool isAdmin = false)
        {
            return _context
                   .Books.Where(o => isAdmin || o.ReadAccess == Access.Public || o.Owner.Login == login)
                   .Select(o => new Book
                   {
                       Id = o.Id,
                       Owner = o.Owner,
                       Title = o.Title,
                       Slug = o.Slug,
                       Description = o.Description,
                       ReadAccess = o.ReadAccess,
                       WriteAccess = o.WriteAccess,
                       CreatedAt = o.CreatedAt,
                       UpdatedAt = o.UpdatedAt,
                       Pages = o.Pages.Where(o => isAdmin || o.ReadAccess == Access.Public || o.Owner.Login == login).Select(p => new Page
                       {
                           Id = p.Id,
                           Book = o,
                           Owner = p.Owner,
                           Title = p.Title,
                           Slug = p.Slug,
                           Description = p.Description,
                           ReadAccess = p.ReadAccess,
                           WriteAccess = p.WriteAccess,
                           CreatedAt = p.CreatedAt,
                           UpdatedAt = p.UpdatedAt,
                           State = p.State
                       })
                   });
        }

        #endregion
    }
}
