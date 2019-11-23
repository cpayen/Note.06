using Moq;
using Note.Core.Identity;

namespace Note.Core.Tests.Mocks
{
    public class AuthMock
    {
        public static Auth Get(string firstName, string lastName, string login)
        {
            var ICurrentUserMock = new Mock<ICurrentUser>();
            ICurrentUserMock.Setup(o => o.FirstName).Returns(firstName);
            ICurrentUserMock.Setup(o => o.LastName).Returns(lastName);
            ICurrentUserMock.Setup(o => o.Login).Returns(login);

            return new Auth(null, ICurrentUserMock.Object);
        }

        public static Auth Get(params string[] roles)
        {
            var ICurrentUserMock = new Mock<ICurrentUser>();
            foreach (var role in roles)
            {
                ICurrentUserMock.Setup(o => o.HasRole(role)).Returns(true);
            }

            return new Auth(null, ICurrentUserMock.Object);
        }
    }
}
