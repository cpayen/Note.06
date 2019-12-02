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
        private IUserRepository _userRepository;
        private IBookRepository _bookRepository;
        private IPageRepository _pageRepository;

        public UnitOfWork(Context context)
        {
            _context = context;
        }

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

        public IPageRepository PageRepository
        {
            get
            {
                if (_pageRepository == null)
                {
                    _pageRepository = new PageRepository(_context);
                }
                return _pageRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
