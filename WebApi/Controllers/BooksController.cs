using Microsoft.AspNetCore.Mvc;
using Data;
using Domain.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/books")]

    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }   

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Message = "Books endpoint is operational."
            });
        }

        [HttpPost]
        public IActionResult Post(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Isbn = dto.Isbn,
                PublishedYear = dto.PublishedYear
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }
    }
}
