using Note.Core.Entities;
using Note.Core.Entities.Base;
using System.Threading.Tasks;

namespace Note.Core.Identity
{
    public interface IAuth
    {
        string Email { get; }
        string FirstName { get; }
        string FullName { get; }
        bool IsAdmin { get; }
        bool IsAuthenticated { get; }
        string LastName { get; }
        string Login { get; }

        bool CanRead(IOwned item);
        bool CanWrite(IOwned item);
        bool HasRole(string role);
        bool Owns(IOwned item);

        Task<User> GetCurrentUserEntityAsync();
    }
}