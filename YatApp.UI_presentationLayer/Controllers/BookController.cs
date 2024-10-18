using Microsoft.AspNetCore.Mvc;
using Repo;
using System.Threading.Tasks;
using Library.Models;
using Interfaces;  // Assuming this is where the Book model is located

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository;

        public BookController(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return Ok(books);
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest("Book is null");
            }

            var addedBook = await _bookRepository.AddAsync(newBook);
            return CreatedAtAction(nameof(GetBookById), new { id = addedBook.BookId }, addedBook);
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null || id != updatedBook.BookId)
            {
                return BadRequest();
            }

            var bookExists = await _bookRepository.GetByIdAsync(id);
            if (bookExists == null)
            {
                return NotFound();
            }

            _bookRepository.Update(updatedBook);
            return NoContent();
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.Delete(book);
            return NoContent();
        }
    }
}
