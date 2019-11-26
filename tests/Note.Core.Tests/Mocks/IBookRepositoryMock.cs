using Moq;
using Note.Core.Data.Repositories;
using Note.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Note.Core.Tests.Mocks
{
    public static class IBookRepositoryMock
    {
        public static IBookRepository Get()
        {
            var mock = new Mock<IBookRepository>();
            return mock.Object;
        }

        public static IBookRepository GetFindAsync(Book book)
        {
            var mock = new Mock<IBookRepository>();
            mock.Setup(o => o.FindAsync(It.IsAny<Guid>())).Returns(Task.FromResult<Book>(book));
            return mock.Object;
        }
    }
}
