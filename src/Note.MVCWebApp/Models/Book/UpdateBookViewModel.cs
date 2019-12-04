using System;
using System.ComponentModel.DataAnnotations;

namespace Note.MVCWebApp.Models.Book
{
    public class UpdateBookViewModel
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(250)]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(100), RegularExpression(@"^(?i)[a-z0-9]+(?:-[a-z0-9]+)*$")]
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool PublicRead { get; set; }
        public bool PublicWrite { get; set; }
    }
}
