using Note.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Note.Core.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindAsync(Guid id);
        Task<User> FindAsync(string id);
        User Create(User user);
        User Update(User user);
    }
}
