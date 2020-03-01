using Microsoft.EntityFrameworkCore;
using Note.Core.Data.Repositories;
using Note.Core.Entities;
using Note.Infra.Data.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Infra.Data.SqlServer.Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly Context _context;

        public BookmarkRepository(Context context)
        {
            _context = context;
        }

        public Bookmark Create(Bookmark bookmark)
        {
            if (bookmark == null)
            {
                throw new ArgumentNullException(nameof(bookmark));
            }

            _context.Bookmarks.Add(bookmark);
            return bookmark;
        }

        public void Delete(Guid id)
        {
            var bookmark = _context.Bookmarks.Find(id);
            if (bookmark != null)
            {
                _context.Bookmarks.Remove(bookmark);
            }
        }

        public async Task<Bookmark> FindAsync(Guid id)
        {
            return await BookmarksWithDependingEntities()
                .SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<ICollection<Bookmark>> GetForUserAsync(string login)
        {
            return await BookmarksWithDependingEntities()
                .Where(o => o.User.Login == login)
                .ToListAsync();
        }

        public async Task<Bookmark> GetForUserAsync(string userLogin, Guid pageId)
        {
            return await BookmarksWithDependingEntities()
                .SingleOrDefaultAsync(o => o.Page.Id == pageId && o.User.Login == userLogin);
        }

        #region Utils

        protected IQueryable<Bookmark> BookmarksWithDependingEntities()
        {
            return _context
                .Bookmarks
                .Include(o => o.User)
                .Include(o => o.Page)
                    .ThenInclude(o => o.Book);
        }

        #endregion
    }
}
