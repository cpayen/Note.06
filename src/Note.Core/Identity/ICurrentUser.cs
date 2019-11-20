namespace Note.Core.Identity
{
    public interface ICurrentUser
    {
        public bool IsAuthenticated { get; }
        public string Login { get;  }
        public string FirstName { get;  }
        public string LastName { get; }
        public string Email { get; }
        public bool HasRole(string role);
    }
}
