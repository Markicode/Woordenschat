using Microsoft.AspNetCore.Mvc;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Dtos;
using Application.Services.Books;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/books")]

    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(LibraryContext context, IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _bookService.GetBooksAsync();
            var books = response.Books!;

            var bookDtos = books
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
                        .ToList(),
                    Authors = b.Authors
                        .OrderBy(a => a.LastName)
                        .ThenBy(a => a.FirstName)
                        .Select(a => new AuthorDto
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName
                        })
                        .ToList()

                })
                .ToList();

            return Ok(bookDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var response = await _bookService.GetBookByIdAsync(id);

            if (!response.IsSuccess)
            {
                return NotFound(response.ErrorMessage);
            }

            var requestedBook = response.Book!;

            var book = new BookDto
            {
                Id = requestedBook.Id,
                Title = requestedBook.Title,
                Description = requestedBook.Description,
                Isbn = requestedBook.Isbn,
                PublishedDate = requestedBook.PublishedDate,
                Genres = requestedBook.Genres
                    .OrderBy(g => g.Name)
                    .Select(g => new GenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                    .ToList(),
                Authors = requestedBook.Authors
                    .OrderBy(a => a.LastName)
                    .ThenBy(a => a.FirstName)
                    .Select(a => new AuthorDto
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    })
                    .ToList()
            };

            return Ok(book);

        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> Post(CreateBookDto dto)
        {
            // TODO: Validate DTO (e.g., check for required fields, valid author and genre IDs, etc.)
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
                    .ToList(),
                Authors = createdBook.Authors
                    .OrderBy(a => a.LastName)
                    .ThenBy(a => a.FirstName)
                    .Select(a => new AuthorDto
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    })
                    .ToList()
            };

            return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);

        }




    }
}
