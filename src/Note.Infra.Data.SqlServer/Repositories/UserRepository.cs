using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Note.Core.Data.Repositories;
using Note.Core.Entities;

namespace Note.Infra.Data.SQLServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<User> FindAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> FindAsync(string login)
        {
            return await _context.Users.Where(o => o.Login == login).FirstOrDefaultAsync();
        }

        public User Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
            return user;
        }

        public User Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Update(user);
            return user;
        }
    }
}
