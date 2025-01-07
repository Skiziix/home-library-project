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


}
