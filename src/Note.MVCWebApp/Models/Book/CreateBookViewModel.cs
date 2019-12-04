using System.ComponentModel.DataAnnotations;

namespace Note.MVCWebApp.Models.Book
{
    public class CreateBookViewModel
    {
        [Required(AllowEmptyStrings = false), MaxLength(250)]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(100)]
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool PublicRead { get; set; }
        public bool PublicWrite { get; set; }
    }
}
