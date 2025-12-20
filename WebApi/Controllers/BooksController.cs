using Microsoft.AspNetCore.Mvc;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;

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
        public async Task<IActionResult> Get()
        {
            var books = await _context.Books
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Isbn = b.Isbn,
                    PublishedDate = b.PublishedDate,
                    Genres = b.Genres
                        .OrderBy(g => g.Name)
                        .Select(g => new GenreDto
                        {
                            Id = g.Id,
                            Name = g.Name
                        })
                        .ToList()

                })
                .ToListAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var book = await _context.Books
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Isbn = b.Isbn,
                    PublishedDate = b.PublishedDate,
                    Genres = b.Genres
                        .OrderBy(g => g.Name)
                        .Select(g => new GenreDto
                        {
                            Id = g.Id,
                            Name = g.Name
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> Post(CreateBookDto dto)
        {
            var genreIds = dto.GenreIds.Distinct().ToList();

            var genres = await _context.Genres
                .Where(g => genreIds.Contains(g.Id))
                .ToListAsync();

            if (genres.Count != genreIds.Count)
            {
                return BadRequest("One or more genre IDs are invalid.");
            }

                var book = new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                Isbn = dto.Isbn,
                PublishedDate = dto.PublishedDate,
                Genres = genres
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Isbn = book.Isbn,
                PublishedDate = book.PublishedDate,
                Genres = genres
                    .OrderBy(g => g.Name)
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToList()
            };
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, bookDto);
        }


    }
}
