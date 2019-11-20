using Note.Core.Entities.Base;
using System.Collections.Generic;

namespace Note.Core.Entities
{
    public class User : Entity
    {
        #region Props

        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FullName 
        { 
            get
            {
                return
                    !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) ?
                    $"{FirstName} {LastName}" :
                    Login;
            }
        }
        public virtual IEnumerable<Book> Books { get; set; }
        public virtual IEnumerable<Page> Pages { get; set; }

        #endregion
    }
}
