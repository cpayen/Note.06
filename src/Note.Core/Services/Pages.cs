using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Exceptions;
using Note.Core.Identity;
using Note.Core.Services.Commands;
using System;
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

        public async Task<Page> FindAsync(string slug)
        {
            var page = await _unitOfWork.PageRepository.FindAsync(slug) ?? throw new NotFoundException(nameof(Page), slug);

            if (!_auth.CanRead(page))
            {
                throw new NotAllowedException(_auth.Login, nameof(Book), slug);
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

            if(!_auth.CanWrite(book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Book), cmd.BookId);
            }

            var page = new Page
            {
                Book = book,
                Title = cmd.Title,
                Slug = cmd.Slug,
                State = cmd.State,
                ReadAccess = cmd.ReadAccess,
                WriteAccess = cmd.WriteAccess,
                Owner = await _auth.GetCurrentUserEntityAsync(),
                CreatedAt = DateTime.Now
            };

            _unitOfWork.PageRepository.Create(page);
            await _unitOfWork.SaveAsync();

            return page;
        }

        public async Task<Page> UpdateAsync(UpdatePageCommand cmd)
        {
            if (!cmd.IsValid)
            {
                throw new InvalidCommandException(nameof(UpdatePageCommand), cmd);
            }

            var page = await _unitOfWork.PageRepository.FindAsync(cmd.Id) ?? throw new NotFoundException(nameof(Page), cmd.Id);

            if (!_auth.CanWrite(page))
            {
                throw new NotAllowedException(_auth.Login, nameof(Page), cmd.Id);
            }

            page.Title = cmd.Title;
            page.Slug = cmd.Slug;
            page.State = cmd.State;
            page.ReadAccess = cmd.ReadAccess;
            page.WriteAccess = cmd.WriteAccess;
            page.UpdatedAt = DateTime.Now;

            _unitOfWork.PageRepository.Update(page);
            await _unitOfWork.SaveAsync();

            return page;
        }

        public async Task<Page> DeleteAsync(Guid id)
        {
            var page = await _unitOfWork.PageRepository.FindAsync(id) ?? throw new NotFoundException(nameof(Page), id);

            if (!_auth.CanWrite(page))
            {
                throw new NotAllowedException(_auth.Login, nameof(Page), id);
            }

            _unitOfWork.PageRepository.Delete(id);
            await _unitOfWork.SaveAsync();

            return page;
        }
    }
}
