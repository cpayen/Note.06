using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Note.Core.Entities;

namespace Note.Core.Data.Repositories
{
    public interface IPageRepository
    {
        Task<ICollection<Page>> GetAllAsync(string login, bool isAdmin = false);
        Task<ICollection<Page>> FindByAsync(Expression<Func<Page, bool>> predicate, string login, bool isAdmin = false);
        Task<Page> FindAsync(Guid id);
        Task<Page> FindAsync(string slug);
        Page Create(Page page);
        Page Update(Page page);
        void Delete(Guid id);
    }
}
