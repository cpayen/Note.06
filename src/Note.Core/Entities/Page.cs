using Note.Core.Entities.Base;
using Note.Core.Enums;

namespace Note.Core.Entities
{
    public class Page : Entity
    {
        #region Props

        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public PageType Type { get; set; }
        public State State { get; set; }
        public virtual User Owner { get; set; }
        public virtual Book Book { get; set; }
        
        #endregion
    }
}
