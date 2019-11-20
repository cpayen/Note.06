﻿using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Exceptions;
using Note.Core.Identity;
using Note.Core.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class Books
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly Auth _auth;

        public Books(IUnitOfWork unitOfWork, Auth identity)
        {
            _unitOfWork = unitOfWork;
            _auth = identity;
        }

        public async Task<Book> CreateAsync(CreateBookCommand cmd)
        {
            if(!cmd.IsValid)
            {
                //TODO: Mieux gérer les exceptions...
                throw new ArgumentException("Invalid command");
            }

            var book = new Book
            {
                Name = cmd.Name,
                Description = cmd.Description,
                ReadAccess = cmd.ReadAccess,
                WriteAccess = cmd.WriteAccess,
                Owner = await _auth.GetCurrentUserEntityAsync(),
                CreatedAt = DateTime.Now
            };

            _unitOfWork.BookRepository.Create(book);
            await _unitOfWork.SaveAsync();
            
            return book;
        }

        public async Task<Book> UpdateAsync(UpdateBookCommand cmd)
        {
            if (!cmd.IsValid)
            {
                //TODO: Mieux gérer les exceptions...
                throw new ArgumentException("Invalid command");
            }
            
            var book = await _unitOfWork.BookRepository.FindAsync(cmd.Id) ?? throw new NotFoundException(nameof(Book), cmd.Id);

            book.Name = cmd.Name;
            book.Description = cmd.Description;
            book.ReadAccess = cmd.ReadAccess;
            book.WriteAccess = cmd.WriteAccess;
            book.UpdatedAt = DateTime.Now;

            _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.SaveAsync();

            return book;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync(_auth.Login, _auth.IsAdmin());
            return books.ToList();
        }

        public async Task<Book> FindAsync(Guid id)
        {
            // Controle...
            var user = await _auth.GetCurrentUserEntityAsync();
            var book = await _unitOfWork.BookRepository.FindAsync(id) ?? throw new NotFoundException(nameof(Book), id);

            if(!_auth.CanRead(book))
            {
                throw new NotAllowedException(_auth.Login, nameof(Book), id);
            }

            return book;
        }

        public async Task DeleteAsync(Guid id)
        {
            _unitOfWork.BookRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}