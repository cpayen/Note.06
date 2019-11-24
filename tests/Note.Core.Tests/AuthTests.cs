using Note.Core.Enums;
using Note.Core.Identity;
using Note.Core.Tests.Mocks;
using Xunit;

namespace Note.Core.Tests
{
    public class AuthTests
    {
        [Theory]
        [InlineData("John", "Doe", null, "John Doe")]
        [InlineData("John", null, "login", "login")]
        [InlineData("John", "", "login", "login")]
        [InlineData(null, "Doe", "login", "login")]
        [InlineData(null, "", "login", "login")]
        [InlineData(null, null, "login", "login")]
        [InlineData("", "", "login", "login")]
        public void UserFullName(string firstName, string lastName, string login, string expected)
        {
            var auth = new Auth(null, ICurrentUserMock.GetAuthenticatedUser(login, firstName, lastName));
            var actual = auth.FullName;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AuthenticatedUserIsAdmin()
        {
            var auth = new Auth(null, ICurrentUserMock.GetAuthenticatedUserWithRoles("UserLogin", Roles.AppAdmin));
            Assert.True(auth.IsAdmin);
        }

        [Fact]
        public void AuthenticatedUserIsNotAdmin()
        {
            var auth = new Auth(null, ICurrentUserMock.GetAuthenticatedUserWithRoles("UserLogin", Roles.AppUser));
            Assert.False(auth.IsAdmin);
        }

        [Fact]
        public void AnonymousUserIsNotAdmin()
        {
            var auth = new Auth(null, ICurrentUserMock.GetAnonymousUser());
            Assert.False(auth.IsAdmin);
        }

        [Fact]
        public void AuthenticatedUserOwnsItem()
        {
            var login = "UserLogin";
            var auth = new Auth(null, ICurrentUserMock.GetAuthenticatedUser(login));
            var owned = IOwnedMock.Get(login, Access.Private, Access.Private);
            Assert.True(auth.Owns(owned));
        }

        [Fact]
        public void AuthenticatedUserDontOwnsItem()
        {
            var login1 = "UserLogin1";
            var login2 = "UserLogin2";
            var auth = new Auth(null, ICurrentUserMock.GetAuthenticatedUser(login1));
            var owned = IOwnedMock.Get(login2, Access.Private, Access.Private);
            Assert.False(auth.Owns(owned));
        }

        [Fact]
        public void AnonymousUserDontOwnsItem()
        {
            var auth = new Auth(null, ICurrentUserMock.GetAnonymousUser());
            var owned = IOwnedMock.Get(null, Access.Private, Access.Private);
            Assert.False(auth.Owns(owned));
        }

        [Fact]
        public void AnonymousUserCanReadPublicItem()
        {
            var auth = new Auth(null, ICurrentUserMock.GetAnonymousUser());
            var owned = IOwnedMock.Get(null, Access.Public, Access.Private);
            Assert.True(auth.CanRead(owned));
        }

        [Fact]
        public void AnonymousUserCantReadPrivateItem()
        {
            var auth = new Auth(null, ICurrentUserMock.GetAnonymousUser());
            var owned = IOwnedMock.Get(null, Access.Private, Access.Private);
            Assert.False(auth.CanRead(owned));
        }
    }
}
