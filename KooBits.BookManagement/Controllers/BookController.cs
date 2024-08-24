using BookManagement.Application.Interfaces;
using BookManagement.Application.Services;
using BookManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace KooBits.BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookServices;
        private readonly ILogger<BookService> _logger;
        public BookController(IBookService bookServices, ILogger<BookService> logger)
        {
            _bookServices = bookServices;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await _bookServices.GetBooks());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookServices.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _bookServices.CreateBook(book);
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch(DuplicateNameException ex) // Custom exception generation module need to implement to refactor exception generations
            {
                _logger.LogError(ex, "Duplicate book ID.");
                return StatusCode(500, "Duplicate book ID.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving books.");
                return StatusCode(500, "An unexpected error occurred.");
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(Book book)
        {  
            var existingBook = await _bookServices.GetBook(book.Id);
            if (existingBook == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _bookServices.UpdateBook(book);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookServices.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookServices.DeleteBook(id);
            return Ok();
        }
    }
}