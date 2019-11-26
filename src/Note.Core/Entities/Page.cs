using Note.Core.Entities.Base;
using Note.Core.Enums;

namespace Note.Core.Entities
{
    public class Page : Entity, IOwned
    {
        #region Props

        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public State State { get; set; }
        public Access ReadAccess { get; set; }
        public Access WriteAccess { get; set; }
        public virtual User Owner { get; set; }
        public virtual Book Book { get; set; }
        
        #endregion
    }
}
