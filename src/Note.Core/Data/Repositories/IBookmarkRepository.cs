using Note.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Core.Data.Repositories
{
    public interface IBookmarkRepository
    {
        Task<ICollection<Bookmark>> GetForUserAsync(string login);
        Task<Bookmark> GetForUserAsync(string login, Guid pageId);
        Task<Bookmark> FindAsync(Guid id);
        Bookmark Create(Bookmark bookmark);
        void Delete(Guid id);
    }
}
