using Note.Core.Entities;
using Note.Core.Enums;
using Note.Core.Exceptions;
using Note.Core.Identity;
using Note.Core.Services;
using Note.Core.Services.Commands;
using Note.Core.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Note.Core.Tests
{
    public class BooksTests
    {
        [Fact]
        public async Task CreateAsyncInvalidCommandAsync()
        {
            var books = new Books(null, null);
            var invalidCmd = new CreateBookCommand(null, null, Access.Private, Access.Private);
            await Assert.ThrowsAsync<InvalidCommandException>(() => books.CreateAsync(invalidCmd));
        }

        [Fact]
        public async Task UpdateAsyncInvalidCommandAsync()
        {
            var books = new Books(null, null);
            var invalidCmd = new UpdateBookCommand(Guid.NewGuid(), null, null, Access.Private, Access.Private);
            await Assert.ThrowsAsync<InvalidCommandException>(() => books.UpdateAsync(invalidCmd));
        }

        [Fact]
        public async Task FindAyncNotFound()
        {
            var uow = IUnitOfWorkMock.Get(IBookRepositoryMock.GetFindAsync(null));
            var auth = new Auth(null, ICurrentUserMock.GetAnonymousUser());
            var books = new Books(uow, auth);
            await Assert.ThrowsAsync<NotFoundException>(() => books.FindAsync(new Guid()));
        }

        [Fact]
        public async Task FindAyncNotAllowed()
        {
            var auth = IAuthMock.GetIOwnedNotAllowed();
            var uow = IUnitOfWorkMock.Get(IBookRepositoryMock.GetFindAsync(_book));
            var books = new Books(uow, auth);
            await Assert.ThrowsAsync<NotAllowedException>(() => books.FindAsync(new Guid()));
        }

        private static Book _book = new Book
        {
            Id = new Guid("8a2109b0-7d55-4fc2-9eb1-57f30ebd6040"),
            Name = "Book 1",
            ReadAccess = Access.Public,
            WriteAccess = Access.Public,
        };

        private static List<Book> _bookList = new List<Book>
        {
            new Book
            {
                Id = new Guid("8a2109b0-7d55-4fc2-9eb1-57f30ebd6040"),
                Name = "Book 1",
                ReadAccess = Access.Public,
                WriteAccess = Access.Public,
            },
            new Book
            {
                Id = new Guid("d6d324f1-3d46-4447-a38c-254499416adf"),
                Name = "Book 2",
                ReadAccess = Access.Public,
                WriteAccess = Access.Public,
            },
            new Book
            {
                Id = new Guid("bad7bf5c-db83-4a1a-93ad-d8f0a0269bbc"),
                Name = "Book 3",
                ReadAccess = Access.Public,
                WriteAccess = Access.Public,
            }
        };
    }
}
