using System.ComponentModel.DataAnnotations;

namespace Note.MVCWebApp.Models.Book
{
    public class CreateBookViewModel
    {
        [Required, MaxLength(250)]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool PublicRead { get; set; }
        public bool PublicWrite { get; set; }
    }
}
