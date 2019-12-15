using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Note.Core.Entities;

namespace Note.Core.Data.Repositories
{
    public interface IBookRepository
    {
        Task<ICollection<Book>> GetAllAsync(string login, bool isAdmin = false);
        Task<ICollection<Book>> FindByAsync(Expression<Func<Book, bool>> predicate, string login, bool isAdmin = false);
        Task<ICollection<Book>> FindByAsync(Expression<Func<Book, bool>> predicate);
        Task<Book> FindAsync(Guid id);
        Task<Book> FindAsync(string slug);
        Book Create(Book book);
        Book Update(Book book);
        void Delete(Guid id);
    }
}
