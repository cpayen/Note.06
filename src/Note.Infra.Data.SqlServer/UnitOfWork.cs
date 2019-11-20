using Note.Core.Data;
using Note.Core.Data.Repositories;
using Note.Infra.Data.SqlServer.Repositories;
using Note.Infra.Data.SQLServer.Repositories;
using System.Threading.Tasks;

namespace Note.Infra.Data.SQLServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public UnitOfWork(Context context)
        {
            _context = context;
        }

        private IUserRepository _userRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }


        private IBookRepository _bookRepository;

        public IBookRepository BookRepository
        {
            get
            {
                if (_bookRepository == null)
                {
                    _bookRepository = new BookRepository(_context);
                }
                return _bookRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
