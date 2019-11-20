using Note.Core.Enums;

namespace Note.Core.Entities.Base
{
    public interface IOwned
    {
        public User Owner { get; set; }
        public Access ReadAccess { get; set; }
        public Access WriteAccess { get; set; }
    }
}
