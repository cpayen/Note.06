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
        public void FullName(string firstName, string lastName, string login, string expected)
        {
            var auth = AuthMock.Get(firstName, lastName, login);
            var actual = auth.FullName;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AssertIsAdmin()
        {
            var auth = AuthMock.Get(Roles.AppAdmin);
            var expected = true;
            var actual = auth.IsAdmin;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AssertIsNotAdmin()
        {
            var auth = AuthMock.Get(Roles.AppUser);
            var expected = false;
            var actual = auth.IsAdmin;
            Assert.Equal(expected, actual);
        }
    }
}
