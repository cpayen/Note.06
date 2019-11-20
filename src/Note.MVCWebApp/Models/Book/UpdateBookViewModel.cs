using System;
using System.ComponentModel.DataAnnotations;

namespace Note.MVCWebApp.Models.Book
{
    public class UpdateBookViewModel
    {
        public Guid Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool PublicRead { get; set; }
        public bool PublicWrite { get; set; }
    }
}
