using Microsoft.AspNetCore.Mvc;
using Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/genres")]

    public class GenresController : ControllerBase
    {
        private readonly LibraryContext _context;

        public GenresController(LibraryContext context)
        {
            _context = context;
        }

        // GET api/genres
        [HttpGet]
        public IActionResult Get()
        {
            var genres = _context.Genres
                .AsNoTracking()
                .OrderBy(g => g.Name)
                .Select(g => new GenreDto  
                {
                    Id = g.Id,
                    Name = g.Name,
                    ParentGenreId = g.ParentGenreId
                })
                .ToList();
            return Ok(genres);
        }

        // GET api/genres/{id}
        [HttpGet("{id}")]
        public ActionResult<GenreDto> GetById(int id)
        {
            var genre = _context.Genres
                .AsNoTracking()
                .Where(g => g.Id == id)
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    ParentGenreId = g.ParentGenreId
                })
                .FirstOrDefault();
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }
    }
}
