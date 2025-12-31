using Microsoft.AspNetCore.Mvc;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Dtos;
using Application.Services.Books;
using Application.Mappings;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/books")]

    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> Get()
        {
            var response = await _bookService.GetBooksAsync();
            var books = response.Value!;

            var bookDtos = books
                .Select(b => b.ToDto())
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

            var requestedBook = response.Value!;

            return Ok(requestedBook.ToDto());

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

            var createdBook = response.Value!;

            var bookDto = createdBook.ToDto();

            return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);

        }

        /**
        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> Put(int id, ReplaceBookDto dto)
        {
            var ReplaceBookCommand = new ReplaceBookCommand(dto.BookId, dto.Isbn, dto.Title, dto.Description, dto.PublishedDate, dto.AuthorIds, dto.GenreIds);




        }**/
    }
}
