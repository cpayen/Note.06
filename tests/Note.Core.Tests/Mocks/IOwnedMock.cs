using Moq;
using Note.Core.Entities.Base;
using Note.Core.Enums;

namespace Note.Core.Tests.Mocks
{
    public static class IOwnedMock
    {
        public static IOwned Get(string ownerLogin, Access readAccess, Access writeAccess)
        {
            var IOwnedMock = new Mock<IOwned>();
            IOwnedMock.Setup(o => o.ReadAccess).Returns(readAccess);
            IOwnedMock.Setup(o => o.WriteAccess).Returns(writeAccess);
            IOwnedMock.Setup(o => o.Owner).Returns(new Entities.User
            {
                Login = ownerLogin
            });

            return IOwnedMock.Object;
        }
    }
}
