using Note.Core.Entities;
using System.Collections.Generic;

namespace Note.MVCWebApp.Models
{
    public class NotesHomeViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Page> LatestPages { get; set; }
    }
}
