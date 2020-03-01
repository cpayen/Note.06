using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Exceptions;
using Note.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class Bookmarks
    {
        #region Props

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAuth _auth;

        #endregion

        #region Ctor

        public Bookmarks(IUnitOfWork unitOfWork, IAuth auth)
        {
            _unitOfWork = unitOfWork;
            _auth = auth;
        }

        #endregion

        #region CRUD

        public async Task<Bookmark> FindAsync(Guid id)
        {
            var bookmark = await _unitOfWork.BookmarkRepository.FindAsync(id);
            return bookmark;
        }

        public async Task<List<Bookmark>> GetForCurrentUserAsync()
        {
            var bookmarks = await _unitOfWork.BookmarkRepository.GetForUserAsync(_auth.Login);
            return bookmarks.ToList();
        }

        public async Task<Bookmark> GetForCurrentUserAsync(Guid pageId)
        {
            var bookmark = await _unitOfWork.BookmarkRepository.GetForUserAsync(_auth.Login, pageId);
            return bookmark;
        }

        public async Task<Bookmark> ToggleForCurrentUserAsync(Guid pageId)
        {
            var user = await _unitOfWork.UserRepository.FindAsync(_auth.Login) ?? throw new NotFoundException(nameof(User), _auth.Login);
            var page = await _unitOfWork.PageRepository.FindAsync(pageId) ?? throw new NotFoundException(nameof(Page), pageId);
            var existingBookmark = await GetForCurrentUserAsync(pageId);

            if (existingBookmark == null)
            {
                var bookmark = new Bookmark
                {
                    Page = page,
                    User = user,
                    CreatedAt = DateTime.Now
                };
                _unitOfWork.BookmarkRepository.Create(bookmark);
                await _unitOfWork.SaveAsync();
                return bookmark;
            }
            else
            {
                _unitOfWork.BookmarkRepository.Delete(existingBookmark.Id);
                await _unitOfWork.SaveAsync();
                return null;
            }
        }

        #endregion
    }
}
