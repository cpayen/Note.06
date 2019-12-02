using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Exceptions;
using Note.Core.Identity;
using Note.Core.Services.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class Pages
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAuth _auth;

        public Pages(IUnitOfWork unitOfWork, IAuth auth)
        {
            _unitOfWork = unitOfWork;
            _auth = auth;
        }

        public async Task<Page> FindAsync(Guid id)
        {
            var page = await _unitOfWork.PageRepository.FindAsync(id) ?? throw new NotFoundException(nameof(Page), id);

            if (!_auth.CanRead(page))
            {
                throw new NotAllowedException(_auth.Login, nameof(Book), id);
            }

            return page;
        }

        public async Task<Page> CreateAsync(CreatePageCommand cmd)
        {
            if (!cmd.IsValid)
            {
                throw new InvalidCommandException(nameof(CreatePageCommand), cmd);
            }

            var book = await _unitOfWork.BookRepository.FindAsync(cmd.BookId);

            if(book == null)
            {
                throw new NotFoundException(nameof(Book), cmd.BookId);
            }

            var page = new Page
            {
                Book = book,
                Title = cmd.Title,
                ReadAccess = cmd.ReadAccess,
                WriteAccess = cmd.WriteAccess,
                Owner = await _auth.GetCurrentUserEntityAsync(),
                CreatedAt = DateTime.Now
            };

            _unitOfWork.PageRepository.Create(page);
            await _unitOfWork.SaveAsync();

            return page;
        }
    }
}
