using Microsoft.AspNetCore.Mvc;
using Library.EntityModels;
using Library.WebApi.Repositories;
namespace Library.WebApi.Controllers;

// Base address: api/books
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _repo;

    public BooksController(IBookRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
    public async Task<IEnumerable<Book>> GetBooks(int? authorId)
    {
        if (authorId is null)
        {
            return await _repo.RetrieveAllAsync();
        }
        else
        {
            return (await _repo.RetrieveAllAsync()).Where(book => book.AuthorId == authorId);
        }
    }

    [HttpGet("{bookId}", Name = nameof(GetBook))]
    [ProducesResponseType(200, Type = typeof(Book))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetBook(int bookId)
    {
        Book? b = await _repo.RetrieveAsync(bookId);
        if (b == null)
        {
            return NotFound();
        }
        return Ok(b);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Book))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Book b)
    {
        if (b == null)
        {
            return BadRequest();
        }
        Book? addedBook = await _repo.CreateAsync(b);
        if (addedBook == null)
        {
            return BadRequest("Repositiory failed to create book.");
        }
        else
        {
            return CreatedAtRoute(
                routeName: nameof(GetBook),
                routeValues: new { bookId = addedBook.BookId },
                value: addedBook);
        }
    }

    [HttpPut("{bookId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int bookId, [FromBody] Book b)
    {
        if (b == null || b.BookId != bookId)
        {
            return BadRequest();
        }

        Book? existing = await _repo.RetrieveAsync(bookId);
        if (existing == null)
        {
            return NotFound();
        }
        await _repo.UpdateAsync(b);
        return new NoContentResult();
    }

    [HttpDelete("{bookId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int bookId)
    {
        Book? existing = await _repo.RetrieveAsync(bookId);
        if (existing == null)
        {
            return NotFound();
        }
        bool? deleted = await _repo.DeleteAsync(bookId);
        if (deleted.HasValue && deleted.Value)
        {
            return new NoContentResult();
        }
        else
        {
            return BadRequest(
                $"Book {bookId} was found but failed to delete."
            );
        }
    }
}
