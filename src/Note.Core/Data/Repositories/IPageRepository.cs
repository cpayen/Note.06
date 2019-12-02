using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Note.Core.Entities;

namespace Note.Core.Data.Repositories
{
    public interface IPageRepository
    {
        Task<ICollection<Page>> GetAllAsync();
        Task<ICollection<Page>> FindByAsync(Expression<Func<Book, bool>> predicate);
        Task<Page> FindAsync(Guid id);
        Page Create(Page page);
        Page Update(Page page);
        void Delete(Guid id);
    }
}
