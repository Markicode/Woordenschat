using Application.Dtos;
using Application.Dtos.Genres;
using Application.Enums;
using Application.Mappings;
using Application.Services;
using Application.Services.Genres;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/genres")]

    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET api/genres
        [HttpGet]
        public async Task<ActionResult<List<GenreWithParentDto>>> Get()
        {
            var response = await _genreService.GetGenresAsync();

            var genres = response.Value!
                .Select(g => g.ToWithParentDto())
                .ToList();

            return Ok(genres);
        }

        // GET api/genres/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreWithParentDto>> GetById(int id)
        {
            var response =  await _genreService.GetGenreByIdAsync(id);

            if(!response.IsSuccess)
            {
                return response.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(response.ErrorMessage),
                    _ => BadRequest()
                };
            }
 
            return Ok(response.Value!.ToWithParentDto());
        }
    }
}
