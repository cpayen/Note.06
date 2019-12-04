using Note.Core.Entities;
using Note.Core.Enums;
using Note.Core.Exceptions;
using Note.Core.Identity;
using Note.Core.Services;
using Note.Core.Services.Commands;
using Note.Core.Tests.Mocks;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Note.Core.Tests
{
    public class BooksTests
    {
        [Fact]
        public async Task CreateAsync_InvalidCommand()
        {
            var books = new Books(null, null);
            var invalidCmd = new CreateBookCommand(null, null, null, Access.Private, Access.Private);
            await Assert.ThrowsAsync<InvalidCommandException>(() => books.CreateAsync(invalidCmd));
        }

        [Fact]
        public async Task CreateAsync_CheckResult()
        {
            var title = "Book's title";
            var slug = "books-title";
            var description = "Book's description";
            var readAccess = Access.Private;
            var writreAccess = Access.Private;

            var uow = IUnitOfWorkMock.Get();
            var auth = IAuthMock.Get();
            var books = new Books(uow, auth);
            var cmd = new CreateBookCommand(title, slug, description, readAccess, writreAccess);
            var book = await books.CreateAsync(cmd);
            
            Assert.Equal(title, book.Title);
            Assert.Equal(slug, book.Slug);
            Assert.Equal(description, book.Description);
            Assert.Equal(readAccess, book.ReadAccess);
            Assert.Equal(writreAccess, book.WriteAccess);
        }

        [Fact]
        public async Task UpdateAsync_InvalidCommand()
        {
            var books = new Books(null, null);
            var invalidCmd = new UpdateBookCommand(Guid.NewGuid(), null, null, null, Access.Private, Access.Private);
            await Assert.ThrowsAsync<InvalidCommandException>(() => books.UpdateAsync(invalidCmd));
        }
        
        [Fact]
        public async Task UpdateAsync_ChecResult()
        {
            var title = "Book's title";
            var slug = "books-title";
            var description = "Book's description";
            var readAccess = Access.Private;
            var writreAccess = Access.Private;

            var auth = IAuthMock.GetIOwnedNotAllowed();
            var uow = IUnitOfWorkMock.Get(IBookRepositoryMock.GetFindAsync(_book));
            var books = new Books(uow, auth);
            var cmd = new UpdateBookCommand(_book.Id, title, slug, description, readAccess, writreAccess);
            var book = await books.UpdateAsync(cmd);

            Assert.Equal(title, book.Title);
            Assert.Equal(slug, book.Slug);
            Assert.Equal(description, book.Description);
            Assert.Equal(readAccess, book.ReadAccess);
            Assert.Equal(writreAccess, book.WriteAccess);
        }

        [Fact]
        public async Task FindAync_NotFound()
        {
            var uow = IUnitOfWorkMock.Get(IBookRepositoryMock.GetFindAsync(null));
            var auth = new Auth(null, ICurrentUserMock.GetAnonymousUser());
            var books = new Books(uow, auth);
            await Assert.ThrowsAsync<NotFoundException>(() => books.FindAsync(new Guid()));
        }

        [Fact]
        public async Task FindAync_NotAllowed()
        {
            var auth = IAuthMock.GetIOwnedNotAllowed();
            var uow = IUnitOfWorkMock.Get(IBookRepositoryMock.GetFindAsync(_book));
            var books = new Books(uow, auth);
            await Assert.ThrowsAsync<NotAllowedException>(() => books.FindAsync(new Guid()));
        }

        private static Book _book = new Book
        {
            Id = new Guid("8a2109b0-7d55-4fc2-9eb1-57f30ebd6040"),
            Title = "Book 1",
            ReadAccess = Access.Public,
            WriteAccess = Access.Public,
        };
    }
}
