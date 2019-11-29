using System.ComponentModel.DataAnnotations;

namespace Note.MVCWebApp.Models.Page
{
    public class CreatePageViewModel
    {
        [Required, MaxLength(250)]
        public string Name { get; set; }
        public bool PublicRead { get; set; }
        public bool PublicWrite { get; set; }
    }
}
