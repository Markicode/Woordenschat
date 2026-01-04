
using Application.Dtos;
using Application.Dtos.Books;
using Application.Mappings;
using Application.Services.Books;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/books")]

    public class BooksController : BaseApiController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _bookService.GetBooksAsync();

            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            var books = response.Value!;

            var bookDtos = books
                .Select(b => b.ToDto())
                .ToList();

            return Ok(bookDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _bookService.GetBookByIdAsync(id);

            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            var requestedBook = response.Value!;

            return Ok(requestedBook.ToDto());

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBookDto dto)
        {
            // TODO: Validate DTO (e.g., check for required fields, valid author and genre IDs, etc.)
            var createBookCommand = new CreateBookCommand(dto.Isbn, dto.Title, dto.Description, dto.PublishedDate, dto.AuthorIds, dto.GenreIds);

            var response = await _bookService.CreateBookAsync(createBookCommand);

            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            var createdBook = response.Value!;

            var bookDto = createdBook.ToDto();

            return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ReplaceBookDto dto)
        {
            var replaceBookCommand = new ReplaceBookCommand(id, dto.Isbn, dto.Title, dto.Description, dto.PublishedDate, dto.AuthorIds, dto.GenreIds);

            var response = await _bookService.ReplaceBookAsync(replaceBookCommand);

            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            return Ok(response.Value!.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _bookService.DeleteBookAsync(id);

            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, PatchBookDto dto)
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
                return response.ToActionResult(this);
            }
            return Ok(response.Value!.ToDto());
        }

    }
}
