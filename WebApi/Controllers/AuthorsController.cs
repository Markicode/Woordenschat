
using Application.Common;
using Application.Dtos.Authors;
using Application.Dtos.Books;
using Application.Mappings;
using Application.Services.Authors;
using Application.Services.Books;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebApi.Extensions;
using WebApi.Helpers;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/authors")]

    public class AuthorsController : BaseApiController
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var response = await _authorService.GetAuthorsAsync();

            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            var authors = response.Value!;
            var authorDtos = authors
                .Select(a => a.ToDto())
                .ToList();
            return Ok(authorDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var response = await _authorService.GetAuthorByIdAsync(id);
            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }
            var requestedAuthor = response.Value!;
            return Ok(requestedAuthor.ToWithBooksDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto dto)
        {
            var createAuthorCommand = new CreateAuthorCommand(dto.FirstName, dto.LastName, dto.BirthDate, dto.Bio);
            var response = await _authorService.CreateAuthorAsync(createAuthorCommand);

            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            var createdAuthor = response.Value!;
            return CreatedAtAction(
                nameof(GetAuthorById),
                new { id = createdAuthor.Id },
                createdAuthor.ToDto()
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ReplaceAuthor(int id, [FromBody] ReplaceAuthorDto dto)
        {
            var replaceAuthorCommand = new ReplaceAuthorCommand(id, dto.FirstName, dto.LastName, dto.BirthDate, dto.Bio, dto.Books);
            var response = await _authorService.ReplaceAuthorAsync(replaceAuthorCommand);

            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            return Ok(response.Value.ToWithBooksDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var response = await _authorService.DeleteAuthorAsync(id);
            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAuthor(int id, [FromBody] PatchAuthorDto dto)
        {
            Optional<DateOnly?> birthDate = default;
            var bio = PatchParsingHelper.ParseOptional(dto.Bio, e => e.GetString());

            try
            {
                birthDate = PatchParsingHelper.ParseOptional(
                    dto.BirthDate,
                    e =>
                    {
                        if (!DateOnly.TryParseExact(
                            e.GetString(),
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out var date))
                        {
                            throw new FormatException("Invalid date format. Use yyyy-MM-dd.");
                        }

                        return (DateOnly?)date;
                    });
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }

            var patchAuthorCommand = new PatchAuthorCommand(id, dto.FirstName, dto.LastName, bio, birthDate);
            var response = await _authorService.PatchAuthorAsync(patchAuthorCommand);

            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }
            return Ok(response.Value!.ToDto());
        }

    }
}
