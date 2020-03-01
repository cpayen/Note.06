using Note.Core.Entities.Base;

namespace Note.Core.Entities
{
    public class Bookmark : Entity
    {
        public Page Page { get; set; }
        public User User { get; set; }
    }
}
