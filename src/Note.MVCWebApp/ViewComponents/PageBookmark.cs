using Microsoft.AspNetCore.Mvc;
using Note.Core.Services;
using Note.MVCWebApp.Models;
using System;
using System.Threading.Tasks;

namespace Note.MVCWebApp.ViewComponents
{
    public class PageBookmark : ViewComponent
    {
        private readonly Bookmarks _bookmarks;

        public PageBookmark(Bookmarks pages)
        {
            _bookmarks = pages;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid pageId)
        {
            var bookmark = await _bookmarks.GetForCurrentUserAsync(pageId);
            var model = new ToggleBookmarkViewModel
            {
                PageId = pageId,
                BookmarkExists = bookmark != null
            };
            return View(model);
        }
    }
}
