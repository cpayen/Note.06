using Note.Core.Entities;
using Note.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Note.MVCWebApp.Models
{
    public class BookFormViewModel
    {
        public string FormAction { get; set; }
        public Guid? FormRouteId { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(250)]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(100), RegularExpression(@"^(?i)[a-z0-9]+(?:-[a-z0-9]+)*$")]
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool PublicRead { get; set; }
        public bool PublicWrite { get; set; }

        public BookFormViewModel() 
        {
            FormAction = "Create";
            FormRouteId = null;
        }

        public BookFormViewModel(Book book)
        {
            FormAction = "Edit";
            FormRouteId = book.Id;

            Title = book.Title;
            Slug = book.Slug;
            Description = book.Description;
            PublicRead = book.ReadAccess == Access.Public ? true : false;
            PublicWrite = book.WriteAccess == Access.Public ? true : false;
        }
    }
}
