using Note.Core.Entities;
using System;

namespace Note.MVCWebApp.Models
{
    public class PageContentFormViewModel
    {
        public string FormAction { get; set; }
        public Guid FormRouteId { get; set; }

        public string Content { get; set; }

        public PageContentFormViewModel() {}

        public PageContentFormViewModel(Page page)
        {
            FormAction = "Write";
            FormRouteId = page.Id;

            Content = page.Content;
        }
    }
}
