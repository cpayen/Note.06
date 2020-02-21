using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Note.Core.Entities;

namespace Note.Core.Data.Repositories
{
    public interface IPageRepository
    {
        Task<ICollection<Page>> FindAllowedByAsync(Expression<Func<Page, bool>> predicate, string login, bool isAdmin = false);
        Task<ICollection<Page>> FindAllowedLatestAsync(int pagesCount, string login, bool isAdmin = false);
        Task<ICollection<Page>> FindByAsync(Expression<Func<Page, bool>> predicate);
        Task<Page> FindAsync(Guid id);
        Task<Page> FindAsync(string bookSlug, string pageSlug);
        Page Create(Page page);
        Page Update(Page page);
        void Delete(Guid id);
    }
}
