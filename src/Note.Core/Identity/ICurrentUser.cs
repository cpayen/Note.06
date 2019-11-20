namespace Note.Core.Identity
{
    public struct Roles
    {
        public static string AppUser = "APP_USER";
        public static string AppAdmin = "APP_ADMIN";
    }

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
