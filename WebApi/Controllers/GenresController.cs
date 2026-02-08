
using Application.Dtos.Genres;
using Application.Mappings;
using Application.Services.Genres;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/genres")]

    public class GenresController : BaseApiController
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET api/genres
        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var response = await _genreService.GetGenresAsync();

            if(!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            var genres = response.Value!
                .Select(g => g.ToWithParentDto())
                .ToList();

            return Ok(genres);
        }

        // GET api/genres/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var response =  await _genreService.GetGenreByIdAsync(id);

            if(!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }
 
            return Ok(response.Value!.ToWithParentDto());
        }

        // POST api/genres
        [HttpPost]
        public async Task<IActionResult> CreateGenre(CreateGenreDto createGenreDto)
        {
            CreateGenreCommand createGenreCommand = new CreateGenreCommand(createGenreDto.Name, createGenreDto.ParentGenreId);

            var response = await _genreService.CreateGenreAsync(createGenreCommand);

            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            var genreDto = response.Value!.ToWithParentDto();

            return CreatedAtAction(nameof(GetGenreById), new { id = genreDto.Id }, genreDto);
        }
    }
}
