
using Application.Mappings;
using Application.Services.Authors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;


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
        public async Task<IActionResult> Get()
        {
            var response = await _authorService.GetAuthorsAsync();

            if(!response.IsSuccess)
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
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _authorService.GetAuthorByIdAsync(id);
            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }
            var requestedAuthor = response.Value!;
            return Ok(requestedAuthor.ToWithBooksDto());
        }

    }
}
