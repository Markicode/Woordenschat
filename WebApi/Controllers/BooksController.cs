using Microsoft.AspNetCore.Mvc;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using Application.Services.Books;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/books")]

    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly IBookService _bookService;

        public BooksController(LibraryContext context, IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
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
            var createBookCommand = new CreateBookCommand(dto.Isbn, dto.Title, dto.Description, dto.PublishedDate, dto.AuthorIds, dto.GenreIds);

            var response = await _bookService.CreateBookAsync(createBookCommand);

            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            var createdBook = response.Book!;

            var bookDto = new BookDto
            {
                Id = createdBook.Id,
                Title = createdBook.Title,
                Description = createdBook.Description,
                Isbn = createdBook.Isbn,
                PublishedDate = createdBook.PublishedDate,
                Genres = createdBook.Genres
                    .OrderBy(g => g.Name)
                    .Select(g => new GenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                    .ToList()
            };

            return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);

        }




    }
}
