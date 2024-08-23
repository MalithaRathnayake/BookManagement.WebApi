using BookManagement.Domain.Interfaces;
using BookManagement.Domain.Models;
using BookManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Infrastructure.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Books.ToListAsync();

        public async Task<Book> GetByIdAsync(int id) => await _context.Books.FindAsync(id);

        public async Task AddAsync(Book book)
        {
            var existingBook = await _context.Books.FindAsync(book.Id);
            if (existingBook is object)
            {
                throw new DuplicateNameException("Duplicate Id.");
            }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            var existingBook = await _context.Books.FindAsync(book.Id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException("The book was not found.");
            }

            // Update only the fields that you want to change
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublishedDate = book.PublishedDate;

            // No need to set the state explicitly if you are updating a tracked entity
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
