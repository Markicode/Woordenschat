using Application.Dtos;
using Application.Dtos.Books;
using Application.Enums;
using Application.Mappings;
using Application.Services.Books;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return response.ErrorType switch
                {
                    ErrorType.ValidationError => BadRequest(response.ErrorMessage),
                    _ => BadRequest()
                };
            }

            var createdBook = response.Value!;

            var bookDto = createdBook.ToDto();

            return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> Put(int id, ReplaceBookDto dto)
        {
            var replaceBookCommand = new ReplaceBookCommand(id, dto.Isbn, dto.Title, dto.Description, dto.PublishedDate, dto.AuthorIds, dto.GenreIds);

            var response = await _bookService.ReplaceBookAsync(replaceBookCommand);

            if (!response.IsSuccess)
            {
                return response.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(response.ErrorMessage),
                    ErrorType.ValidationError => BadRequest(response.ErrorMessage),
                    _ => BadRequest()
                };
            }

            return Ok(response.Value!.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _bookService.DeleteBookAsync(id);

            if (!response.IsSuccess)
            {
                return response.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(response.ErrorMessage),
                    _ => BadRequest()
                };
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<BookDto>> Patch(int id, PatchBookDto dto)
        {
            if (dto.Isbn is null &&
                dto.Title is null &&
                dto.Description is null &&
                dto.PublishedDate is null &&
                dto.AuthorIds is null &&
                dto.GenreIds is null)
            {
                return BadRequest("No fields provided to patch.");
            }

            var patchBookCommand = new PatchBookCommand(id, dto.Isbn, dto.Title, dto.Description, dto.PublishedDate, dto.AuthorIds, dto.GenreIds);
            var response = await _bookService.PatchBookAsync(patchBookCommand);
            if (!response.IsSuccess)
            {
                return response.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(response.ErrorMessage),
                    ErrorType.ValidationError => BadRequest(response.ErrorMessage),
                    _ => BadRequest()
                };
            }
            return Ok(response.Value!.ToDto());
        }

    }
}
