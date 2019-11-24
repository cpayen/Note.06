using Moq;
using Note.Core.Data;
using Note.Core.Data.Repositories;
using Note.Core.Entities;
using System.Collections.Generic;

namespace Note.Core.Tests.Mocks
{
    public static class IUnitOfWorkMock
    {
        public static IUnitOfWork Get(IBookRepository bookRepository)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(o => o.BookRepository).Returns(bookRepository);
            return mock.Object;
        }
    }
}
