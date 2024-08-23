using BookManagement.Domain.Models; 

namespace BookManagement.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks();

        Task<Book?> GetBook(int id);

        Task CreateBook(Book book); 

        Task UpdateBook(Book book); 

        Task DeleteBook(int id); 
    }
}
