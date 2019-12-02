using Microsoft.EntityFrameworkCore;
using Note.Core.Data.Repositories;
using Note.Core.Entities;
using Note.Core.Enums;
using Note.Infra.Data.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Note.Infra.Data.SqlServer.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly Context _context;

        public PageRepository(Context context)
        {
            _context = context;
        }

        public Page Create(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            _context.Pages.Add(page);
            return page;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Page> FindAsync(Guid id)
        {
            return await PageWithDependingEntities()
                .SingleOrDefaultAsync(o => o.Id == id);
        }

        public Task<ICollection<Page>> FindByAsync(Expression<Func<Book, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Page>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Page Update(Page page)
        {
            throw new NotImplementedException();
        }

        #region Utils

        protected IQueryable<Page> PageWithDependingEntities()
        {
            return _context
                .Pages
                .Include(o => o.Owner)
                .Include(o => o.Book)
                    .ThenInclude(o => o.Owner);
        }

        protected IQueryable<Page> AllowedPages(string login, bool isAdmin = false)
        {
            return PageWithDependingEntities().Where(o => isAdmin || o.ReadAccess == Access.Public || o.Owner.Login == login);
        }

        #endregion
    }
}
