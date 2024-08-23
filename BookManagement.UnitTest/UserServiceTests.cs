using BookManagement.Application.Interfaces;
using BookManagement.Application.Services;
using BookManagement.Domain.Interfaces;
using BookManagement.Domain.Models;
using FluentAssertions;
using Moq; 

namespace BookManagement.UnitTests
{
    public class UserServiceTests
    {
        private readonly Mock<IBookRepository> _mockUserRepository;
        private readonly IBookService _bookService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IBookRepository>();
            _bookService = new BookService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task GetBokkByIdAsync_ShouldReturnUser_WhenBookExists()
        {
            // Arrange
            var Id = 1;
            var book = new Book { Id = Id, Title = "Mill on the", Author = "John Lenon", PublishedDate = DateTime.Now.AddMonths(-1) };
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(Id)).ReturnsAsync(book);

            // Act
            var result = await _bookService.GetBook(Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(Id);
            result.Title.Should().Be("Mill on the");
            result.Author.Should().Be("John Lenon");
            result.PublishedDate.Should().NotBeOnOrAfter(DateTime.Now);
        }

        [Fact]
        public async Task GetBokkByIdAsync_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            var Id = 1;
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(Id)).ReturnsAsync((Book)null); 

            // Act
            var result = await _bookService.GetBook(Id);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnBooks_WhenUsersExist()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "THe Snare", Author = "John Lenon",PublishedDate = DateTime.Now.AddMonths(-1) },
                new Book { Id = 2, Title = "Stopping by the woods", Author = "John Lenon",PublishedDate = DateTime.Now.AddMonths(-1) }
            };
            _mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);

            // Act
            var result = await _bookService.GetBooks();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(books.Count);
        } 

    }
}