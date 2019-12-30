using Moq;
using Note.Core.Data.Repositories;
using Note.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Note.Core.Tests.Mocks
{
    public static class IBookRepositoryMock
    {
        public static IBookRepository Get()
        {
            var mock = new Mock<IBookRepository>();
            mock.Setup(o => o.FindByAsync(It.IsAny<Expression<Func<Book, bool>>>())).Returns(Task.FromResult<ICollection<Book>>(new List<Book>()));
            return mock.Object;
        }

        public static IBookRepository GetFindAsync(Book book)
        {
            var mock = new Mock<IBookRepository>();
            mock.Setup(o => o.FindAsync(It.IsAny<Guid>())).Returns(Task.FromResult<Book>(book));
            mock.Setup(o => o.FindByAsync(It.IsAny<Expression<Func<Book, bool>>>())).Returns(Task.FromResult<ICollection<Book>>(new List<Book>()));
            return mock.Object;
        }
    }
}
