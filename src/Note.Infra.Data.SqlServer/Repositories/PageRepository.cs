using Microsoft.EntityFrameworkCore;
using Note.Core.Data.Repositories;
using Note.Core.Entities;
using Note.Core.Entities.Base;
using Note.Core.Enums;
using Note.Infra.Data.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var page = _context.Pages.Find(id);
            if (page != null)
            {
                _context.Pages.Remove(page);
            }
        }

        public async Task<Page> FindAsync(Guid id)
        {
            return await PagesWithDependingEntities()
                .SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Page> FindAsync(string slug)
        {
            return await PagesWithDependingEntities()
                .SingleOrDefaultAsync(o => o.Slug == slug);
        }

        public async Task<ICollection<Page>> FindByAsync(Expression<Func<Page, bool>> predicate, string login, bool isAdmin = false)
        {
            return await AllowedPagesWithDependingEntities(login, isAdmin)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<ICollection<Page>> FindByAsync(Expression<Func<Page, bool>> predicate)
        {
            return await PagesWithDependingEntities()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<ICollection<Page>> GetAllAsync(string login, bool isAdmin = false)
        {
            return await AllowedPagesWithDependingEntities(login, isAdmin).ToListAsync();
        }

        public Page Update(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            _context.Pages.Update(page);
            return page;
        }

        #region Utils

        protected IQueryable<Page> PagesWithDependingEntities()
        {
            return _context
                .Pages
                .Select(o => new Page
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
                    State = o.State,
                    Book = o.Book
                });
        }

        protected IQueryable<Page> AllowedPagesWithDependingEntities(string login, bool isAdmin = false)
        {
            return _context
                .Pages.Where(o => isAdmin || o.ReadAccess == Access.Public || o.Owner.Login == login)
                .Select(o => new Page
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
                    State = o.State,
                    Book = o.Book
                });
        }

        #endregion
    }
}
