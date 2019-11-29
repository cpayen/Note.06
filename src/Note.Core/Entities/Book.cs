using Note.Core.Entities.Base;
using Note.Core.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Note.Core.Entities
{
    public class Book : Entity, IOwned
    {
        #region Props

        public string Title { get; set; }
        public string Description { get; set; }
        public Access ReadAccess { get; set; }
        public Access WriteAccess { get; set; }
        public int PageCount { 
            get
            {
                return Pages?.Count() ?? 0;
            }
        }
        public virtual User Owner { get; set; }
        public virtual IEnumerable<Page> Pages { get; set; }

        #endregion
    }
}
