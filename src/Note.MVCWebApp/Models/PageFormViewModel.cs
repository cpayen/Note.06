using Note.Core.Entities;
using Note.Core.Enums;
using Note.Core.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Note.MVCWebApp.Models
{
    public class PageFormViewModel
    {
        public string FormAction { get; set; }
        public Guid? FormRouteId { get; set; }

        public Guid BookId { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(250)]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(100), RegularExpression(SlugHelper.ValidSludRegex)]
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }

        public PageFormViewModel()
        {
            FormAction = "Create";
            FormRouteId = null;
        }

        public PageFormViewModel(Page page)
        {
            FormAction = "Edit";
            FormRouteId = page.Id;

            BookId = page.Book.Id;
            Title = page.Title;
            Slug = page.Slug;
            Description = page.Description;
            Published = page.State == State.Published;
        }
    }
}
