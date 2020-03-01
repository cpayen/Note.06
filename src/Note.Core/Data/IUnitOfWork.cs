using System.Threading.Tasks;
using Note.Core.Data.Repositories;

namespace Note.Core.Data
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IBookRepository BookRepository { get; }
        IPageRepository PageRepository { get; }
        IBookmarkRepository BookmarkRepository { get; }
        Task SaveAsync();
    }
}
