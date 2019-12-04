using System;
using System.ComponentModel.DataAnnotations;

namespace Note.MVCWebApp.Models.Page
{
    public class UpdatePageViewModel
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(250)]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(100)]
        public string Slug { get; set; }
        public bool PublicRead { get; set; }
        public bool PublicWrite { get; set; }
    }
}
