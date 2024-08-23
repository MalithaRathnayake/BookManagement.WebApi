using BookManagement.Application.Interfaces;
using BookManagement.Domain.Interfaces;
using BookManagement.Domain.Models; 

namespace BookManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository; 

        public BookService(IBookRepository repository)
        {
            _repository = repository; 
        }
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _repository.GetAllAsync();
        }
         
        public async Task<Book?> GetBook(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            
            return book;
        }

        public async Task CreateBook(Book book)
        {
            var existingBook = await _repository.GetByIdAsync(book.Id);
            if (existingBook is object)
            {
                throw new Exception("Duplicate Found");
            }

            await _repository.AddAsync(book);

        }
         
        public async Task UpdateBook(Book book)
        {
            var existingBook = await _repository.GetByIdAsync(book.Id);
            if (existingBook == null)
            {
                throw new Exception("Not found");
            }
             
            await _repository.UpdateAsync(book); 
        }
           
        public async Task DeleteBook(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                throw new Exception("Not found");
            }

            await _repository.DeleteAsync(id); 
        }
    }
}
