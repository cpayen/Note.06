using Moq;
using Note.Core.Entities;
using Note.Core.Entities.Base;
using Note.Core.Identity;
using System;

namespace Note.Core.Tests.Mocks
{
    public static class IAuthMock
    {
        public static IAuth Get()
        {
            var mock = new Mock<IAuth>();
            return mock.Object;
        }

        public static IAuth GetIOwnedWriteAllowed()
        {
            var mock = new Mock<IAuth>();
            mock.Setup(o => o.CanWrite(It.IsAny<IOwned>())).Returns(true);
            return mock.Object;
        }

        public static IAuth GetIOwnedReadNotAllowed()
        {
            var mock = new Mock<IAuth>();
            mock.Setup(o => o.CanRead(It.IsAny<IOwned>())).Returns(false);
            return mock.Object;
        }
    }
}
