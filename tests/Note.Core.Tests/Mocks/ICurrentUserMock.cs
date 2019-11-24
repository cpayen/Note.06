using Moq;
using Note.Core.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Note.Core.Tests.Mocks
{
    public static class ICurrentUserMock
    {
        public static ICurrentUser GetAnonymousUser()
        {
            var mock = new Mock<ICurrentUser>();
            return mock.Object;
        }

        public static ICurrentUser GetAuthenticatedUser(string login, string firstName = "John", string lastName = "Doe", string email = "john.doe@fake.net")
        {
            return GetAuthenticatedUserMock(login, firstName, lastName, email).Object;
        }

        public static ICurrentUser GetAuthenticatedUserWithRoles(string login, params string[] roles)
        {
            var mock = GetAuthenticatedUserMock(login);
            foreach (var role in roles)
            {
                mock.Setup(o => o.HasRole(role)).Returns(true);
            }

            return mock.Object;
        }

        private static Mock<ICurrentUser> GetAuthenticatedUserMock(string login, string firstName = "John", string lastName = "Doe", string email = "john.doe@fake.net")
        {
            var mock = new Mock<ICurrentUser>();
            mock.Setup(o => o.FirstName).Returns(firstName);
            mock.Setup(o => o.LastName).Returns(lastName);
            mock.Setup(o => o.Login).Returns(login);
            mock.Setup(o => o.IsAuthenticated).Returns(true);
            mock.Setup(o => o.Email).Returns(email);

            return mock;
        }
    }
}
